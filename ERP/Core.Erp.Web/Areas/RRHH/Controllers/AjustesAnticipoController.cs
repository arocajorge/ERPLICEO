using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class AjustesAnticipoController : Controller
    {
        // GET: RRHH/AjustesAnticipo

        #region clases
        ro_rol_detalle_Info_list ro_rol_detalle_Info_list = new ro_rol_detalle_Info_list();
        List<ro_nomina_tipo_Info> lista_nomina = new List<ro_nomina_tipo_Info>();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        List<ro_Nomina_Tipoliqui_Info> lst_nomina_tipo = new List<ro_Nomina_Tipoliqui_Info>();
        ro_periodo_x_ro_Nomina_TipoLiqui_Bus bus_periodos_x_nomina = new ro_periodo_x_ro_Nomina_TipoLiqui_Bus();
        List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> lst_periodos = new List<ro_periodo_x_ro_Nomina_TipoLiqui_Info>();
        ro_rol_detalle_Bus bus_rol = new ro_rol_detalle_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        #endregion
        public ActionResult Index()
        {

            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdNomina = 1,
                IdTipoNomina = 1,
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combos(0, 0);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            var lista = ro_rol_detalle_Info_list.get_list(model.IdTransaccionSession).Where(v=>v.check==true).ToList();
          if(!  bus_rol.ajustar_anticipo(lista))
            return View(model);
           return RedirectToAction("Index", "ProcesarRol");
        }
        public ActionResult GridViewPartial_ajuste_anticipo()
        {
            List<ro_rol_detalle_Info> model = new List<ro_rol_detalle_Info>();
            model = ro_rol_detalle_Info_list.get_list(Convert.ToInt32(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ajuste_anticipo", model);
        }
        #region funciones del detalle

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_rol_detalle_Info info_det)
        {
            if (ModelState.IsValid)
                ro_rol_detalle_Info_list.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<ro_rol_detalle_Info> model = new List<ro_rol_detalle_Info>();
            model = ro_rol_detalle_Info_list.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ajuste_anticipo", model);
        }

        public JsonResult CargarEmpleados(int IdNomina_Tipo = 0, int IdSucursal=0, int IdNomina_TipoLiqui = 0, int IdPeriodo = 0, decimal IdTransaccionSession = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_rol_detalle_Bus bus_rol = new ro_rol_detalle_Bus();
            var detalle = bus_rol.get_list_ajustar_anticipo(IdEmpresa,IdSucursal, IdNomina_Tipo, IdNomina_TipoLiqui, IdPeriodo);
            ro_rol_detalle_Info_list.set_list(detalle, Convert.ToDecimal(IdTransaccionSession));
            
            return Json("", JsonRequestBehavior.AllowGet);
        }

        private void cargar_combos(int IdNomina_Tipo, int IdNomina_Tipo_Liqui)
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                lista_nomina = bus_nomina.get_list(IdEmpresa, false);
                lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, IdNomina_Tipo);
                lst_periodos = bus_periodos_x_nomina.get_list(IdEmpresa, IdNomina_Tipo, IdNomina_Tipo_Liqui);
                ViewBag.lst_nomina = lista_nomina;
                ViewBag.lst_nomina_tipo = lst_nomina_tipo;
                ViewBag.lst_periodos = lst_periodos;
                ViewBag.lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);


            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }

    public class ro_rol_detalle_Info_list
    {
        string variable = "ro_rol_detalle_Info";
        public List<ro_rol_detalle_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_rol_detalle_Info> list = new List<ro_rol_detalle_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_rol_detalle_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_rol_detalle_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(ro_rol_detalle_Info info_det, decimal IdTransaccionSession)
        {
            ro_rol_detalle_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdEmpleado == info_det.IdEmpleado).First();
            edited_info.Valor = info_det.Valor;
            edited_info.check = true;


        }

    }
}