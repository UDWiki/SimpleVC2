namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{
    partial class TopScheduleFm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Spanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.STchLab = new System.Windows.Forms.Label();
            this.Tpanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TTchLab = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Spanel);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1MinSize = 50;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Tpanel);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2MinSize = 50;
            this.splitContainer1.Size = new System.Drawing.Size(442, 223);
            this.splitContainer1.SplitterDistance = 212;
            this.splitContainer1.TabIndex = 0;
            // 
            // Spanel
            // 
            this.Spanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Spanel.Location = new System.Drawing.Point(0, 27);
            this.Spanel.Name = "Spanel";
            this.Spanel.Size = new System.Drawing.Size(212, 196);
            this.Spanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.STchLab);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 27);
            this.panel1.TabIndex = 0;
            // 
            // STchLab
            // 
            this.STchLab.AutoSize = true;
            this.STchLab.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.STchLab.Location = new System.Drawing.Point(12, 9);
            this.STchLab.Name = "STchLab";
            this.STchLab.Size = new System.Drawing.Size(65, 12);
            this.STchLab.TabIndex = 0;
            this.STchLab.Text = "关注的教师";
            // 
            // Tpanel
            // 
            this.Tpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tpanel.Location = new System.Drawing.Point(0, 27);
            this.Tpanel.Name = "Tpanel";
            this.Tpanel.Size = new System.Drawing.Size(226, 196);
            this.Tpanel.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TTchLab);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(226, 27);
            this.panel3.TabIndex = 2;
            // 
            // TTchLab
            // 
            this.TTchLab.AutoSize = true;
            this.TTchLab.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TTchLab.Location = new System.Drawing.Point(12, 9);
            this.TTchLab.Name = "TTchLab";
            this.TTchLab.Size = new System.Drawing.Size(65, 12);
            this.TTchLab.TabIndex = 0;
            this.TTchLab.Text = "当前的教师";
            // 
            // TopScheduleFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 223);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(450, 250);
            this.Name = "TopScheduleFm";
            this.Text = "参考的教师课表";
            this.Load += new System.EventHandler(this.TopScheduleFm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label STchLab;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label TTchLab;
        private System.Windows.Forms.Panel Spanel;
        private System.Windows.Forms.Panel Tpanel;
    }
}