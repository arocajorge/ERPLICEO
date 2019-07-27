using Core.Erp.Bus.Banco;
using Core.Erp.Info.Banco;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    public class TipoFlujoPlantillaController : Controller
    {
        #region Variables
        ba_TipoFlujo_Bus bus_TipoFlujo = new ba_TipoFlujo_Bus();
        ba_TipoFlujo_Plantilla_Bus bus_TipoFlujo_Plantilla = new ba_TipoFlujo_Plantilla_Bus();
        ba_TipoFlujo_PlantillaDet_Bus bus_TipoFlujo_PlantillaDet = new ba_TipoFlujo_PlantillaDet_Bus();
        ba_TipoFlujo_PlantillaDet_List TipoFlujo_PlantillaDet_Lista = new ba_TipoFlujo_PlantillaDet_List();
        ba_Banco_Flujo_Det_List List_Det = new ba_Banco_Flujo_Det_List();
        ba_Archivo_Flujo_List List_flujo = new ba_Archivo_Flujo_List();
        string mensaje = string.Empty;
        #endregion

        #region Combos bajo demanda
        #region CmbTipoFlujo

        public ActionResult CmbTipoFlujo()
        {
            ba_TipoFlujo_PlantillaDet_Info model = new ba_TipoFlujo_PlantillaDet_Info();
            return PartialView("_CmbTipoFlujo", model);
        }
        public List<ba_TipoFlujo_Info> get_list_bajo_demanda_tipoflujo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var TipoFlujo_GetList = bus_TipoFlujo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
            return TipoFlujo_GetList;
        }
        public ba_TipoFlujo_Info get_info_bajo_demanda_tipoflujo(ListEditItemRequestedByValueEventArgs args)
        {
            var TipoFlujo_GetInfo = bus_TipoFlujo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
            return TipoFlujo_GetInfo;
        }
        #endregion
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_TipoFlujoPlantilla()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ba_TipoFlujo_Plantilla_Info> model = bus_TipoFlujo_Plantilla.GetList(IdEmpresa, true);

            return PartialView("_GridViewPartial_TipoFlujoPlantilla", model);
        }
        #endregion

        #region Plantilla por asignar
        public ActionResult GridViewPartial_TipoFlujoPlantilla_Asignar()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ba_TipoFlujo_Plantilla_Info> model = bus_TipoFlujo_Plantilla.GetList(IdEmpresa, true);

            return PartialView("_GridViewPartial_TipoFlujoPlantilla_Asignar", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ba_TipoFlujo_Plantilla_Info model = new ba_TipoFlujo_Plantilla_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdUsuarioCreacion = SessionFixed.IdUsuario
            };

            TipoFlujo_PlantillaDet_Lista.set_list(new List<ba_TipoFlujo_PlantillaDet_Info>(), model.IdTransaccionSession);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ba_TipoFlujo_Plantilla_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            model.Lista_TipoFlujo_PlantillaDet = TipoFlujo_PlantillaDet_Lista.get_list(model.IdTransaccionSession);

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            if (!bus_TipoFlujo_Plantilla.GuardarBD(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, decimal IdPlantilla = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ba_TipoFlujo_Plantilla_Info model = bus_TipoFlujo_Plantilla.GetInfo(IdEmpresa, IdPlantilla);

            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.Lista_TipoFlujo_PlantillaDet = bus_TipoFlujo_PlantillaDet.GetList(model.IdEmpresa, model.IdPlantilla);
            TipoFlujo_PlantillaDet_Lista.set_list(model.Lista_TipoFlujo_PlantillaDet, model.IdTransaccionSession);

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ba_TipoFlujo_Plantilla_Info model)
        {
            model.Lista_TipoFlujo_PlantillaDet = TipoFlujo_PlantillaDet_Lista.get_list(model.IdTransaccionSession);
            model.IdUsuarioModificacion = Session["IdUsuario"].ToString();

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_TipoFlujo_Plantilla.ModificarBD(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdPlantilla = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ba_TipoFlujo_Plantilla_Info model = bus_TipoFlujo_Plantilla.GetInfo(IdEmpresa, IdPlantilla);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.Lista_TipoFlujo_PlantillaDet = bus_TipoFlujo_PlantillaDet.GetList(model.IdEmpresa, model.IdPlantilla);
            TipoFlujo_PlantillaDet_Lista.set_list(model.Lista_TipoFlujo_PlantillaDet, model.IdTransaccionSession);

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ba_TipoFlujo_Plantilla_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_TipoFlujo_Plantilla.AnularBD(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Metodos del detalle
        public ActionResult GridViewPartial_TipoFlujoPlantillaDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = TipoFlujo_PlantillaDet_Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_TipoFlujoPlantillaDet", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ba_TipoFlujo_PlantillaDet_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);

            if (info_det != null)
                if (info_det.IdTipoFlujo != 0)
                {
                    ba_TipoFlujo_Info info_TipoFlujo = bus_TipoFlujo.get_info(IdEmpresa, info_det.IdTipoFlujo);
                    if (info_TipoFlujo != null)
                    {
                        info_det.Descricion = info_TipoFlujo.Descricion;
                    }
                }

            TipoFlujo_PlantillaDet_Lista.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = TipoFlujo_PlantillaDet_Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_TipoFlujoPlantillaDet", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ba_TipoFlujo_PlantillaDet_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det != null)
                if (info_det.IdTipoFlujo != 0)
                {
                    ba_TipoFlujo_Info info_TipoFlujo = bus_TipoFlujo.get_info(IdEmpresa, info_det.IdTipoFlujo);
                    if (info_TipoFlujo != null)
                    {
                        info_det.IdTipoFlujo = info_TipoFlujo.IdTipoFlujo;
                        info_det.Descricion = info_TipoFlujo.Descricion;
                    }
                }
            TipoFlujo_PlantillaDet_Lista.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = TipoFlujo_PlantillaDet_Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_TipoFlujoPlantillaDet", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            TipoFlujo_PlantillaDet_Lista.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ba_TipoFlujo_Plantilla_Info model = new ba_TipoFlujo_Plantilla_Info();
            model.Lista_TipoFlujo_PlantillaDet = TipoFlujo_PlantillaDet_Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_TipoFlujoPlantillaDet", model.Lista_TipoFlujo_PlantillaDet);
        }

        private bool Validar(ba_TipoFlujo_Plantilla_Info i_validar, ref string msg)
        {
            i_validar.Lista_TipoFlujo_PlantillaDet = TipoFlujo_PlantillaDet_Lista.get_list(i_validar.IdTransaccionSession);

            if (i_validar.Lista_TipoFlujo_PlantillaDet.Count == 0)
            {
                mensaje = "Debe ingresar al menos un tipo de flujo";
                return false;
            }
            else
            {
                foreach (var item1 in i_validar.Lista_TipoFlujo_PlantillaDet)
                {
                    var contador = 0;
                    foreach (var item2 in i_validar.Lista_TipoFlujo_PlantillaDet)
                    {
                        if (item1.IdTipoFlujo == item2.IdTipoFlujo)
                        {
                            contador++;
                        }

                        if (contador > 1)
                        {
                            mensaje = "Existe tipos de flujo repetidos en el detalle";
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region Json
        public JsonResult cargar_PlantillaTipoFlujo(float Valor = 0, decimal IdPlantillaTipoFlujo = 0 )
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            var ListaPlantillaTipoFlujo = bus_TipoFlujo_PlantillaDet.GetList(IdEmpresa, IdPlantillaTipoFlujo);
            var ListaDetFlujo = new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>();
            var secuencia = 1;

            foreach (var item in ListaPlantillaTipoFlujo)
            {
                ListaDetFlujo.Add(new ba_Cbte_Ban_x_ba_TipoFlujo_Info
                {
                    Secuencia = secuencia++,
                    IdTipoFlujo = item.IdTipoFlujo,
                    Descricion = item.Descricion,
                    Porcentaje = item.Porcentaje,
                    Valor = (item.Porcentaje * Valor)/100
                });        
            }            

            List_Det.set_list(ListaDetFlujo, IdTransaccionSession);            
            return Json(ListaDetFlujo, JsonRequestBehavior.AllowGet);
        }

        
        #endregion
    }

    public class ba_TipoFlujo_PlantillaDet_List
    {
        string Variable = "ba_TipoFlujo_PlantillaDet_Info";
        public List<ba_TipoFlujo_PlantillaDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_TipoFlujo_PlantillaDet_Info> list = new List<ba_TipoFlujo_PlantillaDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_TipoFlujo_PlantillaDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_TipoFlujo_PlantillaDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ba_TipoFlujo_PlantillaDet_Info info_det, decimal IdTransaccionSession)
        {
            List<ba_TipoFlujo_PlantillaDet_Info> list = get_list(IdTransaccionSession);
            if (list.Where(q => q.IdTipoFlujo == info_det.IdTipoFlujo).Count() == 0)
            {
                info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;                
                list.Add(info_det);
            }       
        }

        public void UpdateRow(ba_TipoFlujo_PlantillaDet_Info info_det, decimal IdTransaccionSession)
        {
            ba_TipoFlujo_PlantillaDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdTipoFlujo = info_det.IdTipoFlujo;
            edited_info.Porcentaje = info_det.Porcentaje;
            edited_info.Descricion = info_det.Descricion;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ba_TipoFlujo_PlantillaDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}