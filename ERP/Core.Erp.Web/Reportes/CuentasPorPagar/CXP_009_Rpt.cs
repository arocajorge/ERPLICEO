using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using Core.Erp.Bus.Reportes.CuentasPorPagar;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Web.Reportes.CuentasPorPagar
{
    public partial class CXP_009_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }

        List<CXP_009_resumen_Info> lst_resumen = new List<CXP_009_resumen_Info>();
        public CXP_009_Rpt()
        {
            InitializeComponent();
        }

        private void CXP_009_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;
            lbl_empresa.Text = empresa;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            DateTime Fecha_ini = string.IsNullOrEmpty(p_Fecha_ini.Value.ToString()) ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(p_Fecha_ini.Value).Date;
            DateTime Fecha_fin = string.IsNullOrEmpty(p_Fecha_fin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_Fecha_fin.Value).Date;
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            bool mostrar_anulados = p_mostrar_anulados.Value == null ? false : Convert.ToBoolean(p_mostrar_anulados.Value);

            CXP_009_Bus bus_rpt = new CXP_009_Bus();
            List<CXP_009_Info> lst_rpt = new List<CXP_009_Info>();

            if (IntArray != null)
            {
                foreach (var item in IntArray)
                {            
                    lst_rpt.AddRange(bus_rpt.get_list(IdEmpresa, item, Fecha_ini, Fecha_fin, mostrar_anulados));
                }
            }

            var TdebitosxCta = (from q in lst_rpt
                                /*where q.valor_Retenido > 0*/
                                group q by new
                                {
                                    q.cod_Impuesto_SRI,
                                    q.Impuesto,
                                    q.por_Retencion_SRI,
                                    q.co_descripcion
                                } into grouping
                               select new {
                                   grouping.Key,
                                   total_base_ret = grouping.Sum(p => p.base_retencion),
                                   total_ret = grouping.Sum(p => p.valor_Retenido)
                               }).ToList();

            foreach (var item in TdebitosxCta)
            {
                lst_resumen.Add(new CXP_009_resumen_Info
                {
                    Base_Ret = item.total_base_ret,
                    Cod_Sri = item.Key.cod_Impuesto_SRI,
                    descripcion = item.Key.Impuesto + "." + item.Key.por_Retencion_SRI + " " + item.Key.co_descripcion,
                    Tipo_Retencion = item.Key.Impuesto,
                    Total_Ret = item.total_ret
                });
            }

            this.DataSource = lst_rpt;
        }

        private void Subreporte_resumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            ((XRSubreport)sender).ReportSource.DataSource = lst_resumen;
            ((XRSubreport)sender).ReportSource.FillDataSource();
        }
    }
}
