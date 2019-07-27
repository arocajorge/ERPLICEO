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
    public partial class CXP_001_detalle_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CXP_001_detalle_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_001_detalle_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdTipoCbte_Ogiro = p_IdTipoCbte_Ogiro.Value == null ? 0 : Convert.ToInt32(p_IdTipoCbte_Ogiro.Value);
            decimal IdCbteCble_Ogiro = p_IdCbteCble_Ogiro.Value == null ? 0 : Convert.ToDecimal(p_IdCbteCble_Ogiro.Value);

            CXP_001_detalle_Bus bus_rpt = new CXP_001_detalle_Bus();
            List<CXP_001_detalle_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
            this.DataSource = lst_rpt;
        }
    }
}
