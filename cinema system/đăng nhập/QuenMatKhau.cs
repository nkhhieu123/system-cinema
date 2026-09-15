using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema_system.đăng_nhập
{
    // Tìm lại mật khẩu: nhập đúng tên đăng nhập + email + số điện thoại đã đăng ký thì được đặt mật khẩu mới
    public partial class QuenMatKhau : Form
    {
        public QuenMatKhau()
        {
            InitializeComponent();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string ten = txtTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string matKhau = txtMatKhauMoi.Text.Trim();

            if (ten == "" || email == "" || sdt == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập, email và số điện thoại.");
                return;
            }
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
                SqlCommand cmd = new SqlCommand(
                    "UPDATE TaiKhoan SET Pass = @pass WHERE TenDangNhap = @ten AND Email = @email AND SDT = @sdt", conn);
                cmd.Parameters.AddWithValue("@pass", PasswordHasher.Hash(matKhau));
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@sdt", sdt);

                if (cmd.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Thông tin không khớp với tài khoản nào.\n" +
                                    "Nếu tài khoản chưa có email / số điện thoại, hãy liên hệ admin để đặt lại mật khẩu.");
                    return;
                }
            }

            MessageBox.Show("Đặt lại mật khẩu thành công! Hãy đăng nhập bằng mật khẩu mới.");
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
