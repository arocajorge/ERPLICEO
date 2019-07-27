using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.Inventario;
using Core.Erp.Bus.Inventario;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class ProductoTipoController : Controller
    {
        #region Index

        in_ProductoTipo_Bus bus_producto_tipo = new in_ProductoTipo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipo_producto()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
           var model = bus_producto_tipo.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_tipo_producto", model);
        }
        #endregion
        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            in_ProductoTipo_Info model = new in_ProductoTipo_Info
            {
                IdEmpresa = IdEmpresa
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(in_ProductoTipo_Info model)
        {
            if (!bus_producto_tipo.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0 , int IdProductoTipo = 0)
        {
            in_ProductoTipo_Info model = bus_producto_tipo.get_info(IdEmpresa,IdProductoTipo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(in_ProductoTipo_Info model)
        {
            if (!bus_producto_tipo.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0 , int IdProductoTipo = 0)
        {
            in_ProductoTipo_Info model = bus_producto_tipo.get_info(IdEmpresa, IdProductoTipo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(in_ProductoTipo_Info model)
        {
            if (!bus_producto_tipo.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
        #region json
        public JsonResult get_info_producto_tipo( int IdProductoTipo = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            in_ProductoTipo_Bus bus_producto_tipo = new in_ProductoTipo_Bus();
            var resultado = bus_producto_tipo.get_info(IdEmpresa, IdProductoTipo);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}