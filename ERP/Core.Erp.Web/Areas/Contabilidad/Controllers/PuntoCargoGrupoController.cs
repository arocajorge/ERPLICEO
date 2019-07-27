using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    public class PuntoCargoGrupoController : Controller
    {
        #region Index
        ct_punto_cargo_grupo_Bus bus_punto = new ct_punto_cargo_grupo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_punto_cargo_grupo()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_punto.GetList(IdEmpresa, true);
            return PartialView("_GridViewPartial_punto_cargo_grupo", model);
        }

        #endregion
        #region Acciones
        
        public ActionResult Nuevo()
        {
            ct_punto_cargo_grupo_Info model = new ct_punto_cargo_grupo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ct_punto_cargo_grupo_Info model)
        {
            model.IdUsuarioCreacion = Convert.ToString(SessionFixed.IdUsuario);
            if (!bus_punto.GuardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdPunto_cargo_grupo = 0)
        {
            ct_punto_cargo_grupo_Info model = bus_punto.GetInfo(IdEmpresa, IdPunto_cargo_grupo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ct_punto_cargo_grupo_Info model)
        {
            model.IdUsuarioModificacion = Convert.ToString(SessionFixed.IdUsuario);
            if (!bus_punto.ModificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdPunto_cargo_grupo = 0)
        {
            ct_punto_cargo_grupo_Info model = bus_punto.GetInfo(IdEmpresa, IdPunto_cargo_grupo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_punto_cargo_grupo_Info model)
        {
            model.IdUsuarioAnulacion = Convert.ToString(SessionFixed.IdUsuario);
            if (!bus_punto.AnularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

    }
}