namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{
    partial class SlnPropertyFm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupLessonNumber = new System.Windows.Forms.GroupBox();
            this.lblLessonNumber3 = new System.Windows.Forms.Label();
            this.lblLessonNumber2 = new System.Windows.Forms.Label();
            this.lblLessonNumber1 = new System.Windows.Forms.Label();
            this.lblLessonNumber0 = new System.Windows.Forms.Label();
            this.LsNum3 = new System.Windows.Forms.NumericUpDown();
            this.LsNum2 = new System.Windows.Forms.NumericUpDown();
            this.LsNum1 = new System.Windows.Forms.NumericUpDown();
            this.LsNum0 = new System.Windows.Forms.NumericUpDown();
            this.lblActiveWeek = new System.Windows.Forms.Label();
            this.clActiveWeek = new System.Windows.Forms.CheckedListBox();
            this.groupLessonNumber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LsNum3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LsNum2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LsNum1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LsNum0)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(383, 259);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(293, 259);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // groupLessonNumber
            // 
            this.groupLessonNumber.Controls.Add(this.lblLessonNumber3);
            this.groupLessonNumber.Controls.Add(this.lblLessonNumber2);
            this.groupLessonNumber.Controls.Add(this.lblLessonNumber1);
            this.groupLessonNumber.Controls.Add(this.lblLessonNumber0);
            this.groupLessonNumber.Controls.Add(this.LsNum3);
            this.groupLessonNumber.Controls.Add(this.LsNum2);
            this.groupLessonNumber.Controls.Add(this.LsNum1);
            this.groupLessonNumber.Controls.Add(this.LsNum0);
            this.groupLessonNumber.Location = new System.Drawing.Point(251, 43);
            this.groupLessonNumber.Name = "groupLessonNumber";
            this.groupLessonNumber.Size = new System.Drawing.Size(210, 164);
            this.groupLessonNumber.TabIndex = 37;
            this.groupLessonNumber.TabStop = false;
            this.groupLessonNumber.Text = "各时段上课节数";
            // 
            // lblLessonNumber3
            // 
            this.lblLessonNumber3.AutoSize = true;
            this.lblLessonNumber3.Location = new System.Drawing.Point(18, 136);
            this.lblLessonNumber3.Name = "lblLessonNumber3";
            this.lblLessonNumber3.Size = new System.Drawing.Size(29, 12);
            this.lblLessonNumber3.TabIndex = 35;
            this.lblLessonNumber3.Text = "晚上";
            // 
            // lblLessonNumber2
            // 
            this.lblLessonNumber2.AutoSize = true;
            this.lblLessonNumber2.Location = new System.Drawing.Point(18, 97);
            this.lblLessonNumber2.Name = "lblLessonNumber2";
            this.lblLessonNumber2.Size = new System.Drawing.Size(29, 12);
            this.lblLessonNumber2.TabIndex = 34;
            this.lblLessonNumber2.Text = "下午";
            // 
            // lblLessonNumber1
            // 
            this.lblLessonNumber1.AutoSize = true;
            this.lblLessonNumber1.Location = new System.Drawing.Point(18, 60);
            this.lblLessonNumber1.Name = "lblLessonNumber1";
            this.lblLessonNumber1.Size = new System.Drawing.Size(29, 12);
            this.lblLessonNumber1.TabIndex = 33;
            this.lblLessonNumber1.Text = "上午";
            // 
            // lblLessonNumber0
            // 
            this.lblLessonNumber0.AutoSize = true;
            this.lblLessonNumber0.Location = new System.Drawing.Point(18, 24);
            this.lblLessonNumber0.Name = "lblLessonNumber0";
            this.lblLessonNumber0.Size = new System.Drawing.Size(29, 12);
            this.lblLessonNumber0.TabIndex = 32;
            this.lblLessonNumber0.Text = "早晨";
            // 
            // LsNum3
            // 
            this.LsNum3.Location = new System.Drawing.Point(74, 134);
            this.LsNum3.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.LsNum3.Name = "LsNum3";
            this.LsNum3.Size = new System.Drawing.Size(120, 21);
            this.LsNum3.TabIndex = 31;
            // 
            // LsNum2
            // 
            this.LsNum2.Location = new System.Drawing.Point(74, 95);
            this.LsNum2.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.LsNum2.Name = "LsNum2";
            this.LsNum2.Size = new System.Drawing.Size(120, 21);
            this.LsNum2.TabIndex = 30;
            // 
            // LsNum1
            // 
            this.LsNum1.Location = new System.Drawing.Point(74, 58);
            this.LsNum1.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.LsNum1.Name = "LsNum1";
            this.LsNum1.Size = new System.Drawing.Size(120, 21);
            this.LsNum1.TabIndex = 29;
            // 
            // LsNum0
            // 
            this.LsNum0.Location = new System.Drawing.Point(74, 22);
            this.LsNum0.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.LsNum0.Name = "LsNum0";
            this.LsNum0.Size = new System.Drawing.Size(120, 21);
            this.LsNum0.TabIndex = 28;
            // 
            // lblActiveWeek
            // 
            this.lblActiveWeek.AutoSize = true;
            this.lblActiveWeek.Location = new System.Drawing.Point(28, 43);
            this.lblActiveWeek.Name = "lblActiveWeek";
            this.lblActiveWeek.Size = new System.Drawing.Size(41, 12);
            this.lblActiveWeek.TabIndex = 34;
            this.lblActiveWeek.Text = "工作日";
            // 
            // clActiveWeek
            // 
            this.clActiveWeek.CheckOnClick = true;
            this.clActiveWeek.FormattingEnabled = true;
            this.clActiveWeek.Location = new System.Drawing.Point(85, 43);
            this.clActiveWeek.Name = "clActiveWeek";
            this.clActiveWeek.Size = new System.Drawing.Size(142, 164);
            this.clActiveWeek.TabIndex = 33;
            // 
            // SlnPropertyFm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(490, 305);
            this.ControlBox = false;
            this.Controls.Add(this.groupLessonNumber);
            this.Controls.Add(this.lblActiveWeek);
            this.Controls.Add(this.clActiveWeek);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SlnPropertyFm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "课表属性";
            this.Load += new System.EventHandler(this.SlnPropertyFm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SlnPropertyFm_FormClosing);
            this.groupLessonNumber.ResumeLayout(false);
            this.groupLessonNumber.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LsNum3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LsNum2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LsNum1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LsNum0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupLessonNumber;
        private System.Windows.Forms.Label lblLessonNumber3;
        private System.Windows.Forms.Label lblLessonNumber2;
        private System.Windows.Forms.Label lblLessonNumber1;
        private System.Windows.Forms.Label lblLessonNumber0;
        private System.Windows.Forms.NumericUpDown LsNum3;
        private System.Windows.Forms.NumericUpDown LsNum1;
        private System.Windows.Forms.NumericUpDown LsNum0;
        private System.Windows.Forms.Label lblActiveWeek;
        private System.Windows.Forms.CheckedListBox clActiveWeek;
        private System.Windows.Forms.NumericUpDown LsNum2;
    }
}