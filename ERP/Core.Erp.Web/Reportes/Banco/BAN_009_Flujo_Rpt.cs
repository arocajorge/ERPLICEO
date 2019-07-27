using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.Banco;
using System.Collections.Generic;
using Core.Erp.Bus.Reportes.Banco;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Banco;

namespace Core.Erp.Web.Reportes.Banco
{
    public partial class BAN_009_Flujo_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }
        public BAN_009_Flujo_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_009_Flujo_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;
            lbl_empresa.Text = empresa;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            DateTime fecha_fin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);
            int IdBanco = string.IsNullOrEmpty(p_IdBanco.Value.ToString()) ? 0 : Convert.ToInt32(p_IdBanco.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            BAN_009_Bus bus_rpt = new BAN_009_Bus();


            List<BAN_009_Info> lst_rpt = new List<BAN_009_Info>();
            ba_Banco_Cuenta_x_tb_sucursal_Bus bus_cta_suc = new ba_Banco_Cuenta_x_tb_sucursal_Bus();
            var sucursal = bus_cta_suc.GetListSuc(IdEmpresa, IdSucursal);
            if (sucursal != null)
            {
                foreach (var item in sucursal)
                {
                    lst_rpt.AddRange(bus_rpt.GetList(IdEmpresa, item.IdBanco, fecha_fin, false));
                }
            }
            tb_sucursal_Bus bus_suc = new tb_sucursal_Bus();
            var suc = bus_suc.get_info(IdEmpresa, IdSucursal);
            lbl_sucursal.Text = suc.Su_Descripcion;

            this.DataSource = lst_rpt;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            ImageConverter obj = new ImageConverter();
            lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
        }
    }
}
