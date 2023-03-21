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
    public partial class frmSuaTaiKhoan : Form
    {
        public string username, password, hoUser, tenUser;

        meMUSIC_DBContext contextmeMUSIC = new meMUSIC_DBContext();
        int flagThoat = 0;
        public frmSuaTaiKhoan()
        {
            InitializeComponent();
        }

        private void lblShowPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword_Show.Visible)
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

        private void txtPassword_Hide_TextChanged(object sender, EventArgs e)
        {
            txtPassword_Show.Text = txtPassword_Hide.Text;
        }

        private void txtPassword_Show_TextChanged(object sender, EventArgs e)
        {
            txtPassword_Hide.Text = txtPassword_Show.Text;
        }

        private void txtNewPassword_Hide_TextChanged(object sender, EventArgs e)
        {
            txtNewPassword_Show.Text = txtNewPassword_Hide.Text;
        }

        private void txtNewPassword_Show_TextChanged(object sender, EventArgs e)
        {
            txtNewPassword_Hide.Text = txtNewPassword_Show.Text;
        }

        private void lblShowNewPassword_Click(object sender, EventArgs e)
        {
            if (txtNewPassword_Show.Visible)
            {
                txtNewPassword_Show.Visible = false;
                txtNewPassword_Hide.Visible = true;
                txtNewPassword_Hide.Focus();
                txtNewPassword_Hide.SelectionStart = txtNewPassword_Hide.Text.Length;
            }
            else
            {
                txtNewPassword_Hide.Visible = false;
                txtNewPassword_Show.Visible = true;
                txtNewPassword_Show.Focus();
                txtNewPassword_Show.SelectionStart = txtNewPassword_Show.Text.Length;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword_Hide.Text != this.password)
                    throw new Exception("Mật khẩu không đúng.");
                this.Text = "Sửa thông tin tài khoản";
                lblConfirm.Visible = false;
                txtPassword_Hide.Visible = false;
                txtPassword_Show.Visible = false;
                lblShowPassword.Visible = false;
                btnOK.Visible = false;
                lblSuaTaiKhoan.Visible = true;
                lblNhanPassword.Visible = true;
                txtNewPassword_Hide.Visible = true;
                txtNewPassword_Hide.Focus();
                lblShowNewPassword.Visible = true;
                lblHo.Visible = true;
                txtHo.Visible = true;
                txtTen.Visible = true;
                lblTen.Visible = true;
                btnXacNhan.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "meMUSIC",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSuaTaiKhoan_FormClosing(object sender, FormClosingEventArgs e)
        {
            int flag = 0;
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name == "frmTrangChu")
                {
                    flag = 1;
                    break;
                }    
            }
            if (flag == 1)
            {
                if (flagThoat == 0)
                {
                    DialogResult ret = MessageBox.Show("Bạn vẫn chưa sửa thông tin.\nXác nhận thoát?", "meMUSIC",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ret == DialogResult.No)
                        e.Cancel = true;
                }
            }
        }

        private void SuaThongTinTaiKhoan()
        {
            DialogResult ret = MessageBox.Show("Xác nhận sửa thông tin?", "meMUSIC",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                flagThoat = 1;
                List<User> listUsers = contextmeMUSIC.Users.ToList();
                User dbUpdate = contextmeMUSIC.Users.First(x => x.username == this.username);
                if (txtNewPassword_Hide.Text != "")
                    dbUpdate.matkhau = txtNewPassword_Hide.Text;
                if (txtHo.Text != "")
                    dbUpdate.HoUser = txtHo.Text;
                if (txtTen.Text != "")
                    dbUpdate.TenUser = txtTen.Text;
                contextmeMUSIC.SaveChanges();
                MessageBox.Show("Sửa thông tin thành công.", "meMUSIC",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }    
        }
        
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                List<char> kytucodau = new List<char>();
                kytucodau.Add(' '); kytucodau.Add('á'); kytucodau.Add('à');
                kytucodau.Add('ả'); kytucodau.Add('ã'); kytucodau.Add('ạ');
                kytucodau.Add('ă'); kytucodau.Add('ắ'); kytucodau.Add('ằ');
                kytucodau.Add('ẳ'); kytucodau.Add('ẵ'); kytucodau.Add('ặ');
                kytucodau.Add('â'); kytucodau.Add('ấ'); kytucodau.Add('ầ');
                kytucodau.Add('ẩ'); kytucodau.Add('ẫ'); kytucodau.Add('ậ');
                kytucodau.Add('đ'); kytucodau.Add('ẹ'); kytucodau.Add('é');
                kytucodau.Add('è'); kytucodau.Add('ẻ'); kytucodau.Add('ẽ');
                kytucodau.Add('ê'); kytucodau.Add('ế'); kytucodau.Add('ề');
                kytucodau.Add('ể'); kytucodau.Add('ệ'); kytucodau.Add('ễ');
                kytucodau.Add('ô'); kytucodau.Add('ố'); kytucodau.Add('ồ');
                kytucodau.Add('ổ'); kytucodau.Add('ỗ'); kytucodau.Add('ộ');
                kytucodau.Add('ơ'); kytucodau.Add('ớ'); kytucodau.Add('ờ');
                kytucodau.Add('ở'); kytucodau.Add('ỡ'); kytucodau.Add('ợ');
                kytucodau.Add('ư'); kytucodau.Add('ứ'); kytucodau.Add('ừ');
                kytucodau.Add('ử'); kytucodau.Add('ữ'); kytucodau.Add('ự');
                kytucodau.Add('í'); kytucodau.Add('ì'); kytucodau.Add('ỉ');
                kytucodau.Add('ĩ'); kytucodau.Add('ị');
                kytucodau.Add('ú'); kytucodau.Add('ù'); kytucodau.Add('ũ');
                kytucodau.Add('ủ'); kytucodau.Add('ụ');
                kytucodau.Add('ó'); kytucodau.Add('ò'); kytucodau.Add('õ');
                kytucodau.Add('ỏ'); kytucodau.Add('ọ');
                kytucodau.Add('ý'); kytucodau.Add('ỳ'); kytucodau.Add('ỷ');
                kytucodau.Add('ỹ'); kytucodau.Add('ỵ');

                foreach (char x in txtNewPassword_Hide.Text.ToLower())
                {
                    if (kytucodau.Contains(x))
                        throw new Exception("Password không dấu hoặc khoảng trắng.");
                }
                if (txtHo.Text.Length + txtTen.Text.Length > 20)
                    throw new Exception("Bạn đặt tên ngắn lại nha.");
                if (txtHo.Text != "" && txtTen.Text == "")
                    if ((txtHo.Text.Length + this.tenUser.Length > 20))
                        throw new Exception("Bạn đặt tên ngắn lại nha.");
                if (txtTen.Text != "" && txtHo.Text == "")
                    if ((txtTen.Text.Length + this.hoUser.Length > 20))
                        throw new Exception("Bạn đặt tên ngắn lại nha.");
                if (txtNewPassword_Hide.Text == "" && txtHo.Text == "" &&
                    txtTen.Text == "")
                    throw new Exception("Hãy sửa ít nhất một thông tin.");
                if (txtNewPassword_Hide.Text == this.password)
                    throw new Exception("Mật khẩu vẫn như cũ.\nĐể trống nếu bạn không muốn đổi.");
                if (txtHo.Text == this.hoUser)
                    throw new Exception("Họ vẫn như cũ.\nĐể trống nếu bạn không muốn đổi.");
                if (txtTen.Text == this.tenUser)
                    throw new Exception("Tên vẫn như cũ.\nĐể trống nếu bạn không muốn đổi.");
                if (txtNewPassword_Hide.Text == "" || txtHo.Text == "" ||
                    txtTen.Text == "")
                {
                    DialogResult ret = MessageBox.Show("Vẫn còn trường trống.\nBạn có muốn sửa thông tin đó?", "meMUSIC",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ret == DialogResult.No)
                    {
                        SuaThongTinTaiKhoan();
                    }
                }
                else
                    SuaThongTinTaiKhoan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "meMUSIC",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
