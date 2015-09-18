namespace EmployerInfo
{
    partial class frmM5
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
            this.chkListBox = new System.Windows.Forms.CheckedListBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.numMaxSlg = new System.Windows.Forms.NumericUpDown();
            this.cbxSlg = new System.Windows.Forms.CheckBox();
            this.stt1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.stt3 = new System.Windows.Forms.Label();
            this.cbMenu = new System.Windows.Forms.ComboBox();
            this.cbxGetFromCategory = new System.Windows.Forms.CheckBox();
            this.stt2 = new System.Windows.Forms.Label();
            this.cbMenuSub = new System.Windows.Forms.ComboBox();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.cbxGetFromLink = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkListBox
            // 
            this.chkListBox.FormattingEnabled = true;
            this.chkListBox.Items.AddRange(new object[] {
            "Tên công ty",
            "Tên giao dịch",
            "Tên viết tắt",
            "Địa chỉ",
            "Địa phương",
            "Điện thoại",
            "Fax",
            "Email",
            "Website",
            "Giám đốc",
            "Chứng khoán",
            "Mã số thuế",
            "Giới Thiệu"});
            this.chkListBox.Location = new System.Drawing.Point(9, 39);
            this.chkListBox.Name = "chkListBox";
            this.chkListBox.Size = new System.Drawing.Size(191, 259);
            this.chkListBox.TabIndex = 11;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 13);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(190, 20);
            this.checkBox2.TabIndex = 13;
            this.checkBox2.Text = "Các trường dữ liệu muốn lấy";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // btnRun
            // 
            this.btnRun.Image = global::EmployerInfo.Properties.Resources.info;
            this.btnRun.Location = new System.Drawing.Point(495, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(139, 54);
            this.btnRun.TabIndex = 14;
            this.btnRun.Text = "GET INFO";
            this.btnRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // numMaxSlg
            // 
            this.numMaxSlg.Enabled = false;
            this.numMaxSlg.Location = new System.Drawing.Point(306, 12);
            this.numMaxSlg.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxSlg.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxSlg.Name = "numMaxSlg";
            this.numMaxSlg.Size = new System.Drawing.Size(62, 22);
            this.numMaxSlg.TabIndex = 16;
            this.numMaxSlg.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbxSlg
            // 
            this.cbxSlg.AutoSize = true;
            this.cbxSlg.Location = new System.Drawing.Point(220, 13);
            this.cbxSlg.Name = "cbxSlg";
            this.cbxSlg.Size = new System.Drawing.Size(80, 20);
            this.cbxSlg.TabIndex = 17;
            this.cbxSlg.Text = "Số lượng";
            this.cbxSlg.UseVisualStyleBackColor = true;
            this.cbxSlg.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // stt1
            // 
            this.stt1.AutoSize = true;
            this.stt1.ForeColor = System.Drawing.Color.Green;
            this.stt1.Location = new System.Drawing.Point(3, 16);
            this.stt1.Name = "stt1";
            this.stt1.Size = new System.Drawing.Size(176, 16);
            this.stt1.TabIndex = 18;
            this.stt1.Tag = "Tổng số danh mục: {0:##.##}";
            this.stt1.Text = "Tổng số danh mục: {0:##.##}";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(223, 403);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(411, 12);
            this.progressBar1.TabIndex = 20;
            this.progressBar1.Visible = false;
            // 
            // stt3
            // 
            this.stt3.AutoSize = true;
            this.stt3.ForeColor = System.Drawing.Color.Red;
            this.stt3.Location = new System.Drawing.Point(223, 371);
            this.stt3.Name = "stt3";
            this.stt3.Size = new System.Drawing.Size(160, 16);
            this.stt3.TabIndex = 22;
            this.stt3.Tag = "Số link đã duyệt: {0:##.##}";
            this.stt3.Text = "Số link đã duyệt: {0:##.##}";
            this.stt3.Visible = false;
            // 
            // cbMenu
            // 
            this.cbMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMenu.FormattingEnabled = true;
            this.cbMenu.Location = new System.Drawing.Point(6, 41);
            this.cbMenu.Name = "cbMenu";
            this.cbMenu.Size = new System.Drawing.Size(274, 24);
            this.cbMenu.TabIndex = 23;
            this.cbMenu.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // cbxGetFromCategory
            // 
            this.cbxGetFromCategory.AutoSize = true;
            this.cbxGetFromCategory.Location = new System.Drawing.Point(6, 103);
            this.cbxGetFromCategory.Name = "cbxGetFromCategory";
            this.cbxGetFromCategory.Size = new System.Drawing.Size(139, 20);
            this.cbxGetFromCategory.TabIndex = 24;
            this.cbxGetFromCategory.Text = "Lấy theo danh mục";
            this.cbxGetFromCategory.UseVisualStyleBackColor = true;
            this.cbxGetFromCategory.Visible = false;
            this.cbxGetFromCategory.CheckedChanged += new System.EventHandler(this.cbxGetFromCategory_CheckedChanged);
            // 
            // stt2
            // 
            this.stt2.AutoSize = true;
            this.stt2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.stt2.Location = new System.Drawing.Point(223, 345);
            this.stt2.Name = "stt2";
            this.stt2.Size = new System.Drawing.Size(186, 16);
            this.stt2.TabIndex = 21;
            this.stt2.Tag = "Số link sẽ lấy dữ liệu: {0:##.##}";
            this.stt2.Text = "Số link sẽ lấy dữ liệu: {0:##.##}";
            this.stt2.Visible = false;
            // 
            // cbMenuSub
            // 
            this.cbMenuSub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMenuSub.FormattingEnabled = true;
            this.cbMenuSub.Location = new System.Drawing.Point(6, 71);
            this.cbMenuSub.Name = "cbMenuSub";
            this.cbMenuSub.Size = new System.Drawing.Size(274, 24);
            this.cbMenuSub.TabIndex = 25;
            this.cbMenuSub.Visible = false;
            this.cbMenuSub.SelectedIndexChanged += new System.EventHandler(this.cbCategorySub_SelectedIndexChanged);
            // 
            // cbCategory
            // 
            this.cbCategory.DisplayMember = "TextCate";
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.Enabled = false;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.IntegralHeight = false;
            this.cbCategory.Location = new System.Drawing.Point(6, 129);
            this.cbCategory.MaxDropDownItems = 10;
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(405, 24);
            this.cbCategory.TabIndex = 26;
            this.cbCategory.ValueMember = "LinkCate";
            this.cbCategory.Visible = false;
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(226, 273);
            this.txtLink.Multiline = true;
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(408, 60);
            this.txtLink.TabIndex = 27;
            // 
            // cbxGetFromLink
            // 
            this.cbxGetFromLink.AutoSize = true;
            this.cbxGetFromLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGetFromLink.Location = new System.Drawing.Point(226, 247);
            this.cbxGetFromLink.Name = "cbxGetFromLink";
            this.cbxGetFromLink.Size = new System.Drawing.Size(92, 20);
            this.cbxGetFromLink.TabIndex = 28;
            this.cbxGetFromLink.Text = "Nhập link";
            this.cbxGetFromLink.UseVisualStyleBackColor = true;
            this.cbxGetFromLink.CheckedChanged += new System.EventHandler(this.cbxGetFromLink_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.stt1);
            this.panel1.Controls.Add(this.cbMenu);
            this.panel1.Controls.Add(this.cbxGetFromCategory);
            this.panel1.Controls.Add(this.cbCategory);
            this.panel1.Controls.Add(this.cbMenuSub);
            this.panel1.Location = new System.Drawing.Point(223, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 166);
            this.panel1.TabIndex = 29;
            // 
            // frmM5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 427);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbxGetFromLink);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.stt3);
            this.Controls.Add(this.stt2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cbxSlg);
            this.Controls.Add(this.numMaxSlg);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.chkListBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmM5";
            this.Text = "frmM5";
            this.Load += new System.EventHandler(this.frmM1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkListBox;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.NumericUpDown numMaxSlg;
        private System.Windows.Forms.CheckBox cbxSlg;
        private System.Windows.Forms.Label stt1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label stt3;
        private System.Windows.Forms.ComboBox cbMenu;
        private System.Windows.Forms.CheckBox cbxGetFromCategory;
        private System.Windows.Forms.Label stt2;
        private System.Windows.Forms.ComboBox cbMenuSub;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.CheckBox cbxGetFromLink;
        private System.Windows.Forms.Panel panel1;

    }
}