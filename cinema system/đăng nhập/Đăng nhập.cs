using cinema_system.khách_hàng;
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
using cinema_system.Khách_hàng;
using cinema_system.admin;
using cinema_system.nhân_viên;

namespace cinema_system.đăng_nhập
{
    public partial class Đăng_nhập : Form
    {
        string conn = Db.ConnectionString;
        private string captchaText;
        public Đăng_nhập()
        {
            InitializeComponent();
            GenerateCaptcha();

            UC_Đăng_ký ucDangKy = new UC_Đăng_ký();
            ucDangKy.Dock = DockStyle.Fill;

            tabRegister.Controls.Add(ucDangKy);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập!", "Thông báo");
                return;
            }

            // kiểm tra captcha (không phân biệt hoa thường)
            if (!string.Equals(txtCaptcha.Text.Trim(), captchaText, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Captcha sai, vui lòng thử lại!", "Thông báo");
                GenerateCaptcha();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();

                    // Kiểm tra tài khoản trong bảng
                    string query = "SELECT VaiTro FROM TaiKhoan WHERE TenDangNhap = @username AND Pass = @password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    object result = cmd.ExecuteScalar(); // chỉ lấy 1 giá trị đầu tiên (VaiTro)

                    if (result == null || result == DBNull.Value)
                    {
                        MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo");
                        GenerateCaptcha();
                        return;
                    }

                    string role = result.ToString().Trim();
                    Form next;

                    // chuyển hướng theo vai trò
                    if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                        next = new admin_design();
                    else if (role.Equals("staff", StringComparison.OrdinalIgnoreCase))
                        next = new StaffDesign();
                    else if (role.Equals("user", StringComparison.OrdinalIgnoreCase))
                        next = new thông_tin_khách_hàng(username);
                    else
                    {
                        MessageBox.Show("Vai trò tài khoản không hợp lệ: " + role, "Thông báo");
                        return;
                    }

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                    Program.SwitchForm(this, next);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không kết nối được cơ sở dữ liệu: " + ex.Message, "Lỗi");
            }
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
            txtCaptcha.Clear();
        }
        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage tabPage = tabControl.TabPages[e.Index];
            Rectangle rect = e.Bounds;

            // nếu tab đang được chọn -> nền đỏ, chữ trắng
            if (e.Index == tabControl.SelectedIndex)
            {
                e.Graphics.FillRectangle(Brushes.Red, rect);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, this.Font,
                                      rect, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else
            {
                // tab chưa chọn -> nền trắng, chữ đỏ
                e.Graphics.FillRectangle(Brushes.White, rect);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, this.Font,
                                      rect, Color.Red, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            Program.SwitchForm(this, new UserDesign());
        }
    }
}
