using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    public class PuntoCargoController : Controller
    {
        #region Index
        ct_punto_cargo_Bus bus_punto_cargo = new ct_punto_cargo_Bus();
        public ActionResult Index(int IdPunto_cargo_grupo = 0)
        {
            ViewBag.IdPunto_cargo_grupo = IdPunto_cargo_grupo;
            return View();
        }
        
        [ValidateInput(false)]
        public ActionResult GridViewPartial_punto_cargo(int IdPunto_cargo_grupo=0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_punto_cargo.GetList(IdEmpresa, IdPunto_cargo_grupo, true);
            ViewBag.IdPunto_cargo_grupo = IdPunto_cargo_grupo;
            return PartialView("_GridViewPartial_punto_cargo", model);
        }

        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ct_punto_cargo_grupo_Bus bus_punto_grupo = new ct_punto_cargo_grupo_Bus();
            var lst_grupo = bus_punto_grupo.GetList(IdEmpresa, false);
            ViewBag.lst_grupo = lst_grupo;

        }
        #endregion
        #region Acciones

        public ActionResult Nuevo(int IdPunto_cargo_grupo = 0)
        {
            ct_punto_cargo_Info model = new ct_punto_cargo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdPunto_cargo_grupo = IdPunto_cargo_grupo
            };
            ViewBag.IdPunto_cargo_grupo = IdPunto_cargo_grupo;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ct_punto_cargo_Info model)
        {
            model.IdUsuarioCreacion = Convert.ToString(SessionFixed.IdUsuario);
            if (!bus_punto_cargo.GuardarDB(model))
            {
                ViewBag.IdPunto_cargo_grupo = model.IdPunto_cargo_grupo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdPunto_cargo_grupo = model.IdPunto_cargo_grupo });
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdPunto_cargo_grupo = 0, int IdPunto_cargo= 0)
        {
            ct_punto_cargo_Info model = bus_punto_cargo.GetInfo(IdEmpresa, IdPunto_cargo_grupo, IdPunto_cargo);
            if (model == null)
                return RedirectToAction("Index", new { IdPunto_cargo_grupo = IdPunto_cargo_grupo });
            ViewBag.IdPunto_cargo_grupo = IdPunto_cargo_grupo;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ct_punto_cargo_Info model)
        {
            model.IdUsuarioModificacion = Convert.ToString(SessionFixed.IdUsuario);
            if (!bus_punto_cargo.ModificarDB(model))
            {
                ViewBag.IdPunto_cargo_grupo = model.IdPunto_cargo_grupo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdPunto_cargo_grupo = model.IdPunto_cargo_grupo });
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdPunto_cargo_grupo = 0, int IdPunto_cargo=0)
        {
            ct_punto_cargo_Info model = bus_punto_cargo.GetInfo(IdEmpresa, IdPunto_cargo_grupo, IdPunto_cargo);
            if (model == null)
                return RedirectToAction("Index", new { IdPunto_cargo_grupo = IdPunto_cargo_grupo });
            ViewBag.IdPunto_cargo_grupo = IdPunto_cargo_grupo;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_punto_cargo_Info model)
        {
            model.IdUsuarioAnulacion = Convert.ToString(SessionFixed.IdUsuario);
            if (!bus_punto_cargo.AnularDB(model))
            {
                ViewBag.IdPunto_cargo_grupo = model.IdPunto_cargo_grupo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdPunto_cargo_grupo = model.IdPunto_cargo_grupo });
        }
        #endregion
    }
}