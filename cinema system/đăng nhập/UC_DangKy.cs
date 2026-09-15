using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace cinema_system.đăng_nhập
{
    public partial class UC_Đăng_ký : UserControl
    {
        string conn = Db.ConnectionString;
        private string captchaText;

        public UC_Đăng_ký()
        {
            InitializeComponent();
            LoadNgaySinh();
            GenerateCaptcha();

        }

        // Đổ dữ liệu ngày / tháng / năm sinh (để ở đây vì Designer sẽ xoá code vòng lặp trong InitializeComponent)
        private void LoadNgaySinh()
        {
            for (int i = 1; i <= 31; i++) cbDay.Items.Add(i);
            for (int i = 1; i <= 12; i++) cbMonth.Items.Add(i);
            for (int i = DateTime.Today.Year; i >= 1950; i--) cbYear.Items.Add(i);
        }

        private void GenerateCaptcha()
        {
            Bitmap bmp = new Bitmap(picCaptcha.Width, picCaptcha.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.Clear(Color.White);

            Random rnd = new Random();
            captchaText = "";
            string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ0123456789";
            for (int i = 0; i < 5; i++)
            {
                captchaText += chars[rnd.Next(chars.Length)];
            }

            using (Font font = new Font("Arial", 20, FontStyle.Bold))
            {
                g.DrawString(captchaText, font, Brushes.Black, new PointF(10, 10));
            }

            // vẽ thêm vài đường loằng ngoằng
            for (int i = 0; i < 5; i++)
            {
                g.DrawLine(Pens.Gray, rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height),
                                      rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height));
            }
            g.Dispose();

            picCaptcha.Image?.Dispose();
            picCaptcha.Image = bmp;
        }

        // Nút reload captcha
        private void btnReload_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        // Kiểm tra captcha khi người dùng nhập
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập
            if (!ValidateInput())
                return;

            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();

                    // 1️⃣ Kiểm tra tên đăng nhập đã tồn tại chưa
                    string checkUsername = "SELECT COUNT(*) FROM [dbo].[TaiKhoan] WHERE TenDangNhap = @username";
                    using (SqlCommand CheckUser = new SqlCommand(checkUsername, connect))
                    {
                        CheckUser.Parameters.AddWithValue("@username", txtName.Text.Trim());
                        int count = (int)CheckUser.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Đăng ký thất bại, tài khoản đã tồn tại!");
                            return;
                        }
                    }

                    // 2️⃣ Chèn tài khoản mới (IDTaiKhoan là IDENTITY nên SQL Server tự sinh)
                    string insertData = @"
                INSERT INTO [dbo].[TaiKhoan]
                (TenDangNhap, Pass, HoTen, Email, SDT, VaiTro, NgayTao)
                VALUES
                (@username, @pass, @hoten, @email, @phone, @role, @created)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@username", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", txtPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@hoten", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@role", "User");
                        cmd.Parameters.AddWithValue("@created", DateTime.Now);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đăng ký thành công! Hãy chuyển sang tab Đăng nhập.");
                    }
                }

                txtCaptcha.Clear();
                GenerateCaptcha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký: " + ex.Message);
            }
        }


        private bool ValidateInput()
        {
            if (cbYear.SelectedItem == null || cbMonth.SelectedItem == null || cbDay.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Ngày / Tháng / Năm sinh!");
                return false;
            }

            int year = (int)cbYear.SelectedItem;
            int month = (int)cbMonth.SelectedItem;
            if ((int)cbDay.SelectedItem > DateTime.DaysInMonth(year, month))
            {
                MessageBox.Show("Ngày sinh không hợp lệ!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!");
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email!");
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!");
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCaptcha.Text))
            {
                MessageBox.Show("Vui lòng nhập captcha!");
                txtCaptcha.Focus();
                return false;
            }

            if (!string.Equals(txtCaptcha.Text.Trim(), captchaText, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Sai captcha, vui lòng thử lại!");
                GenerateCaptcha();
                txtCaptcha.Clear();
                txtCaptcha.Focus();
                return false;
            }

            if (!chkAgree4.Checked || !chkAgree2.Checked || !chkAgree3.Checked)
            {
                MessageBox.Show("Bạn phải đồng ý với các điều khoản trước khi đăng ký!");
                return false;
            }

            return true;
        }

        private void reg_showPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = reg_showPass.Checked ? '\0' : '*';
        }
    }
}
