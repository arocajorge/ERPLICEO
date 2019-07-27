using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Inventario;
using System.Collections.Generic;
using Core.Erp.Info.Reportes.Inventario;

namespace Core.Erp.Web.Reportes.Inventario
{
    public partial class INV_005_detalle_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public INV_005_detalle_Rpt()
        {
            InitializeComponent();
        }

        private void INV_005_detalle_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("d/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;
            lbl_empresa.Text = empresa;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega = p_IdBodega.Value == null ? 0 : Convert.ToInt32(p_IdBodega.Value);
            decimal IdProducto = string.IsNullOrEmpty(p_IdProducto.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdProducto.Value);
            string IdUsuario = p_IdUsuario.Value == null ? "" : Convert.ToString(p_IdUsuario.Value);
            DateTime fecha_ini = p_fecha_ini.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = p_fecha_fin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);
            bool no_mostrar_valores_en_0 = p_no_mostrar_valores_en_0.Value == null ? false : Convert.ToBoolean(p_no_mostrar_valores_en_0.Value);
            bool mostrar_detallado = p_mostrar_detallado.Value == null ? false : Convert.ToBoolean(p_mostrar_detallado.Value);
            decimal IdProductoPadre = string.IsNullOrEmpty(P_IdProductoPadre.Value.ToString()) ? 0 : Convert.ToDecimal(P_IdProductoPadre.Value);

            INV_005_Bus bus_rpt = new INV_005_Bus();
            List<INV_005_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdBodega, IdProducto, fecha_ini, fecha_fin, IdUsuario, no_mostrar_valores_en_0, mostrar_detallado,IdProductoPadre);
            this.DataSource = lst_rpt;
        }
    }
}
