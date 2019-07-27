using Core.Erp.Bus.Compras;
using Core.Erp.Info.Compras;
using Core.Erp.Web.Helps;
using System;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Compras.Controllers
{
    [SessionTimeout]
    public class DepartamentoComprasController : Controller
    {
        #region Index

        com_departamento_Bus bus_dpto = new com_departamento_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_departamento()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_dpto.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_departamento", model);
        }
        #endregion

        #region Acciones

        public ActionResult Nuevo (int IdEmpresa = 0)
        {
            com_departamento_Info model = new com_departamento_Info
            {
                IdEmpresa = IdEmpresa
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(com_departamento_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_dpto.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, decimal IdDepartamento = 0)
        {
            com_departamento_Info model = bus_dpto.get_info(IdEmpresa, IdDepartamento);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(com_departamento_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_dpto.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdDepartamento = 0)
        {
            com_departamento_Info model = bus_dpto.get_info(IdEmpresa, IdDepartamento);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(com_departamento_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            if (!bus_dpto.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

    }
}