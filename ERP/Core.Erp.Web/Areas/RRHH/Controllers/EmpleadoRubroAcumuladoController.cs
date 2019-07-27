using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class EmpleadoRubroAcumuladoController : Controller
    {
        #region variables
        ro_empleado_x_rubro_acumulado_Bus bus_rubro_acumulados = new ro_empleado_x_rubro_acumulado_Bus();
        ro_empleado_x_rubro_acumulado_detalle_Bus bus_rubro_acumulados_detalle = new ro_empleado_x_rubro_acumulado_detalle_Bus();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_jornada_Bus bus_jornada = new ro_jornada_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_empleado_x_rubro_acumulado_detalle_List ListaDetalle = new ro_empleado_x_rubro_acumulado_detalle_List();
        #endregion

        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_rubros_acumulados()
        {
            ro_rubro_acumulados_Info model = new ro_rubro_acumulados_Info();
            return PartialView("_CmbEmpleado_rubros_acumulados", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }


        public ActionResult CmbRubro()
        {
            decimal model = new decimal();
            return PartialView("_CmbRubro", model);
        }
        public List<ro_rubro_tipo_Info> get_list_bajo_demanda_rubro(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_rubro.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ro_rubro_tipo_Info get_info_bajo_demanda_rubro(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_rubro.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }
        #endregion

        #region vistas
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_rubros_acumulados(decimal IdEmpleado = 0)
        {
            try
            {
                int  IdEmpresa= Convert.ToInt32(SessionFixed.IdEmpresa);
                ViewBag.IdEmpleado = IdEmpleado;
                bus_rubro_acumulados = new ro_empleado_x_rubro_acumulado_Bus();
                List<ro_empleado_x_rubro_acumulado_Info> model = bus_rubro_acumulados.get_list(IdEmpresa);
                return PartialView("_GridViewPartial_rubros_acumulados", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_rubros_acumulados_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            carga_combo_detalle();
            List<ro_empleado_x_rubro_acumulado_detalle_Info> lista = new List<ro_empleado_x_rubro_acumulado_detalle_Info>();
            lista = ListaDetalle.get_list(IdTransaccionSession);
            return PartialView("_GridViewPartial_rubros_acumulados_detalle", lista);
        }

        private void carga_combo_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_jornada = bus_jornada.get_list(IdEmpresa, false);
            ViewBag.lst_jornada = lst_jornada;
        }
        #endregion

        #region acciones
        [HttpPost]
        public ActionResult Nuevo(ro_empleado_x_rubro_acumulado_Info info)
        {
            try
            {

                ViewBag.IdEmpleado = info.IdEmpleado;
                info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                info.UsuarioIngresa = SessionFixed.IdUsuario;

                info.lst_empleado_x_rubro_acumulado_detalle = ListaDetalle.get_list(info.IdTransaccionSession);

                if ( bus_rubro_acumulados.si_existe(info))
                {
                    ViewBag.mensaje = "El empleado tiene una solicitud vigente para el rubro seleccionado";
                    cargar_combos();
                    return View(info);
                }
                if (ModelState.IsValid)
                {
                    if (!bus_rubro_acumulados.guardarDB(info))
                    {
                        cargar_combos();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index", new { IdEmpleado = info.IdEmpleado });

                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo(int IdEmpleado = 0)
        {
            try
            {
                ro_empleado_x_rubro_acumulado_Info model = new ro_empleado_x_rubro_acumulado_Info
                {
                    IdEmpleado = IdEmpleado,
                    Fec_Inicio_Acumulacion = new DateTime(DateTime.Now.Year, 1, 1),
                    Fec_Fin_Acumulacion = new DateTime(DateTime.Now.Year, 12, 1),
                    IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                    lst_empleado_x_rubro_acumulado_detalle = new List<ro_empleado_x_rubro_acumulado_detalle_Info>()
                };

                var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ListaDetalle.set_list(model.lst_empleado_x_rubro_acumulado_detalle, model.IdTransaccionSession);

                ViewBag.IdEmpleado = IdEmpleado;
                cargar_combos();
                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Modificar(decimal Idempleado = 0, string IdRubro = "")
        {
            try
            {
                cargar_combos();
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

                ro_empleado_x_rubro_acumulado_Info model = bus_rubro_acumulados.get_info(IdEmpresa, Idempleado, IdRubro);

                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
                model.lst_empleado_x_rubro_acumulado_detalle = bus_rubro_acumulados_detalle.get_list(IdEmpresa, Idempleado, IdRubro);
                ListaDetalle.set_list(model.lst_empleado_x_rubro_acumulado_detalle, model.IdTransaccionSession);

                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Modificar(ro_empleado_x_rubro_acumulado_Info info)
        {
            try
            {
                ViewBag.IdEmpleado = info.IdEmpleado;
                info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                info.UsuarioIngresa = SessionFixed.IdUsuario;

                info.lst_empleado_x_rubro_acumulado_detalle = ListaDetalle.get_list(info.IdTransaccionSession);

                if (ModelState.IsValid)
                {
                    if (!bus_rubro_acumulados.modificarDB(info))
                    {
                        cargar_combos();
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

        [HttpPost]
        public ActionResult Anular(ro_empleado_x_rubro_acumulado_Info info)
        {
            try
            {
                info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                if (!bus_rubro_acumulados.anularDB(info))
                {
                    cargar_combos();
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
        public ActionResult Anular(decimal Idempleado = 0, string IdRubro = "")
        {
            try
            {
                cargar_combos();
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

                ro_empleado_x_rubro_acumulado_Info model = bus_rubro_acumulados.get_info(IdEmpresa, Idempleado, IdRubro);

                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
                model.lst_empleado_x_rubro_acumulado_detalle = bus_rubro_acumulados_detalle.get_list(IdEmpresa, Idempleado, IdRubro);
                ListaDetalle.set_list(model.lst_empleado_x_rubro_acumulado_detalle, model.IdTransaccionSession);

                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ro_empleado_x_rubro_acumulado_detalle_Info info_det)
        {
            if (ModelState.IsValid)
                ListaDetalle.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            List<ro_empleado_x_rubro_acumulado_detalle_Info> model = new List<ro_empleado_x_rubro_acumulado_detalle_Info>();
            model = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            carga_combo_detalle();
            return PartialView("_GridViewPartial_rubros_acumulados_detalle", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_empleado_x_rubro_acumulado_detalle_Info info_det)
        {

            if (ModelState.IsValid)
                ListaDetalle.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            List<ro_empleado_x_rubro_acumulado_detalle_Info> model = new List<ro_empleado_x_rubro_acumulado_detalle_Info>();
            model = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            carga_combo_detalle();
            return PartialView("_GridViewPartial_rubros_acumulados_detalle", model);
        }

        public ActionResult EditingDelete(int secuencia)
        {
            ListaDetalle.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<ro_empleado_x_rubro_acumulado_detalle_Info> model = new List<ro_empleado_x_rubro_acumulado_detalle_Info>();
            model = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            carga_combo_detalle();
            return PartialView("_GridViewPartial_rubros_acumulados_detalle", model);
        }
        #endregion
        private void cargar_combos()
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ViewBag.lst_rubro = bus_rubro.get_list_rub_acumula(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
       
    }

    public class ro_empleado_x_rubro_acumulado_List
    {
        string Variable = "ro_empleado_x_rubro_acumulado_Info";
        public List<ro_empleado_x_rubro_acumulado_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_empleado_x_rubro_acumulado_Info> list = new List<ro_empleado_x_rubro_acumulado_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_empleado_x_rubro_acumulado_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_empleado_x_rubro_acumulado_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class ro_empleado_x_rubro_acumulado_detalle_List
    {
        ro_jornada_Bus bus_jornada = new ro_jornada_Bus();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();

        string variable = "ro_empleado_x_rubro_acumulado_detalle_Info";
        public List<ro_empleado_x_rubro_acumulado_detalle_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_empleado_x_rubro_acumulado_detalle_Info> list = new List<ro_empleado_x_rubro_acumulado_detalle_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_empleado_x_rubro_acumulado_detalle_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_empleado_x_rubro_acumulado_detalle_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ro_empleado_x_rubro_acumulado_detalle_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            List<ro_empleado_x_rubro_acumulado_detalle_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            if (info_det.IdRubroContabilizacion != null)
            {
                var info_rubro = bus_rubro.get_info(IdEmpresa, info_det.IdRubroContabilizacion);
                if (!string.IsNullOrEmpty(info_rubro.ToString()))
                    info_det.ru_descripcion = info_rubro.ru_descripcion;
            }

            if (info_det.IdJornada != 0)
            {
                var info_jornada = bus_jornada.get_info(IdEmpresa, info_det.IdJornada);
                if (!string.IsNullOrEmpty(info_jornada.ToString()))
                    info_det.Descripcion = info_jornada.Descripcion;
            }

            list.Add(info_det);
        }

        public void UpdateRow(ro_empleado_x_rubro_acumulado_detalle_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            ro_empleado_x_rubro_acumulado_detalle_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdJornada = info_det.IdJornada;
            edited_info.IdRubroContabilizacion = info_det.IdRubroContabilizacion;
            
            if (info_det.IdRubroContabilizacion != null)
            {
                var info_rubro = bus_rubro.get_info(IdEmpresa, info_det.IdRubroContabilizacion);
                if (!string.IsNullOrEmpty(info_rubro.ToString()))
                    info_det.ru_descripcion = info_rubro.ru_descripcion;
            }

            if (info_det.IdJornada != 0)
            {
                var info_jornada = bus_jornada.get_info(IdEmpresa, info_det.IdJornada);
                if (!string.IsNullOrEmpty(info_jornada.ToString()))
                    info_det.Descripcion = info_jornada.Descripcion;
            }

            edited_info.ru_descripcion = info_det.ru_descripcion;
            edited_info.Descripcion = info_det.Descripcion;
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<ro_empleado_x_rubro_acumulado_detalle_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == secuencia).First());
        }
    }
}