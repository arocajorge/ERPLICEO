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
    public partial class CXP_003_cancelaciones_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CXP_003_cancelaciones_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_003_cancelaciones_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa_pago = p_IdEmpresa_pago.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa_pago.Value);
            int IdTipoCbte_pago = p_IdTipoCbte_pago.Value == null ? 0 : Convert.ToInt32(p_IdTipoCbte_pago.Value);
            decimal IdCbteCble_pago = p_IdCbteCble_pago.Value == null ? 0 : Convert.ToDecimal(p_IdCbteCble_pago.Value);

            CXP_003_cancelaciones_Bus bus_rpt = new CXP_003_cancelaciones_Bus();
            List<CXP_003_cancelaciones_Info> lst_rpt = bus_rpt.get_list(IdEmpresa_pago, IdTipoCbte_pago, IdCbteCble_pago);
            this.DataSource = lst_rpt;
        }
    }
}
