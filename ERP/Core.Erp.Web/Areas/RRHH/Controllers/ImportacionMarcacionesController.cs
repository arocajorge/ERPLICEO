using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using DevExpress.DataAccess.Excel;
using DevExpress.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ImportacionMarcacionesController : Controller
    {
        #region Variables
        ro_marcaciones_x_empleado_Bus bus_marcaciones = new ro_marcaciones_x_empleado_Bus();
        ro_EmpleadoNovedadCargaMasiva_det_Bus bus_novedad_detalle_bus = new ro_EmpleadoNovedadCargaMasiva_det_Bus();
        ro_marcaciones_x_empleado_detLis_Info detalle = new ro_marcaciones_x_empleado_detLis_Info();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        List<ro_rubro_tipo_Info> lst_rubros = new List<ro_rubro_tipo_Info>();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ro_empleado_info_list empleado_info_list = new ro_empleado_info_list();
        int IdEmpresa = 0;
        #endregion


        #region Vistas
       

        [ValidateInput(false)]
        public ActionResult GridViewPartial_importacion_marcaciones_det()
        {
            List<ro_marcaciones_x_empleado_Info> model = new List<ro_marcaciones_x_empleado_Info>();
            model = detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            return PartialView("_GridViewPartial_importacion_marcaciones_det", model);
        }
        #endregion

        #region acciones
        public ActionResult Nuevo()
        {
            empleado_info_list.set_list(bus_empleado.get_list_combo(Convert.ToInt32(SessionFixed.IdEmpresa)));

            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_marcaciones_x_empleado_Info model = new ro_marcaciones_x_empleado_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                Fecha_Transac = DateTime.Now,
                IdNomina = 1,
                IdTransaccionSession=Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };
            model.detalle = new List<ro_marcaciones_x_empleado_Info>();
            detalle.set_list(model.detalle, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ro_marcaciones_x_empleado_Info model)
        {
            model.detalle = detalle.get_list(model.IdTransaccionSession);
            if (model.detalle == null || model.detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la novedad";
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_marcaciones.guardarDB(model.detalle, model.IdEmpresa))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index","MarcacionEmpleado");
        }

        #endregion

        #region cargar combos

        private void cargar_combos()
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.lst_sucursal = bus_sucursal.get_list(Convert.ToInt32(SessionFixed.IdEmpresa), false);

        }
        #endregion

        #region funciones del detalle

       
        #endregion


        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings_marcaciones.UploadValidationSettings, UploadControlSettings_marcaciones.FileUploadComplete_marcaciones);
            return null;
        }

    }
    public class UploadControlSettings_marcaciones
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete_marcaciones(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            int cont = 0;
            ro_empleado_info_list empleado_info_list = new ro_empleado_info_list();
            ro_marcaciones_x_empleado_detLis_Info EmpleadoNovedadCargaMasiva_detLis_Info = new ro_marcaciones_x_empleado_detLis_Info();
            List<ro_marcaciones_x_empleado_Info> lista_novedades = new List<ro_marcaciones_x_empleado_Info>();

            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                DateTime Fecha_registro;
                DateTime marcacion;
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        if (cont != 0)
                        {
                            string cedua = reader.GetString(0);
                            var empleado = empleado_info_list.get_list().Where(v => v.pe_cedulaRuc == cedua).FirstOrDefault();
                            if (empleado != null)
                            {
                                if (!reader.IsDBNull(2))// si tiene fehca de marcacion
                                {
                                    Fecha_registro =Convert.ToDateTime( reader.GetDateTime(2));
                                    if (!reader.IsDBNull(3))// si tiene entrada del primer turno
                                    {
                                        
                                        marcacion = Convert.ToDateTime(reader.GetValue(3));
                                        if (marcacion.Hour != 0)
                                        {
                                            ro_marcaciones_x_empleado_Info info = new ro_marcaciones_x_empleado_Info
                                            {
                                                IdEmpleado = empleado.IdEmpleado,
                                                IdEmpresa = empleado.IdEmpresa,
                                                es_fechaRegistro = Fecha_registro,

                                                IdCalendadrio = Convert.ToInt32(Fecha_registro.ToString("ddMMyyyy")),
                                                IdNomina = empleado.IdTipoNomina,
                                                IdUsuario = SessionFixed.IdUsuario,
                                                es_Hora = new TimeSpan(marcacion.Hour, marcacion.Minute, 0),
                                                IdTipoMarcaciones = cl_enumeradores.eTipoMarcacionRRHH.IN1.ToString(),
                                                pe_NombreCompleato = empleado.Empleado,
                                                pe_cedula = cedua,
                                                EstadoBool = true,
                                                IdRegistro = cont++

                                            };
                                            lista_novedades.Add(info);
                                        }
                                    }


                                    if (!reader.IsDBNull(4))// si tiene salida del primer turno
                                    {
                                        marcacion = Convert.ToDateTime(reader.GetValue(4));
                                        if (marcacion.Hour != 0)
                                        {
                                            ro_marcaciones_x_empleado_Info info = new ro_marcaciones_x_empleado_Info
                                            {
                                                IdEmpleado = empleado.IdEmpleado,
                                                IdEmpresa = empleado.IdEmpresa,
                                                es_fechaRegistro = Fecha_registro,
                                                IdCalendadrio = Convert.ToInt32(Fecha_registro.ToString("ddMMyyyy")),
                                                IdNomina = empleado.IdTipoNomina,
                                                IdUsuario = SessionFixed.IdUsuario,
                                                es_Hora = new TimeSpan(marcacion.Hour, marcacion.Minute, 0),
                                                IdTipoMarcaciones = cl_enumeradores.eTipoMarcacionRRHH.OUT1.ToString(),
                                                pe_NombreCompleato = empleado.Empleado,
                                                pe_cedula = cedua,
                                                EstadoBool = true,
                                                IdRegistro = cont++

                                    };
                                            lista_novedades.Add(info);
                                        }
                                    }

                                    if (!reader.IsDBNull(5))// si tiene entrada del segundo turno
                                    {
                                        marcacion = Convert.ToDateTime(reader.GetValue(5));
                                        if (marcacion.Hour != 0)
                                        {
                                            ro_marcaciones_x_empleado_Info info = new ro_marcaciones_x_empleado_Info
                                            {
                                                IdEmpleado = empleado.IdEmpleado,
                                                IdEmpresa = empleado.IdEmpresa,
                                                es_fechaRegistro = Fecha_registro,
                                                IdCalendadrio = Convert.ToInt32(Fecha_registro.ToString("ddMMyyyy")),
                                                IdNomina = empleado.IdTipoNomina,
                                                IdUsuario = SessionFixed.IdUsuario,
                                                es_Hora = new TimeSpan(marcacion.Hour, marcacion.Minute, 0),
                                                IdTipoMarcaciones = cl_enumeradores.eTipoMarcacionRRHH.IN2.ToString(),
                                                pe_NombreCompleato = empleado.Empleado,
                                                pe_cedula = cedua,
                                                EstadoBool = true,
                                                IdRegistro = cont++


                                        };
                                            lista_novedades.Add(info);
                                        }
                                    }


                                    if (reader.IsDBNull(6))// si tiene salida del segundo turno
                                    {
                                        marcacion = Convert.ToDateTime(reader.GetValue(6));
                                        if (marcacion.Hour != 0)
                                        {
                                            ro_marcaciones_x_empleado_Info info = new ro_marcaciones_x_empleado_Info
                                            {
                                                IdEmpleado = empleado.IdEmpleado,
                                                IdEmpresa = empleado.IdEmpresa,
                                                es_fechaRegistro = Fecha_registro,
                                                
                                                IdCalendadrio = Convert.ToInt32(Fecha_registro.ToString("ddMMyyyy")),
                                                IdNomina = empleado.IdTipoNomina,
                                                IdUsuario = SessionFixed.IdUsuario,
                                                es_Hora = new TimeSpan(marcacion.Hour, marcacion.Minute, 0),
                                                IdTipoMarcaciones = cl_enumeradores.eTipoMarcacionRRHH.OUT2.ToString(),
                                                pe_NombreCompleato = empleado.Empleado,
                                                pe_cedula = cedua,
                                                EstadoBool = true,
                                                IdRegistro = cont++


                                    };
                                            lista_novedades.Add(info);
                                        }
                                    }
                                }

                                
                            }
                        }
                        cont++;

                    }

                }
                EmpleadoNovedadCargaMasiva_detLis_Info.set_list(lista_novedades, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            }
        }


        
    }
    public class ro_marcaciones_x_empleado_detLis_Info
    {
        string variable = "ro_marcaciones_x_empleado_Info";
        public List<ro_marcaciones_x_empleado_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable+ IdTransaccionSession.ToString()] == null)
            {
                List<ro_marcaciones_x_empleado_Info> list = new List<ro_marcaciones_x_empleado_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_marcaciones_x_empleado_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_marcaciones_x_empleado_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }
    }


}