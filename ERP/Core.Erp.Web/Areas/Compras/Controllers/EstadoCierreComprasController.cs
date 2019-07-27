using Core.Erp.Bus.Compras;
using Core.Erp.Info.Compras;
using Core.Erp.Web.Helps;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Compras.Controllers
{
    [SessionTimeout]
    public class EstadoCierreComprasController : Controller
    {
        #region Index
        com_estado_cierre_Bus bus_estado = new com_estado_cierre_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_estadocierre()
        {
            var model = bus_estado.get_list(true);
            return PartialView("_GridViewPartial_estadocierre", model);
        }

        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            com_estado_cierre_Info model = new com_estado_cierre_Info();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(com_estado_cierre_Info model)
        {
            if (bus_estado.validar_existe_IdEstado(model.IdEstado_cierre))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                return View(model);
            }
            if (!bus_estado.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(string IdEstado_cierre = "")
        {
            com_estado_cierre_Info model = bus_estado.get_info( IdEstado_cierre);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(com_estado_cierre_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_estado.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(string IdEstado_cierre = "")
        {
            com_estado_cierre_Info model = bus_estado.get_info(IdEstado_cierre);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(com_estado_cierre_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            if (!bus_estado.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}