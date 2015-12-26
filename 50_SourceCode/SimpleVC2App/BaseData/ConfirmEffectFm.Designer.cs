namespace Telossoft.SimpleVC.WinFormApp.BaseData
{
    partial class ConfirmEffectFm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmEffectFm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btCancel = new System.Windows.Forms.Button();
            this.brOK = new System.Windows.Forms.Button();
            this.EffectsGrd = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EffectsGrd)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.brOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 306);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 58);
            this.panel1.TabIndex = 0;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(361, 23);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // brOK
            // 
            this.brOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.brOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.brOK.Location = new System.Drawing.Point(230, 23);
            this.brOK.Name = "brOK";
            this.brOK.Size = new System.Drawing.Size(75, 23);
            this.brOK.TabIndex = 0;
            this.brOK.Text = "确定";
            this.brOK.UseVisualStyleBackColor = true;
            // 
            // EffectsGrd
            // 
            this.EffectsGrd.AllowUserToAddRows = false;
            this.EffectsGrd.AllowUserToDeleteRows = false;
            this.EffectsGrd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EffectsGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EffectsGrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EffectsGrd.Location = new System.Drawing.Point(3, 3);
            this.EffectsGrd.Name = "EffectsGrd";
            this.EffectsGrd.ReadOnly = true;
            this.EffectsGrd.RowTemplate.Height = 23;
            this.EffectsGrd.Size = new System.Drawing.Size(468, 303);
            this.EffectsGrd.TabIndex = 1;
            // 
            // ConfirmEffectFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 367);
            this.Controls.Add(this.EffectsGrd);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfirmEffectFm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "操作后果分析";
            this.Shown += new System.EventHandler(this.ConfirmEffectFm_Shown);
            this.Load += new System.EventHandler(this.ConfirmEffectFm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EffectsGrd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button brOK;
        private System.Windows.Forms.DataGridView EffectsGrd;
    }
}