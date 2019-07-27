using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Banco;
using Core.Erp.Info.Banco;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    public class TipoFlujoBancoController : Controller
    {
        #region Variables

        ba_TipoFlujo_Bus bus_flujo = new ba_TipoFlujo_Bus();
        ba_Banco_Flujo_Det_List List_Det = new ba_Banco_Flujo_Det_List();
        ba_Cbte_Ban_x_ba_TipoFlujo_Bus bus_flujo_det = new ba_Cbte_Ban_x_ba_TipoFlujo_Bus();
        ba_Archivo_Flujo_List List_flujo = new ba_Archivo_Flujo_List();
        #endregion

        #region Metodos ComboBox bajo demanda flujo
        ba_TipoFlujo_Bus bus_tipo = new ba_TipoFlujo_Bus();
        public ActionResult CmbFlujo_Tipo()
        {
            ba_Cbte_Ban_x_ba_TipoFlujo_Info model = new ba_Cbte_Ban_x_ba_TipoFlujo_Info();
            return PartialView("_CmbFlujo_Tipo", model);
        }
        public List<ba_TipoFlujo_Info> get_list_bajo_demandaFlujo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_tipo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ba_TipoFlujo_Info get_info_bajo_demandaFlujo(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_tipo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Acciones

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipo_flujo()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_flujo.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_tipo_flujo", model);
        }

        private void cargar_combo(int IdEmpresa)
        {
            var lst_tipo = bus_flujo.get_list(IdEmpresa, false);
            ViewBag.lst_tipo = lst_tipo;

            Dictionary<string, string> lst_tip = new Dictionary<string, string>();
            lst_tip.Add(cl_enumeradores.eTipoIngEgr.ING.ToString(), "Ingreso");
            lst_tip.Add(cl_enumeradores.eTipoIngEgr.EGR.ToString(), "Egreso");
            ViewBag.lst_tip = lst_tip;
        }
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            ba_TipoFlujo_Info model = new ba_TipoFlujo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            cargar_combo(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ba_TipoFlujo_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_flujo.guardarDB(model))
            {
                cargar_combo(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, decimal IdTipoFlujo = 0)
        {
            ba_TipoFlujo_Info model = bus_flujo.get_info(IdEmpresa, IdTipoFlujo);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combo(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ba_TipoFlujo_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_flujo.modificarDB(model))
            {
                cargar_combo(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0, decimal IdTipoFlujo = 0)
        {
            ba_TipoFlujo_Info model = bus_flujo.get_info(IdEmpresa, IdTipoFlujo);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combo(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ba_TipoFlujo_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            if (!bus_flujo.anularDB(model))
            {
                cargar_combo(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Detalle
        private void cargar_combos_Detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_flujo = bus_flujo.get_list(IdEmpresa, false);
            ViewBag.lst_flujo = lst_flujo;

            
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_flujo_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            cargar_combos_Detalle();
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_flujo_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ba_Cbte_Ban_x_ba_TipoFlujo_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (info_det != null)
                if (info_det.IdTipoFlujo != 0)
                {
                    ba_TipoFlujo_Info info_TipoFlujo = bus_flujo.get_info(IdEmpresa, info_det.IdTipoFlujo);
                    if (info_TipoFlujo != null)
                    {
                        info_det.Descricion = info_TipoFlujo.Descricion;
                    }
                }

            if (ModelState.IsValid)
                List_Det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_flujo_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ba_Cbte_Ban_x_ba_TipoFlujo_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det != null)
                if (info_det.IdTipoFlujo != 0)
                {
                    ba_TipoFlujo_Info info_TipoFlujo = bus_flujo.get_info(IdEmpresa, info_det.IdTipoFlujo);
                    if (info_TipoFlujo != null)
                    {
                        info_det.IdTipoFlujo = info_TipoFlujo.IdTipoFlujo;
                        info_det.Descricion = info_TipoFlujo.Descricion;
                    }
                }

            if (ModelState.IsValid)
                List_Det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_flujo_det", model);
        }
        public ActionResult EditingDelete(int Secuencia)
        {
            List_Det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_flujo_det", model);
        }
        #endregion

        #region Json
        public JsonResult actualizarGridDetFlujo(float Valor = 0)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            var ListaPlantillaTipoFlujo = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            var ListaDetFlujo = new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>();
            foreach (var item in ListaPlantillaTipoFlujo)
            {
                ListaDetFlujo.Add(new ba_Cbte_Ban_x_ba_TipoFlujo_Info
                {
                    Secuencia = item.Secuencia,
                    IdTipoFlujo = item.IdTipoFlujo,
                    Descricion = item.Descricion,
                    Porcentaje = item.Porcentaje,
                    Valor = (item.Porcentaje * Valor) / 100
                });
            }

            List_Det.set_list(ListaDetFlujo, IdTransaccionSession);
            return Json(ListaDetFlujo, JsonRequestBehavior.AllowGet);
        }

        
        #endregion

    }
    public class ba_Banco_Flujo_Det_List
    {
        string Variable = "ba_Cbte_Ban_x_ba_TipoFlujo_Info";
        public List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list = new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ba_Cbte_Ban_x_ba_TipoFlujo_Info info_det, decimal IdTransaccionSession)
        {
            List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list = get_list(IdTransaccionSession);

            if (list.Where(q => q.IdTipoFlujo == info_det.IdTipoFlujo).Count() == 0)
            {
                info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
                list.Add(info_det);
            }

            //info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            //info_det.IdTipocbte = info_det.IdTipocbte;
            //info_det.IdCbteCble = info_det.IdCbteCble;
            //info_det.IdTipoFlujo = info_det.IdTipoFlujo;
            //info_det.Porcentaje = info_det.Porcentaje;
            //info_det.Valor = info_det.Valor;
            //list.Add(info_det);            
        }

        public void UpdateRow(ba_Cbte_Ban_x_ba_TipoFlujo_Info info_det, decimal IdTransaccionSession)
        {
            ba_Cbte_Ban_x_ba_TipoFlujo_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdTipocbte = info_det.IdTipocbte;
            edited_info.IdCbteCble = info_det.IdCbteCble;
            edited_info.IdTipoFlujo = info_det.IdTipoFlujo;
            edited_info.Porcentaje = info_det.Porcentaje;
            edited_info.Descricion = info_det.Descricion;
            edited_info.Valor = info_det.Valor;
            edited_info.Secuencia = info_det.Secuencia;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

}