using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System.Collections.Generic;
using Core.Erp.Bus.General;
using System.Linq;

namespace Core.Erp.Web.Reportes.CuentasPorPagar
{
    public partial class CXP_014_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }

        List<CXP_014_Info> ListaAgrupada = new List<CXP_014_Info>();

        public CXP_014_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_014_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdProveedor = p_IdProveedor.Value == null ? 0 : Convert.ToDecimal(p_IdProveedor.Value);
            DateTime fechaIni = p_fecha_ini.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fechaFin = p_fecha_fin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);
            string IdTipoServicio = p_IdTipoServicio.Value == null ? "" : Convert.ToString(p_IdTipoServicio.Value);
            bool mostrar_anulados = p_mostrar_anulados.Value == null ? false : Convert.ToBoolean(p_mostrar_anulados.Value);

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            //lbl_empresa.Text = emp.RazonSocial;

            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }

            CXP_014_Bus bus_rpt = new CXP_014_Bus();
            List<CXP_014_Info> lst_rpt = new List<CXP_014_Info>();            

            if (IntArray != null)
            {
                foreach (var item in IntArray)
                {
                    lst_rpt.AddRange(bus_rpt.GetList(IdEmpresa, item, IdProveedor, fechaIni, fechaFin, IdTipoServicio, mostrar_anulados));
                }
            }

            this.DataSource = lst_rpt;

            ListaAgrupada = (from q in lst_rpt
                             group q by new
                             {
                                 q.IdSucursal,
                                 q.Su_Descripcion
                             } into Resumen
                             select new CXP_014_Info
                             {
                                 IdSucursal = Resumen.Key.IdSucursal,
                                 Su_Descripcion = Resumen.Key.Su_Descripcion,
                                 co_subtotal_iva = Resumen.Sum(q => q.co_subtotal_iva),
                                 co_subtotal_siniva = Resumen.Sum(q => q.co_subtotal_siniva),
                                 co_valoriva = Resumen.Sum(q => q.co_valoriva),
                                 co_total = Resumen.Sum(q => q.co_total)
                             }).ToList();
        }

        private void SubReporte_Resumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.DataSource = ListaAgrupada;
        }
    }
}
