using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace cinema_system.nhân_viên
{
    public partial class addmovie : UserControl
    {
        string connectionString = Db.ConnectionString;
        string imgPath = "";

        public addmovie ()
        {
            InitializeComponent();
            LoadMovies();
        }

        private void LoadMovies()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Movies", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMovies.DataSource = dt;

                if (dgvMovies.Columns["Price"] != null)
                {
                    dgvMovies.Columns["Price"].DefaultCellStyle.FormatProvider =
                new System.Globalization.CultureInfo("vi-VN");
                    dgvMovies.Columns["Price"].DefaultCellStyle.Format = "c0";
                }
            }
        }

        // Kiểm tra dữ liệu nhập, trả về giá vé đã parse
        private bool TryGetInput(out decimal price)
        {
            price = 0;
            if (string.IsNullOrWhiteSpace(txtMovieName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên phim!");
                return false;
            }

            // định dạng Việt Nam: chấp nhận "75000" hoặc "75.000"
            if (!decimal.TryParse(txtPrice.Text.Trim(), NumberStyles.Number, new CultureInfo("vi-VN"), out price) || price < 0)
            {
                MessageBox.Show("Giá vé không hợp lệ!");
                return false;
            }
            return true;
        }

        private int? GetSelectedMovieId()
        {
            if (dgvMovies.CurrentRow == null)
                return null;
            object value = dgvMovies.CurrentRow.Cells["MovieID"].Value;
            return value == null || value == DBNull.Value ? (int?)null : Convert.ToInt32(value);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryGetInput(out decimal price))
                return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Movies (MovieName, Price, PosterPath) VALUES (@name, @price, @img)", con);
                cmd.Parameters.AddWithValue("@name", txtMovieName.Text.Trim());
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@img", imgPath);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Movie added successfully!");
                LoadMovies();
                ClearForm();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedMovieId();
            if (id == null)
            {
                MessageBox.Show("Vui lòng chọn phim cần sửa!");
                return;
            }
            if (!TryGetInput(out decimal price))
                return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Movies SET MovieName=@name, Price=@price, PosterPath=@img WHERE MovieID=@id", con);
                cmd.Parameters.AddWithValue("@id", id.Value);
                cmd.Parameters.AddWithValue("@name", txtMovieName.Text.Trim());
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@img", imgPath);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated successfully!");
                LoadMovies();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedMovieId();
            if (id == null)
            {
                MessageBox.Show("Vui lòng chọn phim cần xóa!");
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn xóa phim này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Movies WHERE MovieID=@id", con);
                    cmd.Parameters.AddWithValue("@id", id.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted successfully!");
                    LoadMovies();
                    ClearForm();
                }
            }
            catch (SqlException ex) when (ex.Number == 547) // vi phạm khóa ngoại
            {
                MessageBox.Show("Không thể xóa: phim này đang có suất chiếu hoặc đã được gán phòng.");
            }
        }

        // Chọn 1 dòng -> đổ dữ liệu lên form để sửa
        private void dgvMovies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvMovies.Rows[e.RowIndex];
            txtMovieName.Text = row.Cells["MovieName"].Value?.ToString();
            object priceValue = row.Cells["Price"].Value;
            txtPrice.Text = priceValue == null || priceValue == DBNull.Value
                ? ""
                : Convert.ToDecimal(priceValue).ToString("0", CultureInfo.InvariantCulture);
            imgPath = row.Cells["PosterPath"].Value?.ToString() ?? "";
            SetPoster(imgPath);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtMovieName.Clear();
            txtPrice.Clear();
            SetPoster(null);
            imgPath = "";
        }

        // Nạp ảnh bằng bản sao để không khóa file ảnh gốc
        private void SetPoster(string path)
        {
            Image old = picPoster.Image;
            picPoster.Image = null;
            old?.Dispose();

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                using (Image img = Image.FromFile(path))
                {
                    picPoster.Image = new Bitmap(img);
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgPath = ofd.FileName;
                SetPoster(imgPath);
            }
        }

        }
}
