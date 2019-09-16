using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ProcesarRolController : Controller
    {
        #region variables
        List<ro_nomina_tipo_Info> lista_nomina = new List<ro_nomina_tipo_Info>();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        List<ro_Nomina_Tipoliqui_Info> lst_nomina_tipo = new List<ro_Nomina_Tipoliqui_Info>();
        ro_periodo_x_ro_Nomina_TipoLiqui_Bus bus_periodos_x_nomina = new ro_periodo_x_ro_Nomina_TipoLiqui_Bus();
        List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> lst_periodos = new List<ro_periodo_x_ro_Nomina_TipoLiqui_Info>();
        ro_rol_detalle_Bus bus_detalle = new ro_rol_detalle_Bus();
        ro_rol_Bus bus_rol = new ro_rol_Bus();
        ct_plancta_Bus bus_cuentas = new ct_plancta_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ct_cbtecble_det_lst list_det = new ct_cbtecble_det_lst();
        ct_cbtecble_lst_prov list_prov = new ct_cbtecble_lst_prov();
        ct_cbtecble_tipo_Bus bus_tipo = new ct_cbtecble_tipo_Bus();
        ro_Parametros_Bus bus_parametro = new ro_Parametros_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda xueldo-prov

        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_sueldos()
        {
            ro_rol_Info model = new ro_rol_Info();
            return PartialView("_CmbCuenta_sueldos", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_sueldo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda_sueldo(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }


        public ActionResult CmbCuenta_prov()
        {
            ro_rol_Info model = new ro_rol_Info();
            return PartialView("_CmbCuenta_sueldos", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_prov(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda_prov(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion


        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combo_consulta(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            cargar_combo_consulta(model.IdEmpresa);
            return View(model);
        }
        public ActionResult Index2()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combo_consulta(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index2(cl_filtros_Info model)
        {
            cargar_combo_consulta(model.IdEmpresa);
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ro_rol( int IdSucursal=0)
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ViewBag.IdSucursal = IdSucursal;
                List< ro_rol_Info> model = bus_rol.get_list_nominas(IdEmpresa, IdSucursal);
                return PartialView("_GridViewPartial_ro_rol", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo( ro_rol_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.UsuarioIngresa = SessionFixed.IdUsuario;
                    info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                    if (!bus_rol.procesar(info))
                        return View(info);
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo()
        {
            try
            {
                cargar_combos(0, 0);
                 ro_rol_Info info = new  ro_rol_Info
                 {
                     IdNomina_Tipo = 1,
                     IdSucursal=Convert.ToInt32( SessionFixed.IdSucursal),
                     IdEmpresa=Convert.ToInt32( SessionFixed.IdEmpresa)
                 };
                ViewBag.FechaCorte = DateTime.Now.Date;
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_rol_Info info)
        {
            try
            {
                
                    info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                if (!bus_rol.procesar(info))
                    {
                        cargar_combos(info.IdNomina_Tipo, info.IdNomina_TipoLiqui);
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index");
                

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Modificar( int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0, int IdPeriodo=0, decimal IdRol=0)
        {
            try
            {
                ViewBag.IdRol = IdRol;
                cargar_combos(IdNomina_Tipo, IdNomina_TipoLiqui);
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ro_rol_Info model = bus_rol.get_info(IdEmpresa, IdNomina_Tipo, IdNomina_TipoLiqui, IdPeriodo, IdRol);
                ViewBag.FechaCorte = DateTime.Now;

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult CerrarPeriodo(ro_rol_Info info)
        {
            try
            {
                //cp_orden_pago_tipo_x_empresa_Bus bus_op_tipo = new cp_orden_pago_tipo_x_empresa_Bus();
                //var info_tipo_op = bus_op_tipo.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoOrdenPago.ANTI_EMPLE.ToString());
                //if( (info_tipo_op.IdCtaCble==null | info_tipo_op.IdCtaCble=="") | (info_tipo_op.IdCtaCble_Credito == null | info_tipo_op.IdCtaCble_Credito == ""))
                //{
                //    ViewBag.mensaje = "Falta cuenta contable tipo de orden de pago";
                //    cargar_combos(info.IdNomina_Tipo, info.IdNomina_TipoLiqui);
                //    return View(info);
                //}

                info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                if (!bus_rol.CerrarPeriodo(info))
                    {
                        cargar_combos(info.IdNomina_Tipo,info.IdNomina_TipoLiqui);
                        return View(info);
                    }
                    else
                {
                    if(info.IdNomina_TipoLiqui == 3 || info.IdNomina_TipoLiqui == 4 )
                        return RedirectToAction("Index", "PagoBenediciosSociales", new { Area = "RRHH" });
                }
                return RedirectToAction("Index");
              

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult CerrarPeriodo(int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0, int IdPeriodo = 0, decimal IdRol=0)
        {
            try
            {
                cargar_combos(IdNomina_Tipo, IdNomina_TipoLiqui);
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                return View(bus_rol.get_info(IdEmpresa, IdNomina_Tipo, IdNomina_TipoLiqui, IdPeriodo, IdRol));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult AbrirPeriodo(ro_rol_Info info)
        {
            try
            {

                info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                if (!bus_rol.AbrirPeriodo(info))
                {
                    cargar_combos(info.IdNomina_Tipo, info.IdNomina_TipoLiqui);
                    return View(info);
                }
                else
                    return RedirectToAction("Index2");


            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult AbrirPeriodo(int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0, int IdPeriodo = 0, decimal IdRol=0)
        {
            try
            {
                cargar_combos(IdNomina_Tipo, IdNomina_TipoLiqui);
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                return View(bus_rol.get_info(IdEmpresa, IdNomina_Tipo, IdNomina_TipoLiqui, IdPeriodo, IdRol));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult ContabilizarPeriodo(ro_rol_Info info)
        {
            try
            {
                info.lst_sueldo_x_pagar = list_det.get_list_cta(info.IdTransaccionSession);
                info.lst_provisiones = list_prov.get_list(info.IdTransaccionSession);
                info.UsuarioCierre = SessionFixed.IdUsuario;
                
                foreach (var item in info.lst_sueldo_x_pagar)
                {
                    //item.IdCtaCble = item.IdCtaCble.Trim();
                    if (string.IsNullOrEmpty(item.IdCtaCble))
                    {
                        ViewBag.mensaje = " Falta cueta contable de:" +item.dc_Observacion;
                        cargar_combos(info.IdNomina_Tipo, info.IdNomina_TipoLiqui);
                        cargar_combo_detalle();
                        return View(info);
                    }
                }
                foreach (var item in info.lst_provisiones)
                {
                    item.IdCtaCble = item.IdCtaCble.Trim();

                    if (item.IdCtaCble == null || item.IdCtaCble == "")
                    {
                        ViewBag.mensaje = " Falta cueta contable de:" + item.dc_Observacion;
                        cargar_combos(info.IdNomina_Tipo, info.IdNomina_TipoLiqui);
                        cargar_combo_detalle();
                        return View(info);
                    }
                }
                info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
              //  var param = bus_parametro.get_info(info.IdEmpresa);
                
                    if (!bus_rol.ContabilizarPeriodo(info))
                    {
                        cargar_combos(info.IdNomina_Tipo,info.IdNomina_TipoLiqui);
                        cargar_combo_detalle();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index");
               

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult ContabilizarPeriodo( int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0, int IdPeriodo = 0, int IdRol=0)
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion
                ro_rol_Info model = new ro_rol_Info

                {
                    IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                    IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                    lst_sueldo_x_pagar = new List<ct_cbtecble_det_Info>(),
                    lst_provisiones = new List<ct_cbtecble_det_Info>(),
                };
                model = bus_rol.get_info_contabilizar(Convert.ToInt32(SessionFixed.IdEmpresa), IdNomina_Tipo, IdNomina_TipoLiqui, IdPeriodo, IdRol);
                list_det.set_list_cta(model.lst_sueldo_x_pagar, model.IdTransaccionSession=Convert.ToDecimal(SessionFixed.IdTransaccionSession));
                list_prov.set_list(model.lst_provisiones, model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession));
                model.Fechacontabilizacion = DateTime.Now;
                cargar_combos(IdNomina_Tipo, IdNomina_TipoLiqui);
                cargar_combo_detalle();
                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult ReversarcontabilidadPeriodo(ro_rol_Info info)
        {
            try
            {

                info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                if (!bus_rol.Reversar_contabilidad_Periodo(info))
                {
                    cargar_combos(info.IdNomina_Tipo, info.IdNomina_TipoLiqui);
                    return View(info);
                }
                else
                    return RedirectToAction("Index2");


            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult ReversarcontabilidadPeriodo(int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0, int IdPeriodo = 0, decimal IdRol=0)
        {
            try
            {
                cargar_combos(IdNomina_Tipo, IdNomina_TipoLiqui);
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                return View(bus_rol.get_info(IdEmpresa, IdNomina_Tipo, IdNomina_TipoLiqui, IdPeriodo,IdRol));

            }
            catch (Exception)
            {

                throw;
            }
        }
        
        private void cargar_combos(int IdNomina_Tipo, int IdNomina_Tipo_Liqui)
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                lista_nomina = bus_nomina.get_list(IdEmpresa, false);
                lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, IdNomina_Tipo);
                lst_periodos = bus_periodos_x_nomina.get_list_utimo_periodo_aprocesar(IdEmpresa, IdNomina_Tipo, IdNomina_Tipo_Liqui);
                ViewBag.lst_nomina = lista_nomina;
                ViewBag.lst_nomina_tipo = lst_nomina_tipo;
                ViewBag.lst_periodos = lst_periodos;
                var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);
                ViewBag.lst_sucursal = lst_sucursal;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cargar_combo_consulta(int IdEmpresa)
        {
            try
            {
                var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);

                ViewBag.lst_sucursal = lst_sucursal;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_sueldo_x_pagar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = list_det.get_list_cta(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_sueldo_x_pagar", model);
        }

        public ActionResult GridViewPartial_provisiones()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
           var model = list_prov.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_provisiones", model);
        }

        public ActionResult GridViewPartial_empleados_sin_percibir_sueldo(decimal IdRol=0)
        {
            ViewBag.IdRol = IdRol;
            var model = bus_detalle.get_list_nomina_sin_sueldo_percibir(Convert.ToInt32(SessionFixed.IdEmpresa), IdRol);
            return PartialView("_GridViewPartial_empleados_sin_percibir_sueldo", model);
        }

  
        [ValidateInput(false)]
        public ActionResult GridViewPartial_nominas_cerradas(int IdSucursal = 0)
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ViewBag.IdSucursal = IdSucursal;
                List<ro_rol_Info> model = bus_rol.get_list_nominas_cerradas(IdEmpresa, IdSucursal);
                return PartialView("_GridViewPartial_nominas_cerradas", model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void cargar_combo_detalle()
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ViewBag.lst_cuentas = bus_cuentas.get_list(IdEmpresa, false, true);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_prov([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {

            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var cta = bus_plancta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdCtaCble);
            if (info_det != null)
            {
                if (cta != null)
                {
                    info_det.pc_Cuenta = cta.pc_Cuenta;
                    info_det.IdCtaCble = cta.IdCtaCble;
                }
            }
            if (ModelState.IsValid)
                list_prov.UpdateRow_prov(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
           var model = list_prov.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_provisiones", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {

            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var cta = bus_plancta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdCtaCble);
            if (info_det != null)
            {
                if (cta != null)
                {
                    info_det.pc_Cuenta = cta.pc_Cuenta;
                    info_det.IdCtaCble = cta.IdCtaCble;
                }
            }
            if (ModelState.IsValid)
                list_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = list_det.get_list_cta(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_sueldo_x_pagar", model);
        }
    }
    public class ct_cbtecble_det_lst
    {
        string variable = "ct_cbtecble_det_Info";
        public List<ct_cbtecble_det_Info> get_list_cta(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ct_cbtecble_det_Info> list = new List<ct_cbtecble_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ct_cbtecble_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }
        public void set_list_cta(List<ct_cbtecble_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }
        public void UpdateRow(ct_cbtecble_det_Info info_det, decimal IdTransaccionSession)
        {
            var ls = get_list_cta(IdTransaccionSession);

            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var cta = bus_plancta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdCtaCble);
            ct_cbtecble_det_Info edited_info = get_list_cta(IdTransaccionSession).Where(m => m.secuencia == info_det.secuencia).First();
            if (cta != null)
            {
                info_det.IdCtaCble = cta.IdCtaCble ;
                info_det.pc_Cuenta = cta.pc_Cuenta;
            }

            edited_info.pc_Cuenta = info_det.pc_Cuenta;
            edited_info.IdCtaCble = info_det.IdCtaCble;
        }
    }
    public class ct_cbtecble_lst_prov
    {
        string variable = "ct_cbtecble_det_Info_x";
        public List<ct_cbtecble_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ct_cbtecble_det_Info> list = new List<ct_cbtecble_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ct_cbtecble_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }
        public void set_list(List<ct_cbtecble_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }
        public void UpdateRow_prov(ct_cbtecble_det_Info info_det, decimal IdTransaccionSession)
        {
            var ls = get_list(IdTransaccionSession);

            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var cta = bus_plancta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdCtaCble);
            ct_cbtecble_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.secuencia == info_det.secuencia).First();
            if (cta != null)
            {
                info_det.IdCtaCble = cta.IdCtaCble;
                info_det.pc_Cuenta = cta.pc_Cuenta;
            }

            edited_info.pc_Cuenta = info_det.pc_Cuenta;
            edited_info.IdCtaCble = info_det.IdCtaCble;
        }
    }
}