using DllWebBrowser;
using DreamCMS.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            //if (!CheckIsSingleInstance()) { Close(); }
            InitializeComponent();
        }        

        private void frmMain_Load(object sender, EventArgs e)
        {
            OSVersion = FuncHelp.CheckOSVersion();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {

            Form frm =  new Home();
            frm.TopLevel = false;
            splitP.Panel2.Controls.Clear();
            splitP.Panel2.Controls.Add(frm);
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();

            Application.DoEvents();
            CheckUpdate();
        }

        #region # Effect #

        private const int CS_DROPSHADOW = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void listLeft_ItemActivate(object sender, EventArgs e)
        {
            if (listLeft.SelectedItems.Count < 1) { return; }

            for (int i = 0; i < listLeft.Items.Count; i++)
                listLeft.Items[i].Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
            listLeft.SelectedItems[0].Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold); 
            listLeft.SelectedItems[0].Focused = false;

            splitP.Panel2.Controls.Clear();
            Form frm = new Form();
            
            switch(listLeft.SelectedItems[0].Text)
            {
                case "Trang chủ":
                    frm = new Home();        
                    break;
                case "vieclam24h (Tìm việc)":
                    frm = new frmVieclam24TimViec();                    
                    break;
                case "vieclam24h (NTD)":
                    frm = new frmVieclam24hNTD();
                    break;
                case "thongtincongty.com":
                    frm = new frmThongTinCongTy();
                    break;
                case "ctyvietnam.com":
                    frm = new frmCtyvietnam();
                    break;
                case "danhbadoanhnghiep.vn":
                    frm = new frmDanhbadoanhnghiep();
                    break;
                case "thuongmai.vn":
                    frm = new frmThuongMaiVN();
                    break;
                case "trangvangvietnam.com":
                    frm = new frmTrangVangVN();
                    break;
                case "vieclam.tv (Tìm việc làm)":                    
                    frm = new frmVieclamtvTimViec();
                    break;
                case "vieclam.tv (Ứng viên)":
                    frm = new frmVieclamtvUngVien();
                    break;
                case "vieclam.tv (NTD)":
                    frm = new frmVieclamtvNTD();
                    break;
            }

            if (string.IsNullOrEmpty(frm.Name)) { return; }
            listLeft.SelectedItems[0].Selected = false;
            Application.DoEvents();

            frm.TopLevel = false;
            splitP.Panel2.Controls.Add(frm);
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();            

        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //if(MessageBox.Show("Bạn có chắc muốn thoát chương trình?","Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)== System.Windows.Forms.DialogResult.OK)
                Close();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Object_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        

        private void pTop_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(50, 0, 0, 0));
            pen.Width = 2;
            e.Graphics.DrawLine(pen, 0, 0, Width, 0);
        }

        public void SetPercentProcess(int value)
        {
            processBarEdit1.ValuePercent = value;
        }
        public void SetPercentProcess(int index, int max)
        {
            processBarEdit1.ValuePercent = Convert.ToInt32((double)index / max * 100);
            this.Text = string.Format("{0} - {1}%", this.Tag.ToString(), Convert.ToInt32((double)index / max * 100));
            if (index == max) { this.Text = this.Tag.ToString(); }
            if(OSVersion!="Windows XP")
            {
                TaskbarProgress.SetValue(this.Handle, index, max);
                TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.Normal);
            }            
        }

        public void NoProcess()
        {
            if (OSVersion != "Windows XP")
            {
                TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress);
            }            
            TopMost = true;
            TopMost = false;
            Activate();
            this.Text = this.Tag.ToString();
        }

        #endregion

        bool CheckIsSingleInstance()
        {
            string procName = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcessesByName(procName).Length > 1)
            {
                return false;
            }
            return true;
        }


        string AppPath = Application.StartupPath;

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(File.Exists(AppPath+@"\Update.exe"))
            { 
                Process.Start("Update.exe");
                Close(); 
            }
            else
            {
                return;
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Form frmMini = new frmSetting();
            frmMini.ShowDialog();

        }

        static string OSVersion;

        void CheckUpdate()
        {
            if (!File.Exists(Application.StartupPath + "\\" + @"\Update.exe")) { return; }           
            
            try
            {
                Xmlconfig xcf = new Xmlconfig("Config.ini", true);
                string _Version = xcf.Settings["EmployerInfo/Version"].Value;
                AutoCheckUpdate = xcf.Settings["EmployerInfo/AutoCheckUpdate"].boolValue;
                AskOpenFileWhenDone = xcf.Settings["EmployerInfo/AskOpenFileWhenDone"].boolValue;

                if (!AutoCheckUpdate) { return; }

                string Link = "https://github.com/uyphamvuong/Rena_EmployerInfo/blob/master/README.md";
                WebBrowser wbr = new WebBrowser();
                DllWbr.Redirect(wbr, Link);
                string s_ = wbr.DocumentText;
                s_ = s_.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\"", "'").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                string VersionNew = FuncHelp.CutFromTo(s_, ":::Rena_EmployerInfo Version:::", "</P>");
                
                xcf.Dispose();

                if (_Version != VersionNew)
                {
                    if (MessageBox.Show("Đã có phiên bản " + VersionNew + ", bản có muốn cập nhật?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Process.Start("Update.exe");
                        Close();
                    }
                }

            }
            catch
            {

            }
        }

        public static bool AutoCheckUpdate, AskOpenFileWhenDone = false;
         
    }
}
