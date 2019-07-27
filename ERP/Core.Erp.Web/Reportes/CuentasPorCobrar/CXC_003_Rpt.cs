using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using Core.Erp.Bus.Reportes.CuentasPorCobrar;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_003_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXC_003_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_003_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdCliente = p_IdCliente.Value == null ? 0 : Convert.ToDecimal(p_IdCliente.Value);
            DateTime Fecha_ini = p_Fecha_ini.Value == null ? DateTime.Now : Convert.ToDateTime(p_Fecha_ini.Value);
            DateTime Fecha_fin = p_Fecha_fin.Value == null ? DateTime.Now : Convert.ToDateTime(p_Fecha_fin.Value);

            CXC_003_Bus bus_rpt = new CXC_003_Bus();
            List<CXC_003_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdCliente, Fecha_ini, Fecha_fin);
            this.DataSource = lst_rpt;
        }
    }
}
