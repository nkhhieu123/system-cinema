namespace cinema_system.nhân_viên
{
    partial class RoomMovieControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnRooms = new System.Windows.Forms.Button();
            this.btnFix = new System.Windows.Forms.Button();
            this.lblRoom = new System.Windows.Forms.Label();
            this.cbRoom = new System.Windows.Forms.ComboBox();
            this.lblMovie = new System.Windows.Forms.Label();
            this.cbMovie = new System.Windows.Forms.ComboBox();
            this.btnAssign = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvRoomMovies = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomMovies)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRooms);
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.btnFix);
            this.panel1.Controls.Add(this.lblRoom);
            this.panel1.Controls.Add(this.cbRoom);
            this.panel1.Controls.Add(this.lblMovie);
            this.panel1.Controls.Add(this.cbMovie);
            this.panel1.Controls.Add(this.btnAssign);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 154);
            this.panel1.TabIndex = 6;
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(419, 89);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(141, 62);
            this.btnDel.TabIndex = 11;
            this.btnDel.Text = "Xóa phim cho phòng";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnRooms
            // 
            this.btnRooms.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRooms.Location = new System.Drawing.Point(610, 89);
            this.btnRooms.Name = "btnRooms";
            this.btnRooms.Size = new System.Drawing.Size(141, 62);
            this.btnRooms.TabIndex = 12;
            this.btnRooms.Text = "Quản lý phòng";
            this.btnRooms.Click += new System.EventHandler(this.btnRooms_Click);
            // 
            // btnFix
            // 
            this.btnFix.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFix.Location = new System.Drawing.Point(220, 89);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(141, 62);
            this.btnFix.TabIndex = 10;
            this.btnFix.Text = "Đổi phim cho phòng";
            this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
            // 
            // lblRoom
            // 
            this.lblRoom.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoom.Location = new System.Drawing.Point(25, 9);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(191, 40);
            this.lblRoom.TabIndex = 5;
            this.lblRoom.Text = "Chọn phòng:";
            // 
            // cbRoom
            // 
            this.cbRoom.Location = new System.Drawing.Point(260, 8);
            this.cbRoom.Name = "cbRoom";
            this.cbRoom.Size = new System.Drawing.Size(200, 24);
            this.cbRoom.TabIndex = 6;
            // 
            // lblMovie
            // 
            this.lblMovie.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMovie.Location = new System.Drawing.Point(25, 49);
            this.lblMovie.Name = "lblMovie";
            this.lblMovie.Size = new System.Drawing.Size(175, 37);
            this.lblMovie.TabIndex = 7;
            this.lblMovie.Text = "Chọn phim:";
            // 
            // cbMovie
            // 
            this.cbMovie.Location = new System.Drawing.Point(260, 48);
            this.cbMovie.Name = "cbMovie";
            this.cbMovie.Size = new System.Drawing.Size(200, 24);
            this.cbMovie.TabIndex = 8;
            // 
            // btnAssign
            // 
            this.btnAssign.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAssign.Location = new System.Drawing.Point(30, 89);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(137, 62);
            this.btnAssign.TabIndex = 9;
            this.btnAssign.Text = "Gán phim cho phòng";
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvRoomMovies);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 157);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(772, 426);
            this.panel2.TabIndex = 7;
            // 
            // dgvRoomMovies
            // 
            this.dgvRoomMovies.ColumnHeadersHeight = 29;
            this.dgvRoomMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoomMovies.Location = new System.Drawing.Point(0, 0);
            this.dgvRoomMovies.Name = "dgvRoomMovies";
            this.dgvRoomMovies.RowHeadersWidth = 51;
            this.dgvRoomMovies.Size = new System.Drawing.Size(772, 426);
            this.dgvRoomMovies.TabIndex = 6;
            // 
            // RoomMovieControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "RoomMovieControl";
            this.Size = new System.Drawing.Size(772, 583);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomMovies)).EndInit();
            this.ResumeLayout(false);

           }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFix;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.ComboBox cbRoom;
        private System.Windows.Forms.Label lblMovie;
        private System.Windows.Forms.ComboBox cbMovie;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvRoomMovies;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnRooms;
    }
}