namespace Toto.WinForms
{
    partial class RemoteServerWorker
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
            if (disposing && ( components != null ))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bgworker = new System.ComponentModel.BackgroundWorker();
            this.bgworkertcp = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // bgworker
            // 
            this.bgworker.WorkerReportsProgress = true;
            this.bgworker.WorkerSupportsCancellation = true;
            this.bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworker_DoWork);
            this.bgworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgworker_ProgressChanged);
            // 
            // bgworkertcp
            // 
            this.bgworkertcp.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworkertcp_DoWork);
            this.bgworkertcp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgworkertcp_ProgressChanged);
            // 
            // RemoteServerWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "RemoteServerWorker";
            this.Size = new System.Drawing.Size(0, 0);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.ComponentModel.BackgroundWorker bgworker;
        private System.ComponentModel.BackgroundWorker bgworkertcp;
    }
}
