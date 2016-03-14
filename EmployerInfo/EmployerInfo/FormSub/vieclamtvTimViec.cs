using System;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmVieclamtvTimViec : Form
    {

        #region # INIT #

        public frmVieclamtvTimViec()
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

            dt_DetailLinkFromPage.Columns.Add("Link");
            dt_DetailLinkFromPage.Columns.Add("Name");
            
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
        string host = "http://vieclam.tv";
        DataTable dt_CategoryLink = new DataTable();
        DataTable dt_DetailLinkFromPage = new DataTable();
        DataTable dt_Category = new DataTable();
        DataTable dt = new DataTable();

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
            dt_DetailLinkFromPage = dt_DetailLinkFromPage.DefaultView.ToTable(true, "Link", "Name");

            for (int i = 1; i <= dt_DetailLinkFromPage.Rows.Count; i++)
            {
                Get_DataLink(dt_DetailLinkFromPage.Rows[i - 1]);
                if (IsStop || (cbxSlg.Checked && dt.Rows.Count >= numMaxSlg.Value)) { IsStop = false; break; }
                (Application.OpenForms["frmMain"] as frmMain).SetPercentProcess(i, dt_DetailLinkFromPage.Rows.Count);
                stt3.Text = dt.Rows[i-1][0].ToString();
                stt4.Text = string.Format("Số trang đã duyệt: {0:##.##}/{1}... {2:#0.#}%", i, dt_DetailLinkFromPage.Rows.Count, (double)i * 100 / dt_DetailLinkFromPage.Rows.Count);
                Application.DoEvents();                
            }
            
            // FINISH
            btnRun.Text = "GET INFO";
            IsRun = false;
            string filename = @"Export\Export_ViecLamTV_TimViec " + txtFileName.Text + " " + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".xlsx";
            FuncHelp.ExportExcel(dt, filename);
            Application.DoEvents();
            (Application.OpenForms["frmMain"] as frmMain).NoProcess();

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

        void Get_Category()
        {
            stt1.Visible = true; stt1.Text = "Đang lấy dữ liệu danh mục...";
            Application.DoEvents();

            string s = FuncHelp.GetSource(host + "/timkiem"), l, n, c;
            s = FuncHelp.CutFromTo(s, "id='tabs2'", "id='tabs3'");

            while(s.IndexOf("<td>")>0)
            {
                s = FuncHelp.CutFrom(s, "<td>");
                l = FuncHelp.CutFromTo(s, "<a href='", "'");
                n = FuncHelp.CutFromTo(s, ">", "</a>");
                c = FuncHelp.CutFromTo(s, "<b>(", ")</b>");
                dt_Category.Rows.Add(l, n, n + " (" + c + ")");
            }

            dt_Category.DefaultView.Sort = "Name ASC";
            dt_Category = dt_Category.DefaultView.ToTable();

            chkListCategory.Items.Clear();
            for (int i = 0; i < dt_Category.Rows.Count; i++)
                chkListCategory.Items.Add(dt_Category.Rows[i]["NameWithCount"]);
            chkListCategory.Enabled = true;
            stt1.Text = "Đã lấy được " + dt_Category.Rows.Count.ToString() + " danh mục.";

        }

        void Get_LinkDetailFromPage(DataRow link)
        {
            stt3.Visible = true;
            stt4.Visible = true;

            string s = FuncHelp.GetSource(host + link["Link"].ToString());
            s = FuncHelp.CutFromTo(s, "<div id='main-left'>", "</table>");
            string tmp = FuncHelp.CutFromTo(s, "<div class='title-h1'><h1>", "</div>");
            tmp = FuncHelp.CutFromTo(tmp, "<b>", "</b>").Replace("(", "").Replace(")", "").Replace(",", "").Replace(".", "");

            int totalitem = Convert.ToInt32(tmp);
            int page_max = totalitem;
            page_max = Convert.ToInt32(page_max / 20) + (page_max % 20 > 0 ? 1 : 0);
            if (page_max < 1) { return; }

            //stt3.Text = string.Format("Tổng số ứng viên thật: {0:##,##} Tổng trang: {1:##,##}", Convert.ToInt32(s), pagemax);

            for (int i = 1; i <= page_max; i++)
            {
                if (i > 1) 
                { 
                    s = FuncHelp.GetSource(host + link["Link"].ToString() + "page" + i.ToString());
                    s = FuncHelp.CutFromTo(s, "<div id='main-left'>", "</table>");
                }
                bool IsBreak = false;
                while (s.IndexOf("<tr>") > 0)
                {
                    if (IsStop) { IsBreak = true; break; }
                    if (cbxSlg.Checked && numMaxSlg.Value <= dt_DetailLinkFromPage.Rows.Count) { IsBreak = true; break; }
                    s = FuncHelp.CutFrom(s, "<tr>");
                    string temp = FuncHelp.CutTo(s, "</tr>");
                    string li = FuncHelp.CutFromTo(temp, "<a href='", "'");
                    string da = temp.Substring(temp.LastIndexOf("<td>"), temp.Length - temp.LastIndexOf("<td>")).Trim();
                    da = Regex.Replace(da, @"<[^>]+>|&nbsp;", "").Trim();
                    DateTime dtime;
                    if (!DateTime.TryParseExact(da, "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtime)) { dt_DetailLinkFromPage.Rows.Add(li, link["Name"]); continue; };
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
            string s = FuncHelp.GetSource(host + link["Link"].ToString());
            if (s == "") { IsStop = true; return; }
            
            s = FuncHelp.CutFrom(s, "<div id='main-left'>");
            s = WebUtility.HtmlDecode(s);

            string vitrituyendung = FuncHelp.CutFromTo(s, "Vị trí tuyển dụng:</b></td>", "</td>").Trim();
            string chucvu = FuncHelp.CutFromTo(s, "Chức vụ:</b></td>", "</td>").Trim();
            string yeucaugioitinh = FuncHelp.CutFromTo(s, "Yêu cầu giới tính:</b></td>", "</td>").Trim();            
            string danhmuc = link["Name"].ToString();
            string diadiemlamviec = FuncHelp.CutFromTo(s, "Địa điểm làm việc:</b></td>", "</td>");
            string yeucaudotuoi = FuncHelp.CutFromTo(s, "Yêu cầu độ tuổi:</b></td>", "</td>");
            string mucluong = FuncHelp.CutFromTo(s, "Mức lương:</b></td>", "</td>").Trim();
            string bangcaptoithieu = FuncHelp.CutFromTo(s, "Bằng cấp tối thiểu:</b></td>", "</td>").Trim();
            string hinhthuclamviec = FuncHelp.CutFromTo(s, "Hình thức làm việc:</b></td>", "</td>").Trim();
            string kingnghiemtoithieu = FuncHelp.CutFromTo(s, "Kinh nghiệm tối thiểu:</b></td>", "</td>").Trim();
            string motacongviec = FuncHelp.CutFromTo(s, "Mô tả công việc:</b></td>", "</td>").Trim();
            string yeucaukhac = FuncHelp.CutFromTo(s, "Yêu cầu khác:</b></td>", "</td>").Trim();
            string hosobaogom = FuncHelp.CutFromTo(s, "Hồ sơ bao gồm:</b></td>", "</td>").Trim();
            string hannophoso = FuncHelp.CutFromTo(s, "Hạn nộp HS:</b></td>", "</td>").Trim();
            string ngaydangtuyen = FuncHelp.CutFromTo(s, "Ngày đăng tuyển:</b></td>", "</td>").Trim();
            string hinhthucnophoso = FuncHelp.CutFromTo(s, "Hình thức nộp hồ sơ:</b></td>", "</td>").Trim();
            string soluotxem = FuncHelp.CutFromTo(s, "Số lượt xem:</b></td>", "</td>").Trim();
            string nganhnghetuyendung = FuncHelp.CutFromTo(s, "Ngành nghề tuyển dụng</b></td>", "</td>").Trim();
            string nguoilienhe = FuncHelp.CutFromTo(s, "Người liên hệ:</b></td>", "</td>").Trim();
            string diachilienhe = FuncHelp.CutFromTo(s, "Địa chỉ liên hệ:</b></td>", "</td>").Trim();
            string emaillienhe = FuncHelp.CutFromTo(s, "Email liên hệ:</b></td>", "</td>").Trim();
            string dienthoailienhe = FuncHelp.CutFromTo(s, "Điện thoại liên hệ:</b></td>", "</td>").Trim();
            string tencongty = FuncHelp.CutFromTo(s, "Tên công ty:</b></td>", "</td>").Trim();
            string diachicongty = FuncHelp.CutFromTo(s, "Địa chỉ</b></td>", "</td>").Trim();
            string website = FuncHelp.CutFromTo(s, "Website:</b></td>", "</td>").Trim();
            string dienthoaicongty = FuncHelp.CutFromTo(s, "Điện thoại:</b></td>", "</td>").Trim();
            string gioithieucongty = FuncHelp.CutFromTo(s, "Giới thiệu:</b>", "</tr>").Trim();

            vitrituyendung = Regex.Replace(vitrituyendung, @"<[^>]+>|&nbsp;", "").Trim();
            chucvu = Regex.Replace(chucvu, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaugioitinh = Regex.Replace(yeucaugioitinh, @"<[^>]+>|&nbsp;", "").Trim();
            diadiemlamviec = Regex.Replace(diadiemlamviec, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaudotuoi = Regex.Replace(yeucaudotuoi, @"<[^>]+>|&nbsp;", "").Trim();
            mucluong = Regex.Replace(mucluong, @"<[^>]+>|&nbsp;", "").Trim();
            bangcaptoithieu = Regex.Replace(bangcaptoithieu, @"<[^>]+>|&nbsp;", "").Trim();
            hinhthuclamviec = Regex.Replace(hinhthuclamviec, @"<[^>]+>|&nbsp;", "").Trim();
            kingnghiemtoithieu = Regex.Replace(kingnghiemtoithieu, @"<[^>]+>|&nbsp;", "").Trim();
            motacongviec = Regex.Replace(motacongviec, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaukhac = Regex.Replace(yeucaukhac, @"<[^>]+>|&nbsp;", "").Trim();
            hosobaogom = Regex.Replace(hosobaogom, @"<[^>]+>|&nbsp;", "").Trim();

            hannophoso = Regex.Replace(hannophoso, @"<[^>]+>|&nbsp;", "").Trim();
            ngaydangtuyen = Regex.Replace(ngaydangtuyen, @"<[^>]+>|&nbsp;", "").Trim();
            hinhthucnophoso = Regex.Replace(hinhthucnophoso, @"<[^>]+>|&nbsp;", "").Trim();
            soluotxem = Regex.Replace(soluotxem, @"<[^>]+>|&nbsp;", "").Trim();
            nganhnghetuyendung = Regex.Replace(nganhnghetuyendung, @"<[^>]+>|&nbsp;", "").Trim();
            nguoilienhe = Regex.Replace(nguoilienhe, @"<[^>]+>|&nbsp;", "").Trim();
            diachilienhe = Regex.Replace(diachilienhe, @"<[^>]+>|&nbsp;", "").Trim();
            emaillienhe = Regex.Replace(emaillienhe, @"<[^>]+>|&nbsp;", "").Trim();
            dienthoailienhe = Regex.Replace(dienthoailienhe, @"<[^>]+>|&nbsp;", "").Trim();
            tencongty = Regex.Replace(tencongty, @"<[^>]+>|&nbsp;", "").Trim();
            diachicongty = Regex.Replace(diachicongty, @"<[^>]+>|&nbsp;", "").Trim();
            website = Regex.Replace(website, @"<[^>]+>|&nbsp;", "").Trim();
            dienthoaicongty = Regex.Replace(dienthoaicongty, @"<[^>]+>|&nbsp;", "").Trim();
            gioithieucongty = Regex.Replace(gioithieucongty, @"<[^>]+>|&nbsp;", "").Trim();

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = vitrituyendung; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = chucvu; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = yeucaugioitinh; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = diadiemlamviec; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = yeucaudotuoi; }
            if (chkListBox.GetItemChecked(5)) { i++; dr[i] = mucluong; }
            if (chkListBox.GetItemChecked(6)) { i++; dr[i] = bangcaptoithieu; }
            if (chkListBox.GetItemChecked(7)) { i++; dr[i] = hinhthuclamviec; }
            if (chkListBox.GetItemChecked(8)) { i++; dr[i] = kingnghiemtoithieu; }
            if (chkListBox.GetItemChecked(9)) { i++; dr[i] = motacongviec; }
            if (chkListBox.GetItemChecked(10)) { i++; dr[i] = yeucaukhac; }
            if (chkListBox.GetItemChecked(11)) { i++; dr[i] = hosobaogom; }

            if (chkListBox.GetItemChecked(12)) { i++; dr[i] = hannophoso; }
            if (chkListBox.GetItemChecked(13)) { i++; dr[i] = ngaydangtuyen; }
            if (chkListBox.GetItemChecked(14)) { i++; dr[i] = hinhthucnophoso; }
            if (chkListBox.GetItemChecked(15)) { i++; dr[i] = soluotxem; }
            if (chkListBox.GetItemChecked(16)) { i++; dr[i] = nganhnghetuyendung; }
            if (chkListBox.GetItemChecked(17)) { i++; dr[i] = nguoilienhe; }
            if (chkListBox.GetItemChecked(18)) { i++; dr[i] = diachilienhe; }
            if (chkListBox.GetItemChecked(19)) { i++; dr[i] = emaillienhe; }
            if (chkListBox.GetItemChecked(20)) { i++; dr[i] = dienthoailienhe; }
            if (chkListBox.GetItemChecked(21)) { i++; dr[i] = tencongty; }
            if (chkListBox.GetItemChecked(22)) { i++; dr[i] = diachicongty; }
            if (chkListBox.GetItemChecked(23)) { i++; dr[i] = website; }
            if (chkListBox.GetItemChecked(24)) { i++; dr[i] = dienthoaicongty; }
            if (chkListBox.GetItemChecked(25)) { i++; dr[i] = gioithieucongty; }
            if (chkListBox.GetItemChecked(26)) { i++; dr[i] = danhmuc; }

            dt.Rows.Add(dr);
        }

        private void frmM1_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            Get_Category();
        }

        private void chxGetAllCategory_CheckedChanged(object sender, EventArgs e)
        {
            chkListCategory.Enabled = !chxGetAllCategory.Checked;
        }

    }
}
