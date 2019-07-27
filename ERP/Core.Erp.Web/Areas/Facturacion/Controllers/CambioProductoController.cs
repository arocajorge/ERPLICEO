using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.Facturacion;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Facturacion;
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

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    public class CambioProductoController : Controller
    {
        #region Variables
        tb_sucursal_Bus bus_sucursal;
        fa_CambioProducto_Bus bus_CambioProducto;
        tb_bodega_Bus bus_bodega;
        in_Producto_Bus bus_producto;
        fa_CambioProductoDet_List List_det;
        fa_CambioProductoDet_Bus bus_CambioProductoDet;
        fa_CambioProductoDetFacturas_List List_det_facturas;
        string mensaje = string.Empty;
        ct_periodo_Bus bus_periodo;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion
        #region Constructor
        public CambioProductoController()
        {
            bus_sucursal = new tb_sucursal_Bus();
            bus_CambioProducto = new fa_CambioProducto_Bus();
            bus_bodega = new tb_bodega_Bus();
            bus_producto = new in_Producto_Bus();
            List_det = new fa_CambioProductoDet_List();
            bus_CambioProductoDet = new fa_CambioProductoDet_Bus();
            List_det_facturas = new fa_CambioProductoDetFacturas_List();
            bus_periodo = new ct_periodo_Bus();
        }
        #endregion
        #region Metodos ComboBox bajo demanda producto
        public ActionResult CmbProducto_CambioProducto()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_CambioProducto", model);
        }
        public List<in_Producto_Info> get_list_bajo_demandaProducto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            List<in_Producto_Info> Lista = bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORMODULO, cl_enumeradores.eModulo.FAC, 0,0);
            return Lista;
        }
        public in_Producto_Info get_info_bajo_demandaProducto(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal)
            };
            CargarCombos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            CargarCombos(model.IdEmpresa);
            return View(model);
        }

        private void CargarCombos(int IdEmpresa)
        {
            try
            {
                var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
                ViewBag.lst_sucursal = lst_sucursal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_CambioProducto(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_CambioProducto.GetList(IdEmpresa, IdSucursal, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_CambioProducto", model);
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

            fa_CambioProducto_Info model = new fa_CambioProducto_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal),
                IdUsuario = SessionFixed.IdUsuario,
                Fecha = DateTime.Now,
                FechaIni = DateTime.Now.Date.AddMonths(-1),
                FechaFin = DateTime.Now.Date
            };
            CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(fa_CambioProducto_Info model)
        {
            if (!Validar(model,ref mensaje))
            {
                CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }
            if (!bus_CambioProducto.GuardarDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
           return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdBodega = model.IdBodega, IdCambio = model.IdCambio, Exito = true });
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursal = 0, int IdBodega = 0, decimal IdCambio = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            fa_CambioProducto_Info model = bus_CambioProducto.GetInfo(IdEmpresa, IdSucursal, IdBodega, IdCambio);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.IdUsuario = SessionFixed.IdUsuario;
            model.FechaIni = DateTime.Now.Date.AddMonths(-1);
            model.FechaFin = DateTime.Now.Date;
            CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
            model.LstDet = bus_CambioProductoDet.GetList(model.IdEmpresa, model.IdSucursal, model.IdBodega, model.IdCambio);
            List_det.set_list(model.LstDet, model.IdTransaccionSession);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_CambioProducto_Info model)
        {
            if (!Validar(model, ref mensaje))
            {
                CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            if (!bus_CambioProducto.ModificarDB(model))
            {
                CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdBodega = model.IdBodega, IdCambio = model.IdCambio, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdSucursal = 0, int IdBodega = 0, decimal IdCambio = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_CambioProducto_Info model = bus_CambioProducto.GetInfo(IdEmpresa, IdSucursal, IdBodega, IdCambio);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.IdUsuario = SessionFixed.IdUsuario;
            model.FechaIni = DateTime.Now.Date.AddMonths(-1);
            model.FechaFin = DateTime.Now.Date;
            CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
            model.LstDet = bus_CambioProductoDet.GetList(model.IdEmpresa, model.IdSucursal, model.IdBodega, model.IdCambio);
            List_det.set_list(model.LstDet, model.IdTransaccionSession);
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_CambioProducto_Info model)
        {
            if (!bus_CambioProducto.AnularDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion
        #region Metodos
        private void CargarCombosAccion(int IdEmpresa, int IdSucursal)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_bodega = bus_bodega.get_list(IdEmpresa, IdSucursal, false);
            ViewBag.lst_bodega = lst_bodega;
        }

        private bool Validar(fa_CambioProducto_Info i_validar, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.Fecha, cl_enumeradores.eModulo.FAC, i_validar.IdSucursal, ref mensaje))
                return false;

            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.Fecha, cl_enumeradores.eModulo.INV, i_validar.IdSucursal, ref mensaje))
                return false;

            i_validar.LstDet = List_det.get_list(i_validar.IdTransaccionSession);

            if(i_validar.LstDet.Count == 0)
            {
                mensaje = "Debe ingresar un detalle a devolver";
                return false;
            }

            if (i_validar.LstDet.Where(q=>q.IdProductoCambio == q.IdProductoFact).Count() > 0)
            {
                mensaje = "Los productos de cambio deben ser distintos a los productos facturados en el detalle";
                return false;
            }            

            return true;
        }
        #endregion
        #region Json
        public JsonResult CargarBodega(int IdEmpresa = 0, int IdSucursal = 0)
        {
            var resultado = bus_bodega.get_list(IdEmpresa, IdSucursal, false);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFacturasCambio(DateTime FechaIni, DateTime FechaFin, int IdEmpresa = 0, int IdSucursal = 0, int IdBodega = 0, string NumeroFactura = "", decimal IdTransaccionSession = 0)
        {
            bool resultado = false;

            decimal n = 0;
            var isNumeric = decimal.TryParse(NumeroFactura, out n);
            if (isNumeric)
                n = Convert.ToDecimal(NumeroFactura);
            else
                n = 0;
            var ListaFacturas = bus_CambioProductoDet.GetListFacturas(IdEmpresa, IdSucursal, IdBodega, n, FechaIni, FechaFin);
            List_det_facturas.set_list(ListaFacturas);
            if (ListaFacturas.Count > 0)
                resultado = true;
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Metodos del detalle
        public ActionResult GridViewPartial_CambioProductoDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_CambioProductoDet", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew(string IDs = "", decimal IdTransaccionSession = 0)
        {
            if (!string.IsNullOrEmpty(IDs))
            {
                var lst = List_det_facturas.get_list();
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    var info_det = lst.Where(q => q.IdSecuencial == item).FirstOrDefault();
                    if (info_det != null)
                        List_det.AddRow(info_det, IdTransaccionSession);
                }
            }

            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_CambioProductoDet", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] fa_CambioProductoDet_Info info_det)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            var producto = bus_producto.get_info(IdEmpresa, info_det.IdProductoCambio);
            if (producto != null)
                info_det.pr_descripcionCambio = producto.pr_descripcion;
            if (ModelState.IsValid)
                List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_CambioProductoDet", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            List_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_CambioProductoDet", model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_CambioProductoDetFacturas()
        {
            var model = List_det_facturas.get_list();
            return PartialView("_GridViewPartial_CambioProductoDetFacturas", model);
        }
        #endregion
    }
    public class fa_CambioProductoDet_List
    {
        string Variable = "fa_CambioProductoDet_Info";
        public List<fa_CambioProductoDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_CambioProductoDet_Info> list = new List<fa_CambioProductoDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_CambioProductoDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_CambioProductoDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(fa_CambioProductoDet_Info info_det, decimal IdTransaccionSession)
        {
            List<fa_CambioProductoDet_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            
            list.Add(info_det);
        }

        public void UpdateRow(fa_CambioProductoDet_Info info_det, decimal IdTransaccionSession)
        {
            fa_CambioProductoDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdProductoCambio = info_det.IdProductoCambio;
            edited_info.CantidadCambio = info_det.CantidadCambio;
            edited_info.pr_descripcionCambio = info_det.pr_descripcionCambio;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<fa_CambioProductoDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
    public class fa_CambioProductoDetFacturas_List
    {
        string Variable = "fa_CambioProductoDetFacturas";
        public List<fa_CambioProductoDet_Info> get_list()
        {

            if (HttpContext.Current.Session[Variable] == null)
            {
                List<fa_CambioProductoDet_Info> list = new List<fa_CambioProductoDet_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<fa_CambioProductoDet_Info>)HttpContext.Current.Session[Variable];
        }

        public void set_list(List<fa_CambioProductoDet_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }
    }
}