using Core.Erp.Bus.Compras;
using Core.Erp.Info.Compras;
using Core.Erp.Web.Helps;
using System;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Compras.Controllers
{
    [SessionTimeout]
    public class TerminoPagoComprasController : Controller
    {
        #region Index

        com_TerminoPago_Bus bus_termino = new com_TerminoPago_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_terminopago_com()
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_termino.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_terminopago_com", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo (int IdEmpresa = 0)
        {
            com_TerminoPago_Info model = new com_TerminoPago_Info
            {
                IdEmpresa = IdEmpresa
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(com_TerminoPago_Info model)
        {
            if (!bus_termino.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdTerminoPago = 0)
        {
            com_TerminoPago_Info model = bus_termino.get_info(IdEmpresa, IdTerminoPago);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(com_TerminoPago_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_termino.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdTerminoPago = 0)
        {
            com_TerminoPago_Info model = bus_termino.get_info(IdEmpresa,IdTerminoPago);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(com_TerminoPago_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            if (!bus_termino.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}