namespace EmployerInfo
{
    partial class ProcessBarEdit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pUnderBar = new System.Windows.Forms.Panel();
            this.pProcess = new System.Windows.Forms.Panel();
            this.pUnderBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pUnderBar
            // 
            this.pUnderBar.BackColor = System.Drawing.Color.AliceBlue;
            this.pUnderBar.Controls.Add(this.pProcess);
            this.pUnderBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pUnderBar.Location = new System.Drawing.Point(0, 0);
            this.pUnderBar.Name = "pUnderBar";
            this.pUnderBar.Size = new System.Drawing.Size(500, 8);
            this.pUnderBar.TabIndex = 0;
            // 
            // pProcess
            // 
            this.pProcess.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.pProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.pProcess.Location = new System.Drawing.Point(0, 0);
            this.pProcess.Name = "pProcess";
            this.pProcess.Size = new System.Drawing.Size(96, 8);
            this.pProcess.TabIndex = 0;
            // 
            // ProcessBarEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pUnderBar);
            this.Name = "ProcessBarEdit";
            this.Size = new System.Drawing.Size(500, 8);
            this.Load += new System.EventHandler(this.ProcessBarEdit_Load);
            this.pUnderBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pUnderBar;
        private System.Windows.Forms.Panel pProcess;
    }
}
