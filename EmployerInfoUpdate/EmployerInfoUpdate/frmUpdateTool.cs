using DllWebBrowser;
using DreamCMS.Config;
using Ionic.Zip;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace EmployerInfoUpdate
{
    public partial class frmUpdateTool : Form
    {
        public frmUpdateTool()
        {
            if (!CheckIsSingleInstance()) { Close(); }
            InitializeComponent();
        }

        #region Init
        string Link = "https://github.com/uyphamvuong/Rena_EmployerInfo/blob/master/README.md";
        string PathApp = Application.StartupPath+"\\";
        string FileRun = "EmployerInfo.exe";
        string FilePack = "UpdatePack.zip";
        static string _Version, _LinkDown, _Status;
        static int _Percent;
        static bool _IsStop = false;

        WebBrowser wbr = new WebBrowser();        
        #endregion

        private void frmUpdateTool_Load(object sender, EventArgs e)
        {
            tmer.Stop();
        }

        void GetInfoFromWeb()
        {
            string s_  = "";
            try
            {
                DllWbr.Redirect(wbr, Link);
                s_ = wbr.DocumentText;
                s_ = s_.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\"", "'").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                string VersionNew = FuncHelp.CutFromTo(s_, ":::Rena_EmployerInfo Version:::", "</P>");
                _LinkDown = FuncHelp.CutFromTo(s_, ":::Rena_EmployerInfo LinkDown:::", "</P>");
                _LinkDown = FuncHelp.CutFromTo(_LinkDown, "href='", "'");

                Xmlconfig xcf = new Xmlconfig("Config.ini", true);
                _Version = xcf.Settings["EmployerInfo/Version"].Value;
                xcf.Dispose();

                if (_Version != VersionNew)
                {
                    //Update
                    _Version = VersionNew;
                    startDownload();
                    _Status = "Đang tải gói dữ liệu cập nhật...";
                    tmer.Start();
                }
                else
                {
                    RunSoft();
                }                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Quá trình cập nhật gặp lỗi!!!\n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }
            
        }

        private void frmUpdateTool_Shown(object sender, EventArgs e)
        {
            lStatus.Text = TextForForm.strLoading;
            Application.DoEvents();
            GetInfoFromWeb();
        }

        #region Download

        private void startDownload()
        {
            Thread thread = new Thread(() =>
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(frmUpdateTool._LinkDown), PathApp + FilePack);
            });
            thread.Start();
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                frmUpdateTool._Percent = int.Parse(Math.Truncate(percentage).ToString());
                frmUpdateTool._Status = string.Format("Downloaded {0:##,##}/{1:##,##} Kb ... {2}%",e.BytesReceived,e.TotalBytesToReceive,frmUpdateTool._Percent);

            });
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                frmUpdateTool._IsStop = true;
            });
        }
        #endregion

        #region Unpack
        void Unpack()
        {
            
            _Status = "Đang giải nén dữ liệu...";
            Application.DoEvents();
            tmer.Stop();
            
            try
            {
                var options = new ReadOptions { StatusMessageWriter = System.Console.Out };
                using (ZipFile zip = ZipFile.Read(PathApp+FilePack, options))
                {
                    zip.ExtractAll(PathApp, ExtractExistingFileAction.OverwriteSilently);
                }

                // Xóa UpdatePack và cập nhật thông tin version
                if (File.Exists(PathApp + FilePack)) { File.Delete(PathApp + FilePack); }
                Xmlconfig xcf = new Xmlconfig("Config.ini", true);
                xcf.Settings["EmployerInfo/Version"].Value = _Version;
                xcf.Save("Config.ini");
                xcf.Dispose();

                RunSoft();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Quá trình cập nhật gặp lỗi!!!\n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }
        }

        #endregion

        private void tmer_Tick(object sender, EventArgs e)
        {            
            lStatus.Text = _Status;
            pbProcess.ValuePercent = _Percent;
            if (_IsStop)
            {
                Unpack();
            }
        }

        void RunSoft()
        {
            try
            {
                Process.Start(FileRun);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Quá trình cập nhật gặp lỗi!!!\n\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Close();
            }            
        }

        bool CheckIsSingleInstance()
        {
            string procName = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcessesByName(procName).Length > 1)
            {
                return false;
            }
            return true;
        }
    }
}
