using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;
using Core.Erp.Info.General;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_006_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public FAC_006_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_006_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tb_empresa_Info info_empresa = new tb_empresa_Info();
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            decimal IdProforma = p_IdProforma.Value == null ? 0 : Convert.ToDecimal(p_IdProforma.Value);

            FAC_006_Bus bus_rpt = new FAC_006_Bus();
            List<FAC_006_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdProforma, false, false);
            
            this.DataSource = lst_rpt;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var empresa = bus_empresa.get_info(IdEmpresa);
            lbl_empresa.Text = empresa.em_nombre;
            if (empresa != null && empresa.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                logo.Image = (Image)obj.ConvertFrom(empresa.em_logo);
            }
        }
    }
}
