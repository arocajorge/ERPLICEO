using Core.Erp.Bus.Facturacion;
using Core.Erp.Info.Facturacion;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    public class MotivoTrasladoController : Controller
    {
        #region Index
        fa_MotivoTraslado_Bus bus_motivo = new fa_MotivoTraslado_Bus();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GridViewPartial_Motivo_traslado()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_motivo.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_Motivo_traslado", model);
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo()
        {
            fa_MotivoTraslado_Info model = new fa_MotivoTraslado_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(fa_MotivoTraslado_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_motivo.GuardarDB(model))
            {
                return View(model);

            }
            return RedirectToAction("Index");

        }
        public ActionResult Modificar(int IdEmpresa = 0,int IdMotivoTraslado = 0)
        {
            fa_MotivoTraslado_Info model = bus_motivo.GetInfo(IdEmpresa, IdMotivoTraslado);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(fa_MotivoTraslado_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_motivo.ModificarDB(model))

            {
                return View(model);

            }
            return RedirectToAction("Index");

        }
        public ActionResult Anular(int IdEmpresa = 0, int IdMotivoTraslado = 0)
        {
            fa_MotivoTraslado_Info model = bus_motivo.GetInfo(IdEmpresa, IdMotivoTraslado);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(fa_MotivoTraslado_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_motivo.AnularDB(model))
            {
                return View(model);

            }
            return RedirectToAction("Index");

        }
        #endregion
    }
}