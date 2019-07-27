using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using DevExpress.Web.Mvc;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Areas.Reportes.Controllers;
using Core.Erp.Info.Reportes.RRHH;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class PreLiquidacionController : Controller
    {
        #region variables
        ro_Acta_Finiquito_Bus bus_acta_finiquito = new ro_Acta_Finiquito_Bus();
        ro_Acta_Finiquito_Detalle_Bus bus_detalle = new ro_Acta_Finiquito_Detalle_Bus();
        ro_Acta_Finiquito_Detalle_lst lst_detalle = new ro_Acta_Finiquito_Detalle_lst();
        preliquidacion_List lst_rol_005 = new preliquidacion_List();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_contrato_Bus bus_contrato = new ro_contrato_Bus();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        ro_cargo_Bus bus_cargo = new ro_cargo_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ro_Acta_Finiquito_Info info = new ro_Acta_Finiquito_Info();
        int IdEmpresa = 0;
        #endregion

        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();

        public tb_persona_Bus Bus_persona
        {
            get
            {
                return bus_persona;
            }

            set
            {
                bus_persona = value;
            }
        }

        public ActionResult CmbEmpleado_acta()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_acta", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return Bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return Bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        #endregion
        public ActionResult Index()
        {
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info
            {
                IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdCausaTerminacion = "CTL_02"

            };
            model.lst_detalle = new List<ro_Acta_Finiquito_Detalle_Info>();
            lst_detalle.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos();
            cargar_combos_detalle();
            return View(model);
        }

        #region JSON
        public JsonResult capturar_info_pantalla(DateTime? FechaIngreso, DateTime? FechaSalida, int IdEmpleado=0, string IdContrato_Tipo="", float UltimaRemuneracion=0,
            bool EsMujerEmbarazada= false, bool EsPorDiscapacidad=false, bool EsDirigenteSindical= false, bool EsPorEnfermedadNoProfesional=false, 
            string IdCausaTerminacion="", string Observacion="", decimal IdTransaccionSession=0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            var info_contrato = bus_contrato.get_info_contrato_empleado(IdEmpresa, IdEmpleado);            
            var info_empleado = bus_empleado.get_info(IdEmpresa, IdEmpleado);
            var info_sucursal = bus_sucursal.get_info(IdEmpresa, info_empleado.IdSucursal);
            var info_cargo = bus_cargo.get_info(IdEmpresa, Convert.ToInt32(info_empleado.IdCargo));
            var info_terminacion = bus_catalogo.get_info(IdEmpresa, IdCausaTerminacion);

            var lst_detalle_pantalla = lst_detalle.get_list(IdTransaccionSession);

            List<ROL_005_Info> lista_rpte = new List<ROL_005_Info>();

            foreach (var item in lst_detalle_pantalla)
            {
                ROL_005_Info info_reporte = new ROL_005_Info();
                var info_rubro = bus_rubro.get_info(IdEmpresa, item.IdRubro);

                info_reporte.IdEmpresa = IdEmpresa;
                info_reporte.IdActaFiniquito = 0;
                info_reporte.IdEmpleado = IdEmpleado;
                info_reporte.NombreCompleto = info_empleado.pe_apellido+" "+ info_empleado.pe_nombre;
                info_reporte.pe_cedulaRuc = info_empleado.pe_cedulaRuc;
                info_reporte.ca_descripcion = info_cargo.ca_descripcion;
                info_reporte.UltimaRemuneracion = UltimaRemuneracion;
                info_reporte.IdCausaTerminacion = IdCausaTerminacion;
                info_reporte.TipoTerminacion = info_terminacion.ca_descripcion;
                info_reporte.IdContrato = info_contrato.IdContrato;
                info_reporte.FechaIngreso = Convert.ToDateTime(FechaIngreso);
                info_reporte.FechaSalida = Convert.ToDateTime(FechaSalida);
                info_reporte.Observacion = Observacion;
                info_reporte.Su_Descripcion = info_sucursal.Su_Descripcion;
                info_reporte.EsMujerEmbarazada = EsMujerEmbarazada;
                info_reporte.EsDirigenteSindical = EsDirigenteSindical;
                info_reporte.EsPorDiscapacidad = EsPorDiscapacidad;
                info_reporte.EsPorEnfermedadNoProfesional = EsPorEnfermedadNoProfesional;               
                info_reporte.ru_descripcion = info_rubro.ru_descripcion;
                info_reporte.liquido= item.Valor;

                if (info_rubro.ru_tipo == "I")
                {
                    info_reporte.Ingresos = item.Valor;
                    info_reporte.Egresos = 0;
                }
                else
                {
                    info_reporte.Ingresos = 0;
                    info_reporte.Egresos = item.Valor;
                }

                info_reporte.DescripcionDetalle = item.Observacion;
                
                lista_rpte.Add(info_reporte);                
            }

            lst_rol_005.set_list(lista_rpte, IdTransaccionSession);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
        /*
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);

        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_liquidacion_empleado(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) : Convert.ToDateTime(Fecha_fin);

            List<ro_Acta_Finiquito_Info> model = bus_acta_finiquito.get_list_pre_liquidacion(IdEmpresa);
            return PartialView("_GridViewPartial_liquidacion_empleado", model);
        }
        public ActionResult Nuevo()
        {
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info
            {
                IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]),
                IdCausaTerminacion = "CTL_02"

            };
            model.lst_detalle = new List<ro_Acta_Finiquito_Detalle_Info>();
            lst_detalle.set_list(model.lst_detalle);
            cargar_combos();
            cargar_combos_detalle();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list();
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la novedad";
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_acta_finiquito.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(decimal IdActaFiniquito)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_Acta_Finiquito_Info model = bus_acta_finiquito.get_info(IdEmpresa, IdActaFiniquito);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdActaFiniquito);
            lst_detalle.set_list(model.lst_detalle);
            cargar_combos_detalle();
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list();
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la planificación";
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_acta_finiquito.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }
        public ActionResult Anular(decimal IdActaFiniquito)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_Acta_Finiquito_Info model = bus_acta_finiquito.get_info(IdEmpresa, IdActaFiniquito);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdActaFiniquito);
            lst_detalle.set_list(model.lst_detalle);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list();

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_acta_finiquito.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        */
        [ValidateInput(false)]
        public ActionResult GridViewPartial_liquidacion_empleado_det()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info();
            model.lst_detalle = lst_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            //if (model.lst_detalle.Count == 0)
            //    model.lst_detalle = lst_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        private void cargar_combos()
        {
            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ViewBag.lst_empleado = bus_empleado.get_list_combo_liquidar(IdEmpresa);
            ViewBag.lst_tipo_contrato = bus_catalogo.get_list_x_tipo(2);
            ViewBag.lst_tipo_terminacion = bus_catalogo.get_list_x_tipo(24);

        }
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ViewBag.lst_rubro = bus_rubro.get_list(IdEmpresa, false);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Acta_Finiquito_Detalle_Info info_det)
        {
            info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (info_det != null)
            {
                if (info_det.IdRubro != "")
                {
                    ro_rubro_tipo_Info info_rubro = bus_rubro.get_info(info.IdEmpresa, info_det.IdRubro);
                    if (info_rubro != null)
                    {
                        if (info_rubro.ru_tipo == "E")
                            info_det.Valor = info_det.Valor * -1;
                    }
                }
            }

            if (ModelState.IsValid)
                lst_detalle.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info();
            model.lst_detalle = lst_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Acta_Finiquito_Detalle_Info info_det)
        {
            info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (info_det != null)
            {
                if (info_det.IdRubro != "")
                {
                    ro_rubro_tipo_Info info_rubro = bus_rubro.get_info(info.IdEmpresa, info_det.IdRubro);
                    if (info_rubro != null)
                    {
                        if (info_rubro.ru_tipo == "E")
                            info_det.Valor = info_det.Valor * -1;
                    }
                }
            }

            if (ModelState.IsValid)
                lst_detalle.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info();
            model.lst_detalle = lst_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Acta_Finiquito_Detalle_Info info_det)
        {
            lst_detalle.DeleteRow(info_det.IdSecuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info();
            model.lst_detalle = lst_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        public ActionResult Procesar(DateTime? FechaIngreso, DateTime? FechaSalida, decimal IdEmpleado = 0,
        double UltimaRemuneracion = 0, string idterminacion = "", bool EsMujerEmbarazada = false, bool EsDirigenteSindical = false,
        bool EsPorDiscapacidad = false, bool EsPorEnfermedadNoProfesional = false)
        {
            if (FechaIngreso == null)
                FechaIngreso = DateTime.Now;
            if (FechaSalida == null)
                FechaSalida = DateTime.Now;

            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"].ToString());
            info.IdEmpleado = IdEmpleado;
            info.IdEmpresa = IdEmpresa;
            info.UltimaRemuneracion = UltimaRemuneracion;
            info.FechaIngreso = Convert.ToDateTime(FechaIngreso);
            info.FechaSalida = Convert.ToDateTime(FechaSalida);
            info.IdCausaTerminacion = idterminacion;
            info.EsMujerEmbarazada = EsMujerEmbarazada;
            info.EsDirigenteSindical = EsDirigenteSindical;
            info.EsPorDiscapacidad = EsPorDiscapacidad;
            info.EsPorEnfermedadNoProfesional = EsPorEnfermedadNoProfesional;

            info = bus_acta_finiquito.ObtenerIndemnizacion(info);

            lst_detalle.set_list(info.lst_detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return Json("", JsonRequestBehavior.AllowGet);
        }

        /*
        public ActionResult Liquidar(decimal IdActaFiniquito)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_Acta_Finiquito_Info model = bus_acta_finiquito.get_info(IdEmpresa, IdActaFiniquito);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdActaFiniquito);
            lst_detalle.set_list(model.lst_detalle);
            cargar_combos_detalle();
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Liquidar(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list();
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la planificación";
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuarioUltMod = Session["IdUsuario"].ToString();
            if (!bus_acta_finiquito.Liquidar(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }*/

    }


}


