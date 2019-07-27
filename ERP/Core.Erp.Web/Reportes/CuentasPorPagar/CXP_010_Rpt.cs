using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.CuentasPorPagar
{
    public partial class CXP_010_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXP_010_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_010_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdProveedor = p_IdProveedor.Value == null ? 0 : Convert.ToDecimal(p_IdProveedor.Value);
            DateTime fechaIni = p_fechaIni.Value == null ? DateTime.Now : Convert.ToDateTime(p_fechaIni.Value);
            DateTime fechaFin = p_fechaFin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fechaFin.Value);
            bool mostrarAnulados = p_mostrarAnulados.Value == null ? false : Convert.ToBoolean(p_mostrarAnulados.Value);
            bool mostrarObservacion = string.IsNullOrEmpty(p_mostrar_observacion_completa.Value.ToString()) ? false : Convert.ToBoolean(p_mostrar_observacion_completa.Value);

            if (mostrarObservacion)
                CeldaObservacion.WordWrap = true;
            else
                CeldaObservacion.WordWrap = false;

            CXP_010_Bus bus_rpt = new CXP_010_Bus();
            List<CXP_010_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdProveedor, fechaIni, fechaFin, mostrarAnulados);
            this.DataSource = lst_rpt;
        }
    }
}
