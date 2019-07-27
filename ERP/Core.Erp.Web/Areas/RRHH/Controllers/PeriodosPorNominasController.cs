using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class PeriodosPorNominasController : Controller
    {
        #region vistas

        ro_periodo_x_ro_Nomina_TipoLiqui_Info_list List_det = new ro_periodo_x_ro_Nomina_TipoLiqui_Info_list();
        ro_periodo_x_ro_Nomina_TipoLiqui_Bus periodos_x_nominas = new ro_periodo_x_ro_Nomina_TipoLiqui_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_periodo_x_ro_Nomina_TipoLiqui_Info model = new ro_periodo_x_ro_Nomina_TipoLiqui_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdEmpresa=Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            cargar_combos(0,0);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ro_periodo_x_ro_Nomina_TipoLiqui_Info model)
        {
            cargar_combos(model.IdNomina_Tipo, model.IdNomina_TipoLiqui);
            List_det.set_list(periodos_x_nominas.get_list(model.IdEmpresa, model.IdNomina_Tipo, model.IdNomina_TipoLiqui), model.IdTransaccionSession);
            return View(model);
        }
        private void cargar_combos(int IdNomina_Tipo, int IdNomina_Tipo_Liqui)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lista_nomina = bus_nomina.get_list(IdEmpresa, false);
            var lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, IdNomina_Tipo);
            ViewBag.lst_nomina = lista_nomina;
            ViewBag.lst_nomina_tipo = lst_nomina_tipo;

        }
        #endregion
        #region grillas


        public ActionResult GridViewPartial_periodos()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToInt32(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == false).ToList();
            return PartialView("_GridViewPartial_periodos", model);
        }
        public ActionResult GridViewPartial_periodos_por_nominas()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToInt32(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList();
            return PartialView("_GridViewPartial_periodos_por_nominas", model);
        }
        public void EditingUpdate(int IdPeriodo =0)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List_det.UpdateRow(IdPeriodo, Convert.ToInt32(SessionFixed.IdTransaccionSessionActual));
        }
        #endregion

        #region Json
        public JsonResult guardar(int IdEmpresa = 0,int IdNomina_Tipo=0,int IdNomina_TipoLiqui=0)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            var lista_grabar = List_det.get_list(Convert.ToInt32(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList();
                var resultado = periodos_x_nominas.guardarDB(lista_grabar, IdEmpresa,IdNomina_Tipo,IdNomina_TipoLiqui);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        

        #endregion
    }
    
    public class ro_periodo_x_ro_Nomina_TipoLiqui_Info_list
    {
        string variable = "ro_periodo_x_ro_Nomina_TipoLiqui_Info";
        public List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> list = new List<ro_periodo_x_ro_Nomina_TipoLiqui_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_periodo_x_ro_Nomina_TipoLiqui_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(int IdPeriodo, decimal IdTransaccionSession)
        {
            ro_periodo_x_ro_Nomina_TipoLiqui_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdPeriodo == IdPeriodo).FirstOrDefault();
            if (edited_info != null)
            {
                edited_info.seleccionado = !edited_info.seleccionado;
                //edited_info.Procesado = "N";
            }
                
        }
    }
}