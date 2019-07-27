using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Inventario
{
    public partial class INV_007_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public INV_007_Rpt()
        {
            InitializeComponent();
        }

        private void INV_007_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursalOrigen = p_IdSucursalOrigen.Value == null ? 0 : Convert.ToInt32(p_IdSucursalOrigen.Value);
            int IdBodegaOrigen = p_IdBodegaOrigen.Value == null ? 0 : Convert.ToInt32(p_IdBodegaOrigen.Value);
            decimal IdTransferencia = p_IdTransferencia.Value == null ? 0 : Convert.ToDecimal(p_IdTransferencia.Value);

            INV_007_Bus bus_rpt = new INV_007_Bus();
            List<INV_007_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursalOrigen, IdBodegaOrigen, IdTransferencia);
            this.DataSource = lst_rpt;
        }
    }
}
