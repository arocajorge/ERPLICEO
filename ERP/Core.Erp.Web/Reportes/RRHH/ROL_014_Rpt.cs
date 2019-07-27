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
    public partial class ROL_014_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_014_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_014_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdTipoNomina = p_IdTipoNomina.Value == null ? 0 : Convert.ToInt32(p_IdTipoNomina.Value);
            int IdArea = p_IdArea.Value == null ? 0 : Convert.ToInt32(p_IdArea.Value);
            int IdDivision = p_IdDivision.Value == null ? 0 : Convert.ToInt32(p_IdDivision.Value);

            ROL_014_Bus bus_rpt = new ROL_014_Bus();
            List<ROL_014_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdTipoNomina, IdArea, IdDivision);
            this.DataSource = lst_rpt;
        }
    }
}
