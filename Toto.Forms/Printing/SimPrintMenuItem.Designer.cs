using System.Windows.Forms;

namespace Toto.Forms.Printing
{
    partial class SimPrintMenuItem: ToolStripMenuItem
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
            this.dlg = new System.Windows.Forms.PrintDialog();
            // 
            // dlg
            // 
            this.dlg.AllowCurrentPage = true;
            this.dlg.AllowSelection = true;
            this.dlg.AllowSomePages = true;
            this.dlg.PrintToFile = true;
            this.dlg.ShowHelp = true;
            this.dlg.UseEXDialog = true;
            // 
            // SimPrintCC
            // 
            this.Text = "打印";

        }


        #endregion

        private PrintDialog dlg;
    }
}
