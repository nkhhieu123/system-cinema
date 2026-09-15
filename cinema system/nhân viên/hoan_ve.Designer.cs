namespace cinema_system.nhân_viên
{
    partial class hoan_ve
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
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.chkChuaChieu = new System.Windows.Forms.CheckBox();
            this.btnHoanVe = new System.Windows.Forms.Button();
            this.dgvVe = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVe)).BeginInit();
            this.SuspendLayout();
            //
            // panelTop
            //
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblGhiChu);
            this.panelTop.Controls.Add(this.lblTimKiem);
            this.panelTop.Controls.Add(this.txtTimKiem);
            this.panelTop.Controls.Add(this.btnTim);
            this.panelTop.Controls.Add(this.chkChuaChieu);
            this.panelTop.Controls.Add(this.btnHoanVe);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 115);
            this.panelTop.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(110, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HOÀN VÉ";
            //
            // lblGhiChu
            //
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(17, 45);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(560, 19);
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "Chỉ hoàn được vé của suất chiếu chưa bắt đầu. Giữ Ctrl để chọn nhiều vé.";
            //
            // lblTimKiem
            //
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(17, 80);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(73, 19);
            this.lblTimKiem.TabIndex = 2;
            this.lblTimKiem.Text = "Tìm kiếm:";
            //
            // txtTimKiem
            //
            this.txtTimKiem.Location = new System.Drawing.Point(100, 77);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(260, 26);
            this.txtTimKiem.TabIndex = 3;
            //
            // btnTim
            //
            this.btnTim.Location = new System.Drawing.Point(370, 75);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(80, 30);
            this.btnTim.TabIndex = 4;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            //
            // chkChuaChieu
            //
            this.chkChuaChieu.AutoSize = true;
            this.chkChuaChieu.Checked = true;
            this.chkChuaChieu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChuaChieu.Location = new System.Drawing.Point(470, 79);
            this.chkChuaChieu.Name = "chkChuaChieu";
            this.chkChuaChieu.Size = new System.Drawing.Size(210, 23);
            this.chkChuaChieu.TabIndex = 5;
            this.chkChuaChieu.Text = "Chỉ hiện suất chưa chiếu";
            this.chkChuaChieu.UseVisualStyleBackColor = true;
            this.chkChuaChieu.CheckedChanged += new System.EventHandler(this.chkChuaChieu_CheckedChanged);
            //
            // btnHoanVe
            //
            this.btnHoanVe.BackColor = System.Drawing.Color.Firebrick;
            this.btnHoanVe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoanVe.ForeColor = System.Drawing.Color.White;
            this.btnHoanVe.Location = new System.Drawing.Point(700, 70);
            this.btnHoanVe.Name = "btnHoanVe";
            this.btnHoanVe.Size = new System.Drawing.Size(170, 38);
            this.btnHoanVe.TabIndex = 6;
            this.btnHoanVe.Text = "Hoàn vé đã chọn";
            this.btnHoanVe.UseVisualStyleBackColor = false;
            this.btnHoanVe.Click += new System.EventHandler(this.btnHoanVe_Click);
            //
            // dgvVe
            //
            this.dgvVe.AllowUserToAddRows = false;
            this.dgvVe.AllowUserToDeleteRows = false;
            this.dgvVe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVe.BackgroundColor = System.Drawing.Color.White;
            this.dgvVe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVe.Location = new System.Drawing.Point(0, 115);
            this.dgvVe.Name = "dgvVe";
            this.dgvVe.ReadOnly = true;
            this.dgvVe.RowHeadersWidth = 51;
            this.dgvVe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVe.Size = new System.Drawing.Size(1000, 485);
            this.dgvVe.TabIndex = 1;
            //
            // hoan_ve
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvVe);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "hoan_ve";
            this.Size = new System.Drawing.Size(1000, 600);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.CheckBox chkChuaChieu;
        private System.Windows.Forms.Button btnHoanVe;
        private System.Windows.Forms.DataGridView dgvVe;
    }
}
