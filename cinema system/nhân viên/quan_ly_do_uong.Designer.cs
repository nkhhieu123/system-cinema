namespace cinema_system.nhân_viên
{
    partial class quan_ly_do_uong
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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblGia = new System.Windows.Forms.Label();
            this.numGia = new System.Windows.Forms.NumericUpDown();
            this.chkDangBan = new System.Windows.Forms.CheckBox();
            this.picHinh = new System.Windows.Forms.PictureBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.btnBoAnh = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnBanTaiQuay = new System.Windows.Forms.Button();
            this.dgvDoUong = new System.Windows.Forms.DataGridView();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoUong)).BeginInit();
            this.SuspendLayout();
            //
            // panelLeft
            //
            this.panelLeft.Controls.Add(this.lblTitle);
            this.panelLeft.Controls.Add(this.lblTen);
            this.panelLeft.Controls.Add(this.txtTen);
            this.panelLeft.Controls.Add(this.lblMoTa);
            this.panelLeft.Controls.Add(this.txtMoTa);
            this.panelLeft.Controls.Add(this.lblGia);
            this.panelLeft.Controls.Add(this.numGia);
            this.panelLeft.Controls.Add(this.chkDangBan);
            this.panelLeft.Controls.Add(this.picHinh);
            this.panelLeft.Controls.Add(this.btnChonAnh);
            this.panelLeft.Controls.Add(this.btnBoAnh);
            this.panelLeft.Controls.Add(this.btnThem);
            this.panelLeft.Controls.Add(this.btnSua);
            this.panelLeft.Controls.Add(this.btnXoa);
            this.panelLeft.Controls.Add(this.btnLamMoi);
            this.panelLeft.Controls.Add(this.btnBanTaiQuay);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(390, 600);
            this.panelLeft.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BẮP NƯỚC";
            //
            // lblTen
            //
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(15, 58);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(66, 19);
            this.lblTen.TabIndex = 1;
            this.lblTen.Text = "Tên món";
            //
            // txtTen
            //
            this.txtTen.Location = new System.Drawing.Point(110, 55);
            this.txtTen.MaxLength = 100;
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(260, 26);
            this.txtTen.TabIndex = 2;
            //
            // lblMoTa
            //
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(15, 98);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(47, 19);
            this.lblMoTa.TabIndex = 3;
            this.lblMoTa.Text = "Mô tả";
            //
            // txtMoTa
            //
            this.txtMoTa.Location = new System.Drawing.Point(110, 95);
            this.txtMoTa.MaxLength = 255;
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(260, 60);
            this.txtMoTa.TabIndex = 4;
            //
            // lblGia
            //
            this.lblGia.AutoSize = true;
            this.lblGia.Location = new System.Drawing.Point(15, 173);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(58, 19);
            this.lblGia.TabIndex = 5;
            this.lblGia.Text = "Giá (đ)";
            //
            // numGia
            //
            this.numGia.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numGia.Location = new System.Drawing.Point(110, 170);
            this.numGia.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numGia.Name = "numGia";
            this.numGia.Size = new System.Drawing.Size(150, 26);
            this.numGia.TabIndex = 6;
            this.numGia.ThousandsSeparator = true;
            //
            // chkDangBan
            //
            this.chkDangBan.AutoSize = true;
            this.chkDangBan.Checked = true;
            this.chkDangBan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDangBan.Location = new System.Drawing.Point(110, 208);
            this.chkDangBan.Name = "chkDangBan";
            this.chkDangBan.Size = new System.Drawing.Size(92, 23);
            this.chkDangBan.TabIndex = 7;
            this.chkDangBan.Text = "Đang bán";
            this.chkDangBan.UseVisualStyleBackColor = true;
            //
            // picHinh
            //
            this.picHinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHinh.Location = new System.Drawing.Point(110, 243);
            this.picHinh.Name = "picHinh";
            this.picHinh.Size = new System.Drawing.Size(140, 140);
            this.picHinh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHinh.TabIndex = 8;
            this.picHinh.TabStop = false;
            //
            // btnChonAnh
            //
            this.btnChonAnh.Location = new System.Drawing.Point(260, 243);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(110, 34);
            this.btnChonAnh.TabIndex = 9;
            this.btnChonAnh.Text = "Chọn ảnh";
            this.btnChonAnh.UseVisualStyleBackColor = true;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            //
            // btnBoAnh
            //
            this.btnBoAnh.Location = new System.Drawing.Point(260, 285);
            this.btnBoAnh.Name = "btnBoAnh";
            this.btnBoAnh.Size = new System.Drawing.Size(110, 34);
            this.btnBoAnh.TabIndex = 10;
            this.btnBoAnh.Text = "Bỏ ảnh";
            this.btnBoAnh.UseVisualStyleBackColor = true;
            this.btnBoAnh.Click += new System.EventHandler(this.btnBoAnh_Click);
            //
            // btnThem
            //
            this.btnThem.BackColor = System.Drawing.Color.Maroon;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(15, 405);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(110, 38);
            this.btnThem.TabIndex = 11;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            //
            // btnSua
            //
            this.btnSua.BackColor = System.Drawing.Color.Maroon;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(137, 405);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(110, 38);
            this.btnSua.TabIndex = 12;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            //
            // btnXoa
            //
            this.btnXoa.BackColor = System.Drawing.Color.Maroon;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(260, 405);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(110, 38);
            this.btnXoa.TabIndex = 13;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            //
            // btnLamMoi
            //
            this.btnLamMoi.Location = new System.Drawing.Point(15, 455);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 38);
            this.btnLamMoi.TabIndex = 14;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            //
            // btnBanTaiQuay
            //
            this.btnBanTaiQuay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.btnBanTaiQuay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBanTaiQuay.ForeColor = System.Drawing.Color.White;
            this.btnBanTaiQuay.Location = new System.Drawing.Point(137, 455);
            this.btnBanTaiQuay.Name = "btnBanTaiQuay";
            this.btnBanTaiQuay.Size = new System.Drawing.Size(233, 38);
            this.btnBanTaiQuay.TabIndex = 15;
            this.btnBanTaiQuay.Text = "Bán tại quầy...";
            this.btnBanTaiQuay.UseVisualStyleBackColor = false;
            this.btnBanTaiQuay.Click += new System.EventHandler(this.btnBanTaiQuay_Click);
            //
            // dgvDoUong
            //
            this.dgvDoUong.AllowUserToAddRows = false;
            this.dgvDoUong.AllowUserToDeleteRows = false;
            this.dgvDoUong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoUong.BackgroundColor = System.Drawing.Color.White;
            this.dgvDoUong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoUong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoUong.Location = new System.Drawing.Point(390, 0);
            this.dgvDoUong.MultiSelect = false;
            this.dgvDoUong.Name = "dgvDoUong";
            this.dgvDoUong.ReadOnly = true;
            this.dgvDoUong.RowHeadersWidth = 51;
            this.dgvDoUong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoUong.Size = new System.Drawing.Size(610, 600);
            this.dgvDoUong.TabIndex = 1;
            this.dgvDoUong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDoUong_CellClick);
            //
            // quan_ly_do_uong
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvDoUong);
            this.Controls.Add(this.panelLeft);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "quan_ly_do_uong";
            this.Size = new System.Drawing.Size(1000, 600);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoUong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.NumericUpDown numGia;
        private System.Windows.Forms.CheckBox chkDangBan;
        private System.Windows.Forms.PictureBox picHinh;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.Button btnBoAnh;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnBanTaiQuay;
        private System.Windows.Forms.DataGridView dgvDoUong;
    }
}
