using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.CuentasPorPagar
{
    public partial class CXP_006_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXP_006_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_006_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdRetencion = p_IdRetencion.Value == null ? 0 : Convert.ToDecimal(p_IdRetencion.Value);

            CXP_006_Bus bus_rpt = new CXP_006_Bus();
            List<CXP_006_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdRetencion);
            this.DataSource = lst_rpt;
        }
        private void SubReporte_diario_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdRetencion"].Value = p_IdRetencion.Value == null ? 0 : Convert.ToDecimal(p_IdRetencion.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }

    
    }
}
