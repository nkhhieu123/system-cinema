using cinema_system;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace rạp_chiếu_phim.khách_hàng
{
    // Đặt bắp nước: đặt kèm vé (có suất chiếu) hoặc nhân viên bán tại quầy (không gắn suất chiếu)
    public partial class drink : Form
    {
        private readonly int? showtimeId;
        private readonly List<Order_thức_uống> items = new List<Order_thức_uống>();

        public drink() : this(null)
        {
        }

        public drink(int? showtimeId)
        {
            InitializeComponent();
            this.showtimeId = showtimeId;
            LoadMenu();
            CapNhatTongTien();
        }

        private void LoadMenu()
        {
            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();

                if (showtimeId.HasValue)
                {
                    SqlCommand info = new SqlCommand(
                        @"SELECT m.MovieName, s.ShowDate, s.ShowTime FROM Showtimes s JOIN Movies m ON s.MovieID = m.MovieID
                          WHERE s.ShowtimeID = @id", conn);
                    info.Parameters.AddWithValue("@id", showtimeId.Value);
                    using (SqlDataReader r = info.ExecuteReader())
                    {
                        if (r.Read())
                            lblTieuDe.Text = $"ĐẶT BẮP NƯỚC - {r.GetString(0)} ({r.GetDateTime(1).Date + r.GetTimeSpan(2):HH:mm dd/MM/yyyy})";
                    }
                }
                else
                {
                    lblTieuDe.Text = "BÁN BẮP NƯỚC TẠI QUẦY";
                }

                SqlCommand cmd = new SqlCommand(
                    "SELECT IDDoUong, TenDoUong, MoTa, Gia, HinhAnh FROM DoUong WHERE DangBan = 1 ORDER BY IDDoUong", conn);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        Order_thức_uống item = new Order_thức_uống();
                        item.SetThongTin(r.GetInt32(0), r.GetString(1), r.IsDBNull(2) ? "" : r.GetString(2),
                                         r.GetDecimal(3), PosterImage.Load(r["HinhAnh"], null));
                        item.Margin = new Padding(8);
                        item.SoLuongThayDoi += (s, e) => CapNhatTongTien();
                        flpMonAn.Controls.Add(item);
                        items.Add(item);
                    }
                }
            }

            if (items.Count == 0)
            {
                flpMonAn.Controls.Add(new Label
                {
                    Text = "Hiện chưa có món nào đang bán.",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11, FontStyle.Italic),
                    Margin = new Padding(10)
                });
            }
        }

        private void CapNhatTongTien()
        {
            decimal tong = items.Sum(i => i.Gia * i.LaySoLuong());
            lblTongTien.Text = $"Tổng tiền: {tong:N0} đ";
            btnDatHang.Enabled = tong > 0;
        }

        private void btnDatHang_Click(object sender, EventArgs e)
        {
            List<Order_thức_uống> chon = items.Where(i => i.LaySoLuong() > 0).ToList();
            if (chon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một món.");
                return;
            }
            if (Session.IDTaiKhoan == null)
            {
                MessageBox.Show("Vui lòng đăng nhập để đặt bắp nước.");
                return;
            }

            decimal tong = chon.Sum(i => i.Gia * i.LaySoLuong());
            string danhSach = string.Join("\n", chon.Select(i => $"- {i.TenMon} x{i.LaySoLuong()}: {i.Gia * i.LaySoLuong():N0} đ"));
            if (MessageBox.Show($"{danhSach}\n\nTổng tiền: {tong:N0} đ\nXác nhận đặt?", "Xác nhận đơn bắp nước",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int idDon;
            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    SqlCommand don = new SqlCommand(
                        @"INSERT INTO DonDoUong (IDTaiKhoan, ShowtimeID, TongTien, NgayDat) VALUES (@tk, @st, @tong, GETDATE());
                          SELECT CAST(SCOPE_IDENTITY() AS int);", conn, tran);
                    don.Parameters.AddWithValue("@tk", Session.IDTaiKhoan.Value);
                    don.Parameters.AddWithValue("@st", (object)showtimeId ?? DBNull.Value);
                    don.Parameters.AddWithValue("@tong", tong);
                    idDon = (int)don.ExecuteScalar();

                    foreach (Order_thức_uống item in chon)
                    {
                        SqlCommand ct = new SqlCommand(
                            "INSERT INTO ChiTietDonDoUong (IDDon, IDDoUong, SoLuong, DonGia) VALUES (@don, @mon, @sl, @gia)", conn, tran);
                        ct.Parameters.AddWithValue("@don", idDon);
                        ct.Parameters.AddWithValue("@mon", item.IDDoUong);
                        ct.Parameters.AddWithValue("@sl", item.LaySoLuong());
                        ct.Parameters.AddWithValue("@gia", item.Gia);
                        ct.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
            }

            MessageBox.Show($"Đặt bắp nước thành công! Mã đơn: #{idDon}\nTổng tiền: {tong:N0} đ\nVui lòng nhận tại quầy.",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
