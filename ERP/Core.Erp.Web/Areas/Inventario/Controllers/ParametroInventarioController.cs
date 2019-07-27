using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.Inventario;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class ParametroInventarioController : Controller
    {
        #region Variables
        in_parametro_Bus bus_param_in = new in_parametro_Bus();
        ct_cbtecble_tipo_Bus bus_tipo_comprobante = new ct_cbtecble_tipo_Bus();
        in_movi_inven_tipo_Bus bus_tipo_movimiento = new in_movi_inven_tipo_Bus();
        in_Catalogo_Bus bus_catalogo = new in_Catalogo_Bus();
        in_ProductoTipo_Bus bus_producto_tipo = new in_ProductoTipo_Bus();
        in_Motivo_Inven_Bus bus_motivo_inven = new in_Motivo_Inven_Bus();
        #endregion

        #region Index / Metodos
        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            in_parametro_Info model = bus_param_in.get_info(IdEmpresa);
            if (model == null)
                model = new in_parametro_Info { IdEmpresa = IdEmpresa};
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(in_parametro_Info model)
        {
            if (!bus_param_in.guardarDB(model))
                ViewBag.mensaje = "No se pudieron actualizar los registros";
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_tipo_comprobante = bus_tipo_comprobante.get_list(IdEmpresa, false);
            ViewBag.lst_tipo_comprobante = lst_tipo_comprobante;

            var lst_tipo_movimiento = bus_tipo_movimiento.get_list(IdEmpresa,"", false);
            ViewBag.lst_tipo_movimiento_egr = lst_tipo_movimiento.Where(q => q.cm_tipo_movi == "-").ToList();
            ViewBag.lst_tipo_movimiento_ing = lst_tipo_movimiento.Where(q => q.cm_tipo_movi == "+").ToList();

            var lst_motivo = bus_motivo_inven.get_list(IdEmpresa, false);
            ViewBag.lst_motivo_ing = lst_motivo.Where(q => q.Tipo_Ing_Egr == "ING").ToList();
            ViewBag.lst_motivo_egr = lst_motivo.Where(q => q.Tipo_Ing_Egr == "EGR").ToList();

            var lst_aprobacion = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoInventario.EST_APROB),false);
            ViewBag.lst_aprobacion = lst_aprobacion;

            var lst_tipo_conta = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoInventario.TIPO_CONTA_CTA), false);
            ViewBag.lst_tipo_conta = lst_tipo_conta;

            var lst_fecha_contab = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoInventario.FECH_CONTA), false);
            ViewBag.lst_fecha_contab = lst_fecha_contab;

            var lst_producto_tipo = bus_producto_tipo.get_list(IdEmpresa, false);
            ViewBag.lst_producto_tipo = lst_producto_tipo;
        }

        #endregion

        #region Metodos ComboBox bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_Param3()
        {
            in_parametro_Info model = new in_parametro_Info();
            return PartialView("_CmbCuenta_Param3", model);
        }

        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), true);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

    }
}