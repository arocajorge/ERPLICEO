using Core.Erp.Info.Inventario;
using Core.Erp.Bus.Inventario;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.General;
using DevExpress.Web;
using System.Web.UI;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Info.General;
using Core.Erp.Bus.Facturacion;
using Core.Erp.Info.Facturacion;
using System.IO;
using ExcelDataReader;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class ProductoController : Controller
    {
        #region variables
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        in_Producto_Composicion_List list_producto_composicion = new in_Producto_Composicion_List();
        in_Producto_x_fa_NivelDescuesto_List list_producto_x_fa_NivelDescuento = new in_Producto_x_fa_NivelDescuesto_List();
        in_Producto_Composicion_Bus bus_producto_composicion = new in_Producto_Composicion_Bus();
        fa_NivelDescuento_Bus bus_nivel_descuento = new fa_NivelDescuento_Bus();
        in_ProductoTipo_Bus bus_producto_tipo = new in_ProductoTipo_Bus();
        in_Producto_x_fa_NivelDescuento_Bus bus_producto_x_NivelDescuento = new in_Producto_x_fa_NivelDescuento_Bus();
        in_producto_x_tb_bodega_Info_List Lis_in_producto_x_tb_bodega_Info_List = new in_producto_x_tb_bodega_Info_List();
        in_Producto_x_fa_NivelDescuesto_List list_producto_x_nivel_descuento = new in_Producto_x_fa_NivelDescuesto_List();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
        in_producto_x_tb_bodega_Bus bus_producto_x_bodega = new in_producto_x_tb_bodega_Bus();
        seg_usuario_Bus bus_usuarios = new seg_usuario_Bus();
        tbl_TransaccionesAutorizadas_Bus bus_transacciones_aut = new tbl_TransaccionesAutorizadas_Bus();
        in_UnidadMedida_Equiv_conversion_Bus bus_UnidadMedidaEquivalencia = new in_UnidadMedida_Equiv_conversion_Bus();


        private string mensaje;

        in_Prod_List Lista_Producto = new in_Prod_List();
        in_categorias_List Lista_Categoria = new in_categorias_List();
        in_linea_List Lista_Linea = new in_linea_List();
        in_grupo_List Lista_Grupo = new in_grupo_List();
        in_subgrupo_List Lista_Subgrupo = new in_subgrupo_List();
        in_Marca_List Lista_Marca = new in_Marca_List();
        in_presentacion_List Lista_Presentacion = new in_presentacion_List();
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProducto_composicion()
        {
            in_Producto_Info model = new in_Producto_Info();
            return PartialView("_CmbProducto_composicion", model);
        }
        public ActionResult CmbProducto_padre()
        {
            in_Producto_Info model = new in_Producto_Info();
            return PartialView("_CmbProducto_padre", model);
        }
        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa),cl_enumeradores.eTipoBusquedaProducto.SOLOHIJOS,cl_enumeradores.eModulo.INV,0,0);
        }
        public List<in_Producto_Info> get_list_bajo_demandaComposicion(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.TODOS_MENOS_PADRES, cl_enumeradores.eModulo.INV, 0,0);
        }
        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }




        public ActionResult CmbSucursal_det()
        {
            in_Producto_Info model = new in_Producto_Info();
            return PartialView("_CmbSucursal_det", model);
        }
        public List<tb_sucursal_Info> get_list_bajo_demanda_sucursal(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_sucursal.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }

        public tb_sucursal_Info get_info_bajo_demanda_sucursal(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_sucursal.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }
        #endregion

        #region vistas

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_producto()
        {

            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            List<in_Producto_Info> model = bus_producto.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_producto", model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_producto_por_bodega()
        {
            cargar_combos_detalle();
            in_Producto_Info model = new in_Producto_Info();
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            model.lst_producto_x_bodega = Lis_in_producto_x_tb_bodega_Info_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            return PartialView("_GridViewPartial_producto_por_bodega", model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_producto_x_niveldescuento()
        {
            cargar_combos_producto_x_NivelDescuento();
            in_Producto_Info model = new in_Producto_Info();
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            model.list_producto_x_fa_NivelDescuento = list_producto_x_fa_NivelDescuento.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            return PartialView("_GridViewPartial_producto_x_niveldescuento", model);
        }
        public List<in_Producto_Info> get_lst_productos()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            List<in_Producto_Info> model = bus_producto.get_list(IdEmpresa, true);
            return model;
        }


        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion
                in_Producto_Info model = new in_Producto_Info
                {
                    IdEmpresa = IdEmpresa,
                    IdCod_Impuesto_Iva = "IVA0",
                    IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                    lst_producto_composicion = new List<in_Producto_Composicion_Info>(),
                    IdUnidadMedida = "UNID",
                    IdUnidadMedida_Consumo = "UNID"
                };
                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                var lst_producto_x_bodega = bus_producto_x_bodega.get_list(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSucursal));
                //var lst_producto_x_nivel = bus_producto_x_NivelDescuento.get_list(Convert.ToInt32(SessionFixed.IdEmpresa));
                
                model.pr_imagen = new byte[0];
                list_producto_composicion.set_list(model.lst_producto_composicion, model.IdTransaccionSession);
                Lis_in_producto_x_tb_bodega_Info_List.set_list(lst_producto_x_bodega, model.IdTransaccionSession);
                list_producto_x_fa_NivelDescuento.set_list(new List<in_Producto_x_fa_NivelDescuento_Info>(), model.IdTransaccionSession);

                cargar_combos(model);
                return View(model);
            }
            catch (Exception)
            {
                throw;
               
            }
        }
        [HttpPost]
        public ActionResult Nuevo(in_Producto_Info model)
        {
            try
            {
                bus_producto = new in_Producto_Bus();
                model.IdUsuario = SessionFixed.IdUsuario.ToString();
                model.pr_imagen = Producto_imagen.pr_imagen;
                model.lst_producto_composicion = list_producto_composicion.get_list(model.IdTransaccionSession);
                model.lst_producto_x_bodega = Lis_in_producto_x_tb_bodega_Info_List.get_list(Convert.ToInt32(model.IdTransaccionSession));
                if (model.lst_producto_x_bodega == null)
                    model.lst_producto_x_bodega = new List<in_producto_x_tb_bodega_Info>();

                if (!validar(model, ref mensaje))
                {
                    if (model.pr_imagen == null)
                        model.pr_imagen = new byte[0];
                    cargar_combos(model);
                    ViewBag.mensaje = mensaje;
                    return View(model);
                }

                if (!bus_producto.guardarDB(model))
                {
                    if (model.pr_imagen == null)
                        model.pr_imagen = new byte[0];
                    cargar_combos(model);
                    return View(model);
                }

                model.list_producto_x_fa_NivelDescuento = list_producto_x_fa_NivelDescuento.get_list(model.IdTransaccionSession);
                model.list_producto_x_fa_NivelDescuento.ForEach(q => { q.IdEmpresa = model.IdEmpresa; q.IdProducto = model.IdProducto; });
                bus_producto_x_NivelDescuento.eliminarDB(model.IdEmpresa, model.IdProducto);

                if (!bus_producto_x_NivelDescuento.guardarDB(model.list_producto_x_fa_NivelDescuento))
                {
                    cargar_combos(model);
                    return View(model);
                }

                Producto_imagen.pr_imagen = null;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (model.pr_imagen == null)
                    model.pr_imagen = new byte[0];
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());

                ViewBag.mensaje = ex.Message.ToString();
                cargar_combos(model);
                return View(model);
            }
        }
        public ActionResult Modificar(int IdEmpresa = 0 , decimal IdProducto = 0)
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion
                in_Producto_Info model = bus_producto.get_info(IdEmpresa, IdProducto);
                if (model == null)
                    return RedirectToAction("Index");
                cargar_combos(model);
                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                model.list_producto_x_fa_NivelDescuento = bus_producto_x_NivelDescuento.get_list(model.IdEmpresa, model.IdProducto);
                model.lst_producto_composicion = bus_producto_composicion.get_list(model.IdEmpresa, model.IdProducto);
                model.lst_producto_x_bodega = bus_producto_x_bodega.get_list(model.IdEmpresa, model.IdProducto);
                Lis_in_producto_x_tb_bodega_Info_List.set_list(model.lst_producto_x_bodega, model.IdTransaccionSession);
                list_producto_composicion.set_list(model.lst_producto_composicion, model.IdTransaccionSession);
                list_producto_x_fa_NivelDescuento.set_list(model.list_producto_x_fa_NivelDescuento, model.IdTransaccionSession);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(in_Producto_Info model)
        {
            try
            {
                bus_producto = new in_Producto_Bus();

                model.lst_producto_x_bodega = Lis_in_producto_x_tb_bodega_Info_List.get_list(Convert.ToInt32(model.IdTransaccionSession));
                if (model.lst_producto_x_bodega == null)
                    model.lst_producto_x_bodega = new List<in_producto_x_tb_bodega_Info>();

                model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();

                model.pr_imagen = Producto_imagen.pr_imagen;
                if (!validar(model, ref mensaje))
                {
                    if (model.pr_imagen == null)
                        model.pr_imagen = new byte[0];
                    cargar_combos(model);
                    ViewBag.mensaje = mensaje;
                    return View(model);
                }

                if (!bus_producto.modificarDB(model))
                {
                    if (model.pr_imagen == null)
                        model.pr_imagen = new byte[0];
                    cargar_combos(model);
                    return View(model);
                }

                model.lst_producto_composicion = list_producto_composicion.get_list(model.IdTransaccionSession);
                model.lst_producto_composicion.ForEach(q => { q.IdEmpresa = model.IdEmpresa; q.IdProductoPadre = model.IdProducto; });
                bus_producto_composicion.eliminarDB(model.IdEmpresa, model.IdProducto);
                if (!bus_producto_composicion.guardarDB(model.lst_producto_composicion))
                {
                    cargar_combos(model);
                    return View(model);
                }

                model.list_producto_x_fa_NivelDescuento = list_producto_x_fa_NivelDescuento.get_list(model.IdTransaccionSession);
                model.list_producto_x_fa_NivelDescuento.ForEach(q => { q.IdEmpresa = model.IdEmpresa; q.IdProducto = model.IdProducto; });
                bus_producto_x_NivelDescuento.eliminarDB(model.IdEmpresa, model.IdProducto);

                if (!bus_producto_x_NivelDescuento.guardarDB(model.list_producto_x_fa_NivelDescuento))
                {
                    cargar_combos(model);
                    return View(model);
                }

                Producto_imagen.pr_imagen = null;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (model.pr_imagen == null)
                    model.pr_imagen = new byte[0];
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());
                ViewBag.error = ex.Message.ToString();
                cargar_combos(model);
                return View(model);
            }
        }
        public ActionResult Anular(int IdEmpresa = 0 , decimal IdProducto = 0)
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion
                in_Producto_Info model = bus_producto.get_info(IdEmpresa, IdProducto);
                if (model == null)
                    return RedirectToAction("Index");
                cargar_combos(model);
                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                model.list_producto_x_fa_NivelDescuento = bus_producto_x_NivelDescuento.get_list(model.IdEmpresa, model.IdProducto);
                model.lst_producto_composicion = bus_producto_composicion.get_list(model.IdEmpresa, model.IdProducto);
                model.lst_producto_x_bodega = bus_producto_x_bodega.get_list(model.IdEmpresa, model.IdProducto);
                Lis_in_producto_x_tb_bodega_Info_List.set_list(model.lst_producto_x_bodega, model.IdTransaccionSession);
                list_producto_composicion.set_list(model.lst_producto_composicion, model.IdTransaccionSession);
                list_producto_x_fa_NivelDescuento.set_list(model.list_producto_x_fa_NivelDescuento, model.IdTransaccionSession);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Anular(in_Producto_Info model)
        {
            try
            {
                model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
                if (!bus_producto.validar_anulacion(model.IdEmpresa, model.IdProducto, ref mensaje))
                {
                    cargar_combos(model);
                    ViewBag.mensaje = mensaje;
                    return View(model);
                }
                if (!bus_producto.anularDB(model))
                {
                    cargar_combos(model);
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                if (model.pr_imagen == null)
                    model.pr_imagen = new byte[0];
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());

                ViewBag.mensaje = ex.Message.ToString();
                cargar_combos(model);
                return View(model);
            }
        }

        #endregion

        #region Json
        public JsonResult cargar_lineas(string IdCategoria = "")
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            in_linea_Bus bus_linea = new in_linea_Bus();
            var resultado = bus_linea.get_list(IdEmpresa, IdCategoria, false);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult cargar_grupos(string IdCategoria = "", int IdLinea = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            in_grupo_Bus bus_grupo = new in_grupo_Bus();
            var resultado = bus_grupo.get_list(IdEmpresa, IdCategoria, IdLinea, false);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult cargar_subgrupos(string IdCategoria = "", int IdLinea = 0, int IdGrupo = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            in_subgrupo_Bus bus_subgrupo = new in_subgrupo_Bus();
            var resultado = bus_subgrupo.get_list(IdEmpresa, IdCategoria, IdLinea,IdGrupo, false);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_info_padre(decimal IdProducto = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            var resultado = bus_producto.get_info(IdEmpresa, IdProducto);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Guardar_lote(DateTime?  fecha_fab, DateTime? fecha_ven, string lote="")
        {
            decimal IdProducto_padre = 0;
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (SessionFixed.IdProducto_padre_dist!=null)
            {
               IdProducto_padre = Convert.ToDecimal(SessionFixed.IdProducto_padre_dist);
            }
            bus_producto.guardar_loteDB(IdEmpresa, IdProducto_padre, fecha_fab == null ? DateTime.MinValue : Convert.ToDateTime(fecha_fab), Convert.ToDateTime( fecha_ven), lote);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetUnidadMedida(decimal IdProducto = 0)
        {
            in_Producto_Bus bus_producto = new in_Producto_Bus();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var resultado = bus_producto.get_info(IdEmpresa, IdProducto);
            if (resultado == null)
                resultado = new in_Producto_Info();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MostrarPrecios(string IdUsuarioAut = "", string contrasena_admin = "", decimal IdProducto=0)
        {
            string EstadoDesbloqueo = "NOAUTORIZADO"; 
             var info_usuarios = bus_usuarios.get_info(IdUsuarioAut);
            if (info_usuarios != null)
            {
                if (info_usuarios.es_super_admin)
                {
                    if (contrasena_admin.ToLower() == info_usuarios.contrasena_admin.ToLower())
                    {
                        tbl_TransaccionesAutorizadas_info info_trasnsaccion_aut = new tbl_TransaccionesAutorizadas_info
                        {
                            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                            IdUsuarioAut = IdUsuarioAut,
                            IdUsuarioLog = SessionFixed.IdUsuario,
                            Observacion = "Desbloqueo de pestaña de precio para el producto con ID #" + IdProducto.ToString(),
                        };
                        bus_transacciones_aut.guardarDB(info_trasnsaccion_aut);
                        EstadoDesbloqueo = "AUTORIZADO";
                    }
                }
                else
                    EstadoDesbloqueo = "NOAUTORIZADO";

            }

            return Json(EstadoDesbloqueo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region cargar combo
        private bool validar(in_Producto_Info i_validar, ref string msg)
        {
            var tipo = bus_producto_tipo.get_info(i_validar.IdEmpresa, i_validar.IdProductoTipo);
            if(tipo == null)
            {
                msg = "Seleccion el tipo de producto";
                return false;
            }
           /* if (tipo.tp_es_lote && string.IsNullOrEmpty(i_validar.lote_num_lote) )
            {
                msg = "Ingrese el código del lote";
                return false;
            }
            if (tipo.tp_es_lote && i_validar.lote_fecha_vcto == null)
            {
                msg = "Ingrese la fecha de vencimiento del lote";
                return false;
            }*/

            return true;
        }
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            in_UnidadMedida_Bus bus_unidad_medida = new in_UnidadMedida_Bus();
            var lst_unidad_medida = bus_unidad_medida.get_list(false);
            ViewBag.lst_unidad_medida = lst_unidad_medida;
            var lst_susucrsal = bus_sucursal.get_list(IdEmpresa, false);
            var lst_bodega = bus_bodega.get_list(IdEmpresa, false);


        }
        private void cargar_combos(in_Producto_Info model)
        {
            
            var lst_producto_tipo = bus_producto_tipo.get_list(model.IdEmpresa, false);
            ViewBag.lst_producto_tipo = lst_producto_tipo;

            Dictionary<string, string> lst_signos = new Dictionary<string, string>();
            lst_signos.Add("-", "-");
            lst_signos.Add("+", "+");
            ViewBag.lst_signos = lst_signos;

            in_categorias_Bus bus_categoria = new in_categorias_Bus();
            var lst_categoria = bus_categoria.get_list(model.IdEmpresa, false);
            ViewBag.lst_categoria = lst_categoria;

            in_presentacion_Bus bus_presentacion = new in_presentacion_Bus();
            var lst_presentacion = bus_presentacion.get_list(model.IdEmpresa, false);
            ViewBag.lst_presentacion = lst_presentacion;

            in_Marca_Bus bus_marca = new in_Marca_Bus();
            var lst_marca = bus_marca.get_list(model.IdEmpresa, false);
            ViewBag.lst_marca = lst_marca;

            in_linea_Bus bus_linea = new in_linea_Bus();
            var lst_linea = bus_linea.get_list(model.IdEmpresa, model.IdCategoria, false);
            ViewBag.lst_linea = lst_linea;

            in_grupo_Bus bus_grupo = new in_grupo_Bus();
            var lst_grupo = bus_grupo.get_list(model.IdEmpresa, model.IdCategoria, model.IdLinea, false);
            ViewBag.lst_grupo = lst_grupo;

            in_subgrupo_Bus bus_subgrupo = new in_subgrupo_Bus();
            var lst_subgrupo = bus_subgrupo.get_list(model.IdEmpresa, model.IdCategoria, model.IdLinea, model.IdGrupo, false);
            ViewBag.lst_subgrupo = lst_subgrupo;

            in_UnidadMedida_Bus bus_unidad_medida = new in_UnidadMedida_Bus();
            var lst_unidad_medida = bus_unidad_medida.get_list(false);
            ViewBag.lst_unidad_medida = lst_unidad_medida;

            var lst_producto_padre = bus_producto.get_list_padres(model.IdEmpresa, false);
            ViewBag.lst_producto_padre = lst_producto_padre;

            tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
            var lst_impuesto = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuesto = lst_impuesto;
        }

        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
           
            var lst_susucrsal = bus_sucursal.get_list(IdEmpresa, false);
            var lst_bodega = bus_bodega.get_list(IdEmpresa, false);
            ViewBag.lst_susucrsal = lst_susucrsal;
            ViewBag.lst_bodega = lst_bodega;

        }
        private void cargar_combos_producto_x_NivelDescuento()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            var lst_NivelDescuento = bus_nivel_descuento.GetList(IdEmpresa, false);
            ViewBag.lst_NivelDescuento = lst_NivelDescuento;

        }

        public ActionResult CargarBodega()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);        
            int IdSucursal = Request.Params["IdSucursal"] != null ? Convert.ToInt32(Request.Params["IdSucursal"].ToString()) : 0;
            return GridViewExtension.GetComboBoxCallbackResult(p =>
            {
                p.TextField = "bo_Descripcion";
                p.ValueField = "IdString";
                p.Columns.Add("bo_Descripcion","Bodega");
                p.TextFormatString = "{0}";
                p.ValueType = typeof(string);
                p.BindList(bus_bodega.get_list(IdEmpresa, IdSucursal, false));
            });
        }
        #endregion

        #region funciones del detalle producto por nivel de descuento
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew_pro_x_desc([ModelBinder(typeof(DevExpressEditorsBinder))] in_Producto_x_fa_NivelDescuento_Info info_det)
        {
            in_Producto_Info model = new in_Producto_Info();
            if (ModelState.IsValid)
            {
                in_Producto_x_fa_NivelDescuento_Info info_pro_x_nivel_desc = new in_Producto_x_fa_NivelDescuento_Info();
                info_pro_x_nivel_desc.IdNivel = info_det.IdNivel;
                info_pro_x_nivel_desc.Porcentaje = info_det.Porcentaje;

                var lista = list_producto_x_fa_NivelDescuento.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

                if (lista.Where(v => v.IdNivel == info_det.IdNivel).Count() == 0)
                    list_producto_x_fa_NivelDescuento.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            }
            cargar_combos_producto_x_NivelDescuento();            
            model.list_producto_x_fa_NivelDescuento = list_producto_x_fa_NivelDescuento.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_producto_x_niveldescuento", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_pro_x_desc([ModelBinder(typeof(DevExpressEditorsBinder))] in_Producto_x_fa_NivelDescuento_Info info_det)
        {
            in_Producto_Info model = new in_Producto_Info();
            if (ModelState.IsValid)
            {
                list_producto_x_fa_NivelDescuento.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            model.list_producto_x_fa_NivelDescuento = list_producto_x_fa_NivelDescuento.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_producto_x_NivelDescuento();
            return PartialView("_GridViewPartial_producto_x_niveldescuento", model);
        }

        public ActionResult EditingDelete_pro_x_desc(int Secuencia = 0)
        {
            in_Producto_Info model = new in_Producto_Info();
            list_producto_x_fa_NivelDescuento.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            model.list_producto_x_fa_NivelDescuento = list_producto_x_fa_NivelDescuento.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_producto_x_NivelDescuento();
            return PartialView("_GridViewPartial_producto_x_niveldescuento", model);
        }
        #endregion

        #region funciones del detalle composicion

        [ValidateInput(false)]
        public ActionResult GridViewPartial_producto_composicion(decimal IdProducto = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            in_Producto_Info model = new in_Producto_Info();
            model.lst_producto_composicion = bus_producto_composicion.get_list(IdEmpresa, IdProducto);
            if (model.lst_producto_composicion.Count == 0)
                model.lst_producto_composicion = list_producto_composicion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            model.lst_producto_composicion = list_producto_composicion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos();
            return PartialView("_GridViewPartial_producto_composicion", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] in_Producto_Composicion_Info info_det)
        {
            in_Producto_Info model = new in_Producto_Info();
            if (ModelState.IsValid)
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                in_Producto_Info info_p = new in_Producto_Info();
                if (info_det != null)
                    if (info_det.IdProductoHijo != 0)
                        info_p = bus_producto.get_info(IdEmpresa, info_det.IdProductoHijo);
                if (info_p != null)
                {
                    info_det.pr_descripcion = info_p.pr_descripcion;
                    //info_det.IdUnidadMedida = info_p.IdUnidadMedida;
                }
                list_producto_composicion.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            model.lst_producto_composicion = list_producto_composicion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_producto_composicion", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] in_Producto_Composicion_Info info_det)
        {
            in_Producto_Info model = new in_Producto_Info();
            if (ModelState.IsValid)
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                in_Producto_Info info_p = new in_Producto_Info();
                if (info_det != null)
                    if (info_det.IdProductoHijo != 0)
                        info_p = bus_producto.get_info(IdEmpresa, info_det.IdProductoHijo);
                if (info_p != null)
                {
                    info_det.pr_descripcion = info_p.pr_descripcion;
                    //info_det.IdUnidadMedida = info_p.IdUnidadMedida;
                }
                list_producto_composicion.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            model.lst_producto_composicion = list_producto_composicion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_producto_composicion", model);
        }

        public ActionResult EditingDelete(int secuencia)
        {
            list_producto_composicion.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            in_Producto_Info model = new in_Producto_Info();
            model.lst_producto_composicion = list_producto_composicion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos();
            return PartialView("_GridViewPartial_producto_composicion", model);
        }
        #endregion

        #region funciones del detalle producto por bodega

     
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew_pro_x_bod([ModelBinder(typeof(DevExpressEditorsBinder))] in_producto_x_tb_bodega_Info info_det )
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if(info_det!= null)
            {
                var suc = bus_sucursal.get_info(IdEmpresa, info_det.IdSucursal);

                info_det.IdBodega = string.IsNullOrEmpty(info_det.IdString) ? 0 : Convert.ToInt32(info_det.IdString.Substring(3, 3));

                var bod = bus_bodega.get_info(IdEmpresa, info_det.IdSucursal, info_det.IdBodega);
                if(suc!= null && bod !=null)
                {
                    info_det.IdSucursal = info_det.IdSucursal;
                    info_det.Su_Descripcion = suc.Su_Descripcion;
                    info_det.IdBodega = info_det.IdBodega;
                    info_det.bo_Descripcion = bod.bo_Descripcion;
                }
            }
            if (ModelState.IsValid)
            {
                in_producto_x_tb_bodega_Info info_pro_x_bode = new in_producto_x_tb_bodega_Info();
                info_pro_x_bode.IdSucursal = info_det.IdSucursal;
                info_pro_x_bode.Su_Descripcion = info_det.Su_Descripcion;
                info_pro_x_bode.IdBodega = info_det.IdBodega;
                var lista = Lis_in_producto_x_tb_bodega_Info_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                Lis_in_producto_x_tb_bodega_Info_List.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            }
            cargar_combos_detalle();
            in_Producto_Info model = new in_Producto_Info();
            model.lst_producto_x_bodega = Lis_in_producto_x_tb_bodega_Info_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_producto_por_bodega", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_pro_x_bod([ModelBinder(typeof(DevExpressEditorsBinder))] in_producto_x_tb_bodega_Info info_det)
        {
            in_Producto_Info model = new in_Producto_Info();
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);

            if (info_det != null)
            {
                var suc = bus_sucursal.get_info(IdEmpresa, info_det.IdSucursal);

                info_det.IdBodega = string.IsNullOrEmpty(info_det.IdString) ? 0 : Convert.ToInt32(info_det.IdString.Substring(3, 3));

                var bod = bus_bodega.get_info(IdEmpresa, info_det.IdSucursal, info_det.IdBodega);
                if (suc != null && bod != null)
                {
                    info_det.IdSucursal = info_det.IdSucursal;
                    info_det.Su_Descripcion = suc.Su_Descripcion;
                    info_det.IdBodega = info_det.IdBodega;
                    info_det.bo_Descripcion = bod.bo_Descripcion;
                }
            }

            if (ModelState.IsValid)
            {               
                Lis_in_producto_x_tb_bodega_Info_List.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            model.lst_producto_x_bodega = Lis_in_producto_x_tb_bodega_Info_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_producto_por_bodega", model);
        }

        public ActionResult EditingDelete_pro_x_bod(int Secuencia=0)
        {
            in_Producto_Info model = new in_Producto_Info();
            cargar_combos_detalle();
            Lis_in_producto_x_tb_bodega_Info_List.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            model.lst_producto_x_bodega = Lis_in_producto_x_tb_bodega_Info_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_producto_por_bodega", model);
        }
        #endregion

        #region Imagen

        const string UploadDirectory = "~/Content/imagenes/";

        public UploadedFile UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControl", Producto_imagen.UploadValidationSettings, Producto_imagen.FileUploadComplete);

            byte[] model = Producto_imagen.pr_imagen;
            UploadedFile file = new UploadedFile();
            return file;
        }

        public ActionResult get_imagen()
        {

            byte[] model = Producto_imagen.pr_imagen;
            if (model == null)
                model = new byte[0];
            return PartialView("_Producto_imagen", model);
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


        #region Importacion
        public ActionResult UploadControlUploadImp()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings.UploadValidationSettings, UploadControlSettings.FileUploadComplete);
            return null;
        }
        public ActionResult Importar(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            in_Producto_Info model = new in_Producto_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(in_Producto_Info model)
        {
            try
            {
                var ListaSubgrupo = Lista_Subgrupo.get_list(model.IdTransaccionSession);
                var ListaPresentacion = Lista_Presentacion.get_list(model.IdTransaccionSession);
                var ListaMarca = Lista_Marca.get_list(model.IdTransaccionSession);
                var ListaProducto = Lista_Producto.get_list(model.IdTransaccionSession);
                
                if (!bus_producto.GuardarDbImportacion(ListaSubgrupo, ListaPresentacion, ListaMarca, ListaProducto))
                {
                    ViewBag.mensaje = "Error al importar el archivo";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());

                ViewBag.error = ex.Message.ToString();
                return View(model);
            }

            return RedirectToAction("Index");
        }
        public ActionResult GridViewPartial_SubgrupoPro_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Subgrupo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_SubgrupoPro_importacion", model);
        }
        public ActionResult GridViewPartial_PresentacionPro_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Presentacion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_PresentacionPro_importacion", model);
        }
        public ActionResult GridViewPartial_MarcaPro_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Marca.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MarcaPro_importacion", model);
        }
        public ActionResult GridViewPartial_ProductoPro_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Producto.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ProductoPro_importacion", model);
        }
        public JsonResult ActualizarVariablesSession(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            string retorno = string.Empty;
            SessionFixed.IdEmpresa = IdEmpresa.ToString();
            SessionFixed.IdTransaccionSession = IdTransaccionSession.ToString();
            SessionFixed.IdTransaccionSessionActual = IdTransaccionSession.ToString();
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
    public class Producto_imagen
    {
        public static byte[] pr_imagen { get; set; }
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg" },
            MaxFileSize = 4000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                pr_imagen = e.UploadedFile.FileBytes;
            }
        }
    }
    public class UploadControlSettings
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables

            in_categorias_List Lista_Categoria = new in_categorias_List();
            in_linea_List Lista_Linea = new in_linea_List();
            in_grupo_List Lista_Grupo = new in_grupo_List();
            in_subgrupo_List Lista_Subgrupo = new in_subgrupo_List();
            in_presentacion_List Lista_Presentacion = new in_presentacion_List();
            in_Marca_List Lista_Marca = new in_Marca_List();
            in_Prod_List Lista_Producto = new in_Prod_List();

            List<in_categorias_Info> ListaCategoria = new List<in_categorias_Info>();
            List<in_linea_Info> ListaLinea = new List<in_linea_Info>();
            List<in_grupo_Info> ListaGrupo = new List<in_grupo_Info>();
            List<in_subgrupo_Info> ListaSubgrupo = new List<in_subgrupo_Info>();
            List<in_presentacion_Info> ListaPresentacion = new List<in_presentacion_Info>();
            List<in_Marca_Info> ListaMarca = new List<in_Marca_Info>();
            List<in_Producto_Info> ListaProducto = new List<in_Producto_Info>();
            in_Producto_Bus bus_producto = new in_Producto_Bus();
            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            #endregion

            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                #region Categoria                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        in_subgrupo_Info info = new in_subgrupo_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdCategoria = Convert.ToString(reader.GetValue(0)).Trim(),
                            NomCategoria = Convert.ToString(reader.GetValue(1)).Trim(),
                            IdLinea = Convert.ToInt32(reader.GetValue(2)),
                            NomLinea = Convert.ToString(reader.GetValue(3)).Trim(),
                            IdGrupo = Convert.ToInt32(reader.GetValue(4)),
                            NomGrupo = Convert.ToString(reader.GetValue(5)).Trim(),
                            IdSubgrupo = Convert.ToInt32(reader.GetValue(6)),
                            nom_subgrupo = Convert.ToString(reader.GetValue(7)).Trim(),
                            IdUsuario = SessionFixed.IdUsuario
                        };
                        ListaSubgrupo.Add(info);
                    }
                    else
                        cont++;
                }
                Lista_Subgrupo.set_list(ListaSubgrupo, IdTransaccionSession);

                #endregion

                cont = 0;
                //Para avanzar a la siguiente hoja de excel
                reader.NextResult();
               

                #region Presentacion                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        in_presentacion_Info info = new in_presentacion_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdPresentacion = Convert.ToString(reader.GetValue(0)),
                            nom_presentacion = Convert.ToString(reader.GetValue(1))
                        };
                        ListaPresentacion.Add(info);
                    }
                    else
                        cont++;
                }
                Lista_Presentacion.set_list(ListaPresentacion, IdTransaccionSession);
                #endregion

                cont = 0;
                reader.NextResult();

                #region Marca                
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        in_Marca_Info info = new in_Marca_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdMarca = Convert.ToInt32(reader.GetValue(0)),
                            Descripcion = Convert.ToString(reader.GetValue(1)),
                            IdUsuario = SessionFixed.IdUsuario
                        };

                        ListaMarca.Add(info);
                    }
                    else
                        cont++;
                }
                Lista_Marca.set_list(ListaMarca, IdTransaccionSession);
                #endregion

                cont = 0;
                reader.NextResult();

                #region Producto   
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        if (!bus_producto.ValidarCodigoExists(IdEmpresa, Convert.ToString(reader.GetValue(1)).Trim()))
                        {
                            in_Producto_Info info = new in_Producto_Info
                            {
                                IdEmpresa = IdEmpresa,
                                IdProducto = Convert.ToInt32(reader.GetValue(0)),
                                pr_codigo = Convert.ToString(reader.GetValue(1)).Trim(),
                                pr_descripcion = string.IsNullOrEmpty(Convert.ToString(reader.GetValue(2))) ? null : Convert.ToString(reader.GetValue(2)).Trim(),
                                pr_descripcion_2 = string.IsNullOrEmpty(Convert.ToString(reader.GetValue(2))) ? null : Convert.ToString(reader.GetValue(2)).Trim(),
                                IdMarca = Convert.ToInt32(reader.GetValue(3)),
                                IdPresentacion = Convert.ToString(reader.GetValue(4)),
                                IdCategoria = Convert.ToString(reader.GetValue(5)),
                                IdLinea = Convert.ToInt32(reader.GetValue(6)),
                                IdGrupo = Convert.ToInt32(reader.GetValue(7)),
                                IdSubGrupo = Convert.ToInt32(reader.GetValue(8)),
                                IdCod_Impuesto_Iva = Convert.ToString(reader.GetValue(9)),
                                IdUnidadMedida = Convert.ToString(reader.GetValue(10)),
                                IdUnidadMedida_Consumo = Convert.ToString(reader.GetValue(11)),
                                precio_1 = Convert.ToDouble(reader.GetValue(12)),
                                IdProductoTipo = 1,
                            };
                            ListaProducto.Add(info);
                        }                        
                    }
                    else
                        cont++;
                }
                Lista_Producto.set_list(ListaProducto, IdTransaccionSession);
                #endregion

                cont = 0;
                reader.NextResult();

            }
        }
    }

    public class in_Producto_Composicion_List
    {
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        string Variable = "in_Producto_Composicion_Info";

        public List<in_Producto_Composicion_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_Producto_Composicion_Info> list = new List<in_Producto_Composicion_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_Producto_Composicion_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_Producto_Composicion_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(in_Producto_Composicion_Info info_det, decimal IdTransaccionSession)
        {
            List<in_Producto_Composicion_Info> list = get_list(IdTransaccionSession);
            
            info_det.secuencia = list.Count == 0 ? 1 : list.Max(q => q.secuencia)+1;
            if (info_det.IdProductoHijo != info_det.IdProductoHijo)
            {
                info_det.pr_descripcion = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProductoHijo).pr_descripcion_combo;
            }
            info_det.IdUnidadMedida = info_det.IdUnidadMedida;
            info_det.pr_descripcion = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProductoHijo).pr_descripcion_combo;
            info_det.Cantidad = info_det.Cantidad;
            list.Add(info_det);
        }

        public void UpdateRow(in_Producto_Composicion_Info info_det, decimal IdTransaccionSession)
        {
            in_Producto_Composicion_Info edited_info = get_list(IdTransaccionSession).Where(m => m.secuencia == info_det.secuencia).First();
            if (edited_info.IdProductoHijo != info_det.IdProductoHijo)
            {
                edited_info.pr_descripcion = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProductoHijo).pr_descripcion_combo;
            }
            edited_info.IdProductoHijo = info_det.IdProductoHijo;
            edited_info.IdUnidadMedida = info_det.IdUnidadMedida;
            edited_info.Cantidad = info_det.Cantidad;
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<in_Producto_Composicion_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.secuencia == secuencia).First());
        }
    }

    public class in_Producto_List
    {
        string Variable = "in_producto_x_tb_bodega_Info";
        public List<in_Producto_Info> get_list()
        {
            if (HttpContext.Current.Session[Variable] == null)
            {
                List<in_Producto_Info> list = new List<in_Producto_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<in_Producto_Info>)HttpContext.Current.Session[Variable];
        }

        public void set_list(List<in_Producto_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }

        public void AddRow(in_Producto_Info info_det)
        {
            List<in_Producto_Info> list = get_list();
            if (list.Where(q=>q.IdProducto == info_det.IdProducto).Count() == 0)
               list.Add(info_det);
        }

        public void DeleteRow(decimal IdProducto)
        {
            List<in_Producto_Info> list = get_list();
            list.Remove(list.Where(m => m.IdProducto == IdProducto).First());
        }
    }

    public class in_producto_x_tb_bodega_Info_List
    {
        string Variable = "in_producto_x_tb_bodega_Info";
        public List<in_producto_x_tb_bodega_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_producto_x_tb_bodega_Info> list = new List<in_producto_x_tb_bodega_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_producto_x_tb_bodega_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_producto_x_tb_bodega_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(in_producto_x_tb_bodega_Info info_det, decimal IdTransaccionSession)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();

            List<in_producto_x_tb_bodega_Info> list = get_list(IdTransaccionSession);

            if (list.Where(q => q.IdSucursal == info_det.IdSucursal && q.IdBodega == info_det.IdBodega).Count() == 0)
            {
                info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
                info_det.IdProducto = info_det.IdProducto;
                info_det.IdBodega = info_det.IdBodega;
                info_det.IdSucursal = info_det.IdSucursal;
                info_det.Stock_minimo = info_det.Stock_minimo;
                info_det.Su_Descripcion = info_det.Su_Descripcion;
                info_det.bo_Descripcion = info_det.bo_Descripcion;
                info_det.IdString = info_det.IdString;
                list.Add(info_det);
            }

        }

        public void UpdateRow(in_producto_x_tb_bodega_Info info_det, decimal IdTransaccionSession)
        {
            in_producto_x_tb_bodega_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.IdBodega = info_det.IdBodega;
            edited_info.IdSucursal = info_det.IdSucursal;
            edited_info.Stock_minimo = info_det.Stock_minimo;
            edited_info.Su_Descripcion = info_det.Su_Descripcion;
            edited_info.bo_Descripcion = info_det.bo_Descripcion;
            edited_info.IdString = info_det.IdString;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<in_producto_x_tb_bodega_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

    public class in_Producto_x_fa_NivelDescuesto_List
    {
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        fa_NivelDescuento_Bus bus_nivel_desc = new fa_NivelDescuento_Bus();
        string Variable = "in_Producto_x_fa_NivelDescuento_Info";

        public List<in_Producto_x_fa_NivelDescuento_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_Producto_x_fa_NivelDescuento_Info> list = new List<in_Producto_x_fa_NivelDescuento_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_Producto_x_fa_NivelDescuento_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_Producto_x_fa_NivelDescuento_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(in_Producto_x_fa_NivelDescuento_Info info_det, decimal IdTransaccionSession)
        {
            List<in_Producto_x_fa_NivelDescuento_Info> list = get_list(IdTransaccionSession);
            fa_NivelDescuento_Info info_nivel = bus_nivel_desc.GetInfo(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdNivel);

            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.Descripcion = info_nivel.Descripcion;

            list.Add(info_det);
        }

        public void UpdateRow(in_Producto_x_fa_NivelDescuento_Info info_det, decimal IdTransaccionSession)
        {
            in_Producto_x_fa_NivelDescuento_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            if (edited_info.IdNivel != info_det.IdNivel)
            {
                fa_NivelDescuento_Info info_nivel = bus_nivel_desc.GetInfo(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdNivel);
                edited_info.Descripcion = info_nivel.Descripcion;
            }
            edited_info.IdNivel = info_det.IdNivel;
            edited_info.Porcentaje = info_det.Porcentaje;
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<in_Producto_x_fa_NivelDescuento_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == secuencia).First());
        }
    }

    public class in_Prod_List
    {
        string Variable = "in_Producto_Info";
        public List<in_Producto_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_Producto_Info> list = new List<in_Producto_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_Producto_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_Producto_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

}