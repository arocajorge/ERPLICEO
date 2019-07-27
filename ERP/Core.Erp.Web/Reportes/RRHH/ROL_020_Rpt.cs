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
    public partial class ROL_020_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_020_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_020_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdNominaTipo = string.IsNullOrEmpty(p_IdNominaTipo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNominaTipo.Value);
            int IdNomina = string.IsNullOrEmpty(p_IdNomina.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNomina.Value);
            int IdPeriodo = string.IsNullOrEmpty(p_IdPeriodo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdPeriodo.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdDivision = string.IsNullOrEmpty(P_IdDivision.Value.ToString()) ? 0 : Convert.ToInt32(P_IdDivision.Value);
            int IdArea = string.IsNullOrEmpty(P_IdArea.Value.ToString()) ? 0 : Convert.ToInt32(P_IdArea.Value);
            int IdProceso = string.IsNullOrEmpty(p_IdProceso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdProceso.Value);

            string IdProceso_bancario_tipo = string.IsNullOrEmpty(p_IdProceso_bancario_tipo.Value.ToString()) ? "" : Convert.ToString(p_IdProceso_bancario_tipo.Value);

            ROL_020_Bus bus_rpt = new ROL_020_Bus();
            List<ROL_020_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdNominaTipo, IdNomina, IdPeriodo,   IdDivision,IdArea, IdProceso);
            this.DataSource = lst_rpt;
        }
    }
}
