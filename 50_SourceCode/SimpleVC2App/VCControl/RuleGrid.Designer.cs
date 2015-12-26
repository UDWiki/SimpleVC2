namespace Telossoft.SimpleVC.WinFormApp.VCControl
{
    partial class RuleGrid
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbNowRule = new System.Windows.Forms.Button();
            this.tbExcellent = new System.Windows.Forms.Panel();
            this.tbCommon = new System.Windows.Forms.Panel();
            this.tbFine = new System.Windows.Forms.Panel();
            this.tbIll = new System.Windows.Forms.Panel();
            this.tbCrisscross = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNowRule
            // 
            this.tbNowRule.Enabled = false;
            this.tbNowRule.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbNowRule.Location = new System.Drawing.Point(19, 26);
            this.tbNowRule.Name = "tbNowRule";
            this.tbNowRule.Size = new System.Drawing.Size(51, 23);
            this.tbNowRule.TabIndex = 6;
            this.tbNowRule.Text = "当前";
            this.tbNowRule.UseVisualStyleBackColor = true;
            // 
            // tbExcellent
            // 
            this.tbExcellent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExcellent.Location = new System.Drawing.Point(19, 76);
            this.tbExcellent.Name = "tbExcellent";
            this.tbExcellent.Size = new System.Drawing.Size(51, 23);
            this.tbExcellent.TabIndex = 11;
            // 
            // tbCommon
            // 
            this.tbCommon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCommon.Location = new System.Drawing.Point(19, 138);
            this.tbCommon.Name = "tbCommon";
            this.tbCommon.Size = new System.Drawing.Size(51, 23);
            this.tbCommon.TabIndex = 12;
            // 
            // tbFine
            // 
            this.tbFine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFine.Location = new System.Drawing.Point(19, 107);
            this.tbFine.Name = "tbFine";
            this.tbFine.Size = new System.Drawing.Size(51, 23);
            this.tbFine.TabIndex = 13;
            // 
            // tbIll
            // 
            this.tbIll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbIll.Location = new System.Drawing.Point(19, 169);
            this.tbIll.Name = "tbIll";
            this.tbIll.Size = new System.Drawing.Size(51, 23);
            this.tbIll.TabIndex = 14;
            // 
            // tbCrisscross
            // 
            this.tbCrisscross.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCrisscross.Location = new System.Drawing.Point(19, 200);
            this.tbCrisscross.Name = "tbCrisscross";
            this.tbCrisscross.Size = new System.Drawing.Size(51, 23);
            this.tbCrisscross.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(94, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 343);
            this.panel1.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbNowRule);
            this.panel2.Controls.Add(this.tbExcellent);
            this.panel2.Controls.Add(this.tbCrisscross);
            this.panel2.Controls.Add(this.tbCommon);
            this.panel2.Controls.Add(this.tbIll);
            this.panel2.Controls.Add(this.tbFine);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(94, 343);
            this.panel2.TabIndex = 17;
            // 
            // RuleGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "RuleGrid";
            this.Size = new System.Drawing.Size(467, 343);
            this.Load += new System.EventHandler(this.RuleGrid_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button tbNowRule;
        private System.Windows.Forms.Panel tbExcellent;
        private System.Windows.Forms.Panel tbCommon;
        private System.Windows.Forms.Panel tbFine;
        private System.Windows.Forms.Panel tbIll;
        private System.Windows.Forms.Panel tbCrisscross;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;

    }
}
