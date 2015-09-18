using DllWebBrowser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmM4 : Form
    {

        #region # INIT #

        public frmM4()
        {
            InitializeComponent();           
        }

        private void frmM1_Load(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            numMaxSlg.Value = 100;
            stt1.Text = "Đang lấy tất cả các danh mục...";
            Application.DoEvents();

            Get_Category();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListBox.Items.Count; i++)
                chkListBox.SetItemChecked(i, checkBox2.Checked);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            numMaxSlg.Enabled = cbxSlg.Checked;
        }

        #endregion

        bool IsStop = false, IsRun = false;
        string host = "http://www.danhbadoanhnghiep.vn/";
        List<string> ArrayDetailLink = new List<string>();
        DataTable dt = new DataTable();
        DataTable dtCategoryLink = new DataTable();
        WebBrowser wbr = null;
        WebFormPopup wfp = new WebFormPopup();

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (IsRun) { IsStop = true; return; }
            if (!CheckInput()) { return; }

            wbr = wfp.WebD;
            btnRun.Text = "STOP";
            IsRun = true;
            stt2.Visible = true;
            stt2.Text = "Đang lấy các link của trang...";
            Application.DoEvents();
            Get_TotalLink();

            stt2.Text = string.Format(stt2.Tag as string, ArrayDetailLink.Count);
            progressBar1.Visible = true;
        
            stt3.Visible = true;
            progressBar1.Maximum = ArrayDetailLink.Count;
            dt.Rows.Clear();
            for (int i = 1; i <= ArrayDetailLink.Count; i++) { Get_DataLink(ArrayDetailLink[i - 1]); progressBar1.Value = i; stt3.Text = string.Format(stt3.Tag as string, i); Application.DoEvents(); if (IsStop || (cbxSlg.Checked && dt.Rows.Count >= numMaxSlg.Value)) { IsStop = false; break; } }
            
            // FINISH
            btnRun.Text = "GET INFO";
            IsRun = false;
            progressBar1.Visible = false;
            string filename = "Export_ctyvietnam.xlsx";
            Dictionary<int, int> colw = new Dictionary<int, int>();
            colw.Add(1, 70);
            FuncHelp.ExportExcel(dt, filename, colw);

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
            ArrayDetailLink.Clear();
            dt.Columns.Clear();
            foreach (Object i in chkListBox.CheckedItems) { dt.Columns.Add(i.ToString()); }            

            return true;
        }        

        void Get_Category()
        {
            dtCategoryLink.Columns.Add("LinkCate");
            dtCategoryLink.Columns.Add("NameCate");

            string s = FuncHelp.GetSource(host),l,n;
            s = FuncHelp.CutFromTo(s, "<select name='slt_industry'>", "</select>");

            while (s.IndexOf("<option") >= 0)
            {
                s = FuncHelp.CutFrom(s, "<option");
                l = FuncHelp.CutFromTo(s, "'", "'"); l = host + "/adresult.asp?slt_industry=" + l;
                n = FuncHelp.CutFromTo(s, "'>", "</option>");
                dtCategoryLink.Rows.Add(l, n);
            }

            dtCategoryLink.Rows.RemoveAt(0);
            dtCategoryLink.DefaultView.Sort = "NameCate ASC";

            cbCategory.DataSource = dtCategoryLink.DefaultView.ToTable();
            cbCategory.DisplayMember = "NameCate";
            cbCategory.ValueMember = "LinkCate";

            stt1.Text = string.Format(stt1.Tag as string, dtCategoryLink.Rows.Count);
        }

        void Get_CategoryOne(string link)
        {
            DllWbr.Redirect(wbr, link);
            bool IsBreak = false;

            while(!IsBreak)
            {
                // Add DetailLink
                string s = wbr.Document.Body.InnerHtml, l;
                if (!string.IsNullOrEmpty(s)) { s = s.Replace("\n", "").Replace("\t", "").Replace("\"", "'").Replace("  ", " ").Replace("  ", " ").Replace("  ", " "); }
                while (s.IndexOf("./HosoCty.asp?idproduct=") >= 0)
                {
                    if (cbxSlg.Checked && ArrayDetailLink.Count >= numMaxSlg.Value) { IsBreak = true; break; }   
                    s = FuncHelp.CutFrom(s, "./HosoCty.asp?idproduct=");
                    l = FuncHelp.CutTo(s, "'");
                    ArrayDetailLink.Add(l);
                }
                if (IsBreak) { break; }

                // Find Next
                HtmlElement next = null;
                foreach (HtmlElement elem in wbr.Document.GetElementsByTagName("a"))
                    if (elem.InnerText != null)
                        if (elem.InnerText.Trim().ToLower() == "next") { next = elem; }
                if (next != null)
                {
                    next.InvokeMember("click");
                    DllWbr.WaitForPageLoad(ref wbr);
                    //Application.DoEvents();
                }
                else { IsBreak = true; break; }
            }
        }

        void Get_TotalLink()
        {
            ArrayDetailLink.Clear();
            wfp.Show();
            if(cbxGetFromCategory.Checked)
            {
                Get_CategoryOne(cbCategory.SelectedValue.ToString());
            }
            else
            {
                progressBar1.Visible = true;
                progressBar1.Maximum = dtCategoryLink.Rows.Count;
                for(int i=1; i<=dtCategoryLink.Rows.Count; i++)
                {
                    Get_CategoryOne(dtCategoryLink.Rows[i - 1][0].ToString());
                    progressBar1.Value = i;
                }
            }
            Application.DoEvents();
            wfp.Hide();
        }

        void Get_DataLink(string link)
        {
            string s = FuncHelp.GetSource(host+"/HosoCty.asp?idproduct="+link);
            s = FuncHelp.CutFromTo(s, "Thông tin doanh nghiệp", "</table>").Replace("\r","").Replace("&nbsp;","").Replace("<dd>","");

            string tentiengviet = FuncHelp.CutFromTo(s, "<dt><label>Tên tiếng việt</label></dt>", "</dd>").Trim();
            string tentienganh = FuncHelp.CutFromTo(s, "<dt><label>Tên tiếng anh</label></dt>", "</dd>").Trim();
            string tenviettat = FuncHelp.CutFromTo(s, "<dt><label>Tên viết tắt</label></dt>", "</dd>").Trim();
            string giamdoc = FuncHelp.CutFromTo(s, "<dt><label>Giám đốc</label></dt>", "</dd>").Trim();
            string diachi = FuncHelp.CutFromTo(s, "<dt><label>Địa chỉ</label></dt>", "</dd>").Trim();
            string tinhtp = FuncHelp.CutFromTo(s, "<dt><label>Tỉnh/TP</label></dt>", "</dd>").Trim();
            string loaihinh = FuncHelp.CutFromTo(s, "<dt><label>Loại hình</label></dt>", "</dd>").Trim();
            string nganh = FuncHelp.CutFromTo(s, "<dt><label>Ngành</label></dt>", "</dd>").Trim();
            string phone = FuncHelp.CutFromTo(s, "<dt><label>Telephone</label></dt>", "</dd>").Trim();
            string fax = FuncHelp.CutFromTo(s, "<dt><label>Fax</label></dt>", "</dd>").Trim();
            string email = FuncHelp.CutFromTo(s, "<dt><label>Email</label></dt>", "</dd>").Trim();
            string website = FuncHelp.CutFromTo(s, "<dt><label>Website</label></dt>", "</dd>").Trim();
            string hoatdong = FuncHelp.CutFromTo(s, "<dt><label>Hoạt động</label></dt>", "</dd>").Trim();

            email = Regex.Replace(email, @"<[^>]+>|&nbsp;", "").Trim();
            website = Regex.Replace(website, @"<[^>]+>|&nbsp;", "").Trim();

            //tencongty = WebUtility.HtmlDecode(tencongty);
            //linhvuckinhdoanh = WebUtility.HtmlDecode(linhvuckinhdoanh);
            //diachi = WebUtility.HtmlDecode(diachi);
            //dienthoai = WebUtility.HtmlDecode(dienthoai);
            //gioithieu = WebUtility.HtmlDecode(gioithieu);

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = tentiengviet; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = tentienganh; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = tenviettat; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = giamdoc; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = diachi; }
            if (chkListBox.GetItemChecked(5)) { i++; dr[i] = tinhtp; }
            if (chkListBox.GetItemChecked(6)) { i++; dr[i] = loaihinh; }
            if (chkListBox.GetItemChecked(7)) { i++; dr[i] = nganh; }
            if (chkListBox.GetItemChecked(8)) { i++; dr[i] = phone; }
            if (chkListBox.GetItemChecked(9)) { i++; dr[i] = fax; }
            if (chkListBox.GetItemChecked(10)) { i++; dr[i] = email; }
            if (chkListBox.GetItemChecked(11)) { i++; dr[i] = website; }
            if (chkListBox.GetItemChecked(12)) { i++; dr[i] = hoatdong; }

            dt.Rows.Add(dr);
        }

        private void cbxGetFromCategory_CheckedChanged(object sender, EventArgs e)
        {
            cbCategory.Enabled = cbxGetFromCategory.Checked;
        }
    }
}
