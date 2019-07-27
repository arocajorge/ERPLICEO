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
    public partial class CXC_008_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXC_008_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_008_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString())? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            decimal IdCliente = string.IsNullOrEmpty(p_IdCliente.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdCliente.Value);
            string IdCobro_tipo = string.IsNullOrEmpty(p_IdCobro_tipo.Value.ToString()) ? "": Convert.ToString(p_IdCobro_tipo.Value);
            DateTime fecha_ini = string.IsNullOrEmpty(p_fecha_ini.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_fecha_fin.Value);
            bool mostrar_anulados = string.IsNullOrEmpty(p_mostrar_anulados.Value.ToString()) ? false : Convert.ToBoolean(p_mostrar_anulados.Value);

            CXC_008_Bus bus_rpt = new CXC_008_Bus();
            List<CXC_008_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdCliente, IdCobro_tipo, fecha_ini, fecha_fin, mostrar_anulados);
            this.DataSource = lst_rpt;
        }
    }
}
