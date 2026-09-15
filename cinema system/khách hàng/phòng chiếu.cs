using cinema_system;
using cinema_system.nhân_viên;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace rạp_chiếu_phim.khách_hàng
{
    // Sơ đồ ghế của một suất chiếu: chọn ghế, tính tiền và lưu vé vào bảng BookedSeats
    public partial class phòng_chiếu : Form
    {
        // Giá ghế thường = giá vé của phim; phụ thu VIP / Sweetbox lấy từ Cài đặt (admin chỉnh được)
        private readonly AppSettings settings;

        private static readonly Color MauDangChon = Color.Brown;
        private static readonly Color MauDaDat = Color.Gray;

        private class Ghe
        {
            public int SeatID;
            public string Ten;
            public string Loai;
        }

        private readonly int showtimeId;
        private int roomId;
        private DateTime batDau;
        private decimal giaThuong;

        // Các nút ghế đang chọn, theo thứ tự bấm
        private readonly List<Button> gheDaChon = new List<Button>();

        public phòng_chiếu(int showtimeId)
        {
            InitializeComponent();
            this.showtimeId = showtimeId;
            settings = AppSettings.Load();
            giaThuong = settings.GiaVeMacDinh;

            // Khách chưa đăng nhập: xem được ghế trống, bấm nút để chuyển sang đăng nhập
            if (Session.IDTaiKhoan == null)
            {
                btnNext.Text = "ĐĂNG NHẬP ĐỂ ĐẶT VÉ";
                btnNext.Width = 230;
            }

            // Danh sách ghế dài thì cắt bớt, không đè lên phần chú thích
            lblDanhSachGhe.AutoSize = false;
            lblDanhSachGhe.AutoEllipsis = true;
            lblDanhSachGhe.Size = new Size(290, 28);

            LoadSuatChieu();
            LoadPrice();
            VeSoDoGhe();
        }

        private void LoadSuatChieu()
        {
            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT m.MovieName, m.Price, s.ShowDate, s.ShowTime, s.RoomID, r.RoomName
                      FROM Showtimes s
                      JOIN Movies m ON s.MovieID = m.MovieID
                      LEFT JOIN Rooms r ON s.RoomID = r.RoomID
                      WHERE s.ShowtimeID = @id", conn);
                cmd.Parameters.AddWithValue("@id", showtimeId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new InvalidOperationException("Không tìm thấy suất chiếu (có thể đã bị xóa).");
                    if (reader["RoomID"] == DBNull.Value)
                        throw new InvalidOperationException("Suất chiếu này chưa được gán phòng chiếu.");

                    roomId = Convert.ToInt32(reader["RoomID"]);
                    batDau = Convert.ToDateTime(reader["ShowDate"]).Date + (TimeSpan)reader["ShowTime"];
                    if (reader["Price"] != DBNull.Value && Convert.ToDecimal(reader["Price"]) > 0)
                        giaThuong = Convert.ToDecimal(reader["Price"]);

                    string tenPhong = reader["RoomName"].ToString();
                    Text = $"Đặt vé - {reader["MovieName"]} - {tenPhong} - {batDau:HH:mm dd/MM/yyyy}";
                    lblScreen.Text = "SCREEN  -  " + tenPhong;
                }
            }
        }

        private decimal GiaGhe(string loai)
        {
            if (loai == QuanLyPhong.SeatVIP)
                return giaThuong + settings.PhuThuVIP;
            if (loai == QuanLyPhong.SeatSweetbox)
                return giaThuong + settings.PhuThuSweetbox;
            return giaThuong;
        }

        private static Color MauGhe(string loai)
        {
            if (loai == QuanLyPhong.SeatVIP)
                return Color.Orange;
            if (loai == QuanLyPhong.SeatSweetbox)
                return Color.HotPink;
            return Color.LightGreen;
        }

        private void LoadPrice()
        {
            // Cập nhật chú thích theo giá của phim
            legendPanel.Controls.Clear();
            AddLegendItem(Color.LightGreen, $"Ghế Thường - {GiaGhe(QuanLyPhong.SeatThuong):N0} đ");
            AddLegendItem(Color.Orange, $"Ghế VIP - {GiaGhe(QuanLyPhong.SeatVIP):N0} đ");
            AddLegendItem(Color.HotPink, $"Ghế Sweetbox - {GiaGhe(QuanLyPhong.SeatSweetbox):N0} đ");
            AddLegendItem(MauDangChon, "Ghế đang chọn");
            AddLegendItem(MauDaDat, "Ghế đã đặt");
        }

        private void AddLegendItem(Color color, string text)
        {
            Panel colorBox = new Panel()
            {
                BackColor = color,
                Size = new Size(20, 20),
                Margin = new Padding(8, 8, 3, 3)
            };
            Label lbl = new Label()
            {
                Text = text,
                AutoSize = true,
                Margin = new Padding(3, 6, 8, 3)
            };
            FlowLayoutPanel item = new FlowLayoutPanel() { AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
            item.Controls.Add(colorBox);
            item.Controls.Add(lbl);
            this.legendPanel.Controls.Add(item);
        }

        private void VeSoDoGhe()
        {
            while (panelGhe.Controls.Count > 0)
                panelGhe.Controls[0].Dispose();
            gheDaChon.Clear();

            int startX = 60;
            int startY = 20;
            int size = 60;
            int spacing = 5;

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();

                // Phòng chưa có ghế trong DB (vd: phòng tạo từ script) thì tạo sơ đồ mặc định
                QuanLyPhong.EnsureDefaultSeats(conn, roomId);

                HashSet<int> daDat = new HashSet<int>();
                SqlCommand cmdDat = new SqlCommand(
                    "SELECT SeatID FROM BookedSeats WHERE ShowtimeID = @st AND IsBooked = 1", conn);
                cmdDat.Parameters.AddWithValue("@st", showtimeId);
                using (SqlDataReader r = cmdDat.ExecuteReader())
                {
                    while (r.Read())
                        daDat.Add(r.GetInt32(0));
                }

                SqlCommand cmdGhe = new SqlCommand(
                    "SELECT SeatID, SeatName, SeatType FROM Seats WHERE RoomID = @room ORDER BY SeatID", conn);
                cmdGhe.Parameters.AddWithValue("@room", roomId);
                using (SqlDataReader r = cmdGhe.ExecuteReader())
                {
                    int index = 0;
                    while (r.Read())
                    {
                        Ghe ghe = new Ghe
                        {
                            SeatID = r.GetInt32(0),
                            Ten = r.IsDBNull(1) ? "?" : r.GetString(1),
                            Loai = r.IsDBNull(2) ? QuanLyPhong.SeatThuong : r.GetString(2)
                        };

                        if (ghe.Loai == QuanLyPhong.SeatKhongDung)
                        {
                            index++;
                            continue; // lối đi / ghế không sử dụng: để trống
                        }

                        if (!QuanLyPhong.TryParseSeatName(ghe.Ten, out int hang, out int cot))
                        {
                            hang = index / QuanLyPhong.SeatColumns;
                            cot = index % QuanLyPhong.SeatColumns;
                        }
                        index++;

                        Button btn = new Button();
                        btn.Width = size;
                        btn.Height = size;
                        btn.Left = startX + cot * (size + spacing);
                        btn.Top = startY + hang * (size + spacing);
                        btn.Text = ghe.Ten;
                        btn.Tag = ghe;

                        if (daDat.Contains(ghe.SeatID))
                        {
                            btn.BackColor = MauDaDat;
                            btn.Enabled = false;
                        }
                        else
                        {
                            btn.BackColor = MauGhe(ghe.Loai);
                            btn.Click += Ghe_Click;
                        }

                        panelGhe.Controls.Add(btn);
                    }
                }
            }

            CapNhatThongTin();
        }

        private void Ghe_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Ghe ghe = (Ghe)btn.Tag;

            if (gheDaChon.Contains(btn)) // đang chọn -> bỏ chọn
            {
                gheDaChon.Remove(btn);
                btn.BackColor = MauGhe(ghe.Loai);
            }
            else
            {
                gheDaChon.Add(btn);
                btn.BackColor = MauDangChon;
            }

            CapNhatThongTin();
        }

        private List<Ghe> GheDangChon()
        {
            return gheDaChon.Select(b => (Ghe)b.Tag).ToList();
        }

        private void CapNhatThongTin()
        {
            List<Ghe> ds = GheDangChon();
            lblDanhSachGhe.Text = ds.Count > 0
                ? $"Ghế ({ds.Count}): " + string.Join(", ", ds.Select(g => g.Ten))
                : "Ghế: (chưa chọn)";
            lblTongTien.Text = $"Tổng tiền: {ds.Sum(g => GiaGhe(g.Loai)):N0} đ";
            btnNext.Enabled = ds.Count > 0 || Session.IDTaiKhoan == null;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Đặt vé: lưu từng ghế vào BookedSeats trong một transaction
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Session.IDTaiKhoan == null)
            {
                DialogResult = DialogResult.Retry; // nơi mở form sẽ chuyển sang màn hình đăng nhập
                Close();
                return;
            }

            List<Ghe> ds = GheDangChon();
            if (ds.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một ghế.", "Thông báo");
                return;
            }
            if (batDau <= DateTime.Now)
            {
                MessageBox.Show("Suất chiếu đã bắt đầu, không thể đặt vé.", "Thông báo");
                return;
            }

            string danhSach = string.Join(", ", ds.Select(g => g.Ten));
            decimal tong = ds.Sum(g => GiaGhe(g.Loai));
            if (MessageBox.Show($"Đặt {ds.Count} ghế: {danhSach}\nTổng tiền: {tong:N0} đ\n\nXác nhận đặt vé?",
                    "Xác nhận đặt vé", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string gheBiTrung = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        foreach (Ghe ghe in ds)
                        {
                            // Khóa để 2 người không đặt cùng một ghế cùng lúc
                            SqlCommand check = new SqlCommand(
                                @"SELECT COUNT(*) FROM BookedSeats WITH (UPDLOCK, HOLDLOCK)
                                  WHERE ShowtimeID = @st AND SeatID = @seat AND IsBooked = 1", conn, tran);
                            check.Parameters.AddWithValue("@st", showtimeId);
                            check.Parameters.AddWithValue("@seat", ghe.SeatID);
                            if ((int)check.ExecuteScalar() > 0)
                            {
                                gheBiTrung = ghe.Ten;
                                break;
                            }

                            SqlCommand insert = new SqlCommand(
                                @"INSERT INTO BookedSeats (ShowtimeID, SeatID, IsBooked, IDTaiKhoan, Price, BookedAt)
                                  VALUES (@st, @seat, 1, @tk, @price, GETDATE())", conn, tran);
                            insert.Parameters.AddWithValue("@st", showtimeId);
                            insert.Parameters.AddWithValue("@seat", ghe.SeatID);
                            insert.Parameters.AddWithValue("@tk", Session.IDTaiKhoan.Value);
                            insert.Parameters.AddWithValue("@price", GiaGhe(ghe.Loai));
                            insert.ExecuteNonQuery();
                        }

                        if (gheBiTrung == null)
                            tran.Commit();
                        else
                            tran.Rollback();
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627) // trùng index ghế đã đặt
            {
                gheBiTrung = danhSach;
            }

            if (gheBiTrung != null)
            {
                MessageBox.Show($"Ghế {gheBiTrung} vừa được người khác đặt. Vui lòng chọn lại.", "Thông báo");
                VeSoDoGhe();
                return;
            }

            if (MessageBox.Show($"Đặt vé thành công!\nGhế: {danhSach}\nTổng tiền: {tong:N0} đ\n\n" +
                                "Bạn có muốn đặt thêm bắp nước cho suất chiếu này không?", "Thành công",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                using (drink f = new drink(showtimeId))
                {
                    f.ShowDialog(this);
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
