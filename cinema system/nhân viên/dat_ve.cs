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
    public partial class dat_ve : UserControl
    {
        string conn = Db.ConnectionString;
        private DateTime startDate = DateTime.Today;
        private Button selectedButton = null;
        public dat_ve()
        {
            InitializeComponent();
            GenerateDateButtons(startDate);
            // chọn sẵn ngày hôm nay
            Btn_Click(flowLayoutPanelDates.Controls[0], EventArgs.Empty);
        }

        private void LoadMovies(DateTime selectedDate)
        {
            flowMovies.Controls.Clear();

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                // Chỉ lấy phim có suất chiếu trong ngày đã chọn
                SqlCommand cmd = new SqlCommand(
                    @"SELECT m.MovieID, m.MovieName, m.Poster, m.PosterPath, m.Duration
                      FROM Movies m
                      WHERE EXISTS (SELECT 1 FROM Showtimes s WHERE s.MovieID = m.MovieID AND s.ShowDate = @date)
                      ORDER BY m.MovieName", con);
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = selectedDate.Date;

                int thoiLuongMacDinh = AppSettings.Load(con).ThoiLuongMacDinh;
                List<(int Id, string Name, int? Duration, Image Poster)> movies = new List<(int, string, int?, Image)>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add((Convert.ToInt32(reader["MovieID"]),
                                    reader["MovieName"].ToString(),
                                    reader["Duration"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Duration"]),
                                    PosterImage.Load(reader["Poster"], reader["PosterPath"])));
                    }
                }

                // Với mỗi phim, lấy danh sách suất chiếu theo ngày (kèm phòng chiếu)
                foreach (var movie in movies)
                {
                    SqlCommand cmd2 = new SqlCommand(
                        @"SELECT s.ShowtimeID, s.ShowTime, r.RoomName
                          FROM Showtimes s LEFT JOIN Rooms r ON s.RoomID = r.RoomID
                          WHERE s.MovieID = @id AND s.ShowDate = @date
                          ORDER BY s.ShowTime", con);
                    cmd2.Parameters.AddWithValue("@id", movie.Id);
                    cmd2.Parameters.Add("@date", SqlDbType.Date).Value = selectedDate.Date;

                    List<MovieItem.ShowtimeInfo> showtimes = new List<MovieItem.ShowtimeInfo>();
                    using (SqlDataReader r2 = cmd2.ExecuteReader())
                    {
                        while (r2.Read())
                        {
                            showtimes.Add(new MovieItem.ShowtimeInfo
                            {
                                ShowtimeID = r2.GetInt32(0),
                                Start = selectedDate.Date + r2.GetTimeSpan(1),
                                RoomName = r2.IsDBNull(2) ? null : r2.GetString(2),
                                Duration = movie.Duration > 0 ? movie.Duration.Value : thoiLuongMacDinh
                            });
                        }
                    }

                    // Tạo item phim hiển thị lên UI
                    MovieItem item = new MovieItem();
                    item.SetData(movie.Name, movie.Duration, movie.Poster, showtimes);
                    flowMovies.Controls.Add(item);
                }

                if (flowMovies.Controls.Count == 0)
                {
                    flowMovies.Controls.Add(new Label
                    {
                        Text = "Không có suất chiếu nào trong ngày " + selectedDate.ToString("dd/MM/yyyy"),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11, FontStyle.Italic),
                        Margin = new Padding(10)
                    });
                }
            }
        }


        private void GenerateDateButtons(DateTime start)
        {
            flowLayoutPanelDates.Controls.Clear();
            int numDays = 10; // số ngày hiển thị

            for (int i = 0; i < numDays; i++)
            {
                DateTime d = start.AddDays(i);
                Button btn = new Button();
                btn.Width = 80;
                btn.Height = 80;
                btn.Margin = new Padding(5);
                btn.BackColor = Color.WhiteSmoke;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.Tag = d;
                btn.Click += Btn_Click;
                btn.Text = $"{d:dd}\n{d:ddd}\n{d:MM}";
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                flowLayoutPanelDates.Controls.Add(btn);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (selectedButton != null)
                selectedButton.BackColor = Color.WhiteSmoke;

            selectedButton = sender as Button;
            selectedButton.BackColor = Color.LightBlue;

            DateTime selectedDate = (DateTime)selectedButton.Tag;
            LoadMovies(selectedDate);
        }


    }
}
