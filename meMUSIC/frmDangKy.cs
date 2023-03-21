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
    public partial class frmDangKy : Form
    {
        meMUSIC_DBContext contextmeMUSIC = new meMUSIC_DBContext();
        public frmDangKy()
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

        private void frmDangKy_Load(object sender, EventArgs e)
        {
            string rule = "+ Username và Password\nkhông quá 20 ký tự.";
            rule += "\n+ Username không phân biệt chữ hoa thường.";
            rule += "\nVí dụ: USER12 và user12 là như nhau!";
            rule += "\n+ Username và Password\nkhông dấu và khoảng trắng.";
            rule += "\n+ Cho phép ký tự đặc biệt.";
            rule += "\n+ Họ + Tên không quá 20 ký tự.";
            lblRule.Text = rule;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            List<User> listUsers = contextmeMUSIC.Users.ToList();
            List<Playlist> listPlaylist = contextmeMUSIC.Playlists.ToList();
            try
            {
                if (txtUsername.Text == "" ||
                    txtPassword_Hide.Text == "" ||
                    txtHo.Text == "" || txtTen.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin.");
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
                foreach (char x in txtUsername.Text.ToLower())
                {
                    if (kytucodau.Contains(x))
                        throw new Exception("Username không dấu hoặc khoảng trắng.");
                }
                foreach (char x in txtPassword_Hide.Text.ToLower())
                {
                    if (kytucodau.Contains(x))
                        throw new Exception("Password không dấu hoặc khoảng trắng.");
                }
                if (txtHo.Text.Length + txtTen.Text.Length > 20)
                    throw new Exception("Bạn đặt tên ngắn lại nha.");
                foreach (var x in listUsers)
                {
                    if (x.username.ToLower() == txtUsername.Text.ToLower())
                        throw new Exception("Username đã tồn tại.");
                }
                User u = new User()
                {
                    username = txtUsername.Text,
                    matkhau = txtPassword_Hide.Text,
                    HoUser = txtHo.Text,
                    TenUser = txtTen.Text,
                    NgayUser = DateTime.Now
                };
                Playlist p = new Playlist()
                {
                    MaPlaylist = txtUsername.Text + "_Liked",
                    TenPlaylist = "Bài hát đã thích",
                    username = txtUsername.Text,
                    NgayPlaylist = DateTime.Now
                };
                contextmeMUSIC.Users.Add(u);
                contextmeMUSIC.Playlists.Add(p);
                contextmeMUSIC.SaveChanges();

                MessageBox.Show("Đăng ký thành công!", "meMUSIC", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "meMUSIC", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
