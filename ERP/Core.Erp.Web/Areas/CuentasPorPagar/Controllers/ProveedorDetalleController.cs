using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Bus.General;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    public class ProveedorDetalleController : Controller
    {
        #region Variables
        cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
        cp_proveedor_detalle_Bus bus_proveedor_det = new cp_proveedor_detalle_Bus();
        tb_banco_Bus bus_banco = new tb_banco_Bus();
        tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda banco
        public ActionResult CmbBanco_ProveedorDetalle()
        {
            int model = new int();
            return PartialView("_CmbBanco_ProveedorDetalle", model);
        }
        public List<tb_banco_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_banco.get_list_bajo_demanda(args);
        }
        public tb_banco_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_banco.get_info_bajo_demanda(args);
        }
        #endregion

        #region Index
        public ActionResult Index(int IdEmpresa = 0, int IdProveedor = 0)
        {
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdProveedor = IdProveedor;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Cuentas_x_Proveedor(int IdEmpresa = 0, int IdProveedor = 0)
        {
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdProveedor = IdProveedor;
            List<cp_proveedor_detalle_Info> model = bus_proveedor_det.get_list(IdEmpresa, IdProveedor, true);
            return PartialView("_GridViewPartial_Cuentas_x_Proveedor", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0, int IdProveedor = 0)
        {
            cp_proveedor_Info info_proveedor = bus_proveedor.get_info(IdEmpresa,IdProveedor);
            cp_proveedor_detalle_Info model = new cp_proveedor_detalle_Info
            {
                IdEmpresa = IdEmpresa,
                IdProveedor = IdProveedor,
                NombreAdicional = (info_proveedor.info_persona  == null)? "" : info_proveedor.info_persona.pe_nombreCompleto
            };
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdProveedor = IdProveedor;
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(cp_proveedor_detalle_Info model)
        {
            if (!bus_proveedor_det.guardarDB(model))
            {
                ViewBag.IdEmpresa = model.IdEmpresa;
                ViewBag.IdProveedor = model.IdProveedor;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdProveedor = model.IdProveedor });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdProveedor = 0, int Secuencia = 0)
        {
            cp_proveedor_detalle_Info model = bus_proveedor_det.get_info(IdEmpresa, IdProveedor, Secuencia);
            if (model == null)
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdProveedor = IdProveedor });
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdProveedor = IdProveedor;
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cp_proveedor_detalle_Info model)
        {
            if (!bus_proveedor_det.modificarDB(model))
            {
                ViewBag.IdEmpresa = model.IdEmpresa;
                ViewBag.IdProveedor = model.IdProveedor;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdProveedor = model.IdProveedor });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdProveedor = 0, int Secuencia = 0)
        {
            cp_proveedor_detalle_Info model = bus_proveedor_det.get_info(IdEmpresa, IdProveedor, Secuencia);
            if (model == null)
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdProveedor = IdProveedor });
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdProveedor = IdProveedor;
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cp_proveedor_detalle_Info model)
        {
            if (!bus_proveedor_det.anularDB(model))
            {
                ViewBag.IdEmpresa = model.IdEmpresa;
                ViewBag.IdProveedor = model.IdProveedor;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdProveedor = model.IdProveedor });
        }

        #endregion

        private void cargar_combos(int IdEmpresa)
        {
            var lst_banco = bus_banco.get_list(false);
            ViewBag.lst_banco = lst_banco;

            var lst_tipo_cta = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIP_CTA_AC), false);
            ViewBag.lst_tipo_cta = lst_tipo_cta;

        }
    }
}