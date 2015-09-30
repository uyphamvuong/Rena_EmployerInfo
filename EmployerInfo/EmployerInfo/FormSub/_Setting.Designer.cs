namespace EmployerInfo
{
    partial class frmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetting));
            this.btnSave = new System.Windows.Forms.Button();
            this.chxAutoCheckUpdate = new System.Windows.Forms.CheckBox();
            this.txtVersion = new System.Windows.Forms.Label();
            this.chxAskOpenFileWhenDone = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(152, 131);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 45);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chxAutoCheckUpdate
            // 
            this.chxAutoCheckUpdate.AutoSize = true;
            this.chxAutoCheckUpdate.Location = new System.Drawing.Point(12, 12);
            this.chxAutoCheckUpdate.Name = "chxAutoCheckUpdate";
            this.chxAutoCheckUpdate.Size = new System.Drawing.Size(182, 21);
            this.chxAutoCheckUpdate.TabIndex = 1;
            this.chxAutoCheckUpdate.Text = "Tự động kiểm tra cập nhật";
            this.chxAutoCheckUpdate.UseVisualStyleBackColor = true;
            // 
            // txtVersion
            // 
            this.txtVersion.AutoSize = true;
            this.txtVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersion.ForeColor = System.Drawing.Color.Gray;
            this.txtVersion.Location = new System.Drawing.Point(29, 36);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(117, 15);
            this.txtVersion.TabIndex = 2;
            this.txtVersion.Tag = "Phiên bản hiện tại: {0}";
            this.txtVersion.Text = "Phiên bản hiện tại: ...";
            // 
            // chxAskOpenFileWhenDone
            // 
            this.chxAskOpenFileWhenDone.AutoSize = true;
            this.chxAskOpenFileWhenDone.Location = new System.Drawing.Point(12, 64);
            this.chxAskOpenFileWhenDone.Name = "chxAskOpenFileWhenDone";
            this.chxAskOpenFileWhenDone.Size = new System.Drawing.Size(241, 21);
            this.chxAskOpenFileWhenDone.TabIndex = 3;
            this.chxAskOpenFileWhenDone.Text = "Hỏi mở file Excel khi lấy dữ liệu xong";
            this.chxAskOpenFileWhenDone.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(29, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(311, 15);
            this.label1.TabIndex = 4;
            this.label1.Tag = "";
            this.label1.Text = "Bỏ check: file sẽ được lưu và không có thông báo gì thêm";
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 192);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chxAskOpenFileWhenDone);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.chxAutoCheckUpdate);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cài đặt";
            this.Shown += new System.EventHandler(this.frmSetting_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chxAutoCheckUpdate;
        private System.Windows.Forms.Label txtVersion;
        private System.Windows.Forms.CheckBox chxAskOpenFileWhenDone;
        private System.Windows.Forms.Label label1;
    }
}