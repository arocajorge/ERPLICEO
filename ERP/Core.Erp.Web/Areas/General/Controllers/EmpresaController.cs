using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.General;
using Core.Erp.Bus.General;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using System.IO;
using ExcelDataReader;


namespace Core.Erp.Web.Areas.General.Controllers
{
   // [SessionTimeout]

    public class EmpresaController : Controller
    {
        #region Index
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        tb_empresa_List ListaEmpresa = new tb_empresa_List();
        tb_sucursal_List ListaSucursal = new tb_sucursal_List();
        tb_bodega_List ListaBodega = new tb_bodega_List();

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_empresa()
        {
            List<tb_empresa_Info> model = bus_empresa.get_list(true);
            return PartialView("_GridViewPartial_empresa", model);
        }

        #endregion
        #region Acciones

        public ActionResult Nuevo()
        {
            tb_empresa_Info model = new tb_empresa_Info
            {
                em_fechaInicioContable = DateTime.Now.Date,
                em_logo = new byte[0]
        };
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(tb_empresa_Info model)
        {
            model.em_logo = empresa_imagen.pr_imagen;
            if (Session["imagen"]!=null)
            model.em_logo = Session["imagen"] as byte[];
            if (!bus_empresa.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0)
        {
            tb_empresa_Info model = bus_empresa.get_info(IdEmpresa);
            if (model.em_logo == null)
                model.em_logo = new byte[0];
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(tb_empresa_Info model)
        {
            model.em_logo = empresa_imagen.pr_imagen;
            if (!bus_empresa.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0)
        {
            tb_empresa_Info model = bus_empresa.get_info(IdEmpresa);
            if (model.em_logo == null)
                model.em_logo = new byte[0];
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_empresa_Info model)
        {
            if (!bus_empresa.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Imagen

        const string UploadDirectory = "~/Content/imagenes/";

        public UploadedFile UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControl", empresa_imagen.UploadValidationSettings, empresa_imagen.FileUploadComplete);

            byte[] model = empresa_imagen.pr_imagen;
            UploadedFile file = new UploadedFile();
            return file;
        }

        public ActionResult get_imagen()
        {

            byte[] model = empresa_imagen.pr_imagen;
            if (model == null)
                model = new byte[0];
            return PartialView("_Empresa_imagen", model);
        }

        public class empresa_imagen
        {
            public static byte[] pr_imagen { get; set; }
            public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
            {
                AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" },
                MaxFileSize = 4000000
            };
            public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
            {

                if (e.UploadedFile.IsValid)
                {
                    pr_imagen = e.UploadedFile.FileBytes;
                }
            }
        }
        #endregion
        #region Importacion
        public ActionResult UploadControlUp()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings.UploadValidationSettings, UploadControlSettings.FileUploadComplete);
            return null;
        }
        public ActionResult Importar(int IdEmpresa = 0)
        {
            #region Validar Session
            SessionFixed.IdTransaccionSession = "999999";
            SessionFixed.IdTransaccionSessionActual = "999999";
            #endregion

            tb_empresa_Info model = new tb_empresa_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(tb_empresa_Info model)
        {
            try
            {
                var Lista_Empresa = ListaEmpresa.get_list(model.IdTransaccionSession);
                var Lista_Sucursal = ListaSucursal.get_list(model.IdTransaccionSession);
                var Lista_Bodega = ListaBodega.get_list(model.IdTransaccionSession);
                
                if (!bus_empresa.GuardarDbImportacion(Lista_Empresa, Lista_Sucursal, Lista_Bodega))
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
        public ActionResult GridViewPartial_Empresa_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaEmpresa.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Empresa_importacion", model);
        }

        public ActionResult GridViewPartial_Sucursal_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaSucursal.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Sucursal_importacion", model);
        }

        public ActionResult GridViewPartial_Bodega_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaBodega.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Bodega_importacion", model);
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

        #region Json
        public JsonResult ObtenerURL()
        {
            string baseURL = Request.Url.Scheme + "://" + Request.Url.Authority;
            return Json(baseURL, JsonRequestBehavior.AllowGet);
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
            tb_empresa_List ListaEmpresa = new tb_empresa_List();
            List<tb_empresa_Info> Lista_Empresa = new List<tb_empresa_Info>();
            tb_sucursal_List ListaSucursal = new tb_sucursal_List();
            List<tb_sucursal_Info> Lista_Sucursal = new List<tb_sucursal_Info>();
            tb_bodega_List ListaBodega = new tb_bodega_List();
            List<tb_bodega_Info> Lista_Bodega = new List<tb_bodega_Info>();


            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            #endregion


            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                #region Empresa                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        tb_empresa_Info info = new tb_empresa_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader.GetValue(0)),
                            codigo = Convert.ToString(reader.GetValue(1)),
                            em_nombre = Convert.ToString(reader.GetValue(2)),
                            RazonSocial = Convert.ToString(reader.GetValue(3)),
                            NombreComercial = Convert.ToString(reader.GetValue(4)),
                            ContribuyenteEspecial = Convert.ToString(reader.GetValue(5)),
                            em_ruc = Convert.ToString(reader.GetValue(6)),
                            em_gerente = Convert.ToString(reader.GetValue(7)),
                            em_contador = Convert.ToString(reader.GetValue(8)),
                            em_rucContador = Convert.ToString(reader.GetValue(9)),
                            em_telefonos = Convert.ToString(reader.GetValue(10)),
                            em_direccion = Convert.ToString(reader.GetValue(11)),
                            em_fechaInicioContable = reader.GetDateTime(12),
                            cod_entidad_dinardap = Convert.ToString(reader.GetValue(13)),
                            em_Email = Convert.ToString(reader.GetValue(14))
                        };
                        info.em_fechaInicioActividad = info.em_fechaInicioContable;
                        Lista_Empresa.Add(info);
                    }
                    else
                        cont++;
                }
                ListaEmpresa.set_list(Lista_Empresa, IdTransaccionSession);
                #endregion

                cont = 0;
                reader.NextResult();

                #region Sucursal                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        tb_sucursal_Info info = new tb_sucursal_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader.GetValue(0)),
                            IdSucursal = Convert.ToInt32(reader.GetValue(1)),
                            codigo = Convert.ToString(reader.GetValue(2)),
                            Su_Descripcion = Convert.ToString(reader.GetValue(3)),
                            Su_CodigoEstablecimiento = Convert.ToString(reader.GetValue(4)),
                            Su_Ruc = Convert.ToString(reader.GetValue(5)),
                            Su_JefeSucursal = Convert.ToString(reader.GetValue(6)),
                            Su_Telefonos = Convert.ToString(reader.GetValue(7)),
                            Su_Direccion = Convert.ToString(reader.GetValue(8)),
                            IdUsuario = SessionFixed.IdUsuario
                        };
                        Lista_Sucursal.Add(info);
                    }
                    else
                        cont++;
                }
                ListaSucursal.set_list(Lista_Sucursal, IdTransaccionSession);
                #endregion

                cont = 0;
                //Para avanzar a la siguiente hoja de excel
                reader.NextResult();
               
                #region Bodega                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        tb_bodega_Info info = new tb_bodega_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader.GetValue(0)),
                            IdSucursal = Convert.ToInt32(reader.GetValue(1)),
                            IdBodega = Convert.ToInt32(reader.GetValue(2)),
                            cod_bodega = Convert.ToString(reader.GetValue(3)),
                            bo_Descripcion = Convert.ToString(reader.GetValue(4)),
                            IdCtaCtble_Inve = Convert.ToString(reader.GetValue(5)),
                            IdUsuario = SessionFixed.IdUsuario
                        };
                        Lista_Bodega.Add(info);
                    }
                    else
                        cont++;
                }
                ListaBodega.set_list(Lista_Bodega, IdTransaccionSession);
                #endregion
                
            }
        }
    }

    public class tb_empresa_List
    {
        string Variable = "tb_empresa_Info";
        public List<tb_empresa_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<tb_empresa_Info> list = new List<tb_empresa_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<tb_empresa_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<tb_empresa_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

}


