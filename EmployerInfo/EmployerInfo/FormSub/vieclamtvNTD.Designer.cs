namespace EmployerInfo
{
    partial class frmVieclamtvNTD
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
            this.chxSelectAll = new System.Windows.Forms.CheckBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.numMaxSlg = new System.Windows.Forms.NumericUpDown();
            this.cbxSlg = new System.Windows.Forms.CheckBox();
            this.stt1 = new System.Windows.Forms.Label();
            this.stt2 = new System.Windows.Forms.Label();
            this.stt3 = new System.Windows.Forms.Label();
            this.stt4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chxGetAllCategory = new System.Windows.Forms.CheckBox();
            this.chkListCategory = new System.Windows.Forms.CheckedListBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lFileName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).BeginInit();
            this.SuspendLayout();
            // 
            // chkListBox
            // 
            this.chkListBox.FormattingEnabled = true;
            this.chkListBox.Items.AddRange(new object[] {
            "Tên công ty",
            "Quy mô công ty",
            "Loại hình công ty",
            "Điện thoại",
            "Email",
            "Địa chỉ",
            "Website",
            "Giới thiệu",
            "Ngành nghề tuyển dụng",
            "Danh mục"});
            this.chkListBox.Location = new System.Drawing.Point(9, 94);
            this.chkListBox.Name = "chkListBox";
            this.chkListBox.Size = new System.Drawing.Size(193, 310);
            this.chkListBox.TabIndex = 11;
            // 
            // chxSelectAll
            // 
            this.chxSelectAll.AutoSize = true;
            this.chxSelectAll.Location = new System.Drawing.Point(12, 68);
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
            this.btnRun.Location = new System.Drawing.Point(481, 11);
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
            this.numMaxSlg.Location = new System.Drawing.Point(99, 25);
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
            this.cbxSlg.Location = new System.Drawing.Point(12, 26);
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
            this.label1.Location = new System.Drawing.Point(228, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = "Danh mục";
            // 
            // chxGetAllCategory
            // 
            this.chxGetAllCategory.AutoSize = true;
            this.chxGetAllCategory.Location = new System.Drawing.Point(467, 68);
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
            this.chkListCategory.Location = new System.Drawing.Point(231, 94);
            this.chkListCategory.Name = "chkListCategory";
            this.chkListCategory.Size = new System.Drawing.Size(406, 170);
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
            // frmVieclamtvNTD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 436);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lFileName);
            this.Controls.Add(this.chkListCategory);
            this.Controls.Add(this.chxGetAllCategory);
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
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmVieclamtvNTD";
            this.Text = "frm08";
            this.Load += new System.EventHandler(this.frmM1_Load);
            this.Shown += new System.EventHandler(this.frmM1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSlg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.CheckBox chxGetAllCategory;
        private System.Windows.Forms.CheckedListBox chkListCategory;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lFileName;

    }
}