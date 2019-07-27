using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Caja;
using Core.Erp.Info.Reportes.Caja;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Caja
{
    public partial class CAJ_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CAJ_002_Rpt()
        {
            InitializeComponent();
        }

        private void CAJ_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdConciliacionCaja = p_IdConciliacionCaja.Value == null ? 0 : Convert.ToDecimal(p_IdConciliacionCaja.Value);

            CAJ_002_Bus bus_rpt = new CAJ_002_Bus();
            List<CAJ_002_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdConciliacionCaja);
            this.DataSource = lst_rpt;
        }

        private void SubReporte_ingresos_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdConciliacionCaja"].Value = p_IdConciliacionCaja.Value == null ? 0 : Convert.ToInt32(p_IdConciliacionCaja.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }

        private void subreport_vales_no_conciliados_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdConciliacion_Caja"].Value = p_IdConciliacionCaja.Value == null ? 0 : Convert.ToInt32(p_IdConciliacionCaja.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
