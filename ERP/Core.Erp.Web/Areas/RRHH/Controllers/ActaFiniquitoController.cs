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

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ActaFiniquitoController : Controller
    {
        #region variables
        ro_Acta_Finiquito_Bus bus_acta_finiquito = new ro_Acta_Finiquito_Bus();
        ro_Acta_Finiquito_Detalle_Bus bus_detalle = new ro_Acta_Finiquito_Detalle_Bus();
        ro_Acta_Finiquito_Detalle_lst lst_detalle = new ro_Acta_Finiquito_Detalle_lst();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
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
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                fecha_ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                fecha_fin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1)
            };
            return View(model);
        }
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
            var  model = bus_acta_finiquito.get_list(IdEmpresa);
            return PartialView("_GridViewPartial_liquidacion_empleado", model);
        }
        private void cargar_combos()
        {
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.lst_empleado = bus_empleado.get_list_combo_liquidar(IdEmpresa);
            ViewBag.lst_tipo_contrato = bus_catalogo.get_list_x_tipo(2);
            ViewBag.lst_tipo_terminacion = bus_catalogo.get_list_x_tipo(24);

        }
        public ActionResult Nuevo()
        {
            ro_Acta_Finiquito_Info model = new ro_Acta_Finiquito_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdCausaTerminacion = "CTL_02",
                lst_detalle = new List<ro_Acta_Finiquito_Detalle_Info>()

            };
            lst_detalle.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list(model.IdTransaccionSession);
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la novedad";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_acta_finiquito.guardarDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(decimal IdActaFiniquito)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_Acta_Finiquito_Info model = bus_acta_finiquito.get_info(IdEmpresa, IdActaFiniquito);
            if (model == null)
                return RedirectToAction("Index");

            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdActaFiniquito);

            lst_detalle.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos_detalle();
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list(model.IdTransaccionSession);
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
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }
        public ActionResult Anular( decimal IdActaFiniquito)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_Acta_Finiquito_Info model = bus_acta_finiquito.get_info(IdEmpresa, IdActaFiniquito);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdActaFiniquito);
            lst_detalle.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list(model.IdTransaccionSession);

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_acta_finiquito.anularDB(model))
            {
                cargar_combos();
                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                model.lst_detalle = bus_detalle.get_list(model.IdEmpresa, model.IdActaFiniquito);
                lst_detalle.set_list(model.lst_detalle, model.IdTransaccionSession);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_liquidacion_empleado_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = lst_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
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
                        if(info_rubro.ru_tipo == "E")
                            info_det.Valor = info_det.Valor * -1;
                    }
                }
            }   

            if (ModelState.IsValid)
                lst_detalle.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            
            var model = lst_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
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
            
            var model = lst_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Acta_Finiquito_Detalle_Info info_det)
        {
            lst_detalle.DeleteRow(info_det.IdSecuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            
            var model = lst_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_empleado_det", model);
        }
        public ActionResult Procesar(DateTime? FechaIngreso , DateTime? FechaSalida, decimal IdEmpleado=0, 
        double UltimaRemuneracion=0,  string idterminacion="", bool EsMujerEmbarazada=false, bool EsDirigenteSindical = false,
        bool EsPorDiscapacidad = false,bool EsPorEnfermedadNoProfesional=false)
        {
            if (FechaIngreso == null)
                FechaIngreso = DateTime.Now;
            if (FechaSalida == null)
                FechaSalida = DateTime.Now;

            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            info.IdEmpleado = IdEmpleado;
            info.IdEmpresa = IdEmpresa;
            info.UltimaRemuneracion = UltimaRemuneracion;
            info.FechaIngreso =Convert.ToDateTime( FechaIngreso);
            info.FechaSalida =Convert.ToDateTime( FechaSalida);
            info.IdCausaTerminacion = idterminacion;
            info.EsMujerEmbarazada = EsMujerEmbarazada;
            info.EsDirigenteSindical = EsDirigenteSindical;
            info.EsPorDiscapacidad = EsPorDiscapacidad;
            info.EsPorEnfermedadNoProfesional = EsPorEnfermedadNoProfesional;

            info = bus_acta_finiquito.ObtenerIndemnizacion(info);

            lst_detalle.set_list(info.lst_detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return Json(info.lst_detalle, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult Liquidar( decimal IdActaFiniquito)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_Acta_Finiquito_Info model = bus_acta_finiquito.get_info(IdEmpresa, IdActaFiniquito);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdActaFiniquito);
            lst_detalle.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos_detalle();
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Liquidar(ro_Acta_Finiquito_Info model)
        {
            model.lst_detalle = lst_detalle.get_list(model.IdTransaccionSession);
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la planificación";
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_acta_finiquito.Liquidar(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }

    }


}

    public class ro_Acta_Finiquito_Detalle_lst
    {
        string Variable = "ro_Acta_Finiquito_Detalle_Info";
        public List<ro_Acta_Finiquito_Detalle_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_Acta_Finiquito_Detalle_Info> list = new List<ro_Acta_Finiquito_Detalle_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
        return (List<ro_Acta_Finiquito_Detalle_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
    }

        public void set_list(List<ro_Acta_Finiquito_Detalle_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ro_Acta_Finiquito_Detalle_Info info_det, decimal IdTransaccionSession)
        {
            List<ro_Acta_Finiquito_Detalle_Info> list = get_list(IdTransaccionSession);
            info_det.IdSecuencia = list.Count == 0 ? 1 : list.Max(q => q.IdSecuencia) + 1;
            list.Add(info_det);
        }

        public void UpdateRow(ro_Acta_Finiquito_Detalle_Info info_det, decimal IdTransaccionSession)
        {
            ro_Acta_Finiquito_Detalle_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdSecuencia == info_det.IdSecuencia).First();
            edited_info.IdActaFiniquito = info_det.IdActaFiniquito;
            edited_info.IdRubro = info_det.IdRubro;
            edited_info.Valor = info_det.Valor;
            edited_info.Observacion = info_det.Observacion;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ro_Acta_Finiquito_Detalle_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.IdSecuencia == Secuencia).First());
        }
    }



