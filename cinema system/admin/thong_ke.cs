using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace cinema_system.admin
{
    // Thống kê vé bán / doanh thu theo thời điểm đặt vé (BookedAt)
    public partial class thong_ke : UserControl
    {
        private static readonly CultureInfo VietNam = new CultureInfo("vi-VN");

        public thong_ke()
        {
            InitializeComponent();
            cbNhom.Items.AddRange(new object[] { "Theo phim", "Theo ngày" });
            cbNhom.SelectedIndex = 0;
            dtpTu.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpDen.Value = DateTime.Today;

            dgvThongKe.DataBindingComplete += (s, e) =>
            {
                if (dgvThongKe.Columns["DoanhThu"] == null)
                    return;
                if (dgvThongKe.Columns["Ngay"] != null)
                {
                    dgvThongKe.Columns["Ngay"].HeaderText = "Ngày";
                    dgvThongKe.Columns["Ngay"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvThongKe.Columns["Phim"] != null)
                    dgvThongKe.Columns["Phim"].HeaderText = "Phim";
                dgvThongKe.Columns["SoVe"].HeaderText = "Số vé bán";
                dgvThongKe.Columns["VeHoan"].HeaderText = "Vé đã hoàn";
                dgvThongKe.Columns["DoanhThu"].HeaderText = "Doanh thu";
                dgvThongKe.Columns["DoanhThu"].DefaultCellStyle.FormatProvider = VietNam;
                dgvThongKe.Columns["DoanhThu"].DefaultCellStyle.Format = "c0";
                dgvThongKe.ClearSelection();
            };

            LoadThongKe();
        }

        private void AddRangeParameters(SqlCommand cmd)
        {
            // Khoảng [từ ngày 00:00, đến ngày + 1) để lấy trọn ngày cuối
            cmd.Parameters.Add("@tu", SqlDbType.DateTime).Value = dtpTu.Value.Date;
            cmd.Parameters.Add("@den", SqlDbType.DateTime).Value = dtpDen.Value.Date.AddDays(1);
        }

        private void LoadThongKe()
        {
            if (dtpTu.Value.Date > dtpDen.Value.Date)
            {
                MessageBox.Show("\"Từ ngày\" phải trước hoặc bằng \"Đến ngày\".");
                return;
            }

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();

                // Tổng quan
                SqlCommand tong = new SqlCommand(
                    @"SELECT ISNULL(SUM(CASE WHEN IsBooked = 1 THEN 1 ELSE 0 END), 0) AS SoVe,
                             ISNULL(SUM(CASE WHEN IsBooked = 1 THEN Price ELSE 0 END), 0) AS DoanhThu,
                             ISNULL(SUM(CASE WHEN IsBooked = 0 THEN 1 ELSE 0 END), 0) AS VeHoan
                      FROM BookedSeats
                      WHERE BookedAt >= @tu AND BookedAt < @den", conn);
                AddRangeParameters(tong);
                using (SqlDataReader r = tong.ExecuteReader())
                {
                    r.Read();
                    lblSoVe.Text = "Vé đã bán: " + Convert.ToInt32(r["SoVe"]).ToString("N0", VietNam);
                    lblDoanhThu.Text = "Doanh thu: " + Convert.ToDecimal(r["DoanhThu"]).ToString("N0", VietNam) + " đ";
                    lblVeHoan.Text = "Vé đã hoàn: " + Convert.ToInt32(r["VeHoan"]).ToString("N0", VietNam);
                }

                SqlCommand suat = new SqlCommand(
                    "SELECT COUNT(*) FROM Showtimes WHERE ShowDate >= @tu AND ShowDate < @den", conn);
                AddRangeParameters(suat);
                lblSuatChieu.Text = "Suất chiếu: " + ((int)suat.ExecuteScalar()).ToString("N0", VietNam);

                // Chi tiết theo phim hoặc theo ngày
                string sql = cbNhom.SelectedIndex == 1
                    ? @"SELECT CAST(b.BookedAt AS date) AS Ngay,
                               SUM(CASE WHEN b.IsBooked = 1 THEN 1 ELSE 0 END) AS SoVe,
                               SUM(CASE WHEN b.IsBooked = 0 THEN 1 ELSE 0 END) AS VeHoan,
                               ISNULL(SUM(CASE WHEN b.IsBooked = 1 THEN b.Price ELSE 0 END), 0) AS DoanhThu
                        FROM BookedSeats b
                        WHERE b.BookedAt >= @tu AND b.BookedAt < @den
                        GROUP BY CAST(b.BookedAt AS date)
                        ORDER BY Ngay"
                    : @"SELECT m.MovieName AS Phim,
                               SUM(CASE WHEN b.IsBooked = 1 THEN 1 ELSE 0 END) AS SoVe,
                               SUM(CASE WHEN b.IsBooked = 0 THEN 1 ELSE 0 END) AS VeHoan,
                               ISNULL(SUM(CASE WHEN b.IsBooked = 1 THEN b.Price ELSE 0 END), 0) AS DoanhThu
                        FROM BookedSeats b
                        JOIN Showtimes s ON b.ShowtimeID = s.ShowtimeID
                        JOIN Movies m ON s.MovieID = m.MovieID
                        WHERE b.BookedAt >= @tu AND b.BookedAt < @den
                        GROUP BY m.MovieName
                        ORDER BY DoanhThu DESC";

                SqlCommand chiTiet = new SqlCommand(sql, conn);
                AddRangeParameters(chiTiet);
                DataTable dt = new DataTable();
                new SqlDataAdapter(chiTiet).Fill(dt);
                dgvThongKe.DataSource = null; // đổi nhóm thì cột thay đổi, bind lại từ đầu
                dgvThongKe.DataSource = dt;
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadThongKe();
        }
    }
}
