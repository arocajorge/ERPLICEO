using Core.Erp.Bus.Banco;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Info.Banco;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

using System.Linq;
using Core.Erp.Web.Reportes.Banco;
using Core.Erp.Web.Areas.Contabilidad.Controllers;
using Core.Erp.Info.Contabilidad;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    [SessionTimeout]
    public class BancoCuentaController : Controller
    {
        #region Variables
        ba_Banco_Cuenta_Bus bus_cuenta = new ba_Banco_Cuenta_Bus();
        tb_banco_Bus bus_banco = new tb_banco_Bus();
        ct_plancta_Bus bus_cuentacontable = new ct_plancta_Bus();
        ba_Banco_Cuenta_List ListaBanco = new ba_Banco_Cuenta_List();
        ba_Banco_Cbte_List ListaCbte = new ba_Banco_Cbte_List();

        ba_Banco_Cuenta_x_tb_sucursal_Bus bus_cta_det = new ba_Banco_Cuenta_x_tb_sucursal_Bus();
        ba_Banco_Det_List List_Det = new ba_Banco_Det_List();
        

        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_cuentas()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_cuenta.get_list(IdEmpresa, 0, true);
            return PartialView("_GridViewPartial_cuentas", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {

            Dictionary<string, string> lst_cta = new Dictionary<string, string>();
            lst_cta.Add("cuenta de ahorro", "Cuenta de ahorro");
            lst_cta.Add("cuenta corriente", "Cuenta corriente");
            ViewBag.lst_cta = lst_cta;

            Dictionary<bool, string> lst_impresion = new Dictionary<bool, string>();
            lst_impresion.Add(true, "solo cheque");
            lst_impresion.Add(false, "Cheque y comprobante");
            ViewBag.lst_impresion = lst_impresion;

            var lst_banco = bus_banco.get_list(false);
            ViewBag.lst_banco = lst_banco;

            var lst_cuenta = bus_cuentacontable.get_list(IdEmpresa, false, false);
            ViewBag.lst_cuenta = lst_cuenta;
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
            ba_Banco_Cuenta_Info model = new ba_Banco_Cuenta_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                lstDet = new List<ba_Banco_Cuenta_x_tb_sucursal_Info>()

            };
            List_Det.set_list(model.lstDet, model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ba_Banco_Cuenta_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            model.lstDet = List_Det.get_list(model.IdTransaccionSession);
            if (!bus_cuenta.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdBanco = 0)

        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ba_Banco_Cuenta_Info model = bus_cuenta.get_info(IdEmpresa,IdBanco);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lstDet = bus_cta_det.GetList(IdEmpresa, IdBanco);
            List_Det.set_list(model.lstDet, model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ba_Banco_Cuenta_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            model.lstDet = List_Det.get_list(model.IdTransaccionSession);
            if (!bus_cuenta.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdBanco = 0)
        {
            ba_Banco_Cuenta_Info model = bus_cuenta.get_info(IdEmpresa, IdBanco);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lstDet = bus_cta_det.GetList(IdEmpresa, IdBanco);
            List_Det.set_list(model.lstDet, model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ba_Banco_Cuenta_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            if (!bus_cuenta.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Importacion
        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings.UploadValidationSettings, UploadControlSettings.FileUploadComplete);
            return null;
        }
        public ActionResult Importar(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ba_Banco_Cuenta_Info model = new ba_Banco_Cuenta_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(ba_Banco_Cuenta_Info model)
        {
            try
            {
                var Lista_Banco = ListaBanco.get_list(model.IdTransaccionSession);
                
                if (!bus_cuenta.GuardarDbImportacion(Lista_Banco))
                {
                    ViewBag.mensaje = "Error al importar el archivo";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());

                ViewBag.error = ex.Message.ToString();
                return View(model);
            }

            return RedirectToAction("Index");
        }
        public ActionResult GridViewPartial_Banco_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaBanco.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Banco_importacion", model);
        }

        public ActionResult GridViewPartial_Documento_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaCbte.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Documento_importacion", model);
        }

        public JsonResult ActualizarVariablesSession(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            string retorno = string.Empty;
            SessionFixed.IdEmpresa = IdEmpresa.ToString();
            SessionFixed.IdTransaccionSession = IdTransaccionSession.ToString();
            SessionFixed.IdTransaccionSessionActual = IdTransaccionSession.ToString();
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Diseñador
        public ActionResult Disenar(int IdEmpresa = 0, int IdBanco = 0)
        {
            var model = bus_cuenta.get_info(IdEmpresa, IdBanco);
            if (model == null)
                return RedirectToAction("Index");

            if (!model.Imprimir_Solo_el_cheque)
                model.ReporteCheque = model.ReporteChequeComprobante;
            
            if (model.ReporteCheque == null)
            {
                MemoryStream ms = new MemoryStream();
                if (!model.Imprimir_Solo_el_cheque)
                {
                    BAN_006_Rpt rpt = new BAN_006_Rpt();
                    rpt.SaveLayoutToXml(ms);
                    ms.Position = 0;
                    model.ReporteCheque = ms.ToArray();
                }else
                {
                    BAN_005_Rpt rpt = new BAN_005_Rpt();
                    rpt.SaveLayoutToXml(ms);
                    ms.Position = 0;
                    model.ReporteCheque = ms.ToArray();
                }                    
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Disenar(ba_Banco_Cuenta_Info model)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdBanco = Request.Params["fx_IdBanco"] != null ? Convert.ToInt32(Request.Params["fx_IdBanco"]) : 0;
            model.ReporteCheque = ReportDesignerExtension.GetReportXml("ReportDesigner");
            bus_cuenta.GuardarDisenioDB(IdEmpresa, IdBanco, model.ReporteCheque);
            return View(model);
        }
        #endregion

        #region Json
        public JsonResult GetListBancoPorSucursal(int IdEmpresa = 0, int IdSucursal = 0)
        {
            var lst = bus_cuenta.get_list(IdEmpresa, IdSucursal, false);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

       
        #endregion

        #region Detalle
        private void cargar_combos_Detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;


            var lst_banco = bus_banco.get_list(false);
            ViewBag.lst_banco = lst_banco;
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_cuentas_x_sucursal()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            cargar_combos_Detalle();
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_cuentas_x_sucursal", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ba_Banco_Cuenta_x_tb_sucursal_Info info_det)
        {
            if (ModelState.IsValid)
                List_Det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_cuentas_x_sucursal", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ba_Banco_Cuenta_x_tb_sucursal_Info info_det)
        {

            if (ModelState.IsValid)
                List_Det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_cuentas_x_sucursal", model);
        }
        public ActionResult EditingDelete(int Secuencia)
        {
            List_Det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_cuentas_x_sucursal", model);
        }
        #endregion

    }


    public class UploadControlSettings
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables
         
            ba_Banco_Cuenta_List ListaBanco = new ba_Banco_Cuenta_List();
            List<ba_Banco_Cuenta_Info> Lista_Banco = new List<ba_Banco_Cuenta_Info>();
            ba_Banco_Cbte_List ListaCbte = new ba_Banco_Cbte_List();
            List<ba_Cbte_Ban_Info> Lista_Cbte = new List<ba_Cbte_Ban_Info>();
            tb_banco_Bus bus_banco = new tb_banco_Bus();
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();

            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            #endregion


            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                #region Banco                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        ba_Banco_Cuenta_Info info = new ba_Banco_Cuenta_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdBanco = Convert.ToInt32(reader.GetValue(0)),
                            IdBanco_Financiero = Convert.ToInt32(reader.GetValue(1)),
                            ba_Tipo = Convert.ToString(reader.GetValue(2)),
                            ba_Num_Cuenta = Convert.ToString(reader.GetValue(3)),
                            ba_num_digito_cheq = Convert.ToInt32(reader.GetValue(4)),
                            IdCtaCble = null,
                            IdUsuario = SessionFixed.IdUsuario,
                            

                        };
                        #region GetInfo
                        tb_banco_Info banco = bus_banco.get_info(info.IdBanco);
                        info.ba_descripcion = banco.ba_descripcion + " " + info.ba_Tipo + " " + info.ba_Num_Cuenta;
                        info.Imprimir_Solo_el_cheque = false;
                        Lista_Banco.Add(info);
                        #endregion

                    }
                    else
                        cont++;
                }
                ListaBanco.set_list(Lista_Banco, IdTransaccionSession);
                #endregion

                cont = 0;
                //Para avanzar a la siguiente hoja de excel
                reader.NextResult();

                #region Cbte                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        ba_Cbte_Ban_Info info = new ba_Cbte_Ban_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdTipo_Persona = Convert.ToString(reader.GetValue(0)),
                            Su_Descripcion = Convert.ToString(reader.GetValue(1)),
                            IdBanco = Convert.ToInt32(reader.GetValue(2)),
                            cb_Fecha = Convert.ToDateTime(reader.GetValue(3)),
                            cb_Observacion = Convert.ToString(reader.GetValue(4)),
                            cb_Valor = Convert.ToInt32(reader.GetValue(5)),
                            

                            IdUsuario = SessionFixed.IdUsuario,
                        };
                        #region GetInfo
                        tb_sucursal_Info sucursal = bus_sucursal.GetInfo(IdEmpresa, info.Su_Descripcion);
                        info.Su_Descripcion = sucursal.Su_Descripcion;
                        info.IdSucursal = sucursal.IdSucursal;

                        Lista_Cbte.Add(info);
                        #endregion
                    }
                    else
                        cont++;
                }
                var ListCuenta = ListaBanco.get_list(IdTransaccionSession);

                var lst = (from q in ListCuenta
                           join c in Lista_Cbte
                           on q.IdBanco equals c.IdBanco
                           select new ba_Cbte_Ban_Info
                           {
                               IdEmpresa = c.IdEmpresa,
                               IdTipo_Persona = c.IdTipo_Persona,
                               IdSucursal = c.IdSucursal,
                               ba_descripcion = q.ba_descripcion,
                               Su_Descripcion = c.Su_Descripcion,
                               IdBanco = c.IdBanco,
                               cb_Valor = c.cb_Valor,
                               cb_Fecha = c.cb_Fecha,
                               cb_Observacion = c.cb_Observacion

                           }).ToList();

                Lista_Cbte = lst;
                ListaCbte.set_list(Lista_Cbte, IdTransaccionSession);
                #endregion

            }
        }
    }

    public class ba_Banco_Cuenta_List
    {
        string Variable = "ba_Banco_Cuenta_Info";
        public List<ba_Banco_Cuenta_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_Banco_Cuenta_Info> list = new List<ba_Banco_Cuenta_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_Banco_Cuenta_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_Banco_Cuenta_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class ba_Banco_Cbte_List
    {
        string Variable = "ba_Cbte_Ban_Info";
        public List<ba_Cbte_Ban_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_Cbte_Ban_Info> list = new List<ba_Cbte_Ban_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_Cbte_Ban_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_Cbte_Ban_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class ba_Banco_Det_List
    {
        string Variable = "ba_Banco_Cuenta_x_tb_sucursal_Info";
        public List<ba_Banco_Cuenta_x_tb_sucursal_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_Banco_Cuenta_x_tb_sucursal_Info> list = new List<ba_Banco_Cuenta_x_tb_sucursal_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_Banco_Cuenta_x_tb_sucursal_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_Banco_Cuenta_x_tb_sucursal_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ba_Banco_Cuenta_x_tb_sucursal_Info info_det, decimal IdTransaccionSession)
        {
            List<ba_Banco_Cuenta_x_tb_sucursal_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.IdSucursal = info_det.IdSucursal;


            list.Add(info_det);
        }

        public void UpdateRow(ba_Banco_Cuenta_x_tb_sucursal_Info info_det, decimal IdTransaccionSession)
        {
            ba_Banco_Cuenta_x_tb_sucursal_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdSucursal = info_det.IdSucursal;
            edited_info.IdBanco = info_det.IdBanco;
            edited_info.Secuencia = info_det.Secuencia;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ba_Banco_Cuenta_x_tb_sucursal_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

}