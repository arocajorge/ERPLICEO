using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.RRHH;
using Core.Erp.Bus.Reportes.RRHH;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_027_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_027_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_027_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdNomina_Tipo = string.IsNullOrEmpty(p_IdNomina_Tipo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNomina_Tipo.Value);
            int IdDivision = string.IsNullOrEmpty(p_IdDivision.Value.ToString()) ? 0 : Convert.ToInt32(p_IdDivision.Value);
            int IdArea = string.IsNullOrEmpty(p_IdArea.Value.ToString()) ? 0 : Convert.ToInt32(p_IdArea.Value);
            DateTime fecha_ini = string.IsNullOrEmpty(p_fecha_ini.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_fecha_fin.Value);

            ROL_027_Bus bus_rpt = new ROL_027_Bus();
            List<ROL_027_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdNomina_Tipo,  IdDivision, IdArea, fecha_ini, fecha_fin);
            this.DataSource = lst_rpt;
        }
    }
}
