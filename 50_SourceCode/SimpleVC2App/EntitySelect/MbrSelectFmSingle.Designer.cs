namespace Telossoft.SimpleVC.WinFormApp.EntitySelect
{
    partial class MbrSelectFmSingle
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.btNull = new System.Windows.Forms.Button();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(245, 22);
            // 
            // splitContainer1
            // 
            this.splitContainer1.SplitterDistance = 209;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btNull);
            this.panel2.Controls.SetChildIndex(this.btNull, 0);
            this.panel2.Controls.SetChildIndex(this.btOk, 0);
            // 
            // btNull
            // 
            this.btNull.Location = new System.Drawing.Point(33, 22);
            this.btNull.Name = "btNull";
            this.btNull.Size = new System.Drawing.Size(75, 23);
            this.btNull.TabIndex = 14;
            this.btNull.Text = "null";
            this.btNull.UseVisualStyleBackColor = true;
            this.btNull.Visible = false;
            this.btNull.Click += new System.EventHandler(this.btNull_Click);
            // 
            // MbrSelectFmSingle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(472, 293);
            this.Name = "MbrSelectFmSingle";
            this.Load += new System.EventHandler(this.MbrSelectFmTch_Load);
            this.Shown += new System.EventHandler(this.MbrSelectFmSingle_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MbrSelectFmTch_FormClosing);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btNull;
    }
}
