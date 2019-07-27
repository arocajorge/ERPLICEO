using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.RRHH;
using System.Collections.Generic;
using Core.Erp.Info.Reportes.RRHH;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_019_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_019_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_019_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString())? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdNominaTipoLiqui = string.IsNullOrEmpty(p_IdNominaTipoLiqui.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNominaTipoLiqui.Value);
            DateTime fecha_ini = p_fecha_ini.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = p_fecha_fin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);

            ROL_019_Bus bus_rpt = new ROL_019_Bus();
            List<ROL_019_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdNominaTipoLiqui, fecha_ini, fecha_fin);
            this.DataSource = lst_rpt;

        }
    }
}
