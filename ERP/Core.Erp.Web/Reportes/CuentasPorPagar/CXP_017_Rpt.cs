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
    public partial class CXP_017_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CXP_017_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_017_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int IdTipoCbte = p_IdTipoCbte.Value == null ? 0 : Convert.ToInt32(p_IdTipoCbte.Value);
                decimal IdCbteVta = p_IdCbteCble.Value == null ? 0 : Convert.ToDecimal(p_IdCbteCble.Value);

                CXP_017_Bus bus_rpt = new CXP_017_Bus();
                List<CXP_017_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdTipoCbte, IdCbteVta);
                this.DataSource = lst_rpt;


                tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
                var empresa = bus_empresa.get_info(IdEmpresa);
                if (empresa != null)
                {
                    lblEmpresa.Text = empresa.em_nombre;
                    lblRuc.Text = empresa.em_ruc;
                    lblContribuyente.Text = empresa.ContribuyenteEspecial;

                }

                if (empresa != null && empresa.em_logo != null)
                {
                    ImageConverter obj = new ImageConverter();
                    lbl_imagen.Image = (Image)obj.ConvertFrom(empresa.em_logo);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
