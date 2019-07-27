using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.CuentasPorCobrar;
using Core.Erp.Bus.General;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorCobrar.Controllers
{
    [SessionTimeout]
    public class TipoCobroController : Controller
    {
        #region Variables

        cxc_cobro_tipo_Bus bus_tipocobro = new cxc_cobro_tipo_Bus();
        cxc_cobro_tipo_Param_conta_x_sucursal_Bus bus_tipo_param = new cxc_cobro_tipo_Param_conta_x_sucursal_Bus();
        tipo_param_det_List List_tipo_param_det = new tipo_param_det_List();
        cxc_cobro_tipo_motivo_Bus bus_motivocobro = new cxc_cobro_tipo_motivo_Bus();
        cxc_CatalogoTipo_Bus bus_catalogotipo = new cxc_CatalogoTipo_Bus();


        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipocobro()
        {
            List<cxc_cobro_tipo_Info> model = new List<cxc_cobro_tipo_Info>();
            model = bus_tipocobro.get_list(true);
            return PartialView("_GridViewPartial_tipocobro", model);
        }

        #endregion

        #region Metodos
        private void cargar_combos()
        {
            var lst_motivo_cobro = bus_motivocobro.get_list();
            ViewBag.lst_motivo_cobro = lst_motivo_cobro;

            var lst_catalogotipo = bus_catalogotipo.get_list();
            ViewBag.lst_catalogotipo = lst_catalogotipo;

            Dictionary<string, string> lst_cta = new Dictionary<string, string>();
            lst_cta.Add("CAJA", "CAJA");
            lst_cta.Add("TIP_COBRO", "TIPO COBRO");
            ViewBag.lst_cta = lst_cta;
        }

        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0 )
        {
            cxc_cobro_tipo_Info model = new cxc_cobro_tipo_Info
            {
                IdEmpresa = IdEmpresa
            };
            model.Lst_tipo_param_det = new List<cxc_cobro_tipo_Param_conta_x_sucursal_Info>();
            List_tipo_param_det.set_list(model.Lst_tipo_param_det);
            cargar_combos_detalle();
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cxc_cobro_tipo_Info model)
        {
            model.Lst_tipo_param_det = List_tipo_param_det.get_list();

            if (bus_tipocobro.validar_existe_IdCobro_tipo(model.IdCobro_tipo))
            {
                ViewBag.mensaje = "El codigo ya se encuentra registrado";
                cargar_combos();
                return View(model);
            }
            if (!bus_tipocobro.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0 , string IdCobro_tipo = "")
        {
            cxc_cobro_tipo_Info model = bus_tipocobro.get_info(IdCobro_tipo);
            if (model == null)
                return RedirectToAction("Index");
            model.IdEmpresa = IdEmpresa;
            model.Lst_tipo_param_det = bus_tipo_param.get_list(IdEmpresa, IdCobro_tipo);
            List_tipo_param_det.set_list(model.Lst_tipo_param_det);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(cxc_cobro_tipo_Info model)
        {
            model.Lst_tipo_param_det = List_tipo_param_det.get_list();

            if (!bus_tipocobro.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(string IdCobro_tipo = "")
        {
            cxc_cobro_tipo_Info model = bus_tipocobro.get_info(IdCobro_tipo);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(cxc_cobro_tipo_Info model)
        {
            if (!bus_tipocobro.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Detalle

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipo_param(string IdCobro_tipo = "")
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            cxc_cobro_tipo_Info model = new cxc_cobro_tipo_Info();
            model.Lst_tipo_param_det = bus_tipo_param.get_list(IdEmpresa, IdCobro_tipo);
            if (model.Lst_tipo_param_det.Count == 0)
                model.Lst_tipo_param_det = List_tipo_param_det.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_tipo_param", model);
        }

        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            ct_plancta_Bus bus_cta = new ct_plancta_Bus();
            var lst_cta = bus_cta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cta = lst_cta;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] cxc_cobro_tipo_Param_conta_x_sucursal_Info info_det)
        {
            if (ModelState.IsValid)
                List_tipo_param_det.AddRow(info_det);
            cxc_cobro_tipo_Info model = new cxc_cobro_tipo_Info();
            model.Lst_tipo_param_det = List_tipo_param_det.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_tipo_param", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] cxc_cobro_tipo_Param_conta_x_sucursal_Info info_det)
        {
            if (ModelState.IsValid)
                List_tipo_param_det.UpdateRow(info_det);
            cxc_cobro_tipo_Info model = new cxc_cobro_tipo_Info();
            model.Lst_tipo_param_det = List_tipo_param_det.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_tipo_param", model);
        }

        public ActionResult EditingDelete(int IdSucursal)
        {
            List_tipo_param_det.DeleteRow(IdSucursal);
            cxc_cobro_tipo_Info model = new cxc_cobro_tipo_Info();
            model.Lst_tipo_param_det = List_tipo_param_det.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_tipo_param", model);
        }
        #endregion
    }

    public class tipo_param_det_List
    {
        public List<cxc_cobro_tipo_Param_conta_x_sucursal_Info> get_list()
        {
            if (HttpContext.Current.Session["cxc_cobro_tipo_Param_conta_x_sucursal_Info"] == null)
            {
                List<cxc_cobro_tipo_Param_conta_x_sucursal_Info> list = new List<cxc_cobro_tipo_Param_conta_x_sucursal_Info>();

                HttpContext.Current.Session["cxc_cobro_tipo_Param_conta_x_sucursal_Info"] = list;
            }
            return (List<cxc_cobro_tipo_Param_conta_x_sucursal_Info>)HttpContext.Current.Session["cxc_cobro_tipo_Param_conta_x_sucursal_Info"];
        }

        public void set_list(List<cxc_cobro_tipo_Param_conta_x_sucursal_Info> list)
        {
            HttpContext.Current.Session["cxc_cobro_tipo_Param_conta_x_sucursal_Info"] = list;
        }
        public void AddRow(cxc_cobro_tipo_Param_conta_x_sucursal_Info info_det)
        {
            List<cxc_cobro_tipo_Param_conta_x_sucursal_Info> list = get_list();
            info_det.IdSucursal = list.Count == 0 ? 1 : list.Max(q => q.IdSucursal) + 1;
            info_det.IdCtaCble = info_det.IdCtaCble;

            list.Add(info_det);
        }

        public void UpdateRow(cxc_cobro_tipo_Param_conta_x_sucursal_Info info_det)
        {
            cxc_cobro_tipo_Param_conta_x_sucursal_Info edited_info = get_list().Where(m => m.IdSucursal == info_det.IdSucursal).First();
            edited_info.IdCtaCble = info_det.IdCtaCble;

        }

        public void DeleteRow(int IdSucursal)
        {
            List<cxc_cobro_tipo_Param_conta_x_sucursal_Info> list = get_list();
            list.Remove(list.Where(m => m.IdSucursal == IdSucursal).First());
        }
    }
}