using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toto.Forms.Printing
{
    public partial class SimPrintMenuItem : ToolStripMenuItem
    {
        private bool settingup = false;
        public SimPrintMenuItem(EventHandler handler)
        {
            InitializeComponent();
            dlg.Document = new PrintDocument();
            this.DoubleClick += (sender ,e) =>
            {
                if (settingup) return;
                new Task(() =>
                {
                    var setup = new PageSetupDialog();
                    setup.AllowMargins = true;
                    setup.AllowOrientation = true;
                    setup.AllowPaper = true;
                    setup.AllowPrinter = true;
                    setup.ShowHelp = true;
                    setup.ShowNetwork = true;

                    setup.ShowDialog();
                    settingup = false;
                }).Start();
                settingup = true;
            };
        }

        public PrintDocument Doccument
        {
            get => dlg.Document;
            set => dlg.Document = value;
        }
        public PrinterSettings PrinterSettings
        {
            set => dlg.PrinterSettings = value;
        }

        public Action<PrintPageEventArgs> PrintPageEventHandler
        {
            
            set => Doccument.PrintPage += (sender, e) =>
            {
                value(e); // e.Graphics
            };
        }

        public DialogResult ShowDialog()
        {
            return dlg.ShowDialog();
        }


    }
}
