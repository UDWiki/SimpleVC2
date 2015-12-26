namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{
    partial class SqdScheduleFm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqdScheduleFm));
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.InfoSplit = new System.Windows.Forms.SplitContainer();
            this.TaskGrid = new System.Windows.Forms.DataGridView();
            this.TaskGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMenuScheduleFail = new System.Windows.Forms.ToolStripMenuItem();
            this.RuleGrid = new System.Windows.Forms.DataGridView();
            this.ClmEntity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmRelationDsp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CloseTs = new System.Windows.Forms.ToolStripButton();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.InfoSplit.Panel1.SuspendLayout();
            this.InfoSplit.Panel2.SuspendLayout();
            this.InfoSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TaskGrid)).BeginInit();
            this.TaskGridMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RuleGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplit
            // 
            this.MainSplit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplit.Location = new System.Drawing.Point(0, 31);
            this.MainSplit.Name = "MainSplit";
            this.MainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainSplit.Panel1
            // 
            this.MainSplit.Panel1.Controls.Add(this.InfoSplit);
            // 
            // MainSplit.Panel2
            // 
            this.MainSplit.Panel2.AllowDrop = true;
            this.MainSplit.Size = new System.Drawing.Size(472, 322);
            this.MainSplit.SplitterDistance = 108;
            this.MainSplit.TabIndex = 0;
            // 
            // InfoSplit
            // 
            this.InfoSplit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InfoSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoSplit.Location = new System.Drawing.Point(0, 0);
            this.InfoSplit.Name = "InfoSplit";
            // 
            // InfoSplit.Panel1
            // 
            this.InfoSplit.Panel1.Controls.Add(this.TaskGrid);
            // 
            // InfoSplit.Panel2
            // 
            this.InfoSplit.Panel2.Controls.Add(this.RuleGrid);
            this.InfoSplit.Size = new System.Drawing.Size(472, 108);
            this.InfoSplit.SplitterDistance = 209;
            this.InfoSplit.TabIndex = 0;
            // 
            // TaskGrid
            // 
            this.TaskGrid.AllowDrop = true;
            this.TaskGrid.AllowUserToAddRows = false;
            this.TaskGrid.AllowUserToDeleteRows = false;
            this.TaskGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TaskGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TaskGrid.ContextMenuStrip = this.TaskGridMenu;
            this.TaskGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TaskGrid.Location = new System.Drawing.Point(0, 0);
            this.TaskGrid.MultiSelect = false;
            this.TaskGrid.Name = "TaskGrid";
            this.TaskGrid.ReadOnly = true;
            this.TaskGrid.RowTemplate.Height = 23;
            this.TaskGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TaskGrid.Size = new System.Drawing.Size(205, 104);
            this.TaskGrid.TabIndex = 0;
            // 
            // TaskGridMenu
            // 
            this.TaskGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenuScheduleFail});
            this.TaskGridMenu.Name = "TaskGridMenu";
            this.TaskGridMenu.Size = new System.Drawing.Size(123, 26);
            this.TaskGridMenu.Opening += new System.ComponentModel.CancelEventHandler(this.TaskGridMenu_Opening);
            // 
            // CMenuScheduleFail
            // 
            this.CMenuScheduleFail.Name = "CMenuScheduleFail";
            this.CMenuScheduleFail.Size = new System.Drawing.Size(122, 22);
            this.CMenuScheduleFail.Text = "自动安排";
            this.CMenuScheduleFail.Click += new System.EventHandler(this.CMenuScheduleFail_Click);
            // 
            // RuleGrid
            // 
            this.RuleGrid.AllowUserToAddRows = false;
            this.RuleGrid.AllowUserToDeleteRows = false;
            this.RuleGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RuleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RuleGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClmEntity,
            this.ClmRelationDsp});
            this.RuleGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RuleGrid.Location = new System.Drawing.Point(0, 0);
            this.RuleGrid.Name = "RuleGrid";
            this.RuleGrid.ReadOnly = true;
            this.RuleGrid.RowTemplate.Height = 23;
            this.RuleGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.RuleGrid.Size = new System.Drawing.Size(255, 104);
            this.RuleGrid.TabIndex = 0;
            this.RuleGrid.DataSourceChanged += new System.EventHandler(this.RuleGrid_DataSourceChanged);
            this.RuleGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.RuleGrid_CellPainting);
            this.RuleGrid.DoubleClick += new System.EventHandler(this.RuleGrid_DoubleClick);
            // 
            // ClmEntity
            // 
            this.ClmEntity.DataPropertyName = "Entity";
            this.ClmEntity.FillWeight = 150F;
            this.ClmEntity.HeaderText = "名称";
            this.ClmEntity.Name = "ClmEntity";
            this.ClmEntity.ReadOnly = true;
            // 
            // ClmRelationDsp
            // 
            this.ClmRelationDsp.DataPropertyName = "RelationDsp";
            this.ClmRelationDsp.HeaderText = "关系";
            this.ClmRelationDsp.Name = "ClmRelationDsp";
            this.ClmRelationDsp.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseTs});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(472, 31);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CloseTs
            // 
            this.CloseTs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CloseTs.Image = ((System.Drawing.Image)(resources.GetObject("CloseTs.Image")));
            this.CloseTs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseTs.Name = "CloseTs";
            this.CloseTs.Size = new System.Drawing.Size(28, 28);
            this.CloseTs.Text = "其它全部关闭";
            this.CloseTs.Click += new System.EventHandler(this.CloseTs_Click);
            // 
            // SqdScheduleFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 353);
            this.Controls.Add(this.MainSplit);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(480, 380);
            this.Name = "SqdScheduleFm";
            this.Text = "课表";
            this.Shown += new System.EventHandler(this.SqdScheduleFm_Shown);
            this.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.thisQueryContinueDrag);
            this.MainSplit.Panel1.ResumeLayout(false);
            this.MainSplit.ResumeLayout(false);
            this.InfoSplit.Panel1.ResumeLayout(false);
            this.InfoSplit.Panel2.ResumeLayout(false);
            this.InfoSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TaskGrid)).EndInit();
            this.TaskGridMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RuleGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.SplitContainer InfoSplit;
        private System.Windows.Forms.DataGridView TaskGrid;
        private System.Windows.Forms.DataGridView RuleGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmEntity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmRelationDsp;
        private System.Windows.Forms.ContextMenuStrip TaskGridMenu;
        private System.Windows.Forms.ToolStripMenuItem CMenuScheduleFail;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton CloseTs;
    }
}