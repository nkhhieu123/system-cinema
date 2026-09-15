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

        // Phí giữ lại của vé đã hoàn = giá vé - số tiền trả khách (vé hoàn trước khi có phí hoàn thì RefundAmount = NULL -> 0)
        private const string PhiHoanSql =
            "CASE WHEN b.IsBooked = 0 THEN ISNULL(b.Price, 0) - ISNULL(b.RefundAmount, ISNULL(b.Price, 0)) ELSE 0 END";

        public thong_ke()
        {
            InitializeComponent();
            cbNhom.Items.AddRange(new object[] { "Vé theo phim", "Vé theo ngày", "Bắp nước theo món" });
            cbNhom.SelectedIndex = 0;
            dtpTu.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpDen.Value = DateTime.Today;

            dgvThongKe.DataBindingComplete += (s, e) =>
            {
                string[,] headers =
                {
                    { "Phim", "Phim" }, { "Ngay", "Ngày" }, { "Mon", "Món" }, { "SoVe", "Số vé bán" },
                    { "VeHoan", "Vé đã hoàn" }, { "PhiHoan", "Phí hoàn thu được" }, { "SoLuong", "Số lượng" },
                    { "DoUong", "Bắp nước" }, { "DoanhThu", "Doanh thu" }
                };
                for (int i = 0; i < headers.GetLength(0); i++)
                {
                    DataGridViewColumn col = dgvThongKe.Columns[headers[i, 0]];
                    if (col == null)
                        continue;
                    col.HeaderText = headers[i, 1];
                    if (headers[i, 0] == "Ngay")
                        col.DefaultCellStyle.Format = "dd/MM/yyyy";
                    if (headers[i, 0] == "DoanhThu" || headers[i, 0] == "PhiHoan" || headers[i, 0] == "DoUong")
                    {
                        col.DefaultCellStyle.FormatProvider = VietNam;
                        col.DefaultCellStyle.Format = "c0";
                    }
                }
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

                // Tổng quan vé: doanh thu vé = vé còn hiệu lực + phí giữ lại của vé đã hoàn
                decimal doanhThuVe, doanhThuDoUong;
                SqlCommand tong = new SqlCommand(
                    @"SELECT ISNULL(SUM(CASE WHEN IsBooked = 1 THEN 1 ELSE 0 END), 0) AS SoVe,
                             ISNULL(SUM(CASE WHEN IsBooked = 1 THEN Price ELSE 0 END), 0) AS TienVe,
                             ISNULL(SUM(CASE WHEN IsBooked = 0 THEN 1 ELSE 0 END), 0) AS VeHoan,
                             ISNULL(SUM(" + PhiHoanSql + @"), 0) AS PhiHoan
                      FROM BookedSeats b
                      WHERE b.BookedAt >= @tu AND b.BookedAt < @den", conn);
                AddRangeParameters(tong);
                using (SqlDataReader r = tong.ExecuteReader())
                {
                    r.Read();
                    decimal phiHoan = Convert.ToDecimal(r["PhiHoan"]);
                    doanhThuVe = Convert.ToDecimal(r["TienVe"]) + phiHoan;
                    lblSoVe.Text = "Vé đã bán: " + Convert.ToInt32(r["SoVe"]).ToString("N0", VietNam);
                    lblDoanhThu.Text = "Doanh thu vé: " + doanhThuVe.ToString("N0", VietNam) + " đ";
                    lblVeHoan.Text = "Vé đã hoàn: " + Convert.ToInt32(r["VeHoan"]).ToString("N0", VietNam)
                                     + " (phí thu " + phiHoan.ToString("N0", VietNam) + " đ)";
                }

                SqlCommand doUong = new SqlCommand(
                    "SELECT COUNT(*), ISNULL(SUM(TongTien), 0) FROM DonDoUong WHERE NgayDat >= @tu AND NgayDat < @den", conn);
                AddRangeParameters(doUong);
                using (SqlDataReader r = doUong.ExecuteReader())
                {
                    r.Read();
                    doanhThuDoUong = r.GetDecimal(1);
                    lblDoUong.Text = "Bắp nước: " + doanhThuDoUong.ToString("N0", VietNam) + " đ (" + r.GetInt32(0) + " đơn)";
                }
                lblTongDoanhThu.Text = "TỔNG DOANH THU: " + (doanhThuVe + doanhThuDoUong).ToString("N0", VietNam) + " đ";

                SqlCommand suat = new SqlCommand(
                    "SELECT COUNT(*) FROM Showtimes WHERE ShowDate >= @tu AND ShowDate < @den", conn);
                AddRangeParameters(suat);
                lblSuatChieu.Text = "Suất chiếu: " + ((int)suat.ExecuteScalar()).ToString("N0", VietNam);

                // Chi tiết
                string sql;
                if (cbNhom.SelectedIndex == 1)
                {
                    sql = @"SELECT CAST(b.BookedAt AS date) AS Ngay,
                                   SUM(CASE WHEN b.IsBooked = 1 THEN 1 ELSE 0 END) AS SoVe,
                                   SUM(CASE WHEN b.IsBooked = 0 THEN 1 ELSE 0 END) AS VeHoan,
                                   ISNULL(SUM(" + PhiHoanSql + @"), 0) AS PhiHoan,
                                   ISNULL(SUM(CASE WHEN b.IsBooked = 1 THEN b.Price ELSE 0 END), 0) + ISNULL(SUM(" + PhiHoanSql + @"), 0) AS DoanhThu
                            FROM BookedSeats b
                            WHERE b.BookedAt >= @tu AND b.BookedAt < @den
                            GROUP BY CAST(b.BookedAt AS date)
                            ORDER BY Ngay";
                }
                else if (cbNhom.SelectedIndex == 2)
                {
                    sql = @"SELECT d.TenDoUong AS Mon, SUM(ct.SoLuong) AS SoLuong, SUM(ct.SoLuong * ct.DonGia) AS DoanhThu
                            FROM ChiTietDonDoUong ct
                            JOIN DonDoUong dn ON ct.IDDon = dn.IDDon
                            JOIN DoUong d ON ct.IDDoUong = d.IDDoUong
                            WHERE dn.NgayDat >= @tu AND dn.NgayDat < @den
                            GROUP BY d.TenDoUong
                            ORDER BY DoanhThu DESC";
                }
                else
                {
                    sql = @"SELECT m.MovieName AS Phim,
                                   SUM(CASE WHEN b.IsBooked = 1 THEN 1 ELSE 0 END) AS SoVe,
                                   SUM(CASE WHEN b.IsBooked = 0 THEN 1 ELSE 0 END) AS VeHoan,
                                   ISNULL(SUM(" + PhiHoanSql + @"), 0) AS PhiHoan,
                                   ISNULL(SUM(CASE WHEN b.IsBooked = 1 THEN b.Price ELSE 0 END), 0) + ISNULL(SUM(" + PhiHoanSql + @"), 0) AS DoanhThu
                            FROM BookedSeats b
                            JOIN Showtimes s ON b.ShowtimeID = s.ShowtimeID
                            JOIN Movies m ON s.MovieID = m.MovieID
                            WHERE b.BookedAt >= @tu AND b.BookedAt < @den
                            GROUP BY m.MovieName
                            ORDER BY DoanhThu DESC";
                }

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
