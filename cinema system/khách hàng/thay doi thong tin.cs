using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema_system.khách_hàng
{
    public partial class thay_doi_thong_tin : UserControl
    {
        private string tenDangNhap;
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
                    string query = "SELECT HoTen, Email, SDT, VaiTro, NgayTao FROM TaiKhoan WHERE TenDangNhap = @ten";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ten", tenDangNhap);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
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

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Nếu user muốn đổi mật khẩu => kiểm tra mật khẩu cũ
                    if (chkDoiMatKhau.Checked)
                    {
                        if (string.IsNullOrEmpty(txtMatKhauCu.Text))
                        {
                            MessageBox.Show("Vui lòng nhập mật khẩu cũ.");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtMatKhauMoi.Text))
                        {
                            MessageBox.Show("Vui lòng nhập mật khẩu mới.");
                            return;
                        }
                        if (txtMatKhauMoi.Text != txtNhapLai.Text)
                        {
                            MessageBox.Show("Mật khẩu mới và nhập lại không khớp.");
                            return;
                        }

                        // Lấy mật khẩu hiện tại từ DB
                        string getPassQuery = "SELECT Pass FROM TaiKhoan WHERE TenDangNhap = @ten";
                        using (SqlCommand cmdGet = new SqlCommand(getPassQuery, conn))
                        {
                            cmdGet.Parameters.AddWithValue("@ten", tenDangNhap);
                            object dbPassObj = cmdGet.ExecuteScalar();
                            string dbPass = dbPassObj == DBNull.Value || dbPassObj == null ? "" : dbPassObj.ToString();

                            // So sánh mật khẩu cũ (ghi chú: nếu DB lưu hash thì phải hash txtMatKhauCu tương ứng trước khi so sánh)
                            if (dbPass != txtMatKhauCu.Text)
                            {
                                MessageBox.Show("Mật khẩu cũ không đúng.");
                                return;
                            }
                        }

                        // Nếu pass cũ đúng => cập nhật cả thông tin + pass
                        string updateQueryWithPass = @"UPDATE TaiKhoan
                                                       SET HoTen = @hoten,
                                                           Email = @email,
                                                           SDT = @sdt,
                                                           Pass = @pass
                                                       WHERE TenDangNhap = @ten";
                        using (SqlCommand cmdUpd = new SqlCommand(updateQueryWithPass, conn))
                        {
                            cmdUpd.Parameters.AddWithValue("@hoten", txtTen.Text.Trim());
                            cmdUpd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                            cmdUpd.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                            cmdUpd.Parameters.AddWithValue("@pass", txtMatKhauMoi.Text); // nếu hash thì hash ở đây
                            cmdUpd.Parameters.AddWithValue("@ten", tenDangNhap);

                            int rows = cmdUpd.ExecuteNonQuery();
                            MessageBox.Show(rows > 0 ? "Cập nhật và đổi mật khẩu thành công!" : "Không có thay đổi.");
                        }
                    }
                    else
                    {
                        // Không đổi mật khẩu, chỉ cập nhật thông tin
                        string updateQuery = @"UPDATE TaiKhoan
                                               SET HoTen = @hoten,
                                                   Email = @email,
                                                   SDT = @sdt
                                               WHERE TenDangNhap = @ten";
                        using (SqlCommand cmdUpd = new SqlCommand(updateQuery, conn))
                        {
                            cmdUpd.Parameters.AddWithValue("@hoten", txtTen.Text.Trim());
                            cmdUpd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                            cmdUpd.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                            cmdUpd.Parameters.AddWithValue("@ten", tenDangNhap);

                            int rows = cmdUpd.ExecuteNonQuery();
                            MessageBox.Show(rows > 0 ? "Cập nhật thông tin thành công!" : "Không có thay đổi.");
                        }
                    }
                } // using conn
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin: " + ex.Message);
            }
        }

    }
}
