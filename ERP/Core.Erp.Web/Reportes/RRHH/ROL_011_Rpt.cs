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
    public partial class ROL_011_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_011_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_011_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdHorasExtras = p_IdHorasExtras.Value == null ? 0 : Convert.ToInt32(p_IdHorasExtras.Value);

            ROL_011_Bus bus_rpt = new ROL_011_Bus();
            List<ROL_011_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdHorasExtras);
            this.DataSource = lst_rpt;

        }
    }
}
