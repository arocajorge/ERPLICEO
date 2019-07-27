using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.CuentasPorCobrar;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXC_002_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega_Cbte = p_IdBodega_Cbte.Value == null ? 0 : Convert.ToInt32(p_IdBodega_Cbte.Value);
            decimal IdCbte_vta_nota = p_IdCbte_vta_nota.Value == null ? 0 : Convert.ToDecimal(p_IdCbte_vta_nota.Value);
            string dc_TipoDocumento = p_dc_TipoDocumento.Value == null ? "" : Convert.ToString(p_dc_TipoDocumento.Value);

            CXC_002_Bus bus_rpt = new CXC_002_Bus();
            List<CXC_002_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento);
            this.DataSource = lst_rpt;
        }

        private void Subreporte_diario_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdSucursal"].Value = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdBodega_Cbte"].Value = p_IdBodega_Cbte.Value == null ? 0 : Convert.ToInt32(p_IdBodega_Cbte.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdCbte_vta_nota"].Value = p_IdCbte_vta_nota.Value == null ? 0 : Convert.ToDecimal(p_IdCbte_vta_nota.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_dc_TipoDocumento"].Value = p_dc_TipoDocumento.Value == null ? "" : Convert.ToString(p_dc_TipoDocumento.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
