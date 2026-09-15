using System.Windows.Forms;

namespace cinema_system.đăng_nhập
{
    partial class UC_Đăng_ký
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblBirth;
        private System.Windows.Forms.ComboBox cbDay;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.Label lblCaptcha;
        private System.Windows.Forms.TextBox txtCaptcha;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.CheckBox chkAgree2;
        private System.Windows.Forms.CheckBox chkAgree3;
        private System.Windows.Forms.CheckBox chkAgree4;
        private System.Windows.Forms.Button btnRegister;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Đăng_ký));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblBirth = new System.Windows.Forms.Label();
            this.cbDay = new System.Windows.Forms.ComboBox();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.lblCaptcha = new System.Windows.Forms.Label();
            this.txtCaptcha = new System.Windows.Forms.TextBox();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.chkAgree2 = new System.Windows.Forms.CheckBox();
            this.chkAgree3 = new System.Windows.Forms.CheckBox();
            this.chkAgree4 = new System.Windows.Forms.CheckBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.reg_showPass = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(20, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 23);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên *";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(150, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 22);
            this.txtName.TabIndex = 1;
            // 
            // lblPhone
            // 
            this.lblPhone.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(20, 60);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(100, 23);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "Số điện thoại *";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(150, 60);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(250, 22);
            this.txtPhone.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(20, 100);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(100, 23);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email *";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(150, 100);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 22);
            this.txtEmail.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(20, 140);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(100, 23);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Mật khẩu *";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(150, 140);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(250, 22);
            this.txtPassword.TabIndex = 7;
            // 
            // lblBirth
            // 
            this.lblBirth.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBirth.Location = new System.Drawing.Point(20, 180);
            this.lblBirth.Name = "lblBirth";
            this.lblBirth.Size = new System.Drawing.Size(100, 23);
            this.lblBirth.TabIndex = 8;
            this.lblBirth.Text = "Ngày sinh *";
            //
            // cbDay
            // 
            this.cbDay.Location = new System.Drawing.Point(150, 180);
            this.cbDay.Name = "cbDay";
            this.cbDay.Size = new System.Drawing.Size(50, 24);
            this.cbDay.TabIndex = 9;
            // 
            // cbMonth
            // 
            this.cbMonth.Location = new System.Drawing.Point(210, 180);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(70, 24);
            this.cbMonth.TabIndex = 10;
            // 
            // cbYear
            // 
            this.cbYear.Location = new System.Drawing.Point(290, 180);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(80, 24);
            this.cbYear.TabIndex = 11;
            // 
            // 
            // rbMale
            // 
            this.rbMale.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMale.Location = new System.Drawing.Point(150, 220);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(80, 24);
            this.rbMale.TabIndex = 12;
            this.rbMale.Text = "Nam";
            // 
            // rbFemale
            // 
            this.rbFemale.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFemale.Location = new System.Drawing.Point(251, 220);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(64, 24);
            this.rbFemale.TabIndex = 13;
            this.rbFemale.Text = "Nữ";
            // 
            // lblCaptcha
            // 
            this.lblCaptcha.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaptcha.Location = new System.Drawing.Point(20, 300);
            this.lblCaptcha.Name = "lblCaptcha";
            this.lblCaptcha.Size = new System.Drawing.Size(100, 23);
            this.lblCaptcha.TabIndex = 14;
            this.lblCaptcha.Text = "Nhập ký tự bên dưới *";
            // 
            // txtCaptcha
            // 
            this.txtCaptcha.Location = new System.Drawing.Point(150, 300);
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.Size = new System.Drawing.Size(150, 22);
            this.txtCaptcha.TabIndex = 15;
            // 
            // picCaptcha
            // 
            this.picCaptcha.BackColor = System.Drawing.Color.White;
            this.picCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCaptcha.Location = new System.Drawing.Point(320, 290);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(164, 47);
            this.picCaptcha.TabIndex = 16;
            this.picCaptcha.TabStop = false;
            // 
            // chkAgree2
            // 
            this.chkAgree2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAgree2.Location = new System.Drawing.Point(20, 343);
            this.chkAgree2.Name = "chkAgree2";
            this.chkAgree2.Size = new System.Drawing.Size(627, 71);
            this.chkAgree2.TabIndex = 18;
            this.chkAgree2.Text = resources.GetString("chkAgree2.Text");
            // 
            // chkAgree3
            // 
            this.chkAgree3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAgree3.Location = new System.Drawing.Point(20, 420);
            this.chkAgree3.Name = "chkAgree3";
            this.chkAgree3.Size = new System.Drawing.Size(623, 72);
            this.chkAgree3.TabIndex = 19;
            this.chkAgree3.Text = resources.GetString("chkAgree3.Text");
            // 
            // chkAgree4
            // 
            this.chkAgree4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAgree4.Location = new System.Drawing.Point(20, 498);
            this.chkAgree4.Name = "chkAgree4";
            this.chkAgree4.Size = new System.Drawing.Size(280, 24);
            this.chkAgree4.TabIndex = 20;
            this.chkAgree4.Text = "Tôi đồng ý với Điều Khoản Sử Dụng ";
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.Red;
            this.btnRegister.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(199, 549);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(250, 40);
            this.btnRegister.TabIndex = 21;
            this.btnRegister.Text = "ĐĂNG KÝ";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // reg_showPass
            // 
            this.reg_showPass.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reg_showPass.Location = new System.Drawing.Point(419, 140);
            this.reg_showPass.Name = "reg_showPass";
            this.reg_showPass.Size = new System.Drawing.Size(143, 24);
            this.reg_showPass.TabIndex = 22;
            this.reg_showPass.Text = "Hiện mật khẩu";
            this.reg_showPass.CheckedChanged += new System.EventHandler(this.reg_showPass_CheckedChanged);
            // 
            // UC_Đăng_ký
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.Controls.Add(this.reg_showPass);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblBirth);
            this.Controls.Add(this.cbDay);
            this.Controls.Add(this.cbMonth);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.rbMale);
            this.Controls.Add(this.rbFemale);
            this.Controls.Add(this.lblCaptcha);
            this.Controls.Add(this.txtCaptcha);
            this.Controls.Add(this.picCaptcha);
            this.Controls.Add(this.chkAgree2);
            this.Controls.Add(this.chkAgree3);
            this.Controls.Add(this.chkAgree4);
            this.Controls.Add(this.btnRegister);
            this.Name = "UC_Đăng_ký";
            this.Size = new System.Drawing.Size(706, 717);
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadioButton reg_showPass;
    }
}