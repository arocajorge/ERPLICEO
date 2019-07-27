using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Banco
{
    public partial class BAN_003_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public BAN_003_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_003_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdTipocbte = p_IdTipocbte.Value == null ? 0 : Convert.ToInt32(p_IdTipocbte.Value);
            decimal IdCbteCble = p_IdCbteCble.Value == null ? 0 : Convert.ToDecimal(p_IdCbteCble.Value);

            BAN_003_Bus bus_rpt = new BAN_003_Bus();
            List<BAN_003_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdTipocbte, IdCbteCble);
            this.DataSource = lst_rpt;
        }

        private void SubReporte_cancelaciones_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa_pago"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdTipoCbte_pago"].Value = p_IdTipocbte.Value == null ? 0 : Convert.ToInt32(p_IdTipocbte.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdCbteCble_pago"].Value = p_IdCbteCble.Value == null ? 0 : Convert.ToDecimal(p_IdCbteCble.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
