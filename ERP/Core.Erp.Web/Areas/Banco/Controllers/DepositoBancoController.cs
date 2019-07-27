using Core.Erp.Bus.Banco;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Info.Banco;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Areas.Contabilidad.Controllers;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    [SessionTimeout]
    public class DepositoBancoController : Controller
    {
        #region Variables
        ba_Cbte_Ban_Bus bus_cbteban = new ba_Cbte_Ban_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ba_tipo_nota_Bus bus_tipo_nota = new ba_tipo_nota_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        ba_Banco_Cuenta_Bus bus_banco_cuenta = new ba_Banco_Cuenta_Bus();
        ct_cbtecble_det_List List_ct = new ct_cbtecble_det_List();
        ct_cbtecble_det_Bus bus_det_ct = new ct_cbtecble_det_Bus();
        ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Bus bus_det = new ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Bus();
        ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List List_ing = new ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List();
        ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal = new ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal();

        
        ba_Banco_Flujo_Det_List_Deposito List_Flujo = new ba_Banco_Flujo_Det_List_Deposito();
        ba_Cbte_Ban_x_ba_TipoFlujo_Bus bus_flujo = new ba_Cbte_Ban_x_ba_TipoFlujo_Bus();

        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combos_consulta();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            cargar_combos_consulta();
            return View(model);
        }
        private void cargar_combos_consulta()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            lst_sucursal.Add(new tb_sucursal_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = 0,
                Su_Descripcion = "Todos"
            });
            ViewBag.lst_sucursal = lst_sucursal;
        }
        public ActionResult GridViewPartial_DepositoBanco(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_cbteban.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin, IdSucursal, cl_enumeradores.eTipoCbteBancario.DEPO.ToString(), true);
            return PartialView("_GridViewPartial_DepositoBanco", model);
        }
        #endregion

        #region Metodos ComboBox bajo demanda flujo
        ba_TipoFlujo_Bus bus_tipo = new ba_TipoFlujo_Bus();
        public ActionResult CmbFlujo_Deposito()
        {
            decimal model = new decimal();
            return PartialView("_CmbFlujo_Deposito", model);
        }
        public List<ba_TipoFlujo_Info> get_list_bajo_demandaFlujo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_tipo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoIngEgr.ING.ToString());
        }
        public ba_TipoFlujo_Info get_info_bajo_demandaFlujo(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_tipo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa, int IdSucursal)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_banco_cuenta = bus_banco_cuenta.get_list(IdEmpresa, IdSucursal, false);
            ViewBag.lst_banco_cuenta = lst_banco_cuenta;
        }
        private bool validar(ba_Cbte_Ban_Info i_validar, ref string msg)
        {
            i_validar.lst_det_ct = List_ct.get_list(i_validar.IdTransaccionSession);
            i_validar.lst_det_ing = List_ing.get_list();


            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.cb_Fecha, cl_enumeradores.eModulo.BANCO, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.cb_Fecha, cl_enumeradores.eModulo.CONTA, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            if (i_validar.lst_det_ing.Count == 0)
            {
                msg = "No ha seleccionado ingresos a ser depositados";
                return false;
            }
            if (i_validar.lst_det_ct.Count == 0)
            {
                msg = "El detalle del diario se encuentra vacío";
                return false;
            }

            foreach (var item in i_validar.lst_det_ct)
            {
                if (string.IsNullOrEmpty(item.IdCtaCble))
                {
                    mensaje = "Faltan cuentas contables, por favor verifique";
                    return false;
                }
            }
            if (Math.Round(i_validar.lst_det_ct.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero) != 0)
            {
                mensaje = "La suma de los detalles debe ser 0, por favor verifique";
                return false;
            }
            if (i_validar.lst_det_ct.Where(q => q.dc_Valor == 0).Count() > 0)
            {
                mensaje = "Existen detalles con valor 0 en el debe o haber, por favor verifique";
                return false;
            }

            if (i_validar.list_det.Count() > 0)
            {
                if (Math.Round(i_validar.list_det.Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero) != Convert.ToDouble(i_validar.cb_Valor))
                {
                    mensaje = "La suma de los detalles del flujo debe ser igual a el valor del documento";
                    return false;
                }
            }
            var cta = bus_banco_cuenta.get_info(i_validar.IdEmpresa, i_validar.IdBanco);
            if (cta == null)
            {
                mensaje = "Selecciona la cuenta bancaria";
                return false;
            }
            if (cta.EsFlujoObligatorio)
            {
                if (i_validar.list_det.Count == 0)
                {
                    mensaje = "Falta distribución de flujo";
                    return false;
                }
            }
            i_validar.IdPeriodo = Convert.ToInt32(i_validar.cb_Fecha.ToString("yyyyMM"));
            i_validar.IdUsuario = SessionFixed.IdUsuario;
            i_validar.IdUsuarioUltMod = SessionFixed.IdUsuario;
            i_validar.IdUsuario_Anu = SessionFixed.IdUsuario;
            i_validar.IdUsuarioUltMod = SessionFixed.IdUsuario;
            i_validar.cb_Valor = Math.Round(i_validar.lst_det_ct.Sum(q => q.dc_Valor_debe), 2, MidpointRounding.AwayFromZero);
            return true;
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
            ba_Cbte_Ban_Info model = new ba_Cbte_Ban_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                CodTipoCbteBan = cl_enumeradores.eTipoCbteBancario.DEPO.ToString(),
                cb_Fecha = DateTime.Now.Date,
                lst_det_ct = new List<ct_cbtecble_det_Info>(),
                lst_det_ing = new List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info>(),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                list_det = new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>()
            };
            List_ct.set_list(model.lst_det_ct,model.IdTransaccionSession);
            List_ing.set_list(model.lst_det_ing);
            List_Flujo.set_list(model.list_det, model.IdTransaccionSession);
            cargar_combos(IdEmpresa, model.IdSucursal);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ba_Cbte_Ban_Info model)
        {
            model.list_det = List_Flujo.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            if (!bus_cbteban.guardarDB(model, cl_enumeradores.eTipoCbteBancario.DEPO))
            {
                ViewBag.mensaje = "No se pudo guardar el registro";
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipocbte = model.IdTipocbte, IdCbteCble = model.IdCbteCble, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdTipocbte = 0, decimal IdCbteCble = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ba_Cbte_Ban_Info model = bus_cbteban.get_info(IdEmpresa, IdTipocbte, IdCbteCble);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.list_det = bus_flujo.GetList(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_Flujo.set_list(model.list_det, model.IdTransaccionSession);
            model.lst_det_ct = bus_det_ct.get_list(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            model.lst_det_ing = bus_det.get_list(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_ct.set_list(model.lst_det_ct,model.IdTransaccionSession);
            List_ing.set_list(model.lst_det_ing);
            cargar_combos(IdEmpresa, model.IdSucursal);
            SessionFixed.TipoPersona = model.IdTipo_Persona;

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cb_Fecha, cl_enumeradores.eModulo.BANCO, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ba_Cbte_Ban_Info model)
        {
            model.list_det = List_Flujo.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            if (!bus_cbteban.modificarDB(model, cl_enumeradores.eTipoCbteBancario.DEPO))
            {
                ViewBag.mensaje = "No se pudo modificar el registro";
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipocbte = model.IdTipocbte, IdCbteCble = model.IdCbteCble, Exito = true });
        }


        public ActionResult Anular(int IdEmpresa = 0, int IdTipocbte = 0, decimal IdCbteCble = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ba_Cbte_Ban_Info model = bus_cbteban.get_info(IdEmpresa, IdTipocbte, IdCbteCble);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.list_det = bus_flujo.GetList(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_Flujo.set_list(model.list_det, model.IdTransaccionSession);
            model.lst_det_ct = bus_det_ct.get_list(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_ct.set_list(model.lst_det_ct, model.IdTransaccionSession);
            model.lst_det_ing = bus_det.get_list(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_ing.set_list(model.lst_det_ing);
            cargar_combos(IdEmpresa, model.IdSucursal);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cb_Fecha, cl_enumeradores.eModulo.BANCO, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(ba_Cbte_Ban_Info model)
        {
            model.IdUsuario_Anu = SessionFixed.IdUsuario;
            if (!bus_cbteban.anularDB(model))
            {
                ViewBag.mensaje = "No se pudo anular el registro";
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Detalles

        public ActionResult GridViewPartial_DepositoBanco_x_cruzar()
        {
            
            List <ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> model;
           model = ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal.get_list();

            return PartialView("_GridViewPartial_DepositoBanco_x_cruzar", model);
        }

        public ActionResult GridViewPartial_DepositoBanco_det()
        {
            var model = List_ing.get_list();
            return PartialView("_GridViewPartial_DepositoBanco_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew(string IDs = "")
        {
            if (IDs != "")
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> lst_x_cruzar;
                lst_x_cruzar = ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal.get_list();
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    var info_det = lst_x_cruzar.Where(q => q.mcj_IdCbteCble == Convert.ToInt32(item)).FirstOrDefault();
                    if (info_det != null)
                        List_ing.AddRow(info_det);
                }
            }
            var model = List_ing.get_list();
            return PartialView("_GridViewPartial_DepositoBanco_det", model);
        }

        public ActionResult EditingDelete(decimal mcj_IdCbteCble)
        {
            List_ing.DeleteRow(mcj_IdCbteCble);
            var model = List_ing.get_list();
            return PartialView("_GridViewPartial_DepositoBanco_det", model);
        }
        #endregion

        #region Json
        public JsonResult armar_diario(int IdEmpresa = 0, int IdBanco = 0, decimal IdTransaccionSession = 0)
        {
            List_ct.set_list(new List<ct_cbtecble_det_Info>(), IdTransaccionSession);
            var bco = bus_banco_cuenta.get_info(IdEmpresa, IdBanco);
            var lst_op = List_ing.get_list();

            var lst = (from q in lst_op
                       group q by q.IdCtaCble
                      into g
                       select new
                       {
                           IdCtaCble = g.Key,
                           cr_Valor = g.Sum(q => q.cr_Valor)
                       }).ToList();

            foreach (var item in lst)
            {
                //Haber
                List_ct.AddRow(new ct_cbtecble_det_Info
                {
                    IdCtaCble = item.IdCtaCble,
                    dc_Valor = Math.Round(item.cr_Valor, 2, MidpointRounding.AwayFromZero)*-1,
                    dc_Valor_haber = Math.Round(item.cr_Valor, 2, MidpointRounding.AwayFromZero)
                },IdTransaccionSession);
            }
            List_ct.AddRow(new ct_cbtecble_det_Info
            {
                IdCtaCble = bco.IdCtaCble,
                dc_Valor = Math.Round(lst.Sum(q => q.cr_Valor), 2, MidpointRounding.AwayFromZero),
                dc_Valor_debe = Math.Round(lst.Sum(q => q.cr_Valor), 2, MidpointRounding.AwayFromZero),
                dc_para_conciliar = true
            }, IdTransaccionSession);
            
            return Json(Math.Round(lst.Sum(q => q.cr_Valor), 2, MidpointRounding.AwayFromZero), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Filtrar_GridViewPartial_DepositoBanco_x_cruzar(int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> model;
            model = bus_det.get_list_x_depositar(IdEmpresa, IdSucursal);

            ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal.set_list(model);


            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List
    {
        public List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> get_list()
        {
            if (HttpContext.Current.Session["ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info"] == null)
            {
                List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> list = new List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info>();

                HttpContext.Current.Session["ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info"] = list;
            }
            return (List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info>)HttpContext.Current.Session["ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info"];
        }

        public void set_list(List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> list)
        {
            HttpContext.Current.Session["ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info"] = list;
        }

        public void AddRow(ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info info_det)
        {
            List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> list = get_list();            
            if (list.Where(q => q.mcj_IdCbteCble == info_det.mcj_IdCbteCble).Count() == 0)
                list.Add(info_det);
        }

        public void DeleteRow(decimal mcj_IdCbteCble)
        {
            List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> list = get_list();
            list.Remove(list.Where(m => m.mcj_IdCbteCble == mcj_IdCbteCble).First());
        }
    }

    public class ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal
    {
        public List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> get_list()
        {
            if (HttpContext.Current.Session["ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal"] == null)
            {
                List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> list = new List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info>();

                HttpContext.Current.Session["ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal"] = list;
            }
            return (List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info>)HttpContext.Current.Session["ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal"];
        }


        public void set_list(List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> list)
        {
            HttpContext.Current.Session["ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_List_x_sucursal"] = list;
        }

    }

    public class ba_Banco_Flujo_Det_List_Deposito
    {
        string Variable = "ba_Cbte_Ban_x_ba_TipoFlujo_Info";
        public List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list = new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ba_Cbte_Ban_x_ba_TipoFlujo_Info info_det, decimal IdTransaccionSession)
        {
            List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;


            list.Add(info_det);
        }

        public void UpdateRow(ba_Cbte_Ban_x_ba_TipoFlujo_Info info_det, decimal IdTransaccionSession)
        {
            ba_Cbte_Ban_x_ba_TipoFlujo_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdTipocbte = info_det.IdTipocbte;
            edited_info.IdCbteCble = info_det.IdCbteCble;
            edited_info.Porcentaje = info_det.Porcentaje;
            edited_info.Valor = info_det.Valor;
            edited_info.Secuencia = info_det.Secuencia;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

}