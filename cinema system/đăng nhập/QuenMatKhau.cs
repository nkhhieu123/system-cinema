using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace cinema_system.đăng_nhập
{
    // Tìm lại mật khẩu: gửi mã xác nhận (OTP) 6 số tới email của tài khoản, nhập đúng mã thì được đặt mật khẩu mới
    public partial class QuenMatKhau : Form
    {
        private const int HieuLucPhut = 10;
        private const int SoLanNhapToiDa = 5;
        private const int ChoGuiLaiGiay = 60;

        private int idTaiKhoan = -1;
        private string maDaBam;           // chỉ giữ bản băm của mã, không giữ mã gốc
        private DateTime hetHan;
        private DateTime guiLuc = DateTime.MinValue;
        private int soLanSai;
        private bool dangGui;

        public QuenMatKhau()
        {
            InitializeComponent();
            BatBuocXacNhan(false);
        }

        private void BatBuocXacNhan(bool enabled)
        {
            txtMa.Enabled = enabled;
            txtMatKhauMoi.Enabled = enabled;
            txtNhapLai.Enabled = enabled;
            btnDoiMatKhau.Enabled = enabled;
        }

        private static string TaoMa()
        {
            byte[] bytes = new byte[4];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return (BitConverter.ToUInt32(bytes, 0) % 1000000).ToString("D6");
        }

        private async void btnGuiMa_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text.Trim();
            if (taiKhoan == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập, email hoặc số điện thoại.");
                txtTaiKhoan.Focus();
                return;
            }

            int choThem = ChoGuiLaiGiay - (int)(DateTime.Now - guiLuc).TotalSeconds;
            if (choThem > 0)
            {
                MessageBox.Show($"Vui lòng đợi {choThem} giây nữa rồi gửi lại mã.");
                return;
            }

            if (!EmailSender.IsConfigured)
            {
                MessageBox.Show("Ứng dụng chưa được cấu hình máy chủ gửi email (App.config).\n" +
                                "Hãy liên hệ admin để được đặt lại mật khẩu.", "Chưa thể gửi mã");
                return;
            }

            int id;
            string tenDangNhap, email;
            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT TOP 1 IDTaiKhoan, TenDangNhap, Email FROM TaiKhoan
                      WHERE TenDangNhap = @u OR Email = @u OR SDT = @u
                      ORDER BY CASE WHEN TenDangNhap = @u THEN 0 ELSE 1 END", conn);
                cmd.Parameters.AddWithValue("@u", taiKhoan);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                    {
                        MessageBox.Show("Không tìm thấy tài khoản.");
                        return;
                    }
                    id = r.GetInt32(0);
                    tenDangNhap = r.GetString(1);
                    email = r.IsDBNull(2) ? "" : r.GetString(2).Trim();
                }
            }

            if (email == "")
            {
                MessageBox.Show("Tài khoản này chưa có email nên không gửi được mã.\n" +
                                "Hãy liên hệ admin để đặt lại mật khẩu, sau đó bổ sung email trong \"Thay đổi thông tin\".");
                return;
            }

            string ma = TaoMa();
            dangGui = true;
            btnGuiMa.Enabled = false;
            lblTrangThai.Text = "Đang gửi mã xác nhận...";
            try
            {
                await EmailSender.SendAsync(email, "Mã xác nhận đặt lại mật khẩu",
                    $"Xin chào {tenDangNhap},\n\n" +
                    $"Mã xác nhận để đặt lại mật khẩu của bạn là: {ma}\n" +
                    $"Mã có hiệu lực trong {HieuLucPhut} phút.\n\n" +
                    "Nếu bạn không yêu cầu đặt lại mật khẩu, hãy bỏ qua email này.");
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = "";
                MessageBox.Show("Không gửi được email: " + ex.Message, "Lỗi");
                return;
            }
            finally
            {
                dangGui = false;
                btnGuiMa.Enabled = true;
            }

            idTaiKhoan = id;
            maDaBam = PasswordHasher.Hash(ma);
            hetHan = DateTime.Now.AddMinutes(HieuLucPhut);
            guiLuc = DateTime.Now;
            soLanSai = 0;

            lblTrangThai.Text = $"Đã gửi mã tới {EmailSender.Mask(email)}. Mã có hiệu lực {HieuLucPhut} phút.";
            BatBuocXacNhan(true);
            txtMa.Focus();
        }

        private void HuyMa(string thongBao)
        {
            maDaBam = null;
            idTaiKhoan = -1;
            BatBuocXacNhan(false);
            lblTrangThai.Text = "";
            MessageBox.Show(thongBao);
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (maDaBam == null || idTaiKhoan < 0)
            {
                MessageBox.Show("Hãy gửi mã xác nhận trước.");
                return;
            }
            if (DateTime.Now > hetHan)
            {
                HuyMa("Mã xác nhận đã hết hạn. Hãy gửi lại mã.");
                return;
            }

            if (!PasswordHasher.Verify(txtMa.Text.Trim(), maDaBam, out _))
            {
                soLanSai++;
                if (soLanSai >= SoLanNhapToiDa)
                    HuyMa("Nhập sai mã quá nhiều lần. Hãy gửi lại mã.");
                else
                    MessageBox.Show($"Mã xác nhận không đúng (còn {SoLanNhapToiDa - soLanSai} lần thử).");
                return;
            }

            string matKhau = txtMatKhauMoi.Text.Trim();
            if (matKhau == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới.");
                txtMatKhauMoi.Focus();
                return;
            }
            if (matKhau != txtNhapLai.Text.Trim())
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp.");
                txtNhapLai.Focus();
                return;
            }

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE TaiKhoan SET Pass = @pass WHERE IDTaiKhoan = @id", conn);
                cmd.Parameters.AddWithValue("@pass", PasswordHasher.Hash(matKhau));
                cmd.Parameters.AddWithValue("@id", idTaiKhoan);
                cmd.ExecuteNonQuery();
            }

            maDaBam = null;
            MessageBox.Show("Đặt lại mật khẩu thành công! Hãy đăng nhập bằng mật khẩu mới.");
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void QuenMatKhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đang gửi email thì chờ xong mới cho đóng
            if (dangGui)
                e.Cancel = true;
        }
    }
}
