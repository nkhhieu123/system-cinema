using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace cinema_system.nhân_viên
{
    // Chỉnh sơ đồ ghế của một phòng: đổi loại ghế, đánh dấu lối đi / ghế không dùng, tạo lại số hàng x số ghế
    public partial class SoDoGhe : Form
    {
        private const int SeatWidth = 46;
        private const int SeatHeight = 40;
        private const int Spacing = 4;
        private const int Left0 = 52;
        private const int Top0 = 15;

        private readonly int roomId;
        private bool daCoVe; // phòng đã từng bán vé (kể cả vé đã hoàn) thì không được tạo lại sơ đồ

        private readonly Dictionary<int, string> loaiBanDau = new Dictionary<int, string>();
        private readonly Dictionary<int, string> loaiHienTai = new Dictionary<int, string>();
        private readonly Dictionary<int, int> hangCuaGhe = new Dictionary<int, int>();
        private readonly Dictionary<int, Button> nutGhe = new Dictionary<int, Button>();

        public SoDoGhe(int roomId, string tenPhong)
        {
            InitializeComponent();
            this.roomId = roomId;
            Text = "Sơ đồ ghế - " + tenPhong;
            rbThuong.Checked = true;
            LoadSeats();
        }

        private static Color MauGhe(string loai)
        {
            switch (loai)
            {
                case QuanLyPhong.SeatVIP: return Color.Orange;
                case QuanLyPhong.SeatSweetbox: return Color.HotPink;
                case QuanLyPhong.SeatKhongDung: return Color.Gainsboro;
                default: return Color.LightGreen;
            }
        }

        private string LoaiDangChon()
        {
            if (rbVIP.Checked) return QuanLyPhong.SeatVIP;
            if (rbSweetbox.Checked) return QuanLyPhong.SeatSweetbox;
            if (rbKhongDung.Checked) return QuanLyPhong.SeatKhongDung;
            return QuanLyPhong.SeatThuong;
        }

        private bool CoThayDoi()
        {
            return loaiHienTai.Any(kv => loaiBanDau[kv.Key] != kv.Value);
        }

        private void LoadSeats()
        {
            while (panelGhe.Controls.Count > 0)
                panelGhe.Controls[0].Dispose();
            loaiBanDau.Clear();
            loaiHienTai.Clear();
            hangCuaGhe.Clear();
            nutGhe.Clear();

            int soHang = 0, soCot = 0;

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                QuanLyPhong.EnsureDefaultSeats(conn, roomId);

                SqlCommand coVe = new SqlCommand(
                    "SELECT COUNT(*) FROM BookedSeats b JOIN Seats s ON b.SeatID = s.SeatID WHERE s.RoomID = @room", conn);
                coVe.Parameters.AddWithValue("@room", roomId);
                daCoVe = (int)coVe.ExecuteScalar() > 0;

                SqlCommand cmd = new SqlCommand(
                    "SELECT SeatID, SeatName, SeatType FROM Seats WHERE RoomID = @room ORDER BY SeatID", conn);
                cmd.Parameters.AddWithValue("@room", roomId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    int index = 0;
                    while (r.Read())
                    {
                        int seatId = r.GetInt32(0);
                        string ten = r.IsDBNull(1) ? "?" : r.GetString(1);
                        string loai = r.IsDBNull(2) ? QuanLyPhong.SeatThuong : r.GetString(2);

                        if (!QuanLyPhong.TryParseSeatName(ten, out int hang, out int cot))
                        {
                            hang = index / QuanLyPhong.SeatColumns;
                            cot = index % QuanLyPhong.SeatColumns;
                        }
                        index++;

                        Button btn = new Button();
                        btn.Size = new Size(SeatWidth, SeatHeight);
                        btn.Location = new Point(Left0 + cot * (SeatWidth + Spacing), Top0 + hang * (SeatHeight + Spacing));
                        btn.Text = ten;
                        btn.Tag = seatId;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.Font = new Font("Segoe UI", 8F);
                        btn.Click += Ghe_Click;
                        panelGhe.Controls.Add(btn);

                        loaiBanDau[seatId] = loai;
                        loaiHienTai[seatId] = loai;
                        hangCuaGhe[seatId] = hang;
                        nutGhe[seatId] = btn;
                        CapNhatMau(seatId);

                        soHang = Math.Max(soHang, hang + 1);
                        soCot = Math.Max(soCot, cot + 1);
                    }
                }
            }

            // Nút chữ cái đầu hàng: bấm để đổi cả hàng
            for (int h = 0; h < soHang; h++)
            {
                Button rowBtn = new Button();
                rowBtn.Size = new Size(36, SeatHeight);
                rowBtn.Location = new Point(8, Top0 + h * (SeatHeight + Spacing));
                rowBtn.Text = ((char)('A' + h)).ToString();
                rowBtn.Tag = h;
                rowBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                rowBtn.Click += Hang_Click;
                panelGhe.Controls.Add(rowBtn);
            }

            numHang.Value = Math.Max(numHang.Minimum, Math.Min(numHang.Maximum, soHang));
            numCot.Value = Math.Max(numCot.Minimum, Math.Min(numCot.Maximum, soCot));
            btnTaoLai.Enabled = !daCoVe;
            lblHuongDan.Text = "Chọn loại ghế ở trên rồi bấm vào ghế, hoặc bấm chữ cái đầu hàng để đổi cả hàng. "
                               + (daCoVe ? "Phòng đã có vé nên không thể tạo lại sơ đồ (vẫn đổi được loại ghế)." : "");
        }

        private void CapNhatMau(int seatId)
        {
            Button btn = nutGhe[seatId];
            string loai = loaiHienTai[seatId];
            btn.BackColor = MauGhe(loai);
            btn.ForeColor = loai == QuanLyPhong.SeatKhongDung ? Color.Gray : Color.Black;
            // Ghế đã đổi nhưng chưa lưu thì viền đỏ
            btn.FlatAppearance.BorderColor = loaiBanDau[seatId] != loai ? Color.Red : Color.DimGray;
            btn.FlatAppearance.BorderSize = loaiBanDau[seatId] != loai ? 2 : 1;
        }

        private void Ghe_Click(object sender, EventArgs e)
        {
            int seatId = (int)((Button)sender).Tag;
            loaiHienTai[seatId] = LoaiDangChon();
            CapNhatMau(seatId);
        }

        private void Hang_Click(object sender, EventArgs e)
        {
            int hang = (int)((Button)sender).Tag;
            string loai = LoaiDangChon();
            foreach (int seatId in hangCuaGhe.Where(kv => kv.Value == hang).Select(kv => kv.Key).ToList())
            {
                loaiHienTai[seatId] = loai;
                CapNhatMau(seatId);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            List<KeyValuePair<int, string>> thayDoi = loaiHienTai.Where(kv => loaiBanDau[kv.Key] != kv.Value).ToList();
            if (thayDoi.Count == 0)
            {
                MessageBox.Show("Không có thay đổi nào.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();

                // Ghế đang có vé của suất chưa chiếu thì chưa được chuyển sang "không dùng"
                List<string> gheCoVe = new List<string>();
                foreach (var kv in thayDoi.Where(kv => kv.Value == QuanLyPhong.SeatKhongDung))
                {
                    SqlCommand check = new SqlCommand(
                        @"SELECT COUNT(*) FROM BookedSeats b JOIN Showtimes s ON b.ShowtimeID = s.ShowtimeID
                          WHERE b.SeatID = @seat AND b.IsBooked = 1
                            AND CAST(s.ShowDate AS datetime) + CAST(s.ShowTime AS datetime) > GETDATE()", conn);
                    check.Parameters.AddWithValue("@seat", kv.Key);
                    if ((int)check.ExecuteScalar() > 0)
                        gheCoVe.Add(nutGhe[kv.Key].Text);
                }
                if (gheCoVe.Count > 0)
                {
                    MessageBox.Show("Các ghế sau đang có vé của suất chưa chiếu nên chưa thể chuyển sang \"Không dùng\": "
                                    + string.Join(", ", gheCoVe), "Không thể lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    foreach (var kv in thayDoi)
                    {
                        SqlCommand update = new SqlCommand(
                            "UPDATE Seats SET SeatType = @type WHERE SeatID = @id AND RoomID = @room", conn, tran);
                        update.Parameters.AddWithValue("@type", kv.Value);
                        update.Parameters.AddWithValue("@id", kv.Key);
                        update.Parameters.AddWithValue("@room", roomId);
                        update.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
            }

            MessageBox.Show($"Đã lưu {thayDoi.Count} ghế. Vé đã bán giữ nguyên giá lúc đặt.", "Thành công");
            LoadSeats();
        }

        private void btnTaoLai_Click(object sender, EventArgs e)
        {
            int soHang = (int)numHang.Value;
            int soCot = (int)numCot.Value;
            if (MessageBox.Show($"Tạo lại sơ đồ {soHang} hàng x {soCot} ghế?\nLoại ghế sẽ được đặt lại theo mặc định và các thay đổi chưa lưu sẽ mất.",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        // Xóa ghế cũ; nếu ghế đã có vé thì khóa ngoại sẽ chặn lại
                        SqlCommand delete = new SqlCommand("DELETE FROM Seats WHERE RoomID = @room", conn, tran);
                        delete.Parameters.AddWithValue("@room", roomId);
                        delete.ExecuteNonQuery();

                        QuanLyPhong.CreateSeats(conn, tran, roomId, soHang, soCot);
                        tran.Commit();
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Phòng đã có vé nên không thể tạo lại sơ đồ.");
            }

            LoadSeats();
        }

        private void SoDoGhe_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CoThayDoi() && MessageBox.Show("Có thay đổi chưa lưu. Đóng mà không lưu?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
