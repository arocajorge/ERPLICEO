using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    public class CentroCostoNivelController : Controller
    {
        #region Index
        ct_CentroCostoNivel_Bus bus_cc_nivel = new ct_CentroCostoNivel_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_cencost_nivel()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            List<ct_CentroCostoNivel_Info> model = bus_cc_nivel.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_cencost_nivel", model);
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo()
        {
            ct_CentroCostoNivel_Info model = new ct_CentroCostoNivel_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ct_CentroCostoNivel_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (bus_cc_nivel.validar_existe_nivel(model.IdEmpresa, model.IdNivel))
            {
                ViewBag.mensaje = "El nivel ya se encuentra registrado";
                return View(model);
            }
            model.IdUsuarioCreacion = Session["IdUsuario"].ToString();
            if (!bus_cc_nivel.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdNivel = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ct_CentroCostoNivel_Info model = bus_cc_nivel.get_info(IdEmpresa, IdNivel);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ct_CentroCostoNivel_Info model)
        {
            model.IdUsuarioModificacion = Session["IdUsuario"].ToString();
            if (!bus_cc_nivel.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdNivel = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ct_CentroCostoNivel_Info model = bus_cc_nivel.get_info(IdEmpresa, IdNivel);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_CentroCostoNivel_Info model)
        {
            model.IdUsuarioAnulacion = Session["IdUsuario"].ToString();
            if (!bus_cc_nivel.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}