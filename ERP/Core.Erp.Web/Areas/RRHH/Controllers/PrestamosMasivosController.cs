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
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Bus.Contabilidad;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class PrestamosMasivosController : Controller
    {
        #region Variables
        ro_PrestamoMasivo_Bus bus_prestamo_masivo = new ro_PrestamoMasivo_Bus();
        ro_PrestamoMasivo_Det_Bus bus_prestamo_masivo_det = new ro_PrestamoMasivo_Det_Bus();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ro_prestamo_Info info = new ro_prestamo_Info();
        ro_PrestamoMasivo_Det_List ListaDetalle = new ro_PrestamoMasivo_Det_List();
        ro_empleado_info_list empleado_info_list = new ro_empleado_info_list();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        string mensaje = string.Empty;
        #endregion

        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_PrestamosMasivos()
        {
            ro_PrestamoMasivo_Det_Info model = new ro_PrestamoMasivo_Det_Info();
            return PartialView("_CmbEmpleado_PrestamosMasivos", model);
        }

        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        #endregion

        #region Metodos bajo demanda Rubro
        public ActionResult CmbRubro_PrestamosMasivos()
        {
            ro_PrestamoMasivo_Det_Info model = new ro_PrestamoMasivo_Det_Info();
            return PartialView("_CmbRubro_PrestamosMasivos", model);
        }
        public List<ro_rubro_tipo_Info> get_list_bajo_demanda_rubro(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_rubro.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ro_rubro_tipo_Info get_info_bajo_demanda_rubro(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_rubro.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }
        #endregion

        #region metodo bajo demanda sucursal
        public ActionResult CmbSucursal_PrestamosMasivos()
        {
            ro_PrestamoMasivo_Info model = new ro_PrestamoMasivo_Info();
            return PartialView("_CmbSucursal_PrestamosMasivos", model);
        }
        public List<tb_sucursal_Info> get_list_bajo_demanda_sucursal(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_sucursal.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public tb_sucursal_Info get_info_bajo_demanda_sucursal(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_sucursal.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }
        #endregion

        #region vistas
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                fecha_ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                fecha_fin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);

        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_PrestamosMasivos(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;

            var model = bus_prestamo_masivo.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin, ViewBag.IdSucursal, true);
            return PartialView("_GridViewPartial_PrestamosMasivos", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_PrestamosMasivos_Det()
        {
            ro_PrestamoMasivo_Info model = new ro_PrestamoMasivo_Info();

            model.lst_detalle = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_PrestamosMasivos_Det", model.lst_detalle);
        }
        #endregion

        #region acciones
        public ActionResult Nuevo()
        {
            empleado_info_list.set_list(bus_empleado.get_list_combo(Convert.ToInt32(SessionFixed.IdEmpresa)));

            ro_PrestamoMasivo_Info model = new ro_PrestamoMasivo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                Fecha_PriPago = DateTime.Now,
                descuento_mensual = false,
                descuento_quincena = false,
                descuento_men_quin = false
            };
            model.lst_detalle = new List<ro_PrestamoMasivo_Det_Info>();
            ListaDetalle.set_list(model.lst_detalle, model.IdTransaccionSession);

            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ro_PrestamoMasivo_Info model)
        {
            model.lst_detalle = ListaDetalle.get_list(model.IdTransaccionSession);
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la carga masiva de préstamos";
                return View(model);
            }

            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_prestamo_masivo.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdSucursal, decimal IdCarga)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_PrestamoMasivo_Info model = bus_prestamo_masivo.get_info(IdEmpresa, IdSucursal, IdCarga);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_detalle = bus_prestamo_masivo_det.get_list(IdEmpresa, IdSucursal, IdCarga);
            ListaDetalle.set_list(model.lst_detalle, model.IdTransaccionSession);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha_PriPago, cl_enumeradores.eModulo.RRHH, 0, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_PrestamoMasivo_Info model)
        {
            model.lst_detalle = ListaDetalle.get_list(model.IdTransaccionSession);

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            model.Fecha_UltAnu = DateTime.Now;
            if (!bus_prestamo_masivo.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region funciones del detalle

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_PrestamoMasivo_Det_Info info_det)
        {
            if (ModelState.IsValid)
                ListaDetalle.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_PrestamoMasivo_Info model = new ro_PrestamoMasivo_Info();
            model.lst_detalle = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_PrestamosMasivos_Det", model.lst_detalle);
        }

        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_PrestamoMasivo_Det_Info info_det)
        {
            ListaDetalle.DeleteRow(info_det.Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_PrestamoMasivo_Info model = new ro_PrestamoMasivo_Info();
            model.lst_detalle = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_PrestamosMasivos_Det", model.lst_detalle);
        }
        #endregion


        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings_Prestamos.UploadValidationSettings, UploadControlSettings_Prestamos.FileUploadComplete);
            return null;
        }
    }

    public class UploadControlSettings_Prestamos
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            int cont = 0;
            ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
            ro_empleado_info_list empleado_info_list = new ro_empleado_info_list();
            ro_PrestamoMasivo_Det_List ListaDetalle_PrestamoMasivo = new ro_PrestamoMasivo_Det_List();
            List<ro_PrestamoMasivo_Det_Info> PrestamoMasivo_Det = new List<ro_PrestamoMasivo_Det_Info>();
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            int Secuencia = 1;
            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        if (cont != 0)
                        {
                            
                            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                            string cedula = reader.GetString(0);
                            var empleado = empleado_info_list.get_list().Where(v => v.pe_cedulaRuc == cedula).FirstOrDefault();
                            var CodRubro = Convert.ToString(reader.GetValue(2));
                            var info_rubro = bus_rubro.get_info_x_codigo(IdEmpresa, CodRubro);
                            if (empleado != null)
                            {
                                ro_PrestamoMasivo_Det_Info info = new ro_PrestamoMasivo_Det_Info
                                {
                                    Secuencia = Secuencia++,
                                    IdEmpleado = (empleado== null ? 0 : empleado.IdEmpleado),
                                    IdRubro = (info_rubro== null ? null : info_rubro.IdRubro),
                                    Monto = Convert.ToDouble(reader.GetValue(3)),
                                    NumCuotas= Convert.ToInt32(reader.GetValue(4)),
                                    ru_descripcion = (info_rubro == null ? "" : info_rubro.ru_descripcion),
                                    pe_nombreCompleto = (empleado == null ? "" : empleado.Empleado)
                                };
                                PrestamoMasivo_Det.Add(info);
                            }
                        }
                        cont++;

                    }

                }
                ListaDetalle_PrestamoMasivo.set_list(PrestamoMasivo_Det, IdTransaccionSession);
            }
        }
    }

    public class ro_PrestamoMasivo_Det_List
    {
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        public List<ro_PrestamoMasivo_Det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session["ro_PrestamoMasivo_Det_Info"] == null)
            {
                List<ro_PrestamoMasivo_Det_Info> list = new List<ro_PrestamoMasivo_Det_Info>();

                HttpContext.Current.Session["ro_PrestamoMasivo_Det_Info"] = list;
            }
            return (List<ro_PrestamoMasivo_Det_Info>)HttpContext.Current.Session["ro_PrestamoMasivo_Det_Info"];
        }

        public void set_list(List<ro_PrestamoMasivo_Det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session["ro_PrestamoMasivo_Det_Info"] = list;
        }

        public void UpdateRow(ro_PrestamoMasivo_Det_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_PrestamoMasivo_Det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            var empleado = bus_empleado.get_info(IdEmpresa, info_det.IdEmpleado);
            if (empleado != null)
                edited_info.pe_nombreCompleto = empleado.pe_apellido+' '+empleado.pe_nombre;

            var rubro = bus_rubro.get_info(IdEmpresa, info_det.IdRubro);
            if (rubro != null)
                edited_info.ru_descripcion = rubro.ru_descripcion;

            edited_info.IdEmpleado = info_det.IdEmpleado;
            edited_info.IdRubro = info_det.IdRubro;
            edited_info.NumCuotas = info_det.NumCuotas;
            edited_info.Monto = info_det.Monto;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ro_PrestamoMasivo_Det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }
}