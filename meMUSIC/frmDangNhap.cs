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
    public partial class frmDangNhap : Form
    {
        string username, password, hoUser, tenUser;
        DateTime ngayUser;
        meMUSIC_DBContext contextmeMUSIC = new meMUSIC_DBContext();
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void txtPassword_Show_TextChanged(object sender, EventArgs e)
        {
            txtPassword_Hide.Text = txtPassword_Show.Text;
        }

        private void txtPassword_Hide_TextChanged(object sender, EventArgs e)
        {
            txtPassword_Show.Text = txtPassword_Hide.Text;
        }

        private void lblShowPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword_Show.Visible == true)
            {
                txtPassword_Show.Visible = false;
                txtPassword_Hide.Visible = true;
                txtPassword_Hide.Focus();
                txtPassword_Hide.SelectionStart = txtPassword_Hide.Text.Length;
            }
            else
            {
                txtPassword_Hide.Visible = false;
                txtPassword_Show.Visible = true;
                txtPassword_Show.Focus();
                txtPassword_Show.SelectionStart = txtPassword_Show.Text.Length;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text == "" || txtPassword_Hide.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin.");
                List<User> listUsers = contextmeMUSIC.Users.ToList();
                int flag = 0;
                foreach(var x in listUsers)
                {
                    if (x.username.ToLower() == txtUsername.Text.ToLower())
                    {
                        if (x.matkhau != txtPassword_Hide.Text)
                            throw new Exception("Mật khẩu không đúng.");
                        flag = 1;
                        username = x.username;
                        password = x.matkhau;
                        hoUser = x.HoUser;
                        tenUser = x.TenUser;
                        ngayUser = x.NgayUser;
                        break;
                    }    
                }
                if (flag == 0)
                        throw new Exception("Username không tồn tại.");

                MessageBox.Show($"Đăng nhập thành công!\nWelcome back, {this.hoUser} {this.tenUser}.", 
                    "meMUSIC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmTrangChu frm = new frmTrangChu();
                frm.username = username;
                frm.password = password;
                frm.hoUser = hoUser;
                frm.tenUser = tenUser;
                frm.ngayUser = ngayUser;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "meMUSIC",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có chắc muốn thoát không?", "meMUSIC", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.No)
                e.Cancel = true;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            frmDangKy frm = new frmDangKy();
            frm.ShowDialog();
        }
    }
}
