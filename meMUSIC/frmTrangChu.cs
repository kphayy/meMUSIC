using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace meMUSIC
{
    public partial class frmTrangChu : Form
    {
        public string username, password, hoUser, tenUser;
        public DateTime ngayUser;

        meMUSIC_DBContext contextmeMUSIC = new meMUSIC_DBContext();
        public frmTrangChu()
        {
            InitializeComponent();
        }
        private bool formIsExist(Form frmOpen)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Name == frmOpen.Name)
                {
                    return true;
                }
            }
            return false;
        }

        private void SetHeight(ListView listView, int height)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);
            listView.SmallImageList = imgList;
        }//set độ rộng hàng cho listview

        private void LoadCombobox(ComboBox cbo)
        {
            cbo.Items.Clear();
            cbo.DisplayMember = "HienThi";
            cbo.ValueMember = "Khoa";
            meMUSIC_DBContext temp = new meMUSIC_DBContext();
            List<Playlist> listPlaylist = temp.Playlists
                .Where(p => p.username == username &&
                            p.MaPlaylist != (username + "_Liked"))
                .OrderBy(p => p.NgayPlaylist).ToList();
            foreach (var x in listPlaylist)
            {
                cbo.Items.Add(new { HienThi = x.TenPlaylist, Khoa = x.MaPlaylist });
            }
            cbo.ResetText();
        }

        private void CapNhatCombobox()
        {
            LoadCombobox(cboChonPlaylist_TrChu);
            LoadCombobox(cboChonPlaylist_Khac);
            LoadCombobox(cboChonPlaylist_TVien);
            LoadCombobox(cboChonPlaylist_Xoa);
            LoadCombobox(cboChonPlaylist_XemArtist);
            LoadCombobox(cboChonPlaylist_XemAlbum);
            LoadCombobox(cboChonPlaylist_Collection);
        }

        private void LoadDatabase()
        {
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            List<Album> listAlbum = contextmeMUSIC.Albums.ToList();
            List<CT_Album> listCT_Album = contextmeMUSIC.CT_Album.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabh = b.MaBaiHat,
                            indie = a.Indie,
                            namphathanh = b.NamPhatHanh,
                            luotnghe = b.LuotNghe,
                            maar = a.MaArtist
                        };
            var query2 = from al in listAlbum
                         join c in listCT_Album
                         on al.MaAlbum equals c.MaAlbum
                         select new
                         {
                             tenalbum = al.TenAlbum,
                             mabh = c.MaBaiHat,
                             maalbum = al.MaAlbum
                         };
            var result = from q1 in query
                         join q2 in query2
                         on q1.mabh equals q2.mabh into rp
                         from r in rp.DefaultIfEmpty()
                         orderby q1.indie, q1.namphathanh descending, q1.luotnghe descending,
                                 q1.tenbaihat, q1.tenartist
                         select new
                         {
                             tenbaihat = q1.tenbaihat,
                             tenartist = q1.tenartist,
                             tenalbum = r == null ? "(Single)" : r.tenalbum,
                             luotnghe = q1.luotnghe,
                             mabaihat = q1.mabh,
                             maalbum = r == null ? "" : r.maalbum,
                             maartist = q1.maar
                         };
            foreach (var x in result)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(x.tenalbum);
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maalbum);
                lvi.SubItems.Add(x.maartist);
                lvTrangChu.Items.Add(lvi);
            }
        }//load lv trang chu

        string tenPlist, ngayPlist, maPlistHienThi;// lưu thông tin playlist đnag được chọn hiển thị ở tab thư viện
        List<string> maPlistCapNhat = new List<string>();
        //lưu các playlist có được cập nhật(thêm/xóa bài hát) để reload nếu bị trùng playlist đang hiển thị
        int countSongPlist;
        private void LoadPlaylist(string maPlist)
        {
            HideMenuBH_TVien();
            pnlDoiTenPlaylist.Visible = false;
            lvPlaylist.Items.Clear();
            countSongPlist = 0;
            meMUSIC_DBContext temp = new meMUSIC_DBContext();
            List<BaiHat> listBaiHat = temp.BaiHats.ToList();
            List<Artist> listArtist = temp.Artists.ToList();
            List<Playlist> listPlaylist = temp.Playlists.ToList();
            List<CT_Playlist> listCT_Playlist = temp.CT_Playlist.ToList();
            Playlist playlist = temp.Playlists.First(p => p.MaPlaylist == maPlist);
            tenPlist = playlist.TenPlaylist;
            ngayPlist = String.Format("{0:dd/MM/yyyy}", playlist.NgayPlaylist);
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabh = b.MaBaiHat,
                            maar = b.MaArtist
                        };
            var query2 = from p in listPlaylist
                         join c in listCT_Playlist
                         on p.MaPlaylist equals c.MaPlaylist
                         where p.MaPlaylist == maPlist
                         select new
                         {
                             ngaythem = c.ThoiGianThem,
                             mabh = c.MaBaiHat
                         };
            var result = (from q1 in query
                          join q2 in query2
                          on q1.mabh equals q2.mabh into rp
                          from r in rp.DefaultIfEmpty()
                          select new
                          {
                              tenbaihat = r == null ? "" : q1.tenbaihat,
                              tenartist = r == null ? "" : q1.tenartist,
                              ngaythem = r == null ? DateTime.Now : r.ngaythem,
                              mabaihat = r == null ? "" : q1.mabh,
                              maartist = r == null ? "" : q1.maar
                          }).Where(x => x.tenbaihat != "")
                           .OrderBy(x => x.ngaythem);
            foreach (var x in result)
            {
                countSongPlist++;
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(String.Format("{0:dd/MM/yyyy}", x.ngaythem));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maartist);
                lvPlaylist.Items.Add(lvi);
            }
            lblTenPlaylist.Text = tenPlist;
            if (maPlist != (username + "_Liked"))
            {
                lblThongKe_Playlist.Text = $"Ngày tạo: {ngayPlist}, " +
                    $"{countSongPlist.ToString()} bài hát";
                btnDoiTen.Visible = btnXoa.Visible = true;
            }
            else
            {
                lblThongKe_Playlist.Text = $"{countSongPlist.ToString()} bài hát";
                btnDoiTen.Visible = btnXoa.Visible = false;
            }
        }//load lv thư viện

        private void CapNhatThongTinUser()
        {
            meMUSIC_DBContext temp = new meMUSIC_DBContext();
            User u = temp.Users.Where(x => x.username == username).First();
            password = u.matkhau;
            hoUser = u.HoUser;
            tenUser = u.TenUser;
        }

        string chaychu;
        private void VietLoiChao()
        {
            CapNhatThongTinUser();
            string now = DateTime.Now.ToString("hh:m t");
            int time = int.Parse(now.Substring(0, 2));
            string buoi = now.Last().ToString();
            string hoten = hoUser + " " + tenUser;
            string loichao = "";
            if (buoi == "A" && time >= 6 && time <= 11)
                loichao = $"Chào buổi sáng,\n{hoten}.";
            if (buoi == "P" && (time == 12 || (time >= 1 && time <= 5)))
                loichao = $"Hôm nay thế nào,\n{hoten}?";
            if (buoi == "P" && time >= 6 && time <= 11)
                loichao = $"Tối ấm nhé,\n{hoten}!";
            if (buoi == "A" && (time == 12 || (time >= 1 && time <= 5)))
                loichao = $"Vẫn chưa ngủ sao,\n{hoten}?";
            lblLoiChao.Text = loichao;
            string[] arrListStr = loichao.Split('\n');
            chaychu = $"                    {arrListStr[0]} {arrListStr[1]}                    ";
        }

        private void SetHeightAll()
        {
            SetHeight(lvTrangChu, 43);
            SetHeight(lvPlaylist, 43);
            SetHeight(lvXemArtist_Album, 43);
            SetHeight(lvXemArtist_BaiHat, 43);
            SetHeight(lvXemAlbum, 43);
            SetHeight(lvCollection, 43);
        }

        private void SetUpGiaoDien()
        {
            txtTrangChu.Focus();
            txtTrangChu.Select(0, 0);
            wmpPlayNhac.settings.volume = 100;
            SetHeightAll();
            lblChayChu.Text = chaychu;
        }//của trang chủ

        private void HideTab()
        {
            pnlMainMenu.Visible = false;
            pnlTVien_MainPage.Visible = false;
            pnlGenre_Main.Visible = false;
            pnlHotArtist_Main.Visible = false;
            pnlSideMenu_TrChu.Visible = false;
            pnlSideMenu_TVien.Visible = false;
            pnlGenre_SideMenu.Visible = false;
            pnlArtist_SideMenu.Visible = false;
            pnlXemArtist.Visible = false;
            pnlXemAlbum.Visible = false;
            pnlCollection.Visible = false;
            pnlGoiY_Main.Visible = false;
            pnlGoiY_SideMenu.Visible = false;
        }

        private void GoiTabCon(Panel pnlCon)
        {
            HideTab();
            pnlCon.Visible = true;
            pnlPlayNhac.Visible = true;
        }

        List<BaiHatInfo> bhInfoHistory = new List<BaiHatInfo>();
        List<string> XemArtistHistory = new List<string>();
        List<string> XemAlbumHistory = new List<string>();
        List<HotGenreCollection> CollectionHistory = new List<HotGenreCollection>();
        //lưu thông tin bài hát, trang nghệ sĩ, trang album cũng như collection ng dùng
        //đã xem theo thứ tự để nhấn nút back lại đúng
        //thứ tự khi xem nhiều nghệ sĩ, album hay collection khác nhau
        //xem BackXemArtistHistory()

        string maar_XemArtist;//lưu mã artist đang xem ở tab xem artist
        private void GoiTabCon_XemArtist(string maar)
        {
            maar_XemArtist = maar;
            GoiTabCon(pnlXemArtist);
            if (flagInfo == 1)
            {
                ShowMenuTrai();
                ShowInfoPanel(mabh, tenbh, tenar, tenalb, maalb, lblTopSearch.Text);
            }
            btnBack_XemArtist.Visible = true;
            LoadAlbum_XemArtist(maar);
            LoadBaiHat_XemArtist(maar);
            SetUpGiaoDien_XemArtist(maar);
            if (txtTemp.Visible)
                txtTemp.Enabled = false;
            txtThuVien.Enabled = false;
            txtTrangChu.Enabled = false;
            txtCollection.Enabled = false;
        }

        private void GoiTabCon_XemAlbum(string maalb)
        {
            GoiTabCon(pnlXemAlbum);
            if (flagInfo == 1)
            {
                ShowMenuTrai();
                ShowInfoPanel(mabh, tenbh, tenar, tenalb, maalb, lblTopSearch.Text);
            }
            btnBack_XemAlbum.Visible = true;
            LoadCT_Album(maalb);
            SetUpGiaoDien_XemAlbum(maalb);
            if (txtTemp.Visible)
                txtTemp.Enabled = false;
            txtThuVien.Enabled = false;
            txtTrangChu.Enabled = false;
            txtCollection.Enabled = false;
        }

        private void BackXemArtistHistory()
        {
            if (TabHistory.Last() == 6)
            {
                TabHistory.RemoveAt(TabHistory.LastIndexOf(TabHistory.Last()));
                GoiTabCon_XemArtist(XemArtistHistory.Last());
                cboArtistChonNhanh.ResetText();
            }
            else
            {
                GoiTabCon_XemArtist(XemArtistHistory.Last());
            }
        }

        private void BackCollectionHistory()
        {
            HotGenreCollection temp = CollectionHistory.Last();
            if (TabHistory.Last() == 14)
            {
                TabHistory.RemoveAt(TabHistory.Count - 1);
                GoiTabCon_Collection(temp.tieude, temp.maGenre, temp.stt);
                cboChonTheloai.ResetText();
            }
            else
            {
                GoiTabCon_Collection(temp.tieude, temp.maGenre, temp.stt);
            }
        }

        private void ChuyenTab(Panel mainpnl, Panel sidepnl)
        {
            HideTab();
            mainpnl.Visible = true;
            sidepnl.Visible = true;
            if (mainpnl.Name == "pnlMainMenu" || mainpnl.Name == "pnlTVien_MainPage")
                pnlPlayNhac.Visible = true;
            else
                pnlPlayNhac.Visible = false;
        }
        
        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            ChuyenTabTrChu();
            btnBack_TrChu.Visible = false;
            VietLoiChao();
            SetUpGiaoDien();
            LoadDatabase();
            TabHistory.Add(0);
            CapNhatCombobox();
            LoadPlaylist(maPlistHienThi = username + "_Liked");
            maPlistCapNhat.Add(maPlistHienThi);
        }

        int flagThoat = 0;//chỉ hỏi khi thoát mà không bấm nút đăng xuất
        private void frmTrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flagThoat != 1)
            {
                DialogResult ret = MessageBox.Show("Thoát cũng sẽ đăng xuất.\nBạn vẫn muốn thoát?", "meMUSIC",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ret == DialogResult.No)
                {
                    VietLoiChao();
                    e.Cancel = true;
                }    
            }
            
        }

        private void dangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flagThoat = 1;
            DialogResult ret = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "meMUSIC",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void GetListViewItem(ListViewItem lvi)
        {
            string title = lvi.SubItems[0].Text;
            title = title.Replace("&", "&&");
            string[] arrListStr = title.Split('\n');
            tenbh = arrListStr[0];
            tenar = arrListStr[1];
            string tempalb = lvi.SubItems[1].Text;
            tempalb = tempalb.Replace("&", "&&");
            if (tempalb == "(Single)")
            {
                string[] arrListStr2 = tenbh.Split('(');
                tempalb = arrListStr2[0] + " " + tempalb;
            }    
            tenalb = tempalb;
            mabh = lvi.SubItems[3].Text;
            maalb = lvi.SubItems[4].Text;
            maar = lvi.SubItems[5].Text;
        }//lấy thông tin lv trang chủ

        string mabh, maalb, tenalb, tenbh, tenar, maar; //lưu thông tin bh ở trang chủ
        string mabhTVien, maarTVien;// ở thư viện
        string tenbhFlowText, tenarFlowText, tenalbFlowText = ""; // khung chạy chữ
        private void PlayNhac(string mabh, string tenbh, string tenar, string tenalb, string maar)
        {
            wmpPlayNhac.URL = $@"D:\meMUSIC\{mabh}.mp3";
            ChayChu(tenbh, tenar, tenalb);
            flagPlayAll = 0;
            if (tenalb.Contains("Playlist: "))
                flagPlayTVien = maPlistHienThi;
            else
                flagPlayTVien = "";
            //update lượt nghe
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            BaiHat dbUpdateBH = contextmeMUSIC.BaiHats.First(x => x.MaBaiHat == mabh);
            dbUpdateBH.LuotNghe++;
            //update lượt nghe cá nhân trên nghệ sĩ này
            List<UserFrequentArtist> listUserFrAr = contextmeMUSIC.UserFrequentArtists.ToList();
            int flagFrAr = 0;
            foreach (var x in listUserFrAr)
            {
                if (x.username == username)
                {
                    if (x.MaArtist == maar)
                    {
                        flagFrAr = 1;
                        break;
                    }
                }
            }
            if (flagFrAr == 1)
            {
                UserFrequentArtist dbUpdateUFA = contextmeMUSIC.UserFrequentArtists
                    .First(x => x.username == username && x.MaArtist == maar);
                dbUpdateUFA.LuotNgheCaNhan++;
                dbUpdateUFA.LanNgheCuoi = DateTime.Now;
            }
            else
            {
                UserFrequentArtist ufa = new UserFrequentArtist()
                {
                    username = username,
                    MaArtist = maar,
                    LuotNgheCaNhan = 1,
                    LanNgheCuoi = DateTime.Now
                };
                contextmeMUSIC.UserFrequentArtists.Add(ufa);
            }
            contextmeMUSIC.SaveChanges();
        }//chơi nhạc, cập nhật dòng chạy chữ, update lượt nghe hệ thống và lượt nghe cá nhân

        private void frmTrangChu_FormClosed(object sender, FormClosedEventArgs e)
        {
            wmpPlayNhac.Ctlcontrols.stop();
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "frmDangNhap")
                    Application.OpenForms[i].Close();
            }
        }//sau khi thoát trang chủ cũng dừng nhạc và thoát các form con

        private void suaTaiKhoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSuaTaiKhoan frmsuaTaiKhoan = new frmSuaTaiKhoan();
            try
            {
                if (formIsExist(frmsuaTaiKhoan))
                    throw new Exception("Đã mở cửa sổ này.");
                else
                {
                    frmsuaTaiKhoan.username = username;
                    frmsuaTaiKhoan.password = password;
                    frmsuaTaiKhoan.hoUser = hoUser;
                    frmsuaTaiKhoan.tenUser = tenUser;
                    frmsuaTaiKhoan.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "meMUSIC",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int countSongXemArtist, countAlbum;//lưu thông tin thống kê ở tab con Xem Artist
        long sumLuotNghe;
        string tenar_XemArtist;

        private void LoadBaiHat_XemArtist(string maar)
        {
            countSongXemArtist = 0;
            sumLuotNghe = 0;
            lvXemArtist_BaiHat.Items.Clear();
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        where a.MaArtist == maar
                        orderby b.NamPhatHanh descending, b.LuotNghe descending
                        select new
                        {
                            tenartist = a.TenArtist,
                            tenbaihat = b.TenBaiHat,
                            namphathanh = b.NamPhatHanh,
                            luotnghe = b.LuotNghe,
                            mabaihat = b.MaBaiHat
                        };
            foreach (var x in query)
            {
                countSongXemArtist++;
                sumLuotNghe += x.luotnghe;
                tenar_XemArtist = x.tenartist;
                ListViewItem lvi = new ListViewItem(x.tenbaihat +
                    "\n   Năm phát hành: " + x.namphathanh.ToString());
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvXemArtist_BaiHat.Items.Add(lvi);
            }
        }

        private void LoadAlbum_XemArtist(string maar)
        {
            countAlbum = 0;
            lvXemArtist_Album.Items.Clear();
            List<Album> listAlbum = contextmeMUSIC.Albums.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            var query = from al in listAlbum
                        join a in listArtist
                        on al.MaArtist equals a.MaArtist
                        where a.MaArtist == maar
                        orderby al.NamAlbum descending
                        select new
                        {
                            tenalbum = al.TenAlbum,
                            namalbum = al.NamAlbum,
                            maalbum = al.MaAlbum
                        };
            foreach (var x in query)
            {
                countAlbum++;
                ListViewItem lvi = new ListViewItem(x.tenalbum);
                lvi.SubItems.Add(x.namalbum.ToString());
                lvi.SubItems.Add(x.maalbum);
                lvXemArtist_Album.Items.Add(lvi);
            }
        }

        private void SetUpGiaoDien_XemArtist(string maar)
        {
            pnlXemArtist.AutoScrollPosition = new Point(pnlXemArtist.AutoScrollPosition.X, 0);
            pnlXemArtist.VerticalScroll.Value = 0;
            lblXemArtist_Ten.Text = tenar_XemArtist;
            lblXemArtist_ThongKe.Text = countSongXemArtist.ToString() + " bài hát, " +
                countAlbum.ToString() + " album, " +
                String.Format("{0:n0}", sumLuotNghe) + " lượt nghe";
            pboArtistCover.Image = Image.FromFile($@"D:\meMUSIC_Artist\{maar}.png");
        }

        string mabh_xemAlbum, tenbh_xemAlbum, tenalb_xemAlbum, maar_xemAlbum, tenar_xemAlbum;
        int namalb, countSongXemAlbum;
        private void LoadCT_Album(string maalb)
        {
            countSongXemAlbum = 0;
            lvXemAlbum.Items.Clear();
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<CT_Album> listCT_Album = contextmeMUSIC.CT_Album.ToList();
            Album alb = contextmeMUSIC.Albums.First(x => x.MaAlbum == maalb);
            tenalb_xemAlbum = alb.TenAlbum;
            namalb = alb.NamAlbum;
            maar_xemAlbum = alb.MaArtist;
            Artist artist = contextmeMUSIC.Artists.First(x => x.MaArtist == maar_xemAlbum);
            tenar_xemAlbum = artist.TenArtist;
            var query = from b in listBaiHat
                        join c in listCT_Album
                        on b.MaBaiHat equals c.MaBaiHat
                        where c.MaAlbum == maalb
                        orderby b.LuotNghe descending
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            typeph = c.TrangThaiPhatHanh,
                            mabaihat = b.MaBaiHat
                        };
            foreach (var x in query)
            {
                countSongXemAlbum++;
                ListViewItem lvi = new ListViewItem(x.tenbaihat);
                lvi.SubItems.Add(x.typeph);
                lvi.SubItems.Add(x.mabaihat);
                lvXemAlbum.Items.Add(lvi);
            }
        }//load lv tab con Xem Album

        private void SetUpGiaoDien_XemAlbum(string maalb)
        {
            pnlXemAlbum.AutoScrollPosition = new Point(pnlXemAlbum.AutoScrollPosition.X, 0);
            pnlXemAlbum.VerticalScroll.Value = 0;
            lblTenAlbum_XemAlbum.Text = tenalb_xemAlbum;
            lblTenArtist_XemAlbum.Text = "Artist: " + tenar_xemAlbum;
            lblThongKe_XemAlbum.Text = $"Năm phát hành: {namalb.ToString()}, " +
                $"{countSongXemAlbum.ToString()} bài hát";
            pboAlbumCover.Image = Image.FromFile($@"D:\meMUSIC_Album\{maalb}.png");
        }

        string tieude_Collection, maGenre,
            tenbh_Collection, tenar_Collection, maBH_Collection, maAR_Collection;
        int stt_Collection;

        private void LoadCollectionTheoTL1(string maGenre)
        {
            lvCollection.Items.Clear();
            
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        where b.TheLoai1.Contains(maGenre)
                        orderby a.Indie, b.NamPhatHanh descending, b.LuotNghe descending,
                                 b.TenBaiHat, a.TenArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabaihat = b.MaBaiHat,
                            maartist = b.MaArtist,
                            luotnghe = b.LuotNghe
                        };
            foreach (var x in query)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maartist);
                lvCollection.Items.Add(lvi);
            }
        }

        private void TimCollectionTheoTL1()
        {
            lvCollection.Items.Clear();

            string timkiem = txtCollection.Text.ToLower();
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        where b.TheLoai1.Contains(maGenre) &&
                              ((!String.IsNullOrEmpty(b.BHKeyword) && b.BHKeyword.ToLower().Contains(timkiem)) ||
                              b.TenBaiHat.ToLower().Contains(timkiem) ||
                              (!String.IsNullOrEmpty(a.ArKeyword) && a.ArKeyword.ToLower().Contains(timkiem)) ||
                               a.TenArtist.ToLower().Contains(timkiem))
                        orderby a.Indie, b.NamPhatHanh descending, b.LuotNghe descending,
                                 b.TenBaiHat, a.TenArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabaihat = b.MaBaiHat,
                            maartist = b.MaArtist,
                            luotnghe = b.LuotNghe
                        };
            foreach (var x in query)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maartist);
                lvCollection.Items.Add(lvi);
            }
        }

        private void LoadCollectionTheoTL2(string maGenre)
        {
            lvCollection.Items.Clear();

            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        where b.TheLoai2.Contains(maGenre)
                        orderby a.Indie, b.NamPhatHanh descending, b.LuotNghe descending,
                                 b.TenBaiHat, a.TenArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabaihat = b.MaBaiHat,
                            maartist = b.MaArtist,
                            luotnghe = b.LuotNghe
                        };
            foreach (var x in query)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maartist);
                lvCollection.Items.Add(lvi);
            }
        }

        private void TimCollectionTheoTL2()
        {
            lvCollection.Items.Clear();

            string timkiem = txtCollection.Text.ToLower();
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        where b.TheLoai2.Contains(maGenre) &&
                              ((!String.IsNullOrEmpty(b.BHKeyword) && b.BHKeyword.ToLower().Contains(timkiem)) ||
                              b.TenBaiHat.ToLower().Contains(timkiem) ||
                              (!String.IsNullOrEmpty(a.ArKeyword) && a.ArKeyword.ToLower().Contains(timkiem)) ||
                               a.TenArtist.ToLower().Contains(timkiem))
                        orderby a.Indie, b.NamPhatHanh descending, b.LuotNghe descending,
                                 b.TenBaiHat, a.TenArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabaihat = b.MaBaiHat,
                            maartist = b.MaArtist,
                            luotnghe = b.LuotNghe
                        };
            foreach (var x in query)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maartist);
                lvCollection.Items.Add(lvi);
            }
        }

        private void LoadCollectionTheoIndie()
        {
            lvCollection.Items.Clear();

            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        where a.Indie > 0
                        orderby a.Indie descending, b.NamPhatHanh descending, b.LuotNghe descending,
                                 b.TenBaiHat, a.TenArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabaihat = b.MaBaiHat,
                            maartist = b.MaArtist,
                            luotnghe = b.LuotNghe
                        };
            foreach (var x in query)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maartist);
                lvCollection.Items.Add(lvi);
            }
        }

        private void TimCollectionTheoIndie()
        {
            lvCollection.Items.Clear();

            string timkiem = txtCollection.Text.ToLower();
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        where a.Indie > 0 &&
                              ((!String.IsNullOrEmpty(b.BHKeyword) && b.BHKeyword.ToLower().Contains(timkiem)) ||
                              b.TenBaiHat.ToLower().Contains(timkiem) ||
                              (!String.IsNullOrEmpty(a.ArKeyword) && a.ArKeyword.ToLower().Contains(timkiem)) ||
                               a.TenArtist.ToLower().Contains(timkiem))
                        orderby a.Indie descending, b.NamPhatHanh descending, b.LuotNghe descending,
                                 b.TenBaiHat, a.TenArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabaihat = b.MaBaiHat,
                            maartist = b.MaArtist,
                            luotnghe = b.LuotNghe
                        };
            foreach (var x in query)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maartist);
                lvCollection.Items.Add(lvi);
            }
        }

        private void LoadCollectionQTBH(string maGenre)
        {
            lvCollection.Items.Clear();

            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        where b.TheLoai1.Contains(maGenre)
                        orderby b.NamPhatHanh, b.LuotNghe descending,
                                 b.TenBaiHat, a.TenArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabaihat = b.MaBaiHat,
                            maartist = b.MaArtist,
                            luotnghe = b.LuotNghe
                        };
            foreach (var x in query)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maartist);
                lvCollection.Items.Add(lvi);
            }
        }//qtbh cũng thuộc TL1, nhưng order by khác

        private void TimCollectionQTBH()
        {
            lvCollection.Items.Clear();

            string timkiem = txtCollection.Text.ToLower();
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        where b.TheLoai1.Contains(maGenre) &&
                              ((!String.IsNullOrEmpty(b.BHKeyword) && b.BHKeyword.ToLower().Contains(timkiem)) ||
                              b.TenBaiHat.ToLower().Contains(timkiem) ||
                              (!String.IsNullOrEmpty(a.ArKeyword) && a.ArKeyword.ToLower().Contains(timkiem)) ||
                               a.TenArtist.ToLower().Contains(timkiem))
                        orderby b.NamPhatHanh, b.LuotNghe descending,
                                 b.TenBaiHat, a.TenArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabaihat = b.MaBaiHat,
                            maartist = b.MaArtist,
                            luotnghe = b.LuotNghe
                        };
            foreach (var x in query)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maartist);
                lvCollection.Items.Add(lvi);
            }
        }

        private void SetUpGiaoDien_Collection(string tieude, int stt)
        {
            pnlCollection.AutoScrollPosition = new Point(pnlXemArtist.AutoScrollPosition.X, 0);
            pnlCollection.VerticalScroll.Value = 0;
            string tenCover = "C" + stt.ToString();
            lblTieuDeCollection.Text = tieude;
            pnlCollectionCover.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(tenCover);
        }

        string maar_GoiY1, maar_GoiY2, maar_GoiY3;//lưu các mã artist được gợi ý cho user
        private void ChonArtistGoiY()
        {
            maar_GoiY1 = maar_GoiY2 = maar_GoiY3 = "";
            List<UserFrequentArtist> listUFArtist = contextmeMUSIC.UserFrequentArtists
                .Where(x => x.username == username && x.LuotNgheCaNhan > 1)
                .OrderByDescending(x => x.LuotNgheCaNhan)
                .ThenByDescending(x => x.LanNgheCuoi).ToList();
            if (listUFArtist.Count > 0)
            {
                maar_GoiY1 = listUFArtist[0].MaArtist;
                if (listUFArtist.Count > 1)
                {
                    maar_GoiY2 = listUFArtist[1].MaArtist;
                    if (listUFArtist.Count > 2)
                    {
                        maar_GoiY3 = listUFArtist[2].MaArtist;
                    }
                }
            }
        }

        private void SetUpPanelArtistGoiY(Panel pnlArtistGoiY, 
            PictureBox pboArtistGoiY, Label lblArtistGoiY, string maar_GoiY)
        {
                pnlArtistGoiY.Visible = true;
                pboArtistGoiY.Image = Image.FromFile($@"D:\meMUSIC_Artist\{maar_GoiY}.png");
                lblArtistGoiY.Text = contextmeMUSIC.Artists
                    .First(x => x.MaArtist == maar_GoiY).TenArtist.Replace("&", "&&");
        }
        
        private void SetUpGiaoDien_GoiY()
        {
            pnlArtistGoiY1.Visible = pnlArtistGoiY2.Visible = pnlArtistGoiY3.Visible = false;
            ChonArtistGoiY();
            if (maar_GoiY1 == "")
            {
                lblTieuDeGoiY.Text = "Hãy nghe thêm nhạc để giúp chúng tôi gợi ý";
            }
            else
            {
                lblTieuDeGoiY.Text = "Dựa trên những gì bạn đã nghe";
                SetUpPanelArtistGoiY(pnlArtistGoiY1, pboArtistGoiY1, lblArtistGoiY1, maar_GoiY1);
                if (maar_GoiY2 != "")
                {
                    SetUpPanelArtistGoiY(pnlArtistGoiY2, pboArtistGoiY2, lblArtistGoiY2, maar_GoiY2);
                    if (maar_GoiY3 != "")
                        SetUpPanelArtistGoiY(pnlArtistGoiY3, pboArtistGoiY3, lblArtistGoiY3, maar_GoiY3);
                }
            }
        }

        private void btnXemArtist_Click(object sender, EventArgs e)
        {
            GoiTabCon_XemArtist(maar);
            pnlSideMenu_TrChu.Visible = true;
            btnXemArtist.Visible = false;
            BaiHatInfo temp = new BaiHatInfo()
            {
                mabh = mabh,
                tenbh = tenbh,
                tenar = tenar,
                tenalb = tenalb,
                maalb = maalb,
                topSearch = "BÀI HÁT ĐÃ XEM"
            };
            XemArtistHistory.Add(maar);
            if (TabHistory.Count > 0)
            {
                if (TabHistory.Last() == 0)
                //trchủ -> xem artist
                //ý nghĩa: vào trang chủ và chọn bài hát
                {
                    bhInfoHistory.Add(temp);
                    TabHistory.Add(4);
                }
                else
                //trchủ -> xem album(7) -> xem artist
                //xem artist(4) -> xem album(9) -> xem artist
                //xem album bên trong của artist(10) -> xem artist
                //ý nghĩa: thông tin bài hát chọn ở trang chủ vẫn lưu từ đầu
                {
                    bhInfoHistory.Add(bhInfoHistory.Last());
                    TabHistory.Add(8);
                }
            }
        }

        int flagSubMenuTrChu = 0;
        private void ShowSideMenuTrchu()
        {
            pnlTrangChu.Visible = true;
            btnTopCollection.Visible = true;
            btnTrChu_TVien.Visible = true;
            btnTaoPlaylist_TrChu.Visible = true;
            btnGoiY.Visible = true;
            if (flagSubMenuTrChu == 1)
                pnlSubMenu_NhapPlaylist_TrChu.Visible = true;
            if (flagSubMenuTrChu == 2)
                pnlTopCollection_SubMenu.Visible = true;
        }

        private void HideSideMenuTrchu()
        {
            pnlTrangChu.Visible = false;
            btnTopCollection.Visible = false;
            btnTrChu_TVien.Visible = false;
            btnTaoPlaylist_TrChu.Visible = false;
            btnGoiY.Visible = false;
            HideSubMenuTrChu();
        }

        private void txtTrangChu_TextChanged(object sender, EventArgs e)
        {
            VietLoiChao();
            lvTrangChu.Items.Clear();
            if (txtTrangChu.Text.Length == 0 && !txtThuVien.Visible)
                txtTemp.Visible = true;
            string timkiem = txtTrangChu.Text.ToLower();
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            List<Artist> listArtist = contextmeMUSIC.Artists.ToList();
            List<Album> listAlbum = contextmeMUSIC.Albums.ToList();
            List<CT_Album> listCT_Album = contextmeMUSIC.CT_Album.ToList();
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabh = b.MaBaiHat,
                            indie = a.Indie,
                            namphathanh = b.NamPhatHanh,
                            luotnghe = b.LuotNghe,
                            bhkeyword = b.BHKeyword,
                            arkeyword = a.ArKeyword,
                            maar = a.MaArtist
                        };
            var query2 = from al in listAlbum
                         join c in listCT_Album
                         on al.MaAlbum equals c.MaAlbum
                         select new
                         {
                             tenalbum = al.TenAlbum,
                             mabh = c.MaBaiHat,
                             maalbum = al.MaAlbum,
                             alkeyword = al.AlKeyword
                         };
            var result = from q1 in query
                         join q2 in query2
                         on q1.mabh equals q2.mabh into rp
                         from r in rp.DefaultIfEmpty()
                         where (!String.IsNullOrEmpty(q1.bhkeyword) && q1.bhkeyword.ToLower().Contains(timkiem)) ||
                               q1.tenbaihat.ToLower().Contains(timkiem) ||
                               (!String.IsNullOrEmpty(q1.arkeyword) && q1.arkeyword.ToLower().Contains(timkiem)) ||
                               q1.tenartist.ToLower().Contains(timkiem) ||
                               (!(r == null) && ((!String.IsNullOrEmpty(r.alkeyword) && r.alkeyword.ToLower().Contains(timkiem)) ||
                               (!String.IsNullOrEmpty(r.tenalbum) && r.tenalbum.ToLower().Contains(timkiem))))
                         orderby q1.indie, q1.namphathanh descending, q1.luotnghe descending, q1.tenbaihat, q1.tenartist
                         select new
                         {
                             tenbaihat = q1.tenbaihat,
                             tenartist = q1.tenartist,
                             tenalbum = r == null ? "(Single)" : r.tenalbum,
                             luotnghe = q1.luotnghe,
                             mabaihat = q1.mabh,
                             maalbum = r == null ? "" : r.maalbum,
                             maartist = q1.maar
                         };
            foreach (var x in result)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(x.tenalbum);
                lvi.SubItems.Add(String.Format("{0:n0}", x.luotnghe));
                lvi.SubItems.Add(x.mabaihat);
                lvi.SubItems.Add(x.maalbum);
                lvi.SubItems.Add(x.maartist);
                lvTrangChu.Items.Add(lvi);
            }
            CapNhatCombobox();
            if (txtTrangChu.Text.Length == 0 && lvTrangChu.Items.Count != 0)
            {
                HideMenuTrai();
                HideMenuPhai();
                ShowSideMenuTrchu();
            }
            else
            {
                HideSideMenuTrchu();
                if (lvTrangChu.Items.Count == 0)
                {
                    lblTopSearch.Text = "Rất tiếc! Chúng tôi\nkhông tìm được\nkết quả nào.";
                    HideMenuTrai();
                    HideMenuPhai();
                    pnlInfo.Visible = true;
                    btnBack_Info.Visible = true;
                    lblTopSearch.Visible = true;
                    lblSadFace.Visible = true;
                }
                else
                {
                    ListViewItem lvi = lvTrangChu.Items[0];
                    GetListViewItem(lvi);
                    HienThiNutLike(mabh, btnLike_TrChu);
                    ShowMenuTrai();
                    ShowInfoPanel(mabh, tenbh, tenar, tenalb, maalb, "KẾT QUẢ HÀNG ĐẦU");
                    ShowMenuPhai();
                    CapNhatComboBox_OnSelection(cboChonPlaylist_TrChu, mabh);
                }
            }
        }

        private void txtThuVien_TextChanged(object sender, EventArgs e)
        {
            VietLoiChao();
            HideMenuBH_TVien();
            lvPlaylist.Items.Clear();
            if (txtThuVien.Text.Length == 0 && !txtTrangChu.Visible)
                txtTemp.Visible = true;
            string timkiem = txtThuVien.Text;
            meMUSIC_DBContext temp = new meMUSIC_DBContext();
            List<BaiHat> listBaiHat = temp.BaiHats.ToList();
            List<Artist> listArtist = temp.Artists.ToList();
            List<Playlist> listPlaylist = temp.Playlists.ToList();
            List<CT_Playlist> listCT_Playlist = temp.CT_Playlist.ToList();
            Playlist playlist = temp.Playlists.First(p => p.MaPlaylist == maPlistHienThi);
            tenPlist = playlist.TenPlaylist;
            ngayPlist = String.Format("{0:dd/MM/yyyy}", playlist.NgayPlaylist);
            var query = from b in listBaiHat
                        join a in listArtist
                        on b.MaArtist equals a.MaArtist
                        select new
                        {
                            tenbaihat = b.TenBaiHat,
                            tenartist = a.TenArtist,
                            mabh = b.MaBaiHat,
                            bhkeyword = b.BHKeyword,
                            arkeyword = a.ArKeyword
                        };
            var query2 = from p in listPlaylist
                         join c in listCT_Playlist
                         on p.MaPlaylist equals c.MaPlaylist
                         where p.MaPlaylist == maPlistHienThi
                         select new
                         {
                             ngaythem = c.ThoiGianThem,
                             mabh = c.MaBaiHat
                         };
            var result = (from q1 in query
                          join q2 in query2
                          on q1.mabh equals q2.mabh into rp
                          from r in rp.DefaultIfEmpty()
                          where (!String.IsNullOrEmpty(q1.bhkeyword) && q1.bhkeyword.ToLower().Contains(timkiem)) ||
                               q1.tenbaihat.ToLower().Contains(timkiem) ||
                               (!String.IsNullOrEmpty(q1.arkeyword) && q1.arkeyword.ToLower().Contains(timkiem)) ||
                               q1.tenartist.ToLower().Contains(timkiem)
                          select new
                          {
                              tenbaihat = r == null ? "" : q1.tenbaihat,
                              tenartist = r == null ? "" : q1.tenartist,
                              ngaythem = r == null ? DateTime.Now : r.ngaythem,
                              mabaihat = r == null ? "" : q1.mabh
                          }).Where(x => x.tenbaihat != "")
                           .OrderBy(x => x.ngaythem);
            foreach (var x in result)
            {
                string title = x.tenbaihat + "\n   " + x.tenartist;
                ListViewItem lvi = new ListViewItem(title);
                lvi.SubItems.Add(String.Format("{0:dd/MM/yyyy}", x.ngaythem));
                lvi.SubItems.Add(x.mabaihat);
                lvPlaylist.Items.Add(lvi);
            }
        }

        private void ChayChu(string tenbh, string tenar, string tenalb)
        {
            tenbhFlowText = tenbh;
            tenarFlowText = tenar;
            tenalbFlowText = tenalb;
        }

        private void btnPlay_TrChu_Click(object sender, EventArgs e)
        {
            if (!tenalb.Contains("(Single)"))
                PlayNhac(mabh, tenbh, tenar, $"Album: {tenalb}", maar);
            else
                PlayNhac(mabh, tenbh, tenar, "", maar);
            VietLoiChao();
        }

        private void CapNhatComboBox_OnSelection(ComboBox cbo, string mabh)
        {
            List<Playlist> thisUserPlaylists = contextmeMUSIC.Playlists
                .Where(p => p.username == username && 
                            p.MaPlaylist != (username + "_Liked")).ToList();
            List<CT_Playlist> listCT_Playlist = contextmeMUSIC.CT_Playlist.ToList();
            foreach (var p in thisUserPlaylists)
            {
                foreach (var x in listCT_Playlist)
                {
                    if (p.MaPlaylist == x.MaPlaylist)
                        if (x.MaBaiHat == mabh)
                        {
                            cbo.Items.RemoveAt(cbo.FindStringExact(p.TenPlaylist));
                        }
                }
            }
        }//khi ng dùng chọn bài hát nào đó thì cbo của trang đó được cập nhật ngay xóa các playlist đã có bài đã chọn

        int flagInfo = 0;//ng dùng có nhấn nút back về menu ko để ẩn info bài hát ko
        private void lvTrangChu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvTrangChu.SelectedItems.Count > 0)
            {
                HideSideMenuTrchu();
                pnlAddPlaylist_SubMenu_TrChu.Visible = false;
                lblTopSearch.Text = "BẠN ĐANG XEM";
                ShowMenuPhai();
                ListViewItem lvi = lvTrangChu.SelectedItems[0];
                GetListViewItem(lvi);
                HienThiNutLike(mabh, btnLike_TrChu);
                ShowMenuTrai();
                ShowInfoPanel(mabh, tenbh, tenar, tenalb, maalb, lblTopSearch.Text);
                VietLoiChao();
                CapNhatCombobox();
                CapNhatComboBox_OnSelection(cboChonPlaylist_TrChu, mabh);
            }
        }

        private void NhanNutThich(string mabh, Button btnLike)
        {
            string temp = username + "_Liked";
            maPlistCapNhat.Add(temp);
            meMUSIC_DBContext context = new meMUSIC_DBContext();
            List<CT_Playlist> listCT_Playlist = context.CT_Playlist.ToList();
            if (btnLike.Text == "🤍")
            {
                CT_Playlist x = new CT_Playlist()
                {
                    MaPlaylist = temp,
                    MaBaiHat = mabh,
                    ThoiGianThem = DateTime.Now
                };
                context.CT_Playlist.Add(x);
                context.SaveChanges();
                btnLike.Text = "♥";
            }
            else
            {
                CT_Playlist dbDelete = listCT_Playlist
                    .First(x => (x.MaPlaylist == temp && x.MaBaiHat == mabh));
                context.CT_Playlist.Remove(dbDelete);
                context.SaveChanges();
                btnLike.Text = "🤍";
            }
        }//cập nhật playlist đã thích và hình ảnh trái tim khi nhấn nút like
        
        private void btnLike_TrChu_Click(object sender, EventArgs e)
        {
            NhanNutThich(mabh, sender as Button);
            VietLoiChao();
        }

        private void lvTrangChu_DoubleClick(object sender, EventArgs e)
        {
            lvTrangChu_SelectedIndexChanged(sender, e);
            if (!tenalb.Contains("(Single)"))
                PlayNhac(mabh, tenbh, tenar, $"Album: {tenalb}", maar);
            else
                PlayNhac(mabh, tenbh, tenar, "", maar);
            VietLoiChao();
        }

        private void lvTrangChu_Click(object sender, EventArgs e)
        {
            lvTrangChu_SelectedIndexChanged(sender, e);
            VietLoiChao();
        }
        //khi đang chọn bài hát trên lvi mà ng dùng đã back ra khỏi menu info bài hát khi nhấn lại bài đã chọn sẽ hiện ra lại

        private void xemTaiKhoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmXemTaiKhoan frmxemTaiKhoan = new frmXemTaiKhoan();
            try
            {
                if (formIsExist(frmxemTaiKhoan))
                    throw new Exception("Đã mở cửa sổ này.");
                else
                {
                    frmxemTaiKhoan.username = username;
                    frmxemTaiKhoan.password = password;
                    frmxemTaiKhoan.hoUser = hoUser;
                    frmxemTaiKhoan.tenUser = tenUser;
                    frmxemTaiKhoan.ngayUser = ngayUser;
                    frmxemTaiKhoan.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "meMUSIC",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTopCollection_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "btnTopCollection")
                Show2SubMenu(pnlTopCollection_SubMenu, sender as Button, "    Top Collections",
                    pnlSubMenu_NhapPlaylist_TrChu, btnTaoPlaylist_TrChu, "    Tạo playlist mới");
            if ((sender as Button).Name == "btnTaoPlaylist_TrChu")
            {
                Show2SubMenu(pnlSubMenu_NhapPlaylist_TrChu, sender as Button, "    Tạo playlist mới",
                    pnlTopCollection_SubMenu, btnTopCollection, "    Top Collections");
                txtNhapPlaylist_TrChu.Focus();
                txtNhapPlaylist_TrChu.SelectionStart = txtNhapPlaylist_TrChu.Text.Length;
            }
            if ((sender as Button).Name == "btnTaoPlaylist_Genre")
            {
                Show2SubMenu(pnlSubMenu_NhapPlaylist_Genre, sender as Button, "    Tạo playlist mới",
                    pnlGenreChonNhanh_SubMenu, btnGenreChonNhanh, "    Chọn nhanh");
                txtNhapPlaylist_Genre.Focus();
                txtNhapPlaylist_Genre.SelectionStart = txtNhapPlaylist_Genre.Text.Length;
            }
            if ((sender as Button).Name == "btnGenreChonNhanh")
            {
                Show2SubMenu(pnlGenreChonNhanh_SubMenu, sender as Button, "    Chọn nhanh",
                    pnlSubMenu_NhapPlaylist_Genre, btnTaoPlaylist_Genre, "    Tạo playlist mới");
            }
            if ((sender as Button).Name == "btnTaoPlaylist_Artist")
            {
                Show2SubMenu(pnlSubMenu_NhapPlaylist_Artist, sender as Button, "    Tạo playlist mới",
                    pnlArtistChonNhanh_SubMenu, btnArtistChonNhanh, "    Chọn nhanh");
                txtNhapPlaylist_Artist.Focus();
                txtNhapPlaylist_Artist.SelectionStart = txtNhapPlaylist_Artist.Text.Length;
            }
            if ((sender as Button).Name == "btnArtistChonNhanh")
            {
                Show2SubMenu(pnlArtistChonNhanh_SubMenu, sender as Button, "    Chọn nhanh",
                    pnlSubMenu_NhapPlaylist_Artist, btnTaoPlaylist_Artist, "    Tạo playlist mới");
            }
            VietLoiChao();
        }

        private void btnBack_Info_Click(object sender, EventArgs e)
        {
            HideMenuTrai();
            if (lvTrangChu.SelectedItems.Count == 0)
                HideMenuPhai();
            ShowSideMenuTrchu();
            VietLoiChao();
        }

        private void btnLikedPlaylist_Click(object sender, EventArgs e)
        {
            if (maPlistHienThi != (username + "_Liked"))
                LoadPlaylist(maPlistHienThi = (username + "_Liked"));
            VietLoiChao();
        }

        List<int> TabHistory = new List<int>();
        //lưu thứ tự chuyển tab: 0 - TrChu, 1 - TVien, 2 - Genre,
        //                       3 - HotArtist, 4 - XemArtist mở từ TrChu
        //                       5 - Xem Artistm mở từ HotArtist
        //                       6 - XemArtist mở bằng cbo khi đang xem artist rồi
        //                       7 -> 12: có comment bên trong các event cụ thể
        //                       13 - Collection mở từ HOt Genre
        //                       14 - Collection mở bằng cbo khi đang xem collection khác
        //                       15 - trang gợi ý
        //                       16 - Xem Artist mở từ trang gợi ý
        //                       17 - (16) -> xem album bên trong
        //xem code nút back ở method NhanNutBack()

        private void ChuyenTabTrChu()
        {
            ChuyenTab(pnlMainMenu, pnlSideMenu_TrChu);
            btnBack_TrChu.Visible = true;
            if (flagInfo == 1)
            {
                if (lvTrangChu.SelectedItems.Count > 0)
                    lblTopSearch.Text = "BẠN ĐANG XEM";
                btnXemArtist.Visible = true;
                ShowInfoPanel(mabh, tenbh, tenar, tenalb, maalb, lblTopSearch.Text);
            }
                
            VietLoiChao();
            CapNhatCombobox();
            txtTemp.Enabled = true;
            txtThuVien.Enabled = true;
            txtTrangChu.Enabled = true;
            txtCollection.Enabled = true;
            txtTrangChu.Visible = true;
            txtTrangChu.Focus();
            txtTrangChu.SelectionStart = txtTrangChu.Text.Length;
            txtTemp.Visible = false;
            txtTemp.Text = "Tìm kiếm bài hát, nghệ sĩ, album";
            if (txtTrangChu.Text.Length == 0)
                txtTemp.Visible = true;
            txtCollection.Visible = txtThuVien.Visible = false;
        }

        private void btnAdd_TrChu_Click(object sender, EventArgs e)
        {
            pnlAddPlaylist_SubMenu_TrChu.Visible = !pnlAddPlaylist_SubMenu_TVien.Visible;
            VietLoiChao();
        }

        private void Choncbo_AddPlaylist(ComboBox cbo, string mabh)
        {
            string temp = (cbo.SelectedItem as dynamic).Khoa;
            maPlistCapNhat.Add(temp);
            string tenPl = (cbo.SelectedItem as dynamic).HienThi;
            CT_Playlist x = new CT_Playlist()
            {
                MaPlaylist = temp,
                MaBaiHat = mabh,
                ThoiGianThem = DateTime.Now
            };
            contextmeMUSIC.CT_Playlist.Add(x);
            contextmeMUSIC.SaveChanges();
            cbo.Items.RemoveAt(cbo.FindStringExact(tenPl));
            MessageBox.Show($"Đã thêm vào playlist: {tenPl}",
                    "meMUSIC", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//nhấn chọn playlist để thêm bài hát vào

        private void cboChonPlaylist_TrChu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choncbo_AddPlaylist(sender as ComboBox, mabh);
            pnlAddPlaylist_SubMenu_TrChu.Visible = false;
            VietLoiChao();
        }

        private void ShowMenuBH_TVien()
        {
            btnLike_ThuVien.Visible = true;
            btnPlay_ThuVien.Visible = true;
            btnAdd_ThuVien.Visible = true;
            if (maPlistHienThi != (username + "_Liked"))
                btnXoabhKhoiPl.Visible = true;
        }//hiện các nút con khi chọn bài hát ở thư viện

        private void HideMenuBH_TVien()
        {
            btnLike_ThuVien.Visible = false;
            btnPlay_ThuVien.Visible = false;
            btnAdd_ThuVien.Visible = false;
            btnXoabhKhoiPl.Visible = false;
            pnlAddPlaylist_SubMenu_TVien.Visible = false;
        }//ẩn

        string tenbhTVien, tenarTVien;//lưu thông tin bài hát đc chọn ở thư viện
        private void lvPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPlaylist.SelectedItems.Count > 0)
            {
                pnlAddPlaylist_SubMenu_TVien.Visible = false;
                ListViewItem lvi = lvPlaylist.SelectedItems[0];
                string title = lvi.SubItems[0].Text;
                title = title.Replace("&", "&&");
                string[] arrListStr = title.Split('\n');
                tenbhTVien = arrListStr[0];
                tenarTVien = arrListStr[1];
                mabhTVien = lvi.SubItems[2].Text;
                maarTVien = lvi.SubItems[3].Text;
                HienThiNutLike(mabhTVien, btnLike_ThuVien);
                ShowMenuBH_TVien();
                VietLoiChao();
                CapNhatCombobox();
                CapNhatComboBox_OnSelection(cboChonPlaylist_TVien, mabhTVien);
            }
        }

        private void btnAdd_ThuVien_Click(object sender, EventArgs e)
        {
            pnlAddPlaylist_SubMenu_TVien.Visible = !pnlAddPlaylist_SubMenu_TVien.Visible;
            VietLoiChao();
        }

        private void btnLike_ThuVien_Click(object sender, EventArgs e)
        {
            NhanNutThich(mabhTVien, sender as Button);
            if (maPlistHienThi == (username + "_Liked"))
            {
                LoadPlaylist(maPlistHienThi);
                HideMenuBH_TVien();
            }
            VietLoiChao();
        }

        private void txtTrangChu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                if ((sender as TextBox).Text.Length == 0)
                    txtTemp.Visible = true;
            }
            else
                txtTemp.Visible = false;
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
                (sender as TextBox).Clear();
            VietLoiChao();
        }
        //ô tìm kiếm trống sẽ hiển thị dòng chữ ẩn
        //nhấn Esc sẽ xóa trắng ô tìm kiếm

        private void cboChonPlaylist_Khac_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPlaylist(maPlistHienThi = 
                (cboChonPlaylist_Khac.SelectedItem as dynamic).Khoa.ToString());
            LoadCombobox(cboChonPlaylist_Khac);
            cboChonPlaylist_Khac.Items.RemoveAt(cboChonPlaylist_Khac.FindStringExact(tenPlist));
            VietLoiChao();
        }

        private void lvPlaylist_DoubleClick(object sender, EventArgs e)
        {
            if (maPlistHienThi != (username + "_Liked"))
                PlayNhac(mabhTVien, tenbhTVien, tenarTVien, $"Playlist: {tenPlist}", maarTVien);
            else
                PlayNhac(mabhTVien, tenbhTVien, tenarTVien, "Bài hát đã thích", maarTVien);
            VietLoiChao();
        }

        private void btnPlay_ThuVien_Click(object sender, EventArgs e)
        {
            if (maPlistHienThi != (username + "_Liked"))
                PlayNhac(mabhTVien, tenbhTVien, tenarTVien, $"Playlist: {tenPlist}", maarTVien);
            else
                PlayNhac(mabhTVien, tenbhTVien, tenarTVien, "Bài hát đã thích", maarTVien);

            VietLoiChao();
        }

        private void btnXoabhKhoiPl_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Xóa bài hát khỏi playlist này?", "meMUSIC",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                CT_Playlist dbDelete = contextmeMUSIC.CT_Playlist
                    .Where(x => x.MaPlaylist == maPlistHienThi &&
                                x.MaBaiHat == mabhTVien).First();
                contextmeMUSIC.CT_Playlist.Remove(dbDelete);
                contextmeMUSIC.SaveChanges();
                HideMenuBH_TVien();
                LoadPlaylist(maPlistHienThi);
                MessageBox.Show("Đã xóa khỏi playlist.", "meMUSIC",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            VietLoiChao();
        }

        private void XoaPlaylist(int countSongPlist, string maPlistHienThi)
        {
            Playlist dbDelete = contextmeMUSIC.Playlists
                    .Where(p => p.MaPlaylist == maPlistHienThi).First();
            if (countSongPlist != 0)
            {
                List<CT_Playlist> listCTPlDelete = contextmeMUSIC.CT_Playlist
                    .Where(x => x.MaPlaylist == maPlistHienThi).ToList();
                foreach (var x in listCTPlDelete)
                {
                    contextmeMUSIC.CT_Playlist.Remove(x);
                }
            }
            contextmeMUSIC.Playlists.Remove(dbDelete);
            contextmeMUSIC.SaveChanges();
            CapNhatCombobox();
            MessageBox.Show($"Đã xóa playlist.",
                "meMUSIC", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có chắc muốn xóa playlist này?", "meMUSIC",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                int tempcount = countSongPlist;
                string temp = maPlistHienThi;
                btnLikedPlaylist_Click(sender, e);
                XoaPlaylist(tempcount,temp);
                if (flagPlayTVien == temp)
                {
                    if (flagPlayAll == 1)
                    {
                        tenbhFlowText = "Playlist đã xóa";
                        tenarFlowText = "meMUSIC © version 1.0";
                        tenalbFlowText = "";
                    }
                    else
                        tenalbFlowText = "Playlist đã xóa";
                    if (lblChayChu.Visible)
                    {
                        wmpPlayNhac.Ctlcontrols.play();
                        wmpPlayNhac.Ctlcontrols.pause();
                    }
                }
            }
            VietLoiChao();
        }

        private void btnDoiTen_Click(object sender, EventArgs e)
        {
            pnlDoiTenPlaylist.Visible = !pnlDoiTenPlaylist.Visible;
            txtDoiTenPlaylist.Focus();
            txtDoiTenPlaylist.SelectionStart = txtDoiTenPlaylist.Text.Length;
            VietLoiChao();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VietLoiChao();
        }

        private void wmpPlayNhac_Enter(object sender, EventArgs e)
        {
            VietLoiChao();
        }

        private void cboChonPlaylist_Xoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có chắc muốn xóa playlist này?", "meMUSIC",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                int tempcount;
                string maPlXoa = (cboChonPlaylist_Xoa.SelectedItem as dynamic).Khoa;
                if (maPlXoa != maPlistHienThi)
                {
                    LoadPlaylist(maPlXoa);
                    tempcount = countSongPlist;
                    LoadPlaylist(maPlistHienThi);
                    XoaPlaylist(tempcount, maPlXoa);
                }
                else
                {
                    tempcount = countSongPlist;
                    btnLikedPlaylist_Click(sender, e);
                    XoaPlaylist(tempcount, maPlXoa);
                    if (flagPlayTVien == maPlXoa)
                    {
                        if (flagPlayAll == 1)
                        {
                            tenbhFlowText = "Playlist đã xóa";
                            tenarFlowText = "meMUSIC © version 1.0";
                            tenalbFlowText = "";
                        }
                        else
                            tenalbFlowText = "Playlist đã xóa";
                        if (lblChayChu.Visible)
                        {
                            wmpPlayNhac.Ctlcontrols.play();
                            wmpPlayNhac.Ctlcontrols.pause();
                        }
                    }
                }
            }
            VietLoiChao();
        }

        private void cboChonPlaylist_TVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choncbo_AddPlaylist(sender as ComboBox, mabhTVien);
            pnlAddPlaylist_SubMenu_TVien.Visible = false;
            VietLoiChao();
        }

        private void txtTemp_Click(object sender, EventArgs e)
        {
            if (txtTrangChu.Visible)
            {
                txtTrangChu.Focus();
                txtTrangChu.Select(0, 0);
            }
            if (txtThuVien.Visible)
            {
                txtThuVien.Focus();
                txtThuVien.Select(0, 0);
            }
            else
            {
                txtCollection.Focus();
                txtCollection.Select(0, 0);
            }
            txtTemp.Visible = false;
            VietLoiChao();
        }
        //click vào ô tìm kiếm sẽ ẩn dòng chữ ẩn

        private void txtThuVien_Click(object sender, EventArgs e)
        {
            VietLoiChao();
        }

        private void btnGenre_Back_Click(object sender, EventArgs e)
        {
            ChuyenTabTrChu();
            TabHistory.Add(0);
            VietLoiChao();
        }

        private void btnGenre_TVien_Click(object sender, EventArgs e)
        {
            ChuyenTabTVien();
            TabHistory.Add(1);
            VietLoiChao();
        }

        private void btnHotGenres_Click(object sender, EventArgs e)
        {
            ChuyenTabGenre();
            TabHistory.Add(2);
            VietLoiChao();
        }

        private void btnHotArtists_Click(object sender, EventArgs e)
        {
            ChuyenTabArtist();
            TabHistory.Add(3);
            VietLoiChao();
        }

        private void btnAr0_JB_Click(object sender, EventArgs e)
        {
            if((sender as Button).Name == "btnAr0_JB")
            {
                GoiTabCon_XemArtist("JB");
                XemArtistHistory.Add("JB");
            }
            if ((sender as Button).Name == "btnAr1_BTS")
            {
                GoiTabCon_XemArtist("BTS");
                XemArtistHistory.Add("BTS");
            }
            if ((sender as Button).Name == "btnAr2_ST")
            {
                GoiTabCon_XemArtist("MTP");
                XemArtistHistory.Add("MTP");
            }
            if ((sender as Button).Name == "btnAr3_CRJ")
            {
                GoiTabCon_XemArtist("CRJ");
                XemArtistHistory.Add("CRJ");
            }
            if ((sender as Button).Name == "btnAr4_TS")
            {
                GoiTabCon_XemArtist("TS");
                XemArtistHistory.Add("TS");
            }
            if ((sender as Button).Name == "btnAr5_BE")
            {
                GoiTabCon_XemArtist("BE");
                XemArtistHistory.Add("BE");
            }
            if ((sender as Button).Name == "btnAr6_IU")
            {
                GoiTabCon_XemArtist("IU");
                XemArtistHistory.Add("IU");
            }
            if ((sender as Button).Name == "btnAr7_Heize")
            {
                GoiTabCon_XemArtist("Heize");
                XemArtistHistory.Add("Heize");
            }
            pnlArtist_SideMenu.Visible = true;
            TabHistory.Add(5);
            VietLoiChao();
        }
        //các nút hình nghệ sĩ ở menu hot artist

        private void cboArtistChonNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tenAr = cboArtistChonNhanh.SelectedItem.ToString();
                if (pnlXemArtist.Visible && tenAr == tenar_XemArtist)
                    throw new Exception("Bạn đang xem nghệ sĩ này rồi.");
                switch (cboArtistChonNhanh.SelectedIndex)
                {
                    case 0:
                        GoiTabCon_XemArtist("JB"); XemArtistHistory.Add("JB");
                        break;
                    case 1:
                        GoiTabCon_XemArtist("BTS"); XemArtistHistory.Add("BTS");
                        break;
                    case 2:
                        GoiTabCon_XemArtist("MTP"); XemArtistHistory.Add("MTP");
                        break;
                    case 3:
                        GoiTabCon_XemArtist("CRJ"); XemArtistHistory.Add("CRJ");
                        break;
                    case 4:
                        GoiTabCon_XemArtist("TS"); XemArtistHistory.Add("TS");
                        break;
                    case 5:
                        GoiTabCon_XemArtist("BE"); XemArtistHistory.Add("BE");
                        break;
                    case 6:
                        GoiTabCon_XemArtist("IU"); XemArtistHistory.Add("IU");
                        break;
                    case 7:
                        GoiTabCon_XemArtist("Heize"); XemArtistHistory.Add("Heize");
                        break;
                }
                pnlArtist_SideMenu.Visible = true;
                if (TabHistory.Last() == 3)
                    TabHistory.Add(5);
                if (TabHistory.Last() == 11)
                    TabHistory.Add(12);
                else
                    TabHistory.Add(6);
                VietLoiChao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "meMUSIC",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GoiTabCon_Collection(string tieude, string maGenre, int stt)
        {
            GoiTabCon(pnlCollection);
            SetUpGiaoDien_Collection(tieude, stt);
            pnlGenre_SideMenu.Visible = true;

            switch (stt)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    LoadCollectionTheoTL1(maGenre);
                    break;
                case 4:
                    LoadCollectionTheoIndie();
                    break;
                case 11:
                    LoadCollectionQTBH(maGenre);
                    break;
                default:
                    LoadCollectionTheoTL2(maGenre);
                    break;
            }

            VietLoiChao();
            CapNhatCombobox();
            txtTemp.Enabled = true;
            txtThuVien.Enabled = true;
            txtTrangChu.Enabled = true;
            txtCollection.Enabled = true;
            txtCollection.Visible = true;
            txtCollection.Focus();
            txtCollection.SelectionStart = txtCollection.Text.Length;
            txtTemp.Visible = false;
            txtTemp.Text = "Tìm kiếm trong Collection";
            if (txtCollection.Text.Length == 0)
                txtTemp.Visible = true;
            txtTrangChu.Visible = txtThuVien.Visible = false;
        }
        
        private void btn0_USUK_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "btn0_USUK")
                GoiTabCon_Collection(tieude_Collection = "US_UK Collection",
                    maGenre = "U", stt_Collection = 0);
            if ((sender as Button).Name == "btn1_KPop")
                GoiTabCon_Collection(tieude_Collection = "K-Pop Collection",
                    maGenre = "K", stt_Collection = 1);
            if ((sender as Button).Name == "btn2_VPop")
                GoiTabCon_Collection(tieude_Collection = "V-Pop Collection",
                    maGenre = "V", stt_Collection = 2);
            if ((sender as Button).Name == "btn3_JPop")
                GoiTabCon_Collection(tieude_Collection = "J-Pop Collection",
                    maGenre = "J", stt_Collection = 3);
            if ((sender as Button).Name == "btn4_Indie")
                GoiTabCon_Collection(tieude_Collection = "Indie",
                    maGenre = "I", stt_Collection = 4);
            if ((sender as Button).Name == "btn5_Rap")
                GoiTabCon_Collection(tieude_Collection = "Rap",
                    maGenre = "R", stt_Collection = 5);
            if ((sender as Button).Name == "btn6_EDM")
                GoiTabCon_Collection(tieude_Collection = "EDM",
                    maGenre = "E", stt_Collection = 6);
            if ((sender as Button).Name == "btn7_Dance")
                GoiTabCon_Collection(tieude_Collection = "Dance",
                    maGenre = "D", stt_Collection = 7);
            if ((sender as Button).Name == "btn8_YOLO")
                GoiTabCon_Collection(tieude_Collection = "YOLO",
                    maGenre = "Y", stt_Collection = 8);
            if ((sender as Button).Name == "btn9_Love")
                GoiTabCon_Collection(tieude_Collection = "Love Songs",
                    maGenre = "L", stt_Collection = 9);
            if ((sender as Button).Name == "btn10_Mood")
                GoiTabCon_Collection(tieude_Collection = "Mood",
                    maGenre = "S", stt_Collection = 10);
            if ((sender as Button).Name == "btn11_QTBH")
                GoiTabCon_Collection(tieude_Collection = "Nhạc quốc tế bất hủ",
                    maGenre = "QTBH", stt_Collection = 11);
            HotGenreCollection temp = new HotGenreCollection()
            {
                tieude = tieude_Collection,
                maGenre = maGenre,
                stt = stt_Collection
            };
            CollectionHistory.Add(temp);
            TabHistory.Add(13);
        }
        //các nút hình thể loại ở menu hot genre

        private void cboChonTheloai_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                int cboIndex = cboChonTheloai.SelectedIndex;
                if (pnlCollection.Visible && stt_Collection == cboIndex)
                    throw new Exception("Bạn đang mở bộ sưu tập này rồi.");
                switch (cboChonTheloai.SelectedIndex)
                {
                    case 0:
                        GoiTabCon_Collection(tieude_Collection = "US_UK Collection",
                            maGenre = "U", stt_Collection = 0);
                        break;
                    case 1:
                        GoiTabCon_Collection(tieude_Collection = "K-Pop Collection",
                            maGenre = "K", stt_Collection = 1);
                        break;
                    case 2:
                        GoiTabCon_Collection(tieude_Collection = "V-Pop Collection",
                            maGenre = "V", stt_Collection = 2);
                        break;
                    case 3:
                        GoiTabCon_Collection(tieude_Collection = "J-Pop Collection",
                            maGenre = "J", stt_Collection = 3);
                        break;
                    case 4:
                        GoiTabCon_Collection(tieude_Collection = "Indie",
                            maGenre = "I", stt_Collection = 4);
                        break;
                    case 5:
                        GoiTabCon_Collection(tieude_Collection = "Rap",
                            maGenre = "R", stt_Collection = 5);
                        break;
                    case 6:
                        GoiTabCon_Collection(tieude_Collection = "EDM",
                            maGenre = "E", stt_Collection = 6);
                        break;
                    case 7:
                        GoiTabCon_Collection(tieude_Collection = "Dance",
                            maGenre = "D", stt_Collection = 7);
                        break;
                    case 8:
                        GoiTabCon_Collection(tieude_Collection = "YOLO",
                            maGenre = "Y", stt_Collection = 8);
                        break;
                    case 9:
                        GoiTabCon_Collection(tieude_Collection = "Love Songs",
                            maGenre = "L", stt_Collection = 9);
                        break;
                    case 10:
                        GoiTabCon_Collection(tieude_Collection = "Mood",
                            maGenre = "S", stt_Collection = 10);
                        break;
                    case 11:
                        GoiTabCon_Collection(tieude_Collection = "Nhạc quốc tế bất hủ",
                            maGenre = "QTBH", stt_Collection = 11);
                        break;
                }
                HotGenreCollection temp = new HotGenreCollection()
                {
                    tieude = tieude_Collection,
                    maGenre = maGenre,
                    stt = stt_Collection
                };
                CollectionHistory.Add(temp);
                if (TabHistory.Last() == 13)
                    TabHistory.Add(14);
                else
                    TabHistory.Add(13);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "meMUSIC",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChuyenTabGenre()
        {
            ChuyenTab(pnlGenre_Main,pnlGenre_SideMenu);
            btnBack_Genre.Visible = true;
            VietLoiChao();
            if (txtTemp.Visible)
                txtTemp.Enabled = false;
            txtThuVien.Enabled = false;
            txtTrangChu.Enabled = false;
            txtCollection.Enabled = false;
        }

        private void ChuyenTabArtist()
        {
            ChuyenTab(pnlHotArtist_Main,pnlArtist_SideMenu);
            btnBack_HotArtist.Visible = true;
            VietLoiChao();
            if (txtTemp.Visible)
                txtTemp.Enabled = false;
            txtThuVien.Enabled = false;
            txtTrangChu.Enabled = false;
            txtCollection.Enabled = false;
        }

        int flagPlayAll = 0;//playlist hiện tại là nhiều bài
        string flagPlayTVien = "";//lưu mã playlist đang phát, trống nếu k phải ở thư viện
        private void btnPlayAll_TVien_Click(object sender, EventArgs e)
        {
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            var myPlayList = wmpPlayNhac.playlistCollection.newPlaylist("MyPlayList");
            for (int i = 0; i < lvPlaylist.Items.Count; i++)
            {
                string temp = lvPlaylist.Items[i].SubItems[2].Text;
                var mediaItem = wmpPlayNhac.newMedia($@"D:\meMUSIC\{temp}.mp3");
                myPlayList.appendItem(mediaItem);
                BaiHat dbUpdate = contextmeMUSIC.BaiHats.First(x => x.MaBaiHat == temp);
                dbUpdate.LuotNghe++;
            }
            wmpPlayNhac.currentPlaylist = myPlayList;
            if (maPlistHienThi != (username + "_Liked"))
                ChayChu($"Playlist: {tenPlist}", $"của {hoUser} {tenUser}", $"Ngày tạo: {ngayPlist}");
            else
                ChayChu($"Bài hát đã thích", $"của {hoUser} {tenUser}", "");
            flagPlayAll = 1; flagPlayTVien = maPlistHienThi;
            contextmeMUSIC.SaveChanges();
            VietLoiChao();
        }

        private void btnXemAlbum_TrChu_Click(object sender, EventArgs e)
        {
            GoiTabCon_XemAlbum(maalb);
            pnlSideMenu_TrChu.Visible = true;
            btnXemAlbum_TrChu.Visible = false;
            BaiHatInfo temp = new BaiHatInfo()
            {
                mabh = mabh,
                tenbh = tenbh,
                tenar = tenar,
                tenalb = tenalb,
                maalb = maalb,
                topSearch = "BÀI HÁT ĐÃ XEM"
            };
            XemAlbumHistory.Add(maalb);
            if (TabHistory.Count() > 0)
            {
                if (TabHistory.Last() == 0)
                //trchủ -> xem album
                //ý nghĩa: vào trang chủ và chọn bài hát
                {
                    bhInfoHistory.Add(temp);
                    TabHistory.Add(7);
                }
                else
                //trchủ -> xem artist(4) -> xem album
                //xem album(7) -> xem artist(8) -> xem album
                //xem album bên trong của artist(10) -> xem album
                //ý nghĩa: thông tin bài hát chọn ở trang chủ vẫn lưu từ đầu
                {
                    bhInfoHistory.Add(bhInfoHistory.Last());
                    TabHistory.Add(9);
                }
            }
            VietLoiChao();
        }

        private void BackButton_MouseEnter(Button thisBtn)
        {
            thisBtn.Text += " ❮❮";
            VietLoiChao();
        }
        //hiển thị mũi tên khi trỏ chuột vào các nút back về trang chủ hay thư viện

        private void btnTrChu_TVien_MouseEnter(object sender, EventArgs e)
        {
            BackButton_MouseEnter((sender as Button));
        }

        private void BackButton_MouseLeave(Button thisBtn, string thisBtnText)
        {
            thisBtn.Text = thisBtnText;
            VietLoiChao();
        }
        //khi trỏ chuột ra trở lại bình thường

        private void btnTrChu_TVien_MouseLeave(object sender, EventArgs e)
        {
            List<string> btnBackTVienName = new List<string> { "btnTrChu_TVien", "btnGenre_TVien", "btnArtist_TVien", "btnGoiY_TVien" };
            if(btnBackTVienName.Contains((sender as Button).Name))
                BackButton_MouseLeave(sender as Button,"    Thư viện của tôi");
            List<string> btnBackTrChuName = new List<string> { "btnThuVien_Back", "btnGenre_Back", "btnArtist_Back", "btnGoiY_Back" };
            if (btnBackTrChuName.Contains((sender as Button).Name))
                BackButton_MouseLeave(sender as Button, "    Về trang chủ");
            if ((sender as Button).Name == "btnGoiY")
                BackButton_MouseLeave(sender as Button, "    Dành cho bạn");
        }

        private void DropDownButton_MouseEnter(Panel thisSubMenu, Button thisBtn, string thisBtnText)
        {
            if (!thisSubMenu.Visible)
                thisBtn.Text = thisBtnText + " 🢃";
            else
                thisBtn.Text = thisBtnText + " 🢁";
            VietLoiChao();
        }
        //hiển thị khi trỏ chuột các nút menu xổ xuống

        private void btnTaoPlaylist_TrChu_MouseEnter(object sender, EventArgs e)
        {
            if((sender as Button).Name == "btnTaoPlaylist_TrChu")
                DropDownButton_MouseEnter(pnlSubMenu_NhapPlaylist_TrChu,
                    sender as Button, "    Tạo playlist mới");
            if ((sender as Button).Name == "btnTopCollection")
                DropDownButton_MouseEnter(pnlTopCollection_SubMenu,
                    sender as Button, "    Top Collections");
            if ((sender as Button).Name == "btnTatCaPlaylist")
                DropDownButton_MouseEnter(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Tất cả playlist");
            if ((sender as Button).Name == "btnTaoPlaylist_TVien")
                DropDownButton_MouseEnter(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Tạo playlist mới");
            if ((sender as Button).Name == "btnMenu_XoaPlaylist")
                DropDownButton_MouseEnter(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Xóa playlist");
            if ((sender as Button).Name == "btnTaoPlaylist_Genre")
                DropDownButton_MouseEnter(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Tạo playlist mới");
            if ((sender as Button).Name == "btnGenreChonNhanh")
                DropDownButton_MouseEnter(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Chọn nhanh");
            if ((sender as Button).Name == "btnTaoPlaylist_Artist")
                DropDownButton_MouseEnter(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Tạo playlist mới");
            if ((sender as Button).Name == "btnArtistChonNhanh")
                DropDownButton_MouseEnter(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Chọn nhanh");
        }

        private void DropDownButton_MouseLeave(Panel thisSubMenu, Button thisBtn, string thisBtnText)
        {
            if (!thisSubMenu.Visible)
                thisBtn.Text = thisBtnText;
            VietLoiChao();
        }

        private void btnTaoPlaylist_TrChu_MouseLeave(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "btnTaoPlaylist_TrChu")
                DropDownButton_MouseLeave(pnlSubMenu_NhapPlaylist_TrChu,
                    sender as Button, "    Tạo playlist mới");
            if ((sender as Button).Name == "btnTopCollection")
                DropDownButton_MouseLeave(pnlTopCollection_SubMenu,
                    sender as Button, "    Top Collections");
            if ((sender as Button).Name == "btnTatCaPlaylist")
                DropDownButton_MouseLeave(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Tất cả playlist");
            if ((sender as Button).Name == "btnTaoPlaylist_TVien")
                DropDownButton_MouseLeave(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Tạo playlist mới");
            if ((sender as Button).Name == "btnMenu_XoaPlaylist")
                DropDownButton_MouseLeave(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Xóa playlist");
            if ((sender as Button).Name == "btnTaoPlaylist_Genre")
                DropDownButton_MouseLeave(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Tạo playlist mới");
            if ((sender as Button).Name == "btnGenreChonNhanh")
                DropDownButton_MouseLeave(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Chọn nhanh");
            if ((sender as Button).Name == "btnTaoPlaylist_Artist")
                DropDownButton_MouseLeave(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Tạo playlist mới");
            if ((sender as Button).Name == "btnArtistChonNhanh")
                DropDownButton_MouseLeave(pnlTatCaPlaylist_SubMenu,
                    sender as Button, "    Chọn nhanh");
        }

        private void trChuToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (TabHistory.Last() != 0)
                {
                    ChuyenTabTrChu();
                    TabHistory.Add(0);
                }    
        }

        private void thuVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabHistory.Last() != 1)
            {   
                ChuyenTabTVien();
                TabHistory.Add(1);
            }
        }

        private void genreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabHistory.Last() != 2)
            {     
                ChuyenTabGenre();
                TabHistory.Add(2);
            }
        }

        private void artistToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (TabHistory.Last() != 3)
                {
                    ChuyenTabArtist();
                    TabHistory.Add(3);
                }
        }

        private void XacNhanUpdatePlaylist(TextBox txt, int flagAction)
        {
            List<Playlist> listPlaylist = contextmeMUSIC.Playlists.ToList();
            try
            {
                if (txt.Text == "")
                    throw new Exception("Tên playlist không được để trống");
                foreach (var x in listPlaylist)
                {
                    if (x.username == username)
                        if (x.TenPlaylist == txt.Text)
                            throw new Exception("Bạn đã có playlist đặt tên này rồi.");
                }
                string notify;
                if (flagAction == 0)
                {
                    Playlist p = new Playlist()
                    {
                        MaPlaylist = username + "_" + String.Format("{0:s}", DateTime.Now),
                        TenPlaylist = txt.Text,
                        username = username,
                        NgayPlaylist = DateTime.Now
                    };
                    contextmeMUSIC.Playlists.Add(p);
                    notify = "Đã thêm playlist mới.\nChọn bài hát và thêm vào ngay nào!";
                }
                else
                {
                    Playlist dbUpdate = contextmeMUSIC.Playlists
                        .Where(x => x.MaPlaylist == maPlistHienThi).First();
                    dbUpdate.TenPlaylist = txt.Text;
                    if (flagPlayTVien == maPlistHienThi)
                    {
                        if (flagPlayAll == 0)
                            tenalbFlowText = $"Playlist: {txt.Text}";
                        else
                            tenbhFlowText = $"Playlist: {txt.Text}";
                        if (lblChayChu.Visible)
                        {
                            wmpPlayNhac.Ctlcontrols.play();
                            wmpPlayNhac.Ctlcontrols.pause();
                        }
                    }
                    notify = "Đã đổi tên playlist.";
                    LoadPlaylist(maPlistHienThi = username + "_Liked");
                }
                txt.Clear();
                contextmeMUSIC.SaveChanges();
                CapNhatCombobox();
                MessageBox.Show(notify, "meMUSIC",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "meMUSIC",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnOK_Artist_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "btnOK_Artist")
                XacNhanUpdatePlaylist(txtNhapPlaylist_Artist, 0);
            if ((sender as Button).Name == "btnOK_Genre")
                XacNhanUpdatePlaylist(txtNhapPlaylist_Genre, 0);
            if ((sender as Button).Name == "btnOK_TrChu")
                XacNhanUpdatePlaylist(txtNhapPlaylist_TrChu, 0);
            if ((sender as Button).Name == "btnOK_TVien")
                XacNhanUpdatePlaylist(txtNhapPlaylist_TVien, 0);
            if ((sender as Button).Name == "btnOK_DoiTen")
                XacNhanUpdatePlaylist(txtDoiTenPlaylist, 1);
        }

        private void btnClearTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTrangChu.Visible)
            {
                txtTrangChu.Clear();
                txtTrangChu.Focus();
            }
            if (txtCollection.Visible)
            {
                txtCollection.Clear();
                txtCollection.Focus();
            }
            else
            {
                txtThuVien.Clear();
                txtThuVien.Focus();
            }
        }

        private void NhanNutBack(int lastTabIndex)
        {
            switch (lastTabIndex)
            {
                case 0:
                    ChuyenTabTrChu(); break;
                case 1:
                    ChuyenTabTVien(); break;
                case 2:
                    ChuyenTabGenre(); break;
                case 3:
                    ChuyenTabArtist(); break;
                case 15:
                    ChuyenTabGoiY(); break;
                case 4: case 8:
                    BackXemArtistHistory();
                    BaiHatInfo tempar = bhInfoHistory.Last();
                    pnlSideMenu_TrChu.Visible = true;
                    if (flagInfo == 1)
                    {
                        ShowInfoPanel(tempar.mabh,tempar.tenbh, tempar.tenar,
                            tempar.tenalb, tempar.maalb, tempar.topSearch);
                        btnXemArtist.Visible = false;
                    }
                    break;
                case 5: case 6: case 12:
                    BackXemArtistHistory();
                    pnlArtist_SideMenu.Visible = true;
                    break;
                case 16:
                    BackXemArtistHistory();
                    pnlGoiY_SideMenu.Visible = true;
                    break;
                case 7: case 9: case 10:
                    GoiTabCon_XemAlbum(XemAlbumHistory.Last());
                    BaiHatInfo tempalb = bhInfoHistory.Last();
                    pnlSideMenu_TrChu.Visible = true;
                    if (flagInfo == 1)
                    {
                        ShowInfoPanel(tempalb.mabh, tempalb.tenbh, tempalb.tenar,
                            tempalb.tenalb, tempalb.maalb, tempalb.topSearch);
                        if(tempalb.maalb == XemAlbumHistory.Last())
                            btnXemAlbum_TrChu.Visible = false;
                    }
                    break;
                case 11:
                    GoiTabCon_XemAlbum(XemAlbumHistory.Last());
                    pnlArtist_SideMenu.Visible = true;
                    break;
                case 17:
                    GoiTabCon_XemAlbum(XemAlbumHistory.Last());
                    pnlGoiY_SideMenu.Visible = true;
                    break;
                case 13: case 14:
                    BackCollectionHistory();
                    pnlGenre_SideMenu.Visible = true;
                    break;
            }
        }
        //load lại các tab trong tabhistory với các thông tin đã lưu trong các list history(bài hát, artist, album, collection)
        
        private void btnBack_XemArtist_Click(object sender, EventArgs e)
        {
            if((TabHistory.Last() > 3 && TabHistory.Last() < 7) ||
                TabHistory.Last() == 8 || TabHistory.Last() == 12 || 
                TabHistory.Last() == 16)//trang xem artist
            {
                XemArtistHistory.RemoveAt(XemArtistHistory.LastIndexOf(XemArtistHistory.Last()));
                if(TabHistory.Last() == 4 || TabHistory.Last() == 8)//giao diện trang chủ
                    bhInfoHistory.RemoveAt(bhInfoHistory.LastIndexOf(bhInfoHistory.Last()));
            }
            if (TabHistory.Last() == 7 || TabHistory.Last() == 17 || 
                (TabHistory.Last() > 8 && TabHistory.Last() < 12))//trang xem album
            {
                XemAlbumHistory.RemoveAt(XemAlbumHistory.Count - 1);
                if (TabHistory.Last() < 10)//bấm nút gọi tab từ bên side menu
                    bhInfoHistory.RemoveAt(bhInfoHistory.Count - 1);
            }
            if (TabHistory.Last() > 12 && TabHistory.Last() < 15)//trang xem hot genre
            {
                CollectionHistory.RemoveAt(CollectionHistory.Count - 1);
            }
            if (TabHistory.Last() != 6 && TabHistory.Last() != 14)
            //nếu k phải 2 tab artist liên tục gọi từ hot artist hoặc hot genre
            {
                TabHistory.RemoveAt(TabHistory.LastIndexOf(TabHistory.Last()));
                if (TabHistory.Last() == 6)
                //nếu tab kế cuối vẫn là artist đã gọi liên tục từ hot artist
                //thì add lại 1 tab (6) để ko bị xóa dư 1 tab khi trigger case NhanNutBack(6)
                {
                    TabHistory.Add(6);
                }
                if (TabHistory.Last() == 14)
                //tương tự cho hot genre
                {
                    TabHistory.Add(14);
                }
            }
            if (TabHistory.Count == 1)
            {
                ChuyenTabTrChu();
                btnBack_TrChu.Visible = false;
            }
            else
                NhanNutBack(TabHistory.Last());
        }

        string mabh_xemArtist, tenbh_xemArtist, namBH, maalb_xemArtist;//lưu thông tin bài ở trang xem artist
        
        private void lvXemArtist_BaiHat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvXemArtist_BaiHat.SelectedItems.Count > 0)
            {
                ListViewItem lviBH = lvXemArtist_BaiHat.SelectedItems[0];
                string title = lviBH.SubItems[0].Text;
                title = title.Replace("&", "&&");
                string[] arrListStr = title.Split('\n');
                tenbh_xemArtist = arrListStr[0];
                namBH = arrListStr[1];
                mabh_xemArtist = lviBH.SubItems[2].Text;
                btnXemAlbum_XemArtist.Enabled = false;
                pnlAdd_XemArtist.Enabled = false;
                HienThiNutLike(mabh_xemArtist,btnLike_XemArtist);
                btnLike_XemArtist.Enabled = true;
                btnPlay_XemArtist.Enabled = true;
                btnAdd_XemArtist.Enabled = true;
                VietLoiChao();
                LoadCombobox(cboChonPlaylist_XemArtist);
                CapNhatComboBox_OnSelection(cboChonPlaylist_XemArtist, mabh_xemArtist);
            }
        }

        private void btnPlay_XemArtist_Click(object sender, EventArgs e)
        {
            PlayNhac(mabh_xemArtist, tenbh_xemArtist,
                    tenar_XemArtist, namBH, maar_XemArtist);
            VietLoiChao();
        }

        private void lvXemArtist_BaiHat_DoubleClick(object sender, EventArgs e)
        {
            PlayNhac(mabh_xemArtist, tenbh_xemArtist,
                    tenar_XemArtist, namBH, maar_XemArtist);
            VietLoiChao();
        }

        private void btnLike_XemArtist_Click(object sender, EventArgs e)
        {
            NhanNutThich(mabh_xemArtist, sender as Button);
            VietLoiChao();
        }

        private void btnPlayAll_XemAlbum_Click(object sender, EventArgs e)
        {
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            var myPlayList = wmpPlayNhac.playlistCollection.newPlaylist("MyPlayList");
            for (int i = 0; i < lvXemAlbum.Items.Count; i++)
            {
                string temp = lvXemAlbum.Items[i].SubItems[2].Text;
                var mediaItem = wmpPlayNhac.newMedia($@"D:\meMUSIC\{temp}.mp3");
                myPlayList.appendItem(mediaItem);
                BaiHat dbUpdate = contextmeMUSIC.BaiHats.First(x => x.MaBaiHat == temp);
                dbUpdate.LuotNghe++;
            }
            wmpPlayNhac.currentPlaylist = myPlayList;
            ChayChu("Tất cả bài hát", $"Album: {tenalb_xemAlbum}", tenar_xemAlbum);
            flagPlayAll = 1; flagPlayTVien = "";
            contextmeMUSIC.SaveChanges();
            VietLoiChao();
        }

        private void btnLike_XemAlbum_Click(object sender, EventArgs e)
        {
            NhanNutThich(mabh_xemAlbum, sender as Button);
            VietLoiChao();
        }

        private void btnAddPlaylist_XemAlbum_Click(object sender, EventArgs e)
        {
            pnlAddPlaylist_SubMenu_XemAlbum.Enabled = true;
        }

        private void cboChonPlaylist_XemAlbum_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choncbo_AddPlaylist(cboChonPlaylist_XemAlbum, mabh_xemAlbum);
            VietLoiChao();
        }

        private void lvXemAlbum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvXemAlbum.SelectedItems.Count > 0)
            {
                ListViewItem lviBH = lvXemAlbum.SelectedItems[0];
                tenbh_xemAlbum = lviBH.SubItems[0].Text;
                mabh_xemAlbum = lviBH.SubItems[2].Text;
                pnlAddPlaylist_SubMenu_XemAlbum.Enabled = false;
                HienThiNutLike(mabh_xemAlbum, btnLike_XemAlbum);
                btnLike_XemAlbum.Enabled = true;
                btnPlay_XemAlbum.Enabled = true;
                btnAddPlaylist_XemAlbum.Enabled = true;
                VietLoiChao();
                LoadCombobox(cboChonPlaylist_XemAlbum);
                CapNhatComboBox_OnSelection(cboChonPlaylist_XemAlbum,mabh_xemAlbum);
            }
        }

        private void btnLike_Collection_Click(object sender, EventArgs e)
        {
            NhanNutThich(maBH_Collection, sender as Button);
            VietLoiChao();
        }

        private void btnAddPlaylist_Collection_Click(object sender, EventArgs e)
        {
            pnlAddPlaylist_SubMenu_Collection.Enabled = true;
            VietLoiChao();
        }

        private void ChuyenTabGoiY()
        {
            ChuyenTab(pnlGoiY_Main, pnlGoiY_SideMenu);
            SetUpGiaoDien_GoiY();
            if (txtTemp.Visible)
                txtTemp.Enabled = false;
            txtThuVien.Enabled = false;
            txtTrangChu.Enabled = false;
            txtCollection.Enabled = false;
        }

        private void XemArtistGoiY(string maar_GoiY)
        {
            GoiTabCon_XemArtist(maar_GoiY);
            pnlGoiY_SideMenu.Visible = true;
            XemArtistHistory.Add(maar_GoiY);
            TabHistory.Add(16);
        }

        private void pnlArtistGoiY1_Click(object sender, EventArgs e)
        {
            XemArtistGoiY(maar_GoiY1);
            VietLoiChao();
        }

        private void pboArtistGoiY1_Click(object sender, EventArgs e)
        {
            XemArtistGoiY(maar_GoiY1);
            VietLoiChao();
        }

        private void lblArtistGoiY1_Click(object sender, EventArgs e)
        {
            XemArtistGoiY(maar_GoiY1);
            VietLoiChao();
        }

        private void pnlArtistGoiY2_Click(object sender, EventArgs e)
        {
            XemArtistGoiY(maar_GoiY2);
            VietLoiChao();
        }

        private void pboArtistGoiY2_Click(object sender, EventArgs e)
        {
            XemArtistGoiY(maar_GoiY2);
            VietLoiChao();
        }

        private void lblArtistGoiY2_Click(object sender, EventArgs e)
        {
            XemArtistGoiY(maar_GoiY2);
            VietLoiChao();
        }

        private void pnlArtistGoiY3_Click(object sender, EventArgs e)
        {
            XemArtistGoiY(maar_GoiY3);
            VietLoiChao();
        }

        private void pboArtistGoiY3_Click(object sender, EventArgs e)
        {
            XemArtistGoiY(maar_GoiY3);
            VietLoiChao();
        }

        private void lblArtistGoiY3_Click(object sender, EventArgs e)
        {
            XemArtistGoiY(maar_GoiY3);
            VietLoiChao();
        }

        private void btnGoiY_Click(object sender, EventArgs e)
        {
            ChuyenTabGoiY();
            TabHistory.Add(15);
            VietLoiChao();
        }

        private void goiYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabHistory.Last() != 15)
            {
                ChuyenTabGoiY();
                TabHistory.Add(15);
            }
        }

        private void lvCollection_DoubleClick(object sender, EventArgs e)
        {
            PlayNhac(maBH_Collection, tenbh_Collection,
                    tenar_Collection, tieude_Collection, maAR_Collection);
            VietLoiChao();
        }

        private void cboChonPlaylist_Collection_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choncbo_AddPlaylist(cboChonPlaylist_Collection, maBH_Collection);
            pnlAddPlaylist_SubMenu_Collection.Enabled = false;
            VietLoiChao();
        }

        private void txtCollection_TextChanged(object sender, EventArgs e)
        {
            VietLoiChao();
            if (txtCollection.Text.Length == 0)
                txtTemp.Visible = true;
            btnAddPlaylist_Collection.Enabled = false;
            pnlAddPlaylist_SubMenu_Collection.Enabled = false;
            btnLike_Collection.Enabled = false;
            btnPlay_Collection.Enabled = false;
            switch (stt_Collection)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    TimCollectionTheoTL1();
                    break;
                case 4:
                    TimCollectionTheoIndie();
                    break;
                case 11:
                    TimCollectionQTBH();
                    break;
                default:
                    TimCollectionTheoTL2();
                    break;
            }
        }

        private void btnPlayAll_Collection_Click(object sender, EventArgs e)
        {
            if (lvCollection.Items.Count > 0)
            {
                List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
                var myPlayList = wmpPlayNhac.playlistCollection.newPlaylist("MyPlayList");

                for (int i = 0; i < lvCollection.Items.Count; i++)
                {
                    string temp = lvCollection.Items[i].SubItems[2].Text;
                    var mediaItem = wmpPlayNhac.newMedia($@"D:\meMUSIC\{temp}.mp3");
                    myPlayList.appendItem(mediaItem);
                    BaiHat dbUpdate = contextmeMUSIC.BaiHats.First(x => x.MaBaiHat == temp);
                    dbUpdate.LuotNghe++;
                }
                wmpPlayNhac.currentPlaylist = myPlayList;
                ChayChu(tieude_Collection, "Various Artists", "meMUSIC © version 1.0");
                flagPlayAll = 1; flagPlayTVien = "";
                contextmeMUSIC.SaveChanges();
            }
            VietLoiChao();
        }

        private void btnPlay_Collection_Click(object sender, EventArgs e)
        {
            PlayNhac(maBH_Collection, tenbh_Collection,
                    tenar_Collection, tieude_Collection, maAR_Collection);
            VietLoiChao();
        }

        private void lvCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvCollection.SelectedItems.Count > 0)
            {
                ListViewItem lviCol = lvCollection.SelectedItems[0];
                mabh = lviCol.SubItems[2].Text;
                string title = lviCol.SubItems[0].Text;
                title = title.Replace("&", "&&");
                string[] arrListStr = title.Split('\n');
                tenbh_Collection = arrListStr[0];
                tenar_Collection = arrListStr[1];
                maBH_Collection = lviCol.SubItems[2].Text;
                maAR_Collection = lviCol.SubItems[3].Text;
                pnlAddPlaylist_SubMenu_Collection.Enabled = false;
                HienThiNutLike(maBH_Collection, btnLike_Collection);
                btnLike_Collection.Enabled = true;
                btnPlay_Collection.Enabled = true;
                btnAddPlaylist_Collection.Enabled = true;
                LoadCombobox(cboChonPlaylist_Collection);
                CapNhatComboBox_OnSelection(cboChonPlaylist_Collection, maBH_Collection);
            }
        }

        private void lvXemAlbum_DoubleClick(object sender, EventArgs e)
        {
            PlayNhac(mabh_xemAlbum, tenbh_xemAlbum,
                    tenar_xemAlbum, $"Album: {tenalb_xemAlbum}", maar_xemAlbum);
            VietLoiChao();
        }

        private void btnPlay_XemAlbum_Click(object sender, EventArgs e)
        {
            PlayNhac(mabh_xemAlbum, tenbh_xemAlbum,
                    tenar_xemAlbum, $"Album: {tenalb_xemAlbum}", maar_xemAlbum);
            VietLoiChao();
        }

        private void lvXemArtist_Album_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvXemArtist_Album.SelectedItems.Count > 0)
            {
                ListViewItem lviAR = lvXemArtist_Album.SelectedItems[0];
                maalb_xemArtist = lviAR.SubItems[2].Text;
                btnLike_XemArtist.Enabled = false;
                btnPlay_XemArtist.Enabled = false;
                btnAdd_XemArtist.Enabled = false;
                pnlAdd_XemArtist.Enabled = false;
                btnXemAlbum_XemArtist.Enabled = true;
                VietLoiChao();
            }
        }

        private void btnPlayAll_XemArtist_Click(object sender, EventArgs e)
        {
            List<BaiHat> listBaiHat = contextmeMUSIC.BaiHats.ToList();
            var myPlayList = wmpPlayNhac.playlistCollection.newPlaylist("MyPlayList");
            for (int i = 0; i < lvXemArtist_BaiHat.Items.Count; i++)
            {
                string temp = lvXemArtist_BaiHat.Items[i].SubItems[2].Text;
                var mediaItem = wmpPlayNhac.newMedia($@"D:\meMUSIC\{temp}.mp3");
                myPlayList.appendItem(mediaItem);
                BaiHat dbUpdate = contextmeMUSIC.BaiHats.First(x => x.MaBaiHat == temp);
                dbUpdate.LuotNghe++;
            }
            wmpPlayNhac.currentPlaylist = myPlayList;
            ChayChu("Tất cả bài hát", tenar_XemArtist, "");
            flagPlayAll = 1; flagPlayTVien = "";
            contextmeMUSIC.SaveChanges();
            VietLoiChao();
        }

        private void btnXemAlbum_XemArtist_Click(object sender, EventArgs e)
        {
            GoiTabCon_XemAlbum(maalb_xemArtist);
            if (TabHistory.Last() == 4 || TabHistory.Last() == 8)
            //xem artist(4)(8)(giao diện trang chủ) -> xem album bên trong
            {
                BaiHatInfo tempalb = bhInfoHistory.Last();
                pnlSideMenu_TrChu.Visible = true;
                if (flagInfo == 1)
                {
                    ShowInfoPanel(tempalb.mabh, tempalb.tenbh, tempalb.tenar,
                    tempalb.tenalb, tempalb.maalb, tempalb.topSearch);
                    if (tempalb.maalb == maalb_xemArtist)
                        btnXemAlbum_TrChu.Visible = false;
                }
                XemAlbumHistory.Add(maalb_xemArtist);
                TabHistory.Add(10);
            }
            if (TabHistory.Last() == 5 || TabHistory.Last() == 6 ||
                TabHistory.Last() == 12)
            //xem artist(5)(6)(12)(giao diện hot artist) -> xem album bên trong
            {
                pnlArtist_SideMenu.Visible = true;
                XemAlbumHistory.Add(maalb_xemArtist);
                TabHistory.Add(11);
            }
            if (TabHistory.Last() == 16)
            //xem artist(16) giao diện gợi ý -> xem album bên trong
            {
                pnlGoiY_SideMenu.Visible = true;
                XemAlbumHistory.Add(maalb_xemArtist);
                TabHistory.Add(17);
            }
            VietLoiChao();
        }

        private void lvXemArtist_Album_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnAdd_XemArtist_Click(object sender, EventArgs e)
        {
            pnlAdd_XemArtist.Enabled = true;
        }

        private void cboChonPlaylist_XemArtist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choncbo_AddPlaylist(cboChonPlaylist_XemArtist, mabh_xemArtist);
            pnlAdd_XemArtist.Enabled = false;
            VietLoiChao();
        }

        private void txtNhapPlaylist_Genre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).Name == "txtDoiTenPlaylist")
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    XacNhanUpdatePlaylist(txtDoiTenPlaylist, 1);
            }
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    XacNhanUpdatePlaylist(sender as TextBox, 0);
            }
        }

        private void timerChayChu_Tick(object sender, EventArgs e)
        {
            lblChayChu.Text = lblChayChu.Text.Substring(1) + lblChayChu.Text.Substring(0, 1);
        }
        //làm dòng chữ chạy ở player

        private void wmpPlayNhac_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (lblChayChu.Visible)
                lblChayChu.Visible = false;
            lblChayChu.Text = $"                    ĐANG PHÁT:   {tenbhFlowText}   -  {tenarFlowText}   ";
            if (tenalbFlowText != "")
            {
                lblChayChu.Text += $"-   {tenalbFlowText}                    ";
            }
            if (wmpPlayNhac.playState == WMPLib.WMPPlayState.wmppsPlaying)
                lblChayChu.Visible = false;
            if (wmpPlayNhac.playState == WMPLib.WMPPlayState.wmppsPaused)
                lblChayChu.Visible = true;
            if (wmpPlayNhac.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                lblChayChu.Visible = true;
                lblChayChu.Text = chaychu;
            }
        }
        //khi nhạc pause sẽ hiển thị dòng chữ chạy,
        //play sẽ ẩn để ng dùng xem duoc thông tin bài hát (khi phát nhiều bài)

        private void btnTaoPlaylist_TVien_Click(object sender, EventArgs e)
        {
            if((sender as Button).Name == "btnTaoPlaylist_TVien")
            {
               ShowSubMenuTVien(pnlSubMenu_NhapPlaylist_TVien, sender as Button, "    Tạo playlist mới",
               pnlTatCaPlaylist_SubMenu, btnTatCaPlaylist, "    Tất cả playlist",
               pnlXoaPlaylist_SubMenu, btnMenu_XoaPlaylist, "    Xóa playlist");
                txtNhapPlaylist_TVien.Focus();
                txtNhapPlaylist_TVien.SelectionStart = txtNhapPlaylist_TVien.Text.Length;
            }
            if ((sender as Button).Name == "btnTatCaPlaylist") 
            {
                ShowSubMenuTVien(pnlTatCaPlaylist_SubMenu, sender as Button, "    Tất cả playlist",
                pnlSubMenu_NhapPlaylist_TVien, btnTaoPlaylist_TVien, "    Tạo playlist mới",
                pnlXoaPlaylist_SubMenu, btnMenu_XoaPlaylist, "    Xóa playlist");
            }
            if ((sender as Button).Name == "btnMenu_XoaPlaylist") 
            {
                ShowSubMenuTVien(pnlXoaPlaylist_SubMenu, sender as Button, "    Xóa playlist",
                pnlTatCaPlaylist_SubMenu, btnTatCaPlaylist, "    Tất cả playlist",
                pnlSubMenu_NhapPlaylist_TVien, btnTaoPlaylist_TVien, "    Tạo playlist mới");
            }
            VietLoiChao();
        }

        private void ChuyenTabTVien()
        {
            ChuyenTab(pnlTVien_MainPage,pnlSideMenu_TVien);
            btnBack_TVien.Visible = true;
            if (maPlistCapNhat.Contains(maPlistHienThi))
            {
                LoadPlaylist(maPlistHienThi);
                maPlistCapNhat.Clear();
            }
            txtTemp.Enabled = true;
            txtThuVien.Enabled = true;
            txtTrangChu.Enabled = true;
            txtCollection.Enabled = true;
            txtThuVien.Visible = true;
            txtThuVien.Focus();
            txtThuVien.SelectionStart = txtThuVien.Text.Length;
            txtTemp.Visible = false;
            txtTemp.Text = "Tìm kiếm trong playlist";
            if (txtThuVien.Text.Length == 0)
                txtTemp.Visible = true;
            txtTrangChu.Visible = txtCollection.Visible = false;
            CapNhatCombobox();
            VietLoiChao();
        }

        private void ShowMenuPhai()
        {
            btnPlay_TrChu.Visible = true;
            btnLike_TrChu.Visible = true;
            btnAdd_TrChu.Visible = true;
        }//các nút con ở trang chủ

        //trường hợp người dùng xem artist từ trang chủ thì back về
        //vẫn hiện đúng thông tin bài hát đã chọn
        private void ShowInfoPanel(string mabh, string tenbh, string tenar,
            string tenalb, string maalb, string topSearch)
        {
            flagInfo = 1;
            pnlInfo.Visible = true;
            btnBack_Info.Visible = true;
            lblTopSearch.Text = topSearch;
            lblTopSearch.Visible = true;
            lblTenBaiHat.Text = tenbh;
            lblTenBaiHat.Visible = true;
            lblTenArtist_TrChu.Text = tenar;
            lblTenArtist_TrChu.Visible = true;
            lblTenAlbum_TrChu.Text = tenalb;
            lblTenAlbum_TrChu.Visible = true;
            if (maalb != "")
            {
                pboBaiHatCover.Image = Image.FromFile($@"D:\meMUSIC_Album\{maalb}.png");
                btnXemAlbum_TrChu.Visible = true;
            }
            else
            {
                pboBaiHatCover.Image = Image.FromFile($@"D:\meMUSIC_Cover\{mabh}.png"); ;
                btnXemAlbum_TrChu.Visible = false;
            }
            pboBaiHatCover.Visible = true;
        }//menu info
        
        private void ShowMenuTrai()
        {
            btnXemArtist.Visible = true;
            lblSadFace.Visible = false;
        }//menu info

        private void HideMenuPhai()
        {
            btnPlay_TrChu.Visible = false;
            btnLike_TrChu.Visible = false;
            btnAdd_TrChu.Visible = false;
            pnlAddPlaylist_SubMenu_TrChu.Visible = false;
        }

        private void HideMenuTrai()
        {
            flagInfo = 0;
            pnlInfo.Visible = false;
            btnBack_Info.Visible = false;
            lblTopSearch.Visible = false;
            lblTenBaiHat.Visible = false;
            lblTenArtist_TrChu.Visible = false;
            lblTenAlbum_TrChu.Visible = false;
            btnXemArtist.Visible = false;
            btnXemAlbum_TrChu.Visible = false;
            pboBaiHatCover.Visible = false;
        }

        private void HienThiNutLike(string mabh, Button btnLike)
        {
            List<CT_Playlist> listCT_Playlist = contextmeMUSIC.CT_Playlist.ToList();
            string temp = username + "_Liked";
            int flag = 0;
            foreach (var x in listCT_Playlist)
            {
                if (x.MaPlaylist == temp)
                {
                    if (x.MaBaiHat == mabh)
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            if (flag != 1)
                btnLike.Text = "🤍";
            else
                btnLike.Text = "♥";
        }//cập nhật hình ảnh trái tim khi ng dùng chọn bài hát

        private void HideSubMenuTrChu()
        {
            //đặt flag để nhớ panel nào đang mở để mở lại khi nhấn btnBack
            if (pnlSubMenu_NhapPlaylist_TrChu.Visible)
                flagSubMenuTrChu = 1;
            if (pnlTopCollection_SubMenu.Visible)
                flagSubMenuTrChu = 2;

            pnlSubMenu_NhapPlaylist_TrChu.Visible = false;
            pnlTopCollection_SubMenu.Visible = false;
        }

        private void Show2SubMenu(Panel thisSubMenuTrChu, Button thisBtn, string thisBtnText,
            Panel otherSubMenuTrChu, Button otherBtn, string otherBtnText)
        //nhấn vào nút xổ xuống này thì nút xổ xuống kia xổ lên
        {
            if (otherSubMenuTrChu.Visible)
            {
                otherSubMenuTrChu.Visible = false;
                otherBtn.Text = otherBtnText;
            }
            if (!thisSubMenuTrChu.Visible)
            {
                thisSubMenuTrChu.Visible = true;
                thisBtn.Text = thisBtnText + " 🢁";
            }
            else
            {
                thisSubMenuTrChu.Visible = false;
                thisBtn.Text = thisBtnText + " 🢃";
            }
        }

        private void ShowSubMenuTVien(Panel thisSubMenuTrChu, Button thisBtn, string thisBtnText,
            Panel otherSubMenuTrChu, Button otherBtn, string otherBtnText,
            Panel otherSubMenuTrChu2, Button otherBtn2, string otherBtnText2)
        {
            if (otherSubMenuTrChu.Visible)
            {
                otherSubMenuTrChu.Visible = false;
                otherBtn.Text = otherBtnText;
            }
            if (otherSubMenuTrChu2.Visible)
            {
                otherSubMenuTrChu2.Visible = false;
                otherBtn2.Text = otherBtnText2;
            }
            if (!thisSubMenuTrChu.Visible)
            {
                thisSubMenuTrChu.Visible = true;
                thisBtn.Text = thisBtnText + " 🢁";
            }
            else
            {
                thisSubMenuTrChu.Visible = false;
                thisBtn.Text = thisBtnText + " 🢃";
            }
        }
        //tương tự nhưng menu thư viện có 3 nút xổ
    }
}
