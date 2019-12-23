using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class AjusteImpuestoRentaController : Controller
    {
        #region variables
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        ct_anio_fiscal_Bus bus_anio = new ct_anio_fiscal_Bus();
        ro_AjusteImpuestoRenta_Bus bus_ajuste_ir = new ro_AjusteImpuestoRenta_Bus();
        ro_AjusteImpuestoRentaDet_Bus bus_ajuste_ir_det = new ro_AjusteImpuestoRentaDet_Bus();
        ro_AjusteImpuestoRenta_List Lista_Ajuste = new ro_AjusteImpuestoRenta_List();
        ro_AjusteImpuestoRentaDet_List Lista_Det = new ro_AjusteImpuestoRentaDet_List();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Combo bajo demanda
        public ActionResult CmbEmpleado()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado", model);
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

        #region Index
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cl_filtros_Info model = new cl_filtros_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdAnio = DateTime.Now.Year
            };
            var lista = bus_ajuste_ir.get_list(model.IdEmpresa, model.IdAnio, true);
            Lista_Ajuste.set_list(lista, model.IdTransaccionSession);

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            var lista = bus_ajuste_ir.get_list(model.IdEmpresa, model.IdAnio, true);
            Lista_Ajuste.set_list(lista, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_ajuste()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<ro_AjusteImpuestoRenta_Info> model = Lista_Ajuste.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ajuste", model);

        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            lst_sucursal.Add(new tb_sucursal_Info { IdSucursal = 0, Su_Descripcion = "" });
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_anio = bus_anio.get_list(false);
            ViewBag.lst_anio = lst_anio;
        }
        private string Validar(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                string mensaje = "";
                if (info.lst_det.Count == 0)
                {
                    mensaje = "El detalle debe de tener al menos un registro";
                }

                return mensaje;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Detalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ajuste_det(int IdEmpresa = 0, int IdAjuste=0)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAjuste = IdAjuste;

            //var model = Lista_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = bus_ajuste_ir_det.GetList(IdEmpresa, IdAjuste);
            return PartialView("_GridViewPartial_ajuste_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ro_AjusteImpuestoRentaDet_Info info_det)
        {
            if (ModelState.IsValid)
                Lista_Det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_AjusteImpuestoRenta_Info model = new ro_AjusteImpuestoRenta_Info();
            model.lst_det = Lista_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ajuste_det", model.lst_det);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_AjusteImpuestoRentaDet_Info info_det)
        {
            if (ModelState.IsValid)
                Lista_Det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_AjusteImpuestoRenta_Info model = new ro_AjusteImpuestoRenta_Info();
            model.lst_det = Lista_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ajuste_det", model.lst_det);
        }

        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_AjusteImpuestoRentaDet_Info info_det)
        {
            Lista_Det.DeleteRow(info_det.Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_AjusteImpuestoRenta_Info model = new ro_AjusteImpuestoRenta_Info();
            model.lst_det = Lista_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ajuste_det", model.lst_det);
        }
        #endregion

        #region Json
        public JsonResult Reprocesar(string IdString = "")
        {
            string resultado = string.Empty;
            int IdEmpresa = Convert.ToInt32(IdString.Substring(0, 3));
            int IdAjuste = Convert.ToInt32(IdString.Substring(3, 6));
            int Secuencia = Convert.ToInt32(IdString.Substring(9, 6));
            int IdEmpleado = Convert.ToInt32(IdString.Substring(15, 6));

            var model = bus_ajuste_ir.get_info(IdEmpresa, IdAjuste);
            model = (model==null ? new ro_AjusteImpuestoRenta_Info() : model);
            model.IdEmpleado = IdEmpleado;
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_ajuste_ir.ProcesarDB(model))
            {
                resultado = "No se puede guardar el registro";
                ViewBag.mensaje = resultado;
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion

                cargar_combos();
                ro_AjusteImpuestoRenta_Info info = new ro_AjusteImpuestoRenta_Info
                {
                    IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                    IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                    IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                    IdAnio = DateTime.Now.Year,
                    Fecha = DateTime.Now,
                    FechaCorte = DateTime.Now,
                    lst_det = new List<ro_AjusteImpuestoRentaDet_Info>()
                };

                Lista_Det.set_list(info.lst_det, info.IdTransaccionSession);
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Nuevo(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                info.lst_det = Lista_Det.get_list(info.IdTransaccionSession);
                info.IdUsuarioCreacion = SessionFixed.IdUsuario;
                info.IdUsuario = SessionFixed.IdUsuario;

                if (!bus_ajuste_ir.ProcesarDB(info))
                {
                    cargar_combos();
                    return View(info);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult Modificar(int IdEmpresa=0, int IdAjuste = 0, bool Exito = false)
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion
                cargar_combos();
                ro_AjusteImpuestoRenta_Info info = new ro_AjusteImpuestoRenta_Info();
                info = bus_ajuste_ir.get_info(IdEmpresa, IdAjuste);
                info.IdSucursal = (info.IdSucursal == null ? 0 : info.IdSucursal);
                info.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                info.lst_det = bus_ajuste_ir_det.GetList(info.IdEmpresa, info.IdAjuste);
                Lista_Det.set_list(info.lst_det, info.IdTransaccionSession);

                if (Exito)
                    ViewBag.MensajeSuccess = MensajeSuccess;

                return View(info);

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                info.IdUsuario = SessionFixed.IdUsuario;
                info.IdUsuarioModificacion = SessionFixed.IdUsuario;
                info.lst_det = Lista_Det.get_list(info.IdTransaccionSession);

                if (!bus_ajuste_ir.ProcesarDB(info))
                {
                    cargar_combos();
                    return View(info);
                }

                return RedirectToAction("Modificar", "AjusteImpuestoRenta", new { IdEmpresa = info.IdEmpresa, IdAjuste = info.IdAjuste,  Exito=true });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult Procesar(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                info.IdUsuarioModificacion = SessionFixed.IdUsuario;
                info.lst_det = Lista_Det.get_list(info.IdTransaccionSession);

                if (!bus_ajuste_ir.ProcesarDB(info))
                {
                    cargar_combos();
                    return View(info);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult Anular(int IdEmpresa=0,int IdAjuste = 0)
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion
                cargar_combos();
                ro_AjusteImpuestoRenta_Info info = new ro_AjusteImpuestoRenta_Info();
                info = bus_ajuste_ir.get_info(IdEmpresa, IdAjuste);

                info.IdSucursal = (info.IdSucursal == null ? 0 : info.IdSucursal);

                info.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                info.lst_det = bus_ajuste_ir_det.GetList(info.IdEmpresa, info.IdAjuste);
                Lista_Det.set_list(info.lst_det, info.IdTransaccionSession);

                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                info.IdUsuarioAnulacion = SessionFixed.IdUsuario;

                if (!bus_ajuste_ir.anularDB(info))
                {
                    cargar_combos();
                    return View(info);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }

    public class ro_AjusteImpuestoRenta_List
    {
        string Variable = "ro_AjusteImpuestoRenta_Info";
        public List<ro_AjusteImpuestoRenta_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_AjusteImpuestoRenta_Info> list = new List<ro_AjusteImpuestoRenta_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_AjusteImpuestoRenta_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_AjusteImpuestoRenta_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class ro_AjusteImpuestoRentaDet_List
    {
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        string Variable = "ro_AjusteImpuestoRentaDet_Info";
        public List<ro_AjusteImpuestoRentaDet_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_AjusteImpuestoRentaDet_Info> list = new List<ro_AjusteImpuestoRentaDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_AjusteImpuestoRentaDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_AjusteImpuestoRentaDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ro_AjusteImpuestoRentaDet_Info info_det, decimal IdTransaccionSession)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ro_AjusteImpuestoRentaDet_Info> list = get_list(IdTransaccionSession);
            if (list.Where(q=> q.IdEmpleado == info_det.IdEmpleado).Count()==0)
            {
                var info_empleado = bus_empleado.get_info(IdEmpresa, info_det.IdEmpleado);
                info_det.pe_nombreCompleto = info_empleado.pe_apellido+" " + info_empleado.pe_nombre;
                info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
                var IdString = IdEmpresa.ToString("000") + info_det.IdAjuste.ToString("000000") + info_det.Secuencia.ToString("000000") + info_det.IdEmpleado.ToString("000000");
                info_det.IdString = IdString;
                list.Add(info_det);
            }

            
        }

        public void UpdateRow(ro_AjusteImpuestoRentaDet_Info info_det, decimal IdTransaccionSession)
        {
            ro_AjusteImpuestoRentaDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            //edited_info.IdNovedad = info_det.IdNovedad;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ro_AjusteImpuestoRentaDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }
}