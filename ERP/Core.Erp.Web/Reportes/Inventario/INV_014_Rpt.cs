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
    public partial class INV_014_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }

        public INV_014_Rpt()
        {
            InitializeComponent();
        }

        private void INV_014_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdConsignacion = string.IsNullOrEmpty(p_IdConsignacion.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdConsignacion.Value);

            INV_014_Bus bus_rpt = new INV_014_Bus();
            List<INV_014_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdConsignacion);
            this.DataSource = lst_rpt;

        }
    }
}
