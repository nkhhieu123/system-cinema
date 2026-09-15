using cinema_system.đăng_nhập;
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
            public int Duration { get; set; }    // phút
        }

        private readonly ToolTip toolTip = new ToolTip();

        public MovieItem()
        {
            InitializeComponent();
        }

        public void SetData(string name, int? duration, Image poster, List<ShowtimeInfo> showtimes)
        {
            lblMovieName.Text = duration.HasValue ? $"{name} ({duration} phút)" : name;
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
                string gio = $"{st.Start:HH:mm} - {st.Start.AddMinutes(st.Duration):HH:mm}";
                toolTip.SetToolTip(btn, st.RoomName == null ? "Chưa gán phòng chiếu"
                                      : st.Start <= DateTime.Now ? $"{st.RoomName} ({gio}) - đã bắt đầu"
                                      : $"{st.RoomName} ({gio})");
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

            Form owner = FindForm();
            DialogResult result;
            using (phòng_chiếu pc = new phòng_chiếu(st.ShowtimeID))
            {
                result = pc.ShowDialog(owner);
            }

            // Khách chưa đăng nhập bấm "Đăng nhập để đặt vé"
            if (result == DialogResult.Retry && owner != null)
                Program.SwitchForm(owner, new Đăng_nhập());
        }
    }
}
