using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ParticipacionUtilidadController : Controller
    {
        #region variables
        ro_participacion_utilidad_Bus bus_utilidad = new ro_participacion_utilidad_Bus();
        List<ro_nomina_tipo_Info> lista_nomina = new List<ro_nomina_tipo_Info>();
        List<ro_participacion_utilidad_empleado_Info> lst_detalle = new List<ro_participacion_utilidad_empleado_Info>();
        ro_participacion_utilidad_empleado_Bus bus_detalle = new ro_participacion_utilidad_empleado_Bus();
        ro_periodo_Bus bus_periodo = new ro_periodo_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        List<ro_Nomina_Tipoliqui_Info> lst_nomina_tipo = new List<ro_Nomina_Tipoliqui_Info>();
        ro_periodo_x_ro_Nomina_TipoLiqui_Bus bus_periodos_x_nomina = new ro_periodo_x_ro_Nomina_TipoLiqui_Bus();
        List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> lst_periodos = new List<ro_periodo_x_ro_Nomina_TipoLiqui_Info>();
        ro_participacion_utilidad_Info info_utilidad = new ro_participacion_utilidad_Info();
        #endregion

        int IdEmpresa = 0;
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_utilidades()
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                List<ro_participacion_utilidad_Info> model = bus_utilidad.get_list(IdEmpresa, true);
                return PartialView("_GridViewPartial_utilidades", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult GridViewPartial_utilidades_detalle()
        {
            try
            {
                ro_participacion_utilidad_Info info = new ro_participacion_utilidad_Info();
                info.detalle = Session["detalle"] as List<ro_participacion_utilidad_empleado_Info>;
                if (info == null)
                    info = new ro_participacion_utilidad_Info();
                return PartialView("_GridViewPartial_utilidades_detalle", info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_participacion_utilidad_Info info)
        {
            try
            {
                cargar_combos(info.IdNomina_Tipo,info.IdNomina_TipoLiqui);

                if (info == null)
                {
                    info = new ro_participacion_utilidad_Info();
                    return View(info);

                }
                else
                {
                    IdEmpresa = GetIdEmpresa();
                    info.IdPeriodo = 20181284;
                    info.IdNomina_TipoLiqui = 5;
                    info.IdEmpresa = IdEmpresa;
                    info.detalle = Session["detalle"] as List<ro_participacion_utilidad_empleado_Info>;
                    bus_utilidad.guardarDB(info);

                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Modificar(int IdUtilidad = 0)
        {
            try
            {
                ro_participacion_utilidad_Info info = new ro_participacion_utilidad_Info();
                IdEmpresa = GetIdEmpresa();
                info= bus_utilidad.get_info(IdEmpresa, IdUtilidad);
                Session["detalle"] = info.detalle;
                cargar_combos(info.IdNomina_Tipo,info.IdNomina_TipoLiqui);

                return View(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Anular(ro_participacion_utilidad_Info info)
        {
            try
            {
                cargar_combos(info.IdNomina_Tipo, info.IdNomina_TipoLiqui);
                info.IdEmpresa = GetIdEmpresa();
                if (!bus_utilidad.anularDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index");


            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdUtilidad = 0)
        {
            try
            {
                ro_participacion_utilidad_Info info = new ro_participacion_utilidad_Info();
                IdEmpresa = GetIdEmpresa();
                info = bus_utilidad.get_info(IdEmpresa, IdUtilidad);
                Session["detalle"] = info.detalle;
                cargar_combos(info.IdNomina_Tipo, info.IdNomina_TipoLiqui);

                return View(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_participacion_utilidad_Info info)
        {
            try
            {
                 cargar_combos(info.IdNomina_Tipo, info.IdNomina_TipoLiqui);

                if (info==null)
                {
                    info = new ro_participacion_utilidad_Info();
                    return View(info);

                }
                else
                {                   
                    IdEmpresa = GetIdEmpresa();
                    info.IdEmpresa = IdEmpresa;
                    info.detalle = Session["detalle"] as List<ro_participacion_utilidad_empleado_Info>;
                    bus_utilidad.guardarDB(info);
                    return RedirectToAction("Index");

                }

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
                ro_participacion_utilidad_Info info = new ro_participacion_utilidad_Info();
                cargar_combos(0,0);
                return View(info);
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult get_lst_nomina_tipo_liq(int IdNomina = 0)
        {
            try
            {
                List<ro_Nomina_Tipoliqui_Info> lst_tipo_nomina = new List<ro_Nomina_Tipoliqui_Info>();
                lst_tipo_nomina = bus_nomina_tipo.get_list(GetIdEmpresa(), IdNomina);
                return Json(lst_tipo_nomina, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult get_lst_periodo_x_nomina(int IdNomina = 0, int IdNomina_Tipo = 0)
        {
            try
            {
                List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> lst_tipo_nomina = new List<ro_periodo_x_ro_Nomina_TipoLiqui_Info>();
                lst_periodos = bus_periodos_x_nomina.get_list(GetIdEmpresa(), IdNomina, IdNomina_Tipo);
                return Json(lst_periodos, JsonRequestBehavior.AllowGet);
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
        private void cargar_combos(int IdNomina_Tipo, int IdNomina_Tipo_Liqui)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                lista_nomina = bus_nomina.get_list(IdEmpresa, false);
                lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, IdNomina_Tipo);
                lst_periodos = bus_periodos_x_nomina.get_list(IdEmpresa, IdNomina_Tipo, IdNomina_Tipo_Liqui);
                ViewBag.lst_nomina = lista_nomina;
                ViewBag.lst_nomina_tipo = lst_nomina_tipo;
                ViewBag.lst_periodos = lst_periodos;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult calcular( int IdNomina_Tipo = 0,int IdPeriodo = 0, double UtilidadDerechoIndividual = 0, double UtilidadCargaFamiliar=0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                info_utilidad.IdNomina_Tipo = IdNomina_Tipo;
                info_utilidad.IdPeriodo = info_utilidad.IdPeriodo;
                info_utilidad.UtilidadDerechoIndividual = UtilidadDerechoIndividual;
                info_utilidad.UtilidadCargaFamiliar = UtilidadCargaFamiliar;
                info_utilidad.detalle = bus_detalle.calcular(IdEmpresa, IdNomina_Tipo, IdPeriodo, UtilidadDerechoIndividual, UtilidadCargaFamiliar);
                Session["detalle"] = info_utilidad.detalle as  List < ro_participacion_utilidad_empleado_Info >;
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}