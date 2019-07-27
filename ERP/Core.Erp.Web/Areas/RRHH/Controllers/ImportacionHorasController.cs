using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Data.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
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

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ImportacionHorasController : Controller
    {
        #region Variables
        ro_HorasProfesores_Bus bus_novedad = new ro_HorasProfesores_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        ro_HorasProfesores_det_Bus bus_novedad_detalle_bus = new ro_HorasProfesores_det_Bus();
        ro_HorasProfesores_detLis_Info detalle = new ro_HorasProfesores_detLis_Info();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_contrato_Bus bus_contrato = new ro_contrato_Bus();
        List<ro_rubro_tipo_Info> lst_rubros = new List<ro_rubro_tipo_Info>();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ro_empleado_info_list empleado_info_list = new ro_empleado_info_list();

        ro_rubro_tipo_Info_list ro_rubro_tipo_Info_list = new ro_rubro_tipo_Info_list();
        string mensaje = string.Empty;
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        int IdEmpresa = 0;
        #endregion

        #region Rubro bajo demanda


        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_imp_horas()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_imp_horas", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }



        public ActionResult CmbRubro_impor_horas()
        {
            ro_empleado_novedad_det_Info model = new ro_empleado_novedad_det_Info();
            return PartialView("_CmbRubro_impor_horas", model);
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
        #region Vistas
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);

        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_importacion_horas(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);

            var model = bus_novedad.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin, false);
            return PartialView("_GridViewPartial_importacion_horas", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_importacion_horas_det()
        {
            ro_HorasProfesores_Info modelReturn = new ro_HorasProfesores_Info();

            modelReturn.detalle = detalle.get_list();
            return PartialView("_GridViewPartial_importacion_horas_det", modelReturn);
        }
        #endregion

        #region acciones
        public ActionResult Nuevo()
        {

            var lst_rubros = bus_rubro.get_list(Convert.ToInt32(SessionFixed.IdEmpresa), false);
            ro_rubro_tipo_Info_list.set_list(lst_rubros);

            var lst_empleados = bus_empleado.get_list_profesores(Convert.ToInt32(SessionFixed.IdEmpresa));
            empleado_info_list.set_list(lst_empleados);

            ro_HorasProfesores_Info model = new ro_HorasProfesores_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                FechaCarga = DateTime.Now,
                IdNomina = 1,
                IdNominaTipo = 2,
            };
            model.detalle = new List<ro_HorasProfesores_det_Info>();
            detalle.set_list(model.detalle);
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ro_HorasProfesores_Info model)
        {



            model.detalle = detalle.get_list();
            if (model.detalle == null || model.detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la novedad";
                cargar_combos();
                return View(model);
            }



            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_novedad.GuardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(decimal IdCarga)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_HorasProfesores_Info model = bus_novedad.get_info(IdEmpresa, IdCarga);
            if (model == null)
                return RedirectToAction("Index");
            model.detalle = bus_novedad_detalle_bus.get_list(IdEmpresa, IdCarga);
            detalle.set_list(model.detalle);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.FechaCarga, cl_enumeradores.eModulo.RRHH, 0, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_HorasProfesores_Info model)
        {
            model.detalle = detalle.get_list();

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            model.Fecha_UltAnu = DateTime.Now;
            if (!bus_novedad.AnularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Duplicar(decimal IdCarga)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_HorasProfesores_Info model = bus_novedad.get_info(IdEmpresa, IdCarga);
            if (model == null)
                return RedirectToAction("Index");
            model.detalle = bus_novedad_detalle_bus.get_list(IdEmpresa, IdCarga);
            detalle.set_list(model.detalle);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Duplicar(ro_HorasProfesores_Info model)
        {
            model.detalle = detalle.get_list();

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuario = SessionFixed.IdUsuario;
            model.Fecha_Transac = DateTime.Now;
            if (!bus_novedad.GuardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region cargar combos

        private void cargar_combos()
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.lst_nomina = bus_nomina.get_list(IdEmpresa, false);
            ViewBag.lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, 1);
            ViewBag.lst_sucursal = bus_sucursal.get_list(Convert.ToInt32(SessionFixed.IdEmpresa), false);

            ViewBag.lst_rubro = bus_rubro.get_list(Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        #endregion

        #region funciones del detalle

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ro_HorasProfesores_det_Info info_det)
        {
            if (ModelState.IsValid)
                detalle.AddRow(info_det);
            ro_HorasProfesores_Info model = new ro_HorasProfesores_Info();
            model.detalle = detalle.get_list();
            return PartialView("_GridViewPartial_importacion_horas_det", model);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_HorasProfesores_det_Info info_det)
        {
            if (ModelState.IsValid)
                detalle.UpdateRow(info_det);
            ro_HorasProfesores_Info model = new ro_HorasProfesores_Info();
            model.detalle = detalle.get_list();
            return PartialView("_GridViewPartial_importacion_horas_det", model);
        }

        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_HorasProfesores_det_Info info_det)
        {
            detalle.DeleteRow(info_det.Secuencia);
            ro_HorasProfesores_Info model = new ro_HorasProfesores_Info();
            model.detalle = detalle.get_list();
            return PartialView("_GridViewPartial_importacion_horas_det", model);
        }
        #endregion


        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings_horas.UploadValidationSettings, UploadControlSettings_horas.FileUploadComplete);
            return null;
        }

    }
    public class UploadControlSettings_horas
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            ro_FormulaHorasRecargo_Bus busformulas = new ro_FormulaHorasRecargo_Bus();
            var formula_horas = busformulas.get_info(Convert.ToInt32(SessionFixed.IdEmpresa));
            if (formula_horas == null)
                return;
            int cont = 0;
            ro_empleado_info_list empleado_info_list = new ro_empleado_info_list();
            ro_HorasProfesores_detLis_Info EmpleadoNovedadCargaMasiva_detLis_Info = new ro_HorasProfesores_detLis_Info();
            List<ro_HorasProfesores_det_Info> lista_novedades = new List<ro_HorasProfesores_det_Info>();
            ro_rubros_calculados_Bus bus_rubros_calculados = new ro_rubros_calculados_Bus();
            var rubros_calculados = bus_rubros_calculados.get_info(Convert.ToInt32(SessionFixed.IdEmpresa));
            ro_rubro_tipo_Info_list ro_rubro_tipo_Info_list = new ro_rubro_tipo_Info_list();
            ro_jornada_Data odata_j = new ro_jornada_Data();

            var lst_jornada = odata_j.get_list(Convert.ToInt32(SessionFixed.IdEmpresa), false);
            double horas_mat = 0;
            string jornada = "";
            string IdRubro = "";
            string cedula = "";
            if (rubros_calculados == null)
                return;
            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont != 0)
                    {

                        cedula = reader.GetString(0);
                        IdRubro = Convert.ToString(reader.GetValue(3));

                        jornada = Convert.ToString(reader.GetValue(2));
                        ro_rubro_tipo_Info rubros = new ro_rubro_tipo_Info();
                        var empleado = empleado_info_list.get_list().Where(v => v.pe_cedulaRuc == cedula).FirstOrDefault();

                        if (empleado != null)
                        {
                            if (empleado.Valor_horas_matutino == null)
                                empleado.Valor_horas_matutino = 0;
                            if (empleado.Valor_horas_vespertina == null)
                                empleado.Valor_horas_vespertina = 0;
                            if (empleado.Valor_horas_brigada == null)
                                empleado.Valor_horas_brigada = 0;
                            if (empleado.Valor_hora_adicionales == null)
                                empleado.Valor_hora_adicionales = 0;
                            if (empleado.Valor_hora_control_salida == null)
                                empleado.Valor_hora_control_salida = 0;




                            rubros = ro_rubro_tipo_Info_list.get_list().FirstOrDefault(v => v.rub_codigo == IdRubro);
                            if (rubros != null)
                            {
                                ro_HorasProfesores_det_Info info = new ro_HorasProfesores_det_Info
                                {
                                    NumHoras = Convert.ToDouble(reader.GetValue(4)),
                                    pe_cedulaRuc = cedula,
                                    pe_apellido = empleado.Empleado,
                                    em_codigo = empleado.em_codigo,
                                    IdSucursal = empleado.IdSucursal,
                                    Secuencia = cont,
                                    IdEmpleado = empleado.IdEmpleado,
                                    IdRubro = rubros.IdRubro,
                                    ru_descripcion = rubros.ru_descripcion,

                                };

                                if(info.IdRubro=="63")
                                {

                                }
                                if(info.IdRubro==rubros_calculados.IdRubro_horas_matutina)
                                info.ValorHora = Convert.ToDouble(empleado.Valor_horas_matutino);

                                if (info.IdRubro == rubros_calculados.IdRubro_horas_vespertina)
                                    info.ValorHora = Convert.ToDouble(empleado.Valor_horas_vespertina);

                                if (info.IdRubro == rubros_calculados.IdRubro_horas_control_salida)
                                    info.ValorHora = Convert.ToDouble(empleado.Valor_hora_control_salida);


                                if (info.IdRubro == rubros_calculados.IdRubro_horas_brigadas)
                                    info.ValorHora = Convert.ToDouble(empleado.Valor_horas_brigada);

                                if (info.IdRubro == rubros_calculados.IdRubro_horas_adicionales)
                                    info.ValorHora = Convert.ToDouble(empleado.Valor_hora_adicionales);

                                horas_mat = info.NumHoras;
                                info.Valor = Convert.ToDouble(info.ValorHora * info.NumHoras);
                                info.Secuencia = lista_novedades.Count() + 1;
                                if (jornada != null && jornada != "")
                                    info.IdJornada = lst_jornada.Where(v => v.codigo == jornada).FirstOrDefault().IdJornada;
                                if (info.Valor > 0)
                                    lista_novedades.Add(info);
                            }





                        }


                    }
                    cont++;
                }


            }



            foreach (var item in empleado_info_list.get_list())
            {

                double mat_mas_ves = lista_novedades.Where(v => v.IdEmpleado == item.IdEmpleado && (v.IdRubro == rubros_calculados.IdRubro_horas_matutina || v.IdRubro == rubros_calculados.IdRubro_horas_vespertina)).Sum(v => v.NumHoras);

                #region horas recargo
                if ((mat_mas_ves) * (formula_horas.Dividendo / formula_horas.Divisor) > 0)
                {
                    if (rubros_calculados.IdRubro_horas_recargo != null)
                    {
                        var rubros = ro_rubro_tipo_Info_list.get_list().FirstOrDefault(v => v.IdRubro == rubros_calculados.IdRubro_horas_recargo);
                        if (rubros != null)
                        {
                            ro_HorasProfesores_det_Info info = new ro_HorasProfesores_det_Info
                            {
                                pe_cedulaRuc = item.pe_cedulaRuc,
                                pe_apellido = item.Empleado,
                                IdSucursal = item.IdSucursal,
                                em_codigo = item.em_codigo,
                                Secuencia = cont,
                                IdEmpleado = item.IdEmpleado,
                                IdRubro = rubros_calculados.IdRubro_horas_recargo,
                                ru_descripcion = rubros.ru_descripcion

                            };
                            info.ValorHora = 1.32;
                            info.NumHoras = (Math.Round(Convert.ToDouble((mat_mas_ves) * (formula_horas.Dividendo / formula_horas.Divisor))) - 160);
                            info.Valor = 1.32 * info.NumHoras;
                            info.Secuencia = lista_novedades.Count() + 1;
                            if (info.Valor > 0)
                                lista_novedades.Add(info);
                        }
                    }
                }
                #endregion












                EmpleadoNovedadCargaMasiva_detLis_Info.set_list(lista_novedades);
            }
        }



    }
    public class ro_HorasProfesores_detLis_Info
    {
        public List<ro_HorasProfesores_det_Info> get_list()
        {
            if (HttpContext.Current.Session["ro_HorasProfesores_det_Info"] == null)
            {
                List<ro_HorasProfesores_det_Info> list = new List<ro_HorasProfesores_det_Info>();

                HttpContext.Current.Session["ro_HorasProfesores_det_Info"] = list;
            }
            return (List<ro_HorasProfesores_det_Info>)HttpContext.Current.Session["ro_HorasProfesores_det_Info"];
        }

        public void set_list(List<ro_HorasProfesores_det_Info> list)
        {
            HttpContext.Current.Session["ro_HorasProfesores_det_Info"] = list;
        }

        public void AddRow(ro_HorasProfesores_det_Info info_det)
        {
            ro_rubro_tipo_Bus bus_rub = new ro_rubro_tipo_Bus();
            var info_rubro = bus_rub.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdRubro);
            ro_empleado_Bus bus_emppleado = new ro_empleado_Bus();
            ro_empleado_Info info_empleado = new ro_empleado_Info();
            info_empleado = bus_emppleado.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdEmpleado);

            List<ro_HorasProfesores_det_Info> list = get_list();
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.ru_descripcion = info_rubro.ru_descripcion;
            info_det.pe_apellido = info_empleado.pe_apellido + " " + info_empleado.pe_nombre;

            list.Add(info_det);
        }

        public void UpdateRow(ro_HorasProfesores_det_Info info_det)
        {

            ro_rubro_tipo_Bus bus_rub = new ro_rubro_tipo_Bus();
            var info_rubro = bus_rub.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdRubro);

            ro_empleado_Bus bus_emppleado = new ro_empleado_Bus();
            ro_empleado_Info info_empleado = new ro_empleado_Info();
            info_empleado = bus_emppleado.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdEmpleado);

            ro_HorasProfesores_det_Info edited_info = get_list().Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.NumHoras = info_det.NumHoras;
            edited_info.Valor = info_det.Valor;
            edited_info.ValorHora = info_det.ValorHora;
            edited_info.IdRubro = info_det.IdRubro;
            edited_info.IdEmpleado = info_empleado.IdEmpleado;
            edited_info.ru_descripcion = info_rubro.ru_descripcion;
            edited_info.pe_apellido = info_empleado.pe_apellido + " " + info_empleado.pe_nombre;
        }

        public void DeleteRow(int Secuencia)
        {
            List<ro_HorasProfesores_det_Info> list = get_list();
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

}