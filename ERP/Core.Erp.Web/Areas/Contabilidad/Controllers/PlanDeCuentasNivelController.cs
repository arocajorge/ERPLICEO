using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    [SessionTimeout]
    public class PlanDeCuentasNivelController : Controller
    {
        #region Index
        ct_plancta_nivel_Bus bus_plancta_nivel = new ct_plancta_nivel_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_plancta_nivel()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            List<ct_plancta_nivel_Info> model = bus_plancta_nivel.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_plancta_nivel", model);
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo()
        {
            ct_plancta_nivel_Info model = new ct_plancta_nivel_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ct_plancta_nivel_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (bus_plancta_nivel.validar_existe_nivel(model.IdEmpresa, model.IdNivelCta))
            {
                ViewBag.mensaje = "El nivel ya se encuentra registrado";
                return View(model);
            }
            model.IdUsuario = Session["IdUsuario"].ToString();
            if (!bus_plancta_nivel.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdNivelCta = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ct_plancta_nivel_Info model = bus_plancta_nivel.get_info(IdEmpresa,IdNivelCta);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ct_plancta_nivel_Info model)
        {
            model.IdUsuarioUltModi = Session["IdUsuario"].ToString();
            if (!bus_plancta_nivel.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdNivelCta = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ct_plancta_nivel_Info model = bus_plancta_nivel.get_info(IdEmpresa, IdNivelCta);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_plancta_nivel_Info model)
        {
            model.IdUsuarioUltAnu = Session["IdUsuario"].ToString();
            if (!bus_plancta_nivel.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}