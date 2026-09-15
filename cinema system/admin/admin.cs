using cinema_system.nhân_viên;
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

namespace cinema_system.admin
{
    public partial class admin_design : Form
    {
        public admin_design()
        {
            InitializeComponent();
        }

        Panel indicator;

        private void highlight_Load(object sender, EventArgs e)
        {
            // Tạo thanh highlight
            indicator = new Panel();
            indicator.Size = new Size(5, Quanly.Height); // cao bằng đúng button
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

        // Hiện một UserControl vào vùng nội dung bên phải
        private void ShowContent(Control content)
        {
            panel9.Controls.Clear();
            content.Dock = DockStyle.Fill;
            panel9.Controls.Add(content);
        }

        // Sự kiện các nút menu
        private void SuatChieu_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            ShowContent(new ThemMovie());
        }

        // Nút "Quản lý tài khoản"
        private void Ve_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            ShowContent(new AllStaff());
        }

        private void Phim_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            ShowContent(new addmovie());
        }

        // Nút "Thống kê" (chưa làm)
        private void HoannVe_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Program.SwitchForm(this, new Đăng_nhập());
        }
    }
}