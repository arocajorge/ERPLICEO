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
    public partial class INV_010_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public INV_010_Rpt()
        {
            InitializeComponent();
        }

        private void INV_010_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdProducto = string.IsNullOrEmpty(p_IdProducto.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdProducto.Value);
            string IdCategoria = p_IdCategoria.Value == null ? "" : Convert.ToString(p_IdCategoria.Value);
            int IdLinea = p_IdLinea.Value == null ? 0 : Convert.ToInt32(p_IdLinea.Value);
            int IdGrupo = p_IdGrupo.Value == null ? 0 : Convert.ToInt32(p_IdGrupo.Value);
            int IdSubGrupo = p_IdSubGrupo.Value == null ? 0 : Convert.ToInt32(p_IdSubGrupo.Value);
            int IdMarca = p_IdMarca.Value == null ? 0 : Convert.ToInt32(p_IdMarca.Value);
            string IdUsuario = p_IdUsuario.Value == null ? "" : Convert.ToString(p_IdUsuario.Value);
            DateTime fechaIni = p_fechaIni.Value == null ? DateTime.Now : Convert.ToDateTime(p_fechaIni.Value);
            DateTime fechaFin = p_fechaFin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fechaFin.Value);
            bool mostrarSinMovimiento = p_mostrarSinMovimiento.Value == null ? false : Convert.ToBoolean(p_mostrarSinMovimiento.Value);

            INV_010_Bus bus_rpt = new INV_010_Bus();
            List<INV_010_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdProducto, IdCategoria, IdLinea, IdGrupo, IdSubGrupo, IdMarca, IdUsuario, fechaIni, fechaFin, mostrarSinMovimiento);
            this.DataSource = lst_rpt;
        }
    }
}
