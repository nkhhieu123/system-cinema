using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema_system.khách_hàng
{
    public partial class thay_doi_thong_tin : UserControl
    {
        private string tenDangNhap;
        private int idTaiKhoan = -1;
        private readonly string connStr = Db.ConnectionString;

        public thay_doi_thong_tin(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
            // gọi load sau khi control khởi tạo
            this.Load += thay_doi_thong_tin_Load;
        }

        private void thay_doi_thong_tin_Load(object sender, EventArgs e)
        {
            LoadThongTin();
        }

        private void LoadThongTin()
        {
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Tên đăng nhập chưa được truyền vào.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT IDTaiKhoan, HoTen, Email, SDT, VaiTro, NgayTao FROM TaiKhoan WHERE TenDangNhap = @ten";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ten", tenDangNhap);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idTaiKhoan = Convert.ToInt32(reader["IDTaiKhoan"]);
                                txtTen.Text = reader["HoTen"]?.ToString();
                                txtEmail.Text = reader["Email"]?.ToString();
                                txtSDT.Text = reader["SDT"]?.ToString();
                                if (reader["NgayTao"] != DBNull.Value)
                                    dtpNgayTao.Value = Convert.ToDateTime(reader["NgayTao"]);
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy tài khoản.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin: " + ex.Message);
            }
        }

        private void chkDoiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = chkDoiMatKhau.Checked;
            txtMatKhauCu.Enabled = enabled;
            txtMatKhauMoi.Enabled = enabled;
            txtNhapLai.Enabled = enabled;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // validation cơ bản
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email không được để trống.");
                return;
            }

            bool doiMatKhau = chkDoiMatKhau.Checked;
            if (doiMatKhau)
            {
                if (string.IsNullOrWhiteSpace(txtMatKhauCu.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cũ.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtMatKhauMoi.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới.");
                    return;
                }
                if (txtMatKhauMoi.Text.Trim() != txtNhapLai.Text.Trim())
                {
                    MessageBox.Show("Mật khẩu mới và nhập lại không khớp.");
                    return;
                }
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Email / SĐT dùng để đăng nhập nên không được trùng tài khoản khác
                    string trung = Db.FindAccountDuplicate(conn, null, txtEmail.Text.Trim(), txtSDT.Text.Trim(), idTaiKhoan);
                    if (trung != null)
                    {
                        MessageBox.Show(trung);
                        return;
                    }

                    // Nếu muốn đổi mật khẩu => kiểm tra mật khẩu cũ (mật khẩu trong DB đã được băm)
                    if (doiMatKhau)
                    {
                        SqlCommand cmdGet = new SqlCommand("SELECT Pass FROM TaiKhoan WHERE TenDangNhap = @ten", conn);
                        cmdGet.Parameters.AddWithValue("@ten", tenDangNhap);
                        string dbPass = cmdGet.ExecuteScalar() as string;

                        if (!PasswordHasher.Verify(txtMatKhauCu.Text.Trim(), dbPass, out _))
                        {
                            MessageBox.Show("Mật khẩu cũ không đúng.");
                            return;
                        }
                    }

                    string updateQuery = @"UPDATE TaiKhoan
                                           SET HoTen = @hoten,
                                               Email = @email,
                                               SDT = @sdt" + (doiMatKhau ? ", Pass = @pass" : "") + @"
                                           WHERE TenDangNhap = @ten";
                    using (SqlCommand cmdUpd = new SqlCommand(updateQuery, conn))
                    {
                        string sdt = txtSDT.Text.Trim();
                        cmdUpd.Parameters.AddWithValue("@hoten", txtTen.Text.Trim());
                        cmdUpd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmdUpd.Parameters.AddWithValue("@sdt", sdt == "" ? (object)DBNull.Value : sdt);
                        cmdUpd.Parameters.AddWithValue("@ten", tenDangNhap);
                        if (doiMatKhau)
                            cmdUpd.Parameters.AddWithValue("@pass", PasswordHasher.Hash(txtMatKhauMoi.Text.Trim()));

                        int rows = cmdUpd.ExecuteNonQuery();
                        if (rows == 0)
                            MessageBox.Show("Không có thay đổi.");
                        else
                            MessageBox.Show(doiMatKhau ? "Cập nhật và đổi mật khẩu thành công!" : "Cập nhật thông tin thành công!");
                    }
                }

                if (doiMatKhau)
                {
                    chkDoiMatKhau.Checked = false;
                    txtMatKhauCu.Clear();
                    txtMatKhauMoi.Clear();
                    txtNhapLai.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin: " + ex.Message);
            }
        }

    }
}
