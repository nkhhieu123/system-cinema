using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema_system.nhân_viên
{
    public partial class ThemMovie : UserControl
    {
        string connectionString = Db.ConnectionString;
        private int selectedShowtimeID = -1;
        public ThemMovie()
        {
            InitializeComponent();
            dgvShowtimes.DataBindingComplete += (s, e) =>
            {
                if (dgvShowtimes.Columns["ShowtimeID"] == null)
                    return;
                dgvShowtimes.Columns["MovieID"].Visible = false;
                dgvShowtimes.Columns["RoomID"].Visible = false;
                dgvShowtimes.Columns["ShowtimeID"].HeaderText = "ID";
                dgvShowtimes.Columns["MovieName"].HeaderText = "Phim";
                dgvShowtimes.Columns["RoomName"].HeaderText = "Phòng";
                dgvShowtimes.Columns["ShowDate"].HeaderText = "Ngày chiếu";
                dgvShowtimes.Columns["ShowDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvShowtimes.Columns["ShowTime"].HeaderText = "Giờ chiếu";
                dgvShowtimes.Columns["ShowTime"].DefaultCellStyle.Format = "hh\\:mm";
                dgvShowtimes.Columns["KetThuc"].HeaderText = "Kết thúc";
                dgvShowtimes.Columns["KetThuc"].DefaultCellStyle.Format = "hh\\:mm";
                dgvShowtimes.Columns["SoVe"].HeaderText = "Vé đã bán";
                dgvShowtimes.ClearSelection();
            };
        }

        private void FormAddShowtime_Load(object sender, EventArgs e)
        {
            LoadMovies();
            LoadRooms();
            LoadShowtimes();
        }

        // 🔹 Chỉ load phim có sẵn trong database
        private void LoadMovies()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT MovieID, MovieName FROM Movies", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbMovie.DisplayMember = "MovieName";
                cbMovie.ValueMember = "MovieID";
                cbMovie.DataSource = dt;
            }
        }

        private void LoadRooms()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RoomID, RoomName FROM Rooms ORDER BY RoomID", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbRoom.DisplayMember = "RoomName";
                cbRoom.ValueMember = "RoomID";
                cbRoom.DataSource = dt;
            }
        }

        // 🔹 Hiển thị danh sách suất chiếu
        private void LoadShowtimes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT s.ShowtimeID, s.MovieID, s.RoomID, m.MovieName, r.RoomName, s.ShowDate, s.ShowTime,
                             CONVERT(time(0), DATEADD(MINUTE, ISNULL(NULLIF(m.Duration, 0), @thoiLuong), CAST(s.ShowTime AS datetime))) AS KetThuc,
                             (SELECT COUNT(*) FROM BookedSeats b WHERE b.ShowtimeID = s.ShowtimeID AND b.IsBooked = 1) AS SoVe
                      FROM Showtimes s
                      JOIN Movies m ON s.MovieID = m.MovieID
                      LEFT JOIN Rooms r ON s.RoomID = r.RoomID
                      ORDER BY s.ShowDate DESC, s.ShowTime ASC", conn);
                cmd.Parameters.AddWithValue("@thoiLuong", AppSettings.Load(conn).ThoiLuongMacDinh);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvShowtimes.DataSource = dt;
            }
        }

        // Giờ chiếu chỉ lấy giờ:phút (DateTimePicker có cả giây/mili giây làm kiểm tra trùng bị sai)
        private TimeSpan GetShowTime()
        {
            TimeSpan t = dtpTime.Value.TimeOfDay;
            return new TimeSpan(t.Hours, t.Minutes, 0);
        }

        // Kiểm tra đã chọn phim, phòng và thời gian chưa qua
        private bool ValidateInput()
        {
            if (cbMovie.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn phim từ danh sách có sẵn.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbRoom.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn phòng chiếu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dtpDate.Value.Date + GetShowTime() <= DateTime.Now)
            {
                MessageBox.Show("Không thể đặt suất chiếu vào thời điểm đã qua.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // 🔹 Thêm suất chiếu mới cho phim đã có
        private void btnAddShowtime_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            int movieId = Convert.ToInt32(cbMovie.SelectedValue);
            int roomId = Convert.ToInt32(cbRoom.SelectedValue);
            DateTime showDate = dtpDate.Value.Date;
            TimeSpan showTime = GetShowTime();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // ✅ Kiểm tra chồng giờ với các suất khác trong phòng (thời lượng phim + thời gian dọn phòng)
                AppSettings settings = AppSettings.Load(conn);
                int duration = ShowtimeSchedule.GetMovieDuration(conn, movieId, settings);
                string conflict = ShowtimeSchedule.FindConflict(conn, settings, roomId, showDate + showTime, duration, -1);
                if (conflict != null)
                {
                    MessageBox.Show($"Phòng này bị trùng giờ với suất {conflict}.\n" +
                                    $"(Mỗi suất cần thêm {settings.ThoiGianDonPhong} phút dọn phòng sau khi chiếu xong.)",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Thêm suất chiếu mới
                SqlCommand insertCmd = new SqlCommand(
                    "INSERT INTO Showtimes (MovieID, RoomID, ShowDate, ShowTime) VALUES (@MovieID, @RoomID, @ShowDate, @ShowTime)", conn);
                insertCmd.Parameters.AddWithValue("@MovieID", movieId);
                insertCmd.Parameters.AddWithValue("@RoomID", roomId);
                insertCmd.Parameters.Add("@ShowDate", SqlDbType.Date).Value = showDate;
                insertCmd.Parameters.Add("@ShowTime", SqlDbType.Time).Value = showTime;
                insertCmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm suất chiếu thành công!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadShowtimes();
        }

        private void dgvShowtimes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvShowtimes.Rows[e.RowIndex];
                selectedShowtimeID = Convert.ToInt32(row.Cells["ShowtimeID"].Value);

                cbMovie.SelectedValue = Convert.ToInt32(row.Cells["MovieID"].Value);
                object roomId = row.Cells["RoomID"].Value;
                if (roomId == null || roomId == DBNull.Value)
                    cbRoom.SelectedIndex = -1; // suất chiếu cũ chưa gán phòng
                else
                    cbRoom.SelectedValue = Convert.ToInt32(roomId);
                dtpDate.Value = Convert.ToDateTime(row.Cells["ShowDate"].Value);
                dtpTime.Value = DateTime.Today.Add((TimeSpan)row.Cells["ShowTime"].Value);
            }
        }

        private int CountBookedSeats(SqlConnection conn, int showtimeId)
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM BookedSeats WHERE ShowtimeID=@ID AND IsBooked = 1", conn);
            cmd.Parameters.AddWithValue("@ID", showtimeId);
            return (int)cmd.ExecuteScalar();
        }

        private void btnDelShowtime_Click(object sender, EventArgs e)
        {
            if (selectedShowtimeID == -1)
            {
                MessageBox.Show("Hãy chọn một suất chiếu để xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa suất chiếu này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Showtimes WHERE ShowtimeID=@ID", conn);
                        cmd.Parameters.AddWithValue("@ID", selectedShowtimeID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex) when (ex.Number == 547) // vi phạm khóa ngoại
                {
                    MessageBox.Show("Không thể xóa: suất chiếu này đã có vé (kể cả vé đã hoàn), cần giữ lại để thống kê.");
                    return;
                }

                MessageBox.Show("Xóa thành công!");
                LoadShowtimes();
                selectedShowtimeID = -1;
            }
        }

        private void btnChangeShowtime_Click(object sender, EventArgs e)
        {
            if (selectedShowtimeID == -1)
            {
                MessageBox.Show("Hãy chọn một suất chiếu để sửa.");
                return;
            }
            if (!ValidateInput())
                return;

            int movieId = Convert.ToInt32(cbMovie.SelectedValue);
            int roomId = Convert.ToInt32(cbRoom.SelectedValue);
            DateTime showDate = dtpDate.Value.Date;
            TimeSpan showTime = GetShowTime();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Khách đã mua vé theo phim / phòng / giờ này nên không cho đổi
                int soVe = CountBookedSeats(conn, selectedShowtimeID);
                if (soVe > 0)
                {
                    MessageBox.Show($"Suất chiếu đã bán {soVe} vé nên không thể sửa. Hãy hoàn vé trước nếu cần đổi.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Kiểm tra chồng giờ với các suất khác trong phòng (thời lượng phim + thời gian dọn phòng)
                AppSettings settings = AppSettings.Load(conn);
                int duration = ShowtimeSchedule.GetMovieDuration(conn, movieId, settings);
                string conflict = ShowtimeSchedule.FindConflict(conn, settings, roomId, showDate + showTime, duration, selectedShowtimeID);
                if (conflict != null)
                {
                    MessageBox.Show($"Phòng này bị trùng giờ với suất {conflict}.\n" +
                                    $"(Mỗi suất cần thêm {settings.ThoiGianDonPhong} phút dọn phòng sau khi chiếu xong.)",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Showtimes SET MovieID=@MovieID, RoomID=@RoomID, ShowDate=@ShowDate, ShowTime=@ShowTime WHERE ShowtimeID=@ID", conn);
                cmd.Parameters.AddWithValue("@MovieID", movieId);
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                cmd.Parameters.Add("@ShowDate", SqlDbType.Date).Value = showDate;
                cmd.Parameters.Add("@ShowTime", SqlDbType.Time).Value = showTime;
                cmd.Parameters.AddWithValue("@ID", selectedShowtimeID);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Sửa thành công!");
            LoadShowtimes();
        }
    }
}
