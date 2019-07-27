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
    public partial class BAN_001_cancelaciones_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public BAN_001_cancelaciones_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_001_cancelaciones_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa_pago = p_IdEmpresa_pago.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa_pago.Value);
            int IdTipoCbte_pago = p_IdTipoCbte_pago.Value == null ? 0 : Convert.ToInt32(p_IdTipoCbte_pago.Value);
            decimal IdCbteCble_pago = p_IdCbteCble_pago.Value == null ? 0 : Convert.ToDecimal(p_IdCbteCble_pago.Value);

            BAN_001_cancelaciones_Bus bus_rpt = new BAN_001_cancelaciones_Bus();
            List<BAN_001_cancelaciones_Info> lst_rpt = bus_rpt.get_list(IdEmpresa_pago, IdTipoCbte_pago, IdCbteCble_pago);
            this.DataSource = lst_rpt;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var lst = (List<BAN_001_cancelaciones_Info>)this.DataSource;
            if (lst.Count == 0)
                e.Cancel = true;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var lst = (List<BAN_001_cancelaciones_Info>)this.DataSource;
            if (lst.Count == 0)
                e.Cancel = true;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var lst = (List<BAN_001_cancelaciones_Info>)this.DataSource;
            if (lst.Count == 0)
                e.Cancel = true;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var lst = (List<BAN_001_cancelaciones_Info>)this.DataSource;
            if (lst.Count == 0)
                e.Cancel = true;
        }
    }
}
