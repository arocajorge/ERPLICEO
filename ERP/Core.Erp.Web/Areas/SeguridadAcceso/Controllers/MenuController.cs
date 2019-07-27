using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.SeguridadAcceso;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.SeguridadAcceso.Controllers
{
    //[SessionTimeout]
    public class MenuController : Controller
    {
        #region Index/Metodos

        seg_Menu_Bus bus_menu = new seg_Menu_Bus();
        public ActionResult Index()
        {
            return View();
        }
        private void cargar_combos()
        {
            var lst_menu = bus_menu.get_list_combo(false);
            lst_menu.Add(new seg_Menu_Info { DescripcionMenu_combo = "== Seleccione =="});
            ViewBag.lst_menu = lst_menu;
        }

        #endregion
        [ValidateInput(false)]
        public ActionResult TreeListPartial_menu()
        {
            List<seg_Menu_Info> model = bus_menu.get_list(true);
            return PartialView("_TreeListPartial_menu", model);
        }

        #region Acciones

        public ActionResult Nuevo()
        {
            seg_Menu_Info model = new seg_Menu_Info();
            cargar_combos();
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(seg_Menu_Info model)
        {
            if (!bus_menu.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdMenu = 0)
        {
            seg_Menu_Info model = bus_menu.get_info(IdMenu);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(seg_Menu_Info model)
        {
            if (!bus_menu.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdMenu = 0)
        {
            seg_Menu_Info model = bus_menu.get_info(IdMenu);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(seg_Menu_Info model)
        {
            if (!bus_menu.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}