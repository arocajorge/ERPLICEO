using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    public class CierreAnualController : Controller
    {
        #region Variables
        ct_cbtecble_Bus bus_comprobante = new ct_cbtecble_Bus();
        ct_anio_fiscal_x_tb_sucursal_Bus bus_cierreanual = new ct_anio_fiscal_x_tb_sucursal_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        ct_cbtecble_det_Bus bus_comprobante_detalle = new ct_cbtecble_det_Bus();
        ct_cbtecble_det_List list_ct_cbtecble_det = new ct_cbtecble_det_List();
        string mensaje = string.Empty;
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ct_parametro_Bus bus_parametro = new ct_parametro_Bus();
        ct_anio_fiscal_Bus bus_anio = new ct_anio_fiscal_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda

        public ActionResult CmbCuenta()
        {
            ct_anio_fiscal_x_tb_sucursal_Info model = new ct_anio_fiscal_x_tb_sucursal_Info();
            return PartialView("_CmbCuenta", model);
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
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal)
            };
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_CierreAnual(int IdSucursal = 0)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdSucursal = IdSucursal;
            List<ct_anio_fiscal_x_tb_sucursal_Info> model = bus_cierreanual.get_list(IdEmpresa, IdSucursal);
            return PartialView("_GridViewPartial_CierreAnual", model);
        }

        public void CargarCombosConsulta(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        #endregion

        #region Metodos
        private bool validar(ct_anio_fiscal_x_tb_sucursal_Info i_validar, ref string msg)
        {
            if (i_validar.info_cbtecble_det.Count == 0)
            {
                mensaje = "Debe ingresar registros en el detalle";
                return false;
            }

            if (Math.Round(i_validar.info_cbtecble_det.Sum(q => q.dc_Valor), 2) != 0)
            {
                mensaje = "La suma de los detalles debe ser 0";
                return false;
            }

            if (i_validar.info_cbtecble_det.Where(q => q.dc_Valor == 0).Count() > 0)
            {
                mensaje = "Existen detalles con valor 0 en el debe o haber";
                return false;
            }

            if (i_validar.info_cbtecble_det.Where(q => string.IsNullOrEmpty(q.IdCtaCble)).Count() > 0)
            {
                mensaje = "Existen detalles sin cuenta contable";
                return false;
            }

            return true;
        }
        private void cargar_combos(ct_anio_fiscal_x_tb_sucursal_Info model)
        {
            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_anios = bus_anio.get_list(false);
            ViewBag.lst_anios = lst_anios;

            var lst_anios_sin_cierre = bus_anio.get_list_anio_sincierre(model.IdEmpresa, model.IdSucursal);
            ViewBag.lst_anios_sin_cierre = lst_anios_sin_cierre;
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

            var info_parametro = bus_parametro.get_info(IdEmpresa);

            ct_anio_fiscal_x_tb_sucursal_Info model = new ct_anio_fiscal_x_tb_sucursal_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdTipoCbte = info_parametro.IdTipoCbte_AsientoCierre_Anual,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            model.info_cbtecble = new ct_cbtecble_det_Info();
            model.info_cbtecble_det = new List<ct_cbtecble_det_Info>();
            list_ct_cbtecble_det.set_list(model.info_cbtecble_det, model.IdTransaccionSession);
            cargar_combos(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ct_anio_fiscal_x_tb_sucursal_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            model.info_cbtecble_det = list_ct_cbtecble_det.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            
            if (!bus_cierreanual.guardarDB(model))
            {
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursal = 0, int IdanioFiscal = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ct_anio_fiscal_x_tb_sucursal_Info model = bus_cierreanual.get_info(IdEmpresa, IdSucursal, IdanioFiscal);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.info_cbtecble_det = bus_comprobante_detalle.get_list(IdEmpresa, model.IdTipoCbte, model.IdCbteCble);
            list_ct_cbtecble_det.set_list(model.info_cbtecble_det, model.IdTransaccionSession);
            cargar_combos(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ct_anio_fiscal_x_tb_sucursal_Info model)
        {
            model.info_cbtecble_det = list_ct_cbtecble_det.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_cierreanual.modificarDB(model))
            {
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdSucursal = 0, int IdanioFiscal = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ct_anio_fiscal_x_tb_sucursal_Info model = bus_cierreanual.get_info(IdEmpresa, IdSucursal, IdanioFiscal);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.info_cbtecble_det = bus_comprobante_detalle.get_list(IdEmpresa, model.IdTipoCbte, model.IdCbteCble);
            list_ct_cbtecble_det.set_list(model.info_cbtecble_det, model.IdTransaccionSession);

            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_anio_fiscal_x_tb_sucursal_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_cierreanual.anularDB(model))
            {
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Json
        public JsonResult CargarDetalleCbte(int IdEmpresa = 0, int IdSucursal = 0, int IdanioFiscal = 0, decimal IdTransaccionSession = 0)
        {
            var lst = bus_comprobante_detalle.get_list_para_cierre(IdEmpresa, IdSucursal, IdanioFiscal);
            list_ct_cbtecble_det.set_list(lst, IdTransaccionSession);

            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}