namespace Telossoft.SimpleVC.WinFormApp.BaseData
{
    partial class GroupMemberFm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupMemberFm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MainGrd = new System.Windows.Forms.DataGridView();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.gbGroupEntity = new System.Windows.Forms.GroupBox();
            this.AttachGrd = new System.Windows.Forms.DataGridView();
            this.tsAttach = new System.Windows.Forms.ToolStrip();
            this.tsAttachAdd = new System.Windows.Forms.ToolStripButton();
            this.tsAttachRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsAttachRule = new System.Windows.Forms.ToolStripButton();
            this.panelGroupMember = new System.Windows.Forms.Panel();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.picGrp = new System.Windows.Forms.PictureBox();
            this.lbColor = new System.Windows.Forms.Label();
            this.btContinueAdd = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbMbrName = new System.Windows.Forms.Label();
            this.tsGrpMbr = new System.Windows.Forms.ToolStrip();
            this.tsAdd = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.tssMbr1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsCancel = new System.Windows.Forms.ToolStripButton();
            this.tssMbr2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRule = new System.Windows.Forms.ToolStripButton();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbEntityGroupSwitch = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).BeginInit();
            this.gbFilter.SuspendLayout();
            this.gbGroupEntity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AttachGrd)).BeginInit();
            this.tsAttach.SuspendLayout();
            this.panelGroupMember.SuspendLayout();
            this.panelEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGrp)).BeginInit();
            this.tsGrpMbr.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(2, 27);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(600, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.MainGrd);
            this.splitContainer1.Panel1.Controls.Add(this.gbFilter);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScrollMargin = new System.Drawing.Size(100, 0);
            this.splitContainer1.Panel2.Controls.Add(this.gbGroupEntity);
            this.splitContainer1.Panel2.Controls.Add(this.panelGroupMember);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Panel2MinSize = 280;
            this.splitContainer1.Size = new System.Drawing.Size(628, 344);
            this.splitContainer1.SplitterDistance = 328;
            this.splitContainer1.TabIndex = 2;
            // 
            // MainGrd
            // 
            this.MainGrd.AllowUserToAddRows = false;
            this.MainGrd.AllowUserToDeleteRows = false;
            this.MainGrd.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MainGrd.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.MainGrd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MainGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainGrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGrd.Location = new System.Drawing.Point(2, 60);
            this.MainGrd.Name = "MainGrd";
            this.MainGrd.ReadOnly = true;
            this.MainGrd.RowTemplate.Height = 23;
            this.MainGrd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MainGrd.Size = new System.Drawing.Size(324, 282);
            this.MainGrd.TabIndex = 6;
            this.MainGrd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainGrd_KeyDown);
            this.MainGrd.DoubleClick += new System.EventHandler(this.MainGrd_DoubleClick);
            this.MainGrd.SelectionChanged += new System.EventHandler(this.MainGrd_SelectionChanged);
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.label3);
            this.gbFilter.Controls.Add(this.btnFilter);
            this.gbFilter.Controls.Add(this.tbFilter);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFilter.Location = new System.Drawing.Point(2, 2);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(324, 58);
            this.gbFilter.TabIndex = 5;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "过滤";
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(226, 18);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(49, 23);
            this.btnFilter.TabIndex = 6;
            this.btnFilter.Text = "过滤";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // gbGroupEntity
            // 
            this.gbGroupEntity.Controls.Add(this.AttachGrd);
            this.gbGroupEntity.Controls.Add(this.tsAttach);
            this.gbGroupEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGroupEntity.Location = new System.Drawing.Point(2, 133);
            this.gbGroupEntity.Name = "gbGroupEntity";
            this.gbGroupEntity.Size = new System.Drawing.Size(292, 209);
            this.gbGroupEntity.TabIndex = 17;
            this.gbGroupEntity.TabStop = false;
            this.gbGroupEntity.Text = "组/成员";
            // 
            // AttachGrd
            // 
            this.AttachGrd.AllowUserToAddRows = false;
            this.AttachGrd.AllowUserToDeleteRows = false;
            this.AttachGrd.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AttachGrd.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.AttachGrd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AttachGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AttachGrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AttachGrd.Location = new System.Drawing.Point(3, 42);
            this.AttachGrd.Name = "AttachGrd";
            this.AttachGrd.ReadOnly = true;
            this.AttachGrd.RowTemplate.Height = 23;
            this.AttachGrd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AttachGrd.Size = new System.Drawing.Size(286, 164);
            this.AttachGrd.TabIndex = 0;
            this.AttachGrd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AttachGrd_KeyDown);
            this.AttachGrd.DoubleClick += new System.EventHandler(this.AttachGrd_DoubleClick);
            this.AttachGrd.SelectionChanged += new System.EventHandler(this.AttachGrd_SelectionChanged);
            // 
            // tsAttach
            // 
            this.tsAttach.Items.AddRange(new System.Windows.Forms.ToolStripItem[ ] {
            this.tsAttachAdd,
            this.tsAttachRemove,
            this.toolStripSeparator2,
            this.tsAttachRule});
            this.tsAttach.Location = new System.Drawing.Point(3, 17);
            this.tsAttach.Name = "tsAttach";
            this.tsAttach.Size = new System.Drawing.Size(286, 25);
            this.tsAttach.TabIndex = 25;
            this.tsAttach.Text = "toolStrip1";
            // 
            // tsAttachAdd
            // 
            this.tsAttachAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsAttachAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsAttachAdd.Image")));
            this.tsAttachAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAttachAdd.Name = "tsAttachAdd";
            this.tsAttachAdd.Size = new System.Drawing.Size(23, 22);
            this.tsAttachAdd.Text = "编辑";
            this.tsAttachAdd.Click += new System.EventHandler(this.tsAttachAdd_Click);
            // 
            // tsAttachRemove
            // 
            this.tsAttachRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsAttachRemove.Image = ((System.Drawing.Image)(resources.GetObject("tsAttachRemove.Image")));
            this.tsAttachRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAttachRemove.Name = "tsAttachRemove";
            this.tsAttachRemove.Size = new System.Drawing.Size(23, 22);
            this.tsAttachRemove.Text = "删除";
            this.tsAttachRemove.Click += new System.EventHandler(this.btnAttachRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsAttachRule
            // 
            this.tsAttachRule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsAttachRule.Image = ((System.Drawing.Image)(resources.GetObject("tsAttachRule.Image")));
            this.tsAttachRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAttachRule.Name = "tsAttachRule";
            this.tsAttachRule.Size = new System.Drawing.Size(23, 22);
            this.tsAttachRule.Text = "规则";
            this.tsAttachRule.Click += new System.EventHandler(this.tsAttachRule_Click);
            // 
            // panelGroupMember
            // 
            this.panelGroupMember.Controls.Add(this.panelEdit);
            this.panelGroupMember.Controls.Add(this.tsGrpMbr);
            this.panelGroupMember.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGroupMember.Location = new System.Drawing.Point(2, 2);
            this.panelGroupMember.Name = "panelGroupMember";
            this.panelGroupMember.Size = new System.Drawing.Size(292, 131);
            this.panelGroupMember.TabIndex = 15;
            this.panelGroupMember.Leave += new System.EventHandler(this.panelGroupMember_Leave);
            // 
            // panelEdit
            // 
            this.panelEdit.Controls.Add(this.picGrp);
            this.panelEdit.Controls.Add(this.lbColor);
            this.panelEdit.Controls.Add(this.btContinueAdd);
            this.panelEdit.Controls.Add(this.tbName);
            this.panelEdit.Controls.Add(this.lbMbrName);
            this.panelEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEdit.Location = new System.Drawing.Point(0, 25);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(292, 106);
            this.panelEdit.TabIndex = 26;
            // 
            // picGrp
            // 
            this.picGrp.Image = ((System.Drawing.Image)(resources.GetObject("picGrp.Image")));
            this.picGrp.Location = new System.Drawing.Point(19, 48);
            this.picGrp.Name = "picGrp";
            this.picGrp.Size = new System.Drawing.Size(67, 43);
            this.picGrp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picGrp.TabIndex = 33;
            this.picGrp.TabStop = false;
            // 
            // lbColor
            // 
            this.lbColor.AutoSize = true;
            this.lbColor.BackColor = System.Drawing.SystemColors.Control;
            this.lbColor.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbColor.Location = new System.Drawing.Point(70, 57);
            this.lbColor.Name = "lbColor";
            this.lbColor.Size = new System.Drawing.Size(83, 12);
            this.lbColor.TabIndex = 32;
            this.lbColor.Text = "此课程显示色";
            this.lbColor.DoubleClick += new System.EventHandler(this.lbColor_DoubleClick);
            // 
            // btContinueAdd
            // 
            this.btContinueAdd.Location = new System.Drawing.Point(193, 66);
            this.btContinueAdd.Name = "btContinueAdd";
            this.btContinueAdd.Size = new System.Drawing.Size(75, 23);
            this.btContinueAdd.TabIndex = 31;
            this.btContinueAdd.Text = "保存并继续";
            this.btContinueAdd.UseVisualStyleBackColor = true;
            this.btContinueAdd.Click += new System.EventHandler(this.btContinueAdd_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(72, 21);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(196, 21);
            this.tbName.TabIndex = 29;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbName_KeyDown);
            // 
            // lbMbrName
            // 
            this.lbMbrName.AutoSize = true;
            this.lbMbrName.Location = new System.Drawing.Point(17, 25);
            this.lbMbrName.Name = "lbMbrName";
            this.lbMbrName.Size = new System.Drawing.Size(29, 12);
            this.lbMbrName.TabIndex = 30;
            this.lbMbrName.Text = "名称";
            // 
            // tsGrpMbr
            // 
            this.tsGrpMbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[ ] {
            this.tsAdd,
            this.tsDelete,
            this.tssMbr1,
            this.tsSave,
            this.tsCancel,
            this.tssMbr2,
            this.tsRule});
            this.tsGrpMbr.Location = new System.Drawing.Point(0, 0);
            this.tsGrpMbr.Name = "tsGrpMbr";
            this.tsGrpMbr.Size = new System.Drawing.Size(292, 25);
            this.tsGrpMbr.TabIndex = 24;
            this.tsGrpMbr.Text = "toolStrip1";
            // 
            // tsAdd
            // 
            this.tsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsAdd.Image")));
            this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAdd.Name = "tsAdd";
            this.tsAdd.Size = new System.Drawing.Size(23, 22);
            this.tsAdd.Text = "新建";
            this.tsAdd.Click += new System.EventHandler(this.tsAdd_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(23, 22);
            this.tsDelete.Text = "删除";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // tssMbr1
            // 
            this.tssMbr1.Name = "tssMbr1";
            this.tssMbr1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(23, 22);
            this.tsSave.Text = "保存";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsCancel
            // 
            this.tsCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsCancel.Image")));
            this.tsCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCancel.Name = "tsCancel";
            this.tsCancel.Size = new System.Drawing.Size(23, 22);
            this.tsCancel.Text = "取消";
            this.tsCancel.Click += new System.EventHandler(this.tsCancel_Click);
            // 
            // tssMbr2
            // 
            this.tssMbr2.Name = "tssMbr2";
            this.tssMbr2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsRule
            // 
            this.tsRule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRule.Image = ((System.Drawing.Image)(resources.GetObject("tsRule.Image")));
            this.tsRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRule.Name = "tsRule";
            this.tsRule.Size = new System.Drawing.Size(23, 22);
            this.tsRule.Text = "规则";
            this.tsRule.Click += new System.EventHandler(this.tsRule_Click);
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[ ] {
            this.tsbEntityGroupSwitch});
            this.tsMain.Location = new System.Drawing.Point(2, 2);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(628, 25);
            this.tsMain.TabIndex = 3;
            this.tsMain.Text = "tsMain";
            // 
            // tsbEntityGroupSwitch
            // 
            this.tsbEntityGroupSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEntityGroupSwitch.Image = ((System.Drawing.Image)(resources.GetObject("tsbEntityGroupSwitch.Image")));
            this.tsbEntityGroupSwitch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEntityGroupSwitch.Name = "tsbEntityGroupSwitch";
            this.tsbEntityGroupSwitch.Size = new System.Drawing.Size(87, 22);
            this.tsbEntityGroupSwitch.Text = "切换到组/实体";
            this.tsbEntityGroupSwitch.Click += new System.EventHandler(this.tsbEntityGroupSwitch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "包含";
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(55, 20);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(165, 21);
            this.tbFilter.TabIndex = 5;
            this.tbFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFilter_KeyDown);
            // 
            // GroupMemberFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 373);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tsMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 400);
            this.Name = "GroupMemberFm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GroupMemberFm";
            this.Load += new System.EventHandler(this.GroupMemberFm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrd)).EndInit();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.gbGroupEntity.ResumeLayout(false);
            this.gbGroupEntity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AttachGrd)).EndInit();
            this.tsAttach.ResumeLayout(false);
            this.tsAttach.PerformLayout();
            this.panelGroupMember.ResumeLayout(false);
            this.panelGroupMember.PerformLayout();
            this.panelEdit.ResumeLayout(false);
            this.panelEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGrp)).EndInit();
            this.tsGrpMbr.ResumeLayout(false);
            this.tsGrpMbr.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbEntityGroupSwitch;
        private System.Windows.Forms.GroupBox gbGroupEntity;
        private System.Windows.Forms.Panel panelGroupMember;
        protected System.Windows.Forms.DataGridView MainGrd;
        protected System.Windows.Forms.DataGridView AttachGrd;
        protected System.Windows.Forms.ToolStrip tsGrpMbr;
        private System.Windows.Forms.ToolStripButton tsAdd;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator tssMbr1;
        protected System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsCancel;
        private System.Windows.Forms.ToolStripSeparator tssMbr2;
        private System.Windows.Forms.ToolStripButton tsRule;
        protected System.Windows.Forms.Panel panelEdit;
        private System.Windows.Forms.Label lbColor;
        private System.Windows.Forms.Button btContinueAdd;
        protected System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbMbrName;
        private System.Windows.Forms.PictureBox picGrp;
        protected System.Windows.Forms.ToolStrip tsAttach;
        private System.Windows.Forms.ToolStripButton tsAttachAdd;
        private System.Windows.Forms.ToolStripButton tsAttachRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsAttachRule;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFilter;
    }
}