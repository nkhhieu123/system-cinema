namespace cinema_system.admin
{
    partial class thong_ke
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTu = new System.Windows.Forms.Label();
            this.dtpTu = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtpDen = new System.Windows.Forms.DateTimePicker();
            this.cbNhom = new System.Windows.Forms.ComboBox();
            this.btnXem = new System.Windows.Forms.Button();
            this.lblSoVe = new System.Windows.Forms.Label();
            this.lblDoanhThu = new System.Windows.Forms.Label();
            this.lblVeHoan = new System.Windows.Forms.Label();
            this.lblSuatChieu = new System.Windows.Forms.Label();
            this.lblDoUong = new System.Windows.Forms.Label();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.dgvThongKe = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).BeginInit();
            this.SuspendLayout();
            //
            // panelTop
            //
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblTu);
            this.panelTop.Controls.Add(this.dtpTu);
            this.panelTop.Controls.Add(this.lblDen);
            this.panelTop.Controls.Add(this.dtpDen);
            this.panelTop.Controls.Add(this.cbNhom);
            this.panelTop.Controls.Add(this.btnXem);
            this.panelTop.Controls.Add(this.lblSoVe);
            this.panelTop.Controls.Add(this.lblDoanhThu);
            this.panelTop.Controls.Add(this.lblVeHoan);
            this.panelTop.Controls.Add(this.lblSuatChieu);
            this.panelTop.Controls.Add(this.lblDoUong);
            this.panelTop.Controls.Add(this.lblTongDoanhThu);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 215);
            this.panelTop.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(128, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THỐNG KÊ";
            //
            // lblTu
            //
            this.lblTu.AutoSize = true;
            this.lblTu.Location = new System.Drawing.Point(17, 57);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(66, 19);
            this.lblTu.TabIndex = 1;
            this.lblTu.Text = "Từ ngày";
            //
            // dtpTu
            //
            this.dtpTu.CustomFormat = "dd/MM/yyyy";
            this.dtpTu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTu.Location = new System.Drawing.Point(90, 53);
            this.dtpTu.Name = "dtpTu";
            this.dtpTu.Size = new System.Drawing.Size(140, 26);
            this.dtpTu.TabIndex = 2;
            //
            // lblDen
            //
            this.lblDen.AutoSize = true;
            this.lblDen.Location = new System.Drawing.Point(250, 57);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(74, 19);
            this.lblDen.TabIndex = 3;
            this.lblDen.Text = "Đến ngày";
            //
            // dtpDen
            //
            this.dtpDen.CustomFormat = "dd/MM/yyyy";
            this.dtpDen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDen.Location = new System.Drawing.Point(330, 53);
            this.dtpDen.Name = "dtpDen";
            this.dtpDen.Size = new System.Drawing.Size(140, 26);
            this.dtpDen.TabIndex = 4;
            //
            // cbNhom
            //
            this.cbNhom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNhom.Location = new System.Drawing.Point(490, 53);
            this.cbNhom.Name = "cbNhom";
            this.cbNhom.Size = new System.Drawing.Size(190, 27);
            this.cbNhom.TabIndex = 5;
            //
            // btnXem
            //
            this.btnXem.BackColor = System.Drawing.Color.Firebrick;
            this.btnXem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXem.ForeColor = System.Drawing.Color.White;
            this.btnXem.Location = new System.Drawing.Point(700, 48);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(110, 36);
            this.btnXem.TabIndex = 6;
            this.btnXem.Text = "Xem";
            this.btnXem.UseVisualStyleBackColor = false;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            //
            // lblSoVe
            //
            this.lblSoVe.AutoSize = true;
            this.lblSoVe.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoVe.Location = new System.Drawing.Point(17, 100);
            this.lblSoVe.Name = "lblSoVe";
            this.lblSoVe.Size = new System.Drawing.Size(100, 25);
            this.lblSoVe.TabIndex = 7;
            this.lblSoVe.Text = "Vé đã bán: 0";
            //
            // lblDoanhThu
            //
            this.lblDoanhThu.AutoSize = true;
            this.lblDoanhThu.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoanhThu.ForeColor = System.Drawing.Color.Firebrick;
            this.lblDoanhThu.Location = new System.Drawing.Point(17, 135);
            this.lblDoanhThu.Name = "lblDoanhThu";
            this.lblDoanhThu.Size = new System.Drawing.Size(120, 25);
            this.lblDoanhThu.TabIndex = 8;
            this.lblDoanhThu.Text = "Doanh thu: 0 đ";
            //
            // lblVeHoan
            //
            this.lblVeHoan.AutoSize = true;
            this.lblVeHoan.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVeHoan.Location = new System.Drawing.Point(380, 100);
            this.lblVeHoan.Name = "lblVeHoan";
            this.lblVeHoan.Size = new System.Drawing.Size(110, 25);
            this.lblVeHoan.TabIndex = 9;
            this.lblVeHoan.Text = "Vé đã hoàn: 0";
            //
            // lblSuatChieu
            //
            this.lblSuatChieu.AutoSize = true;
            this.lblSuatChieu.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuatChieu.Location = new System.Drawing.Point(17, 170);
            this.lblSuatChieu.Name = "lblSuatChieu";
            this.lblSuatChieu.Size = new System.Drawing.Size(110, 25);
            this.lblSuatChieu.TabIndex = 10;
            this.lblSuatChieu.Text = "Suất chiếu: 0";
            //
            // lblDoUong
            //
            this.lblDoUong.AutoSize = true;
            this.lblDoUong.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoUong.Location = new System.Drawing.Point(380, 135);
            this.lblDoUong.Name = "lblDoUong";
            this.lblDoUong.Size = new System.Drawing.Size(110, 25);
            this.lblDoUong.TabIndex = 11;
            this.lblDoUong.Text = "Bắp nước: 0 đ";
            //
            // lblTongDoanhThu
            //
            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(380, 168);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(200, 27);
            this.lblTongDoanhThu.TabIndex = 12;
            this.lblTongDoanhThu.Text = "TỔNG DOANH THU: 0 đ";
            //
            // dgvThongKe
            //
            this.dgvThongKe.AllowUserToAddRows = false;
            this.dgvThongKe.AllowUserToDeleteRows = false;
            this.dgvThongKe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThongKe.BackgroundColor = System.Drawing.Color.White;
            this.dgvThongKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThongKe.Location = new System.Drawing.Point(0, 215);
            this.dgvThongKe.MultiSelect = false;
            this.dgvThongKe.Name = "dgvThongKe";
            this.dgvThongKe.ReadOnly = true;
            this.dgvThongKe.RowHeadersWidth = 51;
            this.dgvThongKe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThongKe.Size = new System.Drawing.Size(1000, 450);
            this.dgvThongKe.TabIndex = 1;
            //
            // thong_ke
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvThongKe);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "thong_ke";
            this.Size = new System.Drawing.Size(1000, 600);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.DateTimePicker dtpTu;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtpDen;
        private System.Windows.Forms.ComboBox cbNhom;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Label lblSoVe;
        private System.Windows.Forms.Label lblDoanhThu;
        private System.Windows.Forms.Label lblVeHoan;
        private System.Windows.Forms.Label lblSuatChieu;
        private System.Windows.Forms.Label lblDoUong;
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.DataGridView dgvThongKe;
    }
}
