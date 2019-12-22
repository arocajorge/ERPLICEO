using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_029_OI_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ROL_029_OI_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_029_OI_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAjuste = p_IdAjuste.Value == null ? 0 : Convert.ToInt32(p_IdAjuste.Value);
            int IdEmpleado = p_IdEmpleado.Value == null ? 0 : Convert.ToInt32(p_IdEmpleado.Value);

            ROL_029_Bus bus_rpt = new ROL_029_Bus();
            List<ROL_029_Info> lst_rpt = bus_rpt.get_list_detalle_oi(IdEmpresa, IdAjuste, IdEmpleado);
            this.DataSource = lst_rpt;
        }
    }
}
