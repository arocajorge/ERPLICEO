using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class JornadaController : Controller
    {
        #region Variables
        ro_jornada_Bus bus_jornada = new ro_jornada_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Jornada()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ro_jornada_Info> model = bus_jornada.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_Jornada", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            ro_jornada_Info model = new ro_jornada_Info
            {
                IdEmpresa = IdEmpresa
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ro_jornada_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_jornada.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdJornada = 0)
        {
            ro_jornada_Info model = bus_jornada.get_info(IdEmpresa, IdJornada);
            if (model == null)
                return RedirectToAction("Index");
                      return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ro_jornada_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_jornada.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdJornada = 0)
        {
            ro_jornada_Info model = bus_jornada.get_info(IdEmpresa, IdJornada);
            if (model == null)
                return RedirectToAction("Index");
           return View(model);
        }

        [HttpPost]
        public ActionResult Anular(ro_jornada_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_jornada.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

    }

}