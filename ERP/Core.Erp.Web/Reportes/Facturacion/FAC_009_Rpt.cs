using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_009_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public FAC_009_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_009_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega = string.IsNullOrEmpty(p_IdBodega.Value.ToString()) ? 0 : Convert.ToInt32(p_IdBodega.Value);
            decimal IdGuiaRemision = string.IsNullOrEmpty(p_IdGuiaRemision.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdGuiaRemision.Value);

            FAC_009_Bus bus_rpt = new FAC_009_Bus();
            List<FAC_009_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdBodega, IdGuiaRemision);
            this.DataSource = lst_rpt;
        }
    }
}
