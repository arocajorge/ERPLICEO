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
    public partial class CXP_006_diario_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CXP_006_diario_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_006_diario_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdRetencion = p_IdRetencion.Value == null ? 0 : Convert.ToDecimal(p_IdRetencion.Value);

            CXP_006_diario_Bus bus_rpt = new CXP_006_diario_Bus();
            List<CXP_006_diario_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdRetencion);
            this.DataSource = lst_rpt;
        }
    }
}
