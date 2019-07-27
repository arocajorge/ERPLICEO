using Core.Erp.Bus.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Banco;
using Core.Erp.Bus.General;
using System.IO;
using Core.Erp.Info.General;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ArchivosTransferenciasController : Controller
    {
        string rutafile = System.IO.Path.GetTempPath() ;

        #region Variables
        ro_archivos_bancos_generacion_Bus bus_archivo = new ro_archivos_bancos_generacion_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        ro_archivos_bancos_generacion_x_empleado_Bus bus_archivo_detalle = new ro_archivos_bancos_generacion_x_empleado_Bus();
        ro_archivos_bancos_generacion_x_empleado_list_Info ro_archivos_bancos_generacion_x_empleado_list_Info = new ro_archivos_bancos_generacion_x_empleado_list_Info();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ba_Banco_Cuenta_Bus bus_cuentas_bancarias = new ba_Banco_Cuenta_Bus();
        tb_banco_procesos_bancarios_x_empresa_Bus bus_procesos_bancarios = new tb_banco_procesos_bancarios_x_empresa_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Vistas
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                fecha_ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                fecha_fin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1)
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

        [ValidateInput(false)]
        public ActionResult GridViewPartial_archivo_transferencia(DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            ViewBag.fecha_ini = fecha_ini == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : Convert.ToDateTime(fecha_ini);
            ViewBag.fecha_fin = fecha_fin == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) : Convert.ToDateTime(fecha_fin);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_archivo.get_list(IdEmpresa, ViewBag.fecha_ini, ViewBag.fecha_fin, IdSucursal, true);
            return PartialView("_GridViewPartial_archivo_transferencia", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_archivo_transferencia_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ro_archivos_bancos_generacion_Info model = new ro_archivos_bancos_generacion_Info();
                model.detalle = ro_archivos_bancos_generacion_x_empleado_list_Info.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_archivo_transferencia_det", model);


        }
        #endregion

        #region acciones
        public ActionResult Nuevo(int IdEmpresa=0, int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0, int IdPeriodo=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_archivos_bancos_generacion_Info model = new ro_archivos_bancos_generacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdNomina= IdNomina_Tipo,
                IdNominaTipo= IdNomina_TipoLiqui,
                IdTransaccionSession=Convert.ToDecimal( SessionFixed.IdTransaccionSession),
            };
           
            cargar_combos(0,Convert.ToInt32(SessionFixed.IdSucursal));
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ro_archivos_bancos_generacion_Info model)
        {
            model.detalle = ro_archivos_bancos_generacion_x_empleado_list_Info.get_list(model.IdTransaccionSession);
            if (model.detalle == null || model.detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para el archivo";
                cargar_combos(model.IdNomina, Convert.ToInt32(SessionFixed.IdSucursal));
                return View(model);
            }

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_archivo.guardarDB(model))
            {
                cargar_combos(model.IdNomina, Convert.ToInt32(SessionFixed.IdSucursal));
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdArchivo = model.IdArchivo, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa=0, decimal IdArchivo=0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_archivos_bancos_generacion_Info model = bus_archivo.get_info(IdEmpresa, IdArchivo);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            model.detalle = bus_archivo_detalle.get_list(IdEmpresa, IdArchivo);
            ro_archivos_bancos_generacion_x_empleado_list_Info.set_list(model.detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdNomina, Convert.ToInt32(SessionFixed.IdSucursal));

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ro_archivos_bancos_generacion_Info model)
        {
            model.detalle = ro_archivos_bancos_generacion_x_empleado_list_Info.get_list(model.IdTransaccionSession);
            if (model.detalle == null || model.detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para el arhivo";
                cargar_combos(model.IdNomina, Convert.ToInt32(SessionFixed.IdSucursal));
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_archivo.modificarDB(model))
            {
                cargar_combos(model.IdNomina, Convert.ToInt32(SessionFixed.IdSucursal));
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdArchivo = model.IdArchivo, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa=0, decimal IdArchivo=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_archivos_bancos_generacion_Info model = bus_archivo.get_info(IdEmpresa, IdArchivo);
            if (model == null)
                return RedirectToAction("Index");
            model.detalle = bus_archivo_detalle.get_list(IdEmpresa, IdArchivo);
            ro_archivos_bancos_generacion_x_empleado_list_Info.set_list(model.detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdNomina, Convert.ToInt32(SessionFixed.IdSucursal));
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_archivos_bancos_generacion_Info model)
        {
            model.detalle = ro_archivos_bancos_generacion_x_empleado_list_Info.get_list(model.IdTransaccionSession);

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            model.Fecha_UltAnu = DateTime.Now;
            if (!bus_archivo.anularDB(model))
            {
                cargar_combos(model.IdNomina, Convert.ToInt32(SessionFixed.IdSucursal));
                return View(model);
            }
            return RedirectToAction("Index");
        }


        public FileResult get_archivo(int IdEmpresa = 0, int IdArchivo = 0)
        {
            byte[] archivo;
            string NombreFile = "NCR";
            tb_banco_procesos_bancarios_x_empresa_Bus bus_tipo_file = new tb_banco_procesos_bancarios_x_empresa_Bus();

            var info_archivo = bus_archivo.get_info(IdEmpresa, IdArchivo);
            info_archivo.detalle = bus_archivo_detalle.get_list(IdEmpresa, IdArchivo);
            var tipo_file = bus_tipo_file.get_info(IdEmpresa, info_archivo.IdProceso);
            int secuancia = bus_archivo.get_secuencia_file(IdEmpresa, info_archivo.IdProceso, DateTime.Now.Date);
            info_archivo.TipoFile  = (cl_enumeradores.eTipoProcesoBancario)Enum.Parse(typeof(cl_enumeradores.eTipoProcesoBancario), tipo_file.IdProceso_bancario_tipo, true);

            switch (info_archivo.TipoFile)
            {
                case cl_enumeradores.eTipoProcesoBancario.NCR:
                    NombreFile = "NCR" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0')+tipo_file.Codigo_Empresa+"_"+secuancia.ToString().PadLeft(2,'0');
                    break;
                case cl_enumeradores.eTipoProcesoBancario.ROL_ELECTRONICO:
                    NombreFile = "ROL" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + tipo_file.Codigo_Empresa + "_" + secuancia.ToString().PadLeft(2, '0');
                    break;
                case cl_enumeradores.eTipoProcesoBancario.NCR_TRASN:
                    NombreFile = "NCR" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + tipo_file.Codigo_Empresa + "_" + secuancia.ToString().PadLeft(2, '0');
                    break;
                default:
                    break;
            }
            archivo = GetArchivo(info_archivo, NombreFile);
            return File(archivo, "application/xml", NombreFile + ".txt");


        }


        public JsonResult CargarEmpleados( int IdProceso  = 0, int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0, int IdPeriodo=0, 
            int IdSucursal=0,
            decimal  IdTransaccionSession=0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            string TipoCuenta = "";
            
            var infoProceso = bus_procesos_bancarios.get_info(IdEmpresa, IdProceso);
            if (infoProceso == null)
            {
                infoProceso = new Info.General.tb_banco_procesos_bancarios_x_empresa_Info();
                ro_archivos_bancos_generacion_x_empleado_list_Info.set_list(new List<ro_archivos_bancos_generacion_x_empleado_Info>(), Convert.ToDecimal(IdTransaccionSession));

            }
            else
            {
                if (infoProceso.IdProceso_bancario_tipo == cl_enumeradores.eTipoProcesoBancario.ROL_ELECTRONICO.ToString())
                    TipoCuenta = cl_enumeradores.eTipoCuentaRRHH.VRT.ToString();
                else
                    TipoCuenta = cl_enumeradores.eTipoCuentaRRHH.AHO.ToString() + "," + cl_enumeradores.eTipoCuentaRRHH.COR.ToString();

                var detalle = bus_archivo_detalle.get_list(IdEmpresa, IdNomina_Tipo, IdNomina_TipoLiqui, IdPeriodo, TipoCuenta, IdSucursal);
                ro_archivos_bancos_generacion_x_empleado_list_Info.set_list(detalle, Convert.ToDecimal(IdTransaccionSession));
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region cargar combos

        private void cargar_combos(int IdNomina, int IdSucursal)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.lst_nomina = bus_nomina.get_list(IdEmpresa, false);
            ViewBag.lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, IdNomina);

            var lst_cuenta_bancarias = bus_cuentas_bancarias.get_list(IdEmpresa, IdSucursal, false);
            ViewBag.lst_cuenta_bancarias = lst_cuenta_bancarias;

            var lst_proceso = bus_procesos_bancarios.get_list(IdEmpresa, false);
            ViewBag.lst_proceso = lst_proceso;

            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> lst_periodos = new List<ro_periodo_x_ro_Nomina_TipoLiqui_Info>();
            ViewBag.lst_periodos = lst_periodos;
        }

        private void cargar_combos_consulta()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }
        #endregion

        #region funciones del detalle

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_archivos_bancos_generacion_x_empleado_Info info_det)
        {
            if (ModelState.IsValid)
                ro_archivos_bancos_generacion_x_empleado_list_Info.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_archivos_bancos_generacion_Info model = new ro_archivos_bancos_generacion_Info();
            model.detalle = ro_archivos_bancos_generacion_x_empleado_list_Info.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_archivo_transferencia_det", model);
        }

        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_archivos_bancos_generacion_x_empleado_Info info_det)
        {
            ro_archivos_bancos_generacion_x_empleado_list_Info.DeleteRow(info_det.IdEmpleado, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_archivos_bancos_generacion_Info model = new ro_archivos_bancos_generacion_Info();
            model.detalle = ro_archivos_bancos_generacion_x_empleado_list_Info.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_archivo_transferencia_det", model);
        }
        #endregion

        #region Generar archivos

        public byte[] GetArchivo(ro_archivos_bancos_generacion_Info info, string nombre_file)
        {
            try
            {
                switch (info.TipoFile)
                {
                    case cl_enumeradores.eTipoProcesoBancario.NCR:

                      return   get_NCR(info, nombre_file);

                    case cl_enumeradores.eTipoProcesoBancario.ROL_ELECTRONICO:

                        return get_ROL_ELECTRONICO(info, nombre_file);
                    case cl_enumeradores.eTipoProcesoBancario.NCR_TRASN:

                        return get_NCR_OTRO(info, nombre_file);

                    default:
                        return new byte[00000];
                }

                
            }
            catch (Exception)
            {

                throw;
            }
        }
        #region Archivos para el banco de guayaquil

        private byte[] get_NCR(ro_archivos_bancos_generacion_Info info, string NombreArchivo)
        {
            try
            {
                System.IO.File.Delete(rutafile + NombreArchivo + ".txt");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(rutafile + NombreArchivo + ".txt", true))
                {
                    foreach (var item in info.detalle.Where(v=>v.Valor>0))
                    {
                        
                        item.em_NumCta = item.em_NumCta.Trim();
                        string linea = "";
                        double valor = Convert.ToDouble(item.Valor);
                        double valorEntero = Math.Floor(valor);
                        double valorDecimal = Convert.ToDouble((valor - valorEntero).ToString("N2")) * 100;
                       

                        if (item.em_tipoCta == "COR" || item.em_tipoCta == "AHO")
                        {
                            if (item.em_tipoCta == "AHO")
                                linea += "A";
                            else
                                linea += "C";
                            linea += item.em_NumCta.PadLeft(10, '0');
                            linea += (valorEntero.ToString() + valorDecimal.ToString().PadLeft(2,'0')).PadLeft(15, '0');
                            
                            linea += "EI";
                            linea += "Y";
                            linea += "01";
                            linea += cl_funciones.QuitarTildes(item.pe_apellido + item.pe_nombre);
                        }
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


        private byte[] get_NCR_OTRO(ro_archivos_bancos_generacion_Info info, string NombreArchivo)
        {
            try
            {
                tb_banco_procesos_bancarios_x_empresa_Info info_proceso = new tb_banco_procesos_bancarios_x_empresa_Info();
                info_proceso = bus_procesos_bancarios.get_info(info.IdEmpresa, info.IdProceso);

                try
                {
                    System.IO.File.Delete(rutafile + NombreArchivo + ".txt");
                }
                catch (Exception)
                {

                    
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(rutafile + NombreArchivo + ".txt", true))
                {
                    foreach (var item in info.detalle.Where(v => v.Valor > 0))
                    {
                        if (item.pe_cedulaRuc == "0912646684")
                        {

                        }

                        item.em_NumCta = item.em_NumCta.Trim();
                        string linea = "";
                        double valor = Convert.ToDouble(item.Valor);
                        double valorEntero = Math.Floor(valor);
                        double valorDecimal = Convert.ToDouble((valor - valorEntero).ToString("N2")) * 100;

                        if (item.em_tipoCta == "COR" || item.em_tipoCta == "AHO")
                        {
                            if (item.em_tipoCta == "AHO")
                                linea += "A";
                            else
                                linea += "C";

                            linea += item.em_NumCta.PadLeft(10, '0');
                            linea += (valorEntero.ToString() + valorDecimal.ToString().PadLeft(2, '0')).PadLeft(15, '0');

                            linea += "XX";
                            linea += "Y";
                            linea += "01";
                            linea += "                    ";
                            if (item.pe_nombreCompleto.Length>18)
                            linea += cl_funciones.QuitarTildes(item.pe_nombreCompleto).Substring(0,17)+" ";
                            else
                            linea += cl_funciones.QuitarTildes(item.pe_nombreCompleto).PadRight(18, ' ');
                            linea += info_proceso.Codigo_Empresa;
                        }

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


        private byte[] get_ROL_ELECTRONICO(ro_archivos_bancos_generacion_Info info, string NombreArchivo)
        {
            try
            {
                System.IO.File.Delete(rutafile + NombreArchivo + ".txt");
                MemoryStream memoryStream = new MemoryStream();
                TextWriter tw = new StreamWriter(memoryStream);

                var Info_proceso = bus_procesos_bancarios.get_info(info.IdEmpresa, info.IdProceso);
                var info_empresa = bus_empresa.get_info(info.IdEmpresa);
                info_empresa.RazonSocial = info_empresa.RazonSocial.Replace("S.A", "");
                var info_cuenta = bus_cuentas_bancarias.get_info(info.IdEmpresa, Convert.ToInt32(info.IdCuentaBancaria));
                double valor = 0;
                double valorEntero = 0;
                double valorDecimal = 0;
                int secuencia = 0;

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(rutafile + NombreArchivo + ".txt", true))
                {

                    foreach (var item in info.detalle.Where(v => v.Valor > 0))
                    {
                        string linea = "";
                        if (item.em_tipoCta == "VRT")
                        {
                            if (secuencia == 0)
                            {
                                valor = Convert.ToDouble(info.detalle.Sum(v => v.Valor));
                                valorEntero = Math.Floor(valor);
                                valorDecimal = Convert.ToDouble((valor - valorEntero).ToString("N2")) * 100;

                                linea += "C";
                                linea += Info_proceso.Codigo_Empresa;
                                linea += info_cuenta.ba_Num_Cuenta.PadLeft(10, '0');
                                if (info_empresa.RazonSocial.Length > 38)
                                    linea += info_empresa.RazonSocial.Substring(0, 38);
                                else
                                    linea += info_empresa.RazonSocial.PadRight(38, ' ');
                                linea += "C";
                                linea += (valorEntero.ToString() + valorDecimal.ToString().PadLeft(2,'0')).PadLeft(15, '0');
                                linea += DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                                linea += info.detalle.Count().ToString().PadLeft(5, '0');
                                file.WriteLine(linea);

                                // prime empleado
                                linea = "";
                                valor = Convert.ToDouble(item.Valor);
                                valorEntero = Math.Floor(valor);
                                valorDecimal = Convert.ToDouble((valor - valorEntero).ToString("N2")) * 100;
                                linea += "D";
                                linea += Info_proceso.Codigo_Empresa;
                                linea += item.pe_cedulaRuc.PadLeft(10, '0');
                                linea += item.pe_nombreCompleto.Substring(0, 17);
                                linea += "C";
                                linea += "                    ";
                                linea += "N";
                                linea += (valorEntero.ToString() + valorDecimal.ToString().PadLeft(2, '0')).PadLeft(15, '0');
                                linea += "                                           ";
                                linea += "0900000000";
                                file.WriteLine(linea);

                            }
                            else
                            {
                                valor = Convert.ToDouble(item.Valor);
                                valorEntero = Math.Floor(valor);
                                valorDecimal = Convert.ToDouble((valor - valorEntero).ToString("N2")) * 100;
                                linea += "D";
                                linea += Info_proceso.Codigo_Empresa;
                                linea += item.pe_cedulaRuc.PadLeft(10, '0');
                                linea += item.pe_nombreCompleto.Substring(0, 17);
                                linea += "C";
                                linea += "                    ";
                                linea += "N";
                                linea += (valorEntero.ToString() + valorDecimal.ToString().PadLeft(2, '0')).PadLeft(15, '0');
                                linea += "                                           ";
                                linea += "0900000000";
                                file.WriteLine(linea);
                            }
                        }
                        secuencia++;

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
        #endregion

        #endregion

    }


    public class ro_archivos_bancos_generacion_x_empleado_list_Info
    {
        string variable = "ro_archivos_bancos_generacion_x_empleado_Info";
        public List<ro_archivos_bancos_generacion_x_empleado_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable+IdTransaccionSession.ToString()] == null)
            {
                List<ro_archivos_bancos_generacion_x_empleado_Info> list = new List<ro_archivos_bancos_generacion_x_empleado_Info>();

                HttpContext.Current.Session[variable+IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_archivos_bancos_generacion_x_empleado_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_archivos_bancos_generacion_x_empleado_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(ro_archivos_bancos_generacion_x_empleado_Info info_det, decimal IdTransaccionSession)
        {
            ro_archivos_bancos_generacion_x_empleado_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdEmpleado == info_det.IdEmpleado).First();
            edited_info.Valor = info_det.Valor;
           

        }


        public void DeleteRow(decimal IdEmpleado, decimal IdTransaccionSession)
        {
            List<ro_archivos_bancos_generacion_x_empleado_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.IdEmpleado == IdEmpleado).First());
        }
    }
}