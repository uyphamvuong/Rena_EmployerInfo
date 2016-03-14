using DreamCMS.Config;
using System;
using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();
        }

        string Version;

        private void frmSetting_Shown(object sender, EventArgs e)
        {
            Xmlconfig xcf = new Xmlconfig("Config.ini", true);
            Version = xcf.Settings["EmployerInfo/Version"].Value;
            if (Version == "") { txtVersion.Text = "Phần mềm chưa cập nhật lần nào!"; }
            else { txtVersion.Text = string.Format(txtVersion.Tag as string, Version); }

            chxAutoCheckUpdate.Checked = xcf.Settings["EmployerInfo/AutoCheckUpdate"].boolValue;
            chxAskOpenFileWhenDone.Checked = xcf.Settings["EmployerInfo/AskOpenFileWhenDone"].boolValue;

            xcf.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Xmlconfig xcf = new Xmlconfig("Config.ini", true);
            xcf.Settings["EmployerInfo/AutoCheckUpdate"].boolValue = chxAutoCheckUpdate.Checked;
            frmMain.AutoCheckUpdate = chxAutoCheckUpdate.Checked;
            xcf.Settings["EmployerInfo/AskOpenFileWhenDone"].boolValue = chxAskOpenFileWhenDone.Checked;
            frmMain.AskOpenFileWhenDone = chxAskOpenFileWhenDone.Checked;
            xcf.Dispose();
            Close();
        }
    }
}
