namespace EmployerInfo
{
    partial class frmM6
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
            this.cbxMax = new System.Windows.Forms.ComboBox();
            this.cbxMin = new System.Windows.Forms.ComboBox();
            this.stt3 = new System.Windows.Forms.Label();
            this.stt2 = new System.Windows.Forms.Label();
            this.stt1 = new System.Windows.Forms.Label();
            this.cbxSlg = new System.Windows.Forms.CheckBox();
            this.numMaxSlg = new System.Windows.Forms.NumericUpDown();
            this.btnRun = new System.Windows.Forms.Button();
            this.chxSelectAll = new System.Windows.Forms.CheckBox();
            this.chkListBox = new System.Windows.Forms.CheckedListBox();
            this.cbxPageLimit = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxCategory = new System.Windows.Forms.ComboBox();
            this.dtpOnlyDay = new System.Windows.Forms.DateTimePicker();
            this.cbxOnlyDay = new System.Windows.Forms.CheckBox();
            this.lbCategory = new System.Windows.Forms.Label();
            this.gbAccount = new System.Windows.Forms.GroupBox();
            this.lbPW = new System.Windows.Forms.Label();
            this.txtPW = new System.Windows.Forms.TextBox();
            this.lbID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).BeginInit();
            this.gbAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxMax
            // 
            this.cbxMax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMax.Enabled = false;
            this.cbxMax.FormattingEnabled = true;
            this.cbxMax.Location = new System.Drawing.Point(375, 124);
            this.cbxMax.Name = "cbxMax";
            this.cbxMax.Size = new System.Drawing.Size(70, 24);
            this.cbxMax.TabIndex = 41;
            // 
            // cbxMin
            // 
            this.cbxMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMin.Enabled = false;
            this.cbxMin.FormattingEnabled = true;
            this.cbxMin.Location = new System.Drawing.Point(375, 96);
            this.cbxMin.Name = "cbxMin";
            this.cbxMin.Size = new System.Drawing.Size(70, 24);
            this.cbxMin.TabIndex = 40;
            // 
            // stt3
            // 
            this.stt3.AutoSize = true;
            this.stt3.ForeColor = System.Drawing.Color.Red;
            this.stt3.Location = new System.Drawing.Point(268, 319);
            this.stt3.Name = "stt3";
            this.stt3.Size = new System.Drawing.Size(160, 16);
            this.stt3.TabIndex = 39;
            this.stt3.Tag = "Số link đã duyệt: {0:##.##}/{1:##.##}";
            this.stt3.Text = "Số link đã duyệt: {0:##.##}";
            this.stt3.Visible = false;
            // 
            // stt2
            // 
            this.stt2.AutoSize = true;
            this.stt2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.stt2.Location = new System.Drawing.Point(268, 293);
            this.stt2.Name = "stt2";
            this.stt2.Size = new System.Drawing.Size(186, 16);
            this.stt2.TabIndex = 38;
            this.stt2.Tag = "Số link sẽ lấy dữ liệu: {0:##.##}";
            this.stt2.Text = "Số link sẽ lấy dữ liệu: {0:##.##}";
            this.stt2.Visible = false;
            // 
            // stt1
            // 
            this.stt1.AutoSize = true;
            this.stt1.ForeColor = System.Drawing.Color.Green;
            this.stt1.Location = new System.Drawing.Point(268, 268);
            this.stt1.Name = "stt1";
            this.stt1.Size = new System.Drawing.Size(148, 16);
            this.stt1.TabIndex = 36;
            this.stt1.Tag = "Tổng số trang: {0:##.##}";
            this.stt1.Text = "Tổng số trang: {0:##.##}";
            this.stt1.Visible = false;
            // 
            // cbxSlg
            // 
            this.cbxSlg.AutoSize = true;
            this.cbxSlg.Location = new System.Drawing.Point(271, 12);
            this.cbxSlg.Name = "cbxSlg";
            this.cbxSlg.Size = new System.Drawing.Size(80, 20);
            this.cbxSlg.TabIndex = 35;
            this.cbxSlg.Text = "Số lượng";
            this.cbxSlg.UseVisualStyleBackColor = true;
            this.cbxSlg.CheckedChanged += new System.EventHandler(this.cbxSlg_CheckedChanged);
            // 
            // numMaxSlg
            // 
            this.numMaxSlg.Enabled = false;
            this.numMaxSlg.Location = new System.Drawing.Point(357, 11);
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
            this.numMaxSlg.TabIndex = 34;
            this.numMaxSlg.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnRun
            // 
            this.btnRun.Image = global::EmployerInfo.Properties.Resources.info;
            this.btnRun.Location = new System.Drawing.Point(563, 42);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(81, 82);
            this.btnRun.TabIndex = 33;
            this.btnRun.Text = "GET INFO";
            this.btnRun.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // chxSelectAll
            // 
            this.chxSelectAll.AutoSize = true;
            this.chxSelectAll.Location = new System.Drawing.Point(12, 104);
            this.chxSelectAll.Name = "chxSelectAll";
            this.chxSelectAll.Size = new System.Drawing.Size(190, 20);
            this.chxSelectAll.TabIndex = 32;
            this.chxSelectAll.Text = "Các trường dữ liệu muốn lấy";
            this.chxSelectAll.UseVisualStyleBackColor = true;
            this.chxSelectAll.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // chkListBox
            // 
            this.chkListBox.FormattingEnabled = true;
            this.chkListBox.Items.AddRange(new object[] {
            "Họ và tên",
            "Giới tính",
            "Ngày sinh",
            "Email",
            "Điện thoại",
            "Địa chỉ",
            "Cấp bậc hiện tại",
            "Kinh nghiệm",
            "Trình độ cao nhất",
            "Ngoại ngữ",
            "Hôn nhân",
            "Cấp bậc mong muốn",
            "Địa điểm mong muốn",
            "Ngành nghề mong muốn",
            "Mức lương mong muốn",
            "Hình thức làm việc",
            "Mục tiêu nghề nghiệp",
            "Bằng cấp chứng chỉ",
            "Ngoại ngữ chi tiết",
            "Trình độ tin học",
            "Kinh nghiệm chi tiết",
            "Kỹ năng và sở trường",
            "Hồ sơ đính kèm",
            "Link ứng viên"});
            this.chkListBox.Location = new System.Drawing.Point(36, 129);
            this.chkListBox.Name = "chkListBox";
            this.chkListBox.Size = new System.Drawing.Size(191, 259);
            this.chkListBox.TabIndex = 31;
            // 
            // cbxPageLimit
            // 
            this.cbxPageLimit.AutoSize = true;
            this.cbxPageLimit.Location = new System.Drawing.Point(271, 68);
            this.cbxPageLimit.Name = "cbxPageLimit";
            this.cbxPageLimit.Size = new System.Drawing.Size(138, 20);
            this.cbxPageLimit.TabIndex = 30;
            this.cbxPageLimit.Text = "Giới hạn theo trang";
            this.cbxPageLimit.UseVisualStyleBackColor = true;
            this.cbxPageLimit.CheckedChanged += new System.EventHandler(this.cbxPageLimit_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(292, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 16);
            this.label3.TabIndex = 29;
            this.label3.Text = "Đến trang";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(292, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Từ trang";
            // 
            // cbxCategory
            // 
            this.cbxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategory.Enabled = false;
            this.cbxCategory.FormattingEnabled = true;
            this.cbxCategory.IntegralHeight = false;
            this.cbxCategory.ItemHeight = 16;
            this.cbxCategory.Location = new System.Drawing.Point(271, 230);
            this.cbxCategory.MaxDropDownItems = 12;
            this.cbxCategory.Name = "cbxCategory";
            this.cbxCategory.Size = new System.Drawing.Size(270, 24);
            this.cbxCategory.TabIndex = 44;
            this.cbxCategory.SelectedIndexChanged += new System.EventHandler(this.cbxCategory_SelectedIndexChanged);
            // 
            // dtpOnlyDay
            // 
            this.dtpOnlyDay.CustomFormat = "dd/MM/yyyy";
            this.dtpOnlyDay.Enabled = false;
            this.dtpOnlyDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOnlyDay.Location = new System.Drawing.Point(377, 40);
            this.dtpOnlyDay.Name = "dtpOnlyDay";
            this.dtpOnlyDay.Size = new System.Drawing.Size(89, 22);
            this.dtpOnlyDay.TabIndex = 45;
            // 
            // cbxOnlyDay
            // 
            this.cbxOnlyDay.AutoSize = true;
            this.cbxOnlyDay.Location = new System.Drawing.Point(271, 42);
            this.cbxOnlyDay.Name = "cbxOnlyDay";
            this.cbxOnlyDay.Size = new System.Drawing.Size(100, 20);
            this.cbxOnlyDay.TabIndex = 46;
            this.cbxOnlyDay.Text = "Chỉ lấy ngày";
            this.cbxOnlyDay.UseVisualStyleBackColor = true;
            this.cbxOnlyDay.CheckedChanged += new System.EventHandler(this.cbxOnlyDay_CheckedChanged);
            // 
            // lbCategory
            // 
            this.lbCategory.AutoSize = true;
            this.lbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCategory.Location = new System.Drawing.Point(268, 205);
            this.lbCategory.Name = "lbCategory";
            this.lbCategory.Size = new System.Drawing.Size(76, 16);
            this.lbCategory.TabIndex = 47;
            this.lbCategory.Text = "Danh mục";
            // 
            // gbAccount
            // 
            this.gbAccount.Controls.Add(this.lbPW);
            this.gbAccount.Controls.Add(this.txtPW);
            this.gbAccount.Controls.Add(this.lbID);
            this.gbAccount.Controls.Add(this.txtID);
            this.gbAccount.Location = new System.Drawing.Point(12, 8);
            this.gbAccount.Name = "gbAccount";
            this.gbAccount.Size = new System.Drawing.Size(215, 90);
            this.gbAccount.TabIndex = 48;
            this.gbAccount.TabStop = false;
            this.gbAccount.Text = "Tài khoản vieclam24h";
            // 
            // lbPW
            // 
            this.lbPW.AutoSize = true;
            this.lbPW.Location = new System.Drawing.Point(6, 52);
            this.lbPW.Name = "lbPW";
            this.lbPW.Size = new System.Drawing.Size(30, 16);
            this.lbPW.TabIndex = 3;
            this.lbPW.Text = "PW";
            // 
            // txtPW
            // 
            this.txtPW.ForeColor = System.Drawing.Color.Red;
            this.txtPW.Location = new System.Drawing.Point(42, 49);
            this.txtPW.Name = "txtPW";
            this.txtPW.Size = new System.Drawing.Size(167, 22);
            this.txtPW.TabIndex = 2;
            this.txtPW.Text = "daotao";
            this.txtPW.UseSystemPasswordChar = true;
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(6, 24);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(21, 16);
            this.lbID.TabIndex = 1;
            this.lbID.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.ForeColor = System.Drawing.Color.Red;
            this.txtID.Location = new System.Drawing.Point(42, 21);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(167, 22);
            this.txtID.TabIndex = 0;
            this.txtID.Text = "account.soft@yahoo.com";
            // 
            // frmM6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 425);
            this.Controls.Add(this.gbAccount);
            this.Controls.Add(this.lbCategory);
            this.Controls.Add(this.cbxOnlyDay);
            this.Controls.Add(this.dtpOnlyDay);
            this.Controls.Add(this.cbxCategory);
            this.Controls.Add(this.cbxMax);
            this.Controls.Add(this.cbxMin);
            this.Controls.Add(this.stt3);
            this.Controls.Add(this.stt2);
            this.Controls.Add(this.stt1);
            this.Controls.Add(this.cbxSlg);
            this.Controls.Add(this.numMaxSlg);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.chxSelectAll);
            this.Controls.Add(this.chkListBox);
            this.Controls.Add(this.cbxPageLimit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmM6";
            this.Text = "Việc làm 24h NTD";
            this.Load += new System.EventHandler(this.frmM6_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).EndInit();
            this.gbAccount.ResumeLayout(false);
            this.gbAccount.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxMax;
        private System.Windows.Forms.ComboBox cbxMin;
        private System.Windows.Forms.Label stt3;
        private System.Windows.Forms.Label stt2;
        private System.Windows.Forms.Label stt1;
        private System.Windows.Forms.CheckBox cbxSlg;
        private System.Windows.Forms.NumericUpDown numMaxSlg;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.CheckBox chxSelectAll;
        private System.Windows.Forms.CheckedListBox chkListBox;
        private System.Windows.Forms.CheckBox cbxPageLimit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxCategory;
        private System.Windows.Forms.DateTimePicker dtpOnlyDay;
        private System.Windows.Forms.CheckBox cbxOnlyDay;
        private System.Windows.Forms.Label lbCategory;
        private System.Windows.Forms.GroupBox gbAccount;
        private System.Windows.Forms.Label lbPW;
        private System.Windows.Forms.TextBox txtPW;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.TextBox txtID;

    }
}