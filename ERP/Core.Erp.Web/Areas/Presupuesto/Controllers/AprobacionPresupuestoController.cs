using Core.Erp.Bus.General;
using Core.Erp.Bus.Presupuesto;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Presupuesto;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Presupuesto
{
    public class AprobacionPresupuestoController : Controller
    {
        // GET: Presupuesto/AprobacionPresupuesto
        #region Variables
        pre_Presupuesto_Bus bus_Presupuesto = new pre_Presupuesto_Bus();
        pre_PresupuestoDet_Bus bus_PresupuestoDet = new pre_PresupuestoDet_Bus();
        tb_sucursal_Bus bus_Sucursal = new tb_sucursal_Bus();
        pre_rubro_Bus bus_Rubro = new pre_rubro_Bus();
        pre_Periodo_Bus bus_Periodo = new pre_Periodo_Bus();
        pre_PresupuestoDet_List Lista_PresupuestoDet = new pre_PresupuestoDet_List();
        List<pre_Presupuesto_Info> lst_Presupuesto = new List<pre_Presupuesto_Info>();
        string mensaje = string.Empty;
        #endregion

        #region Index
        public ActionResult Index()
        {
            var info_periodo = bus_Periodo.GetInfo_UltimoPeriodoAbierto(Convert.ToInt32(SessionFixed.IdEmpresa));

            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal),
                IdPeriodo = info_periodo.IdPeriodo
            };

            cargar_filtros(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            model.IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            cargar_filtros(model.IdEmpresa);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_AprobacionPresupuesto(int IdSucursal = 0, decimal IdPeriodo = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdSucursal = IdSucursal == 0 ? 0 : Convert.ToInt32(IdSucursal);
            ViewBag.IdPeriodo = IdPeriodo == 0 ? 0 : Convert.ToInt32(IdPeriodo);

        //    lst_Presupuesto
                var model = bus_Presupuesto.GetList(IdEmpresa, IdSucursal, IdPeriodo, false);
            return PartialView("_GridViewPartial_AprobacionPresupuesto", model);
        }

        #region Metodos
        private void cargar_filtros(int IdEmpresa)
        {
            try
            {
                var lst_Sucursal = bus_Sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
                ViewBag.lst_Sucursal = lst_Sucursal;

                var lst_Periodo = bus_Periodo.GetList(IdEmpresa, false, false);

                lst_Periodo.Add(new Info.Presupuesto.pre_Periodo_Info
                {
                    IdPeriodo = 0,
                    DescripcionPeriodo = "Todos"
                });

                ViewBag.lst_Periodo = lst_Periodo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void cargar_combos(int IdEmpresa)
        {
            try
            {
                var lst_Sucursal = bus_Sucursal.get_list(IdEmpresa, false);
                ViewBag.lst_Sucursal = lst_Sucursal;

                var lst_Periodo = bus_Periodo.GetList(IdEmpresa, false, false);
                ViewBag.lst_Periodo = lst_Periodo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #endregion


        #region Acciones
        public ActionResult Aprobar(int IdEmpresa = 0, decimal IdPresupuesto = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            pre_Presupuesto_Info model = bus_Presupuesto.GetInfo(IdEmpresa, Convert.ToInt32(IdPresupuesto));
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.ListaPresupuestoDet = bus_PresupuestoDet.GetList(model.IdEmpresa, Convert.ToInt32(model.IdPresupuesto));
            Lista_PresupuestoDet.set_list(model.ListaPresupuestoDet, model.IdTransaccionSession);

            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Aprobar(pre_Presupuesto_Info model)
        {
            model.IdUsuarioAprobacion = Session["IdUsuario"].ToString(); 

            if (!bus_Presupuesto.AprobarBD(model))
            {
                ViewBag.mensaje = "No se ha podido aprobar el registro";

                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                //model.ListaPresupuestoDet = bus_PresupuestoDet.GetList(model.IdEmpresa, Convert.ToInt32(model.IdGrupo));
                Lista_PresupuestoDet.set_list(model.ListaPresupuestoDet, model.IdTransaccionSession);

                cargar_combos(model.IdEmpresa);
                return View(model);
            };
            return RedirectToAction("Index");
        }
        #endregion

        #region Metodos del detalle
        public ActionResult GridViewPartial_AprobacionPresupuestoDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_PresupuestoDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AprobacionPresupuestoDet", model);
        }
        #endregion        
    }

    public class pre_PresupuestoDet_List
    {
        string Variable = "pre_PresupuestoDet_Info";
        public List<pre_PresupuestoDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<pre_PresupuestoDet_Info> list = new List<pre_PresupuestoDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<pre_PresupuestoDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<pre_PresupuestoDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}