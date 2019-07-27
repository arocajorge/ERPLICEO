using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Banco;
using Core.Erp.Info.Banco;
using Core.Erp.Bus.General;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.Contabilidad;
using DevExpress.Web.Mvc;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Web.Areas.Contabilidad.Controllers;
using Core.Erp.Info.Contabilidad;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    public class ArchivoBancarioController : Controller
    {

        string rutafile = System.IO.Path.GetTempPath();

        #region Variables
        ba_Archivo_Transferencia_Bus bus_archivo = new ba_Archivo_Transferencia_Bus();
        ba_Archivo_Transferencia_Det_Bus bus_archivo_det = new ba_Archivo_Transferencia_Det_Bus();
        ba_Archivo_Transferencia_Det_List List_det = new ba_Archivo_Transferencia_Det_List();
        ba_Archivo_Transferencia_Det_List_op Lst_det_op = new ba_Archivo_Transferencia_Det_List_op();

        ba_Archivo_Flujo_List List_flujo = new ba_Archivo_Flujo_List();
        ba_archivo_transferencia_x_ba_tipo_flujo_Bus bus_archivo_flujo = new ba_archivo_transferencia_x_ba_tipo_flujo_Bus();
        ba_TipoFlujo_PlantillaDet_Bus bus_TipoFlujo_PlantillaDet = new ba_TipoFlujo_PlantillaDet_Bus();

        tb_banco_procesos_bancarios_x_empresa_Bus bus_procesos_bancarios = new tb_banco_procesos_bancarios_x_empresa_Bus();
        ba_Banco_Cuenta_Bus bus_cuentas_bancarias = new ba_Banco_Cuenta_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        string mensaje = string.Empty;
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        ba_TipoFlujo_Plantilla_Bus bus_TipoFlujo_Plantilla = new ba_TipoFlujo_Plantilla_Bus();

        ba_Banco_Cuenta_Bus bus_banco_cuenta = new ba_Banco_Cuenta_Bus();
        ba_parametros_Bus bus_param = new ba_parametros_Bus();
        ba_TipoFlujo_Bus bus_flujo = new ba_TipoFlujo_Bus();
        ct_cbtecble_det_List List_Cbte = new ct_cbtecble_det_List();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
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

        [ValidateInput(false)]
        public ActionResult GridViewPartial_archivo_bancario(DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(fecha_ini);
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_fin);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_archivo.GetList(IdEmpresa,IdSucursal, ViewBag.fecha_ini, ViewBag.fecha_fin, true);
            return PartialView("_GridViewPartial_archivo_bancario", model);
        }
        #endregion
        #region Metodos
        private bool validar(ba_Archivo_Transferencia_Info i_validar, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.Fecha, cl_enumeradores.eModulo.BANCO, i_validar.IdSucursal, ref msg))
            {
                return false;
            }

            var pro = bus_procesos_bancarios.get_info(i_validar.IdEmpresa, i_validar.IdProceso_bancario);
            i_validar.Cod_Empresa = pro.Codigo_Empresa;

            if (i_validar.SecuencialInicial == 0)
            {
                i_validar.SecuencialInicial = bus_archivo_det.GetIdSecuencial(i_validar.IdEmpresa, i_validar.IdBanco, i_validar.IdProceso_bancario);
            }

            var lst_prov = i_validar.Lst_det.GroupBy(q => new { q.IdTipoPersona, q.IdEntidad, q.IdPersona }).ToList();
            foreach (var item in lst_prov)
            {
                i_validar.Lst_det.Where(q=> q.IdTipoPersona == item.Key.IdTipoPersona && q.IdEntidad == item.Key.IdEntidad && q.IdPersona == item.Key.IdPersona).ToList().ForEach(q => { q.Secuencial_reg_x_proceso = i_validar.SecuencialInicial; });
                i_validar.SecuencialInicial++;
            }

            i_validar.Lst_Flujo = List_flujo.get_list(i_validar.IdTransaccionSession);
            var cta = bus_banco_cuenta.get_info(i_validar.IdEmpresa, i_validar.IdBanco);
            if (cta.EsFlujoObligatorio)
            {
                if (i_validar.Lst_Flujo.Count == 0)
                {
                    mensaje = "Falta distribución de flujo";
                    return false;
                }
                double Diferencia = Math.Round(i_validar.Lst_Flujo.Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero) - Math.Round(i_validar.Lst_det.Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero);
                if (Diferencia != 0)
                {
                    mensaje = "Existe una diferencia entre la distribución del flujo y el total a pagar";
                    return false;
                }
            }

            var param = bus_param.get_info(i_validar.IdEmpresa);
            if (!(param.PermitirSobreGiro))
            {
                var Valor = Math.Round(i_validar.Lst_det.Where(q => q.IdBanco_acreditacion.ToString() == cta.IdCtaCble).Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero);
                if (!bus_banco_cuenta.ValidarSaldoCuenta(i_validar.IdEmpresa, cta.IdCtaCble, Valor))
                {
                    mensaje = "No se puede guardar la transacción por sobre giro en la cuenta";
                    return false;
                }
            }
            return true;
        }
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            
            var lst_cuenta_bancarias = bus_cuentas_bancarias.get_list(IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal), false);
            ViewBag.lst_cuenta_bancarias = lst_cuenta_bancarias;

            var lst_proceso = bus_procesos_bancarios.get_list(IdEmpresa, false);
            ViewBag.lst_proceso = lst_proceso;

            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;
            
        }

        #endregion
        #region CmbTipoFlujo
        public ActionResult CmbFlujo_Tipo()
        {
            int model = new int();
            return PartialView("_CmbFlujo_Tipo", model);
        }
        public List<ba_TipoFlujo_Info> get_list_bajo_demanda_tipoflujo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var TipoFlujo_GetList = bus_flujo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
            return TipoFlujo_GetList;
        }
        public ba_TipoFlujo_Info get_info_bajo_demanda_tipoflujo(ListEditItemRequestedByValueEventArgs args)
        {
            var TipoFlujo_GetInfo = bus_flujo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
            return TipoFlujo_GetInfo;
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
            ba_Archivo_Transferencia_Info model = new ba_Archivo_Transferencia_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                Fecha = DateTime.Now,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                Lst_det = new List<ba_Archivo_Transferencia_Det_Info>(), 
                Lst_Flujo = new List<ba_archivo_transferencia_x_ba_tipo_flujo_Info>(),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            List_det.set_list(model.Lst_det, model.IdTransaccionSession);
            List_flujo.set_list(model.Lst_Flujo, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ba_Archivo_Transferencia_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            model.Lst_det = List_det.get_list(model.IdTransaccionSession);
            model.Lst_Flujo = List_flujo.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos();
                return View(model);
            }
            if (!bus_archivo.GuardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdArchivo = model.IdArchivo, Exito = true });
        }
        public ActionResult Modificar(int IdEmpresa = 0, decimal IdArchivo = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ba_Archivo_Transferencia_Info model = bus_archivo.GetInfo(IdEmpresa, IdArchivo);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.Lst_det = bus_archivo_det.GetList(model.IdEmpresa, model.IdArchivo);
            List_det.set_list(model.Lst_det, model.IdTransaccionSession);

            model.Lst_Flujo = bus_archivo_flujo.GetList(model.IdEmpresa, model.IdArchivo);
            List_flujo.set_list(model.Lst_Flujo, model.IdTransaccionSession);
            cargar_combos();
            model.cb_Valor = model.Lst_det.Sum(q => q.Valor);
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ba_Archivo_Transferencia_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            model.Lst_det = List_det.get_list(model.IdTransaccionSession);
            model.Lst_Flujo = List_flujo.get_list(model.IdTransaccionSession);
            
            if (!validar(model,ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos();
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }
            if (!bus_archivo.ModificarDB(model))
            {
                cargar_combos();
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdArchivo = model.IdArchivo, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdArchivo = 0)
        {
            ba_Archivo_Transferencia_Info model = bus_archivo.GetInfo(IdEmpresa, IdArchivo);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.Lst_det = bus_archivo_det.GetList(model.IdEmpresa, model.IdArchivo);
            List_det.set_list(model.Lst_det, model.IdTransaccionSession);

            model.Lst_Flujo = bus_archivo_flujo.GetList(model.IdEmpresa, model.IdArchivo);
            List_flujo.set_list(model.Lst_Flujo, model.IdTransaccionSession);
            cargar_combos();
            model.cb_Valor = model.Lst_det.Sum(q => q.Valor);

            #region Validacion Periodo CXC
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.BANCO, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            cargar_combos();
            model.cb_Valor = model.Lst_det.Sum(q => q.Valor);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ba_Archivo_Transferencia_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            if (!bus_archivo.AnularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Contabilizar(int IdEmpresa = 0, decimal IdArchivo = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ba_Archivo_Transferencia_Info model = bus_archivo.GetInfo(IdEmpresa, IdArchivo);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.Lst_det = bus_archivo_det.GetList(model.IdEmpresa, model.IdArchivo);
            List_det.set_list(model.Lst_det, model.IdTransaccionSession);

            ArmarDiario(model.IdEmpresa, model.IdTransaccionSession, model.IdBanco);           

            model.Lst_Flujo = bus_archivo_flujo.GetList(model.IdEmpresa, model.IdArchivo);
            List_flujo.set_list(model.Lst_Flujo, model.IdTransaccionSession);
            cargar_combos();
            #region Validacion Periodo Banco
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(model.IdEmpresa, model.Fecha, cl_enumeradores.eModulo.BANCO, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            model.cb_Valor = model.Lst_det.Sum(q => q.Valor);

            return View(model);
        }
        [HttpPost]
        public ActionResult Contabilizar(ba_Archivo_Transferencia_Info model)
        {
            model.Lst_det = List_det.get_list(model.IdTransaccionSession);
            model.Lst_Flujo = List_flujo.get_list(model.IdTransaccionSession);
            model.Lst_diario = List_Cbte.get_list(model.IdTransaccionSession);
            model.IdUsuario = SessionFixed.IdUsuario;

            if (Math.Round(model.Lst_diario.Sum(q=> q.dc_Valor),2,MidpointRounding.AwayFromZero) != 0)
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                ViewBag.mensaje = "El diario se encuentra descuadrado";
                return View(model);
            }

            if (!bus_archivo.ContabilizarDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                ViewBag.mensaje = "No se ha podido contabilizar el archivo bancario";
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Detalle Archivo
        [ValidateInput(false)]
        public ActionResult GridViewPartial_archivo_bancario_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_archivo_bancario_det", model);
        }
        public ActionResult GridViewPartial_archivo_bancario_det_op()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = Lst_det_op.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_archivo_bancario_det_op", model);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditingAddNew(string IDs = "", decimal IdTransaccionSession = 0, int IdEmpresa = 0)
        {
            if (IDs != "")
            {
                var Lst_det = Lst_det_op.get_list(IdTransaccionSession);
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    var info_det = Lst_det.Where(q => q.IdOrdenPago == Convert.ToInt32(item)).FirstOrDefault();
                    if (info_det != null)
                    {
                        List_det.AddRow(info_det, IdTransaccionSession);
                    }
                }
            }
            var model = List_det.get_list(IdTransaccionSession);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ba_Archivo_Transferencia_Det_Info info_det)
        {

            if (ModelState.IsValid)
                List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_archivo_bancario_det", model);
        }
        public ActionResult EditingDelete(decimal IdOrdenPago)
        {
            List_det.DeleteRow(IdOrdenPago, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_archivo_bancario_det", model);
        }
        #endregion
        #region Json
        public JsonResult GetListPorCruzar(int IdEmpresa = 0, decimal IdTransaccionSession = 0, int IdSucursal = 0)
        {
            var lst = bus_archivo_det.get_list_con_saldo(IdEmpresa, 0, "PROVEE", 0, "APRO", SessionFixed.IdUsuario, IdSucursal, false);
            Lst_det_op.set_list(lst, IdTransaccionSession);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetValor(decimal IdTransaccionSession = 0)
        {
            double Valor = Math.Round(List_det.get_list(IdTransaccionSession).Sum(q => q.Valor),2,MidpointRounding.AwayFromZero);
            return Json(Valor,JsonRequestBehavior.AllowGet);
        }
        public JsonResult cargar_PlantillaTipoFlujoArchivo(float Valor = 0, decimal IdPlantillaTipoFlujo = 0, decimal IdTransaccionSession = 0, int IdEmpresa = 0)
        {
            var ListaPlantillaTipoFlujo = bus_TipoFlujo_PlantillaDet.GetList(IdEmpresa, IdPlantillaTipoFlujo);
            var ListaFlujoArchivo = List_flujo.get_list(IdTransaccionSession);
            var secuencia = 1;

            foreach (var item in ListaPlantillaTipoFlujo)
            {
                ListaFlujoArchivo.Add(new ba_archivo_transferencia_x_ba_tipo_flujo_Info
                {
                    Secuencia = secuencia++,
                    IdTipoFlujo = item.IdTipoFlujo,
                    Descricion = item.Descricion,
                    Porcentaje = item.Porcentaje,
                    Valor = (item.Porcentaje * Valor) / 100
                });
            }

            List_flujo.set_list(ListaFlujoArchivo, IdTransaccionSession);
            return Json(ListaFlujoArchivo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult actualizarGridDetFlujoArchivo(float Valor = 0, decimal IdTransaccionSession = 0, int IdEmpresa = 0)
        {

            var ListaPlantillaTipoFlujo = List_flujo.get_list(IdTransaccionSession);

            var ListaDetFlujo = new List<ba_archivo_transferencia_x_ba_tipo_flujo_Info>();
            foreach (var item in ListaPlantillaTipoFlujo)
            {
                ListaDetFlujo.Add(new ba_archivo_transferencia_x_ba_tipo_flujo_Info
                {
                    Secuencia = item.Secuencia,
                    IdTipoFlujo = item.IdTipoFlujo,
                    Descricion = item.Descricion,
                    Porcentaje = item.Porcentaje,
                    Valor = (item.Porcentaje * Valor) / 100
                });
            }

            List_flujo.set_list(ListaDetFlujo, IdTransaccionSession);
            return Json(ListaDetFlujo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ArmarDiario(int IdEmpresa = 0, decimal IdTransaccionSession = 0, int IdBanco = 0)
        {
            List<ct_cbtecble_det_Info> Lista = new List<ct_cbtecble_det_Info>();
            var ListaD = List_det.get_list(IdTransaccionSession);
            var banco = bus_banco_cuenta.get_info(IdEmpresa, IdBanco);
            int Secuencia = 1;
            if (banco != null)
            {
                var LG = ListaD.GroupBy(q => new { q.IdCtaCble, q.pc_Cuenta }).Select(q => new
                {
                    IdCtaCble = q.Key.IdCtaCble,
                    pc_Cuenta = q.Key.pc_Cuenta,
                    Valor = q.Sum(g => g.Valor)
                });

                #region Cuentas proveedores
                foreach (var item in LG)
                {
                    Lista.Add(new ct_cbtecble_det_Info
                    {
                        secuencia = Secuencia++,
                        IdCtaCble = item.IdCtaCble,
                        pc_Cuenta = item.pc_Cuenta,
                        dc_Valor = Math.Round(item.Valor, 2, MidpointRounding.AwayFromZero),
                        dc_Valor_debe = Math.Round(item.Valor, 2, MidpointRounding.AwayFromZero)
                    });
                }
                #endregion

                #region Cuenta banco
                var cuenta = bus_plancta.get_info(IdEmpresa, banco.IdCtaCble);
                Lista.Add(new ct_cbtecble_det_Info
                {
                    secuencia = Secuencia++,
                    IdCtaCble = banco == null ? string.Empty : banco.IdCtaCble,
                    pc_Cuenta = cuenta == null ? string.Empty : cuenta.pc_Cuenta,
                    dc_Valor = Math.Round(ListaD.Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero)*-1,
                    dc_Valor_haber = Math.Round(ListaD.Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero)
                });
                #endregion
            }
            List_Cbte.set_list(Lista, IdTransaccionSession);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Archivo

        public FileResult get_archivo(int IdEmpresa = 0, int IdArchivo = 0)
        {
            byte[] archivo;
            ba_Archivo_Transferencia_Bus bus_tipo_file = new ba_Archivo_Transferencia_Bus();

            var info_archivo = bus_archivo.GetInfo(IdEmpresa, IdArchivo);
            info_archivo.Lst_det = bus_archivo_det.GetList(IdEmpresa, IdArchivo);

            archivo = GetArchivo(info_archivo, info_archivo.Nom_Archivo);
            return File(archivo, "application/xml", info_archivo.Nom_Archivo + ".txt");
        }

        private byte[] GetMulticash(ba_Archivo_Transferencia_Info info, string NombreArchivo)
        {
            try
            {
                System.IO.File.Delete(rutafile + NombreArchivo + ".txt");                
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(rutafile + NombreArchivo + ".txt", true))
                {
                    /*
                    var ListaA = info.Lst_det.Where(v => v.Valor > 0).GroupBy(q => new { q.num_cta_acreditacion, q.Secuencial_reg_x_proceso, q.pe_cedulaRuc, q.CodigoLegalBanco, q.IdTipoCta_acreditacion_cat, q.IdTipoDocumento, q.Nom_Beneficiario }).Select(q => new
                    {
                        num_cta_acreditacion = q.Key.num_cta_acreditacion,
                        Secuencial_reg_x_proceso = q.Key.Secuencial_reg_x_proceso,
                        pe_cedulaRuc = q.Key.pe_cedulaRuc,
                        CodigoLegalBanco = q.Key.CodigoLegalBanco,
                        IdTipoCta_acreditacion_cat = q.Key.IdTipoCta_acreditacion_cat,
                        IdTipoDocumento = q.Key.IdTipoDocumento,
                        Nom_Beneficiario = q.Key.Nom_Beneficiario,
                        Valor = q.Sum(g=> g.Valor)
                    }).ToList();
                    */
                    var banco = bus_banco_cuenta.get_info(info.IdEmpresa, info.IdBanco);
                    foreach (var item in info.Lst_det.Where(v => v.Valor > 0).ToList())
                    {
                        string linea = "";
                        double valor = Convert.ToDouble(item.Valor);
                        double valorEntero = Math.Floor(valor);
                        double valorDecimal = Convert.ToDouble((valor - valorEntero).ToString("N2")) * 100;

                        linea += "PA\t";
                        linea += string.IsNullOrEmpty(banco.ba_Num_Cuenta) ? "" : banco.ba_Num_Cuenta.PadLeft(10, '0') + "\t";
                        linea += item.Secuencial_reg_x_proceso.ToString().PadLeft(7, ' ') + "\t";
                        linea += "\t";//COMPROBANTE DE PAGO
                        linea += (string.IsNullOrEmpty(item.num_cta_acreditacion) ? item.pe_cedulaRuc.Trim() : item.num_cta_acreditacion.Trim()) + "\t";
                        linea += "USD\t";
                        linea += (valorEntero.ToString() + valorDecimal.ToString().PadLeft(2, '0')).PadLeft(13, '0') + "\t";
                        linea += (string.IsNullOrEmpty(item.num_cta_acreditacion) ? "EFE" : "CTA") + "\t";
                        linea += (string.IsNullOrEmpty(item.num_cta_acreditacion) ? "0017" : item.CodigoLegalBanco.ToString().PadLeft(4, '0')) + "\t";
                        linea += (string.IsNullOrEmpty(item.num_cta_acreditacion) || string.IsNullOrEmpty(item.IdTipoCta_acreditacion_cat) ? "" : (item.IdTipoCta_acreditacion_cat.Trim() == "COR" ? "CTE" : item.IdTipoCta_acreditacion_cat)) + "\t";
                        linea += string.IsNullOrEmpty(item.num_cta_acreditacion) ? "" : item.num_cta_acreditacion.PadLeft(10, '0') + "\t";
                        linea += (item.IdTipoDocumento == "CED" ? "C" : (item.IdTipoDocumento == "RUC" ? "R" : "P")) + "\t";
                        linea += item.pe_cedulaRuc.Trim() + "\t";
                        linea += (string.IsNullOrEmpty(item.Nom_Beneficiario) ? "" : (item.Nom_Beneficiario.Length > 40 ? item.Nom_Beneficiario.Substring(0, 40) : item.Nom_Beneficiario.Trim())) + "\t";
                        linea += "\t";//(string.IsNullOrEmpty(item.pr_direccion) ? "" : (item.pr_direccion.Length > 40 ? item.pr_direccion.Substring(0, 40) : item.pr_direccion.Trim())) + "\t";
                        linea += "\t";//Ciudad
                        linea += "\t";//Telefono
                        linea += "\t";//Localidad
                        var Referencia = string.Empty;
                        /*
                        foreach (var refe in info.Lst_det.Where(q => q.pe_cedulaRuc == item.pe_cedulaRuc).ToList())
                        {
                            if(!string.IsNullOrEmpty(refe.Referencia))
                                Referencia += ((string.IsNullOrEmpty(refe.Referencia) ? "" : "/") + refe.Referencia);
                        }
                        linea += (string.IsNullOrEmpty(Referencia) ? "" : (Referencia.Length > 200 ? Referencia.Substring(0, 200) : Referencia.Trim())) + "\t";
                        */
                        linea += (string.IsNullOrEmpty(item.Referencia) ? "" : (item.Referencia.Length > 200 ? item.Referencia.Substring(0, 200) : item.Referencia.Trim())) + "\t";
                        linea += "\t";//Ref adicional

                        file.WriteLine(linea);
                    }
                }
                byte[] filebyte = System.IO.File.ReadAllBytes(rutafile + NombreArchivo + ".txt");
                return filebyte;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public byte[] GetArchivo(ba_Archivo_Transferencia_Info info, string nombre_file)
        {
            try
            {
                return GetMulticash(info, nombre_file);

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region DetalleFlujo
        #region Detalle
        private void cargar_combos_Detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_flujo = bus_flujo.get_list(IdEmpresa, false);
            ViewBag.lst_flujo = lst_flujo;


        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_flujo_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            
            cargar_combos_Detalle();
            var model = List_flujo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_flujo_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNewFlujo([ModelBinder(typeof(DevExpressEditorsBinder))] ba_archivo_transferencia_x_ba_tipo_flujo_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (info_det != null)
                if (info_det.IdTipoFlujo != 0)
                {
                    ba_TipoFlujo_Info info_TipoFlujo = bus_flujo.get_info(IdEmpresa, info_det.IdTipoFlujo);
                    if (info_TipoFlujo != null)
                    {
                        info_det.Descricion = info_TipoFlujo.Descricion;
                    }
                }

            if (ModelState.IsValid)
                List_flujo.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_flujo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_flujo_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdateFlujo([ModelBinder(typeof(DevExpressEditorsBinder))] ba_archivo_transferencia_x_ba_tipo_flujo_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (info_det != null)
                if (info_det.IdTipoFlujo != 0)
                {
                    ba_TipoFlujo_Info info_TipoFlujo = bus_flujo.get_info(IdEmpresa, info_det.IdTipoFlujo);
                    if (info_TipoFlujo != null)
                    {
                        info_det.IdTipoFlujo = info_TipoFlujo.IdTipoFlujo;
                        info_det.Descricion = info_TipoFlujo.Descricion;
                    }
                }

            if (ModelState.IsValid)
                List_flujo.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_flujo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_flujo_det", model);
        }
        public ActionResult EditingDeleteFlujo(int Secuencia)
        {
            List_flujo.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_flujo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_flujo_det", model);
        }
        #endregion
        #endregion
        #region Plantilla por asignar
        public ActionResult GridViewPartial_TipoFlujoPlantilla_Asignar()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ba_TipoFlujo_Plantilla_Info> model = bus_TipoFlujo_Plantilla.GetList(IdEmpresa, true);

            return PartialView("_GridViewPartial_flujo_Asignar", model);
        }
        #endregion
    }
    public class ba_Archivo_Transferencia_Det_List
    {
        string Variable = "ba_Archivo_Transferencia_Det_Info";
        public List<ba_Archivo_Transferencia_Det_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_Archivo_Transferencia_Det_Info> list = new List<ba_Archivo_Transferencia_Det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_Archivo_Transferencia_Det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_Archivo_Transferencia_Det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ba_Archivo_Transferencia_Det_Info info_det, decimal IdTransaccionSession)
        {
            List<ba_Archivo_Transferencia_Det_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            if (list.Where(q => q.IdOrdenPago == info_det.IdOrdenPago).Count() == 0)
                list.Add(info_det);
        }

        public void UpdateRow(ba_Archivo_Transferencia_Det_Info info_det, decimal IdTransaccionSession)
        {
            ba_Archivo_Transferencia_Det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdOrdenPago == info_det.IdOrdenPago).First();
            if (edited_info != null)
            {
                edited_info.Valor = info_det.Valor;
            }
        }

        public void DeleteRow(decimal IdOrdenPago, decimal IdTransaccionSession)
        {
            List<ba_Archivo_Transferencia_Det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.IdOrdenPago == IdOrdenPago).First());
        }
    }

    public class ba_Archivo_Transferencia_Det_List_op
    {
        string Variable = "ba_Archivo_Transferencia_Det_Info_op";
        public List<ba_Archivo_Transferencia_Det_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_Archivo_Transferencia_Det_Info> list = new List<ba_Archivo_Transferencia_Det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_Archivo_Transferencia_Det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_Archivo_Transferencia_Det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class ba_Archivo_Flujo_List
    {
        string Variable = "ba_archivo_transferencia_x_ba_tipo_flujo_Info";
        public List<ba_archivo_transferencia_x_ba_tipo_flujo_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_archivo_transferencia_x_ba_tipo_flujo_Info> list = new List<ba_archivo_transferencia_x_ba_tipo_flujo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_archivo_transferencia_x_ba_tipo_flujo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_archivo_transferencia_x_ba_tipo_flujo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ba_archivo_transferencia_x_ba_tipo_flujo_Info info_det, decimal IdTransaccionSession)
        {
            List<ba_archivo_transferencia_x_ba_tipo_flujo_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            list.Add(info_det);
        }

        public void UpdateRow(ba_archivo_transferencia_x_ba_tipo_flujo_Info info_det, decimal IdTransaccionSession)
        {
            ba_archivo_transferencia_x_ba_tipo_flujo_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdTipoFlujo = info_det.IdTipoFlujo;
            edited_info.IdArchivo = info_det.IdArchivo;
            edited_info.Porcentaje = info_det.Porcentaje;
            edited_info.Valor = info_det.Valor;
            edited_info.Secuencia = info_det.Secuencia;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ba_archivo_transferencia_x_ba_tipo_flujo_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

}