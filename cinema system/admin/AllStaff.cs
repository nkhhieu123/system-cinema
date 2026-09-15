using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace cinema_system.admin
{
    // Quản lý tài khoản nhân viên (VaiTro = 'staff')
    // textBox1: Họ tên, textBox2: SĐT, textBox3: Email, textBox4: Tên đăng nhập, textBox5: Mật khẩu
    public partial class AllStaff : UserControl
    {
        string connectionString = Db.ConnectionString;
        private int selectedId = -1;

        public AllStaff()
        {
            InitializeComponent();
            new ToolTip().SetToolTip(textBox5, "Khi sửa: để trống nếu không muốn đổi mật khẩu");

            // Cột được sinh khi control đã gắn lên form, nên chỉnh cột sau khi bind xong
            dataGridView1.DataBindingComplete += (s, e) =>
            {
                if (dataGridView1.Columns["IDTaiKhoan"] == null)
                    return;
                dataGridView1.Columns["IDTaiKhoan"].Visible = false;
                dataGridView1.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
                dataGridView1.Columns["HoTen"].HeaderText = "Họ và tên";
                dataGridView1.Columns["SDT"].HeaderText = "Sđt";
                dataGridView1.Columns["NgayTao"].HeaderText = "Ngày tạo";
                dataGridView1.ClearSelection();
            };
            LoadStaff();
        }

        private void LoadStaff()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT IDTaiKhoan, TenDangNhap, HoTen, Email, SDT, NgayTao " +
                    "FROM TaiKhoan WHERE VaiTro = N'staff' ORDER BY IDTaiKhoan", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private bool ValidateInput(bool requirePassword)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!");
                textBox1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!");
                textBox4.Focus();
                return false;
            }

            if (requirePassword && string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!");
                textBox5.Focus();
                return false;
            }

            string sdt = textBox2.Text.Trim();
            if (sdt != "" && !Regex.IsMatch(sdt, @"^0\d{9,10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!");
                textBox2.Focus();
                return false;
            }

            string email = textBox3.Text.Trim();
            if (email != "" && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ!");
                textBox3.Focus();
                return false;
            }

            return true;
        }

        // Tên đăng nhập là duy nhất trên toàn bộ tài khoản (kể cả admin / khách hàng)
        private bool UsernameExists(SqlConnection conn, string username, int excludeId)
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @ten AND IDTaiKhoan <> @id", conn);
            cmd.Parameters.AddWithValue("@ten", username);
            cmd.Parameters.AddWithValue("@id", excludeId);
            return (int)cmd.ExecuteScalar() > 0;
        }

        // Thông tin chung cho thêm / sửa; Email và Sđt bỏ trống thì lưu NULL
        private void AddInfoParameters(SqlCommand cmd)
        {
            string email = textBox3.Text.Trim();
            string sdt = textBox2.Text.Trim();
            cmd.Parameters.AddWithValue("@ten", textBox4.Text.Trim());
            cmd.Parameters.AddWithValue("@hoten", textBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@email", email == "" ? (object)DBNull.Value : email);
            cmd.Parameters.AddWithValue("@sdt", sdt == "" ? (object)DBNull.Value : sdt);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(true))
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                if (UsernameExists(conn, textBox4.Text.Trim(), -1))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!");
                    textBox4.Focus();
                    return;
                }

                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO TaiKhoan (TenDangNhap, Pass, HoTen, Email, SDT, VaiTro, NgayTao)
                      VALUES (@ten, @pass, @hoten, @email, @sdt, N'staff', GETDATE())", conn);
                AddInfoParameters(cmd);
                cmd.Parameters.AddWithValue("@pass", textBox5.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm nhân viên thành công!");
            LoadStaff();
            ClearForm();
        }

        private void Fix_Click(object sender, EventArgs e)
        {
            if (selectedId < 0)
            {
                MessageBox.Show("Hãy chọn một nhân viên để sửa.");
                return;
            }
            if (!ValidateInput(false))
                return;

            bool doiMatKhau = !string.IsNullOrWhiteSpace(textBox5.Text);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                if (UsernameExists(conn, textBox4.Text.Trim(), selectedId))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!");
                    textBox4.Focus();
                    return;
                }

                string query = "UPDATE TaiKhoan SET TenDangNhap=@ten, HoTen=@hoten, Email=@email, SDT=@sdt" +
                               (doiMatKhau ? ", Pass=@pass" : "") +
                               " WHERE IDTaiKhoan=@id AND VaiTro = N'staff'";
                SqlCommand cmd = new SqlCommand(query, conn);
                AddInfoParameters(cmd);
                if (doiMatKhau)
                    cmd.Parameters.AddWithValue("@pass", textBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@id", selectedId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(doiMatKhau ? "Cập nhật thành công (đã đổi mật khẩu)!" : "Cập nhật thành công!");
            LoadStaff();
            ClearForm();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (selectedId < 0)
            {
                MessageBox.Show("Hãy chọn một nhân viên để xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa tài khoản \"" + textBox4.Text.Trim() + "\"?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM TaiKhoan WHERE IDTaiKhoan=@id AND VaiTro = N'staff'", conn);
                cmd.Parameters.AddWithValue("@id", selectedId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Xóa thành công!");
            LoadStaff();
            ClearForm();
        }

        // Chọn 1 dòng -> đổ dữ liệu lên form để sửa (mật khẩu không hiển thị)
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["IDTaiKhoan"].Value);
            textBox1.Text = row.Cells["HoTen"].Value?.ToString();
            textBox2.Text = row.Cells["SDT"].Value?.ToString();
            textBox3.Text = row.Cells["Email"].Value?.ToString();
            textBox4.Text = row.Cells["TenDangNhap"].Value?.ToString();
            textBox5.Clear();
        }

        private void ClearForm()
        {
            selectedId = -1;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            dataGridView1.ClearSelection();
        }
    }
}
