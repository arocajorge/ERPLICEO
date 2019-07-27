using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ProyeccionDeGastosController : Controller
    {
        #region variable
        ro_empleado_proyeccion_gastos_Bus bus_proyeccion = new ro_empleado_proyeccion_gastos_Bus();
        ro_empleado_proyeccion_gastos_det_Bus bus_det = new ro_empleado_proyeccion_gastos_det_Bus();
        ro_empleado_proyeccion_gastos_det_Info_lis ro_empleado_proyeccion_gastos_det_Info_lis = new ro_empleado_proyeccion_gastos_det_Info_lis();
        #endregion

        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_proyeccion()
        {
            ro_empleado_proyeccion_gastos_Info model = new ro_empleado_proyeccion_gastos_Info();
            return PartialView("_CmbEmpleado_proyeccion", model);
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
        #region vistas
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_proyeccion_gastos()
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                List<ro_empleado_proyeccion_gastos_Info> model = bus_proyeccion.get_list(IdEmpresa);
                return PartialView("_GridViewPartial_proyeccion_gastos", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_proyeccion_gastos_det(int IdEmpresa, decimal IdTransaccion)
        {
            try
            {
                cargar_combo();
                ro_empleado_proyeccion_gastos_Info model = new ro_empleado_proyeccion_gastos_Info();
                SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
                model.list_proyeciones = ro_empleado_proyeccion_gastos_det_Info_lis.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                return PartialView("_GridViewPartial_proyeccion_gastos_det", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region acciones
        [HttpPost]
        public ActionResult Nuevo(ro_empleado_proyeccion_gastos_Info info)
        {
            try
            {
                ro_tipo_gastos_personales_maxim_x_anio_Bus bus_detalle_techo_x_anio = new ro_tipo_gastos_personales_maxim_x_anio_Bus();

                if (ModelState.IsValid)
                {
                    info.list_proyeciones = ro_empleado_proyeccion_gastos_det_Info_lis.get_list(info.IdTransaccionSession);

                    var gastos = bus_detalle_techo_x_anio.get_list_gastos_tope_x_anio(info.AnioFiscal);

                    if(gastos==null)
                    {
                        cargar_combo();
                        ViewBag.mensaje = "No existen valores maximo registrado para el periodo fiscal";
                        return View(info);
                    }
                    else
                    {
                        if(gastos.Count()==0)
                        {
                            cargar_combo();
                            ViewBag.mensaje = "No existen valores maximo registrado para el periodo fiscal";
                            return View(info);
                        }
                    }
                    foreach (var item in info.list_proyeciones)
                    {

                        if (gastos.Where(v => v.AnioFiscal == info.AnioFiscal && v.IdTipoGasto == item.IdTipoGasto && v.Monto_max < item.Valor).Count() > 0)

                        {
                            cargar_combo();
                            ViewBag.mensaje = "El tipo de gasto "+item.IdTipoGasto+" supera el valor maximo deducible";
                            return View(info);
                        }
                        }
                    int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                    info.IdEmpresa = IdEmpresa;
                    if (!bus_proyeccion.guardarDB(info))
                    {
                        cargar_combo();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo()
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion
                ro_empleado_proyeccion_gastos_Info info = new ro_empleado_proyeccion_gastos_Info
                {
                    AnioFiscal = DateTime.Now.Year,
                    IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
                };
                cargar_combo();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Modificar(ro_empleado_proyeccion_gastos_Info info)
        {
            try
            {
                ro_tipo_gastos_personales_maxim_x_anio_Bus bus_detalle_techo_x_anio = new ro_tipo_gastos_personales_maxim_x_anio_Bus();

                if (ModelState.IsValid)
                {
                    var gastos = bus_detalle_techo_x_anio.get_list_gastos_tope_x_anio(info.AnioFiscal);

                    if (gastos == null)
                    {
                        cargar_combo();
                        ViewBag.mensaje = "No existen valores maximo registrado para el periodo fiscal";
                        return View(info);
                    }
                    else
                    {
                        if (gastos.Count() == 0)
                        {
                            cargar_combo();
                            ViewBag.mensaje = "No existen valores maximo registrado para el periodo fiscal";
                            return View(info);
                        }
                    }
                    info.list_proyeciones = ro_empleado_proyeccion_gastos_det_Info_lis.get_list(info.IdTransaccionSession);

                    foreach (var item in info.list_proyeciones)
                    {
                        gastos = bus_detalle_techo_x_anio.get_list_gastos_tope_x_anio(info.AnioFiscal);

                        if (gastos.Where(v => v.AnioFiscal == info.AnioFiscal && v.IdTipoGasto == item.IdTipoGasto && v.Monto_max < item.Valor).Count() > 0)

                        {
                            cargar_combo();
                            ViewBag.mensaje = "El tipo de gasto " + item.IdTipoGasto + " supera el valor maximo deducible";
                            return View(info);
                        }
                    }
                    int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                    info.IdEmpresa = IdEmpresa;
                    if (!bus_proyeccion.modificarDB(info))
                    {
                        cargar_combo();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Modificar(decimal IdEmpleado=0, int IdTransaccion = 0)
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion
                
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ro_empleado_proyeccion_gastos_Info model = bus_proyeccion.get_info(IdEmpresa, IdEmpleado, IdTransaccion);
                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
               model.list_proyeciones= bus_det.get_list(IdEmpresa, model.IdTransaccion);
                ro_empleado_proyeccion_gastos_det_Info_lis.set_list(model.list_proyeciones, Convert.ToDecimal(model.IdTransaccionSession));
                model.IdTransaccion = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Anular(ro_empleado_proyeccion_gastos_Info info)
        {
            try
            {

                if (!bus_proyeccion.anularDB(info))
                {
                    cargar_combo();
                    return View(info);
                }
                else
                    return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(decimal IdEmpleado=0, int IdTransaccion = 0)
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion

                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ro_empleado_proyeccion_gastos_Info model = bus_proyeccion.get_info(IdEmpresa, IdEmpleado, IdTransaccion);
                model.list_proyeciones = bus_det.get_list(IdEmpresa, model.IdTransaccion);
                ro_empleado_proyeccion_gastos_det_Info_lis.set_list(model.list_proyeciones, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                model.IdTransaccion = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region cargar_combos
        private void cargar_combo()
        {
            try
            {
                ro_tipo_gastos_personales_Bus bus_tipo_gasto = new ro_tipo_gastos_personales_Bus();
                var list_tipo_gasto = bus_tipo_gasto.get_list(false);
                ViewBag.list_tipo_gasto = list_tipo_gasto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region funciones del detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ro_empleado_proyeccion_gastos_det_Info info_det)
        {
            ro_empleado_proyeccion_gastos_Info model = new ro_empleado_proyeccion_gastos_Info();
            if (ModelState.IsValid)
            {
                var lista_tmp = ro_empleado_proyeccion_gastos_det_Info_lis.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                if(lista_tmp.Where(v=>v.IdTipoGasto==info_det.IdTipoGasto).Count()==0)
                ro_empleado_proyeccion_gastos_det_Info_lis.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                model.list_proyeciones = ro_empleado_proyeccion_gastos_det_Info_lis.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            cargar_combo();
            return PartialView("_GridViewPartial_proyeccion_gastos_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_empleado_proyeccion_gastos_det_Info info_det)
        {
            if (ModelState.IsValid)
                ro_empleado_proyeccion_gastos_det_Info_lis.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_empleado_proyeccion_gastos_Info model = new ro_empleado_proyeccion_gastos_Info();
            model.list_proyeciones = ro_empleado_proyeccion_gastos_det_Info_lis.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combo();
            return PartialView("_GridViewPartial_proyeccion_gastos_det", model);
        }

        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_empleado_proyeccion_gastos_det_Info info_det)
        {
            ro_empleado_proyeccion_gastos_det_Info_lis.DeleteRow(info_det.Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_empleado_proyeccion_gastos_Info model = new ro_empleado_proyeccion_gastos_Info();
            model.list_proyeciones = ro_empleado_proyeccion_gastos_det_Info_lis.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combo();
            return PartialView("_GridViewPartial_proyeccion_gastos_det", model);
        }
        #endregion
    }


    public class ro_empleado_proyeccion_gastos_det_Info_lis
    {
        string variable = "ro_empleado_proyeccion_gastos_det_Info";
        public List<ro_empleado_proyeccion_gastos_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable+ IdTransaccionSession.ToString()] == null)
            {
                List<ro_empleado_proyeccion_gastos_det_Info> list = new List<ro_empleado_proyeccion_gastos_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_empleado_proyeccion_gastos_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_empleado_proyeccion_gastos_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable+ IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ro_empleado_proyeccion_gastos_det_Info info_det, decimal IdTransaccionSession)
        {
            List<ro_empleado_proyeccion_gastos_det_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            list.Add(info_det);
        }

        public void UpdateRow(ro_empleado_proyeccion_gastos_det_Info info_det, decimal IdTransaccionSession)
        {
            ro_empleado_proyeccion_gastos_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.Valor = info_det.Valor;
            edited_info.IdTipoGasto = info_det.IdTipoGasto;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ro_empleado_proyeccion_gastos_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}