using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class PeriodosPorNominaController : Controller
    {
        ro_periodo_x_ro_Nomina_TipoLiqui_Bus bus_periodo_por_nomina = new ro_periodo_x_ro_Nomina_TipoLiqui_Bus();
        
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_nomina_tipoLiq( int IdNominaTipo=0, int IdNominaTipoLiq=0)
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> model = bus_periodo_por_nomina.get_list(IdEmpresa,IdNominaTipo,IdNominaTipoLiq);
                return PartialView("_GridViewPartial_nomina_tipoLiq", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Nuevo(ro_periodo_x_ro_Nomina_TipoLiqui_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdEmpresa =Convert.ToInt32(SessionFixed.IdEmpresa);
                    if (!bus_periodo_por_nomina.guardarDB(info))
                        return View(info);
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
                ro_periodo_x_ro_Nomina_TipoLiqui_Info info = new ro_periodo_x_ro_Nomina_TipoLiqui_Info();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

  
        [HttpPost]
        public ActionResult Anular(ro_periodo_x_ro_Nomina_TipoLiqui_Info info)
        {
            try
            {
                if (!bus_periodo_por_nomina.anularDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdNomina_Tipo = 0, int IdNominaTipoLiq = 0, int IdPeriodo=0)
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                return View(bus_periodo_por_nomina.get_info(IdEmpresa, IdNomina_Tipo, IdNominaTipoLiq, IdPeriodo));

            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult get_lst_periodo_x_nomina(int IdEmpresa=0, int IdNomina = 0, int IdNomina_Tipo = 0)
        {
            try
            {
                List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> lst_periodos_x_nominas = new List<ro_periodo_x_ro_Nomina_TipoLiqui_Info>();
                lst_periodos_x_nominas = bus_periodo_por_nomina.get_list_peridos(IdEmpresa, IdNomina, IdNomina_Tipo);
                return Json(lst_periodos_x_nominas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult get_sig_periodo(int IdEmpresa=0, int IdNomina = 0, int IdNomina_Tipo = 0)
        {
            try
            {
                int IdPeriodo = bus_periodo_por_nomina.get_siguinte_periodo_a_procesar(IdEmpresa, IdNomina, IdNomina_Tipo);
                return Json(IdPeriodo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
 
    }
}