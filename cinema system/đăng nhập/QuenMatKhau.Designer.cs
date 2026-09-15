namespace cinema_system.đăng_nhập
{
    partial class QuenMatKhau
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
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblMatKhauMoi = new System.Windows.Forms.Label();
            this.txtMatKhauMoi = new System.Windows.Forms.TextBox();
            this.lblNhapLai = new System.Windows.Forms.Label();
            this.txtNhapLai = new System.Windows.Forms.TextBox();
            this.btnDoiMatKhau = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblHuongDan
            //
            this.lblHuongDan.Location = new System.Drawing.Point(20, 15);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(430, 45);
            this.lblHuongDan.TabIndex = 0;
            this.lblHuongDan.Text = "Nhập đúng tên đăng nhập, email và số điện thoại đã đăng ký để đặt mật khẩu mới.";
            //
            // lblTen
            //
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(20, 73);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(108, 19);
            this.lblTen.TabIndex = 1;
            this.lblTen.Text = "Tên đăng nhập";
            //
            // txtTen
            //
            this.txtTen.Location = new System.Drawing.Point(190, 70);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(250, 26);
            this.txtTen.TabIndex = 2;
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 113);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(46, 19);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email";
            //
            // txtEmail
            //
            this.txtEmail.Location = new System.Drawing.Point(190, 110);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 26);
            this.txtEmail.TabIndex = 4;
            //
            // lblSDT
            //
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(20, 153);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(101, 19);
            this.lblSDT.TabIndex = 5;
            this.lblSDT.Text = "Số điện thoại";
            //
            // txtSDT
            //
            this.txtSDT.Location = new System.Drawing.Point(190, 150);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(250, 26);
            this.txtSDT.TabIndex = 6;
            //
            // lblMatKhauMoi
            //
            this.lblMatKhauMoi.AutoSize = true;
            this.lblMatKhauMoi.Location = new System.Drawing.Point(20, 193);
            this.lblMatKhauMoi.Name = "lblMatKhauMoi";
            this.lblMatKhauMoi.Size = new System.Drawing.Size(103, 19);
            this.lblMatKhauMoi.TabIndex = 7;
            this.lblMatKhauMoi.Text = "Mật khẩu mới";
            //
            // txtMatKhauMoi
            //
            this.txtMatKhauMoi.Location = new System.Drawing.Point(190, 190);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.Size = new System.Drawing.Size(250, 26);
            this.txtMatKhauMoi.TabIndex = 8;
            this.txtMatKhauMoi.UseSystemPasswordChar = true;
            //
            // lblNhapLai
            //
            this.lblNhapLai.AutoSize = true;
            this.lblNhapLai.Location = new System.Drawing.Point(20, 233);
            this.lblNhapLai.Name = "lblNhapLai";
            this.lblNhapLai.Size = new System.Drawing.Size(137, 19);
            this.lblNhapLai.TabIndex = 9;
            this.lblNhapLai.Text = "Nhập lại mật khẩu";
            //
            // txtNhapLai
            //
            this.txtNhapLai.Location = new System.Drawing.Point(190, 230);
            this.txtNhapLai.Name = "txtNhapLai";
            this.txtNhapLai.Size = new System.Drawing.Size(250, 26);
            this.txtNhapLai.TabIndex = 10;
            this.txtNhapLai.UseSystemPasswordChar = true;
            //
            // btnDoiMatKhau
            //
            this.btnDoiMatKhau.BackColor = System.Drawing.Color.Red;
            this.btnDoiMatKhau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoiMatKhau.ForeColor = System.Drawing.Color.White;
            this.btnDoiMatKhau.Location = new System.Drawing.Point(190, 280);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(150, 40);
            this.btnDoiMatKhau.TabIndex = 11;
            this.btnDoiMatKhau.Text = "Đặt mật khẩu";
            this.btnDoiMatKhau.UseVisualStyleBackColor = false;
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            //
            // btnHuy
            //
            this.btnHuy.Location = new System.Drawing.Point(350, 280);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 40);
            this.btnHuy.TabIndex = 12;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            //
            // QuenMatKhau
            //
            this.AcceptButton = this.btnDoiMatKhau;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(464, 340);
            this.Controls.Add(this.lblHuongDan);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblSDT);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.lblMatKhauMoi);
            this.Controls.Add(this.txtMatKhauMoi);
            this.Controls.Add(this.lblNhapLai);
            this.Controls.Add(this.txtNhapLai);
            this.Controls.Add(this.btnDoiMatKhau);
            this.Controls.Add(this.btnHuy);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuenMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tìm lại mật khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblMatKhauMoi;
        private System.Windows.Forms.TextBox txtMatKhauMoi;
        private System.Windows.Forms.Label lblNhapLai;
        private System.Windows.Forms.TextBox txtNhapLai;
        private System.Windows.Forms.Button btnDoiMatKhau;
        private System.Windows.Forms.Button btnHuy;
    }
}
