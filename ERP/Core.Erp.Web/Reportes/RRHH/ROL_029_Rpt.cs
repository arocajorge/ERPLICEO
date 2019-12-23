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
    public partial class ROL_029_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }

        public ROL_029_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_029_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAjuste = p_IdAjuste.Value == null ? 0 : Convert.ToInt32(p_IdAjuste.Value);

            ROL_029_Bus bus_rpt = new ROL_029_Bus();
            List<ROL_029_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdAjuste);
            this.DataSource = lst_rpt;

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdAjuste"].Value = p_IdAjuste.Value == null ? 0 : Convert.ToDecimal(p_IdAjuste.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpleado"].Value = IdEmpleado.Value == null ? 0 : Convert.ToDecimal(IdEmpleado.Text);

            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }

        private void xrLabel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


        }
    }
}
