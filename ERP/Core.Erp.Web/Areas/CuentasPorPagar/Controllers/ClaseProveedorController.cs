using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    [SessionTimeout]
    public class ClaseProveedorController : Controller
    {
        #region Variable
        cp_proveedor_clase_Bus bus_clase_proveedor = new cp_proveedor_clase_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();

        #endregion
        #region Metodos ComboBox bajo demanda CtaCbleCXP
        public ActionResult CmbCtaCbleCXP_Proveedor()
        {
            int model = new int();
            return PartialView("_CmbCtaCbleCXP_Proveedor", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_ctacble_cxp(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_plancta.get_list_bajo_demanda(args, IdEmpresa, true);
        }
        public ct_plancta_Info get_info_bajo_demanda_ctacble_cxp(ListEditItemRequestedByValueEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_plancta.get_info_bajo_demanda(args, IdEmpresa);
        }
        #endregion

        #region Metodos ComboBox bajo demanda CtaCbleGasto
        public ActionResult CmbCtaCbleGasto_Proveedor()
        {
            int model = new int();
            return PartialView("_CmbCtaCbleGasto_Proveedor", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_ctacble_gasto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_plancta.get_list_bajo_demanda(args, IdEmpresa, true);
        }
        public ct_plancta_Info get_info_bajo_demanda_ctacble_gasto(ListEditItemRequestedByValueEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_plancta.get_info_bajo_demanda(args, IdEmpresa);
        }
        #endregion

        #region Metodos ComboBox bajo demanda CtaCbleGasto
        public ActionResult CmbCtaCbleAnticipo_Proveedor()
        {
            int model = new int();
            return PartialView("_CmbCtaCbleAnticipo_Proveedor", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_ctacble_Anticipo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_plancta.get_list_bajo_demanda(args, IdEmpresa, true);
        }
        public ct_plancta_Info get_info_bajo_demanda_ctacble_Anticipo(ListEditItemRequestedByValueEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_plancta.get_info_bajo_demanda(args, IdEmpresa);
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_clase_proveedor()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_clase_proveedor.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_clase_proveedor", model);
        }
        private void cargar_combos()
        {
            var lst_ctacble = bus_plancta.get_list(Convert.ToInt32(Session["IdEmpresa"]), false, true);
            ViewBag.lst_cuentas = lst_ctacble;
        }

        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0, int IdClaseProveedor = 0)
        {
            cp_proveedor_clase_Info model = new cp_proveedor_clase_Info
            {
                IdEmpresa = IdEmpresa
            };
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cp_proveedor_clase_Info model)
        {
            if (!bus_clase_proveedor.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdClaseProveedor = 0)
        {
            cp_proveedor_clase_Info model = bus_clase_proveedor.get_info(Convert.ToInt32(Session["IdEmpresa"]), IdClaseProveedor);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cp_proveedor_clase_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (!bus_clase_proveedor.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdClaseProveedor = 0)
        {
            cp_proveedor_clase_Info model = bus_clase_proveedor.get_info(Convert.ToInt32(Session["IdEmpresa"]), IdClaseProveedor);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cp_proveedor_clase_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (!bus_clase_proveedor.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }

    public class cp_proveedor_clase_List
    {
        string Variable = "cp_proveedor_clase_Info";
        public List<cp_proveedor_clase_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_proveedor_clase_Info> list = new List<cp_proveedor_clase_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_proveedor_clase_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_proveedor_clase_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}