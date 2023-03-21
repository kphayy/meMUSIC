
namespace meMUSIC
{
    partial class frmSuaTaiKhoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSuaTaiKhoan));
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtPassword_Show = new System.Windows.Forms.TextBox();
            this.lblShowPassword = new System.Windows.Forms.Label();
            this.txtPassword_Hide = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblSuaTaiKhoan = new System.Windows.Forms.Label();
            this.lblShowNewPassword = new System.Windows.Forms.Label();
            this.txtNewPassword_Show = new System.Windows.Forms.TextBox();
            this.lblNhanPassword = new System.Windows.Forms.Label();
            this.txtNewPassword_Hide = new System.Windows.Forms.TextBox();
            this.txtHo = new System.Windows.Forms.TextBox();
            this.lblHo = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblConfirm
            // 
            this.lblConfirm.Font = new System.Drawing.Font("Montserrat SemiBold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirm.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblConfirm.Location = new System.Drawing.Point(42, 58);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(274, 39);
            this.lblConfirm.TabIndex = 5;
            this.lblConfirm.Text = "Xác nhận lại mật khẩu";
            this.lblConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPassword_Show
            // 
            this.txtPassword_Show.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword_Show.Location = new System.Drawing.Point(82, 115);
            this.txtPassword_Show.MaxLength = 20;
            this.txtPassword_Show.Name = "txtPassword_Show";
            this.txtPassword_Show.Size = new System.Drawing.Size(196, 31);
            this.txtPassword_Show.TabIndex = 0;
            this.txtPassword_Show.Visible = false;
            this.txtPassword_Show.TextChanged += new System.EventHandler(this.txtPassword_Show_TextChanged);
            // 
            // lblShowPassword
            // 
            this.lblShowPassword.AutoSize = true;
            this.lblShowPassword.Font = new System.Drawing.Font("Montserrat SemiBold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowPassword.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblShowPassword.Location = new System.Drawing.Point(289, 116);
            this.lblShowPassword.Name = "lblShowPassword";
            this.lblShowPassword.Size = new System.Drawing.Size(44, 30);
            this.lblShowPassword.TabIndex = 18;
            this.lblShowPassword.Text = "👁";
            this.lblShowPassword.Click += new System.EventHandler(this.lblShowPassword_Click);
            // 
            // txtPassword_Hide
            // 
            this.txtPassword_Hide.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword_Hide.Location = new System.Drawing.Point(82, 115);
            this.txtPassword_Hide.MaxLength = 20;
            this.txtPassword_Hide.Name = "txtPassword_Hide";
            this.txtPassword_Hide.PasswordChar = '•';
            this.txtPassword_Hide.Size = new System.Drawing.Size(196, 31);
            this.txtPassword_Hide.TabIndex = 0;
            this.txtPassword_Hide.TextChanged += new System.EventHandler(this.txtPassword_Hide_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Lime;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(129, 171);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(103, 38);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("FVF Fernando 08", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(227, 252);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(142, 19);
            this.lblVersion.TabIndex = 21;
            this.lblVersion.Text = "meMUSIC © version 1.0";
            // 
            // lblSuaTaiKhoan
            // 
            this.lblSuaTaiKhoan.Font = new System.Drawing.Font("Montserrat SemiBold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuaTaiKhoan.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblSuaTaiKhoan.Location = new System.Drawing.Point(34, 19);
            this.lblSuaTaiKhoan.Name = "lblSuaTaiKhoan";
            this.lblSuaTaiKhoan.Size = new System.Drawing.Size(299, 39);
            this.lblSuaTaiKhoan.TabIndex = 5;
            this.lblSuaTaiKhoan.Text = "Sửa thông tin tài khoản";
            this.lblSuaTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSuaTaiKhoan.Visible = false;
            // 
            // lblShowNewPassword
            // 
            this.lblShowNewPassword.AutoSize = true;
            this.lblShowNewPassword.Font = new System.Drawing.Font("Montserrat SemiBold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowNewPassword.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblShowNewPassword.Location = new System.Drawing.Point(289, 79);
            this.lblShowNewPassword.Name = "lblShowNewPassword";
            this.lblShowNewPassword.Size = new System.Drawing.Size(44, 30);
            this.lblShowNewPassword.TabIndex = 23;
            this.lblShowNewPassword.Text = "👁";
            this.lblShowNewPassword.Visible = false;
            this.lblShowNewPassword.Click += new System.EventHandler(this.lblShowNewPassword_Click);
            // 
            // txtNewPassword_Show
            // 
            this.txtNewPassword_Show.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPassword_Show.Location = new System.Drawing.Point(140, 82);
            this.txtNewPassword_Show.MaxLength = 20;
            this.txtNewPassword_Show.Name = "txtNewPassword_Show";
            this.txtNewPassword_Show.Size = new System.Drawing.Size(138, 31);
            this.txtNewPassword_Show.TabIndex = 2;
            this.txtNewPassword_Show.Visible = false;
            this.txtNewPassword_Show.TextChanged += new System.EventHandler(this.txtNewPassword_Show_TextChanged);
            // 
            // lblNhanPassword
            // 
            this.lblNhanPassword.AutoSize = true;
            this.lblNhanPassword.Font = new System.Drawing.Font("Montserrat SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhanPassword.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblNhanPassword.Location = new System.Drawing.Point(25, 87);
            this.lblNhanPassword.Name = "lblNhanPassword";
            this.lblNhanPassword.Size = new System.Drawing.Size(113, 22);
            this.lblNhanPassword.TabIndex = 24;
            this.lblNhanPassword.Text = "PASSWORD:";
            this.lblNhanPassword.Visible = false;
            // 
            // txtNewPassword_Hide
            // 
            this.txtNewPassword_Hide.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPassword_Hide.Location = new System.Drawing.Point(140, 82);
            this.txtNewPassword_Hide.MaxLength = 20;
            this.txtNewPassword_Hide.Name = "txtNewPassword_Hide";
            this.txtNewPassword_Hide.PasswordChar = '•';
            this.txtNewPassword_Hide.Size = new System.Drawing.Size(138, 31);
            this.txtNewPassword_Hide.TabIndex = 2;
            this.txtNewPassword_Hide.Visible = false;
            this.txtNewPassword_Hide.TextChanged += new System.EventHandler(this.txtNewPassword_Hide_TextChanged);
            // 
            // txtHo
            // 
            this.txtHo.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHo.Location = new System.Drawing.Point(140, 122);
            this.txtHo.MaxLength = 18;
            this.txtHo.Name = "txtHo";
            this.txtHo.Size = new System.Drawing.Size(138, 31);
            this.txtHo.TabIndex = 3;
            this.txtHo.Visible = false;
            this.txtHo.TextChanged += new System.EventHandler(this.txtNewPassword_Hide_TextChanged);
            // 
            // lblHo
            // 
            this.lblHo.AutoSize = true;
            this.lblHo.Font = new System.Drawing.Font("Montserrat SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHo.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblHo.Location = new System.Drawing.Point(101, 127);
            this.lblHo.Name = "lblHo";
            this.lblHo.Size = new System.Drawing.Size(37, 22);
            this.lblHo.TabIndex = 24;
            this.lblHo.Text = "Họ:";
            this.lblHo.Visible = false;
            // 
            // txtTen
            // 
            this.txtTen.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTen.Location = new System.Drawing.Point(140, 162);
            this.txtTen.MaxLength = 18;
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(138, 31);
            this.txtTen.TabIndex = 4;
            this.txtTen.Visible = false;
            this.txtTen.TextChanged += new System.EventHandler(this.txtNewPassword_Hide_TextChanged);
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Montserrat SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblTen.Location = new System.Drawing.Point(94, 167);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(44, 22);
            this.lblTen.TabIndex = 24;
            this.lblTen.Text = "Tên:";
            this.lblTen.Visible = false;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.Lime;
            this.btnXacNhan.FlatAppearance.BorderSize = 0;
            this.btnXacNhan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.ForeColor = System.Drawing.Color.Black;
            this.btnXacNhan.Location = new System.Drawing.Point(119, 213);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(126, 38);
            this.btnXacNhan.TabIndex = 5;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Visible = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // frmSuaTaiKhoan
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(371, 271);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.lblHo);
            this.Controls.Add(this.lblNhanPassword);
            this.Controls.Add(this.lblShowNewPassword);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.txtHo);
            this.Controls.Add(this.txtNewPassword_Hide);
            this.Controls.Add(this.txtNewPassword_Show);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblShowPassword);
            this.Controls.Add(this.txtPassword_Hide);
            this.Controls.Add(this.txtPassword_Show);
            this.Controls.Add(this.lblSuaTaiKhoan);
            this.Controls.Add(this.lblConfirm);
            this.Font = new System.Drawing.Font("FVF Fernando 08", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Magenta;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmSuaTaiKhoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xác nhận mật khẩu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSuaTaiKhoan_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.TextBox txtPassword_Show;
        private System.Windows.Forms.Label lblShowPassword;
        private System.Windows.Forms.TextBox txtPassword_Hide;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblSuaTaiKhoan;
        private System.Windows.Forms.Label lblShowNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword_Show;
        private System.Windows.Forms.Label lblNhanPassword;
        private System.Windows.Forms.TextBox txtNewPassword_Hide;
        private System.Windows.Forms.TextBox txtHo;
        private System.Windows.Forms.Label lblHo;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Button btnXacNhan;
    }
}