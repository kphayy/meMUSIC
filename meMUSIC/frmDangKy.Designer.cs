
namespace meMUSIC
{
    partial class frmDangKy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangKy));
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblTieuDeDangKy = new System.Windows.Forms.Label();
            this.txtPassword_Show = new System.Windows.Forms.TextBox();
            this.txtPassword_Hide = new System.Windows.Forms.TextBox();
            this.lblShowPassword = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblHo = new System.Windows.Forms.Label();
            this.txtHo = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblNhapHoTen = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.grpRule = new System.Windows.Forms.GroupBox();
            this.lblRule = new System.Windows.Forms.Label();
            this.pboBanner = new System.Windows.Forms.PictureBox();
            this.grpRule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("FVF Fernando 08", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(240, 88);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(84, 19);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "© version 1.0";
            // 
            // lblTieuDeDangKy
            // 
            this.lblTieuDeDangKy.AutoSize = true;
            this.lblTieuDeDangKy.Font = new System.Drawing.Font("Montserrat SemiBold", 27F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDeDangKy.Location = new System.Drawing.Point(394, 31);
            this.lblTieuDeDangKy.Name = "lblTieuDeDangKy";
            this.lblTieuDeDangKy.Size = new System.Drawing.Size(362, 50);
            this.lblTieuDeDangKy.TabIndex = 3;
            this.lblTieuDeDangKy.Text = "Đăng ký tài khoản";
            // 
            // txtPassword_Show
            // 
            this.txtPassword_Show.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword_Show.Location = new System.Drawing.Point(481, 168);
            this.txtPassword_Show.MaxLength = 20;
            this.txtPassword_Show.Name = "txtPassword_Show";
            this.txtPassword_Show.Size = new System.Drawing.Size(182, 31);
            this.txtPassword_Show.TabIndex = 1;
            this.txtPassword_Show.Visible = false;
            this.txtPassword_Show.TextChanged += new System.EventHandler(this.txtPassword_Show_TextChanged);
            // 
            // txtPassword_Hide
            // 
            this.txtPassword_Hide.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword_Hide.Location = new System.Drawing.Point(481, 168);
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
            this.lblShowPassword.Location = new System.Drawing.Point(667, 171);
            this.lblShowPassword.Name = "lblShowPassword";
            this.lblShowPassword.Size = new System.Drawing.Size(30, 23);
            this.lblShowPassword.TabIndex = 9;
            this.lblShowPassword.Text = "👁";
            this.lblShowPassword.Click += new System.EventHandler(this.lblShowPassword_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(394, 171);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(78, 23);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(481, 123);
            this.txtUsername.MaxLength = 20;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(182, 31);
            this.txtUsername.TabIndex = 0;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(394, 126);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(81, 23);
            this.lblUsername.TabIndex = 8;
            this.lblUsername.Text = "Username";
            // 
            // lblHo
            // 
            this.lblHo.AutoSize = true;
            this.lblHo.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHo.Location = new System.Drawing.Point(450, 300);
            this.lblHo.Name = "lblHo";
            this.lblHo.Size = new System.Drawing.Size(28, 23);
            this.lblHo.TabIndex = 8;
            this.lblHo.Text = "Họ";
            // 
            // txtHo
            // 
            this.txtHo.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHo.Location = new System.Drawing.Point(481, 297);
            this.txtHo.MaxLength = 18;
            this.txtHo.Name = "txtHo";
            this.txtHo.Size = new System.Drawing.Size(182, 31);
            this.txtHo.TabIndex = 2;
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.Location = new System.Drawing.Point(444, 345);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(34, 23);
            this.lblTen.TabIndex = 7;
            this.lblTen.Text = "Tên";
            // 
            // txtTen
            // 
            this.txtTen.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTen.Location = new System.Drawing.Point(481, 342);
            this.txtTen.MaxLength = 18;
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(182, 31);
            this.txtTen.TabIndex = 3;
            this.txtTen.TextChanged += new System.EventHandler(this.txtPassword_Hide_TextChanged);
            // 
            // lblNhapHoTen
            // 
            this.lblNhapHoTen.AutoSize = true;
            this.lblNhapHoTen.Font = new System.Drawing.Font("Montserrat SemiBold", 27F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhapHoTen.Location = new System.Drawing.Point(367, 219);
            this.lblNhapHoTen.Name = "lblNhapHoTen";
            this.lblNhapHoTen.Size = new System.Drawing.Size(457, 50);
            this.lblNhapHoTen.TabIndex = 3;
            this.lblNhapHoTen.Text = "Bạn muốn được gọi là...";
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.Violet;
            this.btnDangKy.FlatAppearance.BorderSize = 0;
            this.btnDangKy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKy.ForeColor = System.Drawing.Color.Black;
            this.btnDangKy.Location = new System.Drawing.Point(520, 391);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(103, 38);
            this.btnDangKy.TabIndex = 4;
            this.btnDangKy.Text = "Đăng ký";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // grpRule
            // 
            this.grpRule.Controls.Add(this.lblRule);
            this.grpRule.Font = new System.Drawing.Font("Montserrat SemiBold", 11.24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRule.ForeColor = System.Drawing.Color.Magenta;
            this.grpRule.Location = new System.Drawing.Point(28, 126);
            this.grpRule.Name = "grpRule";
            this.grpRule.Size = new System.Drawing.Size(307, 292);
            this.grpRule.TabIndex = 10;
            this.grpRule.TabStop = false;
            this.grpRule.Text = "Quy tắc đặt Username và Password";
            // 
            // lblRule
            // 
            this.lblRule.AutoSize = true;
            this.lblRule.Font = new System.Drawing.Font("FVF Fernando 08", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRule.ForeColor = System.Drawing.Color.White;
            this.lblRule.Location = new System.Drawing.Point(13, 33);
            this.lblRule.Name = "lblRule";
            this.lblRule.Size = new System.Drawing.Size(86, 23);
            this.lblRule.TabIndex = 7;
            this.lblRule.Text = "Password";
            // 
            // pboBanner
            // 
            this.pboBanner.Image = global::meMUSIC.Properties.Resources.banner;
            this.pboBanner.Location = new System.Drawing.Point(12, 12);
            this.pboBanner.Name = "pboBanner";
            this.pboBanner.Size = new System.Drawing.Size(323, 76);
            this.pboBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboBanner.TabIndex = 2;
            this.pboBanner.TabStop = false;
            // 
            // frmDangKy
            // 
            this.AcceptButton = this.btnDangKy;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(829, 454);
            this.Controls.Add(this.grpRule);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.txtPassword_Show);
            this.Controls.Add(this.txtPassword_Hide);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.lblShowPassword);
            this.Controls.Add(this.txtHo);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblHo);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblNhapHoTen);
            this.Controls.Add(this.lblTieuDeDangKy);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.pboBanner);
            this.Font = new System.Drawing.Font("FVF Fernando 08", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Magenta;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmDangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "meMUSIC ver 1.0";
            this.Load += new System.EventHandler(this.frmDangKy_Load);
            this.grpRule.ResumeLayout(false);
            this.grpRule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pboBanner;
        private System.Windows.Forms.Label lblTieuDeDangKy;
        private System.Windows.Forms.TextBox txtPassword_Show;
        private System.Windows.Forms.TextBox txtPassword_Hide;
        private System.Windows.Forms.Label lblShowPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblHo;
        private System.Windows.Forms.TextBox txtHo;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblNhapHoTen;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.GroupBox grpRule;
        private System.Windows.Forms.Label lblRule;
    }
}