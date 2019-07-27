using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Banco
{
    public partial class BAN_011_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public BAN_011_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_011_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            string IdUsuario = string.IsNullOrEmpty(p_IdUsuario.Value.ToString()) ? "" : Convert.ToString(p_IdUsuario.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            DateTime fechaIni = string.IsNullOrEmpty(p_fechaIni.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fechaIni.Value);
            DateTime fechaFin = string.IsNullOrEmpty(p_fechaFin.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fechaFin.Value);

            BAN_011_Bus bus_rpt = new BAN_011_Bus();
            List<BAN_011_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdUsuario, IdSucursal, fechaIni, fechaFin);
            this.DataSource = lst_rpt;
        }
    }
}
