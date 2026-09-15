using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema_system.admin
{
    // Admin chỉnh các thiết lập nghiệp vụ (bảng CaiDat)
    public partial class cai_dat : UserControl
    {
        public cai_dat()
        {
            InitializeComponent();
            ShowSettings(AppSettings.Load());
        }

        private static decimal Clamp(NumericUpDown num, decimal value)
        {
            return Math.Max(num.Minimum, Math.Min(num.Maximum, value));
        }

        private void ShowSettings(AppSettings s)
        {
            numPhuThuVIP.Value = Clamp(numPhuThuVIP, s.PhuThuVIP);
            numPhuThuSweetbox.Value = Clamp(numPhuThuSweetbox, s.PhuThuSweetbox);
            numGiaVeMacDinh.Value = Clamp(numGiaVeMacDinh, s.GiaVeMacDinh);
            numThoiLuong.Value = Clamp(numThoiLuong, s.ThoiLuongMacDinh);
            numDonPhong.Value = Clamp(numDonPhong, s.ThoiGianDonPhong);
            numPhiHoan.Value = Clamp(numPhiHoan, s.PhiHoanVe);
            numHoanTruoc.Value = Clamp(numHoanTruoc, s.HoanVeTruocPhut);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            AppSettings s = new AppSettings
            {
                PhuThuVIP = numPhuThuVIP.Value,
                PhuThuSweetbox = numPhuThuSweetbox.Value,
                GiaVeMacDinh = numGiaVeMacDinh.Value,
                ThoiLuongMacDinh = (int)numThoiLuong.Value,
                ThoiGianDonPhong = (int)numDonPhong.Value,
                PhiHoanVe = numPhiHoan.Value,
                HoanVeTruocPhut = (int)numHoanTruoc.Value
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
                {
                    conn.Open();
                    s.Save(conn);
                }
            }
            catch (SqlException ex) when (ex.Number == 208) // chưa có bảng CaiDat
            {
                MessageBox.Show("Database chưa có bảng CaiDat. Hãy chạy sql/cap_nhat_csdl.sql rồi thử lại.");
                return;
            }

            MessageBox.Show("Đã lưu cài đặt. Vé và đơn đã bán giữ nguyên giá lúc bán.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            ShowSettings(new AppSettings());
            MessageBox.Show("Đã điền giá trị mặc định. Bấm Lưu để áp dụng.");
        }
    }
}
