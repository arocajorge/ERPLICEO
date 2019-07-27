using Core.Erp.Bus.Importacion;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Importacion;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Importacion.Controllers
{
    [SessionTimeout]
    public class AsignacionDeGastosController : Controller
    {
        #region variables
        imp_ordencompra_ext_Bus bus_liquidacion_oc = new imp_ordencompra_ext_Bus();
        imp_orden_compra_ext_recepcion_det_lst detalle = new imp_orden_compra_ext_recepcion_det_lst();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        imp_gasto_Bus bus_gastos_tipo = new imp_gasto_Bus();
        imp_ordencompra_ext_det_Bus bus_detalle = new imp_ordencompra_ext_det_Bus();
        imp_orden_compra_ext_ct_cbteble_det_gastos_Bus bus_gastos = new imp_orden_compra_ext_ct_cbteble_det_gastos_Bus();
        imp_catalogo_Bus bus_catalogo = new imp_catalogo_Bus();
        imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst Lis_imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst = new imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst();
        imp_ordencompra_ext_det_Info_lst info_detalle_lst = new imp_ordencompra_ext_det_Info_lst();
        imp_gasto_x_ct_plancta_Bus imp_gasto_x_ct_plancta_bus = new imp_gasto_x_ct_plancta_Bus();
        
        #endregion

        #region vistas

        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);
        }

        public ActionResult GridViewPartial_asignacion_gastos(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            if (IdSucursal == 0)
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_liquidacion_oc.get_list_oc_con_recepcion_mercaderia(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_asignacion_gastos", model);
        }
        public ActionResult GridViewPartial_asignacion_gastos_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = info_detalle_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_asignacion_gastos_det", model);
        }

        public ActionResult GridViewPartial_gastos_asignados()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> model = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();
            model = Lis_imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            if (model == null)
                model = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_gastos_asignados", model);
        }
        public ActionResult GridViewPartial_gastos_por_asignar(string IdCtaCble_importacion = "")
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> model = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();
            if (IdCtaCble_importacion != "")
            {
                model = bus_gastos.get_list_gastos_no_asignados(IdEmpresa, IdCtaCble_importacion);
                Session["imp_orden_compra_ext_ct_cbteble_det_gastos_Info_x_asignar"] = model;
            }
            else
            {
                model = Session["imp_orden_compra_ext_ct_cbteble_det_gastos_Info_x_asignar"] as List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>;
            }
            if (model == null)
                model = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();
            return PartialView("_GridViewPartial_gastos_por_asignar", model);
        }
        #endregion

        public ActionResult Nuevo(int IdEmpresa = 0 , decimal IdOrdenCompra_ext = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            imp_ordencompra_ext_Info model = new imp_ordencompra_ext_Info();
            model = bus_liquidacion_oc.get_asignar_gastos(IdEmpresa, IdOrdenCompra_ext);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model != null)
            {
                List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> gastos_asig_no_asig = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();
                if (model.lst_gastos_asignados == null)
                    model.lst_gastos_asignados = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();
                gastos_asig_no_asig.AddRange(model.lst_gastos_asignados);
                if (model.lst_gastos_asignados == null)
                    model.lst_gastos_asignados = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();
                gastos_asig_no_asig.AddRange(model.lst_gastos_por_asignar);
                int secuencia = 0;
                foreach (var item in gastos_asig_no_asig)
                {
                    secuencia++;
                    item.secuencia = secuencia;
                }
                Lis_imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst.set_list(gastos_asig_no_asig, model.IdTransaccionSession);
               
            }
            else
                model = new imp_ordencompra_ext_Info();
            cargar_combos(IdEmpresa);
            cargar_combos_detalle();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(imp_ordencompra_ext_Info model)
        {
            model.lst_gastos_asignados = Lis_imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst.get_list(model.IdTransaccionSession).Where(v=>v.IdGasto_tipo!=null).ToList();
            if (model.lst_gastos_asignados == null)
            {
                ViewBag.mensaje = "no existe detalle";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            else
            {
                if (model.lst_gastos_asignados.Count() == 0)
                {
                    ViewBag.mensaje = "no existe detalle";
                    cargar_combos(model.IdEmpresa);
                    return View(model);
                }
                else
                {
                    foreach (var item in model.lst_gastos_asignados)
                    {
                        if (item.IdGasto_tipo == null)
                        {
                            ViewBag.mensaje = "No se ha especificado el tipo de gasto";
                            cargar_combos(model.IdEmpresa);
                            return View(model);
                        }
                    }
                }

            }
            if (!bus_gastos.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
       
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] imp_orden_compra_ext_ct_cbteble_det_gastos_Info info_det)
        {
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            Lis_imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst.UpdateRow(info_det, IdTransaccionSession);
            var model = Lis_imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst.get_list(IdTransaccionSession);
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_gastos_asignados", model);
        }

        private void cargar_combos(int IdEmpresa)
        {
            var lst_catalogos = bus_catalogo.get_list(1);
            ViewBag.lst_catalogos = lst_catalogos;
        }
        private void cargar_combos_detalle()
        {
            var lst_gastos = bus_gastos_tipo.get_list();
            ViewBag.lst_gastos = lst_gastos;
        }
    }

    public class imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst
    {
        string Variable = "imp_orden_compra_ext_ct_cbteble_det_gastos_Info";
        public List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> list = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(imp_orden_compra_ext_ct_cbteble_det_gastos_Info info_det, decimal IdTransaccionSession)
        {
            List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> list = get_list(IdTransaccionSession);
            info_det.secuencia_ct = list.Count == 0 ? 1 : list.Max(q => q.secuencia_ct) + 1;
            info_det.secuencia = info_det.secuencia_ct;
            list.Add(info_det);
        }

        public void UpdateRow(imp_orden_compra_ext_ct_cbteble_det_gastos_Info info_det, decimal IdTransaccionSession)
        {
            imp_orden_compra_ext_ct_cbteble_det_gastos_Info edited_info = get_list(IdTransaccionSession).Where(m => m.secuencia == info_det.secuencia).First();
            edited_info.IdGasto_tipo = info_det.IdGasto_tipo;

        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.secuencia_ct == Secuencia).First());
        }
    }
}