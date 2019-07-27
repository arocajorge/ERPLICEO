using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.Facturacion;
using Core.Erp.Info.Facturacion;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    [SessionTimeout]
    public class ClienteTipoController : Controller
    {
        #region Index
        fa_cliente_tipo_Bus bus_clientetipo = new fa_cliente_tipo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_clientetipo()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<fa_cliente_tipo_Info> model = bus_clientetipo.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_clientetipo", model);
        }

        #endregion
        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var lst_ctacble = bus_plancta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_ctacble;
        }

        #endregion
        #region Acciones

        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            fa_cliente_tipo_Info model = new fa_cliente_tipo_Info
            {
                IdEmpresa = IdEmpresa
            };
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_cliente_tipo_Info model)
        {
            model.IdUsuario = SessionFixed.IdEmpresa.ToString();
            if (!bus_clientetipo.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0 , int Idtipo_cliente = 0)
        {
            fa_cliente_tipo_Info model = bus_clientetipo.get_info(IdEmpresa, Idtipo_cliente);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_cliente_tipo_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdEmpresa.ToString();
            if (!bus_clientetipo.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0 , int Idtipo_cliente = 0)
        {
            fa_cliente_tipo_Info model = bus_clientetipo.get_info(IdEmpresa, Idtipo_cliente);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_cliente_tipo_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdEmpresa.ToString();
            if (!bus_clientetipo.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }

    public class fa_cliente_tipo_List
    {
        string Variable = "fa_cliente_tipo_Info";
        public List<fa_cliente_tipo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_cliente_tipo_Info> list = new List<fa_cliente_tipo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_cliente_tipo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_cliente_tipo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}