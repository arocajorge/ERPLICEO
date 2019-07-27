using DevExpress.Web.Mvc;
using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using Core.Erp.Web.Reportes.RRHH;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Erp.Info.Reportes.RRHH;
using Core.Erp.Bus.Reportes.RRHH;
using Core.Erp.Web.Areas.Reportes.Views.RRHHReportes;
using Core.Erp.Bus.Contabilidad;
using System.Web;

namespace Core.Erp.Web.Areas.Reportes.Controllers
{
    [SessionTimeout]
    public class RRHHReportesController : Controller
    {
        ro_division_Bus bus_division = new ro_division_Bus();
        ro_area_Bus bus_area = new ro_area_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ro_nomina_tipo_Bus bus_tiponomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        ro_periodo_x_ro_Nomina_TipoLiqui_Bus bus_periodo_x_nominas = new ro_periodo_x_ro_Nomina_TipoLiqui_Bus();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        preliquidacion_List preliquidacion_list = new preliquidacion_List();
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Facturacion.repx";

        #region Metodos ComboBox bajo demanda empleado
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_RRHH()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            return PartialView("_CmbEmpleado_RRHH", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        #endregion



        #region Metodos ComboBox bajo rubros
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        public ActionResult CmbRubro_roles()
        {
            cl_filtros_Info model =new cl_filtros_Info();
            return PartialView("_CmbRubro_roles", model);
        }
        public List<ro_rubro_tipo_Info> get_list_bajo_demanda_rubro(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_rubro.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ro_rubro_tipo_Info get_info_bajo_demanda_rubro(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_rubro.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa),args);
        }

        #endregion

        #region metodo bajo demanda Division
        public ActionResult CmbDivision_reportes()
        {
            int model = new int();
            return PartialView("_CmbDivision_reportes", model);
        }
        public List<ro_division_Info> get_list_bajo_demanda_division(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_division.get_list_bajo_demanda_division(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ro_division_Info get_info_bajo_demanda_division(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_division.get_info_bajo_demanda_division(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion


        #region metodo bajo demanda Area
        public ActionResult CmbArea_reportes()
        {
            SessionFixed.IdDivision = Request.Params["IdDivision"] != null ? Request.Params["IdDivision"].ToString() : SessionFixed.IdDivision;
            int model = new int();
            return PartialView("_CmbArea_reportes", model);
        }
        public List<ro_area_Info> get_list_bajo_demanda_area(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            
            return bus_area.get_list_bajo_demanda_area(args, Convert.ToInt32(SessionFixed.IdEmpresa), false, Convert.ToInt32(SessionFixed.IdDivision));
        }
        public ro_area_Info get_info_bajo_demanda_area(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_area.get_info_bajo_demanda_area(args, Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdDivision));
        }
        #endregion

        #region metodo bajo demanda Area Independiente
        public ActionResult CmbArea_Individual()
        {
            //SessionFixed.IdDivision = Request.Params["IdDivision"] != null ? Request.Params["IdDivision"].ToString() : SessionFixed.IdDivision;
            int model = new int();
            return PartialView("_CmbArea_Individual", model);
        }
        public List<ro_area_Info> get_list_bajo_demanda_individual(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_area.get_list_bajo_demanda_individual(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ro_area_Info get_info_bajo_demanda_individual(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_area.get_info_bajo_demanda_individual(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region metodo bajo demanda sucursal
        public ActionResult CmbSucursalReportes_RRHH()
        {
            int model = new int();
            return PartialView("_CmbSucursal_reportes_RRHH", model);
        }
        public List<tb_sucursal_Info> get_list_bajo_demanda_sucursal(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_sucursal.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public tb_sucursal_Info get_info_bajo_demanda_sucursal(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_sucursal.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa),args);
        }
        #endregion

        #region metodo bajo tipo nomina
        public ActionResult CmbTipoNomina()
        {
            int model = new int();
            return PartialView("_CmbTipoNomina", model);
        }
        public List<ro_nomina_tipo_Info> get_list_bajo_demanda_tiponomina(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_tiponomina.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ro_nomina_tipo_Info get_info_bajo_demanda_tiponomina(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_tiponomina.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }
        #endregion

        public ActionResult ROL_001(int IdNomina_Tipo = 0, int IdNomina_TipoLiqui= 0, int IdPeriodo=0, int IdSucursal=0)
        {
            ROL_001_Rpt model = new ROL_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdNomina.Value = IdNomina_Tipo;
            model.p_IdNominaTipo.Value = IdNomina_TipoLiqui;
            model.p_IdPeriodo.Value = IdPeriodo;
            model.p_IdSucursal.Value = IdSucursal;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult ROL_002(int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0, int IdPeriodo = 0, int IdSucursal = 0)
        {

            ROL_002_Rpt model = new ROL_002_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_002");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdNomina.Value = IdNomina_Tipo;
            model.p_IdNominaTipo.Value = IdNomina_TipoLiqui;
            model.p_IdPeriodo.Value = IdPeriodo;
            model.p_IdSucursal.Value = IdSucursal;
            model.empresa = SessionFixed.NomEmpresa;

            return View(model);
        }
        public ActionResult ROL_003(decimal IdEmpleado=0, decimal IdNovedad=0)
        {
            ROL_003_Rpt model = new ROL_003_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_003");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdEmpleado.Value = IdEmpleado;
            model.p_IdNovedad.Value = IdNovedad;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult ROL_004(int IdUtilidad=0)
        {
            ROL_004_Rpt model = new ROL_004_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_004");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdUtilidad.Value = IdUtilidad;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult ROL_005(decimal IdActaFiniquito = 0, decimal IdTransaccionSession=0)
        {
            ROL_005_Rpt model = new ROL_005_Rpt();
            if (IdTransaccionSession>0)
            {
                model.DataSource = preliquidacion_list.get_list(IdTransaccionSession);
            }
            else
            {
                model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
                model.p_IdActaFiniquito.Value = IdActaFiniquito;
            }

            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_005");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.usuario = SessionFixed.IdUsuario;
            model.empresa = SessionFixed.NomEmpresa;

            return View(model);
        }
        public ActionResult ROL_006( decimal IdEmpleado = 0)
        {
            ROL_006_Rpt model = new ROL_006_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_006");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdEmpleado.Value = IdEmpleado;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult ROL_007(decimal IdEmpleado = 0, int IdSolicitud = 0)
        {
            ROL_007_Rpt model = new ROL_007_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_007");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdEmpleado.Value = IdEmpleado;
            model.p_IdSolicitud.Value = IdSolicitud;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult ROL_008(decimal IdPrestamo = 0 )
        {
            ROL_008_Rpt model = new ROL_008_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_008");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdPrestamo.Value = IdPrestamo;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult ROL_009()
        {
            string[] novedad = new string[2] { "PEN", "CAN" };
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdTipoNomina = 1,
                fecha_ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                fecha_fin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1),
                estado_novedad = novedad,
                TipoRubro = "",
                IdArea=0
                
                
            };
            cargar_combos(model.IdEmpresa);
            ROL_009_Rpt report = new ROL_009_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_009");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdTipo_Nomina.Value = model.IdTipoNomina;
            report.p_fecha_inicio.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_estado_novedad.Value = novedad == null ? "" : Convert.ToString(model.estado_novedad);
            report.p_IdEmpleado.Value = model.IdEmpleado == null ? 0 : Convert.ToDecimal(model.IdEmpleado);
            report.p_IdRubro.Value = model.IdRubro == null ? "" : Convert.ToString(model.IdEmpleado);
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            report.P_IdArea.Value = 0;
            report.P_TipoRubro.Value = "";
            ViewBag.Report = report;

            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_009(cl_filtros_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            string novedad = "";
            foreach (var item in model.estado_novedad)
            {
                novedad += item+",";
            }
            cargar_combos(model.IdEmpresa);
            ROL_009_Rpt report = new ROL_009_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_009");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdTipo_Nomina.Value = model.IdTipoNomina;
            report.p_fecha_inicio.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_estado_novedad.Value = novedad;
            report.p_IdEmpleado.Value = model.IdEmpleado == null ? 0 : Convert.ToDecimal(model.IdEmpleado);
            report.p_IdRubro.Value = model.IdRubro == null ? "" : Convert.ToString(model.IdRubro);
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            report.P_IdArea.Value = model.IdArea;
            report.P_TipoRubro.Value = model.TipoRubro;
            ViewBag.Report = report;
            return View(model);
        }

        private void cargar_filtros_ROL_010()
        {            
            var lst_EstadoEmpleado = bus_catalogo.get_list_x_tipo(25);
            lst_EstadoEmpleado.Add(new ro_catalogo_Info
            {
                CodCatalogo = "",
                ca_descripcion = "Todos"
            });
            ViewBag.lst_EstadoEmpleado = lst_EstadoEmpleado;

            var lst_ubi = bus_catalogo.get_list_x_tipo(44);
            lst_ubi.Add(new ro_catalogo_Info
            {
                CodCatalogo = "",
                ca_descripcion = "Todos"
            });
            ViewBag.lst_ubi = lst_ubi;

            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_nomina_tipo = bus_tiponomina.get_list(IdEmpresa, false);
            lst_nomina_tipo.Add(new ro_nomina_tipo_Info
            {
                IdEmpresa =IdEmpresa,
                IdNomina_Tipo = 0,
                Descripcion = "TODAS"
            });

            ViewBag.lst_nomina_tipo = lst_nomina_tipo;
        }

        public ActionResult ROL_010()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdTipoNomina = 0,
                em_status = "",
                IdDivision = 0,
                IdArea = 0,
                Ubicacion =""
            };

            ROL_010_Rpt report = new ROL_010_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_010");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            report.p_IdSucursal.Value = Convert.ToInt32(SessionFixed.IdSucursal);
            report.p_IdDivision.Value = model.IdDivision;
            report.p_IdArea.Value = model.IdArea;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            report.p_em_status.Value = model.em_status;
            report.p_IdUbicacion.Value = model.Ubicacion;
            ViewBag.Report = report;
            cargar_filtros_ROL_010();
            return View(model);
        }

        [HttpPost]
        public ActionResult ROL_010(cl_filtros_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            ROL_010_Rpt report = new ROL_010_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_010");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdTipoNomina.Value = model.IdTipoNomina;
            report.p_IdDivision.Value = model.IdDivision;
            report.p_IdArea.Value = model.IdArea;
            report.p_em_status.Value = model.em_status;
            report.p_IdUbicacion.Value = model.Ubicacion;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            cargar_filtros_ROL_010();
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult ROL_011( int IdHorasExtras =0)
        {
            ROL_011_Rpt model = new ROL_011_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_011");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdHorasExtras.Value = IdHorasExtras;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
            ct_anio_fiscal_Bus bus_anio = new ct_anio_fiscal_Bus();

            var lst_nomina = bus_nomina.get_list(IdEmpresa, false);
            ViewBag.lst_nomina = lst_nomina;

            var lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, false);
            lst_nomina_tipo.Add(new ro_Nomina_Tipoliqui_Info
            {
                IdEmpresa = IdEmpresa,
                IdNomina_Tipo = 0,
                Descripcion = "TODAS"
            });
            ViewBag.lst_nomina_tipo = lst_nomina_tipo;

            var lst_area = bus_area.get_list(IdEmpresa, false);
            lst_area.Add(new ro_area_Info
            {
                IdEmpresa = IdEmpresa,
                IdArea = 0,
                Descripcion = "TODAS"
            });
            ViewBag.lst_area = lst_area;

            var lst_periodos = bus_periodo_x_nominas.get_list_utimo_periodo_aprocesar(IdEmpresa, 0,0);
            ViewBag.lst_periodos = lst_periodos;

            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            lst_sucursal.Add(new tb_sucursal_Info
            {
                IdEmpresa = IdEmpresa,
                 IdSucursal = 0,
                Su_Descripcion = "TODAS"
            }); ViewBag.lst_sucursal = lst_sucursal;

            ro_departamento_Bus bus_dep = new ro_departamento_Bus();
            var lst_dep = bus_dep.get_list(IdEmpresa, false);
            ViewBag.lst_dep = lst_dep;

            tb_banco_procesos_bancarios_x_empresa_Bus bus_procesos = new tb_banco_procesos_bancarios_x_empresa_Bus();
            var lst_proceso = bus_procesos.get_list(IdEmpresa, false);
            ViewBag.lst_proceso = lst_proceso;

            var lst_anio = bus_anio.get_list(false);
            ViewBag.lst_anio = lst_anio;
        }
        public ActionResult ROL_012( )
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            ROL_012_Rpt report = new ROL_012_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_012");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_fecha_inicio.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_IdRubro.Value = model.IdRubro;            
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROl_012(cl_filtros_Info model)
        {
            ROL_012_Rpt report = new ROL_012_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_012");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_fecha_inicio.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_IdRubro.Value = model.IdRubro;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult ROL_013()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdNomina = 1
            };
            cargar_combos(model.IdEmpresa);
            ROL_013_Rpt report = new ROL_013_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_013");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdNomina.Value = model.IdNomina;
            report.p_FechaIni.Value = model.fecha_ini;
            report.p_IdEmpleado.Value = model.IdEmpleado;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_FechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_013(cl_filtros_Info model)
        {
            ROL_013_Rpt report = new ROL_013_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_013");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdNomina.Value = model.IdNomina;
            report.p_FechaIni.Value = model.fecha_ini;
            report.p_IdEmpleado.Value = model.IdEmpleado;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_FechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            cargar_combos(model.IdEmpresa);

            return View(model);
        }
        public ActionResult ROL_014()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdTipoNomina = 0,
                IdArea = 0,
                IdDivision = 0
            };
            cargar_combos(model.IdEmpresa);
            ROL_014_Rpt report = new ROL_014_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_014");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdTipoNomina.Value = model.IdTipoNomina;
            report.p_IdArea.Value = model.IdArea;
            report.p_IdDivision.Value = model.IdDivision;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();            
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_014(cl_filtros_Info model)
        {
            cargar_combos(model.IdEmpresa);
            ROL_014_Rpt report = new ROL_014_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_014");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdTipoNomina.Value = model.IdTipoNomina;
            report.p_IdArea.Value = model.IdArea;
            report.p_IdDivision.Value = model.IdDivision;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult ROL_015()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdEmpleado = 0
                
            };
            ROL_015_Rpt report = new ROL_015_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_015");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdEmpleado.Value = model.IdEmpleado;
            report.p_fechaInicio.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_015(cl_filtros_Info model)
        {
            ROL_015_Rpt report = new ROL_015_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_015");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdEmpleado.Value = model.IdEmpleado;
            report.p_fechaInicio.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult ROL_016()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdEmpleado = 0

            };
            ROL_016_Rpt report = new ROL_016_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_016");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdEmpleado.Value = model.IdEmpleado;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_016(cl_filtros_Info model)
        {
            ROL_016_Rpt report = new ROL_016_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_016");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdEmpleado.Value = model.IdEmpleado;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult ROL_017()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdEmpleado = 0
            };
            ROL_017_Rpt report = new ROL_017_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_017");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdEmpleado.Value = model.IdEmpleado;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_017(cl_filtros_Info model)
        {
            ROL_017_Rpt report = new ROL_017_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_017");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdEmpleado.Value = model.IdEmpleado;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult ROL_018()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdEmpleado = 0
            };
            ROL_018_Rpt report = new ROL_018_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_018");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdEmpleado.Value = model.IdEmpleado;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_018(cl_filtros_Info model)
        {
            ROL_018_Rpt report = new ROL_018_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_018");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdEmpleado.Value = model.IdEmpleado;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        //public ActionResult ROL_019(cl_filtros_Info model)
        //{
        //    model.fecha_ini = new DateTime (DateTime.Now.Year, DateTime.Now.Month, 1);
        //    DateTime FechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //    model.fecha_fin= FechaFin.AddMonths(1).AddDays(-1);
          
        //    model.IdEmpresa =Convert.ToInt32( SessionFixed.IdEmpresa);
        //    ViewBag.fecha_ini = model.fecha_ini;
        //    ViewBag.fecha_fin = model.fecha_fin;
        //    ViewBag.IdEmpresa = model.IdEmpresa;
        //    ViewBag.IdEmpleado = model.IdEmpleado;

        //    ViewBag.DemoOptions = ViewBag.DemoOptions ?? new PivotGridExportDemoOptions();

        //    Session["IdEmpleado"] = model.IdEmpleado;
        //    Session["fecha_ini"] = model.fecha_ini;
        //    Session["fecha_fin"] = model.fecha_fin;

        //    return PartialView(model);
        //}

        //[ValidateInput(false)]
        //public ActionResult PivotGridROL_019(int? IdEmpresa, DateTime? fecha_ini, DateTime? fecha_fin, decimal ? IdEmpleado)
        //{
        //    List<ROL_019_Info> lista = new List<ROL_019_Info>();
        //    IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
        //    fecha_ini =Convert.ToDateTime( Session["fecha_ini"] );
        //    fecha_fin = Convert.ToDateTime(Session["fecha_fin"]);

        //    if (IdEmpleado == null)
        //        IdEmpleado = 0;
        //    ROL_019_Bus bus = new ROL_019_Bus();
        //    lista = bus.get_list(Convert.ToInt32( IdEmpresa),Convert.ToDecimal(IdEmpleado), Convert.ToDateTime(fecha_ini), Convert.ToDateTime(fecha_fin));
        //    return PartialView("_PivotGridROL_019", lista);
        //}
        public ActionResult ROL_019()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = 0,
                IdNominaTipoLiqui = 0
            };
            ROL_019_Rpt report = new ROL_019_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_019");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa == 0 ? Convert.ToInt32(SessionFixed.IdEmpresa) : model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdNominaTipoLiqui.Value = model.IdNominaTipoLiqui;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_019(cl_filtros_Info model)
        {
            ROL_019_Rpt report = new ROL_019_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_019");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa == 0 ? Convert.ToInt32(SessionFixed.IdEmpresa) : model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal == 0 ? Convert.ToInt32(SessionFixed.IdSucursal) : model.IdSucursal;
            report.p_IdNominaTipoLiqui.Value = model.IdNominaTipoLiqui;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult ROL_020(int IdEmpresa=0, int IdTipoNomina=0, int IdNomina=0, int IdPeriodo=0, int IdSucursal=0, int IdProceso=0)
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = IdEmpresa == 0 ? Convert.ToInt32(SessionFixed.IdEmpresa) : IdEmpresa,
                IdSucursal = IdSucursal==0 ? Convert.ToInt32(SessionFixed.IdSucursal): IdSucursal,
                IdProceso = IdProceso==0 ? 1 : IdProceso,
                IdTipoNomina = IdTipoNomina,
                IdNomina = IdNomina,
                IdPeriodo = IdPeriodo
            };
            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            ROL_020_Rpt reporte = new ROL_020_Rpt();
            #region Cargo diseño desde base
            var report = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_020");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                reporte.LoadLayout(RootReporte);
            }
            #endregion
            reporte.p_IdEmpresa.Value = model.IdEmpresa;
            reporte.p_IdNominaTipo.Value = model.IdTipoNomina;
            reporte.p_IdNomina.Value = model.IdNomina;
            reporte.p_IdPeriodo.Value = model.IdPeriodo;
            reporte.p_IdSucursal.Value = model.IdSucursal;
            reporte.p_IdProceso.Value = model.IdProceso;
            reporte.P_IdArea.Value = model.IdArea;
            reporte.P_IdDivision.Value = model.IdDivision;
            reporte.usuario = SessionFixed.IdUsuario.ToString();
            reporte.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = reporte;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_020( cl_filtros_Info model)
        {
            ROL_020_Rpt reporte = new ROL_020_Rpt();
            #region Cargo diseño desde base
            var report = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ROL_020");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                reporte.LoadLayout(RootReporte);
            }
            #endregion
            reporte.p_IdEmpresa.Value = model.IdEmpresa;
            reporte.p_IdNomina.Value = model.IdNomina;
            reporte.p_IdNominaTipo.Value = model.IdTipoNomina;
            reporte.p_IdPeriodo.Value = model.IdPeriodo;
            reporte.p_IdSucursal.Value = model.IdSucursal;
            reporte.P_IdArea.Value = model.IdArea;
            reporte.P_IdDivision.Value = model.IdDivision;
            reporte.p_IdProceso.Value = model.IdProceso;
            cargar_combos(model.IdEmpresa);
            reporte.usuario = SessionFixed.IdUsuario.ToString();
            reporte.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = reporte;
            return View(model);
        }

        public ActionResult ROL_021(int IdEmpresa=0, int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0,  int IdPeriodo = 0, int IdSucursal=0)
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal=IdSucursal == 0 ? Convert.ToInt32(SessionFixed.IdSucursal) : IdSucursal,
                IdNomina=IdNomina_Tipo,
                IdTipoNomina =IdNomina_TipoLiqui,
                IdPeriodo=IdPeriodo,
                IdPeriodoSet = IdPeriodo,
                TipoRubro = "A"
            };
            ROL_021_Rpt report = new ROL_021_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_021");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = IdEmpresa== 0 ? Convert.ToInt32(SessionFixed.IdEmpresa)  : IdEmpresa;
            report.p_IdNomina.Value = IdNomina_Tipo;
            report.p_IdNominaTipo.Value = IdNomina_TipoLiqui;
            report.p_IdPeriodo.Value = IdPeriodo;
            report.p_IdSucursal.Value = IdSucursal;
            report.P_IdArea.Value = model.IdArea;
            report.P_IdDivision.Value = model.IdDivision;
            report.P_TipoRubro.Value = model.TipoRubro;
            
            SessionFixed.IdDivision = model.IdDivision.ToString();
            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_021(cl_filtros_Info model)
        {
            ROL_021_Rpt report = new ROL_021_Rpt();

            cargar_combos(model.IdEmpresa);
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_021");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdNomina.Value = model.IdNomina;
            report.p_IdNominaTipo.Value = model.IdTipoNomina;
            report.p_IdPeriodo.Value = model.IdPeriodo;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.P_TipoRubro.Value = model.TipoRubro;
            report.P_IdArea.Value = model.IdArea;
            report.P_IdDivision.Value = model.IdDivision;
            SessionFixed.IdDivision = model.IdDivision.ToString();
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult ROL_022(int IdEmpresa = 0, int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0, int IdPeriodo = 0, int IdSucursal = 0)
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = IdSucursal == 0 ? Convert.ToInt32(SessionFixed.IdSucursal) : IdSucursal,
                IdNomina = IdNomina_Tipo,
                IdTipoNomina = IdNomina_TipoLiqui,
                IdPeriodo = IdPeriodo,
                IdPeriodoSet = IdPeriodo
            };
            ROL_022_Rpt report = new ROL_022_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_022");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = IdEmpresa == 0 ? Convert.ToInt32(SessionFixed.IdEmpresa) : IdEmpresa;
            report.p_IdNomina.Value = IdNomina_Tipo;
            report.p_IdNominaTipo.Value = IdNomina_TipoLiqui;
            report.p_IdPeriodo.Value = IdPeriodo;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.P_IdArea.Value = model.IdArea;
            report.P_IdDivision.Value = model.IdDivision;
            SessionFixed.IdDivision = model.IdDivision.ToString();
            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_022(cl_filtros_Info model)
        {
            ROL_022_Rpt report = new ROL_022_Rpt();

            cargar_combos(model.IdEmpresa);
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_022");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdNomina.Value = model.IdNomina;
            report.p_IdNominaTipo.Value = model.IdTipoNomina;
            report.p_IdPeriodo.Value = model.IdPeriodo;
            report.p_IdSucursal.Value = model.IdSucursal;
            SessionFixed.IdDivision = model.IdDivision.ToString();
            report.P_IdArea.Value = model.IdArea;
            report.P_IdDivision.Value = model.IdDivision;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult ROL_023()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdDivision = 0,
                IdArea = 0, 
                IdDepartamento = 0
            };

            ROL_023_Rpt reportDetallado = new ROL_023_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_023");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                reportDetallado.LoadLayout(RootReporte);
            }
            #endregion
            cargar_combos(model.IdEmpresa);
            reportDetallado.p_IdEmpresa.Value = model.IdEmpresa;
            reportDetallado.p_IdSucursal.Value = model.IdSucursal;
            reportDetallado.p_IdNomina.Value = model.IdNomina;
            reportDetallado.p_IdNominaTipoLiqui.Value = model.IdTipoNomina;
            reportDetallado.p_IdPeriodo.Value = model.IdPeriodo;
            reportDetallado.p_IdDivision.Value = model.IdDivision;
            reportDetallado.p_IdArea.Value = model.IdArea;
            reportDetallado.p_IdDepartamento.Value = model.IdDepartamento;
            reportDetallado.usuario = SessionFixed.IdUsuario.ToString();
            reportDetallado.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.ReportDetallado = reportDetallado;

            ROL_023_Resumen_Rpt reportResumen = new ROL_023_Resumen_Rpt();
            #region Cargo diseño desde base
            var report = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_023");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                reportResumen.LoadLayout(RootReporte);
            }
            #endregion
            cargar_combos(model.IdEmpresa);
            reportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            reportResumen.p_IdSucursal.Value = model.IdSucursal;
            reportResumen.p_IdNomina.Value = model.IdNomina;
            reportResumen.p_IdNominaTipoLiqui.Value = model.IdTipoNomina;
            reportResumen.p_IdPeriodo.Value = model.IdPeriodo;
            reportResumen.p_IdDivision.Value = model.IdDivision;
            reportResumen.p_IdArea.Value = model.IdArea;
            reportResumen.p_IdDepartamento.Value = model.IdDepartamento;
            reportResumen.usuario = SessionFixed.IdUsuario.ToString();
            reportResumen.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.ReportResumen = reportResumen;

            return View(model);
        }

        [HttpPost]
        public ActionResult ROL_023(cl_filtros_Info model)
        {
            ROL_023_Rpt reportDetallado = new ROL_023_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_023");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                reportDetallado.LoadLayout(RootReporte);
            }
            #endregion
            cargar_combos(model.IdEmpresa);
            reportDetallado.p_IdEmpresa.Value = model.IdEmpresa;
            reportDetallado.p_IdSucursal.Value = model.IdSucursal;
            reportDetallado.p_IdNomina.Value = model.IdNomina;
            reportDetallado.p_IdNominaTipoLiqui.Value = model.IdTipoNomina;
            reportDetallado.p_IdPeriodo.Value = model.IdPeriodo;
            reportDetallado.p_IdDivision.Value = model.IdDivision;
            reportDetallado.p_IdArea.Value = model.IdArea;
            reportDetallado.p_IdDepartamento.Value = model.IdDepartamento;
            reportDetallado.usuario = SessionFixed.IdUsuario.ToString();
            reportDetallado.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.ReportDetallado = reportDetallado;

            ROL_023_Resumen_Rpt reportResumen = new ROL_023_Resumen_Rpt();
            #region Cargo diseño desde base
            var report = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_023");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                reportResumen.LoadLayout(RootReporte);
            }
            #endregion
            cargar_combos(model.IdEmpresa);
            reportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            reportResumen.p_IdSucursal.Value = model.IdSucursal;
            reportResumen.p_IdNomina.Value = model.IdNomina;
            reportResumen.p_IdNominaTipoLiqui.Value = model.IdTipoNomina;
            reportResumen.p_IdPeriodo.Value = model.IdPeriodo;
            reportResumen.p_IdDivision.Value = model.IdDivision;
            reportResumen.p_IdArea.Value = model.IdArea;
            reportResumen.p_IdDepartamento.Value = model.IdDepartamento;
            reportResumen.usuario = SessionFixed.IdUsuario.ToString();
            reportResumen.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.ReportResumen = reportResumen;

            return View(model);
        }

        public ActionResult ROL_024()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)

            };
            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            ROL_024_Rpt reporte = new ROL_024_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var report = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_024");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                reporte.LoadLayout(RootReporte);
            }
            #endregion
            reporte.p_IdEmpresa.Value = model.IdEmpresa;
            reporte.p_IdSucursal.Value = model.IdSucursal;
            reporte.p_IdNominaTipo.Value = model.IdNomina;
            reporte.p_IdNominaTipoLiqui.Value = model.IdTipoNomina;
            reporte.p_IdPeriodo.Value = model.IdPeriodo;
            reporte.usuario = SessionFixed.IdUsuario.ToString();
            reporte.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = reporte;
            return View(model);
        }
        [HttpPost]
        public ActionResult ROL_024(cl_filtros_Info model)
        {
            ROL_024_Rpt reporte = new ROL_024_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var report = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_024");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                reporte.LoadLayout(RootReporte);
            }
            #endregion
            reporte.p_IdEmpresa.Value = model.IdEmpresa;
            reporte.p_IdSucursal.Value = model.IdSucursal;
            reporte.p_IdNominaTipo.Value = model.IdNomina;
            reporte.p_IdNominaTipoLiqui.Value = model.IdTipoNomina;
            reporte.p_IdPeriodo.Value = model.IdPeriodo;
            cargar_combos(model.IdEmpresa);
            reporte.usuario = SessionFixed.IdUsuario.ToString();
            reporte.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = reporte;
            return View(model);
        }

        public ActionResult ROL_025()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdDivision = 0,
                IdArea = 0
            };

            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            ROL_025_Rpt reporte = new ROL_025_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var report = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_025");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                reporte.LoadLayout(RootReporte);
            }
            #endregion
            reporte.p_IdEmpresa.Value = model.IdEmpresa;
            reporte.p_IdSucursal.Value = model.IdSucursal;
            reporte.p_IdNomina_Tipo.Value = model.IdNomina;
            //reporte.p_IdNominaTipoLiqui.Value = model.IdTipoNomina;
            reporte.p_IdPeriodo.Value = model.IdPeriodo;
            reporte.p_IdDivision.Value = model.IdDivision;
            reporte.p_IdArea.Value = model.IdArea;
            reporte.usuario = SessionFixed.IdUsuario.ToString();

            ViewBag.Report = reporte;
            return View(model);
        }

        [HttpPost]
        public ActionResult ROL_025(cl_filtros_Info model)
        {
            cargar_combos(model.IdEmpresa);

            ROL_025_Rpt reporte = new ROL_025_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var report = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_025");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                reporte.LoadLayout(RootReporte);
            }
            #endregion
            reporte.p_IdEmpresa.Value = model.IdEmpresa;
            reporte.p_IdSucursal.Value = model.IdSucursal;
            reporte.p_IdNomina_Tipo.Value = model.IdNomina;
            //reporte.p_IdNominaTipoLiqui.Value = model.IdTipoNomina;
            reporte.p_IdPeriodo.Value = model.IdPeriodo;
            reporte.p_IdDivision.Value = model.IdDivision;
            reporte.p_IdArea.Value = model.IdArea;
            reporte.usuario = SessionFixed.IdUsuario.ToString();

            ViewBag.Report = reporte;
            return View(model);
        }

        public ActionResult ROL_026()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdDivision = 0,
                IdArea = 0,
                IdAnio = 0,
                IdEmpleado = 0
            };

            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            ROL_026_Rpt reporte = new ROL_026_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var report = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_026");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                reporte.LoadLayout(RootReporte);
            }
            #endregion
            reporte.p_IdEmpresa.Value = model.IdEmpresa;
            reporte.p_IdSucursal.Value = model.IdSucursal;
            reporte.p_IdNomina_Tipo.Value = model.IdNomina;
            reporte.p_IdAnio.Value = model.IdAnio;
            reporte.p_IdEmpleado.Value = model.IdEmpleado;
            //reporte.usuario = SessionFixed.IdUsuario.ToString();

            ViewBag.Report = reporte;
            return View(model);
        }

        [HttpPost]
        public ActionResult ROL_026(cl_filtros_Info model)
        {
            cargar_combos(model.IdEmpresa);

            ROL_026_Rpt reporte = new ROL_026_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var report = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_026");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                reporte.LoadLayout(RootReporte);
            }
            #endregion
            reporte.p_IdEmpresa.Value = model.IdEmpresa;
            reporte.p_IdSucursal.Value = model.IdSucursal;
            reporte.p_IdNomina_Tipo.Value = model.IdNomina;
            reporte.p_IdAnio.Value = model.IdAnio;
            reporte.p_IdEmpleado.Value = model.IdEmpleado;
            //reporte.usuario = SessionFixed.IdUsuario.ToString();

            ViewBag.Report = reporte;
            return View(model);
        }


        public ActionResult ROL_027()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combos(model.IdEmpresa);
            ROL_027_Rpt report = new ROL_027_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_027");
            if (reporte!= null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdNomina_Tipo.Value = model.IdNomina;
            report.p_IdDivision.Value = model.IdDivision;
            report.p_IdArea.Value = model.IdArea;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }

        [HttpPost]
        public ActionResult ROL_027(cl_filtros_Info model)
        {
            ROL_027_Rpt report = new ROL_027_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_027");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdNomina_Tipo.Value = model.IdNomina;
            report.p_IdDivision.Value = model.IdDivision;
            report.p_IdArea.Value = model.IdArea;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            cargar_combos(model.IdEmpresa);
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult ROL_028(int IdEmpresa = 0, int Id_Rdep = 0)
        {
            ROL_028_Rpt model = new ROL_028_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ROL_028");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_Id_Rdep.Value = Id_Rdep;

            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();

            return View(model);
        }
    }

    public class preliquidacion_List
    {
        string Variable = "ROL_005_Info";
        public List<ROL_005_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ROL_005_Info> list = new List<ROL_005_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ROL_005_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ROL_005_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}