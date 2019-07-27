using Core.Erp.Bus.Caja;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Caja;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Caja.Controllers
{
    [SessionTimeout]
    public class TipoMovimientoCajaController : Controller
    {
        #region variables
        caj_Caja_Movimiento_Tipo_Bus bus_tipomovimiento = new caj_Caja_Movimiento_Tipo_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda

        public ActionResult CmbCuenta_Tipo_movimiento()
        {
            caj_Caja_Movimiento_Tipo_Info model = new caj_Caja_Movimiento_Tipo_Info();
            return PartialView("_CmbCuenta_Tipo_movimiento", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipomovimientocaja()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<caj_Caja_Movimiento_Tipo_Info> model = bus_tipomovimiento.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_tipomovimientocaja", model);
        }


        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            var lst_cuentas = bus_plancta.get_list(IdEmpresa, false, false);
            ViewBag.lst_cuentas = lst_cuentas;

            Dictionary<string, string> lst_signo = new Dictionary<string, string>();
            lst_signo.Add("+", "+");
            lst_signo.Add("-", "-");
            ViewBag.lst_signo = lst_signo;
        }


        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            caj_Caja_Movimiento_Tipo_Info model = new caj_Caja_Movimiento_Tipo_Info
            {
                IdEmpresa = IdEmpresa
            };
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(caj_Caja_Movimiento_Tipo_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_tipomovimiento.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0 , int IdTipoMovi = 0)
        {
            caj_Caja_Movimiento_Tipo_Info model = bus_tipomovimiento.get_info(IdEmpresa, IdTipoMovi);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(caj_Caja_Movimiento_Tipo_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_tipomovimiento.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdTipoMovi = 0)
        {
            caj_Caja_Movimiento_Tipo_Info model = bus_tipomovimiento.get_info(IdEmpresa, IdTipoMovi);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(caj_Caja_Movimiento_Tipo_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            if (!bus_tipomovimiento.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}