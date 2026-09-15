using System;
using System.Data;
using System.Data.SqlClient;

namespace cinema_system
{
    // Kiểm tra suất chiếu chồng giờ trong cùng một phòng.
    // Một suất chiếm phòng từ giờ bắt đầu đến hết thời lượng phim + thời gian dọn phòng.
    internal static class ShowtimeSchedule
    {
        public const int MaxDuration = 600; // phút

        // Thời lượng phim; phim cũ chưa nhập thì dùng thời lượng mặc định trong Cài đặt
        public static int GetMovieDuration(SqlConnection conn, int movieId, AppSettings settings)
        {
            SqlCommand cmd = new SqlCommand("SELECT Duration FROM Movies WHERE MovieID = @id", conn);
            cmd.Parameters.AddWithValue("@id", movieId);
            object value = cmd.ExecuteScalar();
            return value == null || value == DBNull.Value || Convert.ToInt32(value) <= 0
                ? settings.ThoiLuongMacDinh
                : Convert.ToInt32(value);
        }

        // Trả về mô tả suất chiếu bị chồng giờ, null nếu không trùng.
        // excludeShowtimeId: suất đang sửa (-1 khi thêm mới).
        public static string FindConflict(SqlConnection conn, AppSettings settings, int roomId,
                                          DateTime start, int durationMinutes, int excludeShowtimeId)
        {
            DateTime end = start.AddMinutes(durationMinutes + settings.ThoiGianDonPhong);

            // Phim dài tối đa MaxDuration phút nên chỉ cần xét các suất trong khoảng hôm trước -> hôm sau
            SqlCommand cmd = new SqlCommand(
                @"SELECT s.ShowDate, s.ShowTime, m.MovieName, m.Duration
                  FROM Showtimes s JOIN Movies m ON s.MovieID = m.MovieID
                  WHERE s.RoomID = @room AND s.ShowtimeID <> @id AND s.ShowDate BETWEEN @from AND @to", conn);
            cmd.Parameters.AddWithValue("@room", roomId);
            cmd.Parameters.AddWithValue("@id", excludeShowtimeId);
            cmd.Parameters.Add("@from", SqlDbType.Date).Value = start.Date.AddDays(-1);
            cmd.Parameters.Add("@to", SqlDbType.Date).Value = start.Date.AddDays(1);

            using (SqlDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    DateTime otherStart = r.GetDateTime(0).Date + r.GetTimeSpan(1);
                    int otherDuration = r.IsDBNull(3) || r.GetInt32(3) <= 0 ? settings.ThoiLuongMacDinh : r.GetInt32(3);
                    DateTime otherEnd = otherStart.AddMinutes(otherDuration + settings.ThoiGianDonPhong);

                    if (start < otherEnd && otherStart < end)
                    {
                        return $"\"{r.GetString(2)}\" chiếu {otherStart:HH:mm} - {otherStart.AddMinutes(otherDuration):HH:mm} ngày {otherStart:dd/MM/yyyy}";
                    }
                }
            }
            return null;
        }
    }
}
