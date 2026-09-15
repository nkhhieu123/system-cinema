namespace cinema_system.nhân_viên
{
    partial class QuanLyPhong
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
            this.lblTenPhong = new System.Windows.Forms.Label();
            this.txtTenPhong = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSoDo = new System.Windows.Forms.Button();
            this.lblSoHang = new System.Windows.Forms.Label();
            this.numHang = new System.Windows.Forms.NumericUpDown();
            this.lblSoCot = new System.Windows.Forms.Label();
            this.numCot = new System.Windows.Forms.NumericUpDown();
            this.dgvRooms = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).BeginInit();
            this.SuspendLayout();
            //
            // panelTop
            //
            this.panelTop.Controls.Add(this.lblTenPhong);
            this.panelTop.Controls.Add(this.txtTenPhong);
            this.panelTop.Controls.Add(this.btnThem);
            this.panelTop.Controls.Add(this.btnSua);
            this.panelTop.Controls.Add(this.btnXoa);
            this.panelTop.Controls.Add(this.btnSoDo);
            this.panelTop.Controls.Add(this.lblSoHang);
            this.panelTop.Controls.Add(this.numHang);
            this.panelTop.Controls.Add(this.lblSoCot);
            this.panelTop.Controls.Add(this.numCot);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(560, 145);
            this.panelTop.TabIndex = 0;
            //
            // lblTenPhong
            //
            this.lblTenPhong.AutoSize = true;
            this.lblTenPhong.Location = new System.Drawing.Point(15, 18);
            this.lblTenPhong.Name = "lblTenPhong";
            this.lblTenPhong.Size = new System.Drawing.Size(78, 19);
            this.lblTenPhong.TabIndex = 0;
            this.lblTenPhong.Text = "Tên phòng";
            //
            // txtTenPhong
            //
            this.txtTenPhong.Location = new System.Drawing.Point(110, 15);
            this.txtTenPhong.MaxLength = 50;
            this.txtTenPhong.Name = "txtTenPhong";
            this.txtTenPhong.Size = new System.Drawing.Size(280, 26);
            this.txtTenPhong.TabIndex = 1;
            //
            // btnThem
            //
            this.btnThem.Location = new System.Drawing.Point(15, 95);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(110, 38);
            this.btnThem.TabIndex = 2;
            this.btnThem.Text = "Thêm phòng";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            //
            // btnSua
            //
            this.btnSua.Location = new System.Drawing.Point(145, 95);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(110, 38);
            this.btnSua.TabIndex = 3;
            this.btnSua.Text = "Đổi tên";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            //
            // btnXoa
            //
            this.btnXoa.Location = new System.Drawing.Point(275, 95);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(110, 38);
            this.btnXoa.TabIndex = 4;
            this.btnXoa.Text = "Xóa phòng";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            //
            // btnSoDo
            //
            this.btnSoDo.Location = new System.Drawing.Point(405, 95);
            this.btnSoDo.Name = "btnSoDo";
            this.btnSoDo.Size = new System.Drawing.Size(140, 38);
            this.btnSoDo.TabIndex = 5;
            this.btnSoDo.Text = "Sơ đồ ghế...";
            this.btnSoDo.UseVisualStyleBackColor = true;
            this.btnSoDo.Click += new System.EventHandler(this.btnSoDo_Click);
            //
            // lblSoHang
            //
            this.lblSoHang.AutoSize = true;
            this.lblSoHang.Location = new System.Drawing.Point(15, 58);
            this.lblSoHang.Name = "lblSoHang";
            this.lblSoHang.Size = new System.Drawing.Size(63, 19);
            this.lblSoHang.TabIndex = 6;
            this.lblSoHang.Text = "Số hàng";
            //
            // numHang
            //
            this.numHang.Location = new System.Drawing.Point(110, 55);
            this.numHang.Maximum = new decimal(new int[] { 26, 0, 0, 0 });
            this.numHang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numHang.Name = "numHang";
            this.numHang.Size = new System.Drawing.Size(70, 26);
            this.numHang.TabIndex = 7;
            this.numHang.Value = new decimal(new int[] { 10, 0, 0, 0 });
            //
            // lblSoCot
            //
            this.lblSoCot.AutoSize = true;
            this.lblSoCot.Location = new System.Drawing.Point(200, 58);
            this.lblSoCot.Name = "lblSoCot";
            this.lblSoCot.Size = new System.Drawing.Size(105, 19);
            this.lblSoCot.TabIndex = 8;
            this.lblSoCot.Text = "Ghế mỗi hàng";
            //
            // numCot
            //
            this.numCot.Location = new System.Drawing.Point(315, 55);
            this.numCot.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            this.numCot.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numCot.Name = "numCot";
            this.numCot.Size = new System.Drawing.Size(70, 26);
            this.numCot.TabIndex = 9;
            this.numCot.Value = new decimal(new int[] { 12, 0, 0, 0 });
            //
            // dgvRooms
            //
            this.dgvRooms.AllowUserToAddRows = false;
            this.dgvRooms.AllowUserToDeleteRows = false;
            this.dgvRooms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRooms.Location = new System.Drawing.Point(0, 145);
            this.dgvRooms.MultiSelect = false;
            this.dgvRooms.Name = "dgvRooms";
            this.dgvRooms.ReadOnly = true;
            this.dgvRooms.RowHeadersWidth = 51;
            this.dgvRooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRooms.Size = new System.Drawing.Size(560, 315);
            this.dgvRooms.TabIndex = 1;
            this.dgvRooms.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRooms_CellClick);
            //
            // QuanLyPhong
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(560, 460);
            this.Controls.Add(this.dgvRooms);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeBox = false;
            this.Name = "QuanLyPhong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý phòng chiếu";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTenPhong;
        private System.Windows.Forms.TextBox txtTenPhong;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSoDo;
        private System.Windows.Forms.Label lblSoHang;
        private System.Windows.Forms.NumericUpDown numHang;
        private System.Windows.Forms.Label lblSoCot;
        private System.Windows.Forms.NumericUpDown numCot;
        private System.Windows.Forms.DataGridView dgvRooms;
    }
}
