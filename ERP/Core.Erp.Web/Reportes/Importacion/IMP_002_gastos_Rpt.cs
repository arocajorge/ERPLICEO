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
    public partial class IMP_002_gastos_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public IMP_002_gastos_Rpt()
        {
            InitializeComponent();
        }

        private void IMP_002_gastos_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_usuario.Text = usuario;
            //int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            //int IdOrdenCompra_ext = p_IdOrdenCompra_ext.Value == null ? 0 : Convert.ToInt32(p_IdOrdenCompra_ext.Value);

            //IMP_002_Bus bus_rpt = new IMP_002_Bus();
            //List<IMP_002_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdOrdenCompra_ext);
          //  this.DataSource = lst_rpt;
        }
    }
}
