namespace EmployerInfo
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Trang chủ", "favicon.png");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("vieclam.24h.com.vn", 0);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("thongtincongty.com", 1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("ctyvietnam.com", "favicon.png");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("danhbadoanhnghiep.vn", "favicon.png");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("thuongmai.vn", "favicon.png");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("vlam24h(NTD)", "icon-viec-lam.gif");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "trangvangvietnam.com"}, "favicon.png", System.Drawing.Color.Black, System.Drawing.Color.White, null);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("vieclam.tv", "favicon.png");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitP = new System.Windows.Forms.SplitContainer();
            this.listLeft = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pMain = new System.Windows.Forms.Panel();
            this.lbLeft = new System.Windows.Forms.Label();
            this.pTop = new System.Windows.Forms.Panel();
            this.btnSetting = new System.Windows.Forms.PictureBox();
            this.btnUpdate = new System.Windows.Forms.PictureBox();
            this.btnHide = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.processBarEdit1 = new EmployerInfo.ProcessBarEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitP)).BeginInit();
            this.splitP.Panel1.SuspendLayout();
            this.splitP.Panel2.SuspendLayout();
            this.splitP.SuspendLayout();
            this.pTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // splitP
            // 
            this.splitP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitP.BackColor = System.Drawing.Color.Silver;
            this.splitP.Location = new System.Drawing.Point(0, 51);
            this.splitP.Name = "splitP";
            // 
            // splitP.Panel1
            // 
            this.splitP.Panel1.BackColor = System.Drawing.Color.White;
            this.splitP.Panel1.Controls.Add(this.listLeft);
            // 
            // splitP.Panel2
            // 
            this.splitP.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitP.Panel2.Controls.Add(this.pMain);
            this.splitP.Size = new System.Drawing.Size(860, 443);
            this.splitP.SplitterDistance = 200;
            this.splitP.TabIndex = 0;
            // 
            // listLeft
            // 
            this.listLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listLeft.FullRowSelect = true;
            listViewItem8.StateImageIndex = 0;
            this.listLeft.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.listLeft.LargeImageList = this.imageList1;
            this.listLeft.Location = new System.Drawing.Point(0, 0);
            this.listLeft.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.listLeft.MultiSelect = false;
            this.listLeft.Name = "listLeft";
            this.listLeft.Scrollable = false;
            this.listLeft.Size = new System.Drawing.Size(200, 443);
            this.listLeft.SmallImageList = this.imageList1;
            this.listLeft.TabIndex = 0;
            this.listLeft.TileSize = new System.Drawing.Size(284, 44);
            this.listLeft.UseCompatibleStateImageBehavior = false;
            this.listLeft.View = System.Windows.Forms.View.Tile;
            this.listLeft.VirtualListSize = 10;
            this.listLeft.ItemActivate += new System.EventHandler(this.listLeft_ItemActivate);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon-viec-lam.gif");
            this.imageList1.Images.SetKeyName(1, "favicon.png");
            // 
            // pMain
            // 
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(656, 443);
            this.pMain.TabIndex = 1;
            // 
            // lbLeft
            // 
            this.lbLeft.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbLeft.AutoSize = true;
            this.lbLeft.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLeft.ForeColor = System.Drawing.Color.Black;
            this.lbLeft.Location = new System.Drawing.Point(12, 9);
            this.lbLeft.Name = "lbLeft";
            this.lbLeft.Size = new System.Drawing.Size(237, 25);
            this.lbLeft.TabIndex = 1;
            this.lbLeft.Text = "PHẦN MỀM LẤY DỮ LIỆU";
            this.lbLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Object_MouseDown);
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.White;
            this.pTop.Controls.Add(this.btnSetting);
            this.pTop.Controls.Add(this.btnUpdate);
            this.pTop.Controls.Add(this.btnHide);
            this.pTop.Controls.Add(this.btnClose);
            this.pTop.Controls.Add(this.lbLeft);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(860, 45);
            this.pTop.TabIndex = 1;
            this.pTop.Paint += new System.Windows.Forms.PaintEventHandler(this.pTop_Paint);
            this.pTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Object_MouseDown);
            // 
            // btnSetting
            // 
            this.btnSetting.Image = global::EmployerInfo.Properties.Resources.gnome_system_run;
            this.btnSetting.Location = new System.Drawing.Point(719, 3);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(39, 39);
            this.btnSetting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSetting.TabIndex = 5;
            this.btnSetting.TabStop = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Image = global::EmployerInfo.Properties.Resources.gnome_software_update_available;
            this.btnUpdate.Location = new System.Drawing.Point(674, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(39, 39);
            this.btnUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.TabStop = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnHide
            // 
            this.btnHide.Image = global::EmployerInfo.Properties.Resources.gnome_list_remove;
            this.btnHide.Location = new System.Drawing.Point(764, 3);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(39, 39);
            this.btnHide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHide.TabIndex = 3;
            this.btnHide.TabStop = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::EmployerInfo.Properties.Resources.gnome_window_close;
            this.btnClose.Location = new System.Drawing.Point(809, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 39);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 2;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // processBarEdit1
            // 
            this.processBarEdit1.ColorProcess = System.Drawing.Color.DeepSkyBlue;
            this.processBarEdit1.ColorUnderBar = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.processBarEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.processBarEdit1.Location = new System.Drawing.Point(0, 45);
            this.processBarEdit1.Name = "processBarEdit1";
            this.processBarEdit1.Size = new System.Drawing.Size(860, 5);
            this.processBarEdit1.TabIndex = 2;
            this.processBarEdit1.ValuePercent = 15;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 495);
            this.Controls.Add(this.processBarEdit1);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.splitP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Employer Info";
            this.Text = "Employer Info";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.splitP.Panel1.ResumeLayout(false);
            this.splitP.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitP)).EndInit();
            this.splitP.ResumeLayout(false);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitP;
        private System.Windows.Forms.Label lbLeft;
        private System.Windows.Forms.ListView listLeft;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox btnHide;
        private ProcessBarEdit processBarEdit1;
        private System.Windows.Forms.PictureBox btnSetting;
        private System.Windows.Forms.PictureBox btnUpdate;
    }
}

