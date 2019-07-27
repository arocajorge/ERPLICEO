using Core.Erp.Bus.ActivoFijo;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.ActivoFijo;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.ActivoFijo.Controllers
{
    [SessionTimeout]

    public class TipoAFController : Controller
    {
        #region Variables
        Af_Activo_fijo_tipo_Bus bus_tipoactivo = new Af_Activo_fijo_tipo_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipoactivo()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_tipoactivo.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_tipoactivo", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            var lst_cuentas = bus_plancta.get_list(IdEmpresa, false, false);
            ViewBag.lst_cuentas = lst_cuentas;
        }
        #endregion
        #region Metodos ComboBox bajo demanda
        public ActionResult CmbCuenta_TipoAF1()
        {
            Af_Activo_fijo_tipo_Info model = new Af_Activo_fijo_tipo_Info();
            return PartialView("_CmbCuenta_TipoAF1", model);
        }
        public ActionResult CmbCuenta_TipoAF2()
        {
            Af_Activo_fijo_tipo_Info model = new Af_Activo_fijo_tipo_Info();
            return PartialView("_CmbCuenta_TipoAF2", model);
        }
        public ActionResult CmbCuenta_TipoAF3()
        {
            Af_Activo_fijo_tipo_Info model = new Af_Activo_fijo_tipo_Info();
            return PartialView("_CmbCuenta_TipoAF3", model);
        }
        public ActionResult CmbCuenta_TipoAF4()
        {
            Af_Activo_fijo_tipo_Info model = new Af_Activo_fijo_tipo_Info();
            return PartialView("_CmbCuenta_TipoAF4", model);
        }
        public ActionResult CmbCuenta_TipoAF5()
        {
            Af_Activo_fijo_tipo_Info model = new Af_Activo_fijo_tipo_Info();
            return PartialView("_CmbCuenta_TipoAF5", model);
        }
        public ActionResult CmbCuenta_TipoAF6()
        {
            Af_Activo_fijo_tipo_Info model = new Af_Activo_fijo_tipo_Info();
            return PartialView("_CmbCuenta_TipoAF6", model);
        }
        public ActionResult CmbCuenta_TipoAF7()
        {
            Af_Activo_fijo_tipo_Info model = new Af_Activo_fijo_tipo_Info();
            return PartialView("_CmbCuenta_TipoAF7", model);
        }


        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            Af_Activo_fijo_tipo_Info model = new Af_Activo_fijo_tipo_Info
            {
                IdEmpresa = IdEmpresa
            };
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(Af_Activo_fijo_tipo_Info model)
        {
            model.IdUsuario = Session["IdUsuario"].ToString();
            if (!bus_tipoactivo.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdActivoFijoTipo = 0)
        {            
            Af_Activo_fijo_tipo_Info model = bus_tipoactivo.get_info(IdEmpresa, IdActivoFijoTipo);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(Af_Activo_fijo_tipo_Info model)
        {
            model.IdUsuarioUltMod = Session["IdUsuario"].ToString();
            if (!bus_tipoactivo.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdActivoFijoTipo = 0)
        {
            Af_Activo_fijo_tipo_Info model = bus_tipoactivo.get_info(IdEmpresa, IdActivoFijoTipo);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(Af_Activo_fijo_tipo_Info model)
        {
            model.IdUsuarioUltAnu = Session["IdUsuario"].ToString();
            if (!bus_tipoactivo.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }

    public class Af_Activo_fijo_tipo_List
    {
        string Variable = "Af_Activo_fijo_tipo_Info";
        public List<Af_Activo_fijo_tipo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<Af_Activo_fijo_tipo_Info> list = new List<Af_Activo_fijo_tipo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<Af_Activo_fijo_tipo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<Af_Activo_fijo_tipo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}