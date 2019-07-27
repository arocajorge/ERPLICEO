using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Bus.General;
using Core.Erp.Info.Helps;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Info.General;
using System.IO;
using ExcelDataReader;
using Core.Erp.Web.Areas.General.Controllers;
using Core.Erp.Info.Contabilidad;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    [SessionTimeout]
    public class ProveedorController : Controller
    {
        #region Variables
        cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
        cp_proveedor_clase_Bus bus_claseproveedor = new cp_proveedor_clase_Bus();
        tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
        ct_plancta_Bus bus_cuenta = new ct_plancta_Bus();
        tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
        cp_codigo_SRI_Bus bus_codigo = new cp_codigo_SRI_Bus();
        cp_proveedor_clase_Bus bus_clase = new cp_proveedor_clase_Bus();
        tb_banco_Bus bus_banco = new tb_banco_Bus();
        cp_proveedor_List ListaProveedor = new cp_proveedor_List();
        cp_proveedor_clase_List ListaClaseProveedor = new cp_proveedor_clase_List();
        tb_persona_List ListaPersona = new tb_persona_List();
        
        #endregion

        #region Metodos ComboBox bajo demanda banco
        public ActionResult CmbBanco_Proveedor()
        {
            int model = new int();
            return PartialView("_CmbBanco_Proveedor", model);
        }
        public List<tb_banco_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_banco.get_list_bajo_demanda(args);
        }
        public tb_banco_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_banco.get_info_bajo_demanda(args);
        }
        #endregion

        #region Metodos ComboBox bajo demanda CtaCbleCXP
        public ActionResult CmbCtaCbleCXP_Proveedor()
        {
            int model = new int();
            return PartialView("_CmbCtaCbleCXP_Proveedor", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_ctacble_cxp(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_cuenta.get_list_bajo_demanda(args, IdEmpresa, true);
        }
        public ct_plancta_Info get_info_bajo_demanda_ctacble_cxp(ListEditItemRequestedByValueEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_cuenta.get_info_bajo_demanda(args, IdEmpresa);
        }
        #endregion
        
        #region Metodos ComboBox bajo demanda CtaCbleGasto
        public ActionResult CmbCtaCbleGasto_Proveedor()
        {
            int model = new int();
            return PartialView("_CmbCtaCbleGasto_Proveedor", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_ctacble_gasto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_cuenta.get_list_bajo_demanda(args, IdEmpresa,true);
        }
        public ct_plancta_Info get_info_bajo_demanda_ctacble_gasto(ListEditItemRequestedByValueEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_cuenta.get_info_bajo_demanda(args, IdEmpresa);
        }
        #endregion

        #region Metodos ComboBox bajo demanda CtaCbleAnticipo
        public ActionResult CmbCtaCbleAnticipo_Proveedor()
        {
            int model = new int();
            return PartialView("_CmbCtaCbleAnticipo_Proveedor", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_ctacble_Anticipo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_cuenta.get_list_bajo_demanda(args, IdEmpresa, true);
        }
        public ct_plancta_Info get_info_bajo_demanda_ctacble_Anticipo(ListEditItemRequestedByValueEventArgs args)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return bus_cuenta.get_info_bajo_demanda(args, IdEmpresa);
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_proveedor()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_proveedor.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_proveedor", model);
        }

        #endregion

        #region Metodos

        private void cargar_combos(int IdEmpresa, string IdTipoSRI = "")
        {
            var lst_banco = bus_banco.get_list(false);
            ViewBag.lst_banco = lst_banco;
            var lst_tipo_cta = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIP_CTA_AC), false);
            ViewBag.lst_tipo_cta = lst_tipo_cta;
            var lst_tipo_doc = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODOC), false);
            ViewBag.lst_tipo_doc = lst_tipo_doc;
            var lst_tipo_naturaleza = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPONATPER), false);
            ViewBag.lst_tipo_naturaleza = lst_tipo_naturaleza;

            
            var lst_cuentas = bus_cuenta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_cuentas;

            var lst_ciudad = bus_ciudad.get_list("",false);
            ViewBag.lst_ciudad = lst_ciudad;

            var lst_codigo = bus_codigo.get_list(IdTipoSRI, false);
            ViewBag.lst_codigo = lst_codigo;

            var lst_clase = bus_clase.get_list(IdEmpresa, false);
            ViewBag.lst_clase = lst_clase;


        }
        #endregion

        #region Acciones

        public ActionResult Nuevo(int IdEmpresa = 0 )
        {
            cp_proveedor_Info model = new cp_proveedor_Info
            {
                IdEmpresa = IdEmpresa,
                IdClaseProveedor = 1,
                IdCiudad = "09",
                info_persona = new Info.General.tb_persona_Info
                {
                    pe_Naturaleza = "NATU",
                    IdTipoDocumento = "CED"
                }
            };
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cp_proveedor_Info model)
        {
            var return_naturaleza = "";
            model.IdUsuario = Session["IdUsuario"].ToString();
            if ((cl_funciones.ValidaIdentificacion(model.info_persona.IdTipoDocumento, model.info_persona.pe_Naturaleza, model.info_persona.pe_cedulaRuc, ref return_naturaleza)))
            {
                model.info_persona.pe_Naturaleza = return_naturaleza;
                if (!bus_proveedor.guardarDB(model))
                {
                    cargar_combos(model.IdEmpresa);
                    return View(model);
                }
            }
            else
            {
                ViewBag.mensaje = "Número de identificación inválida";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0 , decimal IdProveedor = 0)
        {
            cp_proveedor_Info model = bus_proveedor.get_info(IdEmpresa, IdProveedor);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cp_proveedor_Info model)
        {
            var return_naturaleza = "";
            model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();

            if ((cl_funciones.ValidaIdentificacion(model.info_persona.IdTipoDocumento, model.info_persona.pe_Naturaleza, model.info_persona.pe_cedulaRuc, ref return_naturaleza)))
            {
                model.info_persona.pe_Naturaleza = return_naturaleza;
                if (!bus_proveedor.modificarDB(model))
                {
                    cargar_combos(model.IdEmpresa);
                    return View(model);
                }
            }
            else
            {
                ViewBag.mensaje = "Número de identificación inválida";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0 , decimal IdProveedor = 0)
        {
            cp_proveedor_Info model = bus_proveedor.get_info(IdEmpresa, IdProveedor);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cp_proveedor_Info model)
        {
            model.IdUsuarioUltMod = Session["IdUsuario"].ToString();
            if (!bus_proveedor.anularDB(model))
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

            cp_proveedor_Info model = new cp_proveedor_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(cp_proveedor_Info model)
        {
            try
            {
                var Lista_Proveedor = ListaProveedor.get_list(model.IdTransaccionSession);
                var Lista_ClaseProveedor = ListaClaseProveedor.get_list(model.IdTransaccionSession);

                foreach (var item in Lista_ClaseProveedor)
                {
                    if (!bus_claseproveedor.guardarDB(item))
                    {
                        ViewBag.mensaje = "Error al importar el archivo";
                        return View(model);
                    }
                }

                foreach (var item in Lista_Proveedor)
                {
                    //item.info_persona.pe_Naturaleza = return_naturaleza;
                    if (!bus_proveedor.guardarDB_importacion(item))
                    {
                        ViewBag.mensaje = "Error al importar el archivo";
                        return View(model);
                    }
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
        public ActionResult GridViewPartial_clase_proveedor_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaClaseProveedor.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_clase_proveedor_importacion", model);
        }

        public ActionResult GridViewPartial_proveedor_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaProveedor.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_proveedor_importacion", model);
        }
        #endregion

        #region json
        public JsonResult get_info_x_num_cedula(int IdEmpresa = 0 , string pe_cedulaRuc = "")
        {
            var resultado = bus_proveedor.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_info_clase_proveedor(int IdEmpresa = 0 , int IdClaseProveedor = 0)
        {
            cp_proveedor_clase_Bus bus_clase = new cp_proveedor_clase_Bus();
            cp_proveedor_clase_Info resultado = bus_clase.get_info(IdEmpresa, IdClaseProveedor);
            if (resultado == null)
                resultado = new cp_proveedor_clase_Info();

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_info(int IdEmpresa = 0 , decimal IdProveedor = 0 )
        {
            var resultado = bus_proveedor.get_info(IdEmpresa, IdProveedor);
            return Json(resultado, JsonRequestBehavior.AllowGet);
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
            cp_proveedor_List ListaProveedor = new cp_proveedor_List();
            List<cp_proveedor_Info> Lista_Proveedor = new List<cp_proveedor_Info>();
            cp_proveedor_clase_List ListaClaseProveedor = new cp_proveedor_clase_List();
            List<cp_proveedor_clase_Info> Lista_ClaseProveedor = new List<cp_proveedor_clase_Info>();
            tb_persona_Bus bus_persona = new tb_persona_Bus();

            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            #endregion

            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                #region ClaseProveedor                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        cp_proveedor_clase_Info info = new cp_proveedor_clase_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdClaseProveedor = Convert.ToInt32(reader.GetValue(0)),
                            cod_clase_proveedor = Convert.ToString(reader.GetValue(1)),
                            descripcion_clas_prove = Convert.ToString(reader.GetValue(2)),
                            IdCtaCble_gasto = null,//Convert.ToString(reader.GetValue(3)),
                            IdCtaCble_CXP = null,//Convert.ToString(reader.GetValue(4)),
                            IdUsuario = SessionFixed.IdUsuario
                        };
                        Lista_ClaseProveedor.Add(info);
                    }
                    else
                        cont++;
                }
                ListaClaseProveedor.set_list(Lista_ClaseProveedor, IdTransaccionSession);
                #endregion

                cont = 0;
                //Para avanzar a la siguiente hoja de excel
                reader.NextResult();

                #region Proveedor   
                var lst_persona = bus_persona.get_list(false);

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        var cedula_ruc = (Convert.ToString(reader.GetValue(3))).Trim();

                        tb_persona_Info info_persona = new tb_persona_Info();
                        tb_persona_Info info_persona_prov = new tb_persona_Info();
                        var return_naturaleza = "";
                        info_persona = lst_persona.Where(q => q.pe_cedulaRuc.Trim() == cedula_ruc.Trim()).FirstOrDefault();
                        info_persona_prov = info_persona;

                        if (cl_funciones.ValidaIdentificacion(Convert.ToString(reader.GetValue(2)), Convert.ToString(reader.GetValue(4)), cedula_ruc, ref return_naturaleza ))
                        {
                            if (info_persona == null)
                            {
                                tb_persona_Info info_ = new tb_persona_Info
                                {
                                    pe_Naturaleza = Convert.ToString(reader.GetValue(4)),
                                    pe_nombreCompleto = Convert.ToString(reader.GetValue(5)),
                                    pe_razonSocial = Convert.ToString(reader.GetValue(5)),
                                    pe_apellido = Convert.ToString(reader.GetValue(6)),
                                    pe_nombre = Convert.ToString(reader.GetValue(7)),
                                    IdTipoDocumento = Convert.ToString(reader.GetValue(2)),
                                    pe_cedulaRuc = cedula_ruc,
                                    pe_direccion = Convert.ToString(reader.GetValue(9)),
                                    pe_telfono_Contacto = Convert.ToString(reader.GetValue(10)),
                                    pe_celular = Convert.ToString(reader.GetValue(11)),
                                    pe_correo = Convert.ToString(reader.GetValue(8)),
                                };

                                info_persona_prov = info_;
                            }
                            else
                            {
                                info_persona_prov = bus_persona.get_info(info_persona.IdPersona);

                                info_persona_prov.pe_Naturaleza = Convert.ToString(reader.GetValue(4));
                                info_persona_prov.pe_nombreCompleto = Convert.ToString(reader.GetValue(5));
                                info_persona_prov.pe_razonSocial = Convert.ToString(reader.GetValue(5));
                                info_persona_prov.pe_apellido = Convert.ToString(reader.GetValue(6));
                                info_persona_prov.pe_nombre = Convert.ToString(reader.GetValue(7));
                                info_persona_prov.IdTipoDocumento = Convert.ToString(reader.GetValue(2));
                                info_persona_prov.pe_cedulaRuc = cedula_ruc;
                                info_persona_prov.pe_direccion = Convert.ToString(reader.GetValue(9));
                                info_persona_prov.pe_telfono_Contacto = Convert.ToString(reader.GetValue(10));
                                info_persona_prov.pe_celular = Convert.ToString(reader.GetValue(11));
                                info_persona_prov.pe_correo = Convert.ToString(reader.GetValue(8));
                            }

                            info_persona_prov.pe_Naturaleza = return_naturaleza;

                            cp_proveedor_Info info = new cp_proveedor_Info
                            {
                                IdEmpresa = IdEmpresa,
                                IdProveedor = Convert.ToInt32(reader.GetValue(0)),
                                IdPersona = info_persona_prov.IdPersona,
                                IdCiudad = "09",
                                pr_codigo = string.IsNullOrEmpty(Convert.ToString(reader.GetValue(1))) ? null : Convert.ToString(reader.GetValue(1)),
                                pr_plazo = Convert.ToInt32(reader.GetValue(19)),
                                IdCtaCble_CXP = string.IsNullOrEmpty(Convert.ToString(reader.GetValue(15))) ? null : Convert.ToString(reader.GetValue(15)),
                                IdCtaCble_Gasto = string.IsNullOrEmpty(Convert.ToString(reader.GetValue(14))) ? null : Convert.ToString(reader.GetValue(14)),
                                IdClaseProveedor = Convert.ToInt32(reader.GetValue(13)),
                                num_cta_acreditacion = string.IsNullOrEmpty(Convert.ToString(reader.GetValue(18))) ? null : Convert.ToString(reader.GetValue(18)),
                                IdBanco_acreditacion = 4,
                                es_empresa_relacionada = (Convert.ToString(reader.GetValue(12)) == "SI") ? true : false,
                                pr_telefonos = Convert.ToString(reader.GetValue(10)),
                                pr_celular = Convert.ToString(reader.GetValue(11)),
                                pr_direccion = Convert.ToString(reader.GetValue(9)),
                                pr_correo = Convert.ToString(reader.GetValue(8)),
                                IdUsuario = SessionFixed.IdUsuario,
                                
                            };

                            info.info_persona = info_persona_prov;

                            if (Lista_Proveedor.Where(q => q.info_persona.pe_cedulaRuc == info_persona_prov.pe_cedulaRuc).Count() == 0)
                                Lista_Proveedor.Add(info);


                        }                        
                    }
                    else
                        cont++;
                }                
                ListaProveedor.set_list(Lista_Proveedor, IdTransaccionSession);
                #endregion
            }
        }
    }

    public class cp_proveedor_List
    {
        string Variable = "cp_proveedor_Info";
        public List<cp_proveedor_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_proveedor_Info> list = new List<cp_proveedor_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_proveedor_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_proveedor_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}