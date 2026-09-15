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

namespace rạp_chiếu_phim.khách_hàng
{
    public partial class phòng_chiếu : Form
    {
        private List<string> gheDaChon = new List<string>();

        // Giá ghế mặc định
        private int giaThuong = 70000;
        private int giaVIP = 100000;
        private int giaSweetbox = 150000;

        public phòng_chiếu()
        {
            InitializeComponent();
            VeSoDoGhe();
            LoadPrice();
        }

        private void LoadPrice()
        {
            // Hiện đang dùng giá ghế mặc định ở trên (DB chưa có bảng giá theo loại ghế)
            // Cập nhật chú thích
            legendPanel.Controls.Clear();
            AddLegendItem(Color.LightGreen, $"Ghế Thường - {giaThuong:N0} đ");
            AddLegendItem(Color.Orange, $"Ghế VIP - {giaVIP:N0} đ");
            AddLegendItem(Color.HotPink, $"Ghế Sweetbox - {giaSweetbox:N0} đ");
            AddLegendItem(Color.Brown, "Ghế Đã Chọn");
        }

        private void AddLegendItem(Color color, string text)
        {
            Panel colorBox = new Panel()
            {
                BackColor = color,
                Size = new Size(20, 20),
                Margin = new Padding(8, 8, 3, 3)
            };
            Label lbl = new Label()
            {
                Text = text,
                AutoSize = true,
                Margin = new Padding(3, 6, 8, 3)
            };
            FlowLayoutPanel item = new FlowLayoutPanel() { AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
            item.Controls.Add(colorBox);
            item.Controls.Add(lbl);
            this.legendPanel.Controls.Add(item);
        }

        private void VeSoDoGhe()
        {
            int rows = 10;   // số hàng
            int cols = 12;  // số ghế mỗi hàng

            int startX = 60;
            int startY = 20;
            int size = 60;
            int spacing = 5;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button ghe = new Button();
                    ghe.Width = size;
                    ghe.Height = size;
                    ghe.Left = startX + j * (size + spacing);
                    ghe.Top = startY + i * (size + spacing);
                    ghe.Text = $"{(char)('A' + i)}{j + 1}";
                    ghe.Tag = "Thuong"; // mặc định

                    // Gán loại ghế
                    if (i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8) ghe.Tag = "VIP";
                    if (i == 9) ghe.Tag = "Sweetbox";

                    // Màu theo loại
                    if (ghe.Tag.ToString() == "Thuong")
                        ghe.BackColor = Color.LightGreen;
                    else if (ghe.Tag.ToString() == "VIP")
                        ghe.BackColor = Color.Orange;
                    else if (ghe.Tag.ToString() == "Sweetbox")
                        ghe.BackColor = Color.HotPink;

                    ghe.Click += Ghe_Click;

                    panelGhe.Controls.Add(ghe);
                }
            }
        }

        private void Ghe_Click(object sender, EventArgs e)
        {
            Button ghe = sender as Button;
            string tenGhe = ghe.Text;
            string loai = ghe.Tag.ToString();

            if (ghe.BackColor == Color.Brown) // nếu đang chọn -> bỏ chọn
            {
                if (loai == "Thuong") ghe.BackColor = Color.LightGreen;
                else if (loai == "VIP") ghe.BackColor = Color.Orange;
                else if (loai == "Sweetbox") ghe.BackColor = Color.HotPink;

                gheDaChon.Remove(tenGhe);
            }
            else // chọn ghế
            {
                ghe.BackColor = Color.Brown;
                if (!gheDaChon.Contains(tenGhe))
                    gheDaChon.Add(tenGhe);
            }

            CapNhatThongTin();
        }

        private void CapNhatThongTin()
        {
            // Hiển thị danh sách ghế
            lblDanhSachGhe.Text = "Ghế: " + (gheDaChon.Count > 0 ? string.Join(", ", gheDaChon) : "(chưa chọn)");

            // Tính tiền
            int tong = 0;
            foreach (Control c in panelGhe.Controls)
            {
                if (c is Button ghe && ghe.BackColor == Color.Brown)
                {
                    string loai = ghe.Tag.ToString();
                    if (loai == "Thuong") tong += giaThuong;
                    else if (loai == "VIP") tong += giaVIP;
                    else if (loai == "Sweetbox") tong += giaSweetbox;
                }
            }
            lblTongTien.Text = $"Tổng tiền: {tong:N0} đ";
        }
    }
}
