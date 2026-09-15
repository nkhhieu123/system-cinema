using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema_system.khách_hàng
{
    public partial class thông_tin_chung : UserControl
    {
        private string tenDangNhap;

        // ✅ Constructor có tham số
        public thông_tin_chung(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
        }

        private void thông_tin_chung_Load(object sender, EventArgs e)
        {
            LoadThongTin();
        }

        private void LoadThongTin()
        {
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Tên đăng nhập chưa được truyền vào!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
                {
                    string query = "SELECT HoTen, Email, SDT, VaiTro, NgayTao FROM TaiKhoan WHERE TenDangNhap = @ten";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ten", tenDangNhap);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lblXinChao.Text = "Xin chào " + reader["HoTen"].ToString();
                        lblHoTen.Text = reader["HoTen"].ToString();
                        lblEmail.Text = reader["Email"].ToString();
                        lblSDT.Text = reader["SDT"].ToString();
                        lblVaiTro.Text = reader["VaiTro"].ToString();
                        lblNgayTao.Text = Convert.ToDateTime(reader["NgayTao"]).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài khoản!");
                    }

                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
    }
}
