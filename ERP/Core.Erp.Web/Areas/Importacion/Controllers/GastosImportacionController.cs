using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Importacion;
using Core.Erp.Info.Importacion;
using Core.Erp.Bus.Contabilidad;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Contabilidad;

namespace Core.Erp.Web.Areas.Importacion.Controllers
{
    [SessionTimeout]
    public class GastosImportacionController : Controller
    {
        #region Variables

        imp_gasto_Bus bus_gasto = new imp_gasto_Bus();
        imp_gasto_x_ct_plancta_Bus bus_gasto_ct = new imp_gasto_x_ct_plancta_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda

        public ActionResult CmbCuenta_Gasto()
        {
            imp_gasto_Info model = new imp_gasto_Info
            {
                info_gasto_cta = new imp_gasto_x_ct_plancta_Info()

            };
            return PartialView("_CmbCuenta_Gasto", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), true);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Index /  Metodos
        public ActionResult Index()
        {
            ViewBag.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_gastos_imp()
        {
            var model = bus_gasto.get_list();
            return PartialView("_GridViewPartial_gastos_imp", model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_ctacble = bus_plancta.get_list(IdEmpresa, false, false);
            ViewBag.lst_cuentas = lst_ctacble;
        }

        #endregion

        #region Acciones

        public ActionResult Nuevo(int IdGasto_tipo = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            imp_gasto_Info model = new imp_gasto_Info
            {
                info_gasto_cta = new imp_gasto_x_ct_plancta_Info()
                {
                    IdEmpresa = IdEmpresa,
                    IdGasto_tipo = IdGasto_tipo
                }


            };
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(imp_gasto_Info model)
        {
            if (bus_gasto.guardarDB(model))
            {
                model.info_gasto_cta.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                model.info_gasto_cta.IdGasto_tipo = model.IdGasto_tipo;
                model.info_gasto_cta.IdCtaCble = model.IdCtaCble;
                bus_gasto_ct.guardarDB(model.info_gasto_cta);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar( int IdGasto_tipo = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            imp_gasto_Info model = bus_gasto.get_info(IdGasto_tipo);
            if (model == null)
                return RedirectToAction("Index");
            model.info_gasto_cta = bus_gasto_ct.get_info(IdEmpresa, IdGasto_tipo);
            if (model.info_gasto_cta == null)
                model.info_gasto_cta = new imp_gasto_x_ct_plancta_Info
                {
                    IdEmpresa = IdEmpresa,
                    IdGasto_tipo = model.IdGasto_tipo
                };
            else
                model.IdCtaCble = model.info_gasto_cta.IdCtaCble;
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(imp_gasto_Info model)
        {
            model.info_gasto_cta = new imp_gasto_x_ct_plancta_Info { IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa) };
            if (!bus_gasto.modificarDB(model))
            {
                cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular( int IdGasto_tipo = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            imp_gasto_Info model = bus_gasto.get_info(IdGasto_tipo);
            if (model == null)
                return RedirectToAction("Index");
            model.info_gasto_cta = bus_gasto_ct.get_info(IdGasto_tipo, IdEmpresa);
            if (model.info_gasto_cta == null)
                model.info_gasto_cta = new imp_gasto_x_ct_plancta_Info
                {
                    IdEmpresa = IdEmpresa,
                    IdGasto_tipo = model.IdGasto_tipo
                };
            else
                model.IdCtaCble = model.info_gasto_cta.IdCtaCble;
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(imp_gasto_Info model)
        {
            model.info_gasto_cta = new imp_gasto_x_ct_plancta_Info { IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa) };
            if (!bus_gasto.anularDB(model))
            {
                cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}