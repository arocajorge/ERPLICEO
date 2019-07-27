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
    public partial class CXP_005_cancelaciones_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CXP_005_cancelaciones_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_005_cancelaciones_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa_conciliacion = string.IsNullOrEmpty(p_IdEmpresa_conciliacion.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa_conciliacion.Value);
            decimal IdConciliacion = string.IsNullOrEmpty(p_IdConciliacion.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdConciliacion.Value);

            CXP_005_cancelaciones_Bus bus_rpt = new CXP_005_cancelaciones_Bus();
            List<CXP_005_cancelaciones_Info> lst_rpt = bus_rpt.get_list(IdEmpresa_conciliacion, IdConciliacion);
            this.DataSource = lst_rpt;
        }
    }
}
