using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DllWebBrowser;

namespace EmployerInfo
{
    public partial class frmM6 : Form
    {
        public frmM6()
        {
            InitializeComponent();
        }

        string host = "http://vieclam.24h.com.vn";
        string CategoryPagelink = "";
        int CategoryPagetotal = 0;
        DataTable dtListCategory = new DataTable();
        List<string> ArrayDetailLink = new List<string>();
        DataTable dt = new DataTable();
        CookieContainer cookieContainer = new CookieContainer();
        bool IsStop = false, IsRun = false, IsLogin=false;

        #region Function

        void Get_Category()
        {
            stt1.Visible = true;
            stt1.Text = "Đang lấy danh sách các danh mục...";
            Application.DoEvents();

            dtListCategory = new DataTable();
            dtListCategory.Columns.Add("Link");
            dtListCategory.Columns.Add("Name");
            dtListCategory.Columns.Add("IdCategory");
            dtListCategory.Columns.Add("CountItem");
            dtListCategory.Rows.Clear();


            string s = FuncHelp.GetSource(host + "/nha-tuyen-dung");
            s = FuncHelp.CutFromTo(s, "id='gate_nganhnghe_abc'", "id='viec_kinhnghiem_hocvan_mucluong'");

            while (s.IndexOf("nganhnghe_item") > 0)
            {
                s = FuncHelp.CutFrom(s, "nganhnghe_item");
                string temp = FuncHelp.CutFromTo(s, "<a href", "</a>");
                string strLink = FuncHelp.CutFromTo(temp, "='", "' class");
                temp = FuncHelp.CutFrom(temp, ">");
                string strName = FuncHelp.CutTo(temp, "<span").Trim();
                string strCount = FuncHelp.CutFromTo(temp, ">(", ")</span>");
                strName = WebUtility.HtmlDecode(strName);
                //vieclam.24h.com.vn/tim-kiem-ung-vien-nhanh/?hdn_nganh_nghe_cap1=?
                string strIdCategory = strLink.Substring(strLink.LastIndexOf("-c") + 2, strLink.Length - strLink.LastIndexOf("-c") - ".html".Length - 2);
                strLink = "/tim-kiem-ung-vien-nhanh/?hdn_nganh_nghe_cap1=" + strIdCategory;
                dtListCategory.Rows.Add(strLink, strName + " (" + strCount + ")",strIdCategory,strCount.Replace(",","").Replace(".",""));
            }

            if (dtListCategory.Rows.Count > 0)
            {
                
                dtListCategory.DefaultView.Sort = "Name ASC";
                dtListCategory = dtListCategory.DefaultView.ToTable();
                DataRow dr = dtListCategory.NewRow();
                dr["Link"] = "";
                dr["Name"] = "-- Chọn danh mục --";
                dtListCategory.Rows.InsertAt(dr, 0);

                cbxCategory.DataSource = dtListCategory;
                cbxCategory.DisplayMember = "Name";
                cbxCategory.ValueMember = "Link";
                cbxCategory.Enabled = true;
                stt1.Text = "Đã lấy được " + (dtListCategory.Rows.Count-1).ToString() + " danh mục.";
            }

        }

