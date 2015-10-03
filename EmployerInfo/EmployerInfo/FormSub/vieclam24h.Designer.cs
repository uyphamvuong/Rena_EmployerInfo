namespace EmployerInfo
{
    partial class frmM1
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
            this.dtpMin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpMax = new System.Windows.Forms.DateTimePicker();
            this.cbxTimeLimit = new System.Windows.Forms.CheckBox();
            this.chkListBox = new System.Windows.Forms.CheckedListBox();
            this.chxSelectAll = new System.Windows.Forms.CheckBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.numMaxSlg = new System.Windows.Forms.NumericUpDown();
            this.cbxSlg = new System.Windows.Forms.CheckBox();
            this.stt1 = new System.Windows.Forms.Label();
            this.stt2 = new System.Windows.Forms.Label();
            this.stt3 = new System.Windows.Forms.Label();
            this.stt4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxPortList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chxGetAllCategory = new System.Windows.Forms.CheckBox();
            this.chkListCategory = new System.Windows.Forms.CheckedListBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lFileName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpMin
            // 
            this.dtpMin.CustomFormat = "dd/MM";
            this.dtpMin.Enabled = false;
            this.dtpMin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMin.Location = new System.Drawing.Point(119, 38);
            this.dtpMin.Name = "dtpMin";
            this.dtpMin.Size = new System.Drawing.Size(53, 22);
            this.dtpMin.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(37, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Từ ngày";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(37, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Đến ngày";
            // 
            // dtpMax
            // 
            this.dtpMax.CustomFormat = "dd/MM";
            this.dtpMax.Enabled = false;
            this.dtpMax.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMax.Location = new System.Drawing.Point(119, 66);
            this.dtpMax.Name = "dtpMax";
            this.dtpMax.Size = new System.Drawing.Size(53, 22);
            this.dtpMax.TabIndex = 8;
            // 
            // cbxTimeLimit
            // 
            this.cbxTimeLimit.AutoSize = true;
            this.cbxTimeLimit.Location = new System.Drawing.Point(16, 12);
            this.cbxTimeLimit.Name = "cbxTimeLimit";
            this.cbxTimeLimit.Size = new System.Drawing.Size(163, 20);
            this.cbxTimeLimit.TabIndex = 10;
            this.cbxTimeLimit.Text = "Giới hạn hạn nộp hồ sơ";
            this.cbxTimeLimit.UseVisualStyleBackColor = true;
            this.cbxTimeLimit.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chkListBox
            // 
            this.chkListBox.FormattingEnabled = true;
            this.chkListBox.Items.AddRange(new object[] {
            "Tên công việc",
            "Hạn nộp hồ sơ",
            "Lượt xem",
            "Mã NTD",
            "Ngày làm mới",
            "Mức lương",
            "Kinh nghiệm",
            "Yêu cầu bằng cấp",
            "Số lượng cần tuyển",
            "Ngành nghề",
            "Địa điểm làm việc",
            "Chức vụ",
            "Hình thức làm việc",
            "Yêu cầu giới tính",
            "Yêu cầu độ tuổi",
            "Mô tả công việc",
            "Quyền lợi được hưởng",
            "Yêu cầu khác",
            "Hồ sơ bao gồm",
            "Người liên hệ",
            "Địa chỉ liên hệ",
            "Email liên hệ",
            "Điện thoại liên hệ",
            "Tên công ty",
            "Địa chỉ",
            "Quy mô",
            "Danh mục nghề"});
            this.chkListBox.Location = new System.Drawing.Point(13, 154);
            this.chkListBox.Name = "chkListBox";
            this.chkListBox.Size = new System.Drawing.Size(193, 259);
            this.chkListBox.TabIndex = 11;
            // 
            // chxSelectAll
            // 
            this.chxSelectAll.AutoSize = true;
            this.chxSelectAll.Location = new System.Drawing.Point(16, 132);
            this.chxSelectAll.Name = "chxSelectAll";
            this.chxSelectAll.Size = new System.Drawing.Size(190, 20);
            this.chxSelectAll.TabIndex = 13;
            this.chxSelectAll.Text = "Các trường dữ liệu muốn lấy";
            this.chxSelectAll.UseVisualStyleBackColor = true;
            this.chxSelectAll.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // btnRun
            // 
            this.btnRun.Image = global::EmployerInfo.Properties.Resources.info;
            this.btnRun.Location = new System.Drawing.Point(481, 25);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(156, 48);
            this.btnRun.TabIndex = 14;
            this.btnRun.Text = "GET INFO";
            this.btnRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // numMaxSlg
            // 
            this.numMaxSlg.Enabled = false;
            this.numMaxSlg.Location = new System.Drawing.Point(103, 104);
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
            this.cbxSlg.Location = new System.Drawing.Point(16, 105);
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
            this.stt1.ForeColor = System.Drawing.Color.Blue;
            this.stt1.Location = new System.Drawing.Point(228, 287);
            this.stt1.Name = "stt1";
            this.stt1.Size = new System.Drawing.Size(143, 16);
            this.stt1.TabIndex = 18;
            this.stt1.Tag = "Số danh mục: {0:##.##}";
            this.stt1.Text = "Số danh mục: {0:##.##}";
            this.stt1.Visible = false;
            // 
            // stt2
            // 
            this.stt2.AutoSize = true;
            this.stt2.ForeColor = System.Drawing.Color.Green;
            this.stt2.Location = new System.Drawing.Point(228, 312);
            this.stt2.Name = "stt2";
            this.stt2.Size = new System.Drawing.Size(226, 16);
            this.stt2.TabIndex = 19;
            this.stt2.Tag = "Số trang của các danh mục: {0:##.##}";
            this.stt2.Text = "Số trang của các danh mục: {0:##.##}";
            this.stt2.Visible = false;
            // 
            // stt3
            // 
            this.stt3.AutoSize = true;
            this.stt3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.stt3.Location = new System.Drawing.Point(228, 338);
            this.stt3.Name = "stt3";
            this.stt3.Size = new System.Drawing.Size(186, 16);
            this.stt3.TabIndex = 21;
            this.stt3.Tag = "Số link sẽ lấy dữ liệu: {0:##.##}";
            this.stt3.Text = "Số link sẽ lấy dữ liệu: {0:##.##}";
            this.stt3.Visible = false;
            // 
            // stt4
            // 
            this.stt4.AutoSize = true;
            this.stt4.ForeColor = System.Drawing.Color.Red;
            this.stt4.Location = new System.Drawing.Point(228, 364);
            this.stt4.Name = "stt4";
            this.stt4.Size = new System.Drawing.Size(252, 16);
            this.stt4.TabIndex = 22;
            this.stt4.Tag = "Số trang đã duyệt: {0:##.##}/{1}... {2:##.#}%";
            this.stt4.Text = "Số trang đã duyệt: {0:##.#}/{1}... {2:##.#}%";
            this.stt4.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = "Danh mục";
            // 
            // cbxPortList
            // 
            this.cbxPortList.DisplayMember = "Name";
            this.cbxPortList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPortList.ForeColor = System.Drawing.Color.Black;
            this.cbxPortList.IntegralHeight = false;
            this.cbxPortList.ItemHeight = 16;
            this.cbxPortList.Location = new System.Drawing.Point(231, 73);
            this.cbxPortList.MaxDropDownItems = 15;
            this.cbxPortList.Name = "cbxPortList";
            this.cbxPortList.Size = new System.Drawing.Size(236, 24);
            this.cbxPortList.TabIndex = 26;
            this.cbxPortList.ValueMember = "Link";
            this.cbxPortList.SelectedIndexChanged += new System.EventHandler(this.cbxPortList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(228, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Cổng lấy dữ liệu";
            // 
            // chxGetAllCategory
            // 
            this.chxGetAllCategory.AutoSize = true;
            this.chxGetAllCategory.Location = new System.Drawing.Point(467, 105);
            this.chxGetAllCategory.Name = "chxGetAllCategory";
            this.chxGetAllCategory.Size = new System.Drawing.Size(170, 20);
            this.chxGetAllCategory.TabIndex = 27;
            this.chxGetAllCategory.Text = "Lấy tất cả các danh mục";
            this.chxGetAllCategory.UseVisualStyleBackColor = true;
            this.chxGetAllCategory.CheckedChanged += new System.EventHandler(this.chxGetAllCategory_CheckedChanged);
            // 
            // chkListCategory
            // 
            this.chkListCategory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkListCategory.Enabled = false;
            this.chkListCategory.FormattingEnabled = true;
            this.chkListCategory.Location = new System.Drawing.Point(231, 131);
            this.chkListCategory.Name = "chkListCategory";
            this.chkListCategory.Size = new System.Drawing.Size(406, 136);
            this.chkListCategory.TabIndex = 30;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(231, 25);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(236, 22);
            this.txtFileName.TabIndex = 33;
            // 
            // lFileName
            // 
            this.lFileName.AutoSize = true;
            this.lFileName.Location = new System.Drawing.Point(228, 6);
            this.lFileName.Name = "lFileName";
            this.lFileName.Size = new System.Drawing.Size(84, 16);
            this.lFileName.TabIndex = 32;
            this.lFileName.Text = "Tên mở rộng";
            // 
            // frmM1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 436);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lFileName);
            this.Controls.Add(this.chkListCategory);
            this.Controls.Add(this.chxGetAllCategory);
            this.Controls.Add(this.cbxPortList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stt4);
            this.Controls.Add(this.stt3);
            this.Controls.Add(this.stt2);
            this.Controls.Add(this.stt1);
            this.Controls.Add(this.cbxSlg);
            this.Controls.Add(this.numMaxSlg);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.chxSelectAll);
            this.Controls.Add(this.chkListBox);
            this.Controls.Add(this.cbxTimeLimit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpMax);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpMin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmM1";
            this.Text = "frmM1";
            this.Load += new System.EventHandler(this.frmM1_Load);
            this.Shown += new System.EventHandler(this.frmM1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpMin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpMax;
        private System.Windows.Forms.CheckBox cbxTimeLimit;
        private System.Windows.Forms.CheckedListBox chkListBox;
        private System.Windows.Forms.CheckBox chxSelectAll;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.NumericUpDown numMaxSlg;
        private System.Windows.Forms.CheckBox cbxSlg;
        private System.Windows.Forms.Label stt1;
        private System.Windows.Forms.Label stt2;
        private System.Windows.Forms.Label stt3;
        private System.Windows.Forms.Label stt4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxPortList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chxGetAllCategory;
        private System.Windows.Forms.CheckedListBox chkListCategory;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lFileName;

    }
}