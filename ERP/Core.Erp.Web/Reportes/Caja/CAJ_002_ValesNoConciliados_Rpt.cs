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
    public partial class CAJ_002_ValesNoConciliados_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CAJ_002_ValesNoConciliados_Rpt()
        {
            InitializeComponent();
        }

        private void CAJ_002_ValesNoConciliados_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdConciliacionCaja = p_IdConciliacion_Caja.Value == null ? 0 : Convert.ToDecimal(p_IdConciliacion_Caja.Value);

            CAJ_002_ValesNoConciliados_Bus bus_rpt = new CAJ_002_ValesNoConciliados_Bus();
            List<CAJ_002_ValesNoConciliados_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdConciliacionCaja);
            this.DataSource = lst_rpt;
        }
    }
}
