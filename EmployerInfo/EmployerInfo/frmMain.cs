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
            if (!CheckIsSingleInstance()) { Close(); }
            InitializeComponent();
        }        

        private void frmMain_Load(object sender, EventArgs e)
        {

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

            splitP.Panel2.Controls.Clear();
            Form frm = new Form();
            
            switch(listLeft.SelectedItems[0].Index)
            {
                case 0:
                    frm = new frmM1();                    
                    break;
                case 1:
                    frm = new frmM2();
                    break;
                case 2:
                    frm = new frmM3();
                    break;
                case 3:
                    frm = new frmM4();
                    break;
                case 4:
                    frm = new frmM5();
                    break;
                case 5:
                    frm = new frmM6();
                    break;
            }

            if (string.IsNullOrEmpty(frm.Name)) { return; }

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
            MessageBox.Show("Chức năng này đang hoàn thiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        

    }
}
