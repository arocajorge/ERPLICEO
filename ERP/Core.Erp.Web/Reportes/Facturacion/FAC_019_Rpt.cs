using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_019_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }
        public FAC_019_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_019_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdCliente = string.IsNullOrEmpty(p_IdCliente.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdCliente.Value);
            int IdVendedor = string.IsNullOrEmpty(p_IdVendedor.Value.ToString()) ? 0 : Convert.ToInt32(p_IdVendedor.Value);
            DateTime fechaCorte = string.IsNullOrEmpty(p_fechaCorte.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fechaCorte.Value);
            bool mostrarSoloVencido = p_mostrarSoloVencido.Value == null ? false : Convert.ToBoolean(p_mostrarSoloVencido.Value);
            bool mostrarSaldo0 = p_mostrarSaldo0.Value == null ? false : Convert.ToBoolean(p_mostrarSaldo0.Value);
            string IdUsuario = string.IsNullOrEmpty(p_IdUsuario.Value.ToString()) ? "" : Convert.ToString(p_IdUsuario.Value);

            FAC_019_Bus bus_rpt = new FAC_019_Bus();
            List<FAC_019_Info> lst_rpt = bus_rpt.GetList(IdEmpresa,IdCliente, IdVendedor, fechaCorte,mostrarSoloVencido, mostrarSaldo0, IdUsuario);
            this.DataSource = lst_rpt;
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }
        }
    }
}
