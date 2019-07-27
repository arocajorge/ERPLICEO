using Core.Erp.Bus.Reportes.CuentasPorCobrar;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_001_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXC_001_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_001_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString())? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            decimal IdCobro = string.IsNullOrEmpty(p_IdCobro.ToString()) ? 0 : Convert.ToDecimal(p_IdCobro.Value);

            CXC_001_Bus bus_rpt = new CXC_001_Bus();
            List<CXC_001_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdCobro);
            this.DataSource = lst_rpt;
        }

        private void Subreporte_diario_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_cbr_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_cbr_IdSucursal"].Value = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_cbr_IdCobro"].Value = p_IdCobro.Value == null ? 0 : Convert.ToDecimal(p_IdCobro.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
