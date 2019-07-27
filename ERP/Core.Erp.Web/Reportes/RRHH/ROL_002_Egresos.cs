using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.RRHH;
using System.Collections.Generic;
using Core.Erp.Bus.Reportes.RRHH;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_002_Egresos : DevExpress.XtraReports.UI.XtraReport
    {
        List<ROL_002_Info> Lista_detalle_prestamo = new List<ROL_002_Info>();
        public ROL_002_Egresos()
        {
            InitializeComponent();
        }

        private void ROL_002_Egresos_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdNominaTipo = this.p_IdNominaTipo.Value == null ? 0 : Convert.ToInt32(this.p_IdNominaTipo.Value);
            int IdPeriodo = p_IdPeriodo.Value == null ? 0 : Convert.ToInt32(p_IdPeriodo.Value);
            int IdEmpleado = p_IdEmpleado.Value == null ? 0 : Convert.ToInt32(p_IdEmpleado.Value);

            ROL_002_Bus bus_rpt = new ROL_002_Bus();
            Lista_detalle_prestamo = bus_rpt.get_list_detalle_prestamos(IdEmpresa, IdNominaTipo, IdPeriodo, IdEmpleado);
        }

        private void SubRpte_DetallePrestamos_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.DataSource = Lista_detalle_prestamo;
        }
    }
}
