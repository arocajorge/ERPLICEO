using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Facturacion;
using Core.Erp.Info.Facturacion;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    public class FormaPagoController : Controller
    {
        fa_formaPago_Bus bus_forma = new fa_formaPago_Bus();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo()
        {
            fa_formaPago_Info model = new fa_formaPago_Info();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_formaPago_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;

            if (bus_forma.ValidarIdFormaPago(model.IdFormaPago))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                return View(model);
            }

            if (!bus_forma.GuardarDB(model))
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }
        public ActionResult Modificar(string IdFormaPago = "")
        {
            fa_formaPago_Info model = bus_forma.GetInfo(IdFormaPago);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_formaPago_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;

            if (!bus_forma.ModificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");

        }
        public ActionResult Anular(string IdFormaPago = "")
        {
            fa_formaPago_Info model = bus_forma.GetInfo(IdFormaPago);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_formaPago_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;

            if (!bus_forma.AnularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");

        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_forma_pago()
        {
            var model = bus_forma.get_list(true);
            return PartialView("_GridViewPartial_forma_pago", model);
        }
    }
}