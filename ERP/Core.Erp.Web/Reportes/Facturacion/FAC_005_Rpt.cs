using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Core.Erp.Info.Reportes.Facturacion;
using Core.Erp.Bus.Reportes.Facturacion;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_005_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        List<FAC_005_resumen_Info> lst_resumen = new List<FAC_005_resumen_Info>();

        public FAC_005_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_005_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;
            lbl_empresa.Text = empresa;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            decimal IdCliente = string.IsNullOrEmpty(p_IdCliente.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdCliente.Value);
            DateTime Fecha_ini = string.IsNullOrEmpty(p_Fecha_ini.Value.ToString()) ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(p_Fecha_ini.Value).Date;
            DateTime Fecha_fin = string.IsNullOrEmpty(p_Fecha_fin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_Fecha_fin.Value).Date;           
            FAC_005_Bus bus_rpt = new FAC_005_Bus();
            List<FAC_005_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal,IdCliente, Fecha_ini, Fecha_fin, ref lst_resumen);
            this.DataSource = lst_rpt;
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.DataSource = lst_resumen;
            ((XRSubreport)sender).ReportSource.FillDataSource();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
