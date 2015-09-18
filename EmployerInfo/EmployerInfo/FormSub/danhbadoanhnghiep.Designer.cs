namespace EmployerInfo
{
    partial class frmM4
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
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.cbxGetFromCategory = new System.Windows.Forms.CheckBox();
            this.stt2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).BeginInit();
            this.SuspendLayout();
            // 
            // chkListBox
            // 
            this.chkListBox.FormattingEnabled = true;
            this.chkListBox.Items.AddRange(new object[] {
            "Tên tiếng việt",
            "Tên tiếng anh",
            "Tên viết tắt",
            "Giám đốc",
            "Địa chỉ",
            "Tỉnh/TP",
            "Loại hình",
            "Ngành",
            "Telephone",
            "Fax",
            "Email",
            "Website",
            "Hoạt động"});
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
            this.btnRun.Location = new System.Drawing.Point(553, 43);
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
            this.stt1.Location = new System.Drawing.Point(220, 107);
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
            this.stt3.Location = new System.Drawing.Point(220, 158);
            this.stt3.Name = "stt3";
            this.stt3.Size = new System.Drawing.Size(160, 16);
            this.stt3.TabIndex = 22;
            this.stt3.Tag = "Số link đã duyệt: {0:##.##}";
            this.stt3.Text = "Số link đã duyệt: {0:##.##}";
            this.stt3.Visible = false;
            // 
            // cbCategory
            // 
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(220, 69);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(274, 24);
            this.cbCategory.TabIndex = 23;
            // 
            // cbxGetFromCategory
            // 
            this.cbxGetFromCategory.AutoSize = true;
            this.cbxGetFromCategory.Checked = true;
            this.cbxGetFromCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxGetFromCategory.Location = new System.Drawing.Point(220, 43);
            this.cbxGetFromCategory.Name = "cbxGetFromCategory";
            this.cbxGetFromCategory.Size = new System.Drawing.Size(114, 20);
            this.cbxGetFromCategory.TabIndex = 24;
            this.cbxGetFromCategory.Text = "Lấy theo nhóm";
            this.cbxGetFromCategory.UseVisualStyleBackColor = true;
            this.cbxGetFromCategory.CheckedChanged += new System.EventHandler(this.cbxGetFromCategory_CheckedChanged);
            // 
            // stt2
            // 
            this.stt2.AutoSize = true;
            this.stt2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.stt2.Location = new System.Drawing.Point(220, 132);
            this.stt2.Name = "stt2";
            this.stt2.Size = new System.Drawing.Size(186, 16);
            this.stt2.TabIndex = 21;
            this.stt2.Tag = "Số link sẽ lấy dữ liệu: {0:##.##}";
            this.stt2.Text = "Số link sẽ lấy dữ liệu: {0:##.##}";
            this.stt2.Visible = false;
            // 
            // frmM4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 427);
            this.Controls.Add(this.cbxGetFromCategory);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.stt3);
            this.Controls.Add(this.stt2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.stt1);
            this.Controls.Add(this.cbxSlg);
            this.Controls.Add(this.numMaxSlg);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.chkListBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmM4";
            this.Text = "frmM4";
            this.Load += new System.EventHandler(this.frmM1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).EndInit();
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
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.CheckBox cbxGetFromCategory;
        private System.Windows.Forms.Label stt2;

    }
}