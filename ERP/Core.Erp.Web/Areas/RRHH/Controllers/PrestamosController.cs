using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.RRHH;
using DevExpress.Web.Mvc;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.Contabilidad;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class PrestamosController : Controller
    {
        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_prestamos()
        {
            ro_prestamo_Info model = new ro_prestamo_Info();
            return PartialView("_CmbEmpleado_prestamos", model);
        }
        public ActionResult CmbEmpleado_autoriza()
        {
            ro_prestamo_Info model = new ro_prestamo_Info();
            return PartialView("_CmbEmpleado_autoriza", model);
        }
        
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }

        public ActionResult CmbRubro_Prestamos()
        {
            ro_prestamo_Info model = new ro_prestamo_Info();
            return PartialView("_CmbRubro_Prestamos", model);
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

        #region Variables
        ro_prestamo_Bus bus_prestamos = new ro_prestamo_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        ro_prestamo_detalle_Bus bus_detalle = new ro_prestamo_detalle_Bus();
        ro_prestamo_detalle_lst Lis_ro_prestamo_detalle_lst = new ro_prestamo_detalle_lst();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        int IdEmpresa = 0;
        ro_prestamo_Info info = new ro_prestamo_Info();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        #endregion

        #region vistas
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdEmpleado = 0
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);
        }

       
        [ValidateInput(false)]
        public ActionResult GridViewPartial_prestamos(decimal IdEmpleado=0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdEmpleado = IdEmpleado == 0 ? 0 : IdEmpleado;
            List<ro_prestamo_Info> model = bus_prestamos.get_list_prestamo(IdEmpresa, IdEmpleado);
            return PartialView("_GridViewPartial_prestamos", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_prestamos_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lis_ro_prestamo_detalle_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_prestamos_det", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_prestamos_abono_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lis_ro_prestamo_detalle_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_prestamos_abono_det", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_prestamo_Info model = new ro_prestamo_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),

                IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]),
                Fecha = DateTime.Now,
                Fecha_PriPago = DateTime.Now,
                descuento_mensual = true
            };
            model.lst_detalle = new List<ro_prestamo_detalle_Info>();
            Lis_ro_prestamo_detalle_lst.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos();
            cargar_combos_detalle();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ro_prestamo_Info model)
        {
            model.lst_detalle = Lis_ro_prestamo_detalle_lst.get_list(model.IdTransaccionSession);
            if (!validar(model))
            {
                cargar_combos();
                return View(model);
            }
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle del prestamo";
                cargar_combos();
                return View(model);
            }
            decimal diferencia = Convert.ToDecimal((model.MontoSol - model.lst_detalle.Sum(v => v.TotalCuota)));

            if (Convert.ToInt32(diferencia) != 0)
            {
                ViewBag.mensaje = "Monto del prestamo no coincide con la suma del detalle";
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuario = Session["IdUsuario"].ToString();
            foreach (var item in model.lst_detalle)
            {
                item.TotalCuota = Math.Round(item.TotalCuota, 2);
            }
            if (!bus_prestamos.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpleado = model.IdEmpleado, IdPrestamo = model.IdPrestamo, Exito = true });
        }

        public ActionResult Modificar(decimal IdEmpleado, decimal IdPrestamo, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_prestamo_Info model = bus_prestamos.get_info(IdEmpresa, IdEmpleado, IdPrestamo);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            Lis_ro_prestamo_detalle_lst.set_list(model.lst_detalle, model.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.RRHH, 0, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ro_prestamo_Info model)
        {
            model.lst_detalle = Lis_ro_prestamo_detalle_lst.get_list(model.IdTransaccionSession);
            model.IdUsuarioUltMod = Session["IdUsuario"].ToString();
            if (model.lst_detalle == null || model.lst_detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle del prestamo";
                cargar_combos();
                return View(model);
            }
            decimal diferencia = Convert.ToDecimal((model.MontoSol - model.lst_detalle.Sum(v => v.TotalCuota)));
           
            if (Convert.ToInt32(diferencia) != 0)
            {
                ViewBag.mensaje = "Monto del prestamo no coincide con la suma del detalle";
                cargar_combos();
                return View(model);
            }
            if (!validar(model))
            {
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            foreach (var item in model.lst_detalle)
            {
                item.TotalCuota = Math.Round(item.TotalCuota, 2);
            }
            if (!bus_prestamos.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpleado = model.IdEmpleado, IdPrestamo = model.IdPrestamo, Exito = true });
        }

        public ActionResult Anular(decimal IdEmpleado, decimal IdPrestamo)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_prestamo_Info model = bus_prestamos.get_info(IdEmpresa, IdEmpleado, IdPrestamo);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdPrestamo);
            Lis_ro_prestamo_detalle_lst.set_list(model.lst_detalle,model.IdTransaccionSession);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.RRHH, 0, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_prestamo_Info model)
        {
            model.lst_detalle = Lis_ro_prestamo_detalle_lst.get_list(model.IdTransaccionSession);

            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuarioUltAnu = Session["IdUsuario"].ToString();
            model.Fecha_UltAnu = DateTime.Now;

            if (!bus_prestamos.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Abono(decimal IdEmpleado, decimal IdPrestamo)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ro_prestamo_Info model = bus_prestamos.get_info(IdEmpresa, IdEmpleado, IdPrestamo);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdPrestamo);
            if(model.lst_detalle.Count()>0)
            model.Valor_pendiente = model.lst_detalle.Where(v=> v.EstadoPago=="PEN").Sum(v=>v.TotalCuota);
            Lis_ro_prestamo_detalle_lst.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Abono(ro_prestamo_Info model)
        {
            model.lst_detalle = Lis_ro_prestamo_detalle_lst.get_list(model.IdTransaccionSession);

            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuarioUltAnu = Session["IdUsuario"].ToString();
            model.Fecha_UltAnu = DateTime.Now;

            if (!bus_prestamos.Abono(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region funciones del detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ro_prestamo_detalle_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_ro_prestamo_detalle_lst.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lis_ro_prestamo_detalle_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_prestamos_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_prestamo_detalle_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_ro_prestamo_detalle_lst.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lis_ro_prestamo_detalle_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_prestamos_det", model);
        }

        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_prestamo_detalle_Info info_det )
        {
            Lis_ro_prestamo_detalle_lst.DeleteRow(info_det.NumCuota, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lis_ro_prestamo_detalle_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_prestamos_det", model);
        }


        #endregion

        #region funciones del detalle Abono
        [HttpPost, ValidateInput(false)]
        public ActionResult AbonoEditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_prestamo_detalle_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_ro_prestamo_detalle_lst.AbonoUpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lis_ro_prestamo_detalle_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_prestamos_abono_det", model);
        }
        #endregion

        #region cargar combo validaciones
        private void cargar_combos()
        {
            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ViewBag.lst_rubro = bus_rubro.get_list_rub_concepto(IdEmpresa);
        }

        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ViewBag.lst_tipo_nomina = bus_nomina_tipo.get_list(IdEmpresa, false).Where(v=>v.IdNomina_Tipo==1).ToList();
            ViewBag.lst_catalogo = bus_catalogo.get_list_x_tipo(16, false);
        }

        private bool validar(ro_prestamo_Info model)
        {
            bool bandera = true;
            if (model.descuento_quincena && model.Fecha_PriPago.Day > 15)
            {
                ViewBag.mensaje = "La fecha del primer pago debe estar entre el [01 al 15] de cada mes";
                bandera = false;
            }

            if (model.descuento_men_quin && model.Fecha_PriPago.Day != 15 && model.descuento_men_quin && model.Fecha_PriPago.Day != 30)
            {
                ViewBag.mensaje = "La fecha del primer pago debe ser [15 o 30] de cada mes";
                bandera = false;

            }
            if (model.MontoSol == 0 | model.MontoSol < 0)
            {
                ViewBag.mensaje = "El monto del prestamo debe ser mayor a cero";
                bandera = false;

            }
            if (model.NumCuotas == 0 | model.NumCuotas < 0)
            {
                ViewBag.mensaje = "El n'umerode cuota debe ser mayor a cero";
                bandera = false;

            }
            return bandera;
        }
        #endregion

        #region json
        public ActionResult GenerarPrestamo(double MontoSol, DateTime Fecha_PriPago, int NumCuotas = 0, bool descuento_mensual = false, bool descuento_quincena = false, bool descuento_men_quin = false)
        {


            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"].ToString());
            info.IdEmpresa = IdEmpresa;
            info.MontoSol = MontoSol;
            info.NumCuotas = NumCuotas;
            info.Fecha_PriPago = Fecha_PriPago;
            info.descuento_mensual = descuento_mensual;
            info.descuento_men_quin = descuento_men_quin;
            info.descuento_quincena = descuento_quincena;
            info = bus_prestamos.get_calculo_prestamo(info);
            Lis_ro_prestamo_detalle_lst.set_list(info.lst_detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AplicarAbono(int IdEmpresa=0, double Valor_pendiente = 0, double Monto_aplicado = 0, decimal IdTransaccionSession=0)
        {


            info.IdEmpresa = IdEmpresa;
            info.Valor_pendiente = Valor_pendiente;
           
            var detalle=   Lis_ro_prestamo_detalle_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            foreach (var item in detalle)
            {
              if(Monto_aplicado >= item.TotalCuota)
                {
                    if(item.EstadoPago == "PEN")
                    {
                        item.ValorAplicado = item.TotalCuota;
                        Monto_aplicado = Monto_aplicado - item.TotalCuota;

                        if (item.TotalCuota == item.ValorAplicado)
                        {
                            item.EstadoPago = "CAN";
                        }
                            
                    }                                            
                }
              else
                {

                    item.ValorAplicado = Monto_aplicado;

                    break;
                }
            }
            Lis_ro_prestamo_detalle_lst.set_list(detalle, IdTransaccionSession);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        #endregion
    }

    public class ro_prestamo_detalle_lst
    {
        string variable="ro_prestamo_detalle_Info";
        public List<ro_prestamo_detalle_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable+ IdTransaccionSession.ToString()] == null)
            {
                List<ro_prestamo_detalle_Info> list = new List<ro_prestamo_detalle_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_prestamo_detalle_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_prestamo_detalle_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ro_prestamo_detalle_Info info_det, decimal IdTransaccionSession)
        {
            List<ro_prestamo_detalle_Info> list = get_list(IdTransaccionSession);
            info_det.NumCuota = list.Count == 0 ? 1 : list.Max(q => q.NumCuota) + 1;
            list.Add(info_det);
        }

        public void UpdateRow(ro_prestamo_detalle_Info info_det, decimal IdTransaccionSession)
        {
            ro_prestamo_detalle_Info edited_info = get_list(IdTransaccionSession).Where(m => m.NumCuota == info_det.NumCuota).First();
            edited_info.IdNominaTipoLiqui = info_det.IdNominaTipoLiqui;
            edited_info.FechaPago = info_det.FechaPago;
            edited_info.TotalCuota = info_det.TotalCuota;
            edited_info.EstadoPago = "PEN";
        }

        public void DeleteRow(int NumCuota,decimal IdTransaccionSession)
        {
            List<ro_prestamo_detalle_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.NumCuota == NumCuota).First());
        }

        public void AbonoUpdateRow(ro_prestamo_detalle_Info info_det, decimal IdTransaccionSession)
        {
            ro_prestamo_detalle_Info edited_info = get_list(IdTransaccionSession).Where(m => m.NumCuota == info_det.NumCuota).First();
            edited_info.EstadoPago = info_det.EstadoPago;
        }
    }

}
