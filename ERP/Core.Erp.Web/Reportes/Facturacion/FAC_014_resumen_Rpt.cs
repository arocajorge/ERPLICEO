using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_014_resumen_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public FAC_014_resumen_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_014_resumen_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
