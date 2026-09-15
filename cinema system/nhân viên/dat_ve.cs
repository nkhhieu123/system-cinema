using cinema_system.đăng_nhập;
using rạp_chiếu_phim.khách_hàng;
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

                // Lấy danh sách phim
                SqlCommand cmd = new SqlCommand(
                    "SELECT MovieID, MovieName, PosterPath FROM Movies",
                    con
                );

                List<(int, string, string)> movies = new List<(int, string, string)>();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["MovieID"]);
                        string name = reader["MovieName"].ToString();
                        string poster = reader["PosterPath"].ToString();
                        movies.Add((id, name, poster));
                    }
                }

                // Với mỗi phim, lấy danh sách suất chiếu theo ngày
                foreach (var movie in movies)
                {
                    SqlCommand cmd2 = new SqlCommand(
                        "SELECT ShowTime FROM Showtimes WHERE MovieID = @id AND ShowDate = @date ORDER BY ShowTime", con);

                    cmd2.Parameters.AddWithValue("@id", movie.Item1);
                    cmd2.Parameters.Add("@date", SqlDbType.Date).Value = selectedDate.Date;

                    List<TimeSpan> times = new List<TimeSpan>();
                    using (SqlDataReader r2 = cmd2.ExecuteReader())
                    {
                        while (r2.Read())
                        {
                            times.Add(r2.GetTimeSpan(0));
                        }
                    }

                    // Phim không có suất chiếu trong ngày thì không hiển thị
                    if (times.Count == 0)
                        continue;

                    // Tạo item phim hiển thị lên UI
                    MovieItem item = new MovieItem();
                    item.SetData(movie.Item2, movie.Item3, times); // name, poster, times
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
