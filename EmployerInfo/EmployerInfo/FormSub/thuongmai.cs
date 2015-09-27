using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmM5 : Form
    {

        #region # INIT #

        public frmM5()
        {
            InitializeComponent();           
        }

        private void frmM1_Load(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            numMaxSlg.Value = 100;
            stt1.Text = "Đang lấy tất cả các Menu con của 'Danh Bạ'...";
            Application.DoEvents();

            dtCategoryLink.Columns.Add("LinkCate");
            dtCategoryLink.Columns.Add("TextCate");
            
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
        string host = "http://thuongmai.vn", l;
        List<string> ArrayDetailLink = new List<string>();
        DataTable dt = new DataTable();
        DataTable dtCategoryLink = new DataTable();
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (IsRun) { IsStop = true; return; }
            if (!CheckInput()) { return; }

            btnRun.Text = "STOP";
            IsRun = true;            
            Get_TotalLink();
            if (IsStop)
            {
                btnRun.Text = "GET INFO";
                IsRun = false;
                progressBar1.Visible = false;
                Application.DoEvents();
                return;
            }            

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
            string filename = @"Export\Export_thuongmai.xlsx";
            Dictionary<int, int> colw = new Dictionary<int, int>();
            colw.Add(1, 70);

            dt.DefaultView.Sort = "Tên công ty ASC";
            dt = dt.DefaultView.ToTable();

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
            string s = FuncHelp.GetSource(host);
            s = FuncHelp.CutFromTo(s, "<li class='parent'><a href='javascript:'>Danh Bạ</a><ul>", "</ul></li><li class='parent'>");

            doc.LoadHtml(s);

            cbMenu.Items.Clear();
            foreach(HtmlNode hn in doc.DocumentNode.ChildNodes)
                if(hn.FirstChild!=null)
                    cbMenu.Items.Add(hn.FirstChild.InnerText);
                   
            if (cbMenu.Items.Count > 0) { cbMenu.SelectedIndex = 0; }
        }

        void Get_AllCategoryInMenu()
        {
            stt1.Text = "Đang lấy tất cả các Danh mục của Menu này!";
            Application.DoEvents();

            string s = FuncHelp.GetSource(host + l), li, co, na;
            s = FuncHelp.CutFromTo(s, "views-row-first", "</tbody>");
            

            while (s.IndexOf("<a href='") >=0)
            {
                s = FuncHelp.CutFrom(s, "<a href='");
                li = FuncHelp.CutTo(s, "'>");
                co = FuncHelp.CutFromTo(s, "views-field-nid' >", "</td>").Trim();
                na = FuncHelp.CutFromTo(s, "'>", "</a>");
                na = WebUtility.HtmlDecode(na);                
                dtCategoryLink.Rows.Add(li, na+" ("+co+")");
            }

        }

        void Get_AllLinkInCategory(string link)
        {
            string s = FuncHelp.GetSource(host + link + "?title=&items_per_page=100"), li;
            int i, max = 0;
            if (s.IndexOf("Cuối »") >= 0)
            {
                string ss = "items_per_page=100&amp;page=";
                ss = s.Substring(s.LastIndexOf(ss) + ss.Length, s.Length - s.LastIndexOf(ss) - ss.Length);
                ss = ss.Substring(0, ss.IndexOf("'"));
                max = int.Parse(ss);
            }
            
            for(i=0;i<=max;i++)
            {
                if (i != 0) { s = FuncHelp.GetSource(host + link + "?title=&items_per_page=100&page=" + i.ToString()); }
                s = FuncHelp.CutFromTo(s, "views-row-first", "</tbody>");
                while (s.IndexOf("<a href='") >= 0)
                {
                    if (IsStop || (cbxSlg.Checked && ArrayDetailLink.Count >= numMaxSlg.Value)) { break; }
                    s = FuncHelp.CutFrom(s, "<a href='");
                    li = FuncHelp.CutTo(s, "'>");
                    ArrayDetailLink.Add(li);
                }
                if (IsStop || (cbxSlg.Checked && ArrayDetailLink.Count >= numMaxSlg.Value)) { break; }
            }
        }

        void Get_TotalLink()
        {
            stt2.Visible = true;
            stt2.Text = "Đang lấy các link của trang...";
            Application.DoEvents();

            // Kiểm tra nhập link đơn giản
            if(cbxGetFromLink.Checked)
            {
                string s = FuncHelp.GetSource(host+l);
                if (s.IndexOf("views-field-totalcount") >= 0)
                {
                    Get_AllLinkInCategory(l);
                }
                else
                {
                    MessageBox.Show("Link không phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IsStop = true;
                    return;
                }
            }
            else
            {
                if(cbxGetFromCategory.Visible && cbxGetFromCategory.Checked)
                {
                    Get_AllLinkInCategory(cbCategory.SelectedValue.ToString());
                }
                else
                {
                    progressBar1.Visible = true;
                    progressBar1.Maximum = dtCategoryLink.Rows.Count;
                    for (int i = 1; i <= dtCategoryLink.Rows.Count; i++)
                    {
                        Get_AllLinkInCategory(dtCategoryLink.Rows[i - 1][0].ToString());
                        progressBar1.Value = i;
                        Application.DoEvents();
                        if (IsStop || (cbxSlg.Checked && ArrayDetailLink.Count >= numMaxSlg.Value)) { break; }
                    }
                }
            }           

        }

        void Get_DataLink(string link)
        {
            string s = FuncHelp.GetSource(host+link);
            s = FuncHelp.CutFromTo(s, "siteContent", "Các tin mới").Replace("\r", "").Replace("&nbsp;", "").Replace("Điện thoại :","Điện thoại:")
                .Replace("Giới Thệu","Giới Thiệu").Replace("Giới thiệu","Giới Thiệu");

            string tencongty = FuncHelp.CutFromTo(s, "page-title'>", "</h1>").Trim();
            string tengiaodich = FuncHelp.CutFromTo(s, "Tên giao dịch:", "</p>").Trim();
            string tenviettat = FuncHelp.CutFromTo(s, "Tên viết tắt:", "</p>").Trim();
            string diachi = FuncHelp.CutFromTo(s, "Địa chỉ:", "</p>").Trim();
            string diaphuong = FuncHelp.CutFromTo(s, "Địa phương:", "</p>").Trim();
            string dienthoai = FuncHelp.CutFromTo(s, "Điện thoại:", "</p>").Trim();
            string fax = FuncHelp.CutFromTo(s, "Fax:", "</p>").Trim();
            string email = FuncHelp.CutFromTo(s, "Email:", "</p>").Trim();
            string website = FuncHelp.CutFromTo(s, "Website:", "</p>").Trim();
            string giamdoc = FuncHelp.CutFromTo(s, "Giám đốc:", "</p>").Trim();
            string chungkhoan = FuncHelp.CutFromTo(s, "Chứng khoán:", "</p>").Trim();
            string masothue = FuncHelp.CutFromTo(s, "Mã số thuế:", "</p>").Trim();
            string gioithieu = FuncHelp.CutFromTo(s, "Giới Thiệu", "<div class='links-container'>").Trim();

            if (s.IndexOf("Địa chỉ:") < 0) { diachi = FuncHelp.CutFromTo(s, "Trụ sở chính:", "</p>").Trim(); }
            if (s.IndexOf("Giám đốc:") < 0) { giamdoc = FuncHelp.CutFromTo(s, "Giám đốc/Tổng giám đốc:", "</p>").Trim(); }

            if (tenviettat.IndexOf("<stro") > 0) { tenviettat = FuncHelp.CutTo(tenviettat, "<stro").Trim(); }
            if (diachi.IndexOf("<stro") > 0) { diachi = FuncHelp.CutTo(diachi, "<stro").Trim(); }
            if (diaphuong.IndexOf("<stro") > 0) { diaphuong = FuncHelp.CutTo(diaphuong, "<stro").Trim(); }
            if (dienthoai.IndexOf("<stro") > 0) { dienthoai = FuncHelp.CutTo(dienthoai, "<stro").Trim(); }
            if (fax.IndexOf("<stro") > 0) { fax = FuncHelp.CutTo(fax, "<stro").Trim(); }
            if (email.IndexOf("<stro") > 0) { email = FuncHelp.CutTo(email, "<stro").Trim(); }
            if (website.IndexOf("<stro") > 0) { website = FuncHelp.CutTo(website, "<stro").Trim(); }
            if (giamdoc.IndexOf("<stro") > 0) { giamdoc = FuncHelp.CutTo(giamdoc, "<stro").Trim(); }
            if (chungkhoan.IndexOf("<stro") > 0) { chungkhoan = FuncHelp.CutTo(chungkhoan, "<stro").Trim(); }
            if (masothue.IndexOf("<stro") > 0) { masothue = FuncHelp.CutTo(masothue, "<stro").Trim(); }

            if (tenviettat.IndexOf("<br") >= 0) { tenviettat = FuncHelp.CutTo(tenviettat, "<br").Trim(); }
            if (diachi.IndexOf("<br") >= 0) { diachi = FuncHelp.CutTo(diachi, "<br").Trim(); }
            if (diaphuong.IndexOf("<br") >= 0) { diaphuong = FuncHelp.CutTo(diaphuong, "<br").Trim(); }
            if (dienthoai.IndexOf("<br") >= 0) { dienthoai = FuncHelp.CutTo(dienthoai, "<br").Trim(); }
            if (fax.IndexOf("<br") >= 0) { fax = FuncHelp.CutTo(fax, "<br").Trim(); }
            if (email.IndexOf("<br") >= 0) { email = FuncHelp.CutTo(email, "<br").Trim(); }
            if (website.IndexOf("<br") >= 0) { website = FuncHelp.CutTo(website, "<br").Trim(); }
            if (giamdoc.IndexOf("<br") >= 0) { giamdoc = FuncHelp.CutTo(giamdoc, "<br").Trim(); }
            if (chungkhoan.IndexOf("<br") >= 0) { chungkhoan = FuncHelp.CutTo(chungkhoan, "<br").Trim(); }
            if (masothue.IndexOf("<br") >= 0) { masothue = FuncHelp.CutTo(masothue, "<br").Trim(); } 

            tencongty = Regex.Replace(tencongty, @"<[^>]+>|&nbsp;", "").Trim();
            tengiaodich = Regex.Replace(tengiaodich, @"<[^>]+>|&nbsp;", "").Trim();
            tenviettat = Regex.Replace(tenviettat, @"<[^>]+>|&nbsp;", "").Trim();
            diachi = Regex.Replace(diachi, @"<[^>]+>|&nbsp;", "").Trim();
            diaphuong = Regex.Replace(diaphuong, @"<[^>]+>|&nbsp;", "").Trim();
            dienthoai = Regex.Replace(dienthoai, @"<[^>]+>|&nbsp;", "").Trim();
            fax = Regex.Replace(fax, @"<[^>]+>|&nbsp;", "").Trim();
            email = Regex.Replace(email, @"<[^>]+>|&nbsp;", "").Trim();
            website = Regex.Replace(website, @"<[^>]+>|&nbsp;", "").Trim().Replace("http://","");
            giamdoc = Regex.Replace(giamdoc, @"<[^>]+>|&nbsp;", "").Trim();
            chungkhoan = Regex.Replace(chungkhoan, @"<[^>]+>|&nbsp;", "").Trim();
            masothue = Regex.Replace(masothue, @"<[^>]+>|&nbsp;", "").Trim();
            gioithieu = Regex.Replace(gioithieu, @"<[^>]+>|&nbsp;", "").Trim();
            if (gioithieu.IndexOf(":") == 0) { gioithieu = gioithieu.Remove(0, 1).Trim(); }
            if (email.IndexOf("Website:") > 0) { email = FuncHelp.CutTo(email,"Website:"); }
            if (website.IndexOf("Giám đốc:") > 0) { website = FuncHelp.CutTo(website, "Giám đốc:"); }
            if (giamdoc.IndexOf("Giám đốc:") > 0) { giamdoc = FuncHelp.CutFrom(giamdoc, "Giám đốc:"); }

            tencongty = WebUtility.HtmlDecode(tencongty);
            //linhvuckinhdoanh = WebUtility.HtmlDecode(linhvuckinhdoanh);
            //diachi = WebUtility.HtmlDecode(diachi);
            //dienthoai = WebUtility.HtmlDecode(dienthoai);
            //gioithieu = WebUtility.HtmlDecode(gioithieu);

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = tencongty; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = tengiaodich; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = tenviettat; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = diachi; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = diaphuong; }
            if (chkListBox.GetItemChecked(5)) { i++; dr[i] = dienthoai; }
            if (chkListBox.GetItemChecked(6)) { i++; dr[i] = fax; }
            if (chkListBox.GetItemChecked(7)) { i++; dr[i] = email; }
            if (chkListBox.GetItemChecked(8)) { i++; dr[i] = website; }
            if (chkListBox.GetItemChecked(9)) { i++; dr[i] = giamdoc; }
            if (chkListBox.GetItemChecked(10)) { i++; dr[i] = chungkhoan; }
            if (chkListBox.GetItemChecked(11)) { i++; dr[i] = masothue; }
            if (chkListBox.GetItemChecked(12)) { i++; dr[i] = gioithieu; }

            dt.Rows.Add(dr);
        }

        void Check_Category()
        {
            stt1.Text = "Đang kiểm tra menu này...";
            Application.DoEvents();

            string s = FuncHelp.GetSource(host+l);
            dtCategoryLink.Rows.Clear();
            if (s.IndexOf("views-field-totalcount") >= 0) // Số truy cập
            {
                stt1.Text = "Menu là 1 Danh Mục!";
                dtCategoryLink.Rows.Add(l,"Menu hiện tại");
                cbCategory.DataSource = dtCategoryLink;
                cbxGetFromCategory.Visible = false;
                cbCategory.Visible = false;
                return;
            }
            else if (s.IndexOf("views-field-nid") >= 0) // Tổng danh mục
            {
                Get_AllCategoryInMenu();
                stt1.Text = string.Format("Đã tìm được {0} danh mục!", dtCategoryLink.Rows.Count);
                cbCategory.DataSource = dtCategoryLink;
                cbxGetFromCategory.Visible = true;
                cbCategory.Visible = true;
                return;
            }
            else
            {
                stt1.Text = "Link không xác định! Vui lòng chọn chế độ nhập link";
                txtLink.Text = host + l;
                l = ""; // Xóa bỏ để về mor
            }
            Application.DoEvents();
        }

        private void cbxGetFromCategory_CheckedChanged(object sender, EventArgs e)
        {
            cbCategory.Enabled = cbxGetFromCategory.Checked;
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMenu.SelectedIndex < 0) { return; }
            HtmlNode d = doc.DocumentNode.ChildNodes[cbMenu.SelectedIndex];
            l = d.FirstChild.GetAttributeValue("href", "");

            cbMenuSub.Items.Clear();
            if (d.ChildNodes.Count == 1)
            {
                //stt1.Text = d.FirstChild.GetAttributeValue("href", "");
                cbMenuSub.Visible = false;
                Check_Category();
            }
            else
            {
                stt1.Text = "";
                cbMenuSub.Visible = true;
                foreach(HtmlNode x in d.LastChild.ChildNodes)
                {
                    cbMenuSub.Items.Add(x.FirstChild.InnerText);
                }
                if (cbMenuSub.Items.Count > 0) { cbMenuSub.SelectedIndex = 0; }
            }
        }

        private void cbCategorySub_SelectedIndexChanged(object sender, EventArgs e)
        {
            HtmlNode d = doc.DocumentNode.ChildNodes[cbMenu.SelectedIndex];
            HtmlNode dd = d.LastChild.ChildNodes[cbMenuSub.SelectedIndex];
            l = dd.FirstChild.GetAttributeValue("href", "");

            //stt1.Text = dd.FirstChild.GetAttributeValue("href", "");
            Check_Category();
        }

        private void cbxGetFromLink_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = !cbxGetFromLink.Checked;
        }
    }
}
