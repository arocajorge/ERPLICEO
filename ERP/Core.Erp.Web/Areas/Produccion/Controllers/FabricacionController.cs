using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.Produccion;
using Core.Erp.Bus.Produccion;
using Core.Erp.Bus.General;
using Core.Erp.Web.Helps;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Inventario;
using DevExpress.Web;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.Produccion.Controllers
{
    public class FabricacionController : Controller
    {
        #region Variables
        pro_Fabricacion_Bus bus_fabricacion = new pro_Fabricacion_Bus();
        pro_FabricacionDet_Bus bus_fabricacion_det = new pro_FabricacionDet_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
        pro_FabricacionDet_List List_det = new pro_FabricacionDet_List();
        in_Producto_Composicion_Info comp = new in_Producto_Composicion_Info();
        in_Producto_Composicion_Bus bus_comp = new in_Producto_Composicion_Bus();
        in_UnidadMedida_Equiv_conversion_Bus bus_UnidadMedidaEquivalencia = new in_UnidadMedida_Equiv_conversion_Bus();
        pro_FabricacionDet_Fac List_Fac = new pro_FabricacionDet_Fac();
        in_producto_x_tb_bodega_Costo_Historico_Bus bus_costo = new in_producto_x_tb_bodega_Costo_Historico_Bus();

        #endregion
        #region Index

        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combos_consulta(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            cargar_combos_consulta(model.IdEmpresa);
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_fabricacion(DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(fecha_ini);
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_fin);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_fabricacion.GetList(IdEmpresa, IdSucursal, ViewBag.fecha_ini, ViewBag.fecha_fin, true);
            return PartialView("_GridViewPartial_fabricacion", model);
        }
        #endregion
        #region Metodos
        private void cargar_combos_consulta(int IdEmpresa)
        {
            var lst_sucursal_consulta = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal_consulta = lst_sucursal_consulta;
        }
        private void cargar_combos(pro_Fabricacion_Info model)
        {
            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_bodega_ing = bus_bodega.get_list(model.IdEmpresa, model.ing_IdSucursal, false);
            ViewBag.lst_bodega_ing = lst_bodega_ing;

            var lst_bodega_egr = bus_bodega.get_list(model.IdEmpresa, model.egr_IdSucursal, false);
            ViewBag.lst_bodega_egr = lst_bodega_egr;
        }
        private void cargar_combos_detalle()
        {
            in_UnidadMedida_Bus bus_unidad = new in_UnidadMedida_Bus();
            var lst_unidad_medida = bus_unidad.get_list(false);
            ViewBag.lst_unidad_medida = lst_unidad_medida;
        }

        private bool Validar(pro_Fabricacion_Info i_validar, ref string msg)
        {
            #region ValidarStock

            var lst_validar = i_validar.LstDet.GroupBy(q => new { q.IdProducto, q.pr_descripcion, q.tp_ManejaInven, q.se_distribuye }).Select(q => new in_Producto_Stock_Info
            {
                IdEmpresa = i_validar.IdEmpresa,
                IdSucursal = i_validar.egr_IdSucursal,
                IdBodega = i_validar.egr_IdBodega,
                IdProducto = q.Key.IdProducto,
                pr_descripcion = q.Key.pr_descripcion,
                tp_manejaInven = q.Key.tp_ManejaInven,
                SeDestribuye = q.Key.se_distribuye,

                Cantidad = q.Sum(v => v.Cantidad),
                CantidadAnterior = q.Sum(v => v.CantidadAnterior),
            }).ToList();

            if (!bus_producto.validar_stock(lst_validar, ref msg))
            {
                return false;
            }
            #endregion

            return true;
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            pro_Fabricacion_Info model = new pro_Fabricacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                Fecha = DateTime.Now,
                FechaIni = DateTime.Now.Date.AddMonths(-1),
                FechaFin = DateTime.Now.Date,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                LstDet = new List<pro_FabricacionDet_Info>(),
                egr_IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                ing_IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),


            };
            List_det.set_list(model.LstDet, model.IdTransaccionSession);
            List_Fac.set_list_fac(model.LstDet, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(pro_Fabricacion_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            model.LstDet = List_det.get_list(model.IdTransaccionSession);
            if (!bus_fabricacion.GuardarDB(model))
            {
                cargar_combos(model);
                return View(model);

            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, decimal IdFabricacion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            pro_Fabricacion_Info model = bus_fabricacion.GetInfo(IdEmpresa, IdFabricacion);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.LstDet = bus_fabricacion_det.GetList(IdEmpresa, IdFabricacion);
            model.FechaIni = DateTime.Now;
            model.FechaFin = DateTime.Now;
            List_det.set_list(model.LstDet, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);

        }
        [HttpPost]
        public ActionResult Modificar(pro_Fabricacion_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            model.LstDet = List_det.get_list(model.IdTransaccionSession);
            if (!bus_fabricacion.ModificarDB(model))
            {
                cargar_combos(model);
                return View(model);

            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0, decimal IdFabricacion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            pro_Fabricacion_Info model = bus_fabricacion.GetInfo(IdEmpresa, IdFabricacion);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.LstDet = bus_fabricacion_det.GetList(IdEmpresa, IdFabricacion);
            List_det.set_list(model.LstDet, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);

        }
        [HttpPost]
        public ActionResult Anular(pro_Fabricacion_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_fabricacion.AnularDB(model))
            {
                cargar_combos(model);
                return View(model);

            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Json
        public JsonResult CargarBodega(int IdEmpresa = 0, int IdSucursal = 0)
        {
            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            bus_bodega = new tb_bodega_Bus();
            var resultado = bus_bodega.get_list(IdEmpresa, IdSucursal, false);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ArmarMateriaPrima(int IdEmpresa = 0,int IdSucursal = 0, int IdBodega = 0, decimal IdTransaccionSession = 0)
        {
            double CostoProductoElaborado = 0;
            List_det.DeleteAll("-", IdTransaccionSession);
            var lst = List_det.get_list(IdTransaccionSession).Where(q => q.Signo == "+").ToList();
            List_det.DeleteAll("+", IdTransaccionSession);
            foreach (var item in lst)
            {
                var resultado = bus_comp.get_list(IdEmpresa, item.IdProducto);
                CostoProductoElaborado = 0;
                foreach (var cmp in resultado)
                {
                    var costo = bus_costo.get_ultimo_costo(IdEmpresa, IdSucursal, IdBodega, cmp.IdProductoHijo, DateTime.Now.Date);
                    var row = new pro_FabricacionDet_Info
                    {
                        IdEmpresa = cmp.IdEmpresa,
                        IdProducto = cmp.IdProductoHijo,
                        Cantidad = cmp.Cantidad * item.Cantidad,
                        pr_descripcion = cmp.pr_descripcion,
                        IdUnidadMedida = cmp.IdUnidadMedida,
                        Signo = "-",
                        RealizaMovimiento = true,
                        Costo = costo * cmp.Cantidad,
                        se_distribuye = cmp.se_distribuye ?? false,
                        tp_ManejaInven = cmp.tp_ManejaInven
                    };
                    List_det.AddRow(row, IdTransaccionSession);
                    CostoProductoElaborado += row.Costo;
                }
                item.Costo = CostoProductoElaborado;
                List_det.AddRow(item, IdTransaccionSession);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductoFacturadosPorFecha(DateTime FechaIni, DateTime FechaFin, int IdEmpresa = 0, int IdSucursal = 0, int IdBodega = 0, decimal IdTransaccionSession = 0)
        {
            bool resultado = true;
            var Lista = bus_fabricacion_det.GetProductoFacturadosPorFecha(IdEmpresa, IdSucursal, IdBodega, FechaIni, FechaFin);
            if (Lista.Count() == 0)
                resultado = false;
            var det = List_Fac.get_list_fact(IdTransaccionSession);
            List_Fac.set_list_fac(Lista, IdTransaccionSession);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
           public JsonResult EditingAddNew(string IDs = "", decimal IdTransaccionSession = 0)
        {
            bool resultado = true;
            if (!string.IsNullOrEmpty(IDs))
            {
                var lst = List_Fac.get_list_fact(IdTransaccionSession);
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    var info_det = lst.Where(q => q.Secuencia == Convert.ToInt32(item)).FirstOrDefault();
                    if (info_det != null)
                        List_det.AddRow(info_det, IdTransaccionSession);
                }
            }
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Metodos ComboBox bajo demanda
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        public ActionResult CmbProducto_Fabricacion()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_Fabricacion", model);
        }
        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORMODULO, cl_enumeradores.eModulo.INV, 0, 0);
        }
        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Cargar Unidad de medida
        public ActionResult CargarUnidadMedida()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            decimal IdProducto = Request.Params["in_IdProducto"] != null ? Convert.ToDecimal(Request.Params["in_IdProducto"]) : 0;

            in_Producto_Info info_produto = bus_producto.get_info(IdEmpresa, IdProducto);
            return GridViewExtension.GetComboBoxCallbackResult(p =>
            {
                p.TextField = "Descripcion";
                p.ValueField = "IdUnidadMedida_equiva";
                p.ValueType = typeof(string);
                p.BindList(bus_UnidadMedidaEquivalencia.get_list_combo(info_produto.IdUnidadMedida_Consumo));
            });
        }
        #endregion

        #region Detalle_ing
        public ActionResult GridViewPartial_fabricacion_det_ing(decimal IdFabricacion = 0)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.Signo == "+").ToList();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_fabricacion_det_ing", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNewIngreso([ModelBinder(typeof(DevExpressEditorsBinder))] pro_FabricacionDet_Info info_det)
        {
            var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProducto);
            if (producto != null)
                info_det.pr_descripcion = producto.pr_descripcion;
            info_det.Signo = "+";
            if (ModelState.IsValid)
                List_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.Signo == "+").ToList();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_fabricacion_det_ing", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdateIngreso([ModelBinder(typeof(DevExpressEditorsBinder))] pro_FabricacionDet_Info info_det)
        {
            var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProducto);
            if (producto != null)
                info_det.pr_descripcion = producto.pr_descripcion;
            info_det.Signo = "+";
            if (ModelState.IsValid)
                List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.Signo == "+").ToList();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_fabricacion_det_ing", model);
        }
        public ActionResult EditingDeleteIngreso(int Secuencia)
        {
            List_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.Signo == "+").ToList();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_fabricacion_det_ing", model);
        }

        #endregion
        #region Detalle_egr
        public ActionResult GridViewPartial_fabricacion_det_egr(decimal IdFabricacion = 0)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            cargar_combos_detalle();
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.Signo == "-").ToList();
            return PartialView("_GridViewPartial_fabricacion_det_egr", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNewEgreso([ModelBinder(typeof(DevExpressEditorsBinder))] pro_FabricacionDet_Info info_det)
        {
            var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProducto);
            if (producto != null)
                info_det.pr_descripcion = producto.pr_descripcion;
            info_det.Signo = "-";
            if (ModelState.IsValid)
                List_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.Signo == "-").ToList();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_fabricacion_det_egr", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdateEgreso([ModelBinder(typeof(DevExpressEditorsBinder))] pro_FabricacionDet_Info info_det)
        {
            var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProducto);
            if (producto != null)
                info_det.pr_descripcion = producto.pr_descripcion;
            if (ModelState.IsValid)
                info_det.Signo = "-";
            List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.Signo == "-").ToList();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_fabricacion_det_egr", model);
        }
        public ActionResult EditingDeleteEgreso(int Secuencia)
        {
            List_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.Signo == "-").ToList();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_fabricacion_det_egr", model);
        }

        #endregion
        #region Det Fact
            [ValidateInput(false)]
            public ActionResult GridViewPartial_fabricacion_det_fac()
            {
                SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
                var model = List_Fac.get_list_fact(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                return PartialView("_GridViewPartial_fabricacion_det_fac", model);
            }


            #endregion
    }
    public class pro_FabricacionDet_List
    {
        string Variable = "pro_FabricacionDet_Info";
        public List<pro_FabricacionDet_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<pro_FabricacionDet_Info> list = new List<pro_FabricacionDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<pro_FabricacionDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }
        
        public void set_list(List<pro_FabricacionDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(pro_FabricacionDet_Info info_det, decimal IdTransaccionSession)
        {
            List<pro_FabricacionDet_Info> list = get_list(IdTransaccionSession);
            pro_FabricacionDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdProducto == info_det.IdProducto && m.Signo == "-").FirstOrDefault();
            if(edited_info != null)
            {
                edited_info.Cantidad += info_det.Cantidad;
            }
            else
            {
                info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
                info_det.IdProducto = info_det.IdProducto;
                info_det.IdUnidadMedida = info_det.IdUnidadMedida;
                info_det.RealizaMovimiento = info_det.RealizaMovimiento;
                info_det.pr_descripcion = info_det.pr_descripcion;
                list.Add(info_det);
            }            
        }

        public void UpdateRow(pro_FabricacionDet_Info info_det, decimal IdTransaccionSession)
        {
            pro_FabricacionDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.Cantidad = info_det.Cantidad;
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.IdUnidadMedida = info_det.IdUnidadMedida;
            edited_info.RealizaMovimiento = info_det.RealizaMovimiento;
            edited_info.pr_descripcion = info_det.pr_descripcion;
            edited_info.Costo = info_det.Costo;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<pro_FabricacionDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }

        public void DeleteAll(string Signo, decimal IdTransaccionSession)
        {
            List<pro_FabricacionDet_Info> list = get_list(IdTransaccionSession);
            list.RemoveAll(m => m.Signo == Signo);
        }
    }

    public class pro_FabricacionDet_Fac
    {
        string variable = "pro_FabricacionDet_Fac";
        public List<pro_FabricacionDet_Info> get_list_fact(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<pro_FabricacionDet_Info> list = new List<pro_FabricacionDet_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<pro_FabricacionDet_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }
        public void set_list_fac(List<pro_FabricacionDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }
    }
}