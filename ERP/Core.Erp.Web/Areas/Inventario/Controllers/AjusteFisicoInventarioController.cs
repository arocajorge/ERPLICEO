using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    public class AjusteFisicoInventarioController : Controller
    {
        #region variables
        in_Ajuste_Bus bus_ajuste = new in_Ajuste_Bus();
        in_AjusteDet_Bus bus_ajuste_det = new in_AjusteDet_Bus();
        in_AjusteDet_List ListaDetalle = new in_AjusteDet_List();
        string mensaje = string.Empty;
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        in_UnidadMedida_Equiv_conversion_Bus bus_UnidadMedidaEquivalencia = new in_UnidadMedida_Equiv_conversion_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        tb_bodega_Bus bus_bodega= new tb_bodega_Bus();
        in_parametro_Bus bus_in_param = new in_parametro_Bus();
        in_Catalogo_Bus bus_catalogo = new in_Catalogo_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProducto_AjusteFisico()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_AjusteFisico", model);
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

        #region Vistas
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdBodega = 0,
                fecha_ini = DateTime.Now.Date.AddMonths(-1),
                fecha_fin = DateTime.Now
            };

            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }

        private void CargarCombosConsulta(int IdEmpresa)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_ajuste_fisico(DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal = 0)
        {
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : fecha_ini;
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : fecha_fin;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;

            List<in_Ajuste_Info> model = bus_ajuste.get_list(IdEmpresa, IdSucursal, true, ViewBag.fecha_ini, ViewBag.fecha_fin);
            return PartialView("_GridViewPartial_ajuste_fisico", model);
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
            in_parametro_Info i_param = bus_in_param.get_info(IdEmpresa);
            if (i_param == null)
                return RedirectToAction("Index");

            in_Ajuste_Info model = new in_Ajuste_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdEmpresa = IdEmpresa,
                Fecha = DateTime.Now,
                IdCatalogo_Estado = i_param.IdCatalogoEstadoAjuste
            };

            ListaDetalle.set_list(new List<in_AjusteDet_Info>(), model.IdTransaccionSession);
            cargar_combos(model);

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(in_Ajuste_Info model)
        {
            model.lst_detalle = ListaDetalle.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            model.Estado = true;

            if (!bus_ajuste.guardarDB(model))
            {
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdAjuste = model.IdAjuste, Exito = true });
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursal = 0, int IdAjuste = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            in_Ajuste_Info model = bus_ajuste.get_info(IdEmpresa, IdSucursal, IdAjuste);
            if (model == null)
                return RedirectToAction("Index");

            model.lst_detalle = bus_ajuste_det.get_list(IdEmpresa, IdAjuste);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            foreach (var item in model.lst_detalle)
            {
                in_Producto_Info info_producto = bus_producto.get_info(model.IdEmpresa,item.IdProducto);
                item.pr_descripcion = info_producto.pr_descripcion;
            }

            ListaDetalle.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos(model);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.INV, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(in_Ajuste_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            model.lst_detalle = ListaDetalle.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }            

            if (!bus_ajuste.modificarDB(model))
            {
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdAjuste = model.IdAjuste, Exito = true });
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdSucursal = 0, decimal IdAjuste = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            in_Ajuste_Info model = bus_ajuste.get_info(IdEmpresa, IdSucursal, IdAjuste);
            if (model == null)
                return RedirectToAction("Index");
            
            model.lst_detalle = bus_ajuste_det.get_list(IdEmpresa, IdAjuste);

            foreach (var item in model.lst_detalle)
            {
                in_Producto_Info info_producto = bus_producto.get_info(model.IdEmpresa, item.IdProducto);
                item.pr_descripcion = info_producto.pr_descripcion;
            }

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            ListaDetalle.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos(model);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.INV, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(in_Ajuste_Info model)
        {
            model.Estado = false;
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            model.lst_detalle = ListaDetalle.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }            

            if (!bus_ajuste.anularDB(model))
            {
                cargar_combos(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Funciones del detalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ajuste_fisico_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ajuste_fisico_det", model);
        }
        private void cargar_combos_detalle()
        {
            in_UnidadMedida_Bus bus_unidad = new in_UnidadMedida_Bus();
            var lst_unidad = bus_unidad.get_list(false);
            ViewBag.lst_unidad = lst_unidad;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] in_AjusteDet_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                        
                    }
                }

            ListaDetalle.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ajuste_fisico_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] in_AjusteDet_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                    }
                }

            ListaDetalle.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ajuste_fisico_det", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            ListaDetalle.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            in_Ajuste_Info model = new in_Ajuste_Info();
            model.lst_detalle = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ajuste_fisico_det", model.lst_detalle);
        }
        #endregion

        #region Json
        public JsonResult CargarBodega(int IdEmpresa = 0, int IdSucursal = 0)
        {
            bus_bodega = new tb_bodega_Bus();
            var resultado = bus_bodega.get_list(IdEmpresa, IdSucursal, false);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarDetalleAjuste(decimal IdTransaccionSession, DateTime Fecha, int IdEmpresa = 0, int IdSucursal = 0, int IdBodega=0)
        {
            var resultado = bus_ajuste_det.get_list_cargar_detalle(Fecha, IdEmpresa, IdSucursal, IdBodega);
            ListaDetalle.set_list(resultado, IdTransaccionSession);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Validacion
        private bool validar(in_Ajuste_Info i_validar, ref string msg)
        {
            if (i_validar.lst_detalle.Count == 0)
            {
                mensaje = "Debe ingresar al menos un producto";
                return false;
            }

            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.Fecha, cl_enumeradores.eModulo.INV, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            return true;
        }
        private void cargar_combos(in_Ajuste_Info model)
        {
            var lst_estado = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoInventario.EST_APROB), false);
            ViewBag.lst_estado = lst_estado;

            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            var lst_bodega = bus_bodega.get_list(model.IdEmpresa, model.IdSucursal, false);
            ViewBag.lst_bodega = lst_bodega;

            in_UnidadMedida_Bus bus_unidad = new in_UnidadMedida_Bus();
            var lst_unidad = bus_unidad.get_list(false);
            ViewBag.lst_unidad = lst_unidad;
        }
        #endregion
    }

    public class in_AjusteDet_List
    {
        string Variable = "in_AjusteDet_Info";
        public List<in_AjusteDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_AjusteDet_Info> list = new List<in_AjusteDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_AjusteDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_AjusteDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(in_AjusteDet_Info info_det, decimal IdTransaccionSession)
        {
            List<in_AjusteDet_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.IdProducto = info_det.IdProducto;
            info_det.IdUnidadMedida = info_det.IdUnidadMedida;
            list.Add(info_det);
        }

        public void UpdateRow(in_AjusteDet_Info info_det, decimal IdTransaccionSession)
        {
            in_AjusteDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();

            edited_info.StockFisico = info_det.StockFisico;
            edited_info.Ajuste = info_det.StockFisico - info_det.StockSistema;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<in_AjusteDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}