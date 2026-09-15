using cinema_system.đăng_nhập;
using cinema_system.nhân_viên;
using System;
using System.Windows.Forms;

namespace cinema_system.khách_hàng
{
    // Trang chào cho khách chưa đăng nhập: xem lịch chiếu thật, chọn giờ để xem ghế trống,
    // muốn đặt vé thì chuyển sang đăng nhập
    public partial class UserDesign : Form
    {
        public UserDesign()
        {
            InitializeComponent();

            dat_ve lichChieu = new dat_ve();
            lichChieu.Dock = DockStyle.Fill;
            Controls.Add(lichChieu);
            lichChieu.BringToFront(); // control Dock=Fill phải nằm trên cùng để không bị thanh tiêu đề che
        }

        private void dndk_Click(object sender, EventArgs e)
        {
            Program.SwitchForm(this, new Đăng_nhập());
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
