using cinema_system.khách_hàng;
using cinema_system.nhân_viên;
using cinema_system.đăng_nhập;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinema_system.Khách_hàng
{
    public partial class thông_tin_khách_hàng : Form
    {
        private readonly string tenDangNhap;
        public thông_tin_khách_hàng(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
            label2.Text = "Xin chào " + tenDangNhap;
        }

        private void Ve_Click(object sender, EventArgs e)
        {
            panel9.Controls.Clear();
            dat_ve dat_ve = new dat_ve();
            dat_ve.Dock = DockStyle.Fill;
            panel9.Controls.Add(dat_ve);
        }

        private void ThongTin_Click(object sender, EventArgs e)
        {
            panel9.Controls.Clear();
            thông_tin_chung thong_tin_chung = new thông_tin_chung(tenDangNhap);
            thong_tin_chung.Dock = DockStyle.Fill;
            panel9.Controls.Add(thong_tin_chung);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel9.Controls.Clear();
            thay_doi_thong_tin thay_Doi_Thong_Tin = new thay_doi_thong_tin(tenDangNhap);
            thay_Doi_Thong_Tin.Dock = DockStyle.Fill;
            panel9.Controls.Add(thay_Doi_Thong_Tin);
        }

        private void HoanVe_Click(object sender, EventArgs e)
        {
            panel9.Controls.Clear();
            hoan_ve hoanVe = new hoan_ve(true);
            hoanVe.Dock = DockStyle.Fill;
            panel9.Controls.Add(hoanVe);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Program.SwitchForm(this, new Đăng_nhập());
        }
    }
}
