using cinema_system.đăng_nhập;
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
    public partial class StaffDesign : Form
    {
        public StaffDesign()
        {
            InitializeComponent();
        }

        Panel indicator;

        private void highlight_Load(object sender, EventArgs e)
        {
            // Tạo thanh highlight
            indicator = new Panel();
            indicator.Size = new Size(5, Ve.Height); // cao bằng đúng button
            indicator.BackColor = Color.Red;
            indicator.Visible = false;

            // sidebar = panel chứa menu (thay bằng tên panel của bạn)
            panel2.Controls.Add(indicator);
        }

        // Hàm dùng chung
        private void ActivateButton(Button btn)
        {
            indicator.Visible = true;
            indicator.Height = btn.Height;  // chiều cao bằng button
            indicator.Location = new Point(btn.Width - indicator.Width, btn.Top);
            indicator.BringToFront();
        }

        // Sự kiện các nút menu
        private void SuatChieu_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            panel9.Controls.Clear();
            ThemMovie them = new ThemMovie();
            them.Dock = DockStyle.Fill;
            panel9.Controls.Add(them);
        }

        private void Ve_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            panel9.Controls.Clear();
            dat_ve ve = new dat_ve();
            ve.Dock = DockStyle.Fill;
            panel9.Controls.Add(ve);
        }

        private void PhongChieu_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            panel9.Controls.Clear();
            RoomMovieControl roomMovie = new RoomMovieControl();
            roomMovie.Dock = DockStyle.Fill;
            panel9.Controls.Add(roomMovie);
        }

        private void Phim_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            panel9.Controls.Clear();
            addmovie movie = new addmovie();
            movie.Dock =DockStyle.Fill;
            panel9.Controls.Add(movie);
        }

        private void HoannVe_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Program.SwitchForm(this, new Đăng_nhập());
        }

        //private void StaffDesign_Load(object sender, EventArgs e)
        //{
        //    string path = Path.Combine(Application.StartupPath, "picture");

        //    carouselControl1.LoadSlides(
        //        Path.Combine(path, "movie1.jpeg"),
        //        Path.Combine(path, "movie2.jpeg")
        //    );
        //}
    }
}
