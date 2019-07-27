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
    public partial class FAC_010_detalle_forma_pago_Rpte : DevExpress.XtraReports.UI.XtraReport
    {
        List<FAC_010_Info> Lista = new List<FAC_010_Info>();
        List<FAC_010_Info> Lista_detalle = new List<FAC_010_Info>();


        public string usuario { get; set; }
        public string empresa { get; set; }
        public FAC_010_detalle_forma_pago_Rpte()
        {
            InitializeComponent();
        }

        private void FAC_010_detalle_forma_pago_Rpte_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            DateTime fecha_ini = p_fecha_ini.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fech_fin = p_fecha_fin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);
            string IdCatalogo_FormaPago = Convert.ToString(p_IdCatalogo_FormaPago.Value) == "" ? "" : Convert.ToString(p_IdCatalogo_FormaPago.Value);

            FAC_010_Bus bus_rpt = new FAC_010_Bus();
            List<FAC_010_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, fecha_ini, fech_fin, IdCatalogo_FormaPago);
            this.DataSource = lst_rpt;

        }
    }
}
