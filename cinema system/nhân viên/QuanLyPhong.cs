using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema_system.nhân_viên
{
    // Thêm / đổi tên / xóa phòng chiếu
    public partial class QuanLyPhong : Form
    {
        // Sơ đồ ghế mặc định: 10 hàng x 12 ghế; hàng A-C thường, D-I VIP, J sweetbox
        public const int SeatRows = 10;
        public const int SeatColumns = 12;
        public const string SeatThuong = "Thuong";
        public const string SeatVIP = "VIP";
        public const string SeatSweetbox = "Sweetbox";

        private int selectedRoomId = -1;

        public QuanLyPhong()
        {
            InitializeComponent();
            dgvRooms.DataBindingComplete += (s, e) =>
            {
                if (dgvRooms.Columns["RoomID"] == null)
                    return;
                dgvRooms.Columns["RoomID"].HeaderText = "ID";
                dgvRooms.Columns["RoomName"].HeaderText = "Tên phòng";
                dgvRooms.Columns["SoGhe"].HeaderText = "Số ghế";
                dgvRooms.Columns["SoSuatChieu"].HeaderText = "Số suất chiếu";
                dgvRooms.ClearSelection();
            };
            LoadRooms();
        }

        // Phòng chưa có ghế trong DB thì tạo sơ đồ ghế mặc định
        public static void EnsureDefaultSeats(SqlConnection conn, int roomId)
        {
            SqlCommand count = new SqlCommand("SELECT COUNT(*) FROM Seats WHERE RoomID = @room", conn);
            count.Parameters.AddWithValue("@room", roomId);
            if ((int)count.ExecuteScalar() > 0)
                return;

            using (SqlTransaction tran = conn.BeginTransaction())
            {
                for (int i = 0; i < SeatRows; i++)
                {
                    string type = i < 3 ? SeatThuong : i < 9 ? SeatVIP : SeatSweetbox;
                    for (int j = 1; j <= SeatColumns; j++)
                    {
                        // Kiểm tra lại trong transaction phòng khi 2 máy cùng tạo ghế một lúc
                        SqlCommand insert = new SqlCommand(
                            @"IF NOT EXISTS (SELECT 1 FROM Seats WITH (UPDLOCK, HOLDLOCK) WHERE RoomID = @room AND SeatName = @name)
                                  INSERT INTO Seats (RoomID, SeatName, SeatType) VALUES (@room, @name, @type)", conn, tran);
                        insert.Parameters.AddWithValue("@room", roomId);
                        insert.Parameters.AddWithValue("@name", $"{(char)('A' + i)}{j}");
                        insert.Parameters.AddWithValue("@type", type);
                        insert.ExecuteNonQuery();
                    }
                }
                tran.Commit();
            }
        }

        private void LoadRooms()
        {
            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT r.RoomID, r.RoomName,
                             (SELECT COUNT(*) FROM Seats s WHERE s.RoomID = r.RoomID) AS SoGhe,
                             (SELECT COUNT(*) FROM Showtimes st WHERE st.RoomID = r.RoomID) AS SoSuatChieu
                      FROM Rooms r ORDER BY r.RoomID", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRooms.DataSource = dt;
            }
        }

        private bool RoomNameExists(SqlConnection conn, string name, int excludeId)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Rooms WHERE RoomName = @name AND RoomID <> @id", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@id", excludeId);
            return (int)cmd.ExecuteScalar() > 0;
        }

        private string GetInputName()
        {
            string name = txtTenPhong.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("Vui lòng nhập tên phòng.");
                txtTenPhong.Focus();
                return null;
            }
            return name;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string name = GetInputName();
            if (name == null)
                return;

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                if (RoomNameExists(conn, name, -1))
                {
                    MessageBox.Show("Tên phòng đã tồn tại!");
                    return;
                }

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Rooms (RoomName) VALUES (@name); SELECT CAST(SCOPE_IDENTITY() AS int);", conn);
                cmd.Parameters.AddWithValue("@name", name);
                int roomId = (int)cmd.ExecuteScalar();

                EnsureDefaultSeats(conn, roomId);
            }

            MessageBox.Show($"Đã thêm phòng (sơ đồ {SeatRows} hàng x {SeatColumns} ghế).");
            LoadRooms();
            ClearForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedRoomId < 0)
            {
                MessageBox.Show("Hãy chọn phòng cần đổi tên.");
                return;
            }
            string name = GetInputName();
            if (name == null)
                return;

            using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
            {
                conn.Open();
                if (RoomNameExists(conn, name, selectedRoomId))
                {
                    MessageBox.Show("Tên phòng đã tồn tại!");
                    return;
                }

                SqlCommand cmd = new SqlCommand("UPDATE Rooms SET RoomName = @name WHERE RoomID = @id", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@id", selectedRoomId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Đổi tên phòng thành công!");
            LoadRooms();
            ClearForm();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedRoomId < 0)
            {
                MessageBox.Show("Hãy chọn phòng cần xóa.");
                return;
            }
            if (MessageBox.Show("Xóa phòng \"" + txtTenPhong.Text.Trim() + "\"?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(Db.ConnectionString))
                {
                    conn.Open();

                    SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM Showtimes WHERE RoomID = @id", conn);
                    check.Parameters.AddWithValue("@id", selectedRoomId);
                    if ((int)check.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Không thể xóa: phòng này đang có suất chiếu. Hãy xóa hoặc chuyển các suất chiếu trước.");
                        return;
                    }

                    // Xóa ghế và các phim đã gán cho phòng rồi mới xóa phòng
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        foreach (string sql in new[]
                        {
                            "DELETE FROM RoomMovies WHERE RoomID = @id",
                            "DELETE FROM Seats WHERE RoomID = @id",
                            "DELETE FROM Rooms WHERE RoomID = @id"
                        })
                        {
                            SqlCommand cmd = new SqlCommand(sql, conn, tran);
                            cmd.Parameters.AddWithValue("@id", selectedRoomId);
                            cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 547) // vi phạm khóa ngoại
            {
                MessageBox.Show("Không thể xóa: ghế của phòng này đã có vé được đặt.");
                return;
            }

            MessageBox.Show("Xóa phòng thành công!");
            LoadRooms();
            ClearForm();
        }

        private void dgvRooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvRooms.Rows[e.RowIndex];
            selectedRoomId = Convert.ToInt32(row.Cells["RoomID"].Value);
            txtTenPhong.Text = row.Cells["RoomName"].Value?.ToString();
        }

        private void ClearForm()
        {
            selectedRoomId = -1;
            txtTenPhong.Clear();
            dgvRooms.ClearSelection();
        }
    }
}
