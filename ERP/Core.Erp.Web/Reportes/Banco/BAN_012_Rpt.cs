using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System.Collections.Generic;
using System.Linq;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Reportes.Banco
{
    public partial class BAN_012_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        List<BAN_012_Info> Lista = new List<BAN_012_Info>();

        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }

        public BAN_012_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_012_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            string IdUsuario = string.IsNullOrEmpty(p_IdUsuario.Value.ToString()) ? "" : Convert.ToString(p_IdUsuario.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            DateTime fechaIni = string.IsNullOrEmpty(p_fecha_ini.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fechaFin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);
            bool mostrarSaldo0 = string.IsNullOrEmpty(p_mostrarSaldo0.Value.ToString()) ? false : Convert.ToBoolean(p_mostrarSaldo0.Value);

            BAN_012_Bus bus_rpt = new BAN_012_Bus();
            List<BAN_012_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, fechaIni, fechaFin, mostrarSaldo0, IdUsuario);
            #region Grupo

            Lista = (from q in lst_rpt
                     group q by new
                     {
                         q.IdEmpresa,
                         q.ba_descripcion,
                         q.SaldoFinalBanco
                     } into Area
                     select new BAN_012_Info
                     {
                         SaldoFinalBanco = Area.Key.SaldoFinalBanco,
                         IdEmpresa = Area.Key.IdEmpresa,
                         ba_descripcion = Area.Key.ba_descripcion
                     }).ToList();

            #endregion

            tb_sucursal_Bus bus_suc = new tb_sucursal_Bus();
            var suc = bus_suc.get_info(IdEmpresa, IdSucursal);

            lbl_sucursal.Text = suc.Su_Descripcion;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }

            this.DataSource = lst_rpt;

        }

        private void subreport_cuentas_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.DataSource = Lista;

        }
    }
}
