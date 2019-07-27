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
    public partial class BAN_005_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public BAN_005_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_005_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = string.IsNullOrEmpty((p_IdEmpresa.Value).ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdTipocbte = string.IsNullOrEmpty((p_IdTipocbte.Value).ToString()) ? 0 : Convert.ToInt32(p_IdTipocbte.Value);
            decimal IdCbteCble = string.IsNullOrEmpty((p_IdCbteCble.Value).ToString()) ? 0 : Convert.ToDecimal(p_IdCbteCble.Value);
            int NumDesde = string.IsNullOrEmpty((p_NumDesde.Value).ToString()) ? 0 : Convert.ToInt32(p_NumDesde.Value);
            int NumHasta = string.IsNullOrEmpty((p_NumHasta.Value).ToString()) ? 0 : Convert.ToInt32(p_NumHasta.Value);
            int IdBanco = string.IsNullOrEmpty((p_IdBanco.Value).ToString()) ? 0 : Convert.ToInt32(p_IdBanco.Value);

            BAN_005_Bus bus_rpt = new BAN_005_Bus();
            List<BAN_005_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdTipocbte, IdCbteCble, NumDesde, NumHasta, IdBanco);
            this.DataSource = lst_rpt;
        }
    }
}
