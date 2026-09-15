using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinema_system.nhân_viên
{
    public partial class RoomMovieControl : UserControl
    {
        string connectionString = Db.ConnectionString;
        public RoomMovieControl()
        {
            InitializeComponent();
            dgvRoomMovies.AllowUserToAddRows = false;
            dgvRoomMovies.ReadOnly = true;
            dgvRoomMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoomMovies.MultiSelect = false;
            // Cột được sinh khi control đã gắn lên form, nên ẩn cột ID sau khi bind xong
            dgvRoomMovies.DataBindingComplete += (s, e) =>
            {
                if (dgvRoomMovies.Columns["ID"] != null)
                    dgvRoomMovies.Columns["ID"].Visible = false;
            };
            LoadRooms();
            LoadMovies();
            LoadRoomMovies();
        }

        private void LoadRooms()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RoomID, RoomName FROM Rooms", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbRoom.DisplayMember = "RoomName";
                cbRoom.ValueMember = "RoomID";
                cbRoom.DataSource = dt;
            }
        }

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

        private void LoadRoomMovies()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT rm.ID, r.RoomName, m.MovieName
                                 FROM RoomMovies rm
                                 JOIN Rooms r ON rm.RoomID = r.RoomID
                                 JOIN Movies m ON rm.MovieID = m.MovieID";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRoomMovies.DataSource = dt;
            }
        }

        private bool RoomMovieExists(SqlConnection conn, int roomId, int movieId)
        {
            SqlCommand checkCmd = new SqlCommand(
                "SELECT COUNT(*) FROM RoomMovies WHERE RoomID=@RoomID AND MovieID=@MovieID", conn);
            checkCmd.Parameters.AddWithValue("@RoomID", roomId);
            checkCmd.Parameters.AddWithValue("@MovieID", movieId);
            return (int)checkCmd.ExecuteScalar() > 0;
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (cbRoom.SelectedValue == null || cbMovie.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn phòng và phim!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roomId = Convert.ToInt32(cbRoom.SelectedValue);
            int movieId = Convert.ToInt32(cbMovie.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kiểm tra xem phòng này đã có phim gán chưa
                if (RoomMovieExists(conn, roomId, movieId))
                {
                    MessageBox.Show("Phòng này đã có phim này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SqlCommand insertCmd = new SqlCommand(
                    "INSERT INTO RoomMovies (RoomID, MovieID) VALUES (@RoomID, @MovieID)", conn);
                insertCmd.Parameters.AddWithValue("@RoomID", roomId);
                insertCmd.Parameters.AddWithValue("@MovieID", movieId);
                insertCmd.ExecuteNonQuery();
            }

            MessageBox.Show("Đã gán phim cho phòng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadRoomMovies();
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            if (dgvRoomMovies.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbRoom.SelectedValue == null || cbMovie.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn phòng và phim mới!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roomId = Convert.ToInt32(cbRoom.SelectedValue);
            int movieId = Convert.ToInt32(cbMovie.SelectedValue);

            // Sửa đúng dòng đang chọn theo ID (trước đây sửa theo RoomID nên đổi phim của cả phòng)
            int id = Convert.ToInt32(dgvRoomMovies.CurrentRow.Cells["ID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                if (RoomMovieExists(conn, roomId, movieId))
                {
                    MessageBox.Show("Phòng này đã có phim này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SqlCommand updateCmd = new SqlCommand(
                    "UPDATE RoomMovies SET RoomID=@RoomID, MovieID=@MovieID WHERE ID=@ID", conn);
                updateCmd.Parameters.AddWithValue("@RoomID", roomId);
                updateCmd.Parameters.AddWithValue("@MovieID", movieId);
                updateCmd.Parameters.AddWithValue("@ID", id);
                updateCmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cập nhật phim trong phòng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadRoomMovies();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvRoomMovies.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvRoomMovies.CurrentRow.Cells["ID"].Value);
            string selectedRoomName = dgvRoomMovies.CurrentRow.Cells["RoomName"].Value.ToString();
            string selectedMovieName = dgvRoomMovies.CurrentRow.Cells["MovieName"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa phim '{selectedMovieName}' khỏi phòng '{selectedRoomName}'?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM RoomMovies WHERE ID = @ID", conn);
                    deleteCmd.Parameters.AddWithValue("@ID", id);
                    deleteCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRoomMovies();
            }
        }
    }
}
