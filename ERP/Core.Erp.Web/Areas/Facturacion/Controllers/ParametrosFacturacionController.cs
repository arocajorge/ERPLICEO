using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.Facturacion;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Facturacion;
using Core.Erp.Bus.Caja;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Contabilidad;
using DevExpress.Web;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    public class ParametrosFacturacionController : Controller
    {
        #region Variables
        fa_parametro_Bus bus_parametro = new fa_parametro_Bus();
        ct_cbtecble_tipo_Bus bus_tipo_comprobante = new ct_cbtecble_tipo_Bus();
        in_movi_inven_tipo_Bus bus_tipo_movimiento = new in_movi_inven_tipo_Bus();
        caj_Caja_Bus bus_caja = new caj_Caja_Bus();
        fa_TipoNota_Bus bus_nota = new fa_TipoNota_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();

        #endregion

        #region Index
        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            fa_parametro_Info model = bus_parametro.get_info(IdEmpresa);
            if (model == null)
                model = new fa_parametro_Info { IdEmpresa = IdEmpresa };
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(fa_parametro_Info model)
        {
            if (!bus_parametro.guardarDB(model))
                ViewBag.mensaje = "No se pudieron actualizar los registros";
            cargar_combos(model.IdEmpresa);
            return View(model);
        }

        #endregion

        #region MEtodos
        private void cargar_combos(int IdEmpresa)
        {

            var lst_cuentas = bus_plancta.get_list(IdEmpresa, false, false);
            ViewBag.lst_cuentas = lst_cuentas;

            var lst_tipo_comprobante = bus_tipo_comprobante.get_list(IdEmpresa, false);
            ViewBag.lst_tipo_comprobante = lst_tipo_comprobante;


            var lst_tipo_movimiento = bus_tipo_movimiento.get_list(IdEmpresa,"", false);
            ViewBag.lst_tipo_movimiento_egr = lst_tipo_movimiento.Where(q => q.cm_tipo_movi == "-").ToList();
            ViewBag.lst_tipo_movimiento_ing = lst_tipo_movimiento.Where(q => q.cm_tipo_movi == "+").ToList();

            var lst_caja = bus_caja.get_list(IdEmpresa, false);
            ViewBag.lst_caja = lst_caja;

            var lst_nota = bus_nota.get_list(IdEmpresa, false);
            ViewBag.lst_nota = lst_nota;
        }

        #endregion
        #region Metodos ComboBox bajo demanda cliente
        public ActionResult CmbCliente_Parametro()
        {
            fa_parametro_Info model = new fa_parametro_Info();
            return PartialView("_CmbCliente_Parametro", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_cliente(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_cliente(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
        #endregion


        #region Metodos ComboBox bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_Param1()
        {
            fa_parametro_Info model = new fa_parametro_Info();
            return PartialView("_CmbCuenta_Param1", model);
        }
        public ActionResult CmbCuenta_Param2()
        {
            fa_parametro_Info model = new fa_parametro_Info();
            return PartialView("_CmbCuenta_Param2", model);
        }
        public ActionResult CmbCuenta_Param3()
        {
            fa_parametro_Info model = new fa_parametro_Info();
            return PartialView("_CmbCuenta_Param3", model);
        }
        public ActionResult CmbCuenta_Param4()
        {
            fa_parametro_Info model = new fa_parametro_Info();
            return PartialView("_CmbCuenta_Param4", model);
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