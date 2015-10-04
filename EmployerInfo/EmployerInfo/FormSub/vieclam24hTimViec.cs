using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmVieclam24TimViec : Form
    {

        #region # INIT #

        public frmVieclam24TimViec()
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

            if (!chxGetAllCategory.Checked) {
                for (int i = 0; i < chkListCategory.Items.Count; i++)
                    if(chkListCategory.GetItemChecked(i))
                        dt_CategoryLink.Rows.Add(dt_Category.Rows[i][0], dt_Category.Rows[i][1]);
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
                stt2.Text = string.Format("Đang lấy dữ liệu danh mục: {0} ({1}/{2})", dt_CategoryLink.Rows[i - 1]["Name"], i, dt_CategoryLink.Rows.Count);
                Application.DoEvents();
                Get_LinkDetailFromPage(dt_CategoryLink.Rows[i - 1]);
                if (IsStop || (cbxSlg.Checked && dt_DetailLinkFromPage.Rows.Count >= numMaxSlg.Value)) { IsStop = false; break; } 
            }

            dt_CategoryLink.Clear();
            stt4.Visible = true; stt4.Text = "Loading...";
            stt3.Visible = true; stt3.Text = "Loading...";
            stt2.Visible = true; stt2.Text = "Đang lấy dữ liệu chi tiết";
            stt1.Visible = false;
            dt.Rows.Clear();
            Application.DoEvents();
            //dt_DetailLinkFromPage = dt_DetailLinkFromPage.DefaultView.ToTable(true, "Link", "Name");

            for (int i = 1; i <= dt_DetailLinkFromPage.Rows.Count; i++)
            {
                Get_DataLink(dt_DetailLinkFromPage.Rows[i - 1]);
                if (IsStop || (cbxSlg.Checked && dt.Rows.Count >= numMaxSlg.Value)) { IsStop = false; break; }
                (Application.OpenForms["frmMain"] as frmMain).SetPercentProcess(i, dt_DetailLinkFromPage.Rows.Count);
                stt3.Text = dt.Rows[i-1][0].ToString();
                stt4.Text = string.Format("Số trang đã duyệt: {0:##.##}/{1}... {2:##.#}%", i, dt_DetailLinkFromPage.Rows.Count, (double)i * 100 / dt_DetailLinkFromPage.Rows.Count);
                Application.DoEvents();                
            }
            
            // FINISH
            btnRun.Text = "GET INFO";
            IsRun = false;
            string filename = @"Export\Export_vieclam24h " + dt_Port.Rows[cbxPortList.SelectedIndex]["Name"].ToString() + " " + txtFileName.Text + " " + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".xlsx";
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
            if(chkListCategory.CheckedItems.Count<1 && !chxGetAllCategory.Checked)
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

            string s = FuncHelp.GetSourceWithCookie(cbxPortList.SelectedValue as string,ref cookieContainer);
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
                dt_Category.Rows.Add(strLink, strName, "[" + strCount + "] " + strName, strIdCategory, strCount.Replace(",", "").Replace(".", ""));
            }

            dt_Category.DefaultView.Sort = "Name ASC";
            dt_Category = dt_Category.DefaultView.ToTable();

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
            stt4.Visible = true;

            string s = FuncHelp.GetSourceWithCookie(host + link["Link"].ToString(),ref cookieContainer);
            s = FuncHelp.CutFromTo(s, "Kết quả tìm kiếm", "</span>").Replace("(", "").Replace(")", "").Replace(",", "").Replace(".", "");

            int totalitem = Convert.ToInt32(s);
            int page_max = totalitem;
            page_max = Convert.ToInt32(page_max / 20) + (page_max % 20 > 0 ? 1 : 0);
            if (page_max < 1) { return; }

            //stt3.Text = string.Format("Tổng số ứng viên thật: {0:##,##} Tổng trang: {1:##,##}", Convert.ToInt32(s), pagemax);

            for (int i = 1; i <= page_max; i++)
            {
                s = FuncHelp.GetSourceWithCookie(host + link["Link"].ToString() + "&page=" + i.ToString(),ref cookieContainer);
                bool IsBreak = false;
                while (s.IndexOf("title-blockjob-main") > 0)
                {
                    if (IsStop) { IsBreak = true; break; }
                    if (cbxSlg.Checked && numMaxSlg.Value <= dt_DetailLinkFromPage.Rows.Count) { IsBreak = true; break; }
                    s = FuncHelp.CutFrom(s, "title-blockjob-main");
                    string temp = FuncHelp.CutTo(s, "</span>");
                    string li = FuncHelp.CutFromTo(temp, "<a href='", "' class=");
                    string da = FuncHelp.CutFromTo(s, "Hạn nộp hồ sơ'>", "</div>").Trim();
                    da = Regex.Replace(da, @"<[^>]+>|&nbsp;", "").Trim();
                    DateTime dtime;
                    if (!DateTime.TryParseExact(da, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtime)) { dt_DetailLinkFromPage.Rows.Add(li, link["Name"]); continue; };
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
            if (s == "") { IsStop = true; return; }
            
            s = FuncHelp.CutFrom(s, "block-nha-tuyen-dung");
            s = WebUtility.HtmlDecode(s);

            string tencongty = FuncHelp.CutFromTo(s, "<h3 class='font18 mt_10 mb_8'>", "</h3>").Trim();
            string diachi = FuncHelp.CutFromTo(s, "<address class='mb_16 mt_8'>", "</address>").Trim();
            string quymocongty = FuncHelp.CutFromTo(s, "Quy mô công ty:", "</p>").Trim();            
            string danhmuc = link["Name"].ToString();

            s = FuncHelp.CutFrom(s, "box_chi_tiet_cong_viec");

            string tenvieclam = FuncHelp.CutFromTo(s, "title_big'>", "</h1>");
            string hannophoso = FuncHelp.CutFromTo(s, "Hạn nộp hồ sơ:", "</span>");            
            string luotxem = FuncHelp.CutFromTo(s, "Lượt xem:", "</span>").Trim();
            string maNTD = FuncHelp.CutFromTo(s, "<span>Mã:", "</span>").Trim();
            string ngaylammoi = FuncHelp.CutFromTo(s, "Ngày làm mới:", "</span>").Trim();
            string mucluong = FuncHelp.CutFromTo(s, "Mức lương:", "</span>").Trim();
            string kinhnghiem = FuncHelp.CutFromTo(s, "Kinh nghiệm:", "</span>").Trim();
            string yeucaubangcap = FuncHelp.CutFromTo(s, "Yêu cầu bằng cấp:", "</span>").Trim();
            string soluongcantuyen = FuncHelp.CutFromTo(s, "Số lượng cần tuyển:", "</span>").Trim();
            string nganhnghe = FuncHelp.CutFromTo(s, "Ngành nghề:", "</a>").Trim();
            string diadiemlamviec = FuncHelp.CutFromTo(s, "Địa điểm làm việc:", "</a>").Trim();
            string chucvu = FuncHelp.CutFromTo(s, "Chức vụ:", "</span>").Trim();
            string hinhthuclamviec = FuncHelp.CutFromTo(s, "Hình thức làm việc:", "</span>").Trim();
            string yeucaugioitinh = FuncHelp.CutFromTo(s, "Yêu cầu giới tính:", "</span>").Trim();
            string yeucaudotuoi = FuncHelp.CutFromTo(s, "Yêu cầu độ tuổi:", "</span>").Trim();
            string motacongviec = FuncHelp.CutFromTo(s, "<p class='title uppercase text_grey'>Mô tả công việc</p>", "</p>").Trim();
            string quyenloidchuong = FuncHelp.CutFromTo(s, "<p class='title uppercase text_grey'>Quyền lợi được hưởng</p>", "</p>").Trim();
            string yeucaukhac = FuncHelp.CutFromTo(s, "<p class='title uppercase text_grey'>Yêu cầu khác</p>", "</p>").Trim();
            string hosobaogom = FuncHelp.CutFromTo(s, "Hồ sơ bao gồm</p>", "</p>").Trim();
            string nguoilienhe = FuncHelp.CutFromTo(s, "<p class='title uppercase text_grey'>Người liên hệ</p>", "</p>").Trim();
            string diachilienhe = FuncHelp.CutFromTo(s, "<p class='title uppercase text_grey'>Địa chỉ liên hệ</p>", "</p>").Trim();
            string emaillienhe = FuncHelp.CutFromTo(s, "<p class='title uppercase text_grey'>Email liên hệ</p>", "</p>").Trim();
            string dienthoailienhe = FuncHelp.CutFromTo(s, "<p class='title uppercase text_grey'>Điện thoại liên hệ</p>", "</p>").Trim();

            quymocongty = Regex.Replace(quymocongty, @"<[^>]+>|&nbsp;", "").Trim();
            hannophoso = Regex.Replace(hannophoso, @"<[^>]+>|&nbsp;", "").Trim();
            mucluong = Regex.Replace(mucluong, @"<[^>]+>|&nbsp;", "").Trim();
            kinhnghiem = Regex.Replace(kinhnghiem, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaubangcap = Regex.Replace(yeucaubangcap, @"<[^>]+>|&nbsp;", "").Trim();
            soluongcantuyen = Regex.Replace(soluongcantuyen, @"<[^>]+>|&nbsp;", "").Trim();
            nganhnghe = Regex.Replace(nganhnghe, @"<[^>]+>|&nbsp;", "").Trim();
            diadiemlamviec = Regex.Replace(diadiemlamviec, @"<[^>]+>|&nbsp;", "").Trim();
            chucvu = Regex.Replace(chucvu, @"<[^>]+>|&nbsp;", "").Trim();
            hinhthuclamviec = Regex.Replace(hinhthuclamviec, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaugioitinh = Regex.Replace(yeucaugioitinh, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaudotuoi = Regex.Replace(yeucaudotuoi, @"<[^>]+>|&nbsp;", "").Trim();
            motacongviec = Regex.Replace(motacongviec, @"<[^>]+>|&nbsp;", "").Trim();
            quyenloidchuong = Regex.Replace(quyenloidchuong, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaukhac = Regex.Replace(yeucaukhac, @"<[^>]+>|&nbsp;", "").Trim();
            hosobaogom = Regex.Replace(hosobaogom, @"<[^>]+>|&nbsp;", "").Trim();
            nguoilienhe = Regex.Replace(nguoilienhe, @"<[^>]+>|&nbsp;", "").Trim();
            diachilienhe = Regex.Replace(diachilienhe, @"<[^>]+>|&nbsp;", "").Trim();
            emaillienhe = Regex.Replace(emaillienhe, @"<[^>]+>|&nbsp;", "").Trim();
            dienthoailienhe = Regex.Replace(dienthoailienhe, @"<[^>]+>|&nbsp;", "").Trim();

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = tenvieclam; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = hannophoso; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = luotxem; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = maNTD; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = ngaylammoi; }
            if (chkListBox.GetItemChecked(5)) { i++; dr[i] = mucluong; }
            if (chkListBox.GetItemChecked(6)) { i++; dr[i] = kinhnghiem; }
            if (chkListBox.GetItemChecked(7)) { i++; dr[i] = yeucaubangcap; }
            if (chkListBox.GetItemChecked(8)) { i++; dr[i] = soluongcantuyen; }
            if (chkListBox.GetItemChecked(9)) { i++; dr[i] = nganhnghe; }
            if (chkListBox.GetItemChecked(10)) { i++; dr[i] = diadiemlamviec; }
            if (chkListBox.GetItemChecked(11)) { i++; dr[i] = chucvu; }
            if (chkListBox.GetItemChecked(12)) { i++; dr[i] = hinhthuclamviec; }
            if (chkListBox.GetItemChecked(13)) { i++; dr[i] = yeucaugioitinh; }
            if (chkListBox.GetItemChecked(14)) { i++; dr[i] = yeucaudotuoi; }
            if (chkListBox.GetItemChecked(15)) { i++; dr[i] = motacongviec; }
            if (chkListBox.GetItemChecked(16)) { i++; dr[i] = quyenloidchuong; }
            if (chkListBox.GetItemChecked(17)) { i++; dr[i] = yeucaukhac; }
            if (chkListBox.GetItemChecked(18)) { i++; dr[i] = hosobaogom; }
            if (chkListBox.GetItemChecked(19)) { i++; dr[i] = nguoilienhe; }
            if (chkListBox.GetItemChecked(20)) { i++; dr[i] = diachilienhe; }
            if (chkListBox.GetItemChecked(21)) { i++; dr[i] = emaillienhe; }
            if (chkListBox.GetItemChecked(22)) { i++; dr[i] = dienthoailienhe; }
            if (chkListBox.GetItemChecked(23)) { i++; dr[i] = tencongty; }
            if (chkListBox.GetItemChecked(24)) { i++; dr[i] = diachi; }
            if (chkListBox.GetItemChecked(25)) { i++; dr[i] = quymocongty; }
            if (chkListBox.GetItemChecked(26)) { i++; dr[i] = danhmuc; }

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

        private void chxGetAllCategory_CheckedChanged(object sender, EventArgs e)
        {
            chkListCategory.Enabled = !chxGetAllCategory.Checked;
        }

    }
}
