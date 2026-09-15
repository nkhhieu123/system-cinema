using rạp_chiếu_phim.khách_hàng;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinema_system.nhân_viên
{
    public partial class MovieItem : UserControl
    {
        public MovieItem()
        {
            InitializeComponent();
        }

        public void SetData(string name, string poster, List<TimeSpan> times)
        {
            lblMovieName.Text = name;

            // Phim chưa có poster hoặc file ảnh đã bị xóa/di chuyển thì bỏ qua, không crash
            if (!string.IsNullOrEmpty(poster) && File.Exists(poster))
            {
                using (Image img = Image.FromFile(poster))
                {
                    picPoster.Image = new Bitmap(img);
                }
            }
            flowTimes.Controls.Clear();

            foreach (var time in times)
            {
                Button btn = new Button();
                btn.Text = time.ToString(@"hh\:mm");
                btn.Width = 70;
                btn.Height = 30;
                btn.BackColor = Color.LightSkyBlue;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Click += Time_Click;
                flowTimes.Controls.Add(btn);

            }
        }

        // Chọn giờ chiếu -> mở sơ đồ ghế
        private void Time_Click(object sender, EventArgs e)
        {
            using (phòng_chiếu pc = new phòng_chiếu())
            {
                pc.Text = lblMovieName.Text + " - " + ((Button)sender).Text;
                pc.ShowDialog(FindForm());
            }
        }

    }
}
