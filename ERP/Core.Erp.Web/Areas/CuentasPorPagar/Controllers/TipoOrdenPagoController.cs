using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    [SessionTimeout]
    public class TipoOrdenPagoController : Controller
    {
        #region Variables
        cp_orden_pago_tipo_x_empresa_Bus bus_tipo_op = new cp_orden_pago_tipo_x_empresa_Bus();
        ct_plancta_Bus bus_pla_cuenta = new ct_plancta_Bus();
        ct_cbtecble_tipo_Bus bus_tipo_comprobante = new ct_cbtecble_tipo_Bus();
        cp_orden_pago_estado_aprob_Bus bus_estado_op = new cp_orden_pago_estado_aprob_Bus();

        #endregion

        #region Metodos ComboBox bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_Tipo()
        {
            cp_orden_pago_tipo_x_empresa_Info model = new cp_orden_pago_tipo_x_empresa_Info();
            return PartialView("_CmbCuenta_Tipo", model);
        }
        public ActionResult CmbCuenta_credito_Tipo()
        {
            cp_orden_pago_tipo_x_empresa_Info model = new cp_orden_pago_tipo_x_empresa_Info();
            return PartialView("_CmbCuenta_credito_tipo", model);
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
        public ActionResult GridViewPartial_tipo_orden_pago()
        {
            cp_orden_pago_tipo_Bus bus_top = new cp_orden_pago_tipo_Bus();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_top.get_list();
            return PartialView("_GridViewPartial_tipo_orden_pago", model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            ViewBag.lst_tipo_comprobante = bus_tipo_comprobante.get_list(IdEmpresa, false);
            ViewBag.lst_cuenta_contable = bus_pla_cuenta.get_list(IdEmpresa, false, true);
            ViewBag.lst_estado = bus_estado_op.get_list();
        }


        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0 )
        {
            cp_orden_pago_tipo_x_empresa_Info model = new cp_orden_pago_tipo_x_empresa_Info
            {
                IdEmpresa = IdEmpresa
            };
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cp_orden_pago_tipo_x_empresa_Info model)
        {
            model.IdEmpresa = model.IdEmpresa;
            if (bus_tipo_op.si_existe(model))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = "El código ya se encuentra registrado";
                return View(model);
            }

            if (!bus_tipo_op.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);

                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, string IdTipo_op = "")
        {
            cargar_combos(IdEmpresa);
            cp_orden_pago_tipo_x_empresa_Info model = bus_tipo_op.get_info(IdEmpresa, IdTipo_op);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cp_orden_pago_tipo_x_empresa_Info model)
        {
            if (!bus_tipo_op.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0 , string IdTipo_op = "")
        {

            cargar_combos(IdEmpresa);
            cp_orden_pago_tipo_x_empresa_Info model = bus_tipo_op.get_info(IdEmpresa, IdTipo_op);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cp_orden_pago_tipo_x_empresa_Info model)
        {
            if (!bus_tipo_op.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);

                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion

        
    }
}