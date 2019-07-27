using Core.Erp.Bus.ActivoFijo;
using Core.Erp.Bus.General;
using Core.Erp.Info.ActivoFijo;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using Core.Erp.Web.Reportes.ActivoFijo;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Reportes.Controllers
{
    [SessionTimeout]
    public class ActivoFijoReportesController : Controller
    {
        Af_Activo_fijo_Bus bus_activo = new Af_Activo_fijo_Bus();
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Facturacion.repx";

        public ActionResult CmbActivo_fijo()
        {
            int model = new int();
            return PartialView("_CmbActivo_fijo", model);
        }

        public List<Af_Activo_fijo_Info> get_list_bajo_demanda_af(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_activo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }

        public Af_Activo_fijo_Info get_info_bajo_demanda_af(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_activo.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }
        #region Metodos ComboBox bajo demanda empleado
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_AF1()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            return PartialView("_CmbEmpleado_AF1", model);
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

        #region Metodos ComboBox bajo demanda empleado
        public ActionResult CmbEmpleado_AF2()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            return PartialView("_CmbEmpleado_AF2", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_cust(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_cust(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        #endregion


        #region Json
        public JsonResult cargar_categoria(int IdEmpresa = 0 , int IdActivoFijoTipo = 0)
        {
            Af_Activo_fijo_Categoria_Bus bus_categoria = new Af_Activo_fijo_Categoria_Bus();
            var resultado = bus_categoria.get_list(IdEmpresa, IdActivoFijoTipo, false);
            resultado.Add(new Af_Activo_fijo_Categoria_Info
            {
                IdEmpresa = IdEmpresa,
                IdCategoriaAF = 0,
                Descripcion = "Todos"
            });
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult ACTF_001(decimal Id_Mejora_Baja_Activo = 0, string Id_Tipo = "" )
        {
            ACTF_001_Rpt model = new ACTF_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ACTF_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion

            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_Id_Mejora_Baja_Activo.Value = Id_Mejora_Baja_Activo;
            model.p_Id_Tipo.Value = Id_Tipo;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult ACTF_002(decimal IdVtaActivo = 0)
        {
            ACTF_002_Rpt model = new ACTF_002_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ACTF_002");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdVtaActivo.Value = IdVtaActivo;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult ACTF_003(decimal IdRetiroActivo = 0)
        {
            ACTF_003_Rpt model = new ACTF_003_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ACTF_003");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdRetiroActivo.Value = IdRetiroActivo;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);

        }
        public ActionResult ACTF_004()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                Estado_Proceso = ""
            };
  
            if (model.mostrar_agrupado)
            {
                ACTF_004_detalle_Rpt model_detalle = new ACTF_004_detalle_Rpt();
                model_detalle.p_IdEmpresa.Value = model.IdEmpresa;
                model_detalle.p_IdActivoFijoTipo.Value = model.IdActivoFijoTipo;
                model_detalle.p_IdCategoriaAF.Value = model.IdCategoriaAF;
                model_detalle.p_fecha_corte.Value = model.fecha_fin;
                model_detalle.p_Estado_Proceso.Value = model.Estado_Proceso;
                model_detalle.p_IdUsuario.Value = SessionFixed.IdUsuario;
                cargar_combos(model);

                model_detalle.usuario = SessionFixed.IdUsuario;
                model_detalle.empresa = SessionFixed.NomEmpresa;
                ViewBag.report = model_detalle;
           
            }

            else
            {
                ACTF_004_resumen_Rpt model_resumen = new ACTF_004_resumen_Rpt();
                model_resumen.p_IdEmpresa.Value = model.IdEmpresa;
                model_resumen.p_IdActivoFijoTipo.Value = model.IdActivoFijoTipo;
                model_resumen.p_IdCategoriaAF.Value = model.IdCategoriaAF;
                model_resumen.p_fecha_corte.Value = model.fecha_fin;
                model_resumen.p_Estado_Proceso.Value = model.Estado_Proceso;
                model_resumen.p_IdUsuario.Value = SessionFixed.IdUsuario;
                cargar_combos(model);

                model_resumen.usuario = SessionFixed.IdUsuario;
                model_resumen.empresa = SessionFixed.NomEmpresa;
                ViewBag.report = model_resumen;
            }
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult ACTF_004(cl_filtros_Info model)
        {

            if (model.mostrar_agrupado)
            {
                ACTF_004_detalle_Rpt report = new ACTF_004_detalle_Rpt();
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdActivoFijoTipo.Value = model.IdActivoFijoTipo;
                report.p_IdCategoriaAF.Value = model.IdCategoriaAF;
                report.p_Estado_Proceso.Value = model.Estado_Proceso;
                report.p_IdUsuario.Value = model.IdUsuario;
                report.p_fecha_corte.Value = model.fecha_fin;
                report.usuario = SessionFixed.IdUsuario.ToString();
                report.empresa = SessionFixed.NomEmpresa.ToString();
                cargar_combos(model);
                ViewBag.Report = report;
            }
            else
            {
                ACTF_004_resumen_Rpt report = new ACTF_004_resumen_Rpt();
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdActivoFijoTipo.Value = model.IdActivoFijoTipo;
                report.p_IdCategoriaAF.Value = model.IdCategoriaAF;
                report.p_Estado_Proceso.Value = model.Estado_Proceso;
                report.p_IdUsuario.Value = model.IdUsuario;
                report.p_fecha_corte.Value = model.fecha_fin;
                report.usuario = SessionFixed.IdUsuario.ToString();
                report.empresa = SessionFixed.NomEmpresa.ToString();
                cargar_combos(model);
                ViewBag.Report = report;
            }
            return View(model);
        }
        private void cargar_combos(cl_filtros_Info model)
        {

            Af_Activo_fijo_Categoria_Bus bus_categoria = new Af_Activo_fijo_Categoria_Bus();
            var lst_categoria = bus_categoria.get_list(model.IdEmpresa, model.IdActivoFijoTipo, false);
            lst_categoria.Add(new Af_Activo_fijo_Categoria_Info
            {
                IdEmpresa = model.IdEmpresa,
                IdCategoriaAF = 0,
                Descripcion = "Todos"
            });
            ViewBag.lst_categoria = lst_categoria;

            Af_Catalogo_Bus bus_catalogo = new Af_Catalogo_Bus();
            var lst_estado = bus_catalogo.get_list(Convert.ToString(cl_enumeradores.eTipoCatalogoAF.TIP_ESTADO_AF), false);
            lst_estado.Add(new Af_Catalogo_Info
            {
                IdCatalogo = "",
                Descripcion = "Todos"
            });
            ViewBag.lst_estado = lst_estado;
            
            Af_Activo_fijo_tipo_Bus bus_activo = new Af_Activo_fijo_tipo_Bus();
            var lst_activo = bus_activo.get_list(model.IdEmpresa, false);
            lst_activo.Add(new Af_Activo_fijo_tipo_Info
            {
                IdEmpresa = model.IdEmpresa,
                IdActivoFijoTipo = 0,
                Af_Descripcion = "Todos"
            });
            ViewBag.lst_activo = lst_activo;

            Af_Activo_fijo_Bus bus_act = new Af_Activo_fijo_Bus();
            var lst_act = bus_act.get_list(model.IdEmpresa, false);
            lst_act.Add(new Af_Activo_fijo_Info
            {
                IdEmpresa = model.IdEmpresa,
                IdActivoFijo = 0,
                Af_Nombre = "Todos"
            });
            ViewBag.lst_act = lst_act;

            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal= bus_sucursal.get_list(model.IdEmpresa, false);
            lst_sucursal.Add(new tb_sucursal_Info
            {
                IdEmpresa = model.IdEmpresa,
                IdSucursal = 0,
                Su_Descripcion = "Todas"
            });
            ViewBag.lst_sucursal = lst_sucursal;

            Af_Departamento_Bus bus_dep = new Af_Departamento_Bus();
            var lst_dep = bus_dep.GetList(model.IdEmpresa,model.IdAreaAF, false);
            lst_dep.Add(new Af_Departamento_Info
            {
                IdEmpresa = model.IdEmpresa,
                IdDepartamento = 0,
                Descripcion = "Todos"
            });
            ViewBag.lst_dep = lst_dep;
        }
        public ActionResult ACTF_005()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                Estado_Proceso = ""

            };
            ACTF_005_Rpt report = new ACTF_005_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACTF_005");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdActivoFijoTipo.Value = model.IdActivoFijoTipo;
            report.p_IdCategoriaAF.Value = model.IdCategoriaAF;
            report.p_fecha_corte.Value = model.fecha_fin;
            report.p_Estado_Proceso.Value = model.Estado_Proceso;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult ACTF_005(cl_filtros_Info model)
        {
            ACTF_005_Rpt report = new ACTF_005_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACTF_005");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdActivoFijoTipo.Value = model.IdActivoFijoTipo;
            report.p_IdCategoriaAF.Value = model.IdCategoriaAF;
            report.p_Estado_Proceso.Value = model.Estado_Proceso;
            report.p_fecha_corte.Value = model.fecha_fin;
            cargar_combos(model);
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult ACTF_006()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdActivoFijo = 0
            };
            ACTF_006_Rpt report = new ACTF_006_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ACTF_006");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdActivoFijo.Value = model.IdActivoFijo;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ACTF_006(cl_filtros_Info model)
        {
            ACTF_006_Rpt report = new ACTF_006_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ACTF_006");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdActivoFijo.Value = model.IdActivoFijo;
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult ACTF_007(int IdActivoFijo = 0)
        {
            ACTF_007_Rpt model = new ACTF_007_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ACTF_007");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion

            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdActivoFijo.Value = IdActivoFijo;
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);

        }
        
        public ActionResult ACTF_008()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdDepartamento = 0,
                IdEmpleado =0
            };
            cargar_combos(model);
            ACTF_008_Rpt report = new ACTF_008_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACTF_008");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdDepartamento.Value = model.IdDepartamento;
            report.p_IdEmpleadoCustodio.Value = model.IdEmpleado;
            report.p_IdEmpleadoEncargado.Value = model.IdEmpleado;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult ACTF_008(cl_filtros_Info model)
        {
            ACTF_008_Rpt report = new ACTF_008_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACTF_008");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdDepartamento.Value = model.IdDepartamento;
            report.p_IdEmpleadoCustodio.Value = model.IdEmpleado;
            report.p_IdEmpleadoEncargado.Value = model.IdEmpleado;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            cargar_combos(model);
            ViewBag.Report = report;
            return View(model);
        }
    }
}