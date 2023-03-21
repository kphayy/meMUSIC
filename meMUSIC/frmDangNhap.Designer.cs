
namespace meMUSIC
{
    partial class frmDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword_Hide = new System.Windows.Forms.TextBox();
            this.lblShowPassword = new System.Windows.Forms.Label();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.lblMoiDangKy = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtPassword_Show = new System.Windows.Forms.TextBox();
            this.pboBanner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pboBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(35, 146);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(81, 23);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(122, 143);
            this.txtUsername.MaxLength = 20;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(182, 31);
            this.txtUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(35, 191);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(78, 23);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword_Hide
            // 
            this.txtPassword_Hide.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword_Hide.Location = new System.Drawing.Point(122, 188);
            this.txtPassword_Hide.MaxLength = 20;
            this.txtPassword_Hide.Name = "txtPassword_Hide";
            this.txtPassword_Hide.PasswordChar = '•';
            this.txtPassword_Hide.Size = new System.Drawing.Size(182, 31);
            this.txtPassword_Hide.TabIndex = 1;
            this.txtPassword_Hide.TextChanged += new System.EventHandler(this.txtPassword_Hide_TextChanged);
            // 
            // lblShowPassword
            // 
            this.lblShowPassword.AutoSize = true;
            this.lblShowPassword.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowPassword.Location = new System.Drawing.Point(308, 191);
            this.lblShowPassword.Name = "lblShowPassword";
            this.lblShowPassword.Size = new System.Drawing.Size(30, 23);
            this.lblShowPassword.TabIndex = 2;
            this.lblShowPassword.Text = "👁";
            this.lblShowPassword.Click += new System.EventHandler(this.lblShowPassword_Click);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BackColor = System.Drawing.Color.Violet;
            this.btnDangNhap.FlatAppearance.BorderSize = 0;
            this.btnDangNhap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangNhap.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.ForeColor = System.Drawing.Color.Black;
            this.btnDangNhap.Location = new System.Drawing.Point(132, 250);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(103, 38);
            this.btnDangNhap.TabIndex = 3;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.Violet;
            this.btnDangKy.FlatAppearance.BorderSize = 0;
            this.btnDangKy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKy.ForeColor = System.Drawing.Color.Black;
            this.btnDangKy.Location = new System.Drawing.Point(132, 335);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(103, 38);
            this.btnDangKy.TabIndex = 4;
            this.btnDangKy.Text = "Đăng ký";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // lblMoiDangKy
            // 
            this.lblMoiDangKy.AutoSize = true;
            this.lblMoiDangKy.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoiDangKy.Location = new System.Drawing.Point(63, 298);
            this.lblMoiDangKy.Name = "lblMoiDangKy";
            this.lblMoiDangKy.Size = new System.Drawing.Size(236, 23);
            this.lblMoiDangKy.TabIndex = 1;
            this.lblMoiDangKy.Text = "Chưa có tài khoản? Đăng ký ngay!";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("FVF Fernando 08", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(240, 88);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(84, 19);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "© version 1.0";
            // 
            // txtPassword_Show
            // 
            this.txtPassword_Show.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword_Show.Location = new System.Drawing.Point(122, 189);
            this.txtPassword_Show.MaxLength = 20;
            this.txtPassword_Show.Name = "txtPassword_Show";
            this.txtPassword_Show.Size = new System.Drawing.Size(182, 31);
            this.txtPassword_Show.TabIndex = 1;
            this.txtPassword_Show.Visible = false;
            this.txtPassword_Show.TextChanged += new System.EventHandler(this.txtPassword_Show_TextChanged);
            // 
            // pboBanner
            // 
            this.pboBanner.Image = global::meMUSIC.Properties.Resources.banner;
            this.pboBanner.Location = new System.Drawing.Point(12, 12);
            this.pboBanner.Name = "pboBanner";
            this.pboBanner.Size = new System.Drawing.Size(323, 76);
            this.pboBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboBanner.TabIndex = 0;
            this.pboBanner.TabStop = false;
            // 
            // frmDangNhap
            // 
            this.AcceptButton = this.btnDangNhap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(347, 403);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.txtPassword_Show);
            this.Controls.Add(this.txtPassword_Hide);
            this.Controls.Add(this.lblShowPassword);
            this.Controls.Add(this.lblMoiDangKy);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.pboBanner);
            this.Font = new System.Drawing.Font("FVF Fernando 08", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Magenta;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "meMUSIC ver 1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDangNhap_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pboBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pboBanner;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword_Hide;
        private System.Windows.Forms.Label lblShowPassword;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Label lblMoiDangKy;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtPassword_Show;
    }
}

