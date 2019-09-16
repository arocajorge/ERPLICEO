using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    [SessionTimeout]
    public class PlanDeCuentasController : Controller
    {
        #region Variables
        ct_plancta_List ListaPlancta = new ct_plancta_List();
        ct_anio_fiscal_List ListaAnioFiscal = new ct_anio_fiscal_List();
        ct_anio_fiscal_Bus bus_anio_fiscal = new ct_anio_fiscal_Bus();
        #endregion

        #region Index
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_plancta()
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ct_plancta_Info> model = bus_plancta.get_list(IdEmpresa, true, false);
            return PartialView("_GridViewPartial_plancta", model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_cuentas = bus_plancta.get_list(IdEmpresa, false, false);
            ViewBag.lst_cuentas = lst_cuentas;

            Dictionary<string, string> lst_naturaleza = new Dictionary<string, string>();
            lst_naturaleza.Add("D","Deudora");
            lst_naturaleza.Add("C", "Acreedora");
            ViewBag.lst_naturaleza = lst_naturaleza;

            ct_grupocble_Bus bus_grupo_contable = new ct_grupocble_Bus();
            var lst_grupo_contabe = bus_grupo_contable.get_list(false);
            ViewBag.lst_grupo_contabe = lst_grupo_contabe;
        }
        #endregion

        #region Metodos ComboBox bajo demanda

        public ActionResult CmbCuenta_PlanCta()
        {
            ct_plancta_Info model = new ct_plancta_Info();
            return PartialView("_CmbCuenta_PlanCta", model);
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


        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            ct_plancta_Info model = new ct_plancta_Info
            {
                IdEmpresa = IdEmpresa
            };
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ct_plancta_Info model)
        {
            if (bus_plancta.validar_existe_id(model.IdEmpresa,model.IdCtaCble))
            {
                ViewBag.mensaje = "El código de la cuenta ya se encuentra registrado";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            if (!bus_plancta.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0, string IdCtaCble = "")
        {   
            ct_plancta_Info model = bus_plancta.get_info(IdEmpresa, IdCtaCble);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ct_plancta_Info model)
        {
            if (!bus_plancta.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0, string IdCtaCble = "")
        {
            ct_plancta_Info model = bus_plancta.get_info(IdEmpresa, IdCtaCble);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_plancta_Info model)
        {
            if (!bus_plancta.anularDB(model))
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

            ct_plancta_Info model = new ct_plancta_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(ct_plancta_Info model)
        {
            var Lista = ListaPlancta.get_list(model.IdTransaccionSession);
            foreach (var item in Lista)
            {
                bus_plancta.guardarDB(item);
            }
            var ListaAnio = ListaAnioFiscal.get_list(model.IdTransaccionSession);
            foreach (var item in ListaAnio)
            {
                bus_anio_fiscal.guardarDB(item);
            }
            return RedirectToAction("Index");
        }
        public ActionResult GridViewPartial_plancta_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaPlancta.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_plancta_importacion", model);
        }

        public ActionResult GridViewPartial_anio_fiscal_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaAnioFiscal.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_anio_fiscal_importacion", model);
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

        public JsonResult get_info_nuevo(int IdEmpresa = 0, string IdCtaCble_padre = "")
        {
            var resultado = bus_plancta.get_info_nuevo(IdEmpresa, IdCtaCble_padre);
            
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
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
            ct_anio_fiscal_List ListaAnioFiscal = new ct_anio_fiscal_List();
            ct_plancta_List ListaPlancta = new ct_plancta_List();
            List<ct_plancta_Info> ListaPlan = new List<ct_plancta_Info>();
            List<ct_anio_fiscal_Info> ListaAnio = new List<ct_anio_fiscal_Info>();

            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            #endregion


            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                
                #region Plan de cuentas                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        ct_plancta_Info info = new ct_plancta_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdCtaCble = reader.GetString(0),
                            pc_clave_corta = reader.GetString(1),
                            pc_Cuenta = reader.GetString(2),
                            IdCtaCblePadre = reader.GetValue(3) == null ? null : (string.IsNullOrEmpty(reader.GetString(3)) ? null : reader.GetString(3)),
                            pc_Naturaleza = reader.GetString(4),
                            IdNivelCta = Convert.ToInt32(reader.GetValue(5)),
                            pc_EsMovimiento_bool = reader.GetString(6) == "SI" ? true : false,
                            pc_EsMovimiento = reader.GetString(6) == "SI" ? "S" : "N",
                            IdGrupoCble = reader.GetString(7)
                        };
                        ListaPlan.Add(info);
                    }
                    else
                        cont++;
                }
                #endregion

                cont = 0;
                //Para avanzar a la siguiente hoja de excel
                reader.NextResult();

                #region Cuentas contables por anio
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        ct_anio_fiscal_Info info = new ct_anio_fiscal_Info
                        {                            
                            IdanioFiscal = Convert.ToInt32(reader.GetValue(0)),
                            af_fechaIni = new DateTime(Convert.ToInt32(reader.GetValue(0)), 1, 1),
                            af_fechaFin = new DateTime(Convert.ToInt32(reader.GetValue(0)), 12, 31),
                            info_anio_ctautil = new ct_anio_fiscal_x_cuenta_utilidad_Info
                            {
                                IdEmpresa = IdEmpresa,
                                IdCtaCble = reader.GetString(1),
                                IdanioFiscal = Convert.ToInt32(reader.GetValue(0)),                                
                            },                            
                        };
                        ListaAnio.Add(info);
                    }
                    else
                        cont++;
                }
                #endregion

                ListaPlancta.set_list(ListaPlan,IdTransaccionSession);
                ListaAnioFiscal.set_list(ListaAnio, IdTransaccionSession);
            }
        }
    }
    public class ct_plancta_List
    {
        string Variable = "ct_plancta_Info";
        public List<ct_plancta_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession] == null)
            {
                List<ct_plancta_Info> list = new List<ct_plancta_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
            }
            return (List<ct_plancta_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession];
        }

        public void set_list(List<ct_plancta_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
        }
    }    
}