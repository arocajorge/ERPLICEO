using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Inventario
{
    public partial class INV_016_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public INV_016_Rpt()
        {
            InitializeComponent();
        }

        private void INV_016_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdCategoria = string.IsNullOrEmpty(p_IdCategoria.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCategoria.Value);
            int IdLinea = string.IsNullOrEmpty(p_IdLinea.Value.ToString()) ? 0 : Convert.ToInt32(p_IdLinea.Value);
            int IdGrupo = string.IsNullOrEmpty(p_IdGrupo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdGrupo.Value);
            int IdSubGrupo = string.IsNullOrEmpty(p_IdSubGrupo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSubGrupo.Value);
            DateTime fecha_ini = string.IsNullOrEmpty(p_fecha_ini.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);
            bool noMostrarSinVenta = string.IsNullOrEmpty(p_noMostrarSinVenta.Value.ToString()) ? false : Convert.ToBoolean(p_noMostrarSinVenta.Value);
            string IdUsuario = string.IsNullOrEmpty(p_IdUsuario.Value.ToString()) ? "" : Convert.ToString(p_IdUsuario.Value);


            INV_016_Bus bus_rpt = new INV_016_Bus();
            List<INV_016_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdCategoria, IdLinea, IdGrupo, IdSubGrupo, fecha_ini, fecha_fin, noMostrarSinVenta, IdUsuario);
            this.DataSource = lst_rpt;
        }
    }
}
