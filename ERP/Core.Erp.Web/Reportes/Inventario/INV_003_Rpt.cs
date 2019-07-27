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
    public partial class INV_003_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public INV_003_Rpt()
        {
            InitializeComponent();
        }

        private void INV_003_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString())? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega = string.IsNullOrEmpty(p_IdBodega.Value.ToString()) ? 0 : Convert.ToInt32(p_IdBodega.Value);
            decimal IdProducto = string.IsNullOrEmpty(p_IdProducto.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdProducto.Value);
            string IdCategoria = string.IsNullOrEmpty(p_IdCategoria.Value.ToString()) ? "" : Convert.ToString(p_IdCategoria.Value);
            int IdLinea = string.IsNullOrEmpty(p_IdLinea.Value.ToString()) ? 0 : Convert.ToInt32(p_IdLinea.Value);
            int IdGrupo = string.IsNullOrEmpty(p_IdGrupo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdGrupo.Value);
            int IdSubgrupo = string.IsNullOrEmpty(p_IdSubgrupo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSubgrupo.Value);
            DateTime fecha_corte = p_fecha_corte.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_corte.Value);
            bool mostrar_stock_0 = p_mostrar_stock_0.Value == null ? false : Convert.ToBoolean(p_mostrar_stock_0.Value);
            int IdMarca = string.IsNullOrEmpty(p_IdMarca.Value.ToString()) ? 0 : Convert.ToInt32(p_IdMarca.Value);
            bool AgruparPorID = p_AgruparPorID.Value == null ? false : Convert.ToBoolean(p_AgruparPorID.Value);
            INV_003_Bus bus_rpt = new INV_003_Bus();

            Detail.SortFields.Clear();
            if (!Convert.ToBoolean(p_mostrar_agrupados.Value))
            {                
                Detail.SortFields.Add(new GroupField("IdCategoria", XRColumnSortOrder.None));
                Detail.SortFields.Add(new GroupField("IdLinea", XRColumnSortOrder.None));
                Detail.SortFields.Add(new GroupField("IdGrupo", XRColumnSortOrder.None));
                Detail.SortFields.Add(new GroupField("IdSubgrupo", XRColumnSortOrder.None));                
            }
            else
            {
                Detail.SortFields.Add(new GroupField("IdCategoria", XRColumnSortOrder.Ascending));
                Detail.SortFields.Add(new GroupField("IdLinea", XRColumnSortOrder.Ascending));
                Detail.SortFields.Add(new GroupField("IdGrupo", XRColumnSortOrder.Ascending));
                Detail.SortFields.Add(new GroupField("IdSubgrupo", XRColumnSortOrder.Ascending));                
            }

            if (AgruparPorID)
            {
                Detail.SortFields.Add(new GroupField("IdProducto", XRColumnSortOrder.Ascending));
            }else
                Detail.SortFields.Add(new GroupField("pr_descripcion", XRColumnSortOrder.Ascending));

            List<INV_003_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdBodega, IdProducto, IdCategoria, IdLinea, IdGrupo, IdSubgrupo, fecha_corte, mostrar_stock_0, IdMarca);
            this.DataSource = lst_rpt;
            
        }

        private void GroupHeader7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Convert.ToBoolean(p_mostrar_agrupados.Value))
            {
                e.Cancel = true;
            }
        }

        private void GroupFooter2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!Convert.ToBoolean(p_mostrar_agrupados.Value))
            {
                e.Cancel = true;
            }
        }
    }
}
