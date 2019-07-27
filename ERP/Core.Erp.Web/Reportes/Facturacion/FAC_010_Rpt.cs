using Core.Erp.Bus.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_010_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        List<FAC_010_Info> Lista = new List<FAC_010_Info>();
        List<FAC_010_Info> Lista_detalle = new List<FAC_010_Info>();

        public string usuario { get; set; }
        public string empresa { get; set; }
        public FAC_010_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_010_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            #region Grupo

            Lista = (from q in lst_rpt
                     group q by new
                          {
                              q.IdEmpresa,
                              q.IdSucursal,
                              q.IdCatalogo_FormaPago,
                              q.NombreFormaPago
                          } into Area
                          select new FAC_010_Info
                          {
                              Total = Area.Sum(q=>q.Total),
                              IdEmpresa = Area.Key.IdEmpresa,
                              IdSucursal = Area.Key.IdSucursal,
                              IdCatalogo_FormaPago = Area.Key.IdCatalogo_FormaPago,
                              NombreFormaPago = Area.Key.NombreFormaPago
                          }).ToList();

            Lista_detalle = (from q in lst_rpt
                     group q by new
                     {
                         q.IdEmpresa,
                         q.IdSucursal,
                         q.IdCatalogo_FormaPago,
                         q.NombreFormaPago,
                         q.vt_NumFactura,
                         q.vt_fecha,
                         q.Total
                     } into Factura
                     select new FAC_010_Info
                     {
                         IdEmpresa = Factura.Key.IdEmpresa,
                         IdSucursal = Factura.Key.IdSucursal,
                         IdCatalogo_FormaPago = Factura.Key.IdCatalogo_FormaPago,
                         NombreFormaPago = Factura.Key.NombreFormaPago,                         
                         vt_NumFactura = Factura.Key.vt_NumFactura,
                         vt_fecha = Factura.Key.vt_fecha,
                         Total = Factura.Key.Total
                     }).ToList();
            #endregion

            this.DataSource = lst_rpt;
        }

        private void resumen_forma_pago_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.DataSource = Lista;
        }

    }
}
