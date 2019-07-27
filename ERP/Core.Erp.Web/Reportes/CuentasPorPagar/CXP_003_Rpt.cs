using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System.Collections.Generic;
using Core.Erp.Bus.Reportes.CuentasPorPagar;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.CuentasPorPagar
{
    public partial class CXP_003_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXP_003_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_003_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdTipoCbte = p_IdTipoCbte.Value == null ? 0 : Convert.ToInt32(p_IdTipoCbte.Value);
            decimal IdCbteCble = p_IdCbteCble.Value == null ? 0 : Convert.ToDecimal(p_IdCbteCble.Value);

            CXP_003_Bus bus_rpt = new CXP_003_Bus();
            List<CXP_003_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdTipoCbte, IdCbteCble);
            this.DataSource = lst_rpt;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }
        }

        private void SubReporte_cancelaciones_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa_pago"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdTipoCbte_pago"].Value = p_IdTipoCbte.Value == null ? 0 : Convert.ToInt32(p_IdTipoCbte.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdCbteCble_pago"].Value = p_IdCbteCble.Value == null ? 0 : Convert.ToDecimal(p_IdCbteCble.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
