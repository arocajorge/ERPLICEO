using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class BodegaController : Controller
    {
        #region Variables
        tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        #endregion

        #region Index
        public ActionResult Index(int IdEmpresa = 0 , int IdSucursal = 0)
        {
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_bodega(int IdEmpresa = 0 , int IdSucursal = 0)
        {
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            List<tb_bodega_Info> model = bus_bodega.get_list(IdEmpresa, IdSucursal, true);
            return PartialView("_GridViewPartial_bodega", model);
        }

        private void cargar_combos(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_cuentas = bus_plancta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_cuentas;

        }

        #endregion

        #region Acciones
        public ActionResult Nuevo(int  IdEmpresa = 0 , int IdSucursal = 0)
        {
            tb_bodega_Info model = new tb_bodega_Info {
                IdEmpresa = IdEmpresa,
                IdSucursal = IdSucursal
            };
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(tb_bodega_Info model)
        {            
            if (!bus_bodega.guardarDB(model))
            {
                ViewBag.IdEmpresa = model.IdEmpresa;
                ViewBag.IdSucursal = model.IdSucursal;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal });
        }

        public ActionResult Modificar(int IdEmpresa = 0 , int IdSucursal = 0, int IdBodega = 0)
        {
            tb_bodega_Info model = bus_bodega.get_info(IdEmpresa, IdSucursal, IdBodega);
            if (model == null)
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdSucursal = IdSucursal });
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(tb_bodega_Info model)
        {
            if (!bus_bodega.modificarDB(model))
            {
                ViewBag.IdEmpresa = model.IdEmpresa;
                ViewBag.IdSucursal = model.IdSucursal;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal });
        }

        public ActionResult Anular(int  IdEmpresa = 0 , int IdSucursal = 0, int IdBodega = 0)
        {
            tb_bodega_Info model = bus_bodega.get_info(IdEmpresa, IdSucursal, IdBodega);
            if (model == null)
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdSucursal = IdSucursal });
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(tb_bodega_Info model)
        {
            if (!bus_bodega.anularDB(model))
            {
                ViewBag.IdEmpresa = model.IdEmpresa;
                ViewBag.IdSucursal = model.IdSucursal;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal });
        }

        #endregion
    }
    public class tb_bodega_List
    {
        string Variable = "tb_bodega_Info";
        public List<tb_bodega_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<tb_bodega_Info> list = new List<tb_bodega_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<tb_bodega_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<tb_bodega_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

}