using Core.Erp.Bus.ActivoFijo;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.ActivoFijo;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.ActivoFijo.Controllers
{
    [SessionTimeout]
    public class ParametroAFController : Controller
    {
        #region Variables
        Af_Parametros_Bus bus_parametro = new Af_Parametros_Bus();
        ct_cbtecble_tipo_Bus bus_tipo_comprobante = new ct_cbtecble_tipo_Bus();
        ct_plancta_Bus bus_cta = new ct_plancta_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            Af_Parametros_Info model = bus_parametro.get_info(IdEmpresa);
            if (model == null)
                model = new Af_Parametros_Info { IdEmpresa =  IdEmpresa};
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Af_Parametros_Info model)
        {
            if (!bus_parametro.guardarDB(model))
                ViewBag.mensaje = "No se pudieron actualizar los registros";
            cargar_combos(model.IdEmpresa);
            return View(model);
        }

        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {

            var lst_tipo_comprobante = bus_tipo_comprobante.get_list(IdEmpresa, false);
            ViewBag.lst_tipo_comprobante = lst_tipo_comprobante;
            
            var lst_cta = bus_cta.get_list(IdEmpresa, false, false);
            ViewBag.lst_cta = lst_cta;

            Dictionary<string, string> lst_forma = new Dictionary<string, string>();
            lst_forma.Add("Por_Activo", "Por activo");
            lst_forma.Add("Por_Tipo_CtaCble", "Por tipo");
            lst_forma.Add("¨Por_CtaCble", "Por parámetros");
            ViewBag.lst_forma = lst_forma;


        }

        #endregion


        #region Metodos ComboBox bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_Param1()
        {
            Af_Parametros_Info model = new Af_Parametros_Info();
            return PartialView("_CmbCuenta_Param1", model);
        }
        public ActionResult CmbCuenta_Param2()
        {
            Af_Parametros_Info model = new Af_Parametros_Info();
            return PartialView("_CmbCuenta_Param2", model);
        }
        public ActionResult CmbCuenta_Param3()
        {
            Af_Parametros_Info model = new Af_Parametros_Info();
            return PartialView("_CmbCuenta_Param3", model);
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