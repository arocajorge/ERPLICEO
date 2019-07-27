using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;
using Core.Erp.Bus.Reportes.Facturacion;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_010_resumen_forma_pago_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public FAC_010_resumen_forma_pago_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_010_resumen_forma_pago_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
