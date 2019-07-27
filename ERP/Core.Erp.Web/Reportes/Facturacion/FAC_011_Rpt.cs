using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_011_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public FAC_011_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_011_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdCliente = string.IsNullOrEmpty(p_IdCliente.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdCliente.Value);
            DateTime fechaIni = p_fechaIni.Value == null ? DateTime.Now : Convert.ToDateTime(p_fechaIni.Value);
            DateTime fechaFin = p_fechaFin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fechaFin.Value);
            bool mostrarAnulados = p_mostrarAnulados.Value == null ? false : Convert.ToBoolean(p_mostrarAnulados.Value);
            bool mostrarObservacion = string.IsNullOrEmpty(p_mostrar_observacion_completa.Value.ToString()) ? false : Convert.ToBoolean(p_mostrar_observacion_completa.Value);

            if (mostrarObservacion)
                CeldaObservacion.WordWrap = true;
            else
                CeldaObservacion.WordWrap = false;

            FAC_011_Bus bus_rpt = new FAC_011_Bus();
            List<FAC_011_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdCliente, fechaIni, fechaFin, mostrarAnulados);
            this.DataSource = lst_rpt;
        }
    }
}
