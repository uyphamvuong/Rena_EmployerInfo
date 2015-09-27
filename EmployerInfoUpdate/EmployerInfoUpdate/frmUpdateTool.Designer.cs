namespace EmployerInfoUpdate
{
    partial class frmUpdateTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateTool));
            this.lStatus = new System.Windows.Forms.Label();
            this.tmer = new System.Windows.Forms.Timer(this.components);
            this.pbProcess = new EmployerInfo.ProcessBarEdit();
            this.SuspendLayout();
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lStatus.ForeColor = System.Drawing.Color.Black;
            this.lStatus.Location = new System.Drawing.Point(10, 8);
            this.lStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(172, 17);
            this.lStatus.TabIndex = 0;
            this.lStatus.Text = "Và thật đáng để chờ đợi.... :)";
            // 
            // tmer
            // 
            this.tmer.Enabled = true;
            this.tmer.Tick += new System.EventHandler(this.tmer_Tick);
            // 
            // pbProcess
            // 
            this.pbProcess.ColorProcess = System.Drawing.Color.DeepSkyBlue;
            this.pbProcess.ColorUnderBar = System.Drawing.Color.LightGray;
            this.pbProcess.Location = new System.Drawing.Point(13, 29);
            this.pbProcess.Margin = new System.Windows.Forms.Padding(4);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(450, 10);
            this.pbProcess.TabIndex = 1;
            this.pbProcess.ValuePercent = 1;
            // 
            // frmUpdateTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 51);
            this.ControlBox = false;
            this.Controls.Add(this.pbProcess);
            this.Controls.Add(this.lStatus);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmUpdateTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Tool";
            this.Load += new System.EventHandler(this.frmUpdateTool_Load);
            this.Shown += new System.EventHandler(this.frmUpdateTool_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lStatus;
        private EmployerInfo.ProcessBarEdit pbProcess;
        private System.Windows.Forms.Timer tmer;
    }
}

