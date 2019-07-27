using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Web.Helps;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    public class ParametrosCXPController : Controller
    {
        #region Variables
        cp_parametros_Bus bus_parametros = new cp_parametros_Bus();
        ct_plancta_Bus bus_pla_cuenta = new ct_plancta_Bus();
        ct_cbtecble_tipo_Bus bus_tipo_comprobante = new ct_cbtecble_tipo_Bus();
        #endregion
        private void cargar_combos(int IdEmpresa)
        {
            ViewBag.lst_tipo_comprobante = bus_tipo_comprobante.get_list(IdEmpresa, false);
            ViewBag.lst_cuenta_contable = bus_pla_cuenta.get_list(IdEmpresa, false,true);
        }
       
        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            cp_parametros_Info model = bus_parametros.get_info(IdEmpresa);
            if (model == null)
                model = new cp_parametros_Info { IdEmpresa = IdEmpresa };
            cargar_combos(IdEmpresa);

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cp_parametros_Info model)
        {
            if (!bus_parametros.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");

        }

        #region Metodos ComboBox bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_Param1()
        {
            cp_parametros_Info model = new cp_parametros_Info();
            return PartialView("_CmbCuenta_Param1", model);
        }
        public ActionResult CmbCuenta_Param2()
        {
            cp_parametros_Info model = new cp_parametros_Info();
            return PartialView("_CmbCuenta_Param2", model);
        }
        public ActionResult CmbCuenta_Param3()
        {
            cp_parametros_Info model = new cp_parametros_Info();
            return PartialView("_CmbCuenta_Param3", model);
        }
        public ActionResult CmbCuenta_Param4()
        {
            cp_parametros_Info model = new cp_parametros_Info();
            return PartialView("_CmbCuenta_Param4", model);
        }
        public ActionResult CmbCuenta_Param5()
        {
            cp_parametros_Info model = new cp_parametros_Info();
            return PartialView("_CmbCuenta_Param5", model);
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

    }
}