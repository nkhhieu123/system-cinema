using System;
using System.Drawing;
using System.Windows.Forms;

namespace rạp_chiếu_phim.khách_hàng
{
    // Một món trong danh sách bắp nước: ảnh, tên, mô tả, giá và nút tăng / giảm số lượng
    public partial class Order_thức_uống : UserControl
    {
        private const int SoLuongToiDa = 50;

        private int soLuong = 0;
        private decimal gia = 0;

        public int IDDoUong { get; private set; }
        public string TenMon { get { return lblTenMon.Text; } }
        public decimal Gia { get { return gia; } }

        public event EventHandler SoLuongThayDoi;

        public Order_thức_uống()
        {
            InitializeComponent();
            txtSoLuong.ReadOnly = true;
        }

        public void SetThongTin(int idDoUong, string ten, string moTa, decimal gia, Image anh)
        {
            IDDoUong = idDoUong;
            lblTenMon.Text = ten;
            lblMoTa.Text = moTa;
            lblGia.Text = $"Giá: {gia:N0} đ";
            pictureBoxMon.Image = anh;
            this.gia = gia;
        }

        private void DatSoLuong(int value)
        {
            soLuong = Math.Max(0, Math.Min(SoLuongToiDa, value));
            txtSoLuong.Text = soLuong.ToString();
            SoLuongThayDoi?.Invoke(this, EventArgs.Empty);
        }

        private void btnTang_Click(object sender, EventArgs e)
        {
            DatSoLuong(soLuong + 1);
        }

        private void btnGiam_Click(object sender, EventArgs e)
        {
            DatSoLuong(soLuong - 1);
        }

        public int LaySoLuong()
        {
            return soLuong;
        }
    }
}
