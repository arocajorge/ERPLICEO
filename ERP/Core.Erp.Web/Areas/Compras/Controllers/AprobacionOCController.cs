using Core.Erp.Bus.Compras;
using Core.Erp.Bus.General;
using Core.Erp.Info.Compras;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Compras.Controllers
{
    public class AprobacionOCController : Controller
    {
        #region Index

        com_ordencompra_local_Bus bus_ordencompra = new com_ordencompra_local_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        com_orden_aprobacion_List List_apro = new com_orden_aprobacion_List();

        public ActionResult Index()
        {
            com_orden_aprobacion_Info model = new com_orden_aprobacion_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal),
                IdUsuarioAprobacion = string.IsNullOrEmpty(SessionFixed.IdUsuario) ? "" : SessionFixed.IdUsuario,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)

            };
            List_apro.set_list(new List<com_ordencompra_local_Info>(), model.IdTransaccionSession);
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(com_orden_aprobacion_Info model)
        {
            model.IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            List_apro.get_list(model.IdTransaccionSession);
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_aprobacion_oc( DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal)
        {

            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(fecha_ini);
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_fin);
            ViewBag.IdSucursal = IdSucursal == 0 ? 0 : Convert.ToInt32(IdSucursal);

            var model = bus_ordencompra.GetListPorAprobar(IdEmpresa, IdSucursal, ViewBag.fecha_ini, ViewBag.fecha_fin);
            return PartialView("_GridViewPartial_aprobacion_oc", model);
        }
        #endregion

        private void cargar_combos(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
            
        }
        public JsonResult aprobar(int IdEmpresa = 0, int IdSucursal = 0 , string Ids = "", string MotivoAprobacion = "", string IdUsuarioAprobacion = "")
        {
            string[] array = Ids.Split(',');

            if (string.IsNullOrEmpty(array.ToString()))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var resultado_orden = bus_ordencompra.AprobarOC(IdEmpresa, IdSucursal, array, MotivoAprobacion, IdUsuarioAprobacion);
                return Json(resultado_orden, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult rechazar(int IdEmpresa = 0, string Ids = "", int IdSucursal = 0 , string MotivoAprobacion = "", string IdUsuarioAprobacion = "")
        {
            string[] array = Ids.Split(',');

            if (string.IsNullOrEmpty(array.ToString()))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var resultado_orden = bus_ordencompra.RechazarOC(IdEmpresa, IdSucursal, array, MotivoAprobacion, IdUsuarioAprobacion);
                return Json(resultado_orden, JsonRequestBehavior.AllowGet);
            }
        }

    }
    public class com_orden_aprobacion_List
       {
           string variable = "com_ordencompra_local_Info";
           public List<com_ordencompra_local_Info> get_list(decimal IdTransaccionSession)
           {
               if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
               {
                   List<com_ordencompra_local_Info> list = new List<com_ordencompra_local_Info>();

                   HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
               }
               return (List<com_ordencompra_local_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
           }
           public void set_list(List<com_ordencompra_local_Info> list, decimal IdTransaccionSession)
           {
               HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
          }
       }

}