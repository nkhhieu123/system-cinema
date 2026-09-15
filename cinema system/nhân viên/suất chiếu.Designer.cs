namespace cinema_system.nhân_viên
{
    partial class ThemMovie
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvShowtimes = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelShowtime = new System.Windows.Forms.Button();
            this.btnChangeShowtime = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblMovie = new System.Windows.Forms.Label();
            this.cbMovie = new System.Windows.Forms.ComboBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.btnAddShowtime = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowtimes)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvShowtimes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 351);
            this.panel1.TabIndex = 8;
            // 
            // dgvShowtimes
            // 
            this.dgvShowtimes.AllowUserToAddRows = false;
            this.dgvShowtimes.AllowUserToDeleteRows = false;
            this.dgvShowtimes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShowtimes.ColumnHeadersHeight = 29;
            this.dgvShowtimes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShowtimes.Location = new System.Drawing.Point(0, 0);
            this.dgvShowtimes.Name = "dgvShowtimes";
            this.dgvShowtimes.ReadOnly = true;
            this.dgvShowtimes.RowHeadersWidth = 51;
            this.dgvShowtimes.Size = new System.Drawing.Size(695, 351);
            this.dgvShowtimes.TabIndex = 8;
            this.dgvShowtimes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShowtimes.MultiSelect = false;
            this.dgvShowtimes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowtimes_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelShowtime);
            this.panel2.Controls.Add(this.btnChangeShowtime);
            this.panel2.Controls.Add(this.lblDate);
            this.panel2.Controls.Add(this.dtpDate);
            this.panel2.Controls.Add(this.lblMovie);
            this.panel2.Controls.Add(this.cbMovie);
            this.panel2.Controls.Add(this.lblTime);
            this.panel2.Controls.Add(this.dtpTime);
            this.panel2.Controls.Add(this.btnAddShowtime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(695, 138);
            this.panel2.TabIndex = 9;
            // 
            // btnDelShowtime
            // 
            this.btnDelShowtime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelShowtime.Location = new System.Drawing.Point(547, 8);
            this.btnDelShowtime.Name = "btnDelShowtime";
            this.btnDelShowtime.Size = new System.Drawing.Size(130, 55);
            this.btnDelShowtime.TabIndex = 15;
            this.btnDelShowtime.Text = "Xóa suất chiếu";
            this.btnDelShowtime.Click += new System.EventHandler(this.btnDelShowtime_Click);
            // 
            // btnChangeShowtime
            // 
            this.btnChangeShowtime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeShowtime.Location = new System.Drawing.Point(396, 69);
            this.btnChangeShowtime.Name = "btnChangeShowtime";
            this.btnChangeShowtime.Size = new System.Drawing.Size(130, 55);
            this.btnChangeShowtime.TabIndex = 14;
            this.btnChangeShowtime.Text = "Sửa xuất chiếu";
            this.btnChangeShowtime.Click += new System.EventHandler(this.btnChangeShowtime_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(18, 17);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(90, 19);
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "Ngày chiếu:";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(116, 14);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(231, 22);
            this.dtpDate.TabIndex = 8;
            // 
            // lblMovie
            // 
            this.lblMovie.AutoSize = true;
            this.lblMovie.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMovie.Location = new System.Drawing.Point(20, 52);
            this.lblMovie.Name = "lblMovie";
            this.lblMovie.Size = new System.Drawing.Size(88, 19);
            this.lblMovie.TabIndex = 9;
            this.lblMovie.Text = "Chọn phim:";
            // 
            // cbMovie
            // 
            this.cbMovie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMovie.Location = new System.Drawing.Point(116, 47);
            this.cbMovie.Name = "cbMovie";
            this.cbMovie.Size = new System.Drawing.Size(231, 24);
            this.cbMovie.TabIndex = 10;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(20, 87);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(81, 19);
            this.lblTime.TabIndex = 11;
            this.lblTime.Text = "Giờ chiếu:";
            // 
            // dtpTime
            // 
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(116, 84);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(200, 22);
            this.dtpTime.TabIndex = 12;
            // 
            // btnAddShowtime
            // 
            this.btnAddShowtime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddShowtime.Location = new System.Drawing.Point(547, 69);
            this.btnAddShowtime.Name = "btnAddShowtime";
            this.btnAddShowtime.Size = new System.Drawing.Size(130, 55);
            this.btnAddShowtime.TabIndex = 13;
            this.btnAddShowtime.Text = "Thêm suất chiếu";
            this.btnAddShowtime.Click += new System.EventHandler(this.btnAddShowtime_Click);
            // 
            // ThemMovie
            // 
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ThemMovie";
            this.Size = new System.Drawing.Size(695, 495);
            this.Load += new System.EventHandler(this.FormAddShowtime_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowtimes)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvShowtimes;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnChangeShowtime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblMovie;
        private System.Windows.Forms.ComboBox cbMovie;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Button btnAddShowtime;
        private System.Windows.Forms.Button btnDelShowtime;
    }
}
