using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using System;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class TransportistaController : Controller
    {
        #region Index

        tb_transportista_Bus bus_transportista = new tb_transportista_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_transportista()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_transportista.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_transportista", model);
        }
        #endregion
        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            tb_transportista_Info model = new tb_transportista_Info
            {
                IdEmpresa = IdEmpresa
            };
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(tb_transportista_Info model)
        {
            if(!bus_transportista.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0 , decimal IdTransportista = 0)
        {
            tb_transportista_Info model = bus_transportista.get_info(IdEmpresa, IdTransportista);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(tb_transportista_Info model)
        {
            if (!bus_transportista.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0 , decimal IdTransportista = 0)
        {
            tb_transportista_Info model = bus_transportista.get_info(IdEmpresa, IdTransportista);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(tb_transportista_Info model)
        {
            if (!bus_transportista.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}