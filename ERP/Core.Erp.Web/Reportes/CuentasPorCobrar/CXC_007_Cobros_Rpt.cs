using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.CuentasPorCobrar;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_007_Cobros_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CXC_007_Cobros_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_007_Cobros_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            decimal IdLiquidacion = string.IsNullOrEmpty(p_IdLiquidacion.ToString()) ? 0 : Convert.ToDecimal(p_IdLiquidacion.Value);

            CXC_007_Cobros_Bus bus_rpt = new CXC_007_Cobros_Bus();
            List<CXC_007_Cobros_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdLiquidacion);
            this.DataSource = lst_rpt;
        }
    }
}
