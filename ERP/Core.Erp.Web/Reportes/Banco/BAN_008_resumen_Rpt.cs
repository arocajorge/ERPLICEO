using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Web.Reportes.Banco
{
    public partial class BAN_008_resumen_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        List<BAN_008_Info> ListaAgrupada = new List<BAN_008_Info>();
        public BAN_008_resumen_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_008_resumen_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            //DateTime fecha_ini = string.IsNullOrEmpty(p_fecha_ini.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            //DateTime fecha_fin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);
            //int IdBanco = string.IsNullOrEmpty(p_IdBanco.Value.ToString()) ? 0 : Convert.ToInt32(p_IdBanco.Value);
            //int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            //BAN_008_Bus bus_rpt = new BAN_008_Bus();
            //List<BAN_008_Info> lst_rpt = bus_rpt.GetList(IdEmpresa,fecha_ini,fecha_fin, IdBanco);
            //ListaAgrupada = (from q in lst_rpt
            //                 group q by new
            //                 {
            //                     q.IdEmpresa,
            //                     q.ba_descripcion,
            //                     q.Valor
            //                 } into Resumen
            //                 select new BAN_008_Info
            //                 {
            //                     IdEmpresa = Resumen.Key.IdEmpresa,
            //                     ba_descripcion = Resumen.Key.ba_descripcion,
            //                     Valor = Resumen.Sum(q => q.Valor)
            //                 }).ToList();
            //this.DataSource = lst_rpt;
        }
    }
}
