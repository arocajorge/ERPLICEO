using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    public class CentroCostoController : Controller
    {
        #region Variables
        ct_CentroCosto_Info Lista = new ct_CentroCosto_Info();
        ct_CentroCosto_Bus bus_centrocosto = new ct_CentroCosto_Bus();
        ct_CentroCostoNivel_Bus bus_centrocosto_nivel = new ct_CentroCostoNivel_Bus();
        #endregion

        #region Index        
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_centrocosto()
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ct_CentroCosto_Info> model = bus_centrocosto.get_list(IdEmpresa, true, false);
            return PartialView("_GridViewPartial_centrocosto", model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_centrocosto_nivel = bus_centrocosto_nivel.get_list(IdEmpresa, false);
            ViewBag.lst_centrocosto_nivel = lst_centrocosto_nivel;
        }
        #endregion

        #region Metodos ComboBox bajo demanda

        public ActionResult CmbPadre_CentroCosto()
        {
            ct_CentroCosto_Info model = new ct_CentroCosto_Info();
            return PartialView("_CmbPadre_CentroCosto", model);
        }
        public List<ct_CentroCosto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_centrocosto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), true);
        }
        public ct_CentroCosto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_centrocosto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion


        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            ct_CentroCosto_Info model = new ct_CentroCosto_Info
            {
                IdEmpresa = IdEmpresa
            };
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ct_CentroCosto_Info model)
        {
            if (bus_centrocosto.validar_existe_id(model.IdEmpresa, model.IdCentroCosto))
            {
                ViewBag.mensaje = "El código de la cuenta ya se encuentra registrado";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_centrocosto.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0, string IdCentroCosto = "")
        {
            ct_CentroCosto_Info model = bus_centrocosto.get_info(IdEmpresa, IdCentroCosto);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ct_CentroCosto_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_centrocosto.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0, string IdCentroCosto = "")
        {
            ct_CentroCosto_Info model = bus_centrocosto.get_info(IdEmpresa, IdCentroCosto);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_CentroCosto_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_centrocosto.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Json
        public JsonResult get_info_nuevo(int IdEmpresa = 0, string IdCentroCostoPadre = "")
        {
            var resultado = bus_centrocosto.get_info_nuevo(IdEmpresa, IdCentroCostoPadre);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }

    public class ct_CentroCosto_List
    {
        string Variable = "ct_CentroCosto_Info";
        public List<ct_CentroCosto_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession] == null)
            {
                List<ct_CentroCosto_Info> list = new List<ct_CentroCosto_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
            }
            return (List<ct_CentroCosto_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession];
        }

        public void set_list(List<ct_CentroCosto_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
        }
    }
}