using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Produccion;
using Core.Erp.Info.Reportes.Produccion;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Produccion
{
    public partial class PRO_001_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public PRO_001_Rpt()
        {
            InitializeComponent();
        }

        private void PRO_001_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdFabricacion = string.IsNullOrEmpty(p_IdFabricacion.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdFabricacion.Value);

            PRO_001_Bus bus_rpt = new PRO_001_Bus();
            List<PRO_001_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdFabricacion);
            this.DataSource = lst_rpt;

        }
    }
}
