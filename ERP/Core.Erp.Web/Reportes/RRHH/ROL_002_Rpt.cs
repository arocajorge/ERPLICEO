using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.RRHH;
using Core.Erp.Bus.Reportes.RRHH;
using System.Collections.Generic;
using Core.Erp.Bus.General;
using System.Linq;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        List<ROL_002_Info> Lista_ingreso = new List<ROL_002_Info>();
        List<ROL_002_Info> Lista_egreso = new List<ROL_002_Info>();
        List<ROL_002_Info> Lista_Rpte = new List<ROL_002_Info>();

        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_002_Rpt()
        {
            InitializeComponent();
        }
        

        private void ROL_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdNomina = p_IdNomina.Value == null ? 0 : Convert.ToInt32(p_IdNomina.Value);
            int IdNominaTipo = p_IdNominaTipo.Value == null ? 0 : Convert.ToInt32(p_IdNominaTipo.Value);
            int IdPeriodo = p_IdPeriodo.Value == null ? 0 : Convert.ToInt32(p_IdPeriodo.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            //lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            ROL_002_Bus bus_rpt = new ROL_002_Bus();
            List<ROL_002_Info> lst_rpt = bus_rpt.get_list_empleados(IdEmpresa, IdNomina, IdNominaTipo, IdPeriodo, IdSucursal);
            this.DataSource = lst_rpt;
        }

        private void SubReporte_RolPago_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdSucursal"].Value = p_IdSucursal.Value == null ? 0 : Convert.ToDecimal(p_IdSucursal.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdNomina"].Value = p_IdNomina.Value == null ? 0 : Convert.ToDecimal(p_IdNomina.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdNominaTipo"].Value = p_IdNominaTipo.Value == null ? 0 : Convert.ToDecimal(p_IdNominaTipo.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdPeriodo"].Value = p_IdPeriodo.Value == null ? 0 : Convert.ToDecimal(p_IdPeriodo.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpleado"].Value = xrLabel1.Value == null ? 0 : Convert.ToDecimal(xrLabel1.Text);

            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }

 }
}
