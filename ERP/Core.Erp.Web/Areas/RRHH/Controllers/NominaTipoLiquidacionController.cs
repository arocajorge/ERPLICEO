using DevExpress.Web.Mvc;
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
    public class NominaTipoLiquidacionController : Controller
    {
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        List<ro_nomina_tipo_Info> lst_nominas = new List<ro_nomina_tipo_Info>();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_nomina_tipo_liquidacion()
        {
            try
            {
                List<ro_Nomina_Tipoliqui_Info> model = bus_nomina_tipo.get_list(GetIdEmpresa(), true);
                return PartialView("_GridViewPartial_nomina_tipo_liquidacion", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Nuevo(ro_Nomina_Tipoliqui_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_nomina_tipo.guardarDB(info))
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
                cargar_combos();
                ro_Nomina_Tipoliqui_Info info = new ro_Nomina_Tipoliqui_Info
                {
                    IdNomina_Tipo = 1
                };
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Modificar(ro_Nomina_Tipoliqui_Info info)
        {
            try
            {
                cargar_combos();
                if (ModelState.IsValid)
                {
                    info.IdUsuarioUltModi = Session["IdUsuario"].ToString();
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_nomina_tipo.modificarDB(info))
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
        public ActionResult Modificar(int IdNomina_Tipo = 0, int IdNomina_TipoLiqui=0)
        {
            try
            {
                cargar_combos();
                return View(bus_nomina_tipo.get_info(GetIdEmpresa(), IdNomina_Tipo, IdNomina_TipoLiqui));

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Anular(ro_Nomina_Tipoliqui_Info info)
        {
            try
            {
                info.IdUsuarioAnu = Session["IdUsuario"].ToString();
                info.IdEmpresa = GetIdEmpresa();
                if (!bus_nomina_tipo.anularDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdNomina_Tipo = 0, int IdNomina_TipoLiqui=0)
        {
            try
            {
                cargar_combos();
                return View(bus_nomina_tipo.get_info(GetIdEmpresa(), IdNomina_Tipo, IdNomina_TipoLiqui));

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
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                List<ro_Nomina_Tipoliqui_Info> lst_tipo_nomina = new List<ro_Nomina_Tipoliqui_Info>();
                lst_tipo_nomina = bus_nomina_tipo.get_list(IdEmpresa, IdNomina);
                return Json(lst_tipo_nomina, JsonRequestBehavior.AllowGet);
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

        private void cargar_combos()
        {
            try
            {
                lst_nominas = bus_nomina.get_list(GetIdEmpresa(), false);
                ViewBag.lst_nomina = lst_nominas;
            }
            catch (Exception)
            {
               
                throw;
            }
        }
       
    }
}