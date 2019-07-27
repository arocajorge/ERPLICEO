using Core.Erp.Bus.Reportes.Inventario;
using System;

namespace Core.Erp.Web.Reportes.Inventario
{
    public partial class INV_013_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        INV_013_Bus bus_rpt = new INV_013_Bus();
        public INV_013_Rpt()
        {
            InitializeComponent();
        }

        private void INV_013_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdProducto = string.IsNullOrEmpty(p_IdProducto.Value.ToString()) ? 0 : Convert.ToInt32(p_IdProducto.Value);
            this.DataSource = bus_rpt.get_list(IdEmpresa, IdProducto);
        }
    }
}