        void Get_CategoryOneLinkToPage(string link)
        {
            stt2.Visible = true;
            stt2.Text = "Đang lấy tổng số trang của danh mục...";
            Application.DoEvents();

            string s = FuncHelp.GetSource(host + link);
            s = FuncHelp.CutFromTo(s, "Kết quả tìm kiếm", "</span>").Replace("(", "").Replace(")", "").Replace(",","").Replace(".","");

            CategoryPagelink = link;
            CategoryPagetotal = Convert.ToInt32(s);
            CategoryPagetotal = Convert.ToInt32(CategoryPagetotal / 20) + (CategoryPagetotal % 20 > 0 ? 1 : 0);
            if (CategoryPagetotal > 1)
            {
                cbxMin.Items.Clear(); cbxMax.Items.Clear();
                for (int i = 1; i <= CategoryPagetotal; i++)
                {
                    cbxMin.Items.Add(i.ToString());
                    cbxMax.Items.Add(i.ToString());
                }
                cbxMin.SelectedIndex = 0;
                cbxMax.SelectedIndex = cbxMax.Items.Count - 1;
                stt2.Text = string.Format("Tổng số ứng viên thật: {0:##,##} Tổng trang: {1:##,##}",Convert.ToInt32(s),CategoryPagetotal);
            }
            else { MessageBox.Show("Danh mục này không có ứng viên nào!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }

        void Get_TotalLink()
        {
            stt2.Visible = true;
            stt2.Text = "Đang lấy các link của trang...";
            Application.DoEvents();

            int page_min = 1, page_max = CategoryPagetotal;
            if(cbxPageLimit.Checked)
            {
                page_min = int.Parse(cbxMin.SelectedItem.ToString());
                page_max = int.Parse(cbxMax.SelectedItem.ToString());
            }

            stt2.Visible = true;
            stt3.Visible = true;
            for(int i=page_min; i<=page_max; i++)
            {
                string link = host + CategoryPagelink + "&page=" + i.ToString();
                string s = FuncHelp.GetSource(link);
                bool IsBreak = false;
                while (s.IndexOf("title-blockjob-sub") > 0)
                {
                    if (IsStop) { IsBreak = true; break; }
                    if (cbxSlg.Checked && numMaxSlg.Value <= ArrayDetailLink.Count) { IsBreak = true; break; }
                    s = FuncHelp.CutFrom(s, "title-blockjob-sub");
                    string temp = FuncHelp.CutTo(s, "</span>");
                    string li = FuncHelp.CutFromTo(temp, "<a href='", "' class=");
                    li = li.Substring(li.LastIndexOf("id"), li.IndexOf(".html") - li.LastIndexOf("id"));
                    string da = FuncHelp.CutFromTo(temp, "Cập nhật: ", ")</i>").Trim();
                    DateTime dtime = DateTime.ParseExact(da, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (cbxOnlyDay.Checked && (dtpOnlyDay.Value.Date != dtime.Date))
                    {
                        if (dtime.Date >= dtpOnlyDay.Value.Date) { continue; }
                        else { IsBreak = true; break; }
                    }
                    ArrayDetailLink.Add(li);         
                }                
                (Application.OpenForms["frmMain"] as frmMain).SetPercentProcess(i, page_max);
                stt2.Text = string.Format("Đang lấy tổng số link của các trang {0}/{1}", i - page_min + 1, page_max - page_min + 1);
                stt3.Text = string.Format("Tổng số link đã nhận: {0}", ArrayDetailLink.Count);
                if (IsBreak) { break; }
                Application.DoEvents();
            }
        }

        void Get_DataLink(string link)
        {
            string url = host + "/trang-in-ho-so/nhan-vien-" + link + ".html";

            string s = FuncHelp.GetSourceWithCookie(url,ref cookieContainer);            
            s = FuncHelp.CutFrom(s, "box_chi_tiet_cong_viec");

            string hovaten = FuncHelp.CutFromTo(s, "<span class='name font24 lh_12 bold'>","</span>");
            string gioitinh = FuncHelp.CutFromTo(s, "Giới tính:</span>", "</span>").Trim();
            string ngaysinh = FuncHelp.CutFromTo(s, "Ngày sinh:</span>", "<div").Trim();
            string email = FuncHelp.CutFromTo(s, "Email:</span>", "</span>").Trim();
            string dienthoai = FuncHelp.CutFromTo(s, "Điện thoại:</span>", "</div>").Trim();
            string diachi = FuncHelp.CutFromTo(s, "Địa chỉ:</span>", "</div>").Trim();

            string capbachientai = FuncHelp.CutFromTo(s, "Cấp bậc hiện tại:</span>", "</span>").Trim();
            string kinhnghiem = FuncHelp.CutFromTo(s, "Kinh nghiệm:</span>", "</span>").Trim();
            string trinhdocaonhat = FuncHelp.CutFromTo(s, "Trình độ cao nhất:</span>", "</span>").Trim();
            string ngoaingu = FuncHelp.CutFromTo(s, "Ngoại ngữ:</span>", "</span>").Trim();
            string honnhan = FuncHelp.CutFromTo(s, "Hôn nhân:</span>", "</span>").Trim();
            string capbacmongmuon = FuncHelp.CutFromTo(s, "Cấp bậc mong muốn:</span>", "<p").Trim();
            string diadiemmongmuon = FuncHelp.CutFromTo(s, "Địa điểm mong muốn:</span>", "<p").Trim();
            string nganhnghemongmuon = FuncHelp.CutFromTo(s, "Ngành nghề mong muốn:</span>", "<p").Trim();
            string mucluongmongmuon = FuncHelp.CutFromTo(s, "Mức lương mong muốn:</span>", "<p").Trim();
            string hinhthuclamviec = FuncHelp.CutFromTo(s, "Hình thức làm việc:</span>", "<p").Trim();

            string muctieunghenghiep = FuncHelp.CutFromTo(s, "Mục tiêu nghề nghiệp", "</div>").Trim();
            string bangcapchungchi = FuncHelp.CutFromTo(s, "Bằng cấp - Chứng chỉ</span>", "<div class='job_description").Trim();
            string ngoainguchitiet = FuncHelp.CutFromTo(s, "Ngoại ngữ</span>", "<div class='job_description").Trim();
            string trinhdotinhoc = FuncHelp.CutFromTo(s, "Trình độ tin học</span>", "<div class='job_description").Trim();
            string kinhnghiemchitiet = FuncHelp.CutFromTo(s, "Kinh nghiệm</span>", "Kỹ năng và sở trường</span>").Trim();
            string kynangsotruong = FuncHelp.CutFromTo(s, "Kỹ năng và sở trường</span>", "<div class='w_100 txc mt_20 button_print'>").Trim();
            string hosodinhkem = FuncHelp.CutFromTo(s, "Hồ sơ đính kèm</span>", "</a>").Trim();
            hosodinhkem = FuncHelp.CutFromTo(hosodinhkem, "a href='", "' style");

            hovaten = Regex.Replace(hovaten, @"<[^>]+>|&nbsp;", "").Trim();
            gioitinh = Regex.Replace(gioitinh, @"<[^>]+>|&nbsp;", "").Trim();
            ngaysinh = Regex.Replace(ngaysinh, @"<[^>]+>|&nbsp;", "").Trim();
            email = Regex.Replace(email, @"<[^>]+>|&nbsp;", "").Trim();
            dienthoai = Regex.Replace(dienthoai, @"<[^>]+>|&nbsp;", "").Trim();
            diachi = Regex.Replace(diachi, @"<[^>]+>|&nbsp;", "").Trim();

            capbachientai = Regex.Replace(capbachientai, @"<[^>]+>|&nbsp;", "").Trim();
            kinhnghiem = Regex.Replace(kinhnghiem, @"<[^>]+>|&nbsp;", "").Trim();
            trinhdocaonhat = Regex.Replace(trinhdocaonhat, @"<[^>]+>|&nbsp;", "").Trim();
            ngoaingu = Regex.Replace(ngoaingu, @"<[^>]+>|&nbsp;", "").Trim();
            honnhan = Regex.Replace(honnhan, @"<[^>]+>|&nbsp;", "").Trim();
            capbacmongmuon = Regex.Replace(capbacmongmuon, @"<[^>]+>|&nbsp;", "").Trim();
            diadiemmongmuon = Regex.Replace(diadiemmongmuon, @"<[^>]+>|&nbsp;", "").Trim();
            nganhnghemongmuon = Regex.Replace(nganhnghemongmuon, @"<[^>]+>|&nbsp;", "").Trim();
            mucluongmongmuon = Regex.Replace(mucluongmongmuon, @"<[^>]+>|&nbsp;", "").Trim();
            hinhthuclamviec = Regex.Replace(hinhthuclamviec, @"<[^>]+>|&nbsp;", "").Trim();

            muctieunghenghiep = Regex.Replace(muctieunghenghiep, @"<[^>]+>|&nbsp;", "").Trim();
            bangcapchungchi = Regex.Replace(bangcapchungchi, @"<[^>]+>|&nbsp;", "").Trim();
            ngoainguchitiet = Regex.Replace(ngoainguchitiet, @"<[^>]+>|&nbsp;", "").Trim();
            trinhdotinhoc = Regex.Replace(trinhdotinhoc, @"<[^>]+>|&nbsp;", "").Trim();
            kinhnghiemchitiet = Regex.Replace(kinhnghiemchitiet, @"<[^>]+>|&nbsp;", "").Trim();
            kynangsotruong = Regex.Replace(kynangsotruong, @"<[^>]+>|&nbsp;", "").Trim();
            hosodinhkem = Regex.Replace(hosodinhkem, @"<[^>]+>|&nbsp;", "").Trim();

            ////tencongty = WebUtility.HtmlDecode(tencongty);

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = hovaten; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = gioitinh; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = ngaysinh; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = email; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = dienthoai; }
            if (chkListBox.GetItemChecked(5)) { i++; dr[i] = diachi; }
            if (chkListBox.GetItemChecked(6)) { i++; dr[i] = capbachientai; }
            if (chkListBox.GetItemChecked(7)) { i++; dr[i] = kinhnghiem; }
            if (chkListBox.GetItemChecked(8)) { i++; dr[i] = trinhdocaonhat; }
            if (chkListBox.GetItemChecked(9)) { i++; dr[i] = ngoaingu; }
            if (chkListBox.GetItemChecked(10)) { i++; dr[i] = honnhan; }
            if (chkListBox.GetItemChecked(11)) { i++; dr[i] = capbacmongmuon; }
            if (chkListBox.GetItemChecked(12)) { i++; dr[i] = diadiemmongmuon; }
            if (chkListBox.GetItemChecked(13)) { i++; dr[i] = nganhnghemongmuon; }
            if (chkListBox.GetItemChecked(14)) { i++; dr[i] = mucluongmongmuon; }
            if (chkListBox.GetItemChecked(15)) { i++; dr[i] = hinhthuclamviec; }

            if (chkListBox.GetItemChecked(16)) { i++; dr[i] = muctieunghenghiep; }
            if (chkListBox.GetItemChecked(17)) { i++; dr[i] = bangcapchungchi; }
            if (chkListBox.GetItemChecked(18)) { i++; dr[i] = ngoainguchitiet; }
            if (chkListBox.GetItemChecked(19)) { i++; dr[i] = trinhdotinhoc; }
            if (chkListBox.GetItemChecked(20)) { i++; dr[i] = kinhnghiemchitiet; }
            if (chkListBox.GetItemChecked(21)) { i++; dr[i] = kynangsotruong; }
            if (chkListBox.GetItemChecked(22)) { i++; dr[i] = hosodinhkem; }
            if (chkListBox.GetItemChecked(23)) { i++; dr[i] = url; }

            dt.Rows.Add(dr);
        }

        void Get_Login()
        {
            if (IsLogin) { return; }
            stt1.Visible = true;
            stt1.Text = "Đang đăng nhập...";

            var request = (HttpWebRequest)WebRequest.Create(host+"/taikhoan/login");            
            request.CookieContainer = cookieContainer;
            var postData = "username="+txtID.Text + "&password="+txtPW.Text;
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
                string json = responseString.Replace("\n", "").Replace("\t", "").Replace("\"", "'").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                if (FuncHelp.CutFromTo(json, "'user_type':'", "'") == "1")
                {
                    stt1.Visible = false;
                    IsLogin = true;
                }
                else
                {
                    MessageBox.Show("Tài khoản đăng nhập hoặc Mật khẩu không chính xác!!!");
                    IsStop = true;
                }
            }          

        }

        bool CheckInput()
        {
            if (chkListBox.CheckedItems.Count < 1)
            {
                MessageBox.Show("Bạn phải chọn ít nhất 1 trường dữ liệu");
                return false;
            }
            else if (lbID.Text.Trim().Length==0 || lbPW.Text.Trim().Length==0)
            {
                MessageBox.Show("Bạn phải điền tài khoản đăng nhập Vieclam24h");
                return false;
            }
            else if (int.Parse(cbxMax.SelectedItem.ToString()) < int.Parse(cbxMin.SelectedItem.ToString()))
            {
                MessageBox.Show("Lựa chọn trang không phù hợp!!!");
                return false;
            }
            else if(cbxCategory.SelectedIndex==0)
            {
                MessageBox.Show("Bạn phải chọn danh mục cần lấy dữ liệu");
                return false;
            }

            // INIT 
            ArrayDetailLink.Clear();
            dt.Columns.Clear();
            foreach (Object i in chkListBox.CheckedItems) { dt.Columns.Add(i.ToString()); }

            return true;
        } 

        #endregion

        private void frmM6_Load(object sender, EventArgs e)
        {
            chxSelectAll.Checked = true;
            Application.DoEvents();
            Get_Category();
        }

        private void cbxOnlyDay_CheckedChanged(object sender, EventArgs e)
        {
            dtpOnlyDay.Enabled = cbxOnlyDay.Checked;
        }

        private void cbxSlg_CheckedChanged(object sender, EventArgs e)
        {
            numMaxSlg.Enabled = cbxSlg.Checked;
        }

        private void cbxPageLimit_CheckedChanged(object sender, EventArgs e)
        {
            cbxMin.Enabled = cbxPageLimit.Checked;
            cbxMax.Enabled = cbxPageLimit.Checked;
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCategory.SelectedIndex > 0)
                Get_CategoryOneLinkToPage(cbxCategory.SelectedValue.ToString());
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (IsStop)
            {
                btnRun.Text = "GET INFO";
                IsRun = false;
                Application.DoEvents();
                return;
            } 
            if (IsRun) { IsStop = true; return; }
            if (!CheckInput()) { return; }

            btnRun.Text = "STOP";
            IsRun = true;
            Get_TotalLink();
            if (IsStop ) { return; }
            if (ArrayDetailLink.Count == 0) { MessageBox.Show("Không có link thỏa điều kiện!\nQuá trình tải kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            stt1.Visible = false;
            stt2.Visible = false;
            stt3.Visible = false;

            Get_Login();
            if (IsStop) { return; }

            stt3.Visible = true;
            dt.Rows.Clear();

            for (int i = 1; i <= ArrayDetailLink.Count; i++)
            {
                Get_DataLink(ArrayDetailLink[i - 1]);
                (Application.OpenForms["frmMain"] as frmMain).SetPercentProcess(i, ArrayDetailLink.Count);
                stt3.Text = string.Format(stt3.Tag as string, i, ArrayDetailLink.Count);
                Application.DoEvents();
                if (IsStop || (cbxSlg.Checked && dt.Rows.Count >= numMaxSlg.Value))
                {
                    IsStop = false; break;
                }
            }

            // FINISH
            btnRun.Text = "GET INFO";
            IsRun = false;
            (Application.OpenForms["frmMain"] as frmMain).SetPercentProcess(15);
            string filename = @"Export\Export_NTDVieclam24h " + dtListCategory.Rows[cbxCategory.SelectedIndex]["Name"] + ".xlsx";
            Dictionary<int, int> colw = new Dictionary<int, int>();
            colw.Add(1, 35);
            FuncHelp.ExportExcel(dt, filename, colw);

            if (MessageBox.Show("Lưu dữ liệu thành công \n\nBạn có muốn mở file đã lưu?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                Process.Start(filename);

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListBox.Items.Count; i++)
                chkListBox.SetItemChecked(i, chxSelectAll.Checked);
        }

               

    }
}
