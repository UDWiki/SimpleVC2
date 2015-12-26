namespace Telossoft.SimpleVC.WinFormApp.Assist
{
    partial class WizardCourseFm
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
            this.CourseList = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbIfExistThenIgnore = new System.Windows.Forms.CheckBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CourseList
            // 
            this.CourseList.CheckOnClick = true;
            this.CourseList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CourseList.FormattingEnabled = true;
            this.CourseList.Items.AddRange(new object[] {
            "语文",
            "数学",
            "物理",
            "化学",
            "英语",
            "政治",
            "自然",
            "生物",
            "地理",
            "历史",
            "体育",
            "美术",
            "音乐"});
            this.CourseList.Location = new System.Drawing.Point(3, 3);
            this.CourseList.Name = "CourseList";
            this.CourseList.Size = new System.Drawing.Size(346, 292);
            this.CourseList.TabIndex = 0;
            this.CourseList.ThreeDCheckBoxes = true;
            this.CourseList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CourseList_KeyPress);
            this.CourseList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CourseList_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbIfExistThenIgnore);
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 304);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(346, 66);
            this.panel1.TabIndex = 1;
            // 
            // cbIfExistThenIgnore
            // 
            this.cbIfExistThenIgnore.AutoSize = true;
            this.cbIfExistThenIgnore.Checked = true;
            this.cbIfExistThenIgnore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIfExistThenIgnore.Location = new System.Drawing.Point(29, 27);
            this.cbIfExistThenIgnore.Name = "cbIfExistThenIgnore";
            this.cbIfExistThenIgnore.Size = new System.Drawing.Size(132, 16);
            this.cbIfExistThenIgnore.TabIndex = 2;
            this.cbIfExistThenIgnore.Text = "已存在的课程不添加";
            this.cbIfExistThenIgnore.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(258, 23);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(168, 23);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "确定";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // WizardCourseFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 373);
            this.Controls.Add(this.CourseList);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(360, 400);
            this.Name = "WizardCourseFm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "向导－添加课程";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CourseList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbIfExistThenIgnore;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
    }
}