using Core.Erp.Bus.Presupuesto;
using Core.Erp.Info.Presupuesto;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Presupuesto.Controllers
{
    public class RubroTipoPresupuestoController : Controller
    {
        // GET: Presupuesto/TipoRubroPresupuesto
        #region Variables
            pre_RubroTipo_Bus bus_RubroTipo = new pre_RubroTipo_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_RubroTipo()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<pre_RubroTipo_Info> model = bus_RubroTipo.GetList(IdEmpresa, true);
            return PartialView("_GridViewPartial_RubroTipo", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            Dictionary<string, string> lst_signo = new Dictionary<string, string>();
            lst_signo.Add("+", "+");
            lst_signo.Add("-", "-");
            ViewBag.lst_signo = lst_signo;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            pre_RubroTipo_Info model = new pre_RubroTipo_Info();
            cargar_combos(IdEmpresa);
            return View(model);

        }

        [HttpPost]
        public ActionResult Nuevo(pre_RubroTipo_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_RubroTipo.GuardarBD(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdRubroTipo = 0)
        {
            pre_RubroTipo_Info model = bus_RubroTipo.GetInfo(IdEmpresa, IdRubroTipo);
            if (model == null)
                return RedirectToAction("Index");

            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(pre_RubroTipo_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_RubroTipo.ModificarBD(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdRubroTipo = 0)
        {
            pre_RubroTipo_Info model = bus_RubroTipo.GetInfo(IdEmpresa, IdRubroTipo);
            if (model == null)
                return RedirectToAction("Index");

            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(pre_RubroTipo_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_RubroTipo.AnularBD(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}