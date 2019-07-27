using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System.Collections.Generic;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_026_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ROL_026_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_026_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
                int IdNomina_Tipo = p_IdNomina_Tipo.Value == null ? 0 : Convert.ToInt32(p_IdNomina_Tipo.Value);
                int IdAnio = p_IdAnio.Value == null ? 0 : Convert.ToInt32(p_IdAnio.Value);
                decimal IdEmpleado = p_IdEmpleado.Value == null ? 0 : Convert.ToDecimal(p_IdEmpleado.Value);

                ROL_026_Bus bus_rpt = new ROL_026_Bus();
                List<ROL_026_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdNomina_Tipo, IdAnio, IdEmpleado);

                (sender as XtraReport).PrintingSystem.Document.AutoFitToPagesWidth = 1;
                this.DataSource = lst_rpt;

                tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
                var emp = bus_empresa.get_info(IdEmpresa);
                lbl_ruc.Text = emp.em_ruc;
                lbl_razon_social.Text = emp.RazonSocial;
                lbl_ruc_contador.Text = emp.em_rucContador;
                lbl_ruc_contador1.Text = emp.em_rucContador;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
