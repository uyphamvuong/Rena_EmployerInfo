using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmM3 : Form
    {

        #region # INIT #

        public frmM3()
        {
            InitializeComponent();           
        }

        private void frmM1_Load(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            numMaxSlg.Value = 100;
            stt1.Text = "Đang lấy tổng số phân trang...";
            Application.DoEvents();

            Get_TotalPage();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListBox.Items.Count; i++)
                chkListBox.SetItemChecked(i, checkBox2.Checked);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Enabled = cbxPageLimit.Checked;
            label3.Enabled = cbxPageLimit.Checked;
            cbxMin.Enabled = cbxPageLimit.Checked;
            cbxMax.Enabled = cbxPageLimit.Checked;
        }        

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            numMaxSlg.Enabled = cbxSlg.Checked;
        }

        #endregion

        bool IsStop = false, IsRun = false;
        string host = "http://www.ctyvietnam.com/";
        int maxPage = 0;
        List<string> ArrayDetailLink = new List<string>();
        DataTable dt = new DataTable();

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (IsRun) { IsStop = true; return; }
            if (!CheckInput()) { return; }

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

        void Get_TotalPage()
        {
            string s = FuncHelp.GetSource(host+"/category/viet-nam");
            s = FuncHelp.CutFromTo(s, "wp-pagenavi", "fixed");
            int max = 0;

            if (s.LastIndexOf("<a href=") >= 0)
            {
                s = s.Substring(s.LastIndexOf("<a href="), s.Length - s.LastIndexOf("<a href="));
                s = FuncHelp.CutFromTo(s, "page/", "'");
                max = int.Parse(s);
            }
            maxPage = max;

            stt1.Text = string.Format(stt1.Tag as string, maxPage);

            for (int i = 0; i < max; i++) { cbxMin.Items.Add(i + 1); cbxMax.Items.Add(i + 1); }
            if (max > 0) { cbxMin.SelectedItem = 1; cbxMax.SelectedItem = max; }

        }

        void Get_TotalLink()
        {
            string l;
            int min = 1, max = maxPage;
            if (cbxPageLimit.Checked) { min = (int)cbxMin.SelectedItem; max = (int)cbxMax.SelectedItem; }
            ArrayDetailLink.Clear();
            for(int i=min;i<=max;i++)
            {
                string s = FuncHelp.GetSource(host + "/category/viet-nam/page/" + i.ToString());
                s = FuncHelp.CutFromTo(s, "<div id='main'>", "<div id='pagenavi'>");
                while (s.IndexOf("ds-thumb alignleft") >= 0)
                {
                    s = FuncHelp.CutFrom(s, "<a href='");
                    l = FuncHelp.CutTo(s, "'");
                    if (cbxSlg.Checked && ArrayDetailLink.Count >= numMaxSlg.Value) { IsStop = true; break; }                  
                    ArrayDetailLink.Add(l);
                    s = FuncHelp.CutFrom(s, "<a href='");
                }
                stt2.Text = string.Format(stt2.Tag as string, ArrayDetailLink.Count);
                Application.DoEvents();
                if (IsStop) { IsStop = false; break; }
            }
        }

        void Get_DataLink(string link)
        {
            string s = FuncHelp.GetSource(link);
            s = FuncHelp.CutFrom(s, "<div class='post'");
            s = FuncHelp.CutFromTo(s, "<div class='post'", "id='viewdetail'");

            string tencongty = FuncHelp.CutFromTo(s, "<h2>", "<div");
            string tenviettat = FuncHelp.CutFromTo(s, "Tên viết tắt: ", "</p>");
            string linhvuckinhdoanh = FuncHelp.CutFromTo(s, "Lĩnh vực kinh doanh : ", "</p>");
            string diachi = FuncHelp.CutFromTo(s, "Địa chỉ : ", "</p>");
            string dienthoai = FuncHelp.CutFromTo(s, "Điện thoại : ", "</p>");
            string fax = FuncHelp.CutFromTo(s, "Fax : ", "</p>");
            string email = FuncHelp.CutFromTo(s, "data-cfemail='", "'>"); email = GiaiMaEmail(email);
            string website = FuncHelp.CutFromTo(s, "Website : ", "</p>");
            string nguoidaidien = FuncHelp.CutFromTo(s, "Người đại diện : ", "</p>");
            string gioithieu = FuncHelp.CutFromTo(s, "<p> Giới thiệu ", "</p>");

            gioithieu = Regex.Replace(gioithieu, @"<[^>]+>|&nbsp;", "").Trim();
            website = Regex.Replace(website, @"<[^>]+>|&nbsp;", "").Trim();

            tencongty = WebUtility.HtmlDecode(tencongty);
            linhvuckinhdoanh = WebUtility.HtmlDecode(linhvuckinhdoanh);
            diachi = WebUtility.HtmlDecode(diachi);
            dienthoai = WebUtility.HtmlDecode(dienthoai);
            gioithieu = WebUtility.HtmlDecode(gioithieu);

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = tencongty; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = tenviettat; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = linhvuckinhdoanh; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = diachi; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = dienthoai; }
            if (chkListBox.GetItemChecked(5)) { i++; dr[i] = fax; }
            if (chkListBox.GetItemChecked(6)) { i++; dr[i] = email; }
            if (chkListBox.GetItemChecked(7)) { i++; dr[i] = website; }
            if (chkListBox.GetItemChecked(8)) { i++; dr[i] = nguoidaidien; }
            if (chkListBox.GetItemChecked(9)) { i++; dr[i] = gioithieu; }

            dt.Rows.Add(dr);
        }

        private string GiaiMaEmail(string a)
        {
            try
            {
                string s;
                int j, r, c;

                if (!string.IsNullOrEmpty(a))
                {
                    s = "";
                    r = int.Parse(a.Substring(0, 2), NumberStyles.HexNumber);
                    for (j = 2; a.Length > j; j += 2)
                    {
                        c = int.Parse(a.Substring(j, 2), NumberStyles.HexNumber) ^ r;
                        s += Char.ConvertFromUtf32(c);
                    }
                    return s;
                }
            }
            catch{ }
            return "";
        }
    }
}
