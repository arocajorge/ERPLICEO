using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class TipoMovimientoController : Controller
    {
        #region Variables

        in_movi_inven_tipo_Bus bus_tipo_movimiento = new in_movi_inven_tipo_Bus();
        ct_cbtecble_tipo_Bus bus_tipo_comprobante = new ct_cbtecble_tipo_Bus();
        in_Catalogo_Bus bus_catalogo = new in_Catalogo_Bus();
        #endregion
        #region Index/Metodos

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipo_movimiento()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_tipo_movimiento.get_list(IdEmpresa, "", true);
            return PartialView("_GridViewPartial_tipo_movimiento", model);
        }

        private void cargar_combos(in_movi_inven_tipo_Info model)
        {
            Dictionary<string, string> lst_signo = new Dictionary<string, string>();
            lst_signo.Add("+", "+");
            lst_signo.Add("-", "-");
            ViewBag.lst_signo = lst_signo;
            
            var lst_tipo_comprobante = bus_tipo_comprobante.get_list(model.IdEmpresa, false);
            ViewBag.lst_tipo_comprobante = lst_tipo_comprobante;

            var lst_CatalogoAprobacion = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoInventario.EST_APROB),false);
            ViewBag.lst_CatalogoAprobacion = lst_CatalogoAprobacion;
        }
        #endregion
        #region Acciones

        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            in_movi_inven_tipo_Info model = new in_movi_inven_tipo_Info
            {
                IdEmpresa = IdEmpresa
            };
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(in_movi_inven_tipo_Info model)
        {
            if (!bus_tipo_movimiento.guardarDB(model))
            {
                cargar_combos(model);
                return View(model);
            }
            /*
            model.lst_tipo_mov_x_bodega.ForEach(q => { q.IdEmpresa = model.IdEmpresa; q.IdMovi_inven_tipo = model.IdMovi_inven_tipo; });
            bus_tipo_movimiento_x_bodega.guardarDB(model.lst_tipo_mov_x_bodega.Where(q => q.seleccionado == true).ToList());
            */
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0 , int IdMovi_inven_tipo = 0)
        {
            in_movi_inven_tipo_Info model = bus_tipo_movimiento.get_info(IdEmpresa,IdMovi_inven_tipo);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(in_movi_inven_tipo_Info model)
        {
            if (!bus_tipo_movimiento.modificarDB(model))
            {
                cargar_combos(model);
                return View(model);
            }
            /*
            bus_tipo_movimiento_x_bodega.eliminarDB(model.IdEmpresa, model.IdMovi_inven_tipo);
            model.lst_tipo_mov_x_bodega.ForEach(q => { q.IdEmpresa = model.IdEmpresa; q.IdMovi_inven_tipo = model.IdMovi_inven_tipo; });
            bus_tipo_movimiento_x_bodega.guardarDB(model.lst_tipo_mov_x_bodega.Where(q=>q.seleccionado == true).ToList());
            */
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0 , int IdMovi_inven_tipo = 0)
        {
            in_movi_inven_tipo_Info model = bus_tipo_movimiento.get_info(IdEmpresa, IdMovi_inven_tipo);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(in_movi_inven_tipo_Info model)
        {
            if (!bus_tipo_movimiento.anularDB(model))
            {
                cargar_combos(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}