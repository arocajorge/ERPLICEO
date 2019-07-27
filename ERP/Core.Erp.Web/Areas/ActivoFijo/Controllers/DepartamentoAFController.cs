using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.ActivoFijo;
using Core.Erp.Bus.ActivoFijo;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.ActivoFijo.Controllers
{
    public class DepartamentoAFController : Controller
    {
        Af_Departamento_Bus bus_dep = new Af_Departamento_Bus();
        public ActionResult Index(decimal IdArea = 0)
        {
            ViewBag.IdArea = IdArea;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_departamento_af(decimal IdArea = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdArea = IdArea;
            var model = bus_dep.GetList(IdEmpresa,IdArea, true);
            return PartialView("_GridViewPartial_departamento_af", model);
        }

        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            Af_Area_Bus bus_area = new Af_Area_Bus();
            var lst_area = bus_area.GetList(IdEmpresa, false);
            ViewBag.lst_area = lst_area;
        }

        #region Acciones
        public ActionResult Nuevo(decimal IdArea = 0)
        {
            Af_Departamento_Info model = new Af_Departamento_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdArea = IdArea
            };
            ViewBag.IdArea = model.IdArea;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(Af_Departamento_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_dep.GuardarDB(model))
            {
                ViewBag.IdArea = model.IdArea;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdArea = model.IdArea });
        }

        public ActionResult Modificar(int IdEmpresa = 0 , decimal IdArea = 0, decimal IdDepartamento = 0)
        {
            Af_Departamento_Info model = bus_dep.GetInfo(IdEmpresa,IdArea, IdDepartamento);
            if (model == null)
                return RedirectToAction("Index", new { IdArea = IdArea });
            ViewBag.IdArea = model.IdArea;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(Af_Departamento_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_dep.ModificarDB(model))
            {
                ViewBag.IdArea = model.IdArea;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdArea = model.IdArea });
        }
        public ActionResult Anular(int IdEmpresa = 0, decimal IdArea = 0, decimal IdDepartamento = 0)
        {
            Af_Departamento_Info model = bus_dep.GetInfo(IdEmpresa,IdArea, IdDepartamento);
            if (model == null)
                return RedirectToAction("Index", new { IdArea = IdArea });
            ViewBag.IdArea = model.IdArea;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(Af_Departamento_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_dep.AnularDB(model))
            {
                cargar_combos();
                ViewBag.IdArea = model.IdArea;
                return View(model);
            }
            return RedirectToAction("Index", new { IdArea = model.IdArea });
        }

        #endregion

    }

    public class Af_Departamento_List
    {
        string Variable = "Af_Departamento_Info";
        public List<Af_Departamento_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<Af_Departamento_Info> list = new List<Af_Departamento_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<Af_Departamento_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<Af_Departamento_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}