using DevExpress.Web.Mvc;
using Core.Erp.Bus.Banco;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Banco;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    [SessionTimeout]
    public class ConciliacionBancoController : Controller
    {
        #region Variables
        ba_Conciliacion_Bus bus_conciliacion = new ba_Conciliacion_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        ba_Banco_Cuenta_Bus bus_banco_cuenta = new ba_Banco_Cuenta_Bus();
        ba_Conciliacion_det_IngEgr_Bus bus_det = new ba_Conciliacion_det_IngEgr_Bus();
        ba_Conciliacion_det_IngEgr_List List_det = new ba_Conciliacion_det_IngEgr_List();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        string mensaje = string.Empty;
        int IdBanco = 0;

        ba_Conciliacion_det_Bus bus_detalle_con = new ba_Conciliacion_det_Bus();
        ba_Conciliacion_det_List Lista_detalle = new ba_Conciliacion_det_List();
        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);
        }
        public ActionResult GridViewPartial_ConciliacionBanco(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini).Date;
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin).Date;
            var model = bus_conciliacion.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_ConciliacionBanco", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa, int IdSucursal, ref int IdBanco)
        {
            var lst_periodo = bus_periodo.get_list(IdEmpresa, false);
            ViewBag.lst_periodo = lst_periodo;

            var lst_banco_cuenta = bus_banco_cuenta.get_list(IdEmpresa, IdSucursal, false);
            IdBanco = IdBanco == 0 ? (lst_banco_cuenta.Count == 0 ? 0 : lst_banco_cuenta.Min(q=> q.IdBanco)) : IdBanco;
            ViewBag.lst_banco_cuenta = lst_banco_cuenta;

            Dictionary<string, string> lst_estado = new Dictionary<string, string>();
            lst_estado.Add(cl_enumeradores.eEstadoCierreBanco.PRE_CONCIL.ToString(), "PRE-CONCILIADO");
            lst_estado.Add(cl_enumeradores.eEstadoCierreBanco.CONCILIADO.ToString(), "CONCILIADO");
            ViewBag.lst_estado = lst_estado;
        }
        private bool validar(ba_Conciliacion_Info i_validar, ref string msg)
        {
            i_validar.co_totalIng = Math.Round(Math.Round(List_det.get_list(i_validar.IdTransaccionSession).Where(q => q.tipo_IngEgr == "+" && q.seleccionado == true).Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero) + (double)Math.Round(Lista_detalle.get_list(i_validar.IdTransaccionSession).Where(q => q.tipo_IngEgr == "+" && q.Seleccionado == true).Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero), 2);
            i_validar.co_totalEgr = Math.Round(Math.Round(List_det.get_list(i_validar.IdTransaccionSession).Where(q => q.tipo_IngEgr == "-" && q.seleccionado == true).Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero) - (double)Math.Round(Lista_detalle.get_list(i_validar.IdTransaccionSession).Where(q => q.tipo_IngEgr == "-" && q.Seleccionado == true).Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero), 2);
            i_validar.co_SaldoConciliado = Math.Round(i_validar.co_SaldoBanco_anterior + i_validar.co_totalIng + i_validar.co_totalEgr, 2, MidpointRounding.AwayFromZero);
            i_validar.co_Diferencia = Math.Round(i_validar.co_SaldoConciliado - i_validar.co_SaldoBanco_EstCta, 2, MidpointRounding.AwayFromZero);

            if (i_validar.IdEstado_Concil_Cat == cl_enumeradores.eEstadoCierreBanco.CONCILIADO.ToString() && i_validar.co_Diferencia != 0)
            {
                msg = "No se puede asignar el estado de conciliado si la diferencia es diferente a 0";
                return false;
            }

            if (i_validar.IdEstado_Concil_Cat == cl_enumeradores.eEstadoCierreBanco.PRE_CONCIL.ToString())
                i_validar.lst_det = List_det.get_list(i_validar.IdTransaccionSession).Where(q => q.seleccionado == true).ToList();
            else
                i_validar.lst_det = List_det.get_list(i_validar.IdTransaccionSession);

            if (i_validar.IdConciliacion == 0)
            {
                if(bus_conciliacion.ExisteConciliacion(i_validar.IdEmpresa, i_validar.IdPeriodo, i_validar.IdBanco))
                {
                    msg = "Ya existe una conciliación para el banco en el periodo seleccionado";
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Nuevo
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ba_Conciliacion_Info model = new ba_Conciliacion_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                co_Fecha = DateTime.Now.Date,
                IdPeriodo = Convert.ToInt32(DateTime.Now.Date.AddMonths(-1).ToString("yyyyMM")),
                lst_det = new List<ba_Conciliacion_det_IngEgr_Info>(),
                List_detalle = new List<ba_Conciliacion_det_Info>()
            };
            Lista_detalle.set_list(model.List_detalle, model.IdTransaccionSession);
            cargar_combos(IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal), ref IdBanco);
            model.IdBanco = IdBanco;

            var bco = bus_banco_cuenta.get_info(model.IdEmpresa, model.IdBanco);
            var periodo = bus_periodo.get_info(model.IdEmpresa, model.IdPeriodo);            
            if (bco != null && periodo != null)
                model.lst_det = bus_det.get_list_x_conciliar(model.IdEmpresa, model.IdBanco, bco.IdCtaCble, periodo.pe_FechaFin);            
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ba_Conciliacion_Info model)
        {
            model.List_detalle = Lista_detalle.get_list(model.IdTransaccionSession); 
            if (!validar(model,ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal),ref IdBanco);
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }
            if (!bus_conciliacion.guardarDB(model))
            {
                ViewBag.mensaje = "No se pudo guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal), ref IdBanco);
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Json
        public JsonResult GetSaldoContableAnt(int IdBanco = 0, int IdPeriodo = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            double resultado = 0;
            var bco = bus_banco_cuenta.get_info(IdEmpresa, IdBanco);
            var periodo = bus_periodo.get_info(IdEmpresa, IdPeriodo);
            if (bco != null && periodo != null)
                resultado = bus_plancta.get_saldo_anterior(IdEmpresa, bco.IdCtaCble, periodo.pe_FechaIni);

            return Json(Math.Round(resultado, 2, MidpointRounding.AwayFromZero), JsonRequestBehavior.AllowGet);
        }
        public void CargarMovimientos(int IdBanco = 0, int IdPeriodo = 0, decimal IdTransaccionSession=0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var bco = bus_banco_cuenta.get_info(IdEmpresa, IdBanco);
            var periodo = bus_periodo.get_info(IdEmpresa, IdPeriodo);
            List<ba_Conciliacion_det_IngEgr_Info> lst_det = new List<ba_Conciliacion_det_IngEgr_Info>();
            if (bco != null && periodo != null)
                lst_det = bus_det.get_list_x_conciliar(IdEmpresa, IdBanco, bco.IdCtaCble, periodo.pe_FechaFin);
            List_det.set_list(lst_det, IdTransaccionSession);
        }

        public void EditingUpdate(string IdPk = "", decimal IdTransaccionSession=0)
        {
            List_det.UpdateRow(IdPk, IdTransaccionSession);
        }
        #endregion

        public ActionResult GridViewPartial_ConciliacionBanco_x_cruzar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q=>q.seleccionado == false).ToList();
            return PartialView("_GridViewPartial_ConciliacionBanco_x_cruzar", model);
        }

        public ActionResult GridViewPartial_ConciliacionBanco_det()
        {
            var x = Request.Params["TransaccionFixed"];
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList();
            return PartialView("_GridViewPartial_ConciliacionBanco_det", model);
        }
        #region Modificar
        public ActionResult Modificar(int IdEmpresa = 0, decimal IdConciliacion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ba_Conciliacion_Info model = bus_conciliacion.get_info(IdEmpresa,IdConciliacion);
            if(model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_det.get_list(IdEmpresa, IdConciliacion);
            model.List_detalle = bus_detalle_con.GetList(model.IdEmpresa, model.IdConciliacion);
            Lista_detalle.set_list(model.List_detalle, model.IdTransaccionSession);
            cargar_combos(IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal), ref IdBanco);            
            var bco = bus_banco_cuenta.get_info(IdEmpresa, model.IdBanco);
            var periodo = bus_periodo.get_info(IdEmpresa, model.IdPeriodo);
            model.lst_det.AddRange(bus_det.get_list_x_conciliar(IdEmpresa, model.IdBanco, bco.IdCtaCble, periodo.pe_FechaFin.Date));
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ba_Conciliacion_Info model)
        {
            model.List_detalle = Lista_detalle.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal), ref IdBanco);
                return View(model);
            }
            if (!bus_conciliacion.modificarDB(model))
            {
                ViewBag.mensaje = "No se pudo guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal), ref IdBanco);
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult Anular(int IdEmpresa = 0, decimal IdConciliacion = 0)
        {
            ba_Conciliacion_Info model = bus_conciliacion.get_info(IdEmpresa, IdConciliacion);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_det.get_list(IdEmpresa, IdConciliacion);
            
            cargar_combos(IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal), ref IdBanco);

            var bco = bus_banco_cuenta.get_info(IdEmpresa, model.IdBanco);
            var periodo = bus_periodo.get_info(IdEmpresa, model.IdPeriodo);
            model.lst_det.AddRange(bus_det.get_list_x_conciliar(IdEmpresa, model.IdBanco, bco.IdCtaCble, periodo.pe_FechaFin.Date));
            List_det.set_list(model.lst_det, model.IdTransaccionSession);

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ba_Conciliacion_Info model)
        {
            model.IdUsuario_Anu = SessionFixed.IdUsuario;
            if (!bus_conciliacion.anularDB(model))
            {
                ViewBag.mensaje = "No se pudo guardar el registro";
                cargar_combos(model.IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal), ref IdBanco);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public JsonResult Calcular(double co_SaldoContable_MesAnt = 0, double co_SaldoBanco_anterior = 0, double co_SaldoBanco_EstCta = 0 )
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            ba_Conciliacion_valores_Info resultado = new ba_Conciliacion_valores_Info
            {
                co_totalIng = Math.Round(Math.Round(List_det.get_list(IdTransaccionSession).Where(q => q.tipo_IngEgr == "+" && q.seleccionado == true).Sum(q => q.dc_Valor),2,MidpointRounding.AwayFromZero) + (double) Math.Round(Lista_detalle.get_list(IdTransaccionSession).Where(q => q.tipo_IngEgr == "+" && q.Seleccionado == true).Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero),2),
                co_totalEgr = Math.Round(Math.Round(List_det.get_list(IdTransaccionSession).Where(q => q.tipo_IngEgr == "-" && q.seleccionado == true).Sum(q => q.dc_Valor),2,MidpointRounding.AwayFromZero) - (double) Math.Round(Lista_detalle.get_list(IdTransaccionSession).Where(q => q.tipo_IngEgr == "-" && q.Seleccionado == true).Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero),2)
            };
            resultado.co_SaldoConciliado = Math.Round(co_SaldoBanco_anterior + resultado.co_totalIng + resultado.co_totalEgr,2,MidpointRounding.AwayFromZero);
            resultado.co_Diferencia = Math.Round(resultado.co_SaldoConciliado - co_SaldoBanco_EstCta,2,MidpointRounding.AwayFromZero);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }




        #region Detalle

        private void cargar_combos_Detalle()
        {
            ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Bus bus_tipo = new ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Bus();
            var lst_tipo = bus_tipo.GetList(Convert.ToInt32(SessionFixed.IdEmpresa));
            ViewBag.lst_tipo = lst_tipo;
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_banco_conciliacion_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            cargar_combos_Detalle();
            var model = Lista_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_banco_conciliacion_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew_det([ModelBinder(typeof(DevExpressEditorsBinder))] ba_Conciliacion_det_Info info_det)
        {
            if (ModelState.IsValid)
                Lista_detalle.AddRow_det(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_banco_conciliacion_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_det([ModelBinder(typeof(DevExpressEditorsBinder))] ba_Conciliacion_det_Info info_det)
        {

            if (ModelState.IsValid)
                Lista_detalle.UpdateRow_det(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_banco_conciliacion_det", model);
        }
        public ActionResult EditingDelete_det(int Secuencia)
        {
            Lista_detalle.DeleteRow_det(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_banco_conciliacion_det", model);
        }

        #endregion
    }

    public class ba_Conciliacion_valores_Info
    {
        public double co_totalIng { get; set; }
        public double co_totalEgr { get; set; }
        public double co_SaldoContable_MesAct { get; set; }
        public double co_SaldoBanco_anterior { get; set; }
        public double co_SaldoConciliado { get; set; }
        public double co_Diferencia { get; set; }
    }

    public class ba_Conciliacion_det_IngEgr_List
    {
        public List<ba_Conciliacion_det_IngEgr_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session["ba_Conciliacion_det_IngEgr_Info" + IdTransaccionSession.ToString()] == null)
            {
                List<ba_Conciliacion_det_IngEgr_Info> list = new List<ba_Conciliacion_det_IngEgr_Info>();

                HttpContext.Current.Session["ba_Conciliacion_det_IngEgr_Info" + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_Conciliacion_det_IngEgr_Info>)HttpContext.Current.Session["ba_Conciliacion_det_IngEgr_Info" + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_Conciliacion_det_IngEgr_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session["ba_Conciliacion_det_IngEgr_Info" + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(string IdPk, decimal IdTransaccionSession)
        {
            ba_Conciliacion_det_IngEgr_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdPK == IdPk).FirstOrDefault();
            if(edited_info != null)
                edited_info.seleccionado = !edited_info.seleccionado;
        }
    }

    public class ba_Conciliacion_det_List
    {
        string Variable = "ba_Conciliacion_det_Info";
        ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Bus bus_tipo = new ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Bus();

        public List<ba_Conciliacion_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_Conciliacion_det_Info> list = new List<ba_Conciliacion_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_Conciliacion_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_Conciliacion_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow_det(ba_Conciliacion_det_Info info_det, decimal IdTransaccionSession)
        {
            List<ba_Conciliacion_det_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            var tipo = bus_tipo.GetList(Convert.ToInt32(SessionFixed.IdEmpresa)).Where(q=> q.IdTipoCbteCble == info_det.IdTipocbte).FirstOrDefault();
            if (tipo != null)
            {
                info_det.tipo_IngEgr = tipo.Tipo_DebCred == "C" ? "+" : "-";
            }
            list.Add(info_det);
        }

        public void UpdateRow_det(ba_Conciliacion_det_Info info_det, decimal IdTransaccionSession)
        {
            ba_Conciliacion_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdTipocbte = info_det.IdTipocbte;
            var tipo = bus_tipo.GetList(Convert.ToInt32(SessionFixed.IdEmpresa)).Where(q => q.IdTipoCbteCble == info_det.IdTipocbte).FirstOrDefault();
            if (tipo != null)
            {
                edited_info.tipo_IngEgr = tipo.Tipo_DebCred == "C" ? "+" : "-";
            }
            edited_info.Referencia = info_det.Referencia;
            edited_info.Fecha = info_det.Fecha;
            edited_info.Valor = info_det.Valor;
            edited_info.Observacion = info_det.Observacion;            
            edited_info.Seleccionado = info_det.Seleccionado;
        }

        public void DeleteRow_det(int Secuencia, decimal IdTransaccionSession)
        {
            List<ba_Conciliacion_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

}