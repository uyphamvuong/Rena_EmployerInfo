using DreamCMS.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmThongTinCongTy : Form
    {

        #region # INIT #

        public frmThongTinCongTy()
        {
            InitializeComponent();           
        }

        private void frmM1_Load(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            numMaxSlg.Value = 100;  
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

        private void cbxNewItem_CheckedChanged(object sender, EventArgs e)
        {
            Get_LastId();
        }

        #endregion

        bool IsStop = false, IsRun = false;
        string host = "http://www.thongtincongty.com", LastId;
        int maxPage = 0;
        List<string> ArrayDetailLink = new List<string>();
        DataTable dt = new DataTable();

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (IsRun) { IsStop = true; return; }
            if (!CheckInput()) { return; }

            btnRun.Text = "STOP";
            IsRun = true;
            
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
            if (cbxNewItem.Checked) { txtLastID.Text = LastId; Set_LastId(LastId); }
            progressBar1.Visible = false;
            string filename = @"Export\Export_thongtincongty.xlsx";
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
            stt1.Text = "Đang lấy tổng số phân trang...";
            Application.DoEvents();

            string s = FuncHelp.GetSource(host);
            s = FuncHelp.CutFromTo(s, "<ul class='pagination'>", "</ul>");
            s = s.Substring(s.LastIndexOf("?page="), s.Length - s.LastIndexOf("?page="));

            maxPage = int.Parse(FuncHelp.CutFromTo(s, "?page=", "'"));

            stt1.Text = string.Format(stt1.Tag as string, maxPage);

            for (int i = 0; i < maxPage; i++) { cbxMin.Items.Add(i + 1); cbxMax.Items.Add(i + 1); }
            if (maxPage > 0) { cbxMin.SelectedItem = 1; cbxMax.SelectedItem = maxPage; }

        }

        void Get_TotalLink()
        {
            stt2.Visible = true;
            stt2.Text = "Đang lấy các link của trang...";
            Application.DoEvents();

            string l;
            int min = 1, max = maxPage;
            bool isSaveLast = false;
            if (cbxPageLimit.Checked) { min = (int)cbxMin.SelectedItem; max = (int)cbxMax.SelectedItem; }
            ArrayDetailLink.Clear();
            for(int i=min;i<=max;i++)
            {
                string s = FuncHelp.GetSource(host + "/?page=" + i.ToString());
                s = FuncHelp.CutFromTo(s, "<h4>Doanh nghiệp mới cập nhật</h4>", "<ul class='pagination'>");
                while (s.IndexOf("<div class='search-results'>") >= 0)
                {
                    s = FuncHelp.CutFrom(s, "<div class='search-results'>");
                    s = FuncHelp.CutFrom(s, "<a href='");
                    l = FuncHelp.CutTo(s, "'>");
                    if (cbxSlg.Checked && ArrayDetailLink.Count >= numMaxSlg.Value) { IsStop = true; break; }
                    string idd = l.Replace(host+"/company/", "");
                    idd = idd.Substring(0, idd.IndexOf("-"));
                    if (cbxNewItem.Checked && txtLastID.Text == idd) { IsStop = true; break; }
                    if (!isSaveLast) { isSaveLast = true; LastId = idd; }
                    ArrayDetailLink.Add(l);                    
                }
                stt2.Text = string.Format(stt2.Tag as string, ArrayDetailLink.Count);
                Application.DoEvents();
                if (IsStop) { IsStop = false; break; }
            }
        }

        void Get_DataLink(string link)
        {
            string s = FuncHelp.GetSource(link);
            s = FuncHelp.CutFromTo(s, "<div class='jumbotron'>", "<div class='visible-lg'");

            string tencongty = FuncHelp.CutFromTo(s, "<h4><span title='", "'>");
            string diachi = FuncHelp.CutFromTo(s, "Địa chỉ: ", "<br/>").Replace("<strong>", "").Replace("</strong>", "");
            string giamdoc = FuncHelp.CutFromTo(s, "Đại diện pháp luật: ", "<br/>").Replace("<strong>", "").Replace("</strong>", "");
            string giayphep = FuncHelp.CutFromTo(s, "Giấy phép kinh doanh: ", "<br/>").Replace("<strong>", "").Replace("</strong>", "");
            string ngaycap = FuncHelp.CutFromTo(s, "Ngày cấp giấy phép: ", "<br/>").Replace("<strong>", "").Replace("</strong>", "");
            string masothue = FuncHelp.CutFromTo(s, "Mã số thuế: ", "<br/>").Replace("<strong>", "").Replace("</strong>", "");
            string ngayhoatdong = FuncHelp.CutFromTo(s, "Ngày hoạt động: ", " (").Replace("<strong>", "").Replace("</strong>", "");
            string hoatdongchinh = FuncHelp.CutFromTo(s, "Hoạt động chính: ", "<br/>").Replace("<strong>", "").Replace("</strong>", "");
            string dienthoai = FuncHelp.CutFromTo(s, "Điện thoại: ", "<br/>").Replace("<strong>", "").Replace("</strong>", "");

            masothue = Regex.Replace(masothue, @"<[^>]+>|&nbsp;", "").Trim(); 

            DataRow dr = dt.NewRow();
            int i = -1;
            if (chkListBox.GetItemChecked(0)) { i++; dr[i] = tencongty; }
            if (chkListBox.GetItemChecked(1)) { i++; dr[i] = diachi; }
            if (chkListBox.GetItemChecked(2)) { i++; dr[i] = giamdoc; }
            if (chkListBox.GetItemChecked(3)) { i++; dr[i] = giayphep; }
            if (chkListBox.GetItemChecked(4)) { i++; dr[i] = ngaycap; }
            if (chkListBox.GetItemChecked(5)) { i++; dr[i] = masothue; }
            if (chkListBox.GetItemChecked(6)) { i++; dr[i] = ngayhoatdong; }
            if (chkListBox.GetItemChecked(7)) { i++; dr[i] = hoatdongchinh; }
            if (chkListBox.GetItemChecked(8)) { i++; dr[i] = dienthoai; }

            dt.Rows.Add(dr);
        }

        void Get_LastId()
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EmployerInfo");
            if (!di.Exists) { di.Create(); }
            Xmlconfig xg = new Xmlconfig(di.FullName + "/AppConfig.xml", true);
            txtLastID.Text = xg.Settings["LastId"].Value;
        }

        void Set_LastId(string id)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/EmployerInfo");
            if (!di.Exists) { di.Create(); }
            Xmlconfig xg = new Xmlconfig(di.FullName + "/AppConfig.xml", true);
            xg.Settings["LastId"].Value = id;
            xg.Save(di.FullName + "/AppConfig.xml");
        }

        private void frmM2_Shown(object sender, EventArgs e)
        {
            Get_TotalPage();
        }

        
    }
}
