using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Contabilidad;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class ImpuestoController : Controller
    {
        #region Variables
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        tb_sis_Impuesto_Tipo_Bus bus_impuesto_tipo = new tb_sis_Impuesto_Tipo_Bus();
        tb_sis_Impuesto_x_ctacble_Bus bus_impuesto_ctacble = new tb_sis_Impuesto_x_ctacble_Bus();

        #endregion
        #region Metodos ComboBox bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_Cta_Impuesto()
        {
            tb_sis_Impuesto_x_ctacble_Info model = new tb_sis_Impuesto_x_ctacble_Info();
            return PartialView("_CmbCuenta_Cta_Impuesto", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), true);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }

        public ActionResult CmbCuenta_Vta_Impuesto()
        {
            tb_sis_Impuesto_x_ctacble_Info model = new tb_sis_Impuesto_x_ctacble_Info();
            return PartialView("_CmbCuenta_Vta_Impuesto", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_vta(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), true);
        }
        public ct_plancta_Info get_info_bajo_demanda_vta(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_impuesto(string IdCod_Impuesto = "")
        {
            List<tb_sis_Impuesto_Info> model = bus_impuesto.get_list(IdCod_Impuesto, true);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            return PartialView("_GridViewPartial_impuesto", model);
        }
        private void cargar_combos()
        {
            var lst_impuesto_tipo = bus_impuesto_tipo.get_list();
            ViewBag.lst_tipo = lst_impuesto_tipo;

            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var lst_ctacble = bus_plancta.get_list(Convert.ToInt32(Session["IdEmpresa"]), false, false);
            ViewBag.lst_cuentas = lst_ctacble;
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo(string IdCod_Impuesto = "")
        {
            tb_sis_Impuesto_Info model = new tb_sis_Impuesto_Info
            {
                info_impuesto_ctacble = new tb_sis_Impuesto_x_ctacble_Info()
            };
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(tb_sis_Impuesto_Info model)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (bus_impuesto.validar_existe_IdCod_Impuesto(model.IdCod_Impuesto))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                cargar_combos();
                return View(model);
            }

            if (!bus_impuesto.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }

            model.info_impuesto_ctacble = model.info_impuesto_ctacble ?? new tb_sis_Impuesto_x_ctacble_Info();
            model.info_impuesto_ctacble.IdEmpresa_cta = IdEmpresa;
            model.info_impuesto_ctacble.IdCod_Impuesto = model.IdCod_Impuesto;
            model.info_impuesto_ctacble.IdCtaCble = model.IdCtaCble;
            model.info_impuesto_ctacble.IdCtaCble_vta = model.IdCtaCble_vta;
            bus_impuesto_ctacble.guardarDB(model.info_impuesto_ctacble);
            return RedirectToAction("Index");
        }

        public ActionResult Modificar( string IdCod_Impuesto = "")
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_sis_Impuesto_Info model = bus_impuesto.get_info(IdCod_Impuesto);
            if (model == null)
                return RedirectToAction("Index");
            model.info_impuesto_ctacble = new tb_sis_Impuesto_x_ctacble_Info();
            model.info_impuesto_ctacble = bus_impuesto_ctacble.get_info(IdCod_Impuesto, Convert.ToInt32(SessionFixed.IdEmpresa));
            if (model.info_impuesto_ctacble != null)
            {
                model.info_impuesto_ctacble.IdEmpresa_cta = IdEmpresa;
                model.info_impuesto_ctacble.IdCod_Impuesto = model.IdCod_Impuesto;
                model.IdCtaCble = model.info_impuesto_ctacble.IdCtaCble;
                model.IdCtaCble_vta = model.info_impuesto_ctacble.IdCtaCble_vta;
            }else
            {
                model.info_impuesto_ctacble = new tb_sis_Impuesto_x_ctacble_Info
                {
                    IdCod_Impuesto = model.IdCod_Impuesto,
                    IdEmpresa_cta = IdEmpresa
                };
            }
                       
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(tb_sis_Impuesto_Info model)
        {
            if (!bus_impuesto.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }

            bus_impuesto_ctacble.eliminarDB(model.IdCod_Impuesto, model.info_impuesto_ctacble.IdEmpresa_cta);
            model.info_impuesto_ctacble.IdCtaCble = model.IdCtaCble;
            model.info_impuesto_ctacble.IdCtaCble_vta = model.IdCtaCble_vta;

            bus_impuesto_ctacble.guardarDB(model.info_impuesto_ctacble);
            model.info_impuesto_ctacble.IdCtaCble = model.IdCtaCble;
            model.info_impuesto_ctacble.IdCtaCble_vta = model.IdCtaCble_vta;
            return RedirectToAction("Index");
        }

        public ActionResult Anular(string IdCod_Impuesto = "")
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_sis_Impuesto_Info model = bus_impuesto.get_info(IdCod_Impuesto);
            if (model == null)
                return RedirectToAction("Index");
            model.info_impuesto_ctacble = bus_impuesto_ctacble.get_info(IdCod_Impuesto, Convert.ToInt32(SessionFixed.IdEmpresa));
            if (model.info_impuesto_ctacble != null)
            {
                model.info_impuesto_ctacble.IdEmpresa_cta = IdEmpresa;
                model.info_impuesto_ctacble.IdCod_Impuesto = model.IdCod_Impuesto;
                model.IdCtaCble = model.info_impuesto_ctacble.IdCtaCble;
                model.IdCtaCble_vta = model.info_impuesto_ctacble.IdCtaCble_vta;
            }
            else
            {
                model.info_impuesto_ctacble = new tb_sis_Impuesto_x_ctacble_Info
                {
                    IdCod_Impuesto = model.IdCod_Impuesto,
                    IdEmpresa_cta = IdEmpresa
                };
            }
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_sis_Impuesto_Info model)
        {
            if (!bus_impuesto.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
                
            return RedirectToAction("Index");
        }

        #endregion
    }
}