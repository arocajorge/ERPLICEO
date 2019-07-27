using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System.Collections.Generic;
using Core.Erp.Bus.General;
using System.Linq;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_010_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        List<ROL_010_Info> lst_rpt_resumen = new List<ROL_010_Info>();
        public ROL_010_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_010_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdDivision = p_IdDivision.Value == null ? 0 : Convert.ToInt32(p_IdDivision.Value);
            int IdArea = p_IdArea.Value == null ? 0 : Convert.ToInt32(p_IdArea.Value);
            int IdTipoNomina = p_IdTipoNomina.Value == null ? 0 : Convert.ToInt32(p_IdTipoNomina.Value);
            string em_status = Convert.ToString(p_em_status.Value) == "" ? "" : Convert.ToString(p_em_status.Value);
            string Ubicacion = Convert.ToString(p_IdUbicacion.Value) == "" ? "" : Convert.ToString(p_IdUbicacion.Value);

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            lbl_empresa.Text = emp.em_nombre;
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }

            ROL_010_Bus bus_rpt = new ROL_010_Bus();
            List<ROL_010_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdDivision, IdArea, IdTipoNomina, em_status, Ubicacion);
            this.DataSource = lst_rpt;

            lst_rpt_resumen = (from q in lst_rpt
                               group q by new
                             {
                                 q.IdEmpresa,
                                 q.IdSucursal,
                                 q.IdDivision,
                                 q.IdArea,
                                 q.DescDivision,
                                 q.DescArea,
                                 q.de_descripcion
                             } into Resumen
                             select new ROL_010_Info
                             {
                                 IdEmpresa = Resumen.Key.IdEmpresa,
                                 IdSucursal = Resumen.Key.IdSucursal,
                                 IdDivision = Resumen.Key.IdDivision,
                                 IdArea = Resumen.Key.IdArea,                                 
                                 DescDivision = Resumen.Key.DescDivision,
                                 DescArea = Resumen.Key.DescArea,
                                 de_descripcion = Resumen.Key.de_descripcion,
                                 CantidadEmpleados = Resumen.Count()
                             }).ToList();
        }

        private void Subreporte_Resumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var Resumen = lst_rpt_resumen;
            ((XRSubreport)sender).ReportSource.DataSource = Resumen;
        }
    }
}
