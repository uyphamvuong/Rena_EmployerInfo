using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmTrangVangVN : Form
    {

        #region # INIT #

        public frmTrangVangVN()
        {
            InitializeComponent();           
        }

        private void frmM1_Load(object sender, EventArgs e)
        {
            chxSelectAll.Checked = true;
            numMaxSlg.Value = 1000;

            dt_Category.Columns.Add("Link");
            dt_Category.Columns.Add("Name");
            dt_Category.Columns.Add("NameWithCount");
            dt_Category.Columns.Add("CountItem");

            dt_CategoryLink.Columns.Add("Link");
            dt_CategoryLink.Columns.Add("Name");            

            dt_DetailLinkFromPage.Columns.Add("Link");
            dt_DetailLinkFromPage.Columns.Add("Name");

            dt_Port.Columns.Add("Link");
            dt_Port.Columns.Add("Name");

            cbxPortList.DataSource = dt_Port;
            cbxPortList.ValueMember = "Link";
            cbxPortList.DisplayMember = "Name";
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListBox.Items.Count; i++)
                chkListBox.SetItemChecked(i, chxSelectAll.Checked);
        }       

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            numMaxSlg.Enabled = cbxSlg.Checked;
        }

        #endregion

        bool IsStop = false, IsRun = false;
        string host = "http://trangvangvietnam.com/";
        DataTable dt_CategoryLink = new DataTable();
        DataTable dt_DetailLinkFromPage = new DataTable();
        DataTable dt_Category = new DataTable();
        DataTable dt_Port = new DataTable();
        DataTable dt = new DataTable();
        CookieContainer cookieContainer = new CookieContainer();

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (IsRun) { IsStop = true; return; }
            if (!CheckInput()) { return; }

            if (!chxGetAllCategory.Checked)
            {
                for (int i = 0; i < chkListCategory.Items.Count;i++ )
                    if (chkListCategory.GetItemChecked(i)) { dt_CategoryLink.Rows.Add(dt_Category.Rows[i][0], dt_Category.Rows[i][1]); }                   
            }
            else
            {
                for (int i = 0; i < dt_Category.Rows.Count; i++)
                    dt_CategoryLink.Rows.Add(dt_Category.Rows[i][0], dt_Category.Rows[i][1]);
            }

            btnRun.Text = "STOP";
            IsRun = true;

            // Thông báo số link đã tiếp nhận
            stt1.Visible = true;
            stt1.Text = "Số danh mục sẽ lấy dữ liệu: " + dt_CategoryLink.Rows.Count.ToString();
            
            stt2.Visible = true;
            dt_DetailLinkFromPage.Rows.Clear();            

            for (int i = 1; i <= dt_CategoryLink.Rows.Count; i++) 
            {
                (Application.OpenForms["frmMain"] as frmMain).SetPercentProcess(i, dt_CategoryLink.Rows.Count);
                stt2.Text = string.Format("{0} ({1}/{2})", dt_CategoryLink.Rows[i - 1]["Name"], i, dt_CategoryLink.Rows.Count);
                Application.DoEvents();
                Get_LinkDetailFromPage(dt_CategoryLink.Rows[i - 1]);
                if (IsStop || (cbxSlg.Checked && dt_DetailLinkFromPage.Rows.Count >= numMaxSlg.Value)) { IsStop = false; break; } 
            }

            //=============

            dt_CategoryLink.Clear();
            stt4.Visible = true; stt4.Text = "Loading...";
            stt3.Visible = true; stt3.Text = "Loading...";
            stt2.Visible = true; stt2.Text = "Đang lấy dữ liệu chi tiết";
            stt1.Visible = false;
            stt5.Visible = true; stt5.Text = "";
            dt.Rows.Clear();
            dt_DetailLinkFromPage = dt_DetailLinkFromPage.DefaultView.ToTable(true, "Link", "Name");
            Application.DoEvents();

            for (int i = 1; i <= dt_DetailLinkFromPage.Rows.Count; i++)
            {
                Get_DataLink(dt_DetailLinkFromPage.Rows[i - 1]);
                (Application.OpenForms["frmMain"] as frmMain).SetPercentProcess(i, dt_DetailLinkFromPage.Rows.Count);
                stt3.Text = string.Format("{0}", dt_DetailLinkFromPage.Rows[i-1]["Name"]);
                stt4.Text = string.Format("Số công ty đã nhận: {0:##,##}/{1:##,##}...{2:##,##}%", i, dt_DetailLinkFromPage.Rows.Count, (double)i * 100 / dt_DetailLinkFromPage.Rows.Count);
                stt5.Text = dt.Rows[dt.Rows.Count - 1][0].ToString();
                Application.DoEvents();
                if (IsStop || (cbxSlg.Checked && dt.Rows.Count >= numMaxSlg.Value)) { IsStop = false; break; }
            }
            
            // FINISH
            btnRun.Text = "GET INFO";
            IsRun = false;
            stt5.Visible = false;
            (Application.OpenForms["frmMain"] as frmMain).NoProcess();            
            string filename = @"Export\Export_TrangVangVietNam_"+ dt_Port.Rows[cbxPortList.SelectedIndex]["Name"] + " " + txtFileName.Text+" " + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss")  +".xlsx";
            FuncHelp.ExportExcel(dt, filename, new Dictionary<int,int>{{1,70}});

            if (frmMain.AskOpenFileWhenDone)
            {
                if (MessageBox.Show("Lưu dữ liệu thành công \n\nBạn có muốn mở file đã lưu?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    Process.Start(filename);
            }
        }

        bool CheckInput()
        {
            if (chkListBox.CheckedItems.Count < 1)
            {
                MessageBox.Show("Bạn phải chọn ít nhất 1 trường dữ liệu");
                return false;
            }
            if (chkListCategory.CheckedItems.Count < 1 && !chxGetAllCategory.Checked)
            {
                MessageBox.Show("Bạn phải chọn ít nhất 1 danh mục để lấy dữ liệu");
                return false;
            }

            // INIT 
            dt_CategoryLink.Clear();            
            dt_DetailLinkFromPage.Clear();
            dt.Columns.Clear();
            foreach (Object i in chkListBox.CheckedItems) { dt.Columns.Add(i.ToString()); }            

            return true;
        }        

        void Get_ArrayLinkCategory()
        {
            dt_Category.Rows.Clear();
            stt1.Visible = true;
            stt1.Text = "Đang lấy các danh mục...";
            btnRun.Enabled = false;
            Application.DoEvents();

            string s = FuncHelp.GetSource(cbxPortList.SelectedValue as string);
            s = FuncHelp.CutFromTo(s, "niengiamcontent", "id='contentmainr'");

            while (s.IndexOf("<a") > 0)
            {
                s = FuncHelp.CutFrom(s, "<a");
                string temp = FuncHelp.CutFromTo(s, "href", "</a>");
                string strLink = FuncHelp.CutFromTo(temp, "='", "'").Replace("../","");
                temp = FuncHelp.CutFrom(temp, ">");
                string strName = FuncHelp.CutTo(temp, "<span").Trim();
                string strCount = temp.Substring(temp.LastIndexOf("(")+1,temp.Length-temp.LastIndexOf("(")-1);
                strCount = FuncHelp.CutTo(strCount, ")</span>");
                strName = WebUtility.HtmlDecode(strName);
                dt_Category.Rows.Add(strLink, strName, "[" + strCount + "] " +  strName, strCount.Replace(",", "").Replace(".", ""));
            }

            dt_Category.DefaultView.Sort = "Name ASC";
            dt_Category = dt_Category.DefaultView.ToTable(true, "Link", "Name", "NameWithCount", "CountItem");

            chkListCategory.Items.Clear();
            for (int i = 0; i < dt_Category.Rows.Count; i++)
                chkListCategory.Items.Add(dt_Category.Rows[i]["NameWithCount"]);
            stt1.Text = "Tổng danh mục đã nhận: " + dt_Category.Rows.Count.ToString();
            btnRun.Enabled = true;
            chkListCategory.Enabled = true;
        }

        void Get_LinkDetailFromPage(DataRow link)
        {
            stt3.Visible = true;

            string s = FuncHelp.GetSource(host + link["Link"].ToString());
            string to = FuncHelp.CutFromTo(s, "thongbaotimtxt", "</div>");
            to = FuncHelp.CutFromTo(to, "<strong>", "</strong>").Replace("công ty", "").Trim();

            if (to == "") { return; }
            int totalitem = Convert.ToInt32(to);
            int page_max = totalitem;
            page_max = Convert.ToInt32(page_max / 25) + (page_max % 25 > 0 ? 1 : 0);
            if (page_max < 1) { return; }
            stt2.Text = string.Format("{0} [{1:##,##}]", link["Name"], totalitem); Application.DoEvents();
            stt3.Text = "Loading...";
            Application.DoEvents();

            for (int i = 1; i <= page_max; i++)
            {
                if(i!=1){s = FuncHelp.GetSource(host + link["Link"].ToString() + "&page=" + i.ToString());}
                s = FuncHelp.CutFromTo(s, "<div id='main_seachl'>", "<div id='main_seachr'>");
                bool IsBreak = false;
                while (s.IndexOf("<h2 class='company_name'>") > 0)
                {
                    if (IsStop || (cbxSlg.Checked & dt_DetailLinkFromPage.Rows.Count > numMaxSlg.Value)) { IsBreak = true; break; }
                    s = FuncHelp.CutFrom(s, "<h2 class='company_name'>");
                    string li = FuncHelp.CutFromTo(s, "<a href='", "'>");
                    dt_DetailLinkFromPage.Rows.Add(li, link["Name"]);
                    
                }
                (Application.OpenForms["frmMain"] as frmMain).SetPercentProcess(i, page_max);
                stt3.Text = string.Format("Tổng link đã nhận: {0:##,##}     Đang duyệt: {1:##,##}/{2:##,##}", dt_DetailLinkFromPage.Rows.Count, i, page_max);
                Application.DoEvents();
                if (IsBreak) { break; }
                if (i == 1 && dt_DetailLinkFromPage.Rows.Count < 25) { page_max = Convert.ToInt32(totalitem / dt_DetailLinkFromPage.Rows.Count) + (page_max % dt_DetailLinkFromPage.Rows.Count > 0 ? 1 : 0); }
            }                       
        }

        void Get_DataLink(DataRow link)
        {
            string s = FuncHelp.GetSource(link["Link"].ToString());
            s = FuncHelp.CutFromTo(s, "<div id='main_businessdetail'>", "<p class='tranhanhnganh'>").Replace("> <","><");
            s = WebUtility.HtmlDecode(s);

            string tencongty = FuncHelp.CutFromTo(s, "<h1 class='detailcompany_name'>", "</h1>").Trim();
            string diachi = FuncHelp.CutFromTo(s, "<div class='detail_dc11'>Địa chỉ:</div>", "</div>").Trim();
            string dienthoai = FuncHelp.CutFromTo(s, "<div class='detail_dc11'>Điện thoại:</div>", "</div>").Trim();
            string fax = FuncHelp.CutFromTo(s, "<div class='detail_dc11'>Fax:</div>", "</div>").Trim();
            string email = FuncHelp.CutFromTo(s, "<div class='detail_dc11'>Email:</div>", "</div>").Trim();
            string website = FuncHelp.CutFromTo(s, "<div class='detail_dc11'>Website:</div>", "</div>").Trim();
            string loaihinh = FuncHelp.CutFromTo(s, "<div class='detail_dc11' style='margin-top:6px'>Loại hình:</div>", "</div>").Trim();
            string thitruong = FuncHelp.CutFromTo(s, "<div class='detail_dc11'>Thị trường:</div>", "</div>").Trim();
            string vanphonggiaodich = FuncHelp.CutFromTo(s, "VPGD: ", "</div>").Trim();
            string gioithieu = FuncHelp.CutFromTo(s, "Giới thiệu</h2></div>", "</div>").Trim();
            string nganhnghekinhdoanh = FuncHelp.CutFromTo(s, "Ngành nghề kinh doanh</h2></div>", "</div>").Trim();
            string sanphamdichvu = FuncHelp.CutFromTo(s, "Sản phẩm dịch vụ</h2></div>", "Thư viện hình ảnh</h2></div>").Trim();
            if (sanphamdichvu.Length < 1) { sanphamdichvu = FuncHelp.CutFromTo(s, "Sản phẩm dịch vụ</h2></div>", "Hồ sơ công ty</h2></div>").Trim(); }
            string masothue = FuncHelp.CutFromTo(s, "Mã số thuế:</div>", "</div>").Trim();
            string sonhanvien = FuncHelp.CutFromTo(s, "Số nhân viên:</div>", "</div>").Trim();
            string namthanhlap = FuncHelp.CutFromTo(s, "Năm thành lập:</div>", "</div>").Trim();
            string cuahang = FuncHelp.CutFromTo(s, "<b>Cửa Hàng</b>:", "</div>").Trim();
            string danhmuc = link["Name"].ToString();

            tencongty = Regex.Replace(tencongty, @"<[^>]+>|&nbsp;", "").Trim();
            diachi = Regex.Replace(diachi, @"<[^>]+>|&nbsp;", "").Trim();
            dienthoai = Regex.Replace(dienthoai, @"<[^>]+>|&nbsp;", "").Trim();
            fax = Regex.Replace(fax, @"<[^>]+>|&nbsp;", "").Trim();
            email = Regex.Replace(email, @"<[^>]+>|&nbsp;", "").Trim();
            website = Regex.Replace(website, @"<[^>]+>|&nbsp;", "").Trim();
            loaihinh = Regex.Replace(loaihinh, @"<[^>]+>|&nbsp;", "").Trim();
            thitruong = Regex.Replace(thitruong, @"<[^>]+>|&nbsp;", "").Trim();
            vanphonggiaodich = Regex.Replace(vanphonggiaodich, @"<[^>]+>|&nbsp;", "").Trim();
            gioithieu = Regex.Replace(gioithieu, @"<[^>]+>|&nbsp;", "").Trim();
            nganhnghekinhdoanh = Regex.Replace(nganhnghekinhdoanh, @"<[^>]+>|&nbsp;", "").Trim();
            sanphamdichvu = Regex.Replace(sanphamdichvu, @"<[^>]+>|&nbsp;", "").Trim();
            masothue = Regex.Replace(masothue, @"<[^>]+>|&nbsp;", "").Trim();
            sonhanvien = Regex.Replace(sonhanvien, @"<[^>]+>|&nbsp;", "").Trim();
            namthanhlap = Regex.Replace(namthanhlap, @"<[^>]+>|&nbsp;", "").Trim();
            cuahang = Regex.Replace(cuahang, @"<[^>]+>|&nbsp;", "").Trim();

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = tencongty; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = diachi; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = dienthoai; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = fax; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = email; }
            if (chkListBox.GetItemChecked(5)) { i++; dr[i] = website; }
            if (chkListBox.GetItemChecked(6)) { i++; dr[i] = loaihinh; }
            if (chkListBox.GetItemChecked(7)) { i++; dr[i] = thitruong; }
            if (chkListBox.GetItemChecked(8)) { i++; dr[i] = vanphonggiaodich; }
            if (chkListBox.GetItemChecked(9)) { i++; dr[i] = gioithieu; }
            if (chkListBox.GetItemChecked(10)) { i++; dr[i] = nganhnghekinhdoanh; }
            if (chkListBox.GetItemChecked(11)) { i++; dr[i] = sanphamdichvu; }
            if (chkListBox.GetItemChecked(12)) { i++; dr[i] = masothue; }
            if (chkListBox.GetItemChecked(13)) { i++; dr[i] = sonhanvien; }
            if (chkListBox.GetItemChecked(14)) { i++; dr[i] = namthanhlap; }
            if (chkListBox.GetItemChecked(15)) { i++; dr[i] = cuahang; }
            if (chkListBox.GetItemChecked(16)) { i++; dr[i] = danhmuc; }

            dt.Rows.Add(dr);
        }

        void Get_Port()
        {
            stt1.Visible = true;
            stt1.Text = "Đang lấy Niên giám ngành...";
            Application.DoEvents();
            string s = FuncHelp.GetSource(host);
            s = FuncHelp.CutFromTo(s, "niengiamnganh", "</div>");
            while (s.IndexOf("<a href='") > 0)
            {
                s = FuncHelp.CutFrom(s,"<a href='");
                string li = FuncHelp.CutTo(s, "'");
                string na = FuncHelp.CutFromTo(s, ">", "</a>");
                dt_Port.Rows.Add(li, na);
            }
            dt_Port.DefaultView.Sort = "Name ASC";
            dt_Port = dt_Port.DefaultView.ToTable();
            cbxPortList.DataSource = dt_Port;
        }

        private void frmM1_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            Get_Port();
        }

        private void cbxPortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_ArrayLinkCategory();
        }

        private void chxGetAllCategory_CheckedChanged(object sender, EventArgs e)
        {
            chkListCategory.Enabled = !chxGetAllCategory.Checked;
        }

    }
}
