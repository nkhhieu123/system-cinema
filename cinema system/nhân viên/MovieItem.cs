using rạp_chiếu_phim.khách_hàng;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace cinema_system.nhân_viên
{
    public partial class MovieItem : UserControl
    {
        // Thông tin một suất chiếu hiển thị thành một nút giờ
        public class ShowtimeInfo
        {
            public int ShowtimeID { get; set; }
            public DateTime Start { get; set; }
            public string RoomName { get; set; } // null = suất chiếu chưa gán phòng
        }

        private readonly ToolTip toolTip = new ToolTip();

        public MovieItem()
        {
            InitializeComponent();
        }

        public void SetData(string name, Image poster, List<ShowtimeInfo> showtimes)
        {
            lblMovieName.Text = name;
            picPoster.Image = poster; // null = phim chưa có poster

            flowTimes.Controls.Clear();

            foreach (ShowtimeInfo st in showtimes)
            {
                bool coTheDat = st.RoomName != null && st.Start > DateTime.Now;

                Button btn = new Button();
                btn.Text = st.Start.ToString("HH:mm");
                btn.Width = 70;
                btn.Height = 30;
                btn.BackColor = coTheDat ? Color.LightSkyBlue : Color.LightGray;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Tag = st;
                btn.Click += Time_Click;
                toolTip.SetToolTip(btn, st.RoomName == null ? "Chưa gán phòng chiếu"
                                      : st.Start <= DateTime.Now ? st.RoomName + " - đã bắt đầu"
                                      : st.RoomName);
                flowTimes.Controls.Add(btn);
            }
        }

        // Chọn giờ chiếu -> mở sơ đồ ghế của suất chiếu đó
        private void Time_Click(object sender, EventArgs e)
        {
            ShowtimeInfo st = (ShowtimeInfo)((Button)sender).Tag;

            if (st.RoomName == null)
            {
                MessageBox.Show("Suất chiếu này chưa được gán phòng chiếu, chưa thể đặt vé.", "Thông báo");
                return;
            }
            if (st.Start <= DateTime.Now)
            {
                MessageBox.Show("Suất chiếu này đã bắt đầu, không thể đặt vé.", "Thông báo");
                return;
            }

            using (phòng_chiếu pc = new phòng_chiếu(st.ShowtimeID))
            {
                pc.ShowDialog(FindForm());
            }
        }
    }
}
