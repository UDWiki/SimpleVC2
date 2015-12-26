namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{
    partial class LessonEditFm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LessonEditFm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdLesson = new System.Windows.Forms.DataGridView();
            this.grdSqdLesson = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsTeacher = new System.Windows.Forms.ToolStripButton();
            this.MainTs = new System.Windows.Forms.ToolStrip();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSelectCrs = new System.Windows.Forms.ToolStripButton();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mmCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mmSelectCrs = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ts1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLesson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSqdLesson)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.MainTs.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(2, 51);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdLesson);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdSqdLesson);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(468, 218);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 7;
            // 
            // grdLesson
            // 
            this.grdLesson.AllowUserToAddRows = false;
            this.grdLesson.AllowUserToDeleteRows = false;
            this.grdLesson.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdLesson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLesson.Location = new System.Drawing.Point(0, 0);
            this.grdLesson.Name = "grdLesson";
            this.grdLesson.RowTemplate.Height = 23;
            this.grdLesson.Size = new System.Drawing.Size(227, 218);
            this.grdLesson.TabIndex = 2;
            this.grdLesson.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdLesson_CellValidated);
            this.grdLesson.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.InputDataError);
            this.grdLesson.SelectionChanged += new System.EventHandler(this.grdLesson_SelectionChanged);
            // 
            // grdSqdLesson
            // 
            this.grdSqdLesson.AllowUserToAddRows = false;
            this.grdSqdLesson.AllowUserToDeleteRows = false;
            this.grdSqdLesson.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSqdLesson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSqdLesson.Location = new System.Drawing.Point(0, 25);
            this.grdSqdLesson.Name = "grdSqdLesson";
            this.grdSqdLesson.RowTemplate.Height = 23;
            this.grdSqdLesson.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdSqdLesson.Size = new System.Drawing.Size(237, 193);
            this.grdSqdLesson.TabIndex = 3;
            this.grdSqdLesson.DoubleClick += new System.EventHandler(this.grdSqdLesson_DoubleClick);
            this.grdSqdLesson.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.InputDataError);
            this.grdSqdLesson.SelectionChanged += new System.EventHandler(this.grdSqdLesson_SelectionChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsTeacher});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(237, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsTeacher
            // 
            this.tsTeacher.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsTeacher.Image = ((System.Drawing.Image)(resources.GetObject("tsTeacher.Image")));
            this.tsTeacher.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsTeacher.Name = "tsTeacher";
            this.tsTeacher.Size = new System.Drawing.Size(177, 22);
            this.tsTeacher.Text = "设置教师(鼠标双击表格教师列)";
            this.tsTeacher.Click += new System.EventHandler(this.grdSqdLesson_DoubleClick);
            // 
            // MainTs
            // 
            this.MainTs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsOpen,
            this.toolStripSeparator4,
            this.tsSave,
            this.tsCancel,
            this.toolStripSeparator1,
            this.tsSelectCrs});
            this.MainTs.Location = new System.Drawing.Point(2, 26);
            this.MainTs.Name = "MainTs";
            this.MainTs.Size = new System.Drawing.Size(468, 25);
            this.MainTs.TabIndex = 8;
            this.MainTs.Text = "toolStrip2";
            // 
            // tsOpen
            // 
            this.tsOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsOpen.Image")));
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(33, 22);
            this.tsOpen.Text = "打开";
            this.tsOpen.Click += new System.EventHandler(this.mmOpen_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(33, 22);
            this.tsSave.Text = "保存";
            this.tsSave.Click += new System.EventHandler(this.mmSave_Click);
            // 
            // tsCancel
            // 
            this.tsCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsCancel.Image")));
            this.tsCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCancel.Name = "tsCancel";
            this.tsCancel.Size = new System.Drawing.Size(33, 22);
            this.tsCancel.Text = "撤消";
            this.tsCancel.Click += new System.EventHandler(this.mmCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsSelectCrs
            // 
            this.tsSelectCrs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsSelectCrs.Image = ((System.Drawing.Image)(resources.GetObject("tsSelectCrs.Image")));
            this.tsSelectCrs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSelectCrs.Name = "tsSelectCrs";
            this.tsSelectCrs.Size = new System.Drawing.Size(57, 22);
            this.tsSelectCrs.Text = "选择课程";
            this.tsSelectCrs.Click += new System.EventHandler(this.mmSelectCrs_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmFile,
            this.mmEdit});
            this.MainMenu.Location = new System.Drawing.Point(2, 2);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(468, 24);
            this.MainMenu.TabIndex = 10;
            this.MainMenu.Text = "menuStrip1";
            // 
            // mmFile
            // 
            this.mmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmOpen,
            this.toolStripSeparator6,
            this.mmSave,
            this.mmCancel,
            this.toolStripSeparator3,
            this.mmExit});
            this.mmFile.Name = "mmFile";
            this.mmFile.Size = new System.Drawing.Size(41, 20);
            this.mmFile.Text = "文件";
            // 
            // mmOpen
            // 
            this.mmOpen.Name = "mmOpen";
            this.mmOpen.Size = new System.Drawing.Size(94, 22);
            this.mmOpen.Text = "打开";
            this.mmOpen.Click += new System.EventHandler(this.mmOpen_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(91, 6);
            // 
            // mmSave
            // 
            this.mmSave.Name = "mmSave";
            this.mmSave.Size = new System.Drawing.Size(94, 22);
            this.mmSave.Text = "保存";
            this.mmSave.Click += new System.EventHandler(this.mmSave_Click);
            // 
            // mmCancel
            // 
            this.mmCancel.Name = "mmCancel";
            this.mmCancel.Size = new System.Drawing.Size(94, 22);
            this.mmCancel.Text = "撤消";
            this.mmCancel.Click += new System.EventHandler(this.mmCancel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(91, 6);
            // 
            // mmExit
            // 
            this.mmExit.Name = "mmExit";
            this.mmExit.Size = new System.Drawing.Size(94, 22);
            this.mmExit.Text = "退出";
            this.mmExit.Click += new System.EventHandler(this.mmExit_Click);
            // 
            // mmEdit
            // 
            this.mmEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmSelectCrs});
            this.mmEdit.Name = "mmEdit";
            this.mmEdit.Size = new System.Drawing.Size(41, 20);
            this.mmEdit.Text = "编辑";
            // 
            // mmSelectCrs
            // 
            this.mmSelectCrs.Name = "mmSelectCrs";
            this.mmSelectCrs.Size = new System.Drawing.Size(118, 22);
            this.mmSelectCrs.Text = "选择课程";
            this.mmSelectCrs.Click += new System.EventHandler(this.mmSelectCrs_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts1});
            this.statusStrip1.Location = new System.Drawing.Point(2, 269);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(468, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ts1
            // 
            this.ts1.Name = "ts1";
            this.ts1.Size = new System.Drawing.Size(0, 17);
            // 
            // LessonEditFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 293);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.MainTs);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(480, 320);
            this.Name = "LessonEditFm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "课务安排";
            this.Load += new System.EventHandler(this.LessonEditFm_Load);
            this.Shown += new System.EventHandler(this.LessonEditFm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LessonEditFm_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLesson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSqdLesson)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.MainTs.ResumeLayout(false);
            this.MainTs.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView grdLesson;
        private System.Windows.Forms.DataGridView grdSqdLesson;
        private System.Windows.Forms.ToolStrip MainTs;
        private System.Windows.Forms.ToolStripButton tsSelectCrs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mmFile;
        private System.Windows.Forms.ToolStripMenuItem mmSave;
        private System.Windows.Forms.ToolStripMenuItem mmCancel;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mmExit;
        private System.Windows.Forms.ToolStripMenuItem mmOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mmEdit;
        private System.Windows.Forms.ToolStripMenuItem mmSelectCrs;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsTeacher;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ts1;
    }
}