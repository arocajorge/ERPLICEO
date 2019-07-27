using Core.Erp.Bus.Banco;
using Core.Erp.Bus.General;
using Core.Erp.Info.Banco;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    public class TipoFlujoMovimientoController : Controller
    {
        #region Variables
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ba_TipoFlujo_Bus bus_TipoFlujo = new ba_TipoFlujo_Bus();
        ba_Banco_Cuenta_Bus bus_banco_cuenta = new ba_Banco_Cuenta_Bus();
        ba_TipoFlujo_Movimiento_Bus bus_TipoFlujo_Movimiento = new ba_TipoFlujo_Movimiento_Bus();
        string mensaje = string.Empty;
        #endregion

        #region Combo bajo demanda CmbTipoFlujo

        public ActionResult CmbTipoFlujo()
        {
            ba_TipoFlujo_Movimiento_Info model = new ba_TipoFlujo_Movimiento_Info();
            return PartialView("_CmbTipoFlujo", model);
        }
        public List<ba_TipoFlujo_Info> get_list_bajo_demanda_tipoflujo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var TipoFlujo_GetList = bus_TipoFlujo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
            return TipoFlujo_GetList;
        }
        public ba_TipoFlujo_Info get_info_bajo_demanda_tipoflujo(ListEditItemRequestedByValueEventArgs args)
        {
            var TipoFlujo_GetInfo = bus_TipoFlujo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
            return TipoFlujo_GetInfo;
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_TipoFlujoMovimiento()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ba_TipoFlujo_Movimiento_Info> model = bus_TipoFlujo_Movimiento.GetList(IdEmpresa, true);

            return PartialView("_GridViewPartial_TipoFlujoMovimiento", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa, int IdSucursal)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_banco_cuenta = bus_banco_cuenta.get_list(IdEmpresa, IdSucursal, false);
            ViewBag.lst_banco_cuenta = lst_banco_cuenta;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ba_TipoFlujo_Movimiento_Info model = new ba_TipoFlujo_Movimiento_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdUsuarioCreacion = SessionFixed.IdUsuario
            };

            cargar_combos(IdEmpresa, model.IdSucursal);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ba_TipoFlujo_Movimiento_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;

            if (!bus_TipoFlujo_Movimiento.GuardarBD(model))
            {
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, decimal IdMovimiento = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ba_TipoFlujo_Movimiento_Info model = bus_TipoFlujo_Movimiento.GetInfo(IdEmpresa, IdMovimiento);

            if (model == null)
                return RedirectToAction("Index");

            cargar_combos(IdEmpresa, model.IdSucursal);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ba_TipoFlujo_Movimiento_Info model)
        {
            model.IdUsuarioModificacion = Session["IdUsuario"].ToString();

            if (!bus_TipoFlujo_Movimiento.ModificarBD(model))
            {
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdMovimiento = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            #endregion

            ba_TipoFlujo_Movimiento_Info model = bus_TipoFlujo_Movimiento.GetInfo(IdEmpresa, IdMovimiento);
            if (model == null)
                return RedirectToAction("Index");

            cargar_combos(IdEmpresa, model.IdSucursal);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ba_TipoFlujo_Movimiento_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;

            if (!bus_TipoFlujo_Movimiento.AnularBD(model))
            {
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}