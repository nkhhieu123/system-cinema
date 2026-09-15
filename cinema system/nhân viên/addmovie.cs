using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace cinema_system.nhân_viên
{
    public partial class addmovie : UserControl
    {
        string connectionString = Db.ConnectionString;
        private int selectedMovieId = -1;
        // Ảnh poster (JPEG) sẽ lưu vào cột Movies.Poster; null = phim chưa có poster
        private byte[] posterData;

        public addmovie ()
        {
            InitializeComponent();
            textBox1.ReadOnly = true; // ô ID phim chỉ để xem
            dgvMovies.DataBindingComplete += (s, e) =>
            {
                if (dgvMovies.Columns["MovieID"] == null)
                    return;
                dgvMovies.Columns["MovieID"].HeaderText = "ID phim";
                dgvMovies.Columns["MovieName"].HeaderText = "Tên phim";
                dgvMovies.Columns["Price"].HeaderText = "Giá vé";
                dgvMovies.Columns["Price"].DefaultCellStyle.FormatProvider = new CultureInfo("vi-VN");
                dgvMovies.Columns["Price"].DefaultCellStyle.Format = "c0";
                dgvMovies.Columns["CoPoster"].HeaderText = "Có poster";
            };
            LoadMovies();
        }

        private void LoadMovies()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                // Không lấy dữ liệu ảnh ra bảng cho nhẹ, chỉ báo phim đã có poster chưa
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT MovieID, MovieName, Price,
                             CAST(CASE WHEN Poster IS NOT NULL THEN 1 ELSE 0 END AS bit) AS CoPoster
                      FROM Movies ORDER BY MovieID", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMovies.DataSource = dt;
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
            if (!decimal.TryParse(txtPrice.Text.Trim(), NumberStyles.Number, new CultureInfo("vi-VN"), out price) || price <= 0)
            {
                MessageBox.Show("Giá vé không hợp lệ! (đây là giá ghế thường, ghế VIP / Sweetbox tính thêm phụ thu)");
                return false;
            }
            return true;
        }

        private SqlParameter PosterParameter()
        {
            SqlParameter p = new SqlParameter("@poster", SqlDbType.VarBinary, -1);
            p.Value = (object)posterData ?? DBNull.Value;
            return p;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryGetInput(out decimal price))
                return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Movies (MovieName, Price, Poster) VALUES (@name, @price, @poster)", con);
                cmd.Parameters.AddWithValue("@name", txtMovieName.Text.Trim());
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.Add(PosterParameter());
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm phim thành công!");
            LoadMovies();
            ClearForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedMovieId < 0)
            {
                MessageBox.Show("Vui lòng chọn phim cần sửa!");
                return;
            }
            if (!TryGetInput(out decimal price))
                return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                // Đã lưu ảnh vào DB thì bỏ đường dẫn file cũ
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Movies SET MovieName=@name, Price=@price, Poster=@poster, PosterPath=NULL WHERE MovieID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedMovieId);
                cmd.Parameters.AddWithValue("@name", txtMovieName.Text.Trim());
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.Add(PosterParameter());
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Sửa phim thành công!");
            LoadMovies();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedMovieId < 0)
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
                    cmd.Parameters.AddWithValue("@id", selectedMovieId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) when (ex.Number == 547) // vi phạm khóa ngoại
            {
                MessageBox.Show("Không thể xóa: phim này đang có suất chiếu hoặc đã được gán phòng.");
                return;
            }

            MessageBox.Show("Xóa phim thành công!");
            LoadMovies();
            ClearForm();
        }

        // Chọn 1 dòng -> đổ dữ liệu lên form để sửa
        private void dgvMovies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvMovies.Rows[e.RowIndex];
            selectedMovieId = Convert.ToInt32(row.Cells["MovieID"].Value);
            textBox1.Text = selectedMovieId.ToString();
            txtMovieName.Text = row.Cells["MovieName"].Value?.ToString();
            object priceValue = row.Cells["Price"].Value;
            txtPrice.Text = priceValue == null || priceValue == DBNull.Value
                ? ""
                : Convert.ToDecimal(priceValue).ToString("0", CultureInfo.InvariantCulture);

            LoadPoster(selectedMovieId);
        }

        // Lấy poster của phim đang chọn; phim cũ chỉ có đường dẫn file thì đọc file để lần Sửa sau lưu vào DB
        private void LoadPoster(int movieId)
        {
            posterData = null;
            object path = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Poster, PosterPath FROM Movies WHERE MovieID=@id", con);
                cmd.Parameters.AddWithValue("@id", movieId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        posterData = reader["Poster"] as byte[];
                        path = reader["PosterPath"];
                    }
                }
            }

            if (posterData == null && path is string file && System.IO.File.Exists(file))
            {
                try
                {
                    posterData = PosterImage.FromFile(file);
                }
                catch (OutOfMemoryException) // file không phải ảnh
                {
                    posterData = null;
                }
            }

            ShowPoster();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            selectedMovieId = -1;
            textBox1.Clear();
            txtMovieName.Clear();
            txtPrice.Clear();
            posterData = null;
            ShowPoster();
            dgvMovies.ClearSelection();
        }

        private void ShowPoster()
        {
            Image old = picPoster.Image;
            picPoster.Image = PosterImage.Load(posterData, null);
            old?.Dispose();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    posterData = PosterImage.FromFile(ofd.FileName);
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("File đã chọn không phải là ảnh hợp lệ.");
                    return;
                }
                ShowPoster();
            }
        }
    }
}
