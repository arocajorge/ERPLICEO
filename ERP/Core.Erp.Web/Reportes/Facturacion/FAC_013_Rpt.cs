using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_013_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public FAC_013_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_013_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega = p_IdBodega.Value == null ? 0 : Convert.ToInt32(p_IdBodega.Value);
            decimal IdCbteVta = p_IdCbteVta.Value == null ? 0 : Convert.ToDecimal(p_IdCbteVta.Value);

            FAC_013_Bus bus_rpt = new FAC_013_Bus();
            List<FAC_013_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);

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
