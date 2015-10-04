using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmVieclamtvUngVien : Form
    {

        #region # INIT #

        public frmVieclamtvUngVien()
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
            string filename = @"Export\Export_ViecLamTV_UngVien " + txtFileName.Text + " " + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".xlsx";
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

            string s = FuncHelp.GetSource(host + "/timungvien/"), l, n, c;
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
                    dt_DetailLinkFromPage.Rows.Add(li, link["Name"]);                  
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

            string hovaten = FuncHelp.CutFromTo(s, "Họ và tên:</b></td>", "</td>").Trim();
            string ngaysinh = FuncHelp.CutFromTo(s, "Ngày sinh:</b></td>", "</td>").Trim();
            string gioitinh = FuncHelp.CutFromTo(s, "Giới tính:</b></td>", "</td>").Trim();            
            string danhmuc = link["Name"].ToString();
            string trinhdohocvan = FuncHelp.CutFromTo(s, "Trình độ học vấn:</b></td>", "</td>");
            string totnghiepnam = FuncHelp.CutFromTo(s, "Tốt nghiệp năm:</b></td>", "</td>");
            string nganhhoc = FuncHelp.CutFromTo(s, "Ngành học:</b></td>", "</td>").Trim();
            string totnghieploai = FuncHelp.CutFromTo(s, "Tốt nghiệp loại:</b></td>", "</td>").Trim();
            string totnghieptaitruong = FuncHelp.CutFromTo(s, "Tối nghiệp tại trừờng:</b></td>", "</td>").Trim();
            string ngoaingu = FuncHelp.CutFromTo(s, "Ngoại ngữ:</b></td>", "</td>").Trim();
            string trinhdotinhoc = FuncHelp.CutFromTo(s, "Trình độ tin học:</b></td>", "</td>").Trim();
            string bangchungchikhac = FuncHelp.CutFromTo(s, "Bằng/chứng chỉ khác:</b></td>", "</td>").Trim();
            string soluotxem = FuncHelp.CutFromTo(s, "Số lượt xem:</b></td>", "</td>").Trim();
            string sonamkinhnghiem = FuncHelp.CutFromTo(s, "Số năm kinh nghiệm:</b></td>", "</td>").Trim();
            string kinhnghiem = FuncHelp.CutFromTo(s, "<b>Kinh nghiệm</b></td>", "</td>").Trim();
            string cackynang = FuncHelp.CutFromTo(s, "Các kỹ năng:</b></td>", "</td>").Trim();
            string vitrimongmuon = FuncHelp.CutFromTo(s, "Vị trí mong muốn:</b></td>", "</td>").Trim();
            string capbac = FuncHelp.CutFromTo(s, "Cấp bậc:</b></td>", "</td>").Trim();
            string nganhnghe = FuncHelp.CutFromTo(s, "Ngành nghề:</b></td>", "</td>").Trim();
            string hinhthuclamviec = FuncHelp.CutFromTo(s, "Hình thức làm việc:</b></td>", "</td>").Trim();
            string diadiemlamviec = FuncHelp.CutFromTo(s, "Địa điểm làm việc:</b></td>", "</td>").Trim();
            string mucluongmongmuon = FuncHelp.CutFromTo(s, "Mức lương mong muốn:</b></td>", "</td>").Trim();
            string muctieunghenghiep = FuncHelp.CutFromTo(s, "Mục tiêu nghề nghiệp:</b></td>", "</td>").Trim();
            string tennglienhe = FuncHelp.CutFromTo(s, "Tên người liên hệ:</b></td>", "</td>").Trim();
            string email = FuncHelp.CutFromTo(s, "<b>Email:</b></td>", "</td>").Trim();
            string dienthoai = FuncHelp.CutFromTo(s, "<b>Điện thoại:</b></td>", "</td>").Trim();

            //vitrituyendung = Regex.Replace(vitrituyendung, @"<[^>]+>|&nbsp;", "").Trim();
            hovaten = Regex.Replace(hovaten, @"<[^>]+>|&nbsp;", "").Trim();
            ngaysinh = Regex.Replace(ngaysinh, @"<[^>]+>|&nbsp;", "").Trim();
            gioitinh = Regex.Replace(gioitinh, @"<[^>]+>|&nbsp;", "").Trim();
            trinhdohocvan = Regex.Replace(trinhdohocvan, @"<[^>]+>|&nbsp;", "").Trim();
            totnghiepnam = Regex.Replace(totnghiepnam, @"<[^>]+>|&nbsp;", "").Trim();
            nganhhoc = Regex.Replace(nganhhoc, @"<[^>]+>|&nbsp;", "").Trim();
            totnghieploai = Regex.Replace(totnghieploai, @"<[^>]+>|&nbsp;", "").Trim();
            totnghieptaitruong = Regex.Replace(totnghieptaitruong, @"<[^>]+>|&nbsp;", "").Trim();
            ngoaingu = Regex.Replace(ngoaingu, @"<[^>]+>|&nbsp;", "").Trim();
            trinhdotinhoc = Regex.Replace(trinhdotinhoc, @"<[^>]+>|&nbsp;", "").Trim();
            bangchungchikhac = Regex.Replace(bangchungchikhac, @"<[^>]+>|&nbsp;", "").Trim();
            soluotxem = Regex.Replace(soluotxem, @"<[^>]+>|&nbsp;", "").Trim();
            sonamkinhnghiem = Regex.Replace(sonamkinhnghiem, @"<[^>]+>|&nbsp;", "").Trim();
            kinhnghiem = Regex.Replace(kinhnghiem, @"<[^>]+>|&nbsp;", "").Trim();
            cackynang = Regex.Replace(cackynang, @"<[^>]+>|&nbsp;", "").Trim();
            vitrimongmuon = Regex.Replace(vitrimongmuon, @"<[^>]+>|&nbsp;", "").Trim();
            capbac = Regex.Replace(capbac, @"<[^>]+>|&nbsp;", "").Trim();
            nganhnghe = Regex.Replace(nganhnghe, @"<[^>]+>|&nbsp;", "").Trim();
            hinhthuclamviec = Regex.Replace(hinhthuclamviec, @"<[^>]+>|&nbsp;", "").Trim();
            diadiemlamviec = Regex.Replace(diadiemlamviec, @"<[^>]+>|&nbsp;", "").Trim();
            mucluongmongmuon = Regex.Replace(mucluongmongmuon, @"<[^>]+>|&nbsp;", "").Trim();
            muctieunghenghiep = Regex.Replace(muctieunghenghiep, @"<[^>]+>|&nbsp;", "").Trim();
            tennglienhe = Regex.Replace(tennglienhe, @"<[^>]+>|&nbsp;", "").Trim();
            email = Regex.Replace(email, @"<[^>]+>|&nbsp;", "").Trim();
            dienthoai = Regex.Replace(dienthoai, @"<[^>]+>|&nbsp;", "").Trim();

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = hovaten; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = ngaysinh; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = gioitinh; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = trinhdohocvan; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = totnghiepnam; }
            if (chkListBox.GetItemChecked(5)) { i++; dr[i] = nganhhoc; }
            if (chkListBox.GetItemChecked(6)) { i++; dr[i] = totnghieploai; }
            if (chkListBox.GetItemChecked(7)) { i++; dr[i] = totnghieptaitruong; }
            if (chkListBox.GetItemChecked(8)) { i++; dr[i] = ngoaingu; }
            if (chkListBox.GetItemChecked(9)) { i++; dr[i] = trinhdotinhoc; }
            if (chkListBox.GetItemChecked(10)) { i++; dr[i] = bangchungchikhac; }
            if (chkListBox.GetItemChecked(11)) { i++; dr[i] = soluotxem; }
            if (chkListBox.GetItemChecked(12)) { i++; dr[i] = sonamkinhnghiem; }
            if (chkListBox.GetItemChecked(13)) { i++; dr[i] = kinhnghiem; }
            if (chkListBox.GetItemChecked(14)) { i++; dr[i] = cackynang; }
            if (chkListBox.GetItemChecked(15)) { i++; dr[i] = vitrimongmuon; }
            if (chkListBox.GetItemChecked(16)) { i++; dr[i] = capbac; }
            if (chkListBox.GetItemChecked(17)) { i++; dr[i] = nganhnghe; }
            if (chkListBox.GetItemChecked(18)) { i++; dr[i] = hinhthuclamviec; }
            if (chkListBox.GetItemChecked(19)) { i++; dr[i] = diadiemlamviec; }
            if (chkListBox.GetItemChecked(20)) { i++; dr[i] = mucluongmongmuon; }
            if (chkListBox.GetItemChecked(21)) { i++; dr[i] = muctieunghenghiep; }
            if (chkListBox.GetItemChecked(22)) { i++; dr[i] = tennglienhe; }
            if (chkListBox.GetItemChecked(23)) { i++; dr[i] = email; }
            if (chkListBox.GetItemChecked(24)) { i++; dr[i] = dienthoai; }
            if (chkListBox.GetItemChecked(25)) { i++; dr[i] = danhmuc; }

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
