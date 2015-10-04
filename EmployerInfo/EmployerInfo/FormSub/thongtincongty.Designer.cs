namespace EmployerInfo
{
    partial class frmThongTinCongTy
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
            this.cbxPageLimit = new System.Windows.Forms.CheckBox();
            this.chkListBox = new System.Windows.Forms.CheckedListBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.numMaxSlg = new System.Windows.Forms.NumericUpDown();
            this.cbxSlg = new System.Windows.Forms.CheckBox();
            this.stt1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.stt2 = new System.Windows.Forms.Label();
            this.stt3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxMin = new System.Windows.Forms.ComboBox();
            this.cbxMax = new System.Windows.Forms.ComboBox();
            this.cbxNewItem = new System.Windows.Forms.CheckBox();
            this.txtLastID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxPageLimit
            // 
            this.cbxPageLimit.AutoSize = true;
            this.cbxPageLimit.Location = new System.Drawing.Point(16, 12);
            this.cbxPageLimit.Name = "cbxPageLimit";
            this.cbxPageLimit.Size = new System.Drawing.Size(138, 20);
            this.cbxPageLimit.TabIndex = 10;
            this.cbxPageLimit.Text = "Giới hạn theo trang";
            this.cbxPageLimit.UseVisualStyleBackColor = true;
            this.cbxPageLimit.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chkListBox
            // 
            this.chkListBox.FormattingEnabled = true;
            this.chkListBox.Items.AddRange(new object[] {
            "Tên công ty",
            "Địa chỉ",
            "Giám đốc/Đại diện pháp luật",
            "Giấy phép kinh doanh",
            "Ngày cấp",
            "Mã số thuế",
            "Ngày hoạt động",
            "Hoạt động chính",
            "Điện thoại"});
            this.chkListBox.Location = new System.Drawing.Point(40, 129);
            this.chkListBox.Name = "chkListBox";
            this.chkListBox.Size = new System.Drawing.Size(191, 259);
            this.chkListBox.TabIndex = 11;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(16, 104);
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
            this.btnRun.Location = new System.Drawing.Point(567, 42);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(81, 82);
            this.btnRun.TabIndex = 14;
            this.btnRun.Text = "GET INFO";
            this.btnRun.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // numMaxSlg
            // 
            this.numMaxSlg.Enabled = false;
            this.numMaxSlg.Location = new System.Drawing.Point(361, 11);
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
            this.cbxSlg.Location = new System.Drawing.Point(275, 12);
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
            this.stt1.Location = new System.Drawing.Point(272, 156);
            this.stt1.Name = "stt1";
            this.stt1.Size = new System.Drawing.Size(148, 16);
            this.stt1.TabIndex = 18;
            this.stt1.Tag = "Tổng số trang: {0:##.##}";
            this.stt1.Text = "Tổng số trang: {0:##.##}";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(253, 403);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(395, 12);
            this.progressBar1.TabIndex = 20;
            this.progressBar1.Visible = false;
            // 
            // stt2
            // 
            this.stt2.AutoSize = true;
            this.stt2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.stt2.Location = new System.Drawing.Point(272, 181);
            this.stt2.Name = "stt2";
            this.stt2.Size = new System.Drawing.Size(186, 16);
            this.stt2.TabIndex = 21;
            this.stt2.Tag = "Số link sẽ lấy dữ liệu: {0:##.##}";
            this.stt2.Text = "Số link sẽ lấy dữ liệu: {0:##.##}";
            this.stt2.Visible = false;
            // 
            // stt3
            // 
            this.stt3.AutoSize = true;
            this.stt3.ForeColor = System.Drawing.Color.Red;
            this.stt3.Location = new System.Drawing.Point(272, 207);
            this.stt3.Name = "stt3";
            this.stt3.Size = new System.Drawing.Size(160, 16);
            this.stt3.TabIndex = 22;
            this.stt3.Tag = "Số link đã duyệt: {0:##.##}";
            this.stt3.Text = "Số link đã duyệt: {0:##.##}";
            this.stt3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(37, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Từ trang";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(37, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Đến trang";
            // 
            // cbxMin
            // 
            this.cbxMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMin.Enabled = false;
            this.cbxMin.FormattingEnabled = true;
            this.cbxMin.Location = new System.Drawing.Point(120, 40);
            this.cbxMin.Name = "cbxMin";
            this.cbxMin.Size = new System.Drawing.Size(70, 24);
            this.cbxMin.TabIndex = 23;
            // 
            // cbxMax
            // 
            this.cbxMax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMax.Enabled = false;
            this.cbxMax.FormattingEnabled = true;
            this.cbxMax.Location = new System.Drawing.Point(120, 68);
            this.cbxMax.Name = "cbxMax";
            this.cbxMax.Size = new System.Drawing.Size(70, 24);
            this.cbxMax.TabIndex = 24;
            // 
            // cbxNewItem
            // 
            this.cbxNewItem.AutoSize = true;
            this.cbxNewItem.Location = new System.Drawing.Point(275, 42);
            this.cbxNewItem.Name = "cbxNewItem";
            this.cbxNewItem.Size = new System.Drawing.Size(144, 20);
            this.cbxNewItem.TabIndex = 26;
            this.cbxNewItem.Text = "Chỉ lấy thông tin mới";
            this.cbxNewItem.UseVisualStyleBackColor = true;
            this.cbxNewItem.CheckedChanged += new System.EventHandler(this.cbxNewItem_CheckedChanged);
            // 
            // txtLastID
            // 
            this.txtLastID.Enabled = false;
            this.txtLastID.Location = new System.Drawing.Point(425, 40);
            this.txtLastID.Name = "txtLastID";
            this.txtLastID.Size = new System.Drawing.Size(95, 22);
            this.txtLastID.TabIndex = 27;
            // 
            // frmThongTinCongTy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 427);
            this.Controls.Add(this.txtLastID);
            this.Controls.Add(this.cbxNewItem);
            this.Controls.Add(this.cbxMax);
            this.Controls.Add(this.cbxMin);
            this.Controls.Add(this.stt3);
            this.Controls.Add(this.stt2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.stt1);
            this.Controls.Add(this.cbxSlg);
            this.Controls.Add(this.numMaxSlg);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.chkListBox);
            this.Controls.Add(this.cbxPageLimit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmThongTinCongTy";
            this.Text = "frmM2";
            this.Load += new System.EventHandler(this.frmM1_Load);
            this.Shown += new System.EventHandler(this.frmM2_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxPageLimit;
        private System.Windows.Forms.CheckedListBox chkListBox;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.NumericUpDown numMaxSlg;
        private System.Windows.Forms.CheckBox cbxSlg;
        private System.Windows.Forms.Label stt1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label stt2;
        private System.Windows.Forms.Label stt3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxMin;
        private System.Windows.Forms.ComboBox cbxMax;
        private System.Windows.Forms.CheckBox cbxNewItem;
        private System.Windows.Forms.TextBox txtLastID;

    }
}