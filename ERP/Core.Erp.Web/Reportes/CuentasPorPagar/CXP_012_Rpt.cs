using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.CuentasPorPagar
{
    public partial class CXP_012_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CXP_012_Rpt()
        {
            InitializeComponent();
        }
        List<CXP_012_Info> lst_rpt = new List<CXP_012_Info>();
       

        private void CXP_012_Rpt_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {

                int IdEmpresa = p_IdEmpresa == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                decimal IdRetencion = p_IdRetencion.Value == null ? 0 : Convert.ToDecimal(p_IdRetencion.Value);
                

                CXP_012_Bus bus_rpt = new CXP_012_Bus();
                lst_rpt = bus_rpt.get_list(IdEmpresa, IdRetencion);
                this.DataSource = lst_rpt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SubReporte_RIDE1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdRetencion"].Value = p_IdRetencion.Value == null ? 0 : Convert.ToDecimal(p_IdRetencion.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }

        private void SubReporte_RIDE2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdRetencion"].Value = p_IdRetencion.Value == null ? 0 : Convert.ToDecimal(p_IdRetencion.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;

        }

        private void SubReporte_RIDE3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdRetencion"].Value = p_IdRetencion.Value == null ? 0 : Convert.ToDecimal(p_IdRetencion.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;

        }
    }
}
