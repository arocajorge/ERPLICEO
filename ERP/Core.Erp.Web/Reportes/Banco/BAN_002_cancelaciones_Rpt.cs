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
    public partial class BAN_002_cancelaciones_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public BAN_002_cancelaciones_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_002_cancelaciones_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int mba_IdEmpresa = p_mba_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_mba_IdEmpresa.Value);
            int mba_IdTipocbte = p_mba_IdTipocbte.Value == null ? 0 : Convert.ToInt32(p_mba_IdTipocbte.Value);
            decimal mba_IdCbteCble = p_mba_IdCbteCble.Value == null ? 0 : Convert.ToDecimal(p_mba_IdCbteCble.Value);

            BAN_002_cancelaciones_Bus bus_rpt = new BAN_002_cancelaciones_Bus();
            List<BAN_002_cancelaciones_Info> lst_rpt = bus_rpt.get_list(mba_IdEmpresa, mba_IdTipocbte, mba_IdCbteCble);
            this.DataSource = lst_rpt;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var lst = (List<BAN_002_cancelaciones_Info>)this.DataSource;
            if (lst.Count == 0)
                e.Cancel = true;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var lst = (List<BAN_002_cancelaciones_Info>)this.DataSource;
            if (lst.Count == 0)
                e.Cancel = true;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var lst = (List<BAN_002_cancelaciones_Info>)this.DataSource;
            if (lst.Count == 0)
                e.Cancel = true;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var lst = (List<BAN_002_cancelaciones_Info>)this.DataSource;
            if(lst.Count == 0)
                e.Cancel = true;            
        }
    }
}
