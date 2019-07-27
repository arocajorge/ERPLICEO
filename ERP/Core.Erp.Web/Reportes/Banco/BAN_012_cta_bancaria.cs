using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Core.Erp.Web.Reportes.Banco
{
    public partial class BAN_012_cta_bancaria : DevExpress.XtraReports.UI.XtraReport
    {
        public BAN_012_cta_bancaria()
        {
            InitializeComponent();
        }

        private void BAN_012_cta_bancaria_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
