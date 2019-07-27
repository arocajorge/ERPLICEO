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
    public partial class ROL_023_Resumen_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        List<ROL_023_Info> ListaAgrupada = new List<ROL_023_Info>();
        List<ROL_023_Info> ListaAgrupadaResumen = new List<ROL_023_Info>();
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_023_Resumen_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_023_Resumen_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdNomina = string.IsNullOrEmpty(p_IdNomina.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNomina.Value);
            int IdNominaTipoLiqui = string.IsNullOrEmpty(p_IdNominaTipoLiqui.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNominaTipoLiqui.Value);
            int IdPeriodo = string.IsNullOrEmpty(p_IdPeriodo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdPeriodo.Value);
            int IdDivision = string.IsNullOrEmpty(p_IdDivision.Value.ToString()) ? 0 : Convert.ToInt32(p_IdDivision.Value);
            int IdArea = string.IsNullOrEmpty(p_IdArea.Value.ToString()) ? 0 : Convert.ToInt32(p_IdArea.Value);
            int IdDepartamento = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdDepartamento.Value);


            ROL_023_Bus bus_rpt = new ROL_023_Bus();
            List<ROL_023_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdNomina, IdNominaTipoLiqui, IdPeriodo, IdDivision, IdArea, IdDepartamento);
            
            lst_rpt.ForEach(q => {
                q.FRESERVA_R = Math.Round(q.FRESERVA ?? 0, 2, MidpointRounding.AwayFromZero);
                q.IESS_R = Math.Round(q.IESS ?? 0, 2, MidpointRounding.AwayFromZero);
                q.TOTALI = q.SUELDO + q.OTROING + q.SOBRET + q.DECIMOC + q.DECIMOT + q.FRESERVA_R;
                q.TOTALE = q.PRESTAMO + q.IESS_R + q.ANTICIPO + q.OTROEGR;
                q.NETO = q.TOTALI - q.TOTALE;
            });

            lst_rpt.ForEach(q => {
                q.FRESERVA_R = Math.Round(q.FRESERVA ?? 0, 2, MidpointRounding.AwayFromZero);
                q.IESS_R = Math.Round(q.IESS ?? 0, 2, MidpointRounding.AwayFromZero);
                q.TOTALI = q.SUELDO + q.OTROING + q.SOBRET + q.DECIMOC + q.DECIMOT + q.FRESERVA_R;
                q.TOTALE = q.PRESTAMO + q.IESS_R + q.ANTICIPO + q.OTROEGR;
                q.NETO = q.TOTALI - q.TOTALE;
            });

            var lst = (from q in lst_rpt
                       where q.Fila != 1
                       group q by new
                       {
                           q.IdEmpleado
                       }
                      into g
                       select new ROL_023_Info
                       {
                           IdEmpleado = g.Key.IdEmpleado,
                           IESS_2 = g.Sum(q => q.IESS_R),
                           FRESERVA_2 = g.Sum(q => q.FRESERVA_R)
                       }).ToList();

            var lst_final = (from a in lst_rpt
                             join b in lst on new { a.IdEmpleado } equals new { b.IdEmpleado } into c
                             from b in c.DefaultIfEmpty()
                             select new ROL_023_Info
                             {
                                 IdEmpresa = a.IdEmpresa,
                                 IdPeriodo = a.IdPeriodo,
                                 ANTICIPO = a.ANTICIPO,
                                 DECIMOC = a.DECIMOC,
                                 DECIMOT = a.DECIMOT,
                                 DIASTRABAJADOS = a.DIASTRABAJADOS,
                                 FRESERVA = a.FRESERVA,
                                 IdArea = a.IdArea,
                                 IdDepartamento = a.IdDepartamento,
                                 IdDivision = a.IdDivision,
                                 IdEmpleado = a.IdEmpleado,
                                 IdNominaTipo = a.IdNominaTipo,
                                 IdNominaTipoLiqui = a.IdNominaTipoLiqui,
                                 IdRol = a.IdRol,
                                 IdSucursal = a.IdSucursal,
                                 IESS = a.IESS,
                                 NETO = a.NETO,
                                 NombreArea = a.NombreArea,
                                 NombreDepartamento = a.NombreDepartamento,
                                 NombreDivision = a.NombreDivision,
                                 OTROEGR = a.OTROEGR,
                                 OTROING = a.OTROING,
                                 pe_nombreCompleto = a.pe_nombreCompleto,
                                 PRESTAMO = a.PRESTAMO,
                                 SOBRET = a.SOBRET,
                                 SUELDO = a.SUELDO,
                                 Su_Descripcion = a.Su_Descripcion,
                                 TOTALE = a.TOTALE,
                                 TOTALI = a.TOTALI,
                                 Descripcion = a.Descripcion,
                                 DescripcionProcesoNomina = a.DescripcionProcesoNomina,
                                 JORNADA = a.JORNADA,
                                 UBUCACION = a.UBUCACION,

                                 FRESERVA_R = a.FRESERVA_R,
                                 IESS_R = a.IESS_R,

                                 FRESERVA_TOTAL = a.FRESERVA_TOTAL,
                                 IESS_TOTAL = a.IESS_TOTAL,
                                 Fila = a.Fila,

                                 IESS_2 = b != null ? b.IESS_2 : 0,
                                 FRESERVA_2 = b != null ? b.FRESERVA_2 : 0
                             }).ToList();
            lst_final.Where(q => q.Fila == 1).ToList().ForEach(q =>
            {
                q.FRESERVA_R += q.FRESERVA_TOTAL - (q.FRESERVA_R + (q.FRESERVA_2 ?? 0));
                q.IESS_R += q.IESS_TOTAL - (q.IESS_R + (q.IESS_2 ?? 0));
                q.TOTALI = q.SUELDO + q.OTROING + q.SOBRET + q.DECIMOC + q.DECIMOT + q.FRESERVA_R;
                q.TOTALE = q.PRESTAMO + q.IESS_R + q.ANTICIPO + q.OTROEGR;
                q.NETO = q.TOTALI - q.TOTALE;
            });

            ListaAgrupada = (from q in lst_final
                             group q by new
                             {
                                 q.IdEmpresa,
                                 q.IdSucursal,
                                 q.Su_Descripcion,
                                 q.Descripcion,
                                 q.DescripcionProcesoNomina,
                                 q.IdPeriodo,
                                 q.IdNominaTipo,
                                 q.IdNominaTipoLiqui,
                                 q.NombreDivision,
                                 q.IdArea,
                                 q.NombreArea,
                                 q.IdDepartamento,
                                 q.NombreDepartamento,
                                 q.IdEmpleado
                             } into Resumen
                             select new ROL_023_Info
                             {
                                 IdEmpresa = Resumen.Key.IdEmpresa,
                                 IdSucursal = Resumen.Key.IdSucursal,
                                 IdPeriodo = Resumen.Key.IdPeriodo,
                                 IdNominaTipo = Resumen.Key.IdNominaTipo,
                                 IdNominaTipoLiqui = Resumen.Key.IdNominaTipoLiqui,
                                 NombreDivision = Resumen.Key.NombreDivision,
                                 IdArea = Resumen.Key.IdArea,
                                 NombreArea = Resumen.Key.NombreArea,
                                 IdDepartamento = Resumen.Key.IdDepartamento,
                                 Su_Descripcion = Resumen.Key.Su_Descripcion,
                                 Descripcion = Resumen.Key.Descripcion,
                                 DescripcionProcesoNomina = Resumen.Key.DescripcionProcesoNomina,
                                 NombreDepartamento = Resumen.Key.NombreDepartamento,
                                 IdEmpleado = Resumen.Key.IdEmpleado,
                                 TOTALI = Resumen.Sum(q => q.TOTALI),
                                 DECIMOT = Resumen.Sum(q => q.DECIMOT),
                                 DECIMOC = Resumen.Sum(q => q.DECIMOC),
                                 FRESERVA = Resumen.Sum(q => q.FRESERVA_R),
                                 SUELDO = Resumen.Sum(q => q.SUELDO + q.SOBRET + q.OTROING),
                                 SOBRET = Resumen.Sum(q => q.SOBRET),
                                 OTROING = Resumen.Sum(q => q.OTROING),

                                 TotalResumen = Resumen.Sum(q => q.SUELDO + q.DECIMOT + q.DECIMOC + q.FRESERVA_R + q.SOBRET + q.OTROING)
                             }).ToList();


            ListaAgrupadaResumen = (from q in ListaAgrupada
                                    group q by new
                                    {
                                        q.IdEmpresa,
                                        q.Su_Descripcion,
                                        q.Descripcion,
                                        q.DescripcionProcesoNomina,
                                        q.IdSucursal,
                                        q.IdPeriodo,
                                        q.IdNominaTipo,
                                        q.IdNominaTipoLiqui,
                                        q.NombreDivision,
                                        q.IdArea,
                                        q.NombreArea,
                                        q.IdDepartamento,
                                        q.NombreDepartamento
                                    } into Resumen_Reporte
                                    select new ROL_023_Info
                                    {
                                        IdEmpresa = Resumen_Reporte.Key.IdEmpresa,
                                        IdSucursal = Resumen_Reporte.Key.IdSucursal,
                                        IdPeriodo = Resumen_Reporte.Key.IdPeriodo,
                                        IdNominaTipo = Resumen_Reporte.Key.IdNominaTipo,
                                        IdNominaTipoLiqui = Resumen_Reporte.Key.IdNominaTipoLiqui,
                                        Su_Descripcion = Resumen_Reporte.Key.Su_Descripcion,
                                        Descripcion = Resumen_Reporte.Key.Descripcion,
                                        DescripcionProcesoNomina = Resumen_Reporte.Key.DescripcionProcesoNomina,
                                        NombreDivision = Resumen_Reporte.Key.NombreDivision,
                                        IdArea = Resumen_Reporte.Key.IdArea,
                                        NombreArea = Resumen_Reporte.Key.NombreArea,
                                        IdDepartamento = Resumen_Reporte.Key.IdDepartamento,
                                        NombreDepartamento = Resumen_Reporte.Key.NombreDepartamento,
                                        CantidadEmpleados = Resumen_Reporte.Count()
                                    }).ToList();

            foreach (var item in ListaAgrupadaResumen)
            {
                //item.CantidadEmpleados = ListaAgrupadaResumen.Where(q => q.IdDepartamento == item.IdDepartamento).FirstOrDefault().CantidadEmpleados;
                item.TOTALI = ListaAgrupada.Where(q => q.IdArea == item.IdArea && q.IdDepartamento == item.IdDepartamento).Sum(q => q.TOTALI);
                item.DECIMOT = ListaAgrupada.Where(q => q.IdArea == item.IdArea && q.IdDepartamento == item.IdDepartamento).Sum(q => q.DECIMOT);
                item.DECIMOC = ListaAgrupada.Where(q => q.IdArea == item.IdArea && q.IdDepartamento == item.IdDepartamento).Sum(q => q.DECIMOC);
                item.FRESERVA = ListaAgrupada.Where(q => q.IdArea == item.IdArea && q.IdDepartamento == item.IdDepartamento).Sum(q => q.FRESERVA);
                item.SUELDO = ListaAgrupada.Where(q => q.IdArea == item.IdArea && q.IdDepartamento == item.IdDepartamento).Sum(q => q.SUELDO);
                item.SOBRET = ListaAgrupada.Where(q => q.IdArea == item.IdArea && q.IdDepartamento == item.IdDepartamento).Sum(q => q.SOBRET);
                item.OTROING = ListaAgrupada.Where(q => q.IdArea == item.IdArea && q.IdDepartamento == item.IdDepartamento).Sum(q => q.OTROING);
                item.TotalResumen = ListaAgrupada.Where(q => q.IdArea == item.IdArea && q.IdDepartamento == item.IdDepartamento).Sum(q => q.TotalResumen);
            }

            this.DataSource = ListaAgrupadaResumen;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            lbl_empresa.Text = emp.em_nombre;
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }
        }
    }
}
