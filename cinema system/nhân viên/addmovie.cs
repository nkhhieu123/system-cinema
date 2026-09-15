using System;
using System.Collections.Generic;
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
                dgvMovies.Columns["Duration"].HeaderText = "Thời lượng (phút)";
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
                    @"SELECT MovieID, MovieName, Price, Duration,
                             CAST(CASE WHEN Poster IS NOT NULL THEN 1 ELSE 0 END AS bit) AS CoPoster
                      FROM Movies ORDER BY MovieID", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMovies.DataSource = dt;
            }
        }

        // Kiểm tra dữ liệu nhập, trả về giá vé và thời lượng
        private bool TryGetInput(out decimal price, out int duration)
        {
            price = 0;
            duration = (int)numDuration.Value;
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

            if (duration <= 0)
            {
                MessageBox.Show("Vui lòng nhập thời lượng phim (phút) để kiểm tra suất chiếu không bị chồng giờ.");
                numDuration.Focus();
                return false;
            }
            return true;
        }

        // Đổi thời lượng có thể làm các suất sắp chiếu của phim bị chồng giờ với suất khác trong cùng phòng
        private string FindScheduleConflict(SqlConnection con, int movieId, int duration)
        {
            AppSettings settings = AppSettings.Load(con);
            List<(int Id, int RoomId, DateTime Start)> showtimes = new List<(int, int, DateTime)>();

            SqlCommand cmd = new SqlCommand(
                @"SELECT ShowtimeID, RoomID, ShowDate, ShowTime FROM Showtimes
                  WHERE MovieID = @id AND RoomID IS NOT NULL AND ShowDate >= @today", con);
            cmd.Parameters.AddWithValue("@id", movieId);
            cmd.Parameters.Add("@today", SqlDbType.Date).Value = DateTime.Today;
            using (SqlDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                    showtimes.Add((r.GetInt32(0), r.GetInt32(1), r.GetDateTime(2).Date + r.GetTimeSpan(3)));
            }

            foreach (var st in showtimes)
            {
                if (st.Start <= DateTime.Now)
                    continue;
                string conflict = ShowtimeSchedule.FindConflict(con, settings, st.RoomId, st.Start, duration, st.Id);
                if (conflict != null)
                    return $"Suất {st.Start:HH:mm dd/MM/yyyy} của phim này sẽ bị chồng giờ với {conflict}.";
            }
            return null;
        }

        private SqlParameter PosterParameter()
        {
            SqlParameter p = new SqlParameter("@poster", SqlDbType.VarBinary, -1);
            p.Value = (object)posterData ?? DBNull.Value;
            return p;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryGetInput(out decimal price, out int duration))
                return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Movies (MovieName, Price, Duration, Poster) VALUES (@name, @price, @duration, @poster)", con);
                cmd.Parameters.AddWithValue("@name", txtMovieName.Text.Trim());
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@duration", duration);
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
            if (!TryGetInput(out decimal price, out int duration))
                return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string conflict = FindScheduleConflict(con, selectedMovieId, duration);
                if (conflict != null)
                {
                    MessageBox.Show(conflict + "\nHãy dời suất chiếu đó trước khi đổi thời lượng.", "Không thể sửa",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đã lưu ảnh vào DB thì bỏ đường dẫn file cũ
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Movies SET MovieName=@name, Price=@price, Duration=@duration, Poster=@poster, PosterPath=NULL WHERE MovieID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedMovieId);
                cmd.Parameters.AddWithValue("@name", txtMovieName.Text.Trim());
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@duration", duration);
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
            object durationValue = row.Cells["Duration"].Value;
            numDuration.Value = durationValue == null || durationValue == DBNull.Value
                ? 0
                : Math.Min(numDuration.Maximum, Convert.ToDecimal(durationValue));

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
            numDuration.Value = 0;
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
