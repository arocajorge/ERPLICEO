using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Bus.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Areas.Contabilidad.Controllers;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    public class ConciliacionAnticipoProveedorController : Controller
    {
        #region Variables
        cp_ConciliacionAnticipoDetAnt_x_Ingresar_List Lista_OP_x_Ing = new cp_ConciliacionAnticipoDetAnt_x_Ingresar_List();
        cp_ConciliacionAnticipoDetAnt_List Lista_det_OP = new cp_ConciliacionAnticipoDetAnt_List();
        cp_ConciliacionAnticipoDetCXP_x_Ingresar_List Lista_Fact_x_Ing = new cp_ConciliacionAnticipoDetCXP_x_Ingresar_List();
        cp_ConciliacionAnticipoDetCXP_List Lista_det_Fact = new cp_ConciliacionAnticipoDetCXP_List();
        ct_cbtecble_det_List list_ct_cbtecble_det = new ct_cbtecble_det_List();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        ct_cbtecble_Bus bus_cbte = new ct_cbtecble_Bus();
        ct_cbtecble_det_Bus bus_cbte_det = new ct_cbtecble_det_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        cp_ConciliacionAnticipo_Bus bus_conc_anticipo = new cp_ConciliacionAnticipo_Bus();
        cp_ConciliacionAnticipoDetAnt_Bus bus_conc_ant_op = new cp_ConciliacionAnticipoDetAnt_Bus();
        cp_ConciliacionAnticipoDetCXP_Bus bus_conc_ant_fact = new cp_ConciliacionAnticipoDetCXP_Bus();
        cp_proveedor_Info info_proveedor = new cp_proveedor_Info();
        cp_proveedor_Bus bus_prov = new cp_proveedor_Bus();
        cp_parametros_Info info_parametro = new cp_parametros_Info();
        cp_parametros_Bus bus_param = new cp_parametros_Bus();
        ct_cbtecble_tipo_Bus bus_tipocbte = new ct_cbtecble_tipo_Bus();
        string mensaje = string.Empty;
        #endregion

        #region Metodos ComboBox bajo demanda Proveedor
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbProveedor()
        {
            cp_conciliacionAnticipo_Info model = new cp_conciliacionAnticipo_Info();
            return PartialView("_CmbProveedor", model);
        }
        public ActionResult CmbProveedor_readonly()
        {
            cp_conciliacionAnticipo_Info model = new cp_conciliacionAnticipo_Info();
            return PartialView("_CmbProveedor_readonly", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_proveedor(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_proveedor(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
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
        public ActionResult GridViewPartial_ConciliacionAnticipoProveedor(DateTime? fecha_ini, DateTime? fecha_fin, int IdEmpresa = 0, int IdSucursal = 0)
        {
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : fecha_ini;
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : fecha_fin;
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;

            List<cp_conciliacionAnticipo_Info> model = bus_conc_anticipo.GetList(IdEmpresa, IdSucursal, ViewBag.fecha_ini, ViewBag.fecha_fin, true);
            return PartialView("_GridViewPartial_ConciliacionAnticipoProveedor", model);
        }

        public void CargarCombosConsulta(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
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
            info_parametro = bus_param.get_info(IdEmpresa);
            cp_conciliacionAnticipo_Info model = new cp_conciliacionAnticipo_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdUsuarioCreacion = SessionFixed.IdUsuario,
                Fecha = DateTime.Now.Date,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdTipoCbte = info_parametro.pa_TipoCbte_para_conci_x_antcipo
            };

            cargar_combos(IdEmpresa);
            Lista_det_OP.set_list(new List<cp_ConciliacionAnticipoDetAnt_Info>(), model.IdTransaccionSession);
            Lista_det_Fact.set_list(new List<cp_ConciliacionAnticipoDetCXP_Info>(), model.IdTransaccionSession);
            list_ct_cbtecble_det.set_list(new List<ct_cbtecble_det_Info>(), model.IdTransaccionSession);

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(cp_conciliacionAnticipo_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            model.Lista_det_OP = Lista_det_OP.get_list(model.IdTransaccionSession);
            model.Lista_det_Fact = Lista_det_Fact.get_list(model.IdTransaccionSession);
            model.Lista_det_Cbte = list_ct_cbtecble_det.get_list(model.IdTransaccionSession);

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa);

                return View(model);
            }

            if (!bus_conc_anticipo.GuardarBD(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdConciliacion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cp_conciliacionAnticipo_Info model = bus_conc_anticipo.GetInfo(IdEmpresa, IdConciliacion);

            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.InfoCbte = bus_cbte.get_info(model.IdEmpresa, Convert.ToInt32(model.IdTipoCbte), Convert.ToDecimal(model.IdCbteCble));
            model.Lista_det_Cbte = bus_cbte_det.get_list(model.IdEmpresa, Convert.ToInt32(model.IdTipoCbte), Convert.ToDecimal(model.IdCbteCble));

            Lista_det_OP.set_list(model.Lista_det_OP, model.IdTransaccionSession);
            Lista_det_Fact.set_list(model.Lista_det_Fact, model.IdTransaccionSession);
            list_ct_cbtecble_det.set_list(model.Lista_det_Cbte, model.IdTransaccionSession);

            cargar_combos(IdEmpresa);

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cp_conciliacionAnticipo_Info model)
        {
            model.Lista_det_OP = Lista_det_OP.get_list(model.IdTransaccionSession);
            model.Lista_det_Fact = Lista_det_Fact.get_list(model.IdTransaccionSession);
            model.Lista_det_Cbte = list_ct_cbtecble_det.get_list(model.IdTransaccionSession);
            model.IdUsuarioModificacion = SessionFixed.IdUsuario.ToString();

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            if (!bus_conc_anticipo.ModificarBD(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdConciliacion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cp_conciliacionAnticipo_Info model = bus_conc_anticipo.GetInfo(IdEmpresa, Convert.ToInt32(IdConciliacion));
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.InfoCbte = bus_cbte.get_info(model.IdEmpresa, Convert.ToInt32(model.IdTipoCbte), Convert.ToDecimal(model.IdCbteCble));
            model.Lista_det_Cbte = bus_cbte_det.get_list(model.IdEmpresa, Convert.ToInt32(model.IdTipoCbte), Convert.ToDecimal(model.IdCbteCble));

            Lista_det_OP.set_list(model.Lista_det_OP, model.IdTransaccionSession);
            Lista_det_Fact.set_list(model.Lista_det_Fact, model.IdTransaccionSession);
            list_ct_cbtecble_det.set_list(model.Lista_det_Cbte, model.IdTransaccionSession);

            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cp_conciliacionAnticipo_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario.ToString();

            if (!bus_conc_anticipo.AnularBD(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";

                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                model.Lista_det_OP = Lista_det_OP.get_list(model.IdTransaccionSession);
                model.Lista_det_Fact = Lista_det_Fact.get_list(model.IdTransaccionSession);
                model.Lista_det_Cbte = list_ct_cbtecble_det.get_list(model.IdTransaccionSession);

                Lista_det_OP.set_list(model.Lista_det_OP, model.IdTransaccionSession);
                Lista_det_Fact.set_list(model.Lista_det_Fact, model.IdTransaccionSession);
                list_ct_cbtecble_det.set_list(model.Lista_det_Cbte, model.IdTransaccionSession);

                cargar_combos(model.IdEmpresa);
                return View(model);
            };

            return RedirectToAction("Index");
        }
        #endregion

        #region Metodos
        private bool Validar(cp_conciliacionAnticipo_Info i_validar, ref string msg)
        {
            i_validar.Lista_det_OP = Lista_det_OP.get_list(i_validar.IdTransaccionSession);

            if (i_validar.Lista_det_OP.Count == 0)
            {
                mensaje = "Debe ingresar al menos una orden de pago";
                return false;
            }
            else
            {
                foreach (var item1 in i_validar.Lista_det_OP)
                {
                    var contador = 0;
                    foreach (var item2 in i_validar.Lista_det_OP)
                    {
                        if (item1.IdOrdenPago == item2.IdOrdenPago)
                        {
                            contador++;
                        }

                        if (contador > 1)
                        {
                            mensaje = "Existen ordenes de pago repetidas en el detalle";
                            return false;
                        }
                    }
                }
            }

            if (i_validar.Lista_det_Fact.Count == 0)
            {
                mensaje = "Debe ingresar al menos una factura de proveedor";
                return false;
            }

            if (i_validar.Lista_det_OP.Sum(q=>q.MontoAplicado) != i_validar.Lista_det_Fact.Sum(q => q.MontoAplicado))
            {
                var a = i_validar.Lista_det_OP.Sum(q => q.MontoAplicado);
                var b = i_validar.Lista_det_Fact.Sum(q => q.MontoAplicado);
                mensaje = "El monto aplicado entre la orden de pago y la factura por proveedor debe coincidir";
                return false;
            }


            if (i_validar.Lista_det_Cbte.Count == 0)
            {
                mensaje = "Debe ingresar registros en el detalle del diario";
                return false;
            }

            if (Math.Round(i_validar.Lista_det_Cbte.Sum(q => q.dc_Valor), 2) != 0)
            {
                mensaje = "La suma de los detalles del diario debe ser 0";
                return false;
            }

            if (i_validar.Lista_det_Cbte.Where(q => q.dc_Valor == 0).Count() > 0)
            {
                mensaje = "Existen detalles con valor 0 en el debe o haber";
                return false;
            }

            if (i_validar.Lista_det_Cbte.Where(q => string.IsNullOrEmpty(q.IdCtaCble)).Count() > 0)
            {
                mensaje = "Existen detalles sin cuenta contable";
                return false;
            }
            return true;
        }

        private void cargar_combos(int IdEmpresa)
        {
            ct_cbtecble_tipo_Bus bus_tipo_comprobante = new ct_cbtecble_tipo_Bus();
            var lst_tipo_comprobante = bus_tipo_comprobante.get_list(IdEmpresa, false);
            ViewBag.lst_tipo_comprobante = lst_tipo_comprobante;

            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;
        }
        #endregion

        #region Json
        public JsonResult GetList_OP_PorCruzar(decimal IdTransaccionSession = 0, int IdEmpresa = 0, int IdSucursal = 0, decimal IdProveedor = 0)
        {
            var lst = bus_conc_ant_op.get_list_op_x_cruzar(IdEmpresa, IdSucursal, IdProveedor);
            Lista_OP_x_Ing.set_list(lst, IdTransaccionSession);
            //Lista_det_OP.set_list(lst, IdTransaccionSession);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList_Fact_PorCruzar(decimal IdTransaccionSession = 0, int IdEmpresa = 0, int IdSucursal = 0, decimal IdProveedor = 0)
        {
            var Usuario = SessionFixed.IdUsuario;
            var lst = bus_conc_ant_fact.get_list_facturas_x_cruzar(IdEmpresa, IdSucursal, IdProveedor, Usuario);
            Lista_Fact_x_Ing.set_list(lst, IdTransaccionSession);
            //Lista_det_Fact.set_list(lst, IdTransaccionSession);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult armar_diario(decimal IdTransaccionSession=0, int IdEmpresa = 0, decimal IdProveedor = 0)
        {
            info_proveedor = bus_prov.get_info(IdEmpresa, IdProveedor);
            info_parametro = bus_param.get_info(IdEmpresa);

            list_ct_cbtecble_det.set_list(new List<ct_cbtecble_det_Info>(), IdTransaccionSession);
            var lst_det_anticipo = Lista_det_OP.get_list(IdTransaccionSession);

            var MontoAplicado = Math.Round(lst_det_anticipo.Sum(q => q.MontoAplicado), 2, MidpointRounding.AwayFromZero);

            #region Detalle Diaro
            list_ct_cbtecble_det.AddRow(new ct_cbtecble_det_Info
            {
                IdCtaCble = info_proveedor.IdCtaCble_CXP,
                dc_Valor_debe = Math.Round(MontoAplicado, 2, MidpointRounding.AwayFromZero),
                dc_Valor = Math.Round(MontoAplicado * 1, 2, MidpointRounding.AwayFromZero),
                dc_Observacion = "",
            }, IdTransaccionSession);

            list_ct_cbtecble_det.AddRow(new ct_cbtecble_det_Info
            {
                IdCtaCble = info_proveedor.IdCtaCble_Anticipo,
                dc_Valor_haber = Math.Round(MontoAplicado, 2, MidpointRounding.AwayFromZero),
                dc_Valor = Math.Round(MontoAplicado * -1, 2, MidpointRounding.AwayFromZero),
                dc_Observacion = "",
            }, IdTransaccionSession);
            #endregion

            var lst_cbte_det = list_ct_cbtecble_det.get_list(IdTransaccionSession);
            list_ct_cbtecble_det.set_list(lst_cbte_det, IdTransaccionSession);

            return Json(new { lst_cbte_det }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region OP por ingresar
        public ActionResult GridViewPartial_ConciliacionAnticipo_OP_x_Cruzar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_OP_x_Ing.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ConciliacionAnticipo_OP_x_Cruzar", model);
        }
        #endregion

        #region Facturas por ingresar
        public ActionResult GridViewPartial_ConciliacionAnticipo_Fact_x_Cruzar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Fact_x_Ing.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ConciliacionAnticipo_Fact_x_Cruzar", model);
        }
        #endregion

        #region Metodos del detalle OP
        public ActionResult GridViewPartial_ConciliacionAnticipo_OP_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_det_OP.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ConciliacionAnticipo_OP_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditingAddNew_OP(string IDs = "", decimal IdTransaccionSession = 0)
        {
            if (IDs != "")
            {
                int IdEmpresaSesion = Convert.ToInt32(SessionFixed.IdEmpresa);
                var lst_x_ingresar = Lista_OP_x_Ing.get_list(IdTransaccionSession);
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    var repetido = 0;
                    var info_det = lst_x_ingresar.Where(q => q.IdOrdenPago == Convert.ToInt32(item)).FirstOrDefault();
                    var detalle_op = Lista_det_OP.get_list(IdTransaccionSession);
                    cp_ConciliacionAnticipoDetAnt_Info info_det_op = new cp_ConciliacionAnticipoDetAnt_Info();

                    if (info_det != null)
                    {
                        foreach (var item2 in detalle_op)
                        {
                            if (info_det.IdOrdenPago == item2.IdOrdenPago)
                            {
                                repetido = 1;
                                break;
                            }
                        }

                        if (repetido == 0)
                        {
                            info_det_op.IdEmpresa = info_det.IdEmpresa;
                            info_det_op.IdOrdenPago = info_det.IdOrdenPago;
                            info_det_op.IdConciliacion = info_det.IdConciliacion;
                            info_det_op.MontoAplicado = info_det.MontoAplicado;
                            info_det_op.Fecha = info_det.Fecha;
                            info_det_op.Observacion = info_det.Observacion;
                            Lista_det_OP.AddRow(info_det_op, IdTransaccionSession);
                        }                        
                    }
                }
            }

            var model = Lista_det_OP.get_list(IdTransaccionSession);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_OP([ModelBinder(typeof(DevExpressEditorsBinder))] cp_ConciliacionAnticipoDetAnt_Info info_det_op)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            Lista_det_OP.UpdateRow(info_det_op, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_det_OP.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ConciliacionAnticipo_OP_det", model);
        }

        public ActionResult EditingDelete_OP(int Secuencia)
        {
            Lista_det_OP.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_det_OP.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ConciliacionAnticipo_OP_det", model);
        }
        #endregion

        #region Metodos del detalle Facturas
        public ActionResult GridViewPartial_ConciliacionAnticipo_Fact_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_det_Fact.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ConciliacionAnticipo_Fact_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditingAddNew_Fact(string IDs = "", decimal IdTransaccionSession = 0)
        {
            if (IDs != "")
            {
                int IdEmpresaSesion = Convert.ToInt32(SessionFixed.IdEmpresa);
                var lst_x_ingresar = Lista_Fact_x_Ing.get_list(IdTransaccionSession);
                var detalle_fact = Lista_det_Fact.get_list(IdTransaccionSession);
                string[] array = IDs.Split(',');

                foreach (var item in array)
                {
                    var info_det = lst_x_ingresar.Where(q => q.IdOrdenPago == Convert.ToInt32(item)).FirstOrDefault();
                    var repetido = 0;
                    cp_ConciliacionAnticipoDetCXP_Info info_det_fact = new cp_ConciliacionAnticipoDetCXP_Info();

                    if (info_det != null)
                    {
                        foreach (var item2 in detalle_fact)
                        {
                            if (info_det.IdOrdenPago == item2.IdOrdenPago)
                            {
                                repetido = 1;
                                break;
                            }
                        }

                        if (repetido == 0)
                        {
                            var info_tipocbte = bus_tipocbte.get_info(info_det.IdTipoCbte_cxp);
                            info_det_fact.IdEmpresa = info_det.IdEmpresa;
                            info_det_fact.IdOrdenPago = info_det.IdOrdenPago;
                            info_det_fact.IdConciliacion = info_det.IdConciliacion;
                            info_det_fact.IdEmpresa_cxp = info_det.IdEmpresa_cxp;
                            info_det_fact.IdTipoCbte_cxp = info_det.IdTipoCbte_cxp;
                            info_det_fact.tc_TipoCbte = info_tipocbte.tc_TipoCbte;
                            info_det_fact.IdCbteCble_cxp = info_det.IdCbteCble_cxp;
                            info_det_fact.MontoAplicado = info_det.MontoAplicado;
                            info_det_fact.Fecha_cxp = info_det.Fecha_cxp;
                            info_det_fact.Observacion_cxp = info_det.Observacion_cxp;
                            Lista_det_Fact.AddRow(info_det_fact, IdTransaccionSession);
                        }                        
                    }
                }
            }

            var model = Lista_det_Fact.get_list(IdTransaccionSession);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_Fact([ModelBinder(typeof(DevExpressEditorsBinder))] cp_ConciliacionAnticipoDetCXP_Info info_det_op)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det_op != null)
            {
                Lista_det_Fact.UpdateRow(info_det_op, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            
            var model = Lista_det_Fact.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ConciliacionAnticipo_Fact_det", model);
        }

        public ActionResult EditingDelete_Fact(int Secuencia)
        {
            Lista_det_Fact.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_det_Fact.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ConciliacionAnticipo_Fact_det", model);
        }
        #endregion
        
    }

    public class cp_ConciliacionAnticipoDetAnt_x_Ingresar_List
    {
        string Variable = "cp_ConciliacionAnticipoDetAnt_x_Ingresar_Info";
        public List<cp_ConciliacionAnticipoDetAnt_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_ConciliacionAnticipoDetAnt_Info> list = new List<cp_ConciliacionAnticipoDetAnt_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_ConciliacionAnticipoDetAnt_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_ConciliacionAnticipoDetAnt_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
    public class cp_ConciliacionAnticipoDetAnt_List
    {
        string Variable = "cp_ConciliacionAnticipoDetAnt_Info";
        public List<cp_ConciliacionAnticipoDetAnt_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_ConciliacionAnticipoDetAnt_Info> list = new List<cp_ConciliacionAnticipoDetAnt_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_ConciliacionAnticipoDetAnt_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_ConciliacionAnticipoDetAnt_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cp_ConciliacionAnticipoDetAnt_Info info_det, decimal IdTransaccionSession)
        {
            List<cp_ConciliacionAnticipoDetAnt_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;

            list.Add(info_det);
        }

        public void UpdateRow(cp_ConciliacionAnticipoDetAnt_Info info_det, decimal IdTransaccionSession)
        {
            cp_ConciliacionAnticipoDetAnt_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).FirstOrDefault();
            edited_info.MontoAplicado = info_det.MontoAplicado;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<cp_ConciliacionAnticipoDetAnt_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }
    public class cp_ConciliacionAnticipoDetCXP_x_Ingresar_List
    {
        string Variable = "cp_ConciliacionAnticipoDetCXP_x_Ingresar_Info";
        public List<cp_ConciliacionAnticipoDetCXP_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_ConciliacionAnticipoDetCXP_Info> list = new List<cp_ConciliacionAnticipoDetCXP_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_ConciliacionAnticipoDetCXP_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_ConciliacionAnticipoDetCXP_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
    public class cp_ConciliacionAnticipoDetCXP_List
    {
        string Variable = "cp_ConciliacionAnticipoDetCXP_Info";
        public List<cp_ConciliacionAnticipoDetCXP_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_ConciliacionAnticipoDetCXP_Info> list = new List<cp_ConciliacionAnticipoDetCXP_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_ConciliacionAnticipoDetCXP_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_ConciliacionAnticipoDetCXP_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cp_ConciliacionAnticipoDetCXP_Info info_det, decimal IdTransaccionSession)
        {
            List<cp_ConciliacionAnticipoDetCXP_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;

            list.Add(info_det);
        }

        public void UpdateRow(cp_ConciliacionAnticipoDetCXP_Info info_det, decimal IdTransaccionSession)
        {
            ct_cbtecble_tipo_Bus bus_tipocbte = new ct_cbtecble_tipo_Bus();

            cp_ConciliacionAnticipoDetCXP_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).FirstOrDefault();
            var info_tipocbte = bus_tipocbte.get_info(edited_info.IdTipoCbte_cxp);

            edited_info.tc_TipoCbte = info_tipocbte.tc_TipoCbte;
            edited_info.MontoAplicado = info_det.MontoAplicado;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<cp_ConciliacionAnticipoDetCXP_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }
}