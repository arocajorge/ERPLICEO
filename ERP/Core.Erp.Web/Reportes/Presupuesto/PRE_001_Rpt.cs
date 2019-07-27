using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Presupuesto;
using Core.Erp.Info.Reportes.Presupuesto;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Presupuesto
{
    public partial class PRE_001_Rpt : DevExpress.XtraReports.UI.XtraReport
    {

        public string usuario { get; set; }
        public string empresa { get; set; }

        public PRE_001_Rpt()
        {
            InitializeComponent();
        }

        private void PRE_001_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdPresupuesto = string.IsNullOrEmpty(p_IdPresupuesto.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdPresupuesto.Value);

            PRE_001_Bus bus_rpt = new PRE_001_Bus();
            List<PRE_001_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdPresupuesto);
            this.DataSource = lst_rpt;
        }
    }
}
