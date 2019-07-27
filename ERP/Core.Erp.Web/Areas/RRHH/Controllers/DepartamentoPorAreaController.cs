using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using DevExpress.Web.Mvc;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class DepartamentoPorAreaController : Controller
    {
        #region variables
        ro_area_x_departamento_Bus bus_area_x_departamento = new ro_area_x_departamento_Bus();
        List<ro_area_x_departamento_Info> lst_departamento_area = new List<ro_area_x_departamento_Info>();
        ro_division_Bus bus_divisiaon = new ro_division_Bus();
        ro_area_Bus bus_area = new ro_area_Bus();
        ro_departamento_Bus bus_departamento = new ro_departamento_Bus();

        ro_departamento_Info info = new ro_departamento_Info();
        int IdEmpresa = 0;
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_departamento_area()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"].ToString());
            lst_departamento_area = bus_area_x_departamento.get_list(IdEmpresa);
            return PartialView("_GridViewPartial_departamento_area", lst_departamento_area);
        }

        [ValidateInput(false)]
        public ActionResult Nuevo()
        {
            cargar_combos();
            ro_area_x_departamento_Info info = new ro_area_x_departamento_Info();
            return View(info);
        }
        [HttpPost]
        public ActionResult Nuevo(ro_area_x_departamento_Info model)
        {
            if (model == null)
                return View(model);
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (bus_area_x_departamento.guardarDB(model))
                return RedirectToAction("Index");
            else
            {
                cargar_combos();
                return View(model);
            }
        }
        public ActionResult Modificar(int Secuencia)
        {
            cargar_combos();
            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            return View(bus_area_x_departamento.get_info(IdEmpresa, Secuencia));
        }
        [HttpPost]
        public ActionResult Modificar(ro_area_x_departamento_Info model)
        {

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (!bus_area_x_departamento.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }
        public ActionResult Anular(int Secuencia)
        {
            cargar_combos();
            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            return View(bus_area_x_departamento.get_info(IdEmpresa, Secuencia));
        }
        [HttpPost]
        public ActionResult Anular(ro_area_x_departamento_Info model)
        {

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (!bus_area_x_departamento.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }
        private void cargar_combos()
        {
            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"].ToString());
            ViewBag.lst_division = bus_divisiaon.get_list(IdEmpresa, false);
            ViewBag.lst_area = bus_area.get_list(IdEmpresa, false);
            ViewBag.lst_departamento = bus_departamento.get_list(IdEmpresa, false);
        }

    }
}