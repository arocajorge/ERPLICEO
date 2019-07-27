using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System.Collections.Generic;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.CuentasPorPagar
{
    public partial class CXP_011_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CXP_011_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_011_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdSolicitud = string.IsNullOrEmpty(p_IdSolicitud.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdSolicitud.Value);

            CXP_011_Bus bus_rpt = new CXP_011_Bus();
            List<CXP_011_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSolicitud);
            this.DataSource = lst_rpt;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var empresa = bus_empresa.get_info(IdEmpresa);
            lbl_empresa.Text = empresa.em_nombre;
            if (empresa != null && empresa.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(empresa.em_logo);
                lbl_imag.Image = (Image)obj.ConvertFrom(empresa.em_logo);
            }
            lbl_emp.Text = empresa.em_nombre;

        }
    }
}
