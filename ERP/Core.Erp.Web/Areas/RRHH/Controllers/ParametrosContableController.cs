using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using DevExpress.Web.Mvc;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Contabilidad;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ParametrosContableController : Controller
    {

        #region
        ro_Parametros_Bus bus_parametros = new ro_Parametros_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        ro_Config_Param_contable_lst lst_cta_rubro = new ro_Config_Param_contable_lst();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_Config_Param_contable_Bus bus_configuracion_ctas = new ro_Config_Param_contable_Bus();
        ct_plancta_Bus bus_cuenta = new ct_plancta_Bus();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Bus bus_configuracion_cta_x_sueldo = new ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Bus();
        ct_cbtecble_tipo_Bus bus_comprobante_tipo = new ct_cbtecble_tipo_Bus();
        cp_orden_pago_tipo_x_empresa_Bus bus_tipo_op = new cp_orden_pago_tipo_x_empresa_Bus();
        int IdEmpresa = 0;
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();

        #endregion


        #region Metodos ComboBox bajo demanda sueldo por rubros

        public ActionResult CmbCuenta_rubros_x_sueldo()
        {
            ct_cbtecble_det_Info model = new ct_cbtecble_det_Info();
            return PartialView("_CmbCuenta_rubros_x_sueldo", model);
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

        #region Metodos ComboBox bajo demanda provisiones debito

        public ActionResult CmbCuenta_provisiones_debito()
        {
            ct_cbtecble_det_Info model = new ct_cbtecble_det_Info();
            return PartialView("_CmbCuenta_provisiones_debito", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_prov_debito(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), true);
        }
        public ct_plancta_Info get_info_bajo_demanda_prov_debito(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Metodos ComboBox bajo demanda provisiones credito

        public ActionResult CmbCuenta_provisiones_credito()
        {
            ct_cbtecble_det_Info model = new ct_cbtecble_det_Info();
            return PartialView("_CmbCuenta_provisiones_credito", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_prov_credito(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), true);
        }
        public ct_plancta_Info get_info_bajo_demanda_prov_credito(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion



        #region Metodos ComboBox bajo demanda sueldo por pagar

        public ActionResult CmbCuenta_sueldo_x_pagar()
        {
            ct_cbtecble_det_Info model = new ct_cbtecble_det_Info();
            return PartialView("_CmbCuenta_sueldo_x_pagar", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_sueldo_x_pagar(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda_sueldo_x_pagar(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion


        #region Metodos ComboBox bajo demanda provisiones credito

        public ActionResult CmbNomina()
        {
            ro_Parametros_Info model = new ro_Parametros_Info();
            return PartialView("_CmbNomina", model);
        }
        public List<ro_nomina_tipo_Info> get_list_bajo_demanda_nomina(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_nomina.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ro_nomina_tipo_Info get_info_bajo_demanda_prov_nomina(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_nomina.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }


       
        #endregion

        public ActionResult CargarNomina()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            //int IdNomina = 0;
            //if (Request.Params["IdNomina"] == null || !Int32.TryParse(Request.Params["IdNomina"].ToString(), out IdNomina))
            //    IdNomina = 0;
            int IdNomina = Request.Params["IdNomina"] != null ? Convert.ToInt32(Request.Params["IdNomina"].ToString()) : 0;

            return GridViewExtension.GetComboBoxCallbackResult(p =>
            {
                p.TextField = "DescripcionProcesoNomina";
                p.ValueField = "IdString";
                p.Columns.Add("DescripcionProcesoNomina", "Descripción proceso nómina");
                p.TextFormatString = "{0}";
                p.ValueType = typeof(string);
                p.BindList(bus_nomina_tipo.get_list(IdEmpresa, IdNomina));
            });
        }
        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);

            ro_Parametros_Info model = new ro_Parametros_Info();
            model = bus_parametros.get_info(IdEmpresa);

            lst_cta_rubro.set_list_cta_rubros(model.lst_cta_x_rubros);
            lst_cta_rubro.set_list_sueldo_x_pagar(model.lst_cta_x_sueldo_pagar);
            cargar_combos();
            cargar_combos_detalle();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ro_Parametros_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.lst_cta_x_rubros = lst_cta_rubro.get_list_cta_rubros();
            model.lst_cta_x_sueldo_pagar = lst_cta_rubro.get_list_sueldo_x_pagar();
            if (!bus_parametros.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            else
             {
                bus_parametros = new ro_Parametros_Bus();
                model = bus_parametros.get_info(Convert.ToInt32(SessionFixed.IdEmpresa));
                lst_cta_rubro.set_list_cta_rubros(model.lst_cta_x_rubros);
                lst_cta_rubro.set_list_sueldo_x_pagar(model.lst_cta_x_sueldo_pagar);
                cargar_combos();
                return View(model);
            }

        }

        [ValidateInput(false)]

        private void cargar_combos()
        {
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.lst_nomina = bus_nomina.get_list(IdEmpresa, false);
            ViewBag.lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, false);
            ViewBag.lst_rubro = bus_rubro.get_list(IdEmpresa, false);
            ViewBag.lst_comprobante_tipo = bus_comprobante_tipo.get_list(IdEmpresa, false);
            ViewBag.lst_rubro = bus_rubro.get_list_rub_concepto(IdEmpresa);
            ViewBag.lst_tipo_op = bus_tipo_op.get_list(IdEmpresa);
            ViewBag.lst_estado_prestamo = bus_catalogo.get_list_x_tipo(42);

        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_cta_ctble_rubros()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_Parametros_Info model = new ro_Parametros_Info();
            var lst = lst_cta_rubro.get_list_cta_rubros();
            model.lst_cta_x_rubros = lst_cta_rubro.get_list_cta_rubros().Where(v => v.rub_provision == false).ToList();

            cargar_combos_detalle();
            return PartialView("_GridViewPartial_cta_ctble_rubros", model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_cta_ctble_provisiones()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_Parametros_Info model = new ro_Parametros_Info();
            model.lst_cta_x_rubros = lst_cta_rubro.get_list_cta_rubros();
            
          model.lst_cta_x_provisiones = model.lst_cta_x_rubros.Where(v => v.rub_provision == true).ToList();
            
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_cta_ctble_provisiones", model);
        }

        public ActionResult GridViewPartial_cta_contable_sueldo_pagar()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_Parametros_Info model = new ro_Parametros_Info();
            model.lst_cta_x_sueldo_pagar = lst_cta_rubro.get_list_sueldo_x_pagar();
           
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_cta_contable_sueldo_pagar", model);
        }


        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.lst_catalogo = bus_catalogo.get_list_x_tipo(34);
            ViewBag.lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, false);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Config_Param_contable_Info info_det)
        {
            if (ModelState.IsValid)
            {
                lst_cta_rubro.UpdateRow_cta_rubros(info_det);
                bus_configuracion_ctas.ModificarDB(lst_cta_rubro.get_list_cta_rubros().Where(q => q.Secuencia == info_det.Secuencia).FirstOrDefault());
            }
            ro_Parametros_Info model = new ro_Parametros_Info();
            model.lst_cta_x_rubros = lst_cta_rubro.get_list_cta_rubros().Where(v => v.rub_provision == false).ToList();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_cta_ctble_rubros", model);
        }
        public ActionResult EditingUpdate_provisiones([ModelBinder(typeof(DevExpressEditorsBinder))] ro_Config_Param_contable_Info info_det)
        {
            if (ModelState.IsValid)
            {
                lst_cta_rubro.UpdateRow_cta_rubros_x_provision(info_det);
                bus_configuracion_ctas.ModificarDB(lst_cta_rubro.get_list_cta_rubros().Where(q => q.Secuencia == info_det.Secuencia).FirstOrDefault());
            }            
            ro_Parametros_Info model = new ro_Parametros_Info();
            model.lst_cta_x_provisiones = lst_cta_rubro.get_list_cta_rubros().Where(v => v.rub_provision == true).ToList();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_cta_ctble_provisiones", model);
        }

        public ActionResult EditingNew_cta_sueldo([ModelBinder(typeof(DevExpressEditorsBinder))] ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if(info_det!=null)
            {
            var nom = bus_nomina.get_info(IdEmpresa, info_det.IdNomina);

            info_det.IdNominaTipo = string.IsNullOrEmpty(info_det.IdString) ? 0 : Convert.ToInt32(info_det.IdString.Substring(3, 3));

            var nom_tipo = bus_nomina_tipo.get_info(IdEmpresa, info_det.IdNomina, info_det.IdNominaTipo);

                if (nom!=null && nom_tipo!= null)
                {
                    info_det.IdNomina = info_det.IdNomina;
                    info_det.Descripcion = nom.Descripcion;
                    info_det.IdNominaTipo = info_det.IdNominaTipo;
                    info_det.DescripcionProcesoNomina = nom_tipo.DescripcionProcesoNomina;
                }
            }

            if (ModelState.IsValid)
            {
                lst_cta_rubro.NewRow_cta_sueldo_x_pagar(info_det);
                info_det.IdEmpresa = IdEmpresa;
                bus_configuracion_cta_x_sueldo.modificar(lst_cta_rubro.get_list_sueldo_x_pagar().Where(q => q.Secuencia == info_det.Secuencia).First());
            }
            ro_Parametros_Info model = new ro_Parametros_Info();
            model.lst_cta_x_sueldo_pagar = lst_cta_rubro.get_list_sueldo_x_pagar();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_cta_contable_sueldo_pagar", model);
        }
        public ActionResult EditingUpdate_cta_sueldo([ModelBinder(typeof(DevExpressEditorsBinder))] ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (info_det != null)
            {

            var nom = bus_nomina.get_info(IdEmpresa, info_det.IdNomina);

            info_det.IdNominaTipo = string.IsNullOrEmpty(info_det.IdString) ? 0 : Convert.ToInt32(info_det.IdString.Substring(3, 3));

            var nom_tipo = bus_nomina_tipo.get_info(IdEmpresa, info_det.IdNomina, info_det.IdNominaTipo);

                if (nom != null && nom_tipo != null)
                {
                    info_det.IdNomina = info_det.IdNomina;
                    info_det.Descripcion = nom.Descripcion;
                    info_det.IdNominaTipo = info_det.IdNominaTipo;
                    info_det.DescripcionProcesoNomina = nom_tipo.DescripcionProcesoNomina;
                }
            }
            if (ModelState.IsValid)
            {
                lst_cta_rubro.UpdateRow_cta_sueldo_x_pagar(info_det);
                info_det.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                bus_configuracion_cta_x_sueldo.modificar(lst_cta_rubro.get_list_sueldo_x_pagar().Where(q => q.Secuencia == info_det.Secuencia).First());
            }
            ro_Parametros_Info model = new ro_Parametros_Info();
            model.lst_cta_x_sueldo_pagar = lst_cta_rubro.get_list_sueldo_x_pagar();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_cta_contable_sueldo_pagar", model);
        }
        public ActionResult EditingDelete_cta_sueldo([ModelBinder(typeof(DevExpressEditorsBinder))] ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info info_det)
        {
            if (ModelState.IsValid)
            {
                var det = lst_cta_rubro.get_list_sueldo_x_pagar().Where(q => q.Secuencia == info_det.Secuencia).FirstOrDefault();
                if (det != null)
                {
                    det.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                    if (bus_configuracion_cta_x_sueldo.eliminarDB(det.IdEmpresa, det.IdNomina, det.IdNominaTipo))
                        lst_cta_rubro.DeleteRow_cta_sueldo_x_pagar(info_det);
                }                
            }
            ro_Parametros_Info model = new ro_Parametros_Info();
            model.lst_cta_x_sueldo_pagar = lst_cta_rubro.get_list_sueldo_x_pagar();
            lst_cta_rubro.set_list_sueldo_x_pagar(model.lst_cta_x_sueldo_pagar);
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_cta_contable_sueldo_pagar", model);
        }

    }


    public class ro_Config_Param_contable_lst
    {
        public List<ro_Config_Param_contable_Info> get_list_cta_rubros()
        {
            if (HttpContext.Current.Session["ro_Config_Param_contable_Info"] == null)
            {
                List<ro_Config_Param_contable_Info> list = new List<ro_Config_Param_contable_Info>();

                HttpContext.Current.Session["ro_Config_Param_contable_Info"] = list;
            }
            return (List<ro_Config_Param_contable_Info>)HttpContext.Current.Session["ro_Config_Param_contable_Info"];
        }
        public void set_list_cta_rubros(List<ro_Config_Param_contable_Info> list)
        {
            HttpContext.Current.Session["ro_Config_Param_contable_Info"] = list;
        }
        public void UpdateRow_cta_rubros(ro_Config_Param_contable_Info info_det)
        {
            var ls = get_list_cta_rubros();

            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            ro_Config_Param_contable_Info edited_info = get_list_cta_rubros().Where(m => m.Secuencia == info_det.Secuencia).First();
            var cta = bus_plancta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdCtaCble);
            if (cta != null)
            {
                info_det.pc_Cuenta = cta.IdCtaCble + " - " + cta.pc_Cuenta;
                edited_info.pc_Cuenta_prov_debito = info_det.pc_Cuenta_prov_debito;
            }

            edited_info.IdCtaCble = info_det.IdCtaCble;
            edited_info.pc_Cuenta = info_det.pc_Cuenta;

            edited_info.IdCtaCble_prov_credito = info_det.IdCtaCble_prov_credito;
            edited_info.DebCre = info_det.DebCre;
        }

        public void UpdateRow_cta_rubros_x_provision(ro_Config_Param_contable_Info info_det)
        {
            var ls = get_list_cta_rubros();

            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            ro_Config_Param_contable_Info edited_info = get_list_cta_rubros().Where(m => m.Secuencia == info_det.Secuencia).First();
            var cta = bus_plancta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdCtaCble_prov_credito);
            if (cta != null)
            {
                info_det.pc_Cuenta_prov_credito = cta.IdCtaCble + " - " + cta.pc_Cuenta;
            }

            var cta_deb = bus_plancta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdCtaCble_prov_debito);
            if (cta_deb != null)
            {
                info_det.pc_Cuenta_prov_debito = cta_deb.IdCtaCble + " - " + cta_deb.pc_Cuenta;
            }

            edited_info.IdCtaCble_prov_credito = info_det.IdCtaCble_prov_credito;
            edited_info.IdCtaCble_prov_debito = info_det.IdCtaCble_prov_debito;

            edited_info.pc_Cuenta_prov_credito = info_det.pc_Cuenta_prov_credito;
            edited_info.pc_Cuenta_prov_debito = info_det.pc_Cuenta_prov_debito;
        }


        public List<ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info> get_list_sueldo_x_pagar()
        {
            if (HttpContext.Current.Session["lst_cta_sueldo"] == null)
            {
                List<ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info> list = new List<ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info>();

                HttpContext.Current.Session["lst_cta_sueldo"] = list;
            }
            return (List<ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info>)HttpContext.Current.Session["lst_cta_sueldo"];
        }
        public void set_list_sueldo_x_pagar(List<ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info> list)
        {
            HttpContext.Current.Session["lst_cta_sueldo"] = list;
        }
        public void UpdateRow_cta_sueldo_x_pagar(ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info info_det)
        {
            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var cta = bus_plancta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdCtaCble_sueldo);
            ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info edited_info = get_list_sueldo_x_pagar().Where(m => m.Secuencia == info_det.Secuencia).First();
            if (cta != null)
            {
                info_det.pc_Cuenta = cta.IdCtaCble + " - " + cta.pc_Cuenta;
            }
            edited_info.IdCtaCble_sueldo = info_det.IdCtaCble_sueldo;
            edited_info.IdNomina = info_det.IdNomina;
            edited_info.IdNominaTipo = info_det.IdNominaTipo;
            edited_info.pc_Cuenta = info_det.pc_Cuenta;
            edited_info.IdString = info_det.IdString;

        }
        public void NewRow_cta_sueldo_x_pagar(ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info info_det)
        {
            List<ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info> list = get_list_sueldo_x_pagar();
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;

            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var cta = bus_plancta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdCtaCble_sueldo);
            if (cta != null)
            {
                info_det.pc_Cuenta = cta.IdCtaCble + " - " + cta.pc_Cuenta;
                info_det.IdString = info_det.IdString;
            }



            list.Add(info_det);
        }
        public void DeleteRow_cta_sueldo_x_pagar(ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info info_det)
        {
            List<ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_Info> list = get_list_sueldo_x_pagar();
            list.Remove(list.Where(m => m.Secuencia == info_det.Secuencia).First());
        }


    }

}
