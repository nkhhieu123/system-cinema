namespace cinema_system.nhân_viên
{
    partial class SoDoGhe
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblLoai = new System.Windows.Forms.Label();
            this.rbThuong = new System.Windows.Forms.RadioButton();
            this.rbVIP = new System.Windows.Forms.RadioButton();
            this.rbSweetbox = new System.Windows.Forms.RadioButton();
            this.rbKhongDung = new System.Windows.Forms.RadioButton();
            this.lblSoHang = new System.Windows.Forms.Label();
            this.numHang = new System.Windows.Forms.NumericUpDown();
            this.lblSoCot = new System.Windows.Forms.Label();
            this.numCot = new System.Windows.Forms.NumericUpDown();
            this.btnTaoLai = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.panelGhe = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCot)).BeginInit();
            this.SuspendLayout();
            //
            // panelTop
            //
            this.panelTop.Controls.Add(this.lblLoai);
            this.panelTop.Controls.Add(this.rbThuong);
            this.panelTop.Controls.Add(this.rbVIP);
            this.panelTop.Controls.Add(this.rbSweetbox);
            this.panelTop.Controls.Add(this.rbKhongDung);
            this.panelTop.Controls.Add(this.lblSoHang);
            this.panelTop.Controls.Add(this.numHang);
            this.panelTop.Controls.Add(this.lblSoCot);
            this.panelTop.Controls.Add(this.numCot);
            this.panelTop.Controls.Add(this.btnTaoLai);
            this.panelTop.Controls.Add(this.btnLuu);
            this.panelTop.Controls.Add(this.lblHuongDan);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 125);
            this.panelTop.TabIndex = 0;
            //
            // lblLoai
            //
            this.lblLoai.AutoSize = true;
            this.lblLoai.Location = new System.Drawing.Point(12, 15);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(66, 19);
            this.lblLoai.TabIndex = 0;
            this.lblLoai.Text = "Loại ghế:";
            //
            // rbThuong
            //
            this.rbThuong.AutoSize = true;
            this.rbThuong.BackColor = System.Drawing.Color.LightGreen;
            this.rbThuong.Location = new System.Drawing.Point(95, 13);
            this.rbThuong.Name = "rbThuong";
            this.rbThuong.Size = new System.Drawing.Size(80, 23);
            this.rbThuong.TabIndex = 1;
            this.rbThuong.Text = "Thường";
            this.rbThuong.UseVisualStyleBackColor = false;
            //
            // rbVIP
            //
            this.rbVIP.AutoSize = true;
            this.rbVIP.BackColor = System.Drawing.Color.Orange;
            this.rbVIP.Location = new System.Drawing.Point(195, 13);
            this.rbVIP.Name = "rbVIP";
            this.rbVIP.Size = new System.Drawing.Size(55, 23);
            this.rbVIP.TabIndex = 2;
            this.rbVIP.Text = "VIP";
            this.rbVIP.UseVisualStyleBackColor = false;
            //
            // rbSweetbox
            //
            this.rbSweetbox.AutoSize = true;
            this.rbSweetbox.BackColor = System.Drawing.Color.HotPink;
            this.rbSweetbox.Location = new System.Drawing.Point(270, 13);
            this.rbSweetbox.Name = "rbSweetbox";
            this.rbSweetbox.Size = new System.Drawing.Size(95, 23);
            this.rbSweetbox.TabIndex = 3;
            this.rbSweetbox.Text = "Sweetbox";
            this.rbSweetbox.UseVisualStyleBackColor = false;
            //
            // rbKhongDung
            //
            this.rbKhongDung.AutoSize = true;
            this.rbKhongDung.BackColor = System.Drawing.Color.Gainsboro;
            this.rbKhongDung.Location = new System.Drawing.Point(385, 13);
            this.rbKhongDung.Name = "rbKhongDung";
            this.rbKhongDung.Size = new System.Drawing.Size(200, 23);
            this.rbKhongDung.TabIndex = 4;
            this.rbKhongDung.Text = "Không dùng (lối đi / hỏng)";
            this.rbKhongDung.UseVisualStyleBackColor = false;
            //
            // lblSoHang
            //
            this.lblSoHang.AutoSize = true;
            this.lblSoHang.Location = new System.Drawing.Point(12, 55);
            this.lblSoHang.Name = "lblSoHang";
            this.lblSoHang.Size = new System.Drawing.Size(63, 19);
            this.lblSoHang.TabIndex = 5;
            this.lblSoHang.Text = "Số hàng";
            //
            // numHang
            //
            this.numHang.Location = new System.Drawing.Point(95, 52);
            this.numHang.Maximum = new decimal(new int[] { 26, 0, 0, 0 });
            this.numHang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numHang.Name = "numHang";
            this.numHang.Size = new System.Drawing.Size(65, 26);
            this.numHang.TabIndex = 6;
            this.numHang.Value = new decimal(new int[] { 10, 0, 0, 0 });
            //
            // lblSoCot
            //
            this.lblSoCot.AutoSize = true;
            this.lblSoCot.Location = new System.Drawing.Point(180, 55);
            this.lblSoCot.Name = "lblSoCot";
            this.lblSoCot.Size = new System.Drawing.Size(105, 19);
            this.lblSoCot.TabIndex = 7;
            this.lblSoCot.Text = "Ghế mỗi hàng";
            //
            // numCot
            //
            this.numCot.Location = new System.Drawing.Point(290, 52);
            this.numCot.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            this.numCot.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numCot.Name = "numCot";
            this.numCot.Size = new System.Drawing.Size(65, 26);
            this.numCot.TabIndex = 8;
            this.numCot.Value = new decimal(new int[] { 12, 0, 0, 0 });
            //
            // btnTaoLai
            //
            this.btnTaoLai.Location = new System.Drawing.Point(375, 47);
            this.btnTaoLai.Name = "btnTaoLai";
            this.btnTaoLai.Size = new System.Drawing.Size(150, 36);
            this.btnTaoLai.TabIndex = 9;
            this.btnTaoLai.Text = "Tạo lại sơ đồ";
            this.btnTaoLai.UseVisualStyleBackColor = true;
            this.btnTaoLai.Click += new System.EventHandler(this.btnTaoLai_Click);
            //
            // btnLuu
            //
            this.btnLuu.BackColor = System.Drawing.Color.Firebrick;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(545, 47);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(150, 36);
            this.btnLuu.TabIndex = 10;
            this.btnLuu.Text = "Lưu loại ghế";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            //
            // lblHuongDan
            //
            this.lblHuongDan.Location = new System.Drawing.Point(12, 92);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(975, 25);
            this.lblHuongDan.TabIndex = 11;
            this.lblHuongDan.Text = "Chọn loại ghế ở trên rồi bấm vào ghế, hoặc bấm chữ cái đầu hàng để đổi cả hàng.";
            //
            // panelGhe
            //
            this.panelGhe.AutoScroll = true;
            this.panelGhe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(237)))));
            this.panelGhe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGhe.Location = new System.Drawing.Point(0, 125);
            this.panelGhe.Name = "panelGhe";
            this.panelGhe.Size = new System.Drawing.Size(1000, 575);
            this.panelGhe.TabIndex = 1;
            //
            // SoDoGhe
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panelGhe);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeBox = false;
            this.Name = "SoDoGhe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sơ đồ ghế";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SoDoGhe_FormClosing);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.RadioButton rbThuong;
        private System.Windows.Forms.RadioButton rbVIP;
        private System.Windows.Forms.RadioButton rbSweetbox;
        private System.Windows.Forms.RadioButton rbKhongDung;
        private System.Windows.Forms.Label lblSoHang;
        private System.Windows.Forms.NumericUpDown numHang;
        private System.Windows.Forms.Label lblSoCot;
        private System.Windows.Forms.NumericUpDown numCot;
        private System.Windows.Forms.Button btnTaoLai;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.Panel panelGhe;
    }
}
