using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Compra;
using Core.Erp.Info.Reportes.Compra;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Compra
{
    public partial class COMP_001_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public COMP_001_Rpt()
        {
            InitializeComponent();
        }

        private void COMP_001_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            decimal IdOrdenCompra = string.IsNullOrEmpty(p_IdOrdenCompra.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdOrdenCompra.Value);

            COMP_001_Bus bus_rpt = new COMP_001_Bus();
            List<COMP_001_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdOrdenCompra);
            
            this.DataSource = lst_rpt;
        }
    }
}
