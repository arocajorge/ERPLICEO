using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Info.SeguridadAcceso;
using Core.Erp.Bus.SeguridadAcceso;
using DevExpress.Web;
using Core.Erp.Bus.Inventario;
using Core.Erp.Bus.Contabilidad;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    public class SolicitudPagoController : Controller
    {
        #region Variables
        cp_SolicitudPago_Bus bus_solicitud = new cp_SolicitudPago_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        cp_SolicitudPagoDet_Bus bus_pago_Det = new cp_SolicitudPagoDet_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        seg_Menu_x_Empresa_x_Usuario_Bus bus_permisos = new seg_Menu_x_Empresa_x_Usuario_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion
        #region Index

        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combos_consulta();

            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "CuentasPorPagar", "SolicitudPago", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            cargar_combos_consulta();
            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "CuentasPorPagar", "SolicitudPago", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }
        private void cargar_combos_consulta()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            lst_sucursal.Add(new tb_sucursal_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = 0,
                Su_Descripcion = "TODAS"
            });
            ViewBag.lst_sucursal = lst_sucursal;
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            ViewBag.lst_sucursal = lst_sucursal;
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_solicitud_pago(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0, bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {

            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            bool EsContador = Convert.ToBoolean(SessionFixed.EsContador);
            ViewBag.IdSucursal = IdSucursal;
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);

            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;

            var model = bus_solicitud.GetList(IdEmpresa, IdSucursal, ViewBag.Fecha_ini, ViewBag.Fecha_fin, SessionFixed.IdUsuario,EsContador);
            return PartialView("_GridViewPartial_solicitud_pago", model);
        }
        #endregion
        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbProveedor_CXP()
        {
            decimal model = new decimal();
            return PartialView("_CmbProveedor_CXP", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        #endregion
        #region Metodos
        private bool validar(cp_SolicitudPago_Info i_validar, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.Fecha, cl_enumeradores.eModulo.CXP, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            return true;
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
            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "CuentasPorPagar", "SolicitudPago", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion

            cp_SolicitudPago_Info model = new cp_SolicitudPago_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                Fecha = DateTime.Now,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            seg_usuario_Info mod = bus_usuario.get_info(SessionFixed.IdUsuario);
            model.Solicitante = mod.Nombre;
            cargar_combos(model.IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cp_SolicitudPago_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;

            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_solicitud.GuardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSolicitud = model.IdSolicitud, Exito = true });

        }

        public ActionResult Modificar(int IdEmpresa = 0 , decimal IdSolicitud = 0, bool Exito = false)
        {
            int IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            cp_SolicitudPago_Info model = bus_solicitud.GetInfo(IdEmpresa, IdSolicitud);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);

            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "CuentasPorPagar", "SolicitudPago", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.CXP, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cp_SolicitudPago_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;

            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_solicitud.ModificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSolicitud = model.IdSolicitud, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdSolicitud = 0)
        {
            cp_SolicitudPago_Info model = bus_solicitud.GetInfo(IdEmpresa, IdSolicitud);
            if (model == null)
                return RedirectToAction("Index");

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.CXP, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            #region Permisos
            seg_Menu_x_Empresa_x_Usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.IdUsuario, "CuentasPorPagar", "SolicitudPago", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion

            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]

        public ActionResult Anular(cp_SolicitudPago_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_solicitud.AnularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}