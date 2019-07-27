using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_014_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        List<FAC_014_Info> ListaAgrupada = new List<FAC_014_Info>();
        public FAC_014_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_014_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            DateTime fecha_ini = p_fecha_ini.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = p_fecha_fin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);

            FAC_014_Bus bus_rpt = new FAC_014_Bus();
            List<FAC_014_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, fecha_ini, fecha_fin);
            this.DataSource = lst_rpt;

            ListaAgrupada = (from q in lst_rpt
                             group q by new
                             {
                                 q.IdEmpresa,
                                 q.Nombre_Evento
                             } into Resumen
                             select new FAC_014_Info
                             {
                                 IdEmpresa = Resumen.Key.IdEmpresa,
                                 Nombre_Evento = Resumen.Key.Nombre_Evento,
                                 Total = Resumen.Sum(q=>q.Total)
                             }).ToList();
        }

        private void SubReporte_resumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.DataSource = ListaAgrupada;
        }
    }
}
