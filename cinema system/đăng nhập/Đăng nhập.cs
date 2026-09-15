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
            Session.DangXuat(); // về màn hình đăng nhập = đăng xuất
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

                    // Đăng nhập bằng tên đăng nhập, email hoặc số điện thoại (ưu tiên trùng tên đăng nhập)
                    string query = @"SELECT IDTaiKhoan, TenDangNhap, Pass, VaiTro FROM TaiKhoan
                                     WHERE TenDangNhap = @username OR Email = @username OR SDT = @username
                                     ORDER BY CASE WHEN TenDangNhap = @username THEN 0 ELSE 1 END";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);

                    int id = 0;
                    string tenDangNhap = null, role = null;
                    bool needsRehash = false;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (PasswordHasher.Verify(password, reader["Pass"].ToString(), out needsRehash))
                            {
                                id = Convert.ToInt32(reader["IDTaiKhoan"]);
                                tenDangNhap = reader["TenDangNhap"].ToString();
                                role = reader["VaiTro"].ToString().Trim();
                                break;
                            }
                        }
                    }

                    if (tenDangNhap == null)
                    {
                        MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo");
                        GenerateCaptcha();
                        return;
                    }

                    // Mật khẩu cũ còn lưu dạng văn bản thường -> băm lại
                    if (needsRehash)
                    {
                        SqlCommand rehash = new SqlCommand("UPDATE TaiKhoan SET Pass = @pass WHERE IDTaiKhoan = @id", con);
                        rehash.Parameters.AddWithValue("@pass", PasswordHasher.Hash(password));
                        rehash.Parameters.AddWithValue("@id", id);
                        rehash.ExecuteNonQuery();
                    }

                    Session.DangNhap(id, tenDangNhap, role);
                    Form next;

                    // chuyển hướng theo vai trò
                    if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                        next = new admin_design();
                    else if (role.Equals("staff", StringComparison.OrdinalIgnoreCase))
                        next = new StaffDesign();
                    else if (role.Equals("user", StringComparison.OrdinalIgnoreCase))
                        next = new thông_tin_khách_hàng(tenDangNhap);
                    else
                    {
                        Session.DangXuat();
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

        private void linkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (QuenMatKhau f = new QuenMatKhau())
            {
                f.ShowDialog(this);
            }
        }
    }
}
