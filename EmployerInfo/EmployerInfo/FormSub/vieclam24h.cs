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
        List<string> ArrayCategoryLink = new List<string>();
        List<string> ArrayPageLinkFromCategory = new List<string>();
        List<string> ArrayDetailLinkFromPage = new List<string>();
        DataTable dt_Category = new DataTable();
        DataTable dt = new DataTable();

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (IsRun) { IsStop = true; return; }
            if (!CheckInput()) { return; }

            //if (radioButton2.Checked) { ArrayCategoryLink.Add(cbxCategory.SelectedValue as string); }
            //else
            //{
            //    for (int i = 0; i < dt_Category.Rows.Count; i++)
            //        ArrayCategoryLink.Add(dt_Category.Rows[0][0] as string);
            //}

            btnRun.Text = "STOP";
            IsRun = true;

            // Thông báo số link đã tiếp nhận
            stt1.Visible = true;
            stt1.Text = string.Format(stt1.Tag as string, ArrayCategoryLink.Count);
            progressBar1.Visible = true;
            
            stt2.Visible = true;
            progressBar1.Maximum = ArrayCategoryLink.Count;
            for (int i = 1; i <= ArrayCategoryLink.Count; i++) { Get_LinkPageFromCategory(ArrayCategoryLink[i - 1]); progressBar1.Value = i; stt2.Text = string.Format(stt2.Tag as string, ArrayPageLinkFromCategory.Count); Application.DoEvents(); if (cbxSlg.Checked && ArrayPageLinkFromCategory.Count * 20 >= numMaxSlg.Value) { break; } }

            stt3.Visible = true;
            progressBar1.Maximum = ArrayPageLinkFromCategory.Count;
            ArrayDetailLinkFromPage.Clear();
            for (int i = 1; i <= ArrayPageLinkFromCategory.Count; i++) { Get_LinkDetailFromPage(ArrayPageLinkFromCategory[i - 1]); progressBar1.Value = i; stt3.Text = string.Format(stt3.Tag as string, ArrayDetailLinkFromPage.Count); Application.DoEvents(); if (IsStop || (cbxSlg.Checked && ArrayDetailLinkFromPage.Count >= numMaxSlg.Value)) { IsStop = false; break; } }

            ArrayCategoryLink.Clear();
            ArrayPageLinkFromCategory.Clear();            

            stt4.Visible = true;
            progressBar1.Maximum = ArrayDetailLinkFromPage.Count;
            dt.Rows.Clear();

            for (int i = 1; i <= ArrayDetailLinkFromPage.Count; i++)
            {
                Get_DataLink(ArrayDetailLinkFromPage[i - 1]); progressBar1.Value = i;
                stt4.Text = string.Format(stt4.Tag as string, i, ArrayDetailLinkFromPage.Count, (double)i * 100 / ArrayDetailLinkFromPage.Count);
                Application.DoEvents();
                if (IsStop || (cbxSlg.Checked && dt.Rows.Count >= numMaxSlg.Value)) { IsStop = false; break; }
            }
            
            // FINISH
            btnRun.Text = "GET INFO";
            IsRun = false;
            progressBar1.Visible = false;
            string filename = "Export_vieclam24h.xlsx";
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
            ArrayCategoryLink.Clear();
            ArrayPageLinkFromCategory.Clear();
            ArrayDetailLinkFromPage.Clear();
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

            string s = FuncHelp.GetSource(host).Replace(" class ='div_40' style='display:none;'","");

            if (s.IndexOf("category'><li><a href='") > 0)
                s = FuncHelp.CutFrom(s, "<div id='div_ds_nganh_nghe_trang_chu_ntv'>");

            while (s.IndexOf("category'><li><a href='") >= 0)
            {
                string l = host+FuncHelp.CutFromTo(s, "category'><li><a href='", "' title='");
                string n = FuncHelp.CutFromTo(s, "' title='", "'").Replace("Tìm việc làm","").Trim();
                string c = FuncHelp.CutFromTo(s, "<span class='orangeTxt'>", "</span>");
                n = WebUtility.HtmlDecode(n);
                dt_Category.Rows.Add(l, n + " (" + c + ")");
                s = FuncHelp.CutFrom(s, "</ul>");
            }

            dt_Category.DefaultView.Sort = "Name ASC";
            dt_Category = dt_Category.DefaultView.ToTable();          

            cbxCategory.DataSource = dt_Category;
            stt1.Visible = false;
            btnRun.Enabled = true;
        }

        void Get_LinkPageFromCategory(string link)
        {
            string s = FuncHelp.GetSource(link);
            int max = 0;

            if (s.IndexOf("' >Cu&#7889;i</a>") > 0)
            {

                s = FuncHelp.CutTo(s, "' >Cu&#7889;i</a>");
                if (s.LastIndexOf("=") > 0) { max = int.Parse(s.Substring(s.LastIndexOf("=") + 1, s.Length - s.LastIndexOf("=") - 1)); }
                if (s.LastIndexOf("<a href='") > 0) { s = s.Substring(s.LastIndexOf("<a href='") + "<a href='".Length, s.Length - s.LastIndexOf("<a href='") - "<a href='".Length); }
                s = FuncHelp.CutTo(s, "page=");
            }

            if (cbxSlg.Checked && ArrayPageLinkFromCategory.Count*20 >= numMaxSlg.Value) { return; }
            ArrayPageLinkFromCategory.Add(link);
            for (int j = 2; j <= max; j++)
            {
                if (cbxSlg.Checked && ArrayPageLinkFromCategory.Count * 20 >= numMaxSlg.Value) { return; }
                ArrayPageLinkFromCategory.Add(host + s + "page=" + j.ToString());
            }        
        }

        void Get_LinkDetailFromPage(string link)
        {
            string s = FuncHelp.GetSource(link);

            s = FuncHelp.CutFrom(s, "frm_viec_lam_nhieu_nguoi_xem");

            int mint = dtpMin.Value.Month * 100 + dtpMin.Value.Day;
            int maxt = dtpMax.Value.Month * 100 + dtpMax.Value.Day;

            while (s.IndexOf("<td ><a href='") > 0)
            {
                s = FuncHelp.CutFrom(s, "<td ><a href='");
                string l = FuncHelp.CutTo(s, "' title='");

                string t = FuncHelp.CutFrom(s, "</td><td style='text-align:center' >");
                t = FuncHelp.CutFrom(t, "</td><td style='text-align:center' >");
                t = FuncHelp.CutTo(t, "</td>");
                string[] arrt = t.Split('-');

                int tt = int.Parse(arrt[1]) * 100 + int.Parse(arrt[0]);

                if (cbxTimeLimit.Checked && (tt < mint || tt > maxt)) { break; }
                if (cbxSlg.Checked && ArrayDetailLinkFromPage.Count >= numMaxSlg.Value) { break; }
                ArrayDetailLinkFromPage.Add(host + l);
            }
        }

        void Get_DataLink(string link)
        {
            string s = FuncHelp.GetSource(link);
            s = FuncHelp.CutFromTo(s, "colLeft", "iframe_show_danh_gia_binh_luan").Replace(" class='br-L'", "");
            s = WebUtility.HtmlDecode(s);

            string luotxem = FuncHelp.CutFromTo(s, "Lượt xem: <b>", "</b>");
            string ma = FuncHelp.CutFromTo(s, "Mã: <b>", "</b>");
            string ngaylammoi = FuncHelp.CutFromTo(s, "Ngày làm mới: <b>", "</b>");
            string vitrituyendung = FuncHelp.CutFromTo(s, "Vị trí tuyển dụng</b></p></td><td colspan='3'>", "</td>");
            string chucvu = FuncHelp.CutFromTo(s, "Chức vụ</b></p></td><td style='width:200px'>", "</td>");
            string sonamkinhnghiem = FuncHelp.CutFromTo(s, "Số năm kinh nghiệm</b></p></td><td>", "</td>");
            string nganhnghe = FuncHelp.CutFromTo(s, "Ngành nghề</b></p></td><td>", "</td>");
            string yeucaubangcap = FuncHelp.CutFromTo(s, "Yêu cầu bằng cấp</b></p></td><td>", "</td>");
            string hinhthuclamviec = FuncHelp.CutFromTo(s, "Hình thức làm việc</b></p></td><td>", "</td>");
            string yeucaugiotinh = FuncHelp.CutFromTo(s, "Yêu cầu giới tính</b></p></td><td>", "</td>");
            string diadiemlamviec = FuncHelp.CutFromTo(s, "Địa điểm làm việc</b></p></td><td>", "</td>");
            string yeucaudotuoi = FuncHelp.CutFromTo(s, "Yêu cầu độ tuổi</b></p></td><td>", "</td>");
            string mucluong = FuncHelp.CutFromTo(s, "Mức lương</b></p></td><td>", "</td>");
            string soluongcantuyen = FuncHelp.CutFromTo(s, "Số lượng cần tuyển</b></p></td><td>", "</td>");
            string motacongviec = FuncHelp.CutFromTo(s, "Mô tả công việc</b></p></td><td colspan='3'>", "</td>");
            string quyenloiduochuong = FuncHelp.CutFromTo(s, ">Quyền lợi được hưởng</b></p></td><td colspan='3'>", "</td>");
            string yeucaukhac = FuncHelp.CutFromTo(s, "Yêu cầu khác</b></p></td><td colspan='3'>", "</td>");
            string hosobaogom = FuncHelp.CutFromTo(s, "Hồ sơ bao gồm</b></p></td><td colspan='3'>", "</td>");
            string hannophoso = FuncHelp.CutFromTo(s, "Hạn nộp Hồ sơ</b></p></td><td colspan='3'>", "</b></p></td></tr>");
            string hinhthucnophoso = FuncHelp.CutFromTo(s, "Hình thức nộp hồ sơ</b></td><td>", "</td>");
            string nguoilienhe = FuncHelp.CutFromTo(s, "Người liên hệ</b></p></td><td colspan='3'>", "</td>");
            string diachilienhe = FuncHelp.CutFromTo(s, "Địa chỉ liên hệ</b></p></td><td colspan='3'>", "</td>");
            string emaillienhe = FuncHelp.CutFromTo(s, "Email liên hệ</b></p></td><td colspan='3'><p class='mg-0'><a href='mailto:", "' title");
            string dienthoailienhe = FuncHelp.CutFromTo(s, "Điện thoại liên hệ</b></p></td><td colspan='3'>", "</td>");
            string tencongty = FuncHelp.CutFromTo(s, "Tên công ty</b></p></td><td colspan='3'>", "</td>");
            string diachicongty = FuncHelp.CutFromTo(s, "Địa chỉ</b></p></td><td colspan='3'>", "<!--a");
            string websitecongty = FuncHelp.CutFromTo(s, "Website</b></p></td><td colspan='3'>", "</td>");
            string dienthoaicongty = FuncHelp.CutFromTo(s, "Điện thoại</b></td><td colspan='3'>", "</td>");
            string gioithieu = FuncHelp.CutFromTo(s, "div_mota_day_du'>", "<p>");
            string quymo = FuncHelp.CutFromTo(s, "Quy mô công ty</b></p></td><td colspan='3'>", "</td>");

            vitrituyendung = Regex.Replace(vitrituyendung, @"<[^>]+>|&nbsp;", "").Trim();
            chucvu = Regex.Replace(chucvu, @"<[^>]+>|&nbsp;", "").Trim();
            sonamkinhnghiem = Regex.Replace(sonamkinhnghiem, @"<[^>]+>|&nbsp;", "").Trim();
            nganhnghe = Regex.Replace(nganhnghe, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaubangcap = Regex.Replace(yeucaubangcap, @"<[^>]+>|&nbsp;", "").Trim();
            hinhthuclamviec = Regex.Replace(hinhthuclamviec, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaugiotinh = Regex.Replace(yeucaugiotinh, @"<[^>]+>|&nbsp;", "").Trim();
            diadiemlamviec = Regex.Replace(diadiemlamviec, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaudotuoi = Regex.Replace(yeucaudotuoi, @"<[^>]+>|&nbsp;", "").Trim();
            mucluong = Regex.Replace(mucluong, @"<[^>]+>|&nbsp;", "").Trim();
            soluongcantuyen = Regex.Replace(soluongcantuyen, @"<[^>]+>|&nbsp;", "").Trim();
            motacongviec = Regex.Replace(motacongviec, @"<[^>]+>|&nbsp;", "").Trim();
            quyenloiduochuong = Regex.Replace(quyenloiduochuong, @"<[^>]+>|&nbsp;", "").Trim();
            yeucaukhac = Regex.Replace(yeucaukhac, @"<[^>]+>|&nbsp;", "").Trim();
            hosobaogom = Regex.Replace(hosobaogom, @"<[^>]+>|&nbsp;", "").Trim();
            hannophoso = Regex.Replace(hannophoso, @"<[^>]+>|&nbsp;", "").Trim();
            hinhthucnophoso = Regex.Replace(hinhthucnophoso, @"<[^>]+>|&nbsp;", "").Trim();
            nguoilienhe = Regex.Replace(nguoilienhe, @"<[^>]+>|&nbsp;", "").Trim();
            diachilienhe = Regex.Replace(diachilienhe, @"<[^>]+>|&nbsp;", "").Trim();
            emaillienhe = Regex.Replace(emaillienhe, @"<[^>]+>|&nbsp;", "").Trim();
            dienthoailienhe = Regex.Replace(dienthoailienhe, @"<[^>]+>|&nbsp;", "").Trim();
            tencongty = Regex.Replace(tencongty, @"<[^>]+>|&nbsp;", "").Trim();
            diachicongty = Regex.Replace(diachicongty, @"<[^>]+>|&nbsp;", "").Trim();
            websitecongty = Regex.Replace(websitecongty, @"<[^>]+>|&nbsp;", "").Trim();
            dienthoaicongty = Regex.Replace(dienthoaicongty, @"<[^>]+>|&nbsp;", "").Trim();
            gioithieu = Regex.Replace(gioithieu, @"<[^>]+>|&nbsp;", "").Trim();
            quymo = Regex.Replace(quymo, @"<[^>]+>|&nbsp;", "").Trim();

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = luotxem; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = ma; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = ngaylammoi; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = vitrituyendung; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = chucvu; }
            if (chkListBox.GetItemChecked(5)) { i++; dr[i] = sonamkinhnghiem; }
            if (chkListBox.GetItemChecked(6)) { i++; dr[i] = nganhnghe; }
            if (chkListBox.GetItemChecked(7)) { i++; dr[i] = yeucaubangcap; }
            if (chkListBox.GetItemChecked(8)) { i++; dr[i] = hinhthuclamviec; }
            if (chkListBox.GetItemChecked(9)) { i++; dr[i] = yeucaugiotinh; }
            if (chkListBox.GetItemChecked(10)) { i++; dr[i] = diadiemlamviec; }
            if (chkListBox.GetItemChecked(11)) { i++; dr[i] = yeucaudotuoi; }
            if (chkListBox.GetItemChecked(12)) { i++; dr[i] = mucluong; }
            if (chkListBox.GetItemChecked(13)) { i++; dr[i] = soluongcantuyen; }
            if (chkListBox.GetItemChecked(14)) { i++; dr[i] = motacongviec; }
            if (chkListBox.GetItemChecked(15)) { i++; dr[i] = quyenloiduochuong; }
            if (chkListBox.GetItemChecked(16)) { i++; dr[i] = yeucaukhac; }
            if (chkListBox.GetItemChecked(17)) { i++; dr[i] = hosobaogom; }
            if (chkListBox.GetItemChecked(18)) { i++; dr[i] = hannophoso; }
            if (chkListBox.GetItemChecked(19)) { i++; dr[i] = hinhthucnophoso; }
            if (chkListBox.GetItemChecked(20)) { i++; dr[i] = nguoilienhe; }
            if (chkListBox.GetItemChecked(21)) { i++; dr[i] = diachilienhe; }
            if (chkListBox.GetItemChecked(22)) { i++; dr[i] = emaillienhe; }
            if (chkListBox.GetItemChecked(23)) { i++; dr[i] = dienthoailienhe; }
            if (chkListBox.GetItemChecked(24)) { i++; dr[i] = tencongty; }
            if (chkListBox.GetItemChecked(25)) { i++; dr[i] = diachicongty; }
            if (chkListBox.GetItemChecked(26)) { i++; dr[i] = websitecongty; }
            if (chkListBox.GetItemChecked(27)) { i++; dr[i] = dienthoaicongty; }
            if (chkListBox.GetItemChecked(28)) { i++; dr[i] = gioithieu; }
            if (chkListBox.GetItemChecked(29)) { i++; dr[i] = quymo; }


            dt.Rows.Add(dr);
        }

        private void frmM1_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            Get_ArrayLinkCategory();
        }

        private void rbtnChooseOne_CheckedChanged(object sender, EventArgs e)
        {
            cbxPortList.Enabled = rbtnChooseOne.Checked;
        }

    }
}
