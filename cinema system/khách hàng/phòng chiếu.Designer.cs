using System.Drawing;
using System.Windows.Forms;

namespace rạp_chiếu_phim.khách_hàng
{
    partial class phòng_chiếu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new Panel();
            this.lblScreen = new Label();
            this.panelGhe = new Panel();
            this.panelBottom = new Panel();
            this.lblDanhSachGhe = new Label();
            this.lblTongTien = new Label();
            this.btnPrev = new Button();
            this.btnNext = new Button();
            this.legendPanel = new FlowLayoutPanel();
            // 
            // Form
            // 
            this.Font = new Font("Times new Roman", 12F, FontStyle.Regular);
            this.ClientSize = new Size(950, 700);
            this.Text = "Đặt Vé CGV";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            // 
            // panelTop (hiển thị SCREEN)
            // 
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 50;
            this.panelTop.BackColor = Color.White;
            //
            // lblScreen
            //
            this.lblScreen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScreen.Text = "SCREEN";
            this.lblScreen.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblScreen.TextAlign = ContentAlignment.MiddleCenter;
            this.lblScreen.Dock = DockStyle.Fill;
            //
            // Add lblScreen to panelTop
            //
            this.panelTop.Controls.Add(this.lblScreen);
            // 
            // panelGhe (sơ đồ ghế)
            // 
            this.panelGhe.Dock = DockStyle.Fill;
            this.panelGhe.BackColor = Color.FromArgb(250, 247, 237);
            this.panelGhe.AutoScroll = true;
            // 
            // panelBottom (hiển thị thông tin + nút)
            // 
            this.panelBottom.Dock = DockStyle.Bottom;
            this.panelBottom.Height = 160;
            this.panelBottom.BackColor = Color.WhiteSmoke;
            //
            // lblDanhSachGhe
            //
            this.lblDanhSachGhe.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanhSachGhe.AutoSize = true;
            this.lblDanhSachGhe.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblDanhSachGhe.Text = "Ghế: (chưa chọn)";
            this.lblDanhSachGhe.Location = new Point(20, 20);
            //
            // lblTongTien
            //
            this.lblTongTien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTongTien.Text = "Tổng tiền: 0 đ";
            this.lblTongTien.Location = new Point(20, 55);
            //
            // btnPrev
            //
            this.btnPrev.Text = "QUAY LẠI";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            this.btnPrev.Size = new Size(120, 48);
            this.btnPrev.Location = new Point(20, 100);
            this.btnPrev.BackColor = Color.FromArgb(45, 45, 45);
            this.btnPrev.ForeColor = Color.White;
            this.btnPrev.FlatStyle = FlatStyle.Flat;
            this.lblTongTien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //
            // btnNext
            //
            this.btnNext.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Text = "ĐẶT VÉ";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            this.btnNext.Size = new Size(120, 48);
            this.btnNext.Location = new Point(160, 100);
            this.btnNext.BackColor = Color.FromArgb(200, 30, 45);
            this.btnNext.ForeColor = Color.White;
            this.btnNext.FlatStyle = FlatStyle.Flat;
            //
            // legendPanel
            //
            this.legendPanel.FlowDirection = FlowDirection.LeftToRight;
            this.legendPanel.Location = new Point(320, 20);
            this.legendPanel.Size = new Size(600, 120);
            this.legendPanel.AutoSize = true;
            this.legendPanel.WrapContents = true;

            // Thêm chú thích
            AddLegendItem(Color.Brown, "Checked (Đang chọn)");
            AddLegendItem(Color.LightGreen, "Thường");
            AddLegendItem(Color.Orange, "VIP");
            AddLegendItem(Color.HotPink, "Sweetbox");
            AddLegendItem(Color.Gray, "Đã đặt / Không thể chọn");

            // Panel bottom controls
            this.panelBottom.Controls.Add(this.lblDanhSachGhe);
            this.panelBottom.Controls.Add(this.lblTongTien);
            this.panelBottom.Controls.Add(this.btnPrev);
            this.panelBottom.Controls.Add(this.btnNext);
            this.panelBottom.Controls.Add(this.legendPanel);

            // Add panels vào Form
            this.Controls.Add(this.panelGhe);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
        }

        private Panel panelTop;
        private Label lblScreen;
        private Panel panelGhe;
        private Panel panelBottom;
        private Label lblDanhSachGhe;
        private Label lblTongTien;
        private Button btnPrev;
        private Button btnNext;
        private FlowLayoutPanel legendPanel;


        #endregion
    }
}