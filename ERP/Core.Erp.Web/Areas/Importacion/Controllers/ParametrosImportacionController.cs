using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Importacion;
using Core.Erp.Info.Importacion;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.Inventario;
using Core.Erp.Bus.General;
using Core.Erp.Info.Contabilidad;
using DevExpress.Web;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Importacion.Controllers
{
    public class ParametrosImportacionController : Controller
    {
        #region Metodos ComboBox bajo demanda

        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();

        public ActionResult CmbCuenta_cta_contable_inv()
        {
            imp_parametro_Info model = new imp_parametro_Info();

            return PartialView("_CmbCuenta_contable_inv", model);
        }
        public ActionResult CmbCuenta_cta_contable_imp()
        {
            imp_parametro_Info model = new imp_parametro_Info();

            return PartialView("_CmbCuenta_contable_imp", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_cta(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa),false);
        }
        public ct_plancta_Info get_info_bajo_demanda_cta(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }


        #endregion

        #region Variables
        imp_parametro_Bus bus_parametro = new imp_parametro_Bus();
        ct_cbtecble_tipo_Bus bus_comprobante_tipo = new ct_cbtecble_tipo_Bus();
        in_movi_inven_tipo_Bus bus_tipo = new in_movi_inven_tipo_Bus();
        in_Motivo_Inven_Bus bus_motivo = new in_Motivo_Inven_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            imp_parametro_Info model = bus_parametro.get_info(IdEmpresa);
            if (model == null)
                model = new imp_parametro_Info { IdEmpresa = IdEmpresa };
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(imp_parametro_Info model)
        {
            if (!bus_parametro.guardarDB(model))
                ViewBag.mensaje = "No se pudieron actualizar los registros";
            cargar_combos(model.IdEmpresa);
            return View(model);
        }

        private void cargar_combos(int IdEmpresa)
        {
            var lst_tipo = bus_comprobante_tipo.get_list(IdEmpresa, false);
            ViewBag.lst_tipo = lst_tipo;

            var lst_tipo_mov = bus_tipo.get_list(IdEmpresa,"+", false);
            ViewBag.lst_tipo_mov = lst_tipo_mov;

            var lst_motivo = bus_motivo.get_list(IdEmpresa, false);
            ViewBag.lst_motivo = lst_motivo;

            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_bodega = bus_bodega.get_list(IdEmpresa, false);
            ViewBag.lst_bodega = lst_bodega;
        }

        #endregion
    }
}