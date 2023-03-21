using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace meMUSIC
{
    public partial class frmXemTaiKhoan : Form
    {
        public string username, password, hoUser, tenUser;
        public DateTime ngayUser;
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblShowPassword_Click(object sender, EventArgs e)
        {
            lblPassword_Hide.Visible = !lblPassword_Hide.Visible;
        }

        private void frmXemTaiKhoan_Load(object sender, EventArgs e)
        {
            lblPassword_Hide.Text = "";
            lblHoTen.Text = this.hoUser + " " + this.tenUser;
            lblUsername.Text = this.username;
            lblPassword_Show.Text = this.password;
            for (int i = 0; i < this.password.Length - 1; i++)
                lblPassword_Hide.Text += "•";
            lblNgayUser.Text = this.ngayUser.ToString("dd -- MM -- yyyy");
        }

        public frmXemTaiKhoan()
        {
            InitializeComponent();
        }
    }
}
