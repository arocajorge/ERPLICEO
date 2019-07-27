using Core.Erp.Bus.Facturacion;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Facturacion;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;

using ExcelDataReader;
using System.IO;
using Core.Erp.Web.Areas.General.Controllers;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    [SessionTimeout]
    public class ClienteController : Controller
    {
        #region Variables
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        fa_cliente_tipo_Bus bus_cliente_tipo = new fa_cliente_tipo_Bus();
        fa_cliente_contactos_Bus bus_cliente_contacto = new fa_cliente_contactos_Bus();
        fa_cliente_contactos_List List_fa_cliente_contactos = new fa_cliente_contactos_List();
        fa_cliente_x_fa_Vendedor_x_sucursal_list List_fa_cliente_x_fa_Vendedor_x_sucursal = new fa_cliente_x_fa_Vendedor_x_sucursal_list();
        fa_cliente_x_fa_Vendedor_x_sucursal_Bus bus_fa_vendedor = new fa_cliente_x_fa_Vendedor_x_sucursal_Bus();
        tb_parroquia_Bus bus_parroquia = new tb_parroquia_Bus();
        tb_parroquia_List ListaParroquia = new tb_parroquia_List();
        List<tb_parroquia_Info> Lista_Parroquia = new List<tb_parroquia_Info>();
        tb_persona_List ListaPersona = new tb_persona_List();
        fa_cliente_tipo_List ListaTipoCliente = new fa_cliente_tipo_List();
        fa_cliente_List ListaCliente = new fa_cliente_List();
        string mensaje = string.Empty;
        #endregion
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        private bool validar(fa_cliente_Info i_validar, ref string msg)
        {
            return true;
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_cliente()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_cliente.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_cliente", model);
        }
        #endregion
        #region Metodos
        private void cargar_combos(fa_cliente_Info info)
        {
            fa_NivelDescuento_Bus bus_nivel = new fa_NivelDescuento_Bus();
            var lst_nivel = bus_nivel.GetList(info.IdEmpresa, false);
            ViewBag.lst_nivel = lst_nivel;

            fa_formaPago_Bus bus_formapago = new fa_formaPago_Bus();
            var lst_formapago = bus_formapago.get_list(false);
            ViewBag.lst_formapago = lst_formapago;

            fa_cliente_tipo_Bus bus_clientetipo = new fa_cliente_tipo_Bus();
            var lst_clientetipo = bus_clientetipo.get_list(info.IdEmpresa, false);
            ViewBag.lst_clientetipo = lst_clientetipo;

  
            fa_TerminoPago_Bus bus_termino_pago = new fa_TerminoPago_Bus();
            var lst_termino_pago = bus_termino_pago.get_list(false);
            ViewBag.lst_termino_pago = lst_termino_pago;

            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var lst_ctacble = bus_plancta.get_list(info.IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_ctacble;

            tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
            var lst_tipo_doc = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODOC), false);
            ViewBag.lst_tipo_doc = lst_tipo_doc;

            var lst_tipo_naturaleza = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPONATPER), false);
            ViewBag.lst_tipo_naturaleza = lst_tipo_naturaleza;

            tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
            var lst_ciudad = bus_ciudad.get_list("", false);
            ViewBag.lst_ciudad = lst_ciudad;
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
            fa_cliente_Info model = new fa_cliente_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                Idtipo_cliente = 1,
                IdCiudad = "09",
                IdParroquia = "09",
                info_persona = new Info.General.tb_persona_Info
                {
                    pe_Naturaleza = "NATU",
                    IdTipoDocumento = "CED",                    
                },

                Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>()
            };

            List_fa_cliente_x_fa_Vendedor_x_sucursal.set_list(model.Lst_fa_cliente_x_fa_Vendedor_x_sucursal, model.IdTransaccionSession);
            cargar_combos(model);
            cargar_combos_det();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(fa_cliente_Info model)
        {
            string return_naturaleza = "";
            model.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = List_fa_cliente_x_fa_Vendedor_x_sucursal.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuario = Session["IdUsuario"].ToString();

            if (cl_funciones.ValidaIdentificacion(model.info_persona.IdTipoDocumento, model.info_persona.pe_Naturaleza, model.info_persona.pe_cedulaRuc, ref return_naturaleza))
            {
                model.info_persona.pe_Naturaleza = return_naturaleza;
                if (!bus_cliente.guardarDB(model))
                {
                    cargar_combos(model);
                    return View(model);
                }
            }
            else
            {
                ViewBag.mensaje = "Número de identificación inválida";
                cargar_combos(model);
                return View(model);
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0, decimal IdCliente = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            fa_cliente_Info model = bus_cliente.get_info(IdEmpresa, IdCliente);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = bus_fa_vendedor.get_list(IdEmpresa, IdCliente);
            List_fa_cliente_x_fa_Vendedor_x_sucursal.set_list(model.Lst_fa_cliente_x_fa_Vendedor_x_sucursal, model.IdTransaccionSession);

            //List_fa_cliente_contactos.set_list(model.lst_fa_cliente_contactos, model.IdTransaccionSession);

            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(fa_cliente_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();
            model.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = List_fa_cliente_x_fa_Vendedor_x_sucursal.get_list(model.IdTransaccionSession);
            //model.lst_fa_cliente_contactos = List_fa_cliente_contactos.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            //model.lst_fa_cliente_contactos.ForEach(q => { q.IdEmpresa = model.IdEmpresa; q.IdCliente = model.IdCliente; });
            fa_cliente_contactos_Info info_contacto = new fa_cliente_contactos_Info
            {
                IdEmpresa = model.IdEmpresa,
                IdCliente = model.IdCliente,
                IdContacto = model.IdContacto,
                Nombres = "",
                Direccion = model.Direccion,
                Correo = model.Correo,
                Telefono = model.Telefono,
                Celular = model.Celular,
                IdCiudad = model.IdCiudad,
                IdParroquia = model.IdParroquia,
            };
            

            if (!bus_cliente_contacto.guardarDB(info_contacto))
            {
                cargar_combos(model);
                return View(model);
            }

            if (!bus_cliente.modificarDB(model))
            {
                cargar_combos(model);
                return View(model);
            }
           
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0 , decimal IdCliente = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            fa_cliente_Info model = bus_cliente.get_info(IdEmpresa, IdCliente);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_fa_cliente_contactos = bus_cliente_contacto.get_list(IdEmpresa, IdCliente);
            List_fa_cliente_contactos.set_list(model.lst_fa_cliente_contactos, model.IdTransaccionSession);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = bus_fa_vendedor.get_list(IdEmpresa, IdCliente);
            List_fa_cliente_x_fa_Vendedor_x_sucursal.set_list(model.Lst_fa_cliente_x_fa_Vendedor_x_sucursal, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(fa_cliente_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            if (!bus_cliente.anularDB(model))
            {
                cargar_combos(model);
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

            fa_cliente_Info model = new fa_cliente_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(fa_cliente_Info model)
        {
            try
            {
                var Lista_TipoCliente = ListaTipoCliente.get_list(model.IdTransaccionSession);
                var Lista_Cliente = ListaCliente.get_list(model.IdTransaccionSession);
                var Lista_ClienteContactos = List_fa_cliente_contactos.get_list(model.IdTransaccionSession);
                var Lista_clienteVendedorSucursal = List_fa_cliente_x_fa_Vendedor_x_sucursal.get_list(model.IdTransaccionSession);

                foreach (var item in Lista_TipoCliente)
                {
                    if (!bus_cliente_tipo.guardarDB(item))
                    {
                        ViewBag.mensaje = "Error al importar el archivo";
                        return View(model);
                    }
                }

                foreach (var item in Lista_Cliente)
                {
                    if (!bus_cliente.guardarDB_importacion(item))
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
        public ActionResult GridViewPartial_cliente_tipo_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaTipoCliente.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_cliente_tipo_importacion", model);
        }

        public ActionResult GridViewPartial_cliente_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaCliente.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_cliente_importacion", model);
        }
        #endregion

        #region Json

        public ActionResult cmb_parroquia()
        {
            string IdCiudad = (Request.Params["fx_IdCiudad"] != null) ? Request.Params["fx_IdCiudad"].ToString() : "";
            return PartialView("_cmb_parroquia", new fa_cliente_Info { IdCiudad = IdCiudad});
        }

        public JsonResult get_info_termino_pago(string IdTerminoPago = "")
        {
            fa_TerminoPago_Bus bus_termino_pago = new fa_TerminoPago_Bus();
            var resultado = bus_termino_pago.get_info(IdTerminoPago);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_info_tipo_cliente(int IdEmpresa = 0 , int IdCliente_tipo = 0)
        {
            fa_cliente_tipo_Bus bus_cliente_tipo = new fa_cliente_tipo_Bus();
            var resultado = bus_cliente_tipo.get_info(IdEmpresa, IdCliente_tipo);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_info_x_num_cedula(int IdEmpresa = 0 , string pe_cedulaRuc = "")
        {
            var resultado = bus_cliente.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region detalle
        private void cargar_combos_det()
        {
            tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
            var lst_ciudad = bus_ciudad.get_list("", false);
            ViewBag.lst_ciudad = lst_ciudad;

            tb_parroquia_Bus bus_parroquia = new tb_parroquia_Bus();
            var lst_parroquia = bus_parroquia.get_list("", false);
            ViewBag.lst_parroquia = lst_parroquia;

            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            fa_Vendedor_Bus bus_vendedor = new fa_Vendedor_Bus();
            var lst_vendedor = bus_vendedor.get_list(IdEmpresa, false);
            ViewBag.lst_vendedor = lst_vendedor;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_cliente_contacto()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_fa_cliente_contactos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_cliente_contacto", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] fa_cliente_contactos_Info info_det)
        {
            if (ModelState.IsValid)
            {
                var parroquia = bus_parroquia.get_info(info_det.IdParroquia);
                info_det.nom_parroquia = parroquia == null ? "" : parroquia.nom_parroquia;
                List_fa_cliente_contactos.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            var model  = List_fa_cliente_contactos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_cliente_contacto", model);
        }

        [ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] fa_cliente_contactos_Info info_det)
        {
            if (ModelState.IsValid)
            {
                var parroquia = bus_parroquia.get_info(info_det.IdParroquia);
                info_det.nom_parroquia = parroquia == null ? "" : parroquia.nom_parroquia;
                List_fa_cliente_contactos.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            var model = List_fa_cliente_contactos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_cliente_contacto", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_fa_vendedor()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_fa_cliente_x_fa_Vendedor_x_sucursal.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_fa_vendedor", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Editing_AddNew([ModelBinder(typeof(DevExpressEditorsBinder))] fa_cliente_x_fa_Vendedor_x_sucursal_Info info_det)
        {
            if (ModelState.IsValid)
                List_fa_cliente_x_fa_Vendedor_x_sucursal.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model  = List_fa_cliente_x_fa_Vendedor_x_sucursal.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_fa_vendedor", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Editing_Update([ModelBinder(typeof(DevExpressEditorsBinder))] fa_cliente_x_fa_Vendedor_x_sucursal_Info info_det)
        {
            if (ModelState.IsValid)
                List_fa_cliente_x_fa_Vendedor_x_sucursal.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_fa_cliente_x_fa_Vendedor_x_sucursal.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_fa_vendedor", model);
        }

        public ActionResult Editing_Delete(decimal IdCliente)
        {
            List_fa_cliente_x_fa_Vendedor_x_sucursal.DeleteRow(IdCliente, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_fa_cliente_x_fa_Vendedor_x_sucursal.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_fa_vendedor", model);
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
            fa_cliente_tipo_List ListaClienteTipo = new fa_cliente_tipo_List();
            List<fa_cliente_tipo_Info> Lista_ClienteTipo = new List<fa_cliente_tipo_Info>();
            fa_cliente_List ListaCliente = new fa_cliente_List();
            List<fa_cliente_Info> Lista_Cliente = new List<fa_cliente_Info>();
            fa_cliente_contactos_List List_fa_cliente_contactos = new fa_cliente_contactos_List();
            List<fa_cliente_contactos_Info> Lista_ClienteContactos = new List<fa_cliente_contactos_Info>();
            fa_cliente_x_fa_Vendedor_x_sucursal_list List_fa_cliente_x_fa_Vendedor_x_sucursal = new fa_cliente_x_fa_Vendedor_x_sucursal_list();
            List<fa_cliente_x_fa_Vendedor_x_sucursal_Info> Lista_ClienteVendedor= new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();

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

                #region ClienteTipo                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        fa_cliente_tipo_Info info = new fa_cliente_tipo_Info
                        {
                            IdEmpresa = IdEmpresa,
                            Idtipo_cliente = Convert.ToInt32(reader.GetValue(0)),
                            Cod_cliente_tipo = Convert.ToString(reader.GetValue(1)),
                            Descripcion_tip_cliente = Convert.ToString(reader.GetValue(2)),
                            IdCtaCble_CXC_Cred = Convert.ToString(reader.GetValue(3)),
                            IdUsuario = SessionFixed.IdUsuario
                        };
                        Lista_ClienteTipo.Add(info);
                    }
                    else
                        cont++;
                }
                ListaClienteTipo.set_list(Lista_ClienteTipo, IdTransaccionSession);
                #endregion

                cont = 0;
                //Para avanzar a la siguiente hoja de excel
                reader.NextResult();

                #region Cliente   
                var lst_persona = bus_persona.get_list(false);

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        var return_naturaleza="";
                        var cedula_ruc = (Convert.ToString(reader.GetValue(3))).Trim();
                        tb_persona_Info info_persona = new tb_persona_Info();
                        tb_persona_Info info_persona_cliente = new tb_persona_Info();

                        info_persona = lst_persona.Where(q => q.pe_cedulaRuc == cedula_ruc).FirstOrDefault();
                        info_persona_cliente = info_persona;

                        if (cl_funciones.ValidaIdentificacion(Convert.ToString(reader.GetValue(2)), Convert.ToString(reader.GetValue(4)), cedula_ruc, ref return_naturaleza ))
                        {
                            if (info_persona == null)
                            {
                                tb_persona_Info info_ = new tb_persona_Info
                                {
                                    pe_Naturaleza = Convert.ToString(reader.GetValue(4)),
                                    pe_nombreCompleto = Convert.ToString(reader.GetValue(6)) + ' ' + Convert.ToString(reader.GetValue(7)),
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
                                info_persona_cliente = info_;
                            }
                            else
                            {
                                info_persona_cliente = bus_persona.get_info(info_persona.IdPersona);
                                var x = Convert.ToString(reader.GetValue(4));
                                info_persona_cliente.pe_Naturaleza = x;
                                info_persona_cliente.pe_nombreCompleto = Convert.ToString(reader.GetValue(6)) + ' ' + Convert.ToString(reader.GetValue(7));
                                info_persona_cliente.pe_razonSocial = Convert.ToString(reader.GetValue(5));
                                info_persona_cliente.pe_apellido = Convert.ToString(reader.GetValue(6));
                                info_persona_cliente.pe_nombre = Convert.ToString(reader.GetValue(7));
                                info_persona_cliente.IdTipoDocumento = Convert.ToString(reader.GetValue(2));
                                info_persona_cliente.pe_cedulaRuc = cedula_ruc;
                                info_persona_cliente.pe_direccion = Convert.ToString(reader.GetValue(9));
                                info_persona_cliente.pe_telfono_Contacto = Convert.ToString(reader.GetValue(10));
                                info_persona_cliente.pe_celular = Convert.ToString(reader.GetValue(11));
                                info_persona_cliente.pe_correo = Convert.ToString(reader.GetValue(8));
                            }

                            info_persona_cliente.pe_Naturaleza = return_naturaleza;
                            info_persona_cliente.pe_nombreCompleto = (info_persona_cliente.pe_razonSocial != "" ? info_persona_cliente.pe_razonSocial : (info_persona_cliente.pe_apellido+' '+info_persona.pe_nombre));

                            var CtaCbleClase = Lista_ClienteTipo.Where(q => q.Idtipo_cliente == Convert.ToInt32(reader.GetValue(13))).FirstOrDefault();
                            fa_cliente_Info info = new fa_cliente_Info
                            {
                                IdEmpresa = IdEmpresa,
                                IdPersona = info_persona_cliente.IdPersona,
                                IdCliente = Convert.ToInt32(reader.GetValue(0)),
                                Codigo = Convert.ToString(reader.GetValue(1)),
                                Idtipo_cliente = Convert.ToInt32(reader.GetValue(13)),
                                cl_plazo = Convert.ToInt32(reader.GetValue(15)),
                                cl_Cupo = Convert.ToDouble(reader.GetValue(16)),
                                IdCtaCble_cxc_Credito = CtaCbleClase==null ? null: CtaCbleClase.IdCtaCble_CXC_Cred,
                                es_empresa_relacionada = (Convert.ToString(reader.GetValue(12)) == "SI") ? true : false,
                                EsClienteExportador = false,
                                IdNivel = 1,
                                IdTipoCredito = "CON",
                                FormaPago = "01",
                                IdUsuario = SessionFixed.IdUsuario
                            };

                            fa_cliente_contactos_Info info_cliente_contacto = new fa_cliente_contactos_Info
                            {
                                IdEmpresa = IdEmpresa,
                                IdContacto = 1,
                                IdCiudad = Convert.ToString(reader.GetValue(18)),
                                IdParroquia = Convert.ToString(reader.GetValue(19)),
                                Celular = Convert.ToString(reader.GetValue(11)),
                                Correo = Convert.ToString(reader.GetValue(8)),
                                Direccion = Convert.ToString(reader.GetValue(9)),
                                Nombres = (Convert.ToString(reader.GetValue(4)) == "NATU") ? Convert.ToString(reader.GetValue(6)) + ' ' + Convert.ToString(reader.GetValue(7)) : Convert.ToString(reader.GetValue(5)),
                                Telefono = Convert.ToString(reader.GetValue(10)),

                            };

                            info.lst_fa_cliente_contactos = new List<fa_cliente_contactos_Info>();
                            info.lst_fa_cliente_contactos.Add(info_cliente_contacto);
                            info.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();
                            info.info_persona = info_persona_cliente;

                            if (Lista_Cliente.Where(q => q.info_persona.pe_cedulaRuc == info_persona_cliente.pe_cedulaRuc).Count() == 0)
                                Lista_Cliente.Add(info);
                        }
                    }
                    else
                        cont++;                    
                }
                ListaCliente.set_list(Lista_Cliente, IdTransaccionSession);
                #endregion
            }
        }
    }

    #region List

    public class fa_cliente_contactos_List
    {
        string variable = "fa_cliente_contactos_Info";

        public List<fa_cliente_contactos_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_cliente_contactos_Info> list = new List<fa_cliente_contactos_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_cliente_contactos_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_cliente_contactos_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }
        public void AddRow(fa_cliente_contactos_Info info_det, decimal IdTransaccionSession)
        {
            List<fa_cliente_contactos_Info> list = get_list(IdTransaccionSession);
            info_det.IdContacto = list.Count == 0 ? 1 : list.Max(q => q.IdContacto) + 1;
            list.Add(info_det);
        }

        public void UpdateRow(fa_cliente_contactos_Info info_det, decimal IdTransaccionSession)
        {
            fa_cliente_contactos_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdContacto == info_det.IdContacto).First();
            edited_info.IdCiudad = info_det.IdCiudad;
            edited_info.IdParroquia = info_det.IdParroquia;
            edited_info.Celular = info_det.Celular;
            edited_info.Correo = info_det.Correo;
            edited_info.Telefono = info_det.Telefono;
            edited_info.Direccion = info_det.Direccion;
            edited_info.Nombres = info_det.Nombres;
            edited_info.nom_parroquia = info_det.nom_parroquia;
        }

    }
    public class fa_cliente_x_fa_Vendedor_x_sucursal_list
    {
        string variable = "fa_cliente_x_fa_Vendedor_x_sucursal_Info";
        public List<fa_cliente_x_fa_Vendedor_x_sucursal_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_cliente_x_fa_Vendedor_x_sucursal_Info> list = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_cliente_x_fa_Vendedor_x_sucursal_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }
        public void AddRow(fa_cliente_x_fa_Vendedor_x_sucursal_Info info_det, decimal IdTransaccionSession)
        {
            List<fa_cliente_x_fa_Vendedor_x_sucursal_Info> list = get_list(IdTransaccionSession);
            info_det.IdCliente = list.Count == 0 ? 1 : list.Max(q => q.IdCliente) + 1;
            info_det.IdEmpresa = info_det.IdEmpresa;
            info_det.IdSucursal = info_det.IdSucursal;
            info_det.IdVendedor = info_det.IdVendedor;

            list.Add(info_det);
        }

        public void UpdateRow(fa_cliente_x_fa_Vendedor_x_sucursal_Info info_det, decimal IdTransaccionSession)
        {
            fa_cliente_x_fa_Vendedor_x_sucursal_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdCliente == info_det.IdCliente).First();
            info_det.IdEmpresa = info_det.IdEmpresa;
            info_det.IdSucursal = info_det.IdSucursal;
            edited_info.IdVendedor = info_det.IdVendedor;

        }

        public void DeleteRow(decimal IdCliente, decimal IdTransaccionSession)
        {
            List<fa_cliente_x_fa_Vendedor_x_sucursal_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.IdCliente == IdCliente).First());
        }
    }

    public class fa_cliente_List
    {
        string Variable = "fa_cliente_Info";
        public List<fa_cliente_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_cliente_Info> list = new List<fa_cliente_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_cliente_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_cliente_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
    #endregion
}