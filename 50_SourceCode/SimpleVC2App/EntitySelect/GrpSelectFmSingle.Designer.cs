namespace Telossoft.SimpleVC.WinFormApp.EntitySelect
{
    partial class GrpSelectFmSingle
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
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 249);
            this.panel2.Size = new System.Drawing.Size(386, 64);
            // 
            // GrpSelectFmSingle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(386, 313);
            this.Name = "GrpSelectFmSingle";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GrpSelectFmSingle_FormClosed);
            this.Load += new System.EventHandler(this.GrpSelectFmSingle_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
