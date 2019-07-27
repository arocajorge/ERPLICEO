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
    public partial class CXC_002_diario_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CXC_002_diario_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_002_diario_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega_Cbte = p_IdBodega_Cbte.Value == null ? 0 : Convert.ToInt32(p_IdBodega_Cbte.Value);
            decimal IdCbte_vta_nota = p_IdCbte_vta_nota.Value == null ? 0 : Convert.ToDecimal(p_IdCbte_vta_nota.Value);
            string dc_TipoDocumento = p_dc_TipoDocumento.Value == null ? "" : Convert.ToString(p_dc_TipoDocumento.Value);

            CXC_002_diario_Bus bus_rpt = new CXC_002_diario_Bus();
            List<CXC_002_diario_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento);
            this.DataSource = lst_rpt;
        }
    }
}
