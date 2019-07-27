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
    public partial class FAC_008_aplicaciones_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public FAC_008_aplicaciones_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_008_aplicaciones_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa_nt = p_IdEmpresa_nt.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa_nt.Value);
            int IdSucursal_nt = p_IdSucursal_nt.Value == null ? 0 : Convert.ToInt32(p_IdSucursal_nt.Value);
            int IdBodega_nt = p_IdBodega_nt.Value == null ? 0 : Convert.ToInt32(p_IdBodega_nt.Value);
            decimal IdNota_nt = p_IdNota_nt.Value == null ? 0 : Convert.ToDecimal(p_IdNota_nt.Value);

            FAC_008_aplicaciones_Bus bus_rpt = new FAC_008_aplicaciones_Bus();
            List<FAC_008_aplicaciones_Info> lst_rpt = bus_rpt.get_list(IdEmpresa_nt, IdSucursal_nt, IdBodega_nt, IdNota_nt);
            this.DataSource = lst_rpt;
        }
    }
}
