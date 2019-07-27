using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class PrestamosAprobacionController : Controller
    {
        ro_prestamo_Bus bus_prestamos = new ro_prestamo_Bus();
        ro_prestamo_List list_prestamos_aprobar = new ro_prestamo_List();
        ro_Parametros_Bus bus_parametros = new ro_Parametros_Bus();
        cp_orden_pago_tipo_x_empresa_Bus bus_orden_pago_tipo_x_empresa = new cp_orden_pago_tipo_x_empresa_Bus();
        // GET: RRHH/PrestamosAprobacion
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            model.IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            list_prestamos_aprobar.get_list(model.IdTransaccionSession);

            return View(model);
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartial_prestamos_aprobacion()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
           
            List<ro_prestamo_Info> model = bus_prestamos.get_list_aprobacion(IdEmpresa);
            return PartialView("_GridViewPartial_prestamos_aprobacion", model);
        }

        public JsonResult aprobar(int IdEmpresa = 0, string Ids = "")
        {
            var existen_cuentas = true;
            var mensaje_aprobar = "";
            ro_prestamo_Info info_prestamo = new ro_prestamo_Info();
            string[] array = Ids.Split(',');
            string IdUsuarioAprueba = SessionFixed.IdUsuario;

            if (string.IsNullOrEmpty(Ids))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                foreach (var item in array)
                {
                    info_prestamo = bus_prestamos.get_info(IdEmpresa, Convert.ToDecimal(item));

                    if (info_prestamo.GeneraOP)
                    {
                        if (info_prestamo == null || (info_prestamo.IdCtaCble_Emplea == null || info_prestamo.IdCtaCble_x_pagar_empleado == null  ))
                        {
                            existen_cuentas = false;
                            mensaje_aprobar = "No estan asignadas las cuentas contables de los empleados seleccionados";
                            break;
                        }
                    }
                }

                if (existen_cuentas)
                {
                    var resultado_orden = bus_prestamos.aprobar_prestamo(IdEmpresa, array, IdUsuarioAprueba);
                    return Json(mensaje_aprobar, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(mensaje_aprobar, JsonRequestBehavior.AllowGet);
                }
                               
            }         
        }
    }

    public class ro_prestamo_List
    {
        string variable = "ro_prestamo_Info";
        public List<ro_prestamo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_prestamo_Info> list = new List<ro_prestamo_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_prestamo_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }
        public void set_list(List<ro_prestamo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }
    }
}