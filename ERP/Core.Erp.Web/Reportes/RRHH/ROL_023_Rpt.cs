using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.General;
using Core.Erp.Info.Reportes.RRHH;
using System.Collections.Generic;
using Core.Erp.Bus.Reportes.RRHH;
using System.Linq;

namespace Core.Erp.Web.Reportes.RRHH
{
    public partial class ROL_023_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        List<ROL_023_Info> ListaAgrupada = new List<ROL_023_Info>();
        List<ROL_023_Info> ListaAgrupadaResumen = new List<ROL_023_Info>();
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ROL_023_Rpt()
        {
            InitializeComponent();
        }

        private void ROL_023_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty( p_IdEmpresa.Value.ToString())? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdNomina = string.IsNullOrEmpty(p_IdNomina.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNomina.Value);
            int IdNominaTipoLiqui = string.IsNullOrEmpty(p_IdNominaTipoLiqui.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNominaTipoLiqui.Value);
            int IdPeriodo = string.IsNullOrEmpty(p_IdPeriodo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdPeriodo.Value);
            int IdDivision = string.IsNullOrEmpty(p_IdDivision.Value.ToString()) ? 0 : Convert.ToInt32(p_IdDivision.Value);
            int IdArea = string.IsNullOrEmpty(p_IdArea.Value.ToString()) ? 0 : Convert.ToInt32(p_IdArea.Value);
            int IdDepartamento = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdDepartamento.Value);


            ROL_023_Bus bus_rpt = new ROL_023_Bus();
            List<ROL_023_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdSucursal, IdNomina, IdNominaTipoLiqui, IdPeriodo, IdDivision, IdArea, IdDepartamento);

            #region MOVIMIENTO DE ANTICIPO PARA QUE AFECTE LA FILA CON MAYOR INGRESO
            //OBTENER REGISTROS DE LA FILA 1 QUE ESTEN EN NEGATIVO
            var lst_1 = lst_rpt.Where(q => q.NETO < 0 && q.Fila == 1).ToList();
            //CRUZAR CON REGISTROS DE FILA 2 PARA PASAR EL VALOR
            var lst_2_ = lst_rpt.Where(q => q.Fila == 2).ToList();
            ROL_023_Info objrpt = new ROL_023_Info();
            lst_1.ForEach(q =>
            {
                objrpt = lst_rpt.Where(v => v.IdEmpresa == q.IdEmpresa && v.IdEmpleado == q.IdEmpleado && v.Fila == 2).FirstOrDefault();
                if(objrpt != null)
                {
                    objrpt.ANTICIPO = q.ANTICIPO;
                    q.ANTICIPO = 0;
                }
            });
            #endregion

            #region REDONDEOS POR CUADRE DE FONDO DE RESERVA E IESS
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
                           IESS_2 = g.Sum(q=>q.IESS_R),
                           FRESERVA_2 = g.Sum(q=> q.FRESERVA_R)
                       }).ToList();

            var lst_final = (from a in lst_rpt
                             join b in lst on new { a.IdEmpleado }  equals new { b.IdEmpleado } into c
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
            lst_final.Where(q=> q.Fila == 1).ToList().ForEach(q =>
            {
                q.FRESERVA_R += q.FRESERVA_TOTAL - (q.FRESERVA_R + (q.FRESERVA_2 ?? 0));
                q.IESS_R += q.IESS_TOTAL- (q.IESS_R + (q.IESS_2 ?? 0));
                q.TOTALI = q.SUELDO + q.OTROING + q.SOBRET + q.DECIMOC + q.DECIMOT + q.FRESERVA_R;
                q.TOTALE = q.PRESTAMO + q.IESS_R + q.ANTICIPO + q.OTROEGR;
                q.NETO = q.TOTALI - q.TOTALE;
            });
            #endregion

            this.DataSource = lst_final;
            

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
