using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace cinema_system.nhân_viên
{
    // Hoàn vé: khách hàng chỉ thấy vé của mình, nhân viên thấy và tìm được vé của mọi người.
    // Vé hoàn không bị xóa mà đánh dấu IsBooked = 0: ghế được mở bán lại, dữ liệu vẫn giữ để thống kê.
    public partial class hoan_ve : UserControl
    {
        // Chỉ hoàn được vé của suất chiếu chưa bắt đầu
        private const string SuatChuaChieu = "(s.ShowDate > @homNay OR (s.ShowDate = @homNay AND s.ShowTime > @gio))";

        private readonly bool chiVeCuaToi;

        public hoan_ve(bool chiVeCuaToi)
        {
            InitializeComponent();
            this.chiVeCuaToi = chiVeCuaToi;

            lblTimKiem.Visible = !chiVeCuaToi;
            txtTimKiem.Visible = !chiVeCuaToi;
            btnTim.Visible = !chiVeCuaToi;
            txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    LoadVe();
                }
            };

            dgvVe.DataBindingComplete += (s, e) =>
            {
                if (dgvVe.Columns["BookingID"] == null)
                    return;
                dgvVe.Columns["BookingID"].HeaderText = "Mã vé";
                dgvVe.Columns["MovieName"].HeaderText = "Phim";
                dgvVe.Columns["RoomName"].HeaderText = "Phòng";
                dgvVe.Columns["ShowDate"].HeaderText = "Ngày chiếu";
                dgvVe.Columns["ShowDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvVe.Columns["ShowTime"].HeaderText = "Giờ chiếu";
                dgvVe.Columns["ShowTime"].DefaultCellStyle.Format = "hh\\:mm";
                dgvVe.Columns["SeatName"].HeaderText = "Ghế";
                dgvVe.Columns["Price"].HeaderText = "Giá vé";
                dgvVe.Columns["Price"].DefaultCellStyle.FormatProvider = new CultureInfo("vi-VN");
                dgvVe.Columns["Price"].DefaultCellStyle.Format = "c0";
                dgvVe.Columns["TenDangNhap"].HeaderText = "Người đặt";
                dgvVe.Columns["SDT"].HeaderText = "Sđt";
                dgvVe.Columns["BookedAt"].HeaderText = "Thời điểm đặt";
                dgvVe.Columns["BookedAt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvVe.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvVe.Columns["TenDangNhap"].Visible = !chiVeCuaToi;
                dgvVe.Columns["SDT"].Visible = !chiVeCuaToi;
                dgvVe.ClearSelection();
            };

            LoadVe();
        }

        private static void AddTimeParameters(SqlCommand cmd)
        {
            DateTime now = DateTime.Now;
            cmd.Parameters.Add("@homNay", SqlDbType.Date).Value = now.Date;
            cmd.Parameters.Add("@gio", SqlDbType.Time).Value = now.TimeOfDay;
        }

        private void LoadVe()
        {
            string sql = @"SELECT b.BookingID, m.MovieName, r.RoomName, s.ShowDate, s.ShowTime, se.SeatName, b.Price,
                                  tk.TenDangNhap, tk.SDT, b.BookedAt,
                                  CASE WHEN " + SuatChuaChieu + @" THEN N'Chưa chiếu' ELSE N'Đã chiếu' END AS TrangThai
                           FROM BookedSeats b
                           JOIN Showtimes s ON b.ShowtimeID = s.ShowtimeID
                           JOIN Movies m ON s.MovieID = m.MovieID
                           LEFT JOIN Rooms r ON s.RoomID = r.RoomID
                           LEFT JOIN Seats se ON b.SeatID = se.SeatID
                           LEFT JOIN TaiKhoan tk ON b.IDTaiKhoan = tk.IDTaiKhoan
                           WHERE b.IsBooked = 1";

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand { Connection = conn };
                AddTimeParameters(cmd);

                if (chiVeCuaToi)
                {
                    sql += " AND b.IDTaiKhoan = @tk";
                    cmd.Parameters.AddWithValue("@tk", (object)Session.IDTaiKhoan ?? DBNull.Value);
                }

                string q = txtTimKiem.Text.Trim();
                if (!chiVeCuaToi && q != "")
                {
                    sql += " AND (tk.TenDangNhap LIKE @q OR tk.HoTen LIKE @q OR tk.SDT LIKE @q OR tk.Email LIKE @q OR m.MovieName LIKE @q)";
                    cmd.Parameters.AddWithValue("@q", "%" + q + "%");
                }

                if (chkChuaChieu.Checked)
                    sql += " AND " + SuatChuaChieu;

                cmd.CommandText = sql + " ORDER BY s.ShowDate, s.ShowTime, b.BookingID";

                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);
                dgvVe.DataSource = dt;
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            LoadVe();
        }

        private void chkChuaChieu_CheckedChanged(object sender, EventArgs e)
        {
            LoadVe();
        }

        private void btnHoanVe_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows = dgvVe.SelectedRows.Cast<DataGridViewRow>().ToList();
            if (rows.Count == 0)
            {
                MessageBox.Show("Hãy chọn vé cần hoàn (giữ Ctrl để chọn nhiều vé).");
                return;
            }

            decimal tong = rows.Sum(r => r.Cells["Price"].Value == DBNull.Value ? 0 : Convert.ToDecimal(r.Cells["Price"].Value));
            if (MessageBox.Show($"Hoàn {rows.Count} vé, số tiền trả lại {tong:N0} đ?", "Xác nhận hoàn vé",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int thanhCong = 0;
            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                foreach (DataGridViewRow row in rows)
                {
                    SqlCommand cmd = new SqlCommand(
                        @"UPDATE b SET b.IsBooked = 0
                          FROM BookedSeats b JOIN Showtimes s ON b.ShowtimeID = s.ShowtimeID
                          WHERE b.BookingID = @id AND b.IsBooked = 1 AND " + SuatChuaChieu +
                        (chiVeCuaToi ? " AND b.IDTaiKhoan = @tk" : ""), conn);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(row.Cells["BookingID"].Value));
                    AddTimeParameters(cmd);
                    if (chiVeCuaToi)
                        cmd.Parameters.AddWithValue("@tk", (object)Session.IDTaiKhoan ?? DBNull.Value);
                    thanhCong += cmd.ExecuteNonQuery();
                }
            }

            if (thanhCong == rows.Count)
                MessageBox.Show($"Đã hoàn {thanhCong} vé.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"Đã hoàn {thanhCong}/{rows.Count} vé. Các vé còn lại không hoàn được vì suất chiếu đã bắt đầu hoặc vé đã được hoàn trước đó.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            LoadVe();
        }
    }
}
