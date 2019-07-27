using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.Importacion;
using Core.Erp.Bus.Reportes.Importacion;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Importacion
{
    public partial class IMP_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        List<IMP_002_gastos_Info> lst_resumen = new List<IMP_002_gastos_Info>();

        public IMP_002_Rpt()
        {
            InitializeComponent();
        }

        private void IMP_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdOrdenCompra_ext = p_IdOrdenCompra_ext.Value == null ? 0 : Convert.ToInt32(p_IdOrdenCompra_ext.Value);

            IMP_002_Bus bus_rpt = new IMP_002_Bus();
            List<IMP_002_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdOrdenCompra_ext, ref lst_resumen);
            this.DataSource = lst_rpt;
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource = new Core.Erp.Web.Reportes.Importacion.IMP_002_gastos_Rpt(); 
            ((XRSubreport)sender).ReportSource.DataSource = lst_resumen;
           // ((XRSubreport)sender).ReportSource.FillDataSource();

        }
    }
}
