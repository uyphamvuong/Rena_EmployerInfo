using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmM1 : Form
    {

        #region # INIT #

        public frmM1()
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

            dt_CategoryLink.Columns.Add("Link");
            dt_CategoryLink.Columns.Add("Name");

            dt_Category.Columns.Add("IdCategory");
            dt_Category.Columns.Add("CountItem");

            dt_DetailLinkFromPage.Columns.Add("Link");
            dt_DetailLinkFromPage.Columns.Add("Name");

            dt_Port.Columns.Add("Link");
            dt_Port.Columns.Add("Name");
            dt_Port.Rows.Add(host + "/viec-lam-quan-ly", "Việc làm quản lý");
            dt_Port.Rows.Add(host + "/viec-lam-chuyen-mon", "Việc làm theo chuyên môn");
            dt_Port.Rows.Add(host + "/viec-lam-lao-dong-pho-thong", "Lao động phổ thông/Nghề");
            dt_Port.Rows.Add(host + "/viec-lam-sinh-vien-ban-thoi-gian", "Sinh viên - Bán thời gian");
            cbxPortList.DataSource = dt_Port;
            cbxPortList.ValueMember = "Link";
            cbxPortList.DisplayMember = "Name";
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListBox.Items.Count; i++)
                chkListBox.SetItemChecked(i, chxSelectAll.Checked);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Enabled = cbxTimeLimit.Checked;
            label3.Enabled = cbxTimeLimit.Checked;
            dtpMin.Enabled = cbxTimeLimit.Checked;
            dtpMax.Enabled = cbxTimeLimit.Checked;
        }        

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            numMaxSlg.Enabled = cbxSlg.Checked;
        }

        #endregion

        bool IsStop = false, IsRun = false;
        string host = "http://hcm.vieclam.24h.com.vn";
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

            if (!chxGetAllCategory.Checked) { dt_CategoryLink.Rows.Add(dt_Category.Rows[cbxCategory.SelectedIndex][0], dt_Category.Rows[cbxCategory.SelectedIndex][1]); }
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
                stt2.Text = string.Format("Đang lấy dữ liệu danh mục: {0} ({1}/{2})", dt_CategoryLink.Rows[i - 1]["Name"], i, dt_CategoryLink.Rows.Count);
                Application.DoEvents();
                Get_LinkDetailFromPage(dt_CategoryLink.Rows[i - 1]);
                if (IsStop || (cbxSlg.Checked && dt_DetailLinkFromPage.Rows.Count >= numMaxSlg.Value)) { IsStop = false; break; } 
            }

            dt_CategoryLink.Clear();  
            stt4.Visible = true;
            stt3.Visible = true; stt3.Text = "Đang lấy dữ liệu chi tiết";
            stt2.Visible = false;
            stt1.Visible = false;
            dt.Rows.Clear();
            dt_DetailLinkFromPage = dt_DetailLinkFromPage.DefaultView.ToTable(true, "Link", "Name");

            for (int i = 1; i <= dt_DetailLinkFromPage.Rows.Count; i++)
            {
                Get_DataLink(dt_DetailLinkFromPage.Rows[i - 1]);
                (Application.OpenForms["frmMain"] as frmMain).SetPercentProcess(i, dt_DetailLinkFromPage.Rows.Count);
                stt4.Text = string.Format(stt4.Tag as string, i, dt_DetailLinkFromPage.Rows.Count, (double)i * 100 / dt_DetailLinkFromPage.Rows.Count);
                Application.DoEvents();
                if (IsStop || (cbxSlg.Checked && dt.Rows.Count >= numMaxSlg.Value)) { IsStop = false; break; }
            }
            
            // FINISH
            btnRun.Text = "GET INFO";
            IsRun = false;
            string filename = @"Export\Export_vieclam24h.xlsx";
            FuncHelp.ExportExcel(dt, filename);

            if (MessageBox.Show("Lưu dữ liệu thành công \n\nBạn có muốn mở file đã lưu?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                Process.Start(filename);
        }

        bool CheckInput()
        {
            if (chkListBox.CheckedItems.Count < 1)
            {
                MessageBox.Show("Bạn phải chọn ít nhất 1 trường dữ liệu");
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

            string s = FuncHelp.GetSourceWithCookie(cbxPortList.SelectedValue as string, ref cookieContainer);
            s = FuncHelp.CutFromTo(s, "id='gate_nganhnghe_abc'", "id='gate_tinhthanh_sl'");

            while (s.IndexOf("nganhnghe_item") > 0)
            {
                s = FuncHelp.CutFrom(s, "nganhnghe_item");
                string temp = FuncHelp.CutFromTo(s, "<a href", "</a>");
                string strLink = FuncHelp.CutFromTo(temp, "='", "' class");
                temp = FuncHelp.CutFrom(temp, ">");
                string strName = FuncHelp.CutTo(temp, "<span").Trim();
                string strCount = FuncHelp.CutFromTo(temp, ">(", ")</span>");
                strName = WebUtility.HtmlDecode(strName);
                //vieclam.24h.com.vn/tim-kiem-viec-lam-nhanh/?hdn_nganh_nghe_cap1=??
                string strIdCategory = strLink.Substring(strLink.LastIndexOf("-c") + 2, strLink.Length - strLink.LastIndexOf("-c") - ".html".Length - 2);
                strLink = "/tim-kiem-viec-lam-nhanh/?hdn_nganh_nghe_cap1=" + strIdCategory;
                dt_Category.Rows.Add(strLink, strName, strName + " (" + strCount + ")", strIdCategory, strCount.Replace(",", "").Replace(".", ""));
            }

            dt_Category.DefaultView.Sort = "Name ASC";
            dt_Category = dt_Category.DefaultView.ToTable();          

            cbxCategory.DataSource = dt_Category;
            stt1.Text = "Tổng danh mục đã nhận: " + dt_Category.Rows.Count.ToString();
            btnRun.Enabled = true;
            cbxCategory.Enabled = true;
        }

        void Get_LinkDetailFromPage(DataRow link)
        {
            stt3.Visible = true;
            stt4.Visible = true;

            string s = FuncHelp.GetSourceWithCookie(host + link["Link"].ToString(), ref cookieContainer);
            s = FuncHelp.CutFromTo(s, "Kết quả tìm kiếm", "</span>").Replace("(", "").Replace(")", "").Replace(",", "").Replace(".", "");

            int totalitem = Convert.ToInt32(s);
            int page_max = totalitem;
            page_max = Convert.ToInt32(page_max / 20) + (page_max % 20 > 0 ? 1 : 0);
            if (page_max < 1) { return; }

            //stt3.Text = string.Format("Tổng số ứng viên thật: {0:##,##} Tổng trang: {1:##,##}", Convert.ToInt32(s), pagemax);

            for (int i = 1; i <= page_max; i++)
            {
                s = FuncHelp.GetSourceWithCookie(host + link["Link"].ToString() + "&page=" + i.ToString(), ref cookieContainer);
                bool IsBreak = false;
                while (s.IndexOf("title-blockjob-sub") > 0)
                {
                    if (IsStop) { IsBreak = true; break; }
                    if (cbxSlg.Checked && numMaxSlg.Value <= dt_DetailLinkFromPage.Rows.Count) { IsBreak = true; break; }
                    s = FuncHelp.CutFrom(s, "title-blockjob-sub");
                    string temp = FuncHelp.CutTo(s, "</span>");
                    string li = FuncHelp.CutFromTo(temp, "<a href='", "' class=");
                    string da = FuncHelp.CutFromTo(s, "Hạn nộp hồ sơ'>", "</div>").Trim();
                    da = Regex.Replace(da, @"<[^>]+>|&nbsp;", "").Trim();
                    DateTime dtime = DateTime.ParseExact(da, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (cbxTimeLimit.Checked && (dtpMin.Value.Date > dtime.Date || dtime.Date > dtpMax.Value.Date))
                    {
                        //IsBreak = true; break;
                        continue;
                    }
                    else
                    {
                        dt_DetailLinkFromPage.Rows.Add(li, link["Name"]);
                    }                    
                }
                (Application.OpenForms["frmMain"] as frmMain).SetPercentProcess(i, page_max);
                stt3.Text = string.Format("Đang lấy trang {0}/{1} trang // Số dữ liệu thật: {2:##,##} link", i, page_max, totalitem);
                stt4.Text = string.Format("Tổng số link đã nhận: {0}", dt_DetailLinkFromPage.Rows.Count);
                if (IsBreak) { break; }
                Application.DoEvents();
            }           
        }

        void Get_DataLink(DataRow link)
        {
            string s = FuncHelp.GetSource(link["Link"].ToString());
            s = FuncHelp.CutFrom(s, "logo-company");
            s = WebUtility.HtmlDecode(s);

            string tencongty = FuncHelp.CutFromTo(s, "title='", "'").Trim();
            string diachi = FuncHelp.CutFromTo(s, "Địa chỉ:", "</p>").Trim();
            string quymocongty = FuncHelp.CutFromTo(s, "Quy mô công ty:", "</p>").Trim();
            string dienthoai = FuncHelp.CutFromTo(s, "Điện thoại:", "</p>").Trim();
            string danhmuc = link["Name"].ToString();

            //vitrituyendung = Regex.Replace(vitrituyendung, @"<[^>]+>|&nbsp;", "").Trim();

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = tencongty; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = diachi; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = quymocongty; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = dienthoai; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = danhmuc; }

            dt.Rows.Add(dr);
        }

        private void frmM1_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            
        }

        private void cbxPortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_ArrayLinkCategory();
        }

    }
}
