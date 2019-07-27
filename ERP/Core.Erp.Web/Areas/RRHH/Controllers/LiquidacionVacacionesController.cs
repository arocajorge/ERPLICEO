using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class LiquidacionVacacionesController : Controller
    {
        ro_Historico_Liquidacion_Vacaciones_Bus bus_liquidacion = new ro_Historico_Liquidacion_Vacaciones_Bus();
        List<ro_Historico_Liquidacion_Vacaciones_Info> lst_vacaciones = new List<ro_Historico_Liquidacion_Vacaciones_Info>();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        List<ro_Historico_Liquidacion_Vacaciones_Det_Info> lst_detalle = new List<ro_Historico_Liquidacion_Vacaciones_Det_Info>();
        ro_Historico_Liquidacion_Vacaciones_Det_Info_lst ro_Historico_Liquidacion_Vacaciones_Det_Info = new ro_Historico_Liquidacion_Vacaciones_Det_Info_lst();
        ro_Historico_Liquidacion_Vacaciones_Info info_liquidacion = new ro_Historico_Liquidacion_Vacaciones_Info();
        ro_Solicitud_Vacaciones_x_empleado_Bus bus_solicitud = new ro_Solicitud_Vacaciones_x_empleado_Bus();

        
        string MensajeSuccess = "La transacción se ha realizado con éxito";

        public static int IdSolicitud { get; set; }
        int IdEmpresa = 0;

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

        public ActionResult CmbEmpleado_vaca()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_vaca", model);
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
        public ActionResult GridViewPartial_vacaciones_liquidadas(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                ViewBag.Fecha_ini = Fecha_ini == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : Convert.ToDateTime(Fecha_ini);
                ViewBag.Fecha_fin = Fecha_fin == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) : Convert.ToDateTime(Fecha_fin);

                List<ro_Historico_Liquidacion_Vacaciones_Info> model = bus_liquidacion.get_list(IdEmpresa, ViewBag. Fecha_ini,ViewBag. Fecha_fin);
                return PartialView("_GridViewPartial_vacaciones_liquidadas", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_vacaciones_liquidadas_det()
        {
            try
            {
                lst_detalle = ro_Historico_Liquidacion_Vacaciones_Det_Info.get_list();
                return PartialView("_GridViewPartial_vacaciones_liquidadas_det", lst_detalle);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_Historico_Liquidacion_Vacaciones_Info info)
        {
            try
            {
                bus_liquidacion = new ro_Historico_Liquidacion_Vacaciones_Bus();
                if (ModelState.IsValid)
                {
                    string mensaje = "";
                    info.detalle = ro_Historico_Liquidacion_Vacaciones_Det_Info.get_list();
                    if (info.detalle != null)
                    {
                        foreach (var item in info.detalle)
                        {
                            if (item.Valor_Cancelar == 0)
                            {
                                mensaje = "Existen periodos con valores cero a cancelar";
                            }
                        }
                    }
                    if (mensaje != "")
                    {
                        ViewBag.mensaje = mensaje;
                        cargar_combo();
                        return View(info);
                    }
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_liquidacion.guardarDB(info))
                    {
                        cargar_combo();
                        return View(info);
                    }
                    else
                    {
                        return RedirectToAction("Modificar", new { IdEmpleado = info.IdEmpleado, IdLiquidacion = info.IdLiquidacion, Exito = true });
                    }
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo(decimal IdEmpleado=0, decimal IdSolicitud = 0)
        {
            try
            {
                ro_Historico_Liquidacion_Vacaciones_Info model = new ro_Historico_Liquidacion_Vacaciones_Info
                {
                  
                };
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                var  info_solicitud = bus_solicitud.get_info(IdEmpresa, IdEmpleado, IdSolicitud);
                model = bus_liquidacion.obtener_valores(info_solicitud);
                IdSolicitud = model.IdSolicitud;
                ro_Historico_Liquidacion_Vacaciones_Det_Info.set_list(model.detalle);

                cargar_combo();
                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_Historico_Liquidacion_Vacaciones_Info info)
        {

            try
            {
                bus_liquidacion = new ro_Historico_Liquidacion_Vacaciones_Bus();
                if (ModelState.IsValid)
                {
                    string mensaje = "";
                    info.detalle = ro_Historico_Liquidacion_Vacaciones_Det_Info.get_list();
                    if(info.detalle!=null)
                    {
                        foreach (var item in info.detalle)
                        {
                            if (item.Valor_Cancelar == 0)
                            {
                                mensaje = "Existen periodos con valores cero a cancelar";
                            }
                        }
                    }
                    if (mensaje != "")
                    {
                        ViewBag.mensaje = mensaje;
                        cargar_combo();
                        return View(info);
                    }
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_liquidacion.modificarDB(info))
                    {
                        cargar_combo();
                        return View(info);
                    }
                    else
                    {
                        return RedirectToAction("Modificar", new { IdEmpleado = info.IdEmpleado, IdLiquidacion = info.IdLiquidacion, Exito = true });
                    }
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Modificar(decimal IdEmpleado = 0, decimal IdLiquidacion = 0, bool Exito = false)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                info_liquidacion = bus_liquidacion.get_info(IdEmpresa, IdEmpleado, IdLiquidacion);
                ro_Historico_Liquidacion_Vacaciones_Det_Info.set_list( info_liquidacion.detalle);
                cargar_combo();
                if (Exito)
                    ViewBag.MensajeSuccess = MensajeSuccess;

                return View(info_liquidacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_Historico_Liquidacion_Vacaciones_Info info)
        {
            try
            {
                bus_liquidacion = new ro_Historico_Liquidacion_Vacaciones_Bus();
                IdEmpresa = GetIdEmpresa();
                info.IdEmpresa = IdEmpresa;
                if (!bus_liquidacion.anularDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index");


            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(decimal IdEmpleado = 0, decimal IdLiquidacion = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                info_liquidacion = bus_liquidacion.get_info(IdEmpresa, IdEmpleado, IdLiquidacion);
                ro_Historico_Liquidacion_Vacaciones_Det_Info.set_list( info_liquidacion.detalle);
                cargar_combo();
                return View(info_liquidacion);

            }
            catch (Exception)
            {

                throw;
            }
        }
        private int GetIdEmpresa()
        {
            try
            {
                if (Session["IdEmpresa"] != null)
                    return Convert.ToInt32(Session["IdEmpresa"]);
                else
                    return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cargar_combo()
        {
            IdEmpresa = GetIdEmpresa();
            ViewBag.lst_empleado = bus_empleado.get_list_combo(IdEmpresa);
            ViewBag.lst_vacaciones = lst_vacaciones;
        }
   

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Historico_Liquidacion_Vacaciones_Det_Info info_det)
        {
            bus_solicitud = new ro_Solicitud_Vacaciones_x_empleado_Bus();
            ro_Historico_Liquidacion_Vacaciones_Info model = new ro_Historico_Liquidacion_Vacaciones_Info();
            string IdSolicitud = !string.IsNullOrEmpty(Request.Params["IdSolicitud"]) ? Request.Params["IdSolicitud"].ToString() : "0";
            string IdEmpleado = !string.IsNullOrEmpty(Request.Params["IdEmpleado"]) ? Request.Params["IdEmpleado"].ToString() : "0";

            var ro_solicitud = bus_solicitud.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(IdEmpleado), Convert.ToInt32(IdSolicitud));
            if (ro_solicitud == null)
                ro_solicitud = new ro_Solicitud_Vacaciones_x_empleado_Info();
            info_det.Total_Vacaciones = info_det.Total_Remuneracion / 24;
            info_det.Valor_Cancelar = (info_det.Total_Vacaciones / ro_solicitud.Dias_q_Corresponde)*ro_solicitud.Dias_a_disfrutar;

            ro_Historico_Liquidacion_Vacaciones_Det_Info.UpdateRow(info_det);
            model.detalle = ro_Historico_Liquidacion_Vacaciones_Det_Info.get_list() as List<ro_Historico_Liquidacion_Vacaciones_Det_Info>;
            return PartialView("_GridViewPartial_vacaciones_liquidadas_det", model.detalle);
        }

        public JsonResult get_list_vacaciones(DateTime? Anio_Desde, DateTime? Anio_Hasta, decimal IdEmpleado = 0, decimal IdSolicitud=0)
        {
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var info_solicitud = bus_solicitud.get_info(IdEmpresa, IdEmpleado, IdSolicitud);
            info_solicitud.Anio_Desde = Convert.ToDateTime(Anio_Desde);
            info_solicitud.Anio_Hasta = Convert.ToDateTime(Anio_Hasta);
            var  model = bus_liquidacion.obtener_valores(info_solicitud);
            if (model != null)
            {
                
                ro_Historico_Liquidacion_Vacaciones_Det_Info.set_list(model.detalle);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }

   

    public class ro_Historico_Liquidacion_Vacaciones_Det_Info_lst
    {
        string variable = "ro_Historico_Liquidacion_Vacaciones_Det_Info";
        public List<ro_Historico_Liquidacion_Vacaciones_Det_Info> get_list()
        {
            if (HttpContext.Current.Session[variable] == null)
            {
                List<ro_Historico_Liquidacion_Vacaciones_Det_Info> list = new List<ro_Historico_Liquidacion_Vacaciones_Det_Info>();

                HttpContext.Current.Session[variable] = list;
            }
            return (List<ro_Historico_Liquidacion_Vacaciones_Det_Info>)HttpContext.Current.Session[variable];
        }

        public void set_list(List<ro_Historico_Liquidacion_Vacaciones_Det_Info> list)
        {
            HttpContext.Current.Session[variable] = list;
        }


        public void UpdateRow(ro_Historico_Liquidacion_Vacaciones_Det_Info info_det)
        {
            ro_Historico_Liquidacion_Vacaciones_Det_Info edited_info = get_list().Where(m => m.Sec == info_det.Sec).First();
            edited_info.IdLiquidacion = info_det.IdLiquidacion;
            edited_info.Total_Remuneracion = info_det.Total_Remuneracion;
            edited_info.Total_Vacaciones = info_det.Total_Vacaciones;
            edited_info.Valor_Cancelar = info_det.Valor_Cancelar;

        }

    }
}

