using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;
using System.Linq;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_003_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        cl_funciones funciones = new cl_funciones();
        public FAC_003_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_003_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            int IdEmpresa = String.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = String.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega = String.IsNullOrEmpty(p_IdBodega.Value.ToString()) ? 0 : Convert.ToInt32(p_IdBodega.Value);
            decimal IdCbteVta = String.IsNullOrEmpty(p_IdCbteVta.Value.ToString())? 0 : Convert.ToDecimal(p_IdCbteVta.Value);
            bool mostrar_cuotas = String.IsNullOrEmpty(p_mostrar_cuotas.Value.ToString()) ? false : Convert.ToBoolean(p_mostrar_cuotas.Value);

            FAC_003_Bus bus_rpt = new FAC_003_Bus();
            List<FAC_003_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, mostrar_cuotas);
            if (lst_rpt.Count > 0)
            {
                string ValorEnLetras = funciones.NumeroALetras(lst_rpt[0].Total.ToString());
                lbl_ValorEnLetras.Text = ValorEnLetras;
                lst_rpt.ForEach(q => q.ValorEnLetras = ValorEnLetras);
            }

            this.DataSource = lst_rpt;
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var empresa = bus_empresa.get_info(IdEmpresa);

            if(empresa != null)
            { 
                lbl_empresa.Text = empresa.em_nombre;
                lbl_direccion.Text = empresa.em_direccion;
                lbl_telefono.Text = empresa.em_telefonos;
                lbl_ruc.Text = empresa.em_ruc;
            }
        }
    }
}
