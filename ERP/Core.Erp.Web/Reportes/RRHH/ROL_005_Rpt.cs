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
    public partial class ROL_005_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_005_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_005_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty( p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdActaFiniquito = string.IsNullOrEmpty(p_IdActaFiniquito.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdActaFiniquito.Value);

            if (IdEmpresa>0)
            {
                ROL_005_Bus bus_rpt = new ROL_005_Bus();
                List<ROL_005_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdActaFiniquito);
                this.DataSource = lst_rpt;
            }
            
        }
    }
}
