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
    public partial class FAC_004_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public FAC_004_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_004_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblFechaImpresion.Text = DateTime.Now.ToString("d/MM/yyyy hh:mm:ss");

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega = p_IdBodega.Value == null ? 0 : Convert.ToInt32(p_IdBodega.Value);
            decimal IdNota = p_IdNota.Value == null ? 0 : Convert.ToDecimal(p_IdNota.Value);

            FAC_004_Bus bus_rpt = new FAC_004_Bus();
            List<FAC_004_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            this.DataSource = lst_rpt;
        }
    }
}
