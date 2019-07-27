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
    public partial class ROL_004_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_004_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_004_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdUtilidad = p_IdUtilidad.Value == null ? 0 : Convert.ToInt32(p_IdUtilidad.Value);

            ROL_004_Bus bus_rpt = new ROL_004_Bus();
            List<ROL_004_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdUtilidad);
            this.DataSource = lst_rpt;
        }
    }
}
