using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    public class AuditoriaInventarioController : Controller
    {
        #region  Variables
        tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        in_movi_inven_tipo_Bus bus_tipo = new in_movi_inven_tipo_Bus();
        in_transferencia_Bus bus_transferencia = new in_transferencia_Bus();
        in_Ing_Egr_Inven_Bus bus_ing_egr_Inven = new in_Ing_Egr_Inven_Bus();
        in_producto_x_tb_bodega_Costo_Historico_Bus bus_producto_bodega_historico = new in_producto_x_tb_bodega_Costo_Historico_Bus();
        in_transferencia_Corregir_List ListaCorregirTransferencia = new in_transferencia_Corregir_List();
        List<in_transferencia_Info> Lista_CorregirTransferencia = new List<in_transferencia_Info>();
        in_Ing_Egr_Inven_Movimientos_List ListaIngEgrInvMovimientos = new in_Ing_Egr_Inven_Movimientos_List();
        List<in_Ing_Egr_Inven_Info> Lista_IngEgrInv_Movimientos = new List<in_Ing_Egr_Inven_Info>();
        in_producto_x_tb_bodega_Costo_Historico_Recosteo_List ListaRecosteoXSucursal = new in_producto_x_tb_bodega_Costo_Historico_Recosteo_List();
        List<in_producto_x_tb_bodega_Costo_Historico_Info> Lista_Recosteo_x_Sucursal = new List<in_producto_x_tb_bodega_Costo_Historico_Info>();
        #endregion

        #region Index
        public ActionResult Index()
        {
            in_transferencia_Info model = new in_transferencia_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                fecha_correccion_transferencia = DateTime.Now.Date,
                fecha_recosteo_sucursal = DateTime.Now.Date,
                fecha_inicio_mov = DateTime.Now.Date.AddMonths(-1),
                fecha_fin_mov = DateTime.Now.Date,
                TipoMovimiento = "+"
            };

            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(in_transferencia_Info model)
        {
            cargar_combos(model);
            return View(model);
        }

        #region Cargar Combos
        private void cargar_combos(in_transferencia_Info model)
        {
            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_bodega = bus_bodega.get_list(model.IdEmpresa, model.IdSucursal, false);
            ViewBag.lst_bodega = lst_bodega;

            Dictionary<string, string> lst_Rubro = new Dictionary<string, string>();
            lst_Rubro.Add( "+", "Ingresos");
            lst_Rubro.Add("-", "Egresos");
            ViewBag.lst_tipo_movimiento = lst_Rubro;

        }
        #endregion
        #endregion

        #region GridView
        #region CorreccionTransferencia
        public ActionResult GridViewPartial_CorreccionTransferencias(DateTime? fecha_ini)
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_ini);

            var model = ListaCorregirTransferencia.get_list(IdTransaccionSession);

            return PartialView("_GridViewPartial_CorreccionTransferencias", model);
        }
        #endregion

        #region Recosteo por sucursal        
        public ActionResult GridViewPartial_Recosteo_x_Sucursal(int IdSucursal, int IdBodega, DateTime? fecha_ini)
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdSucursal = IdSucursal == 0 ? Convert.ToInt32(SessionFixed.IdSucursal) : IdSucursal;
            ViewBag.IdBodega = IdSucursal == 0 ? 0 : IdBodega;
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_ini);

            var model = ListaRecosteoXSucursal.get_list(IdTransaccionSession);

            return PartialView("_GridViewPartial_Recosteo_x_Sucursal", model);
        }
        #endregion

        #region Movimientos sin contabilizar      
        public ActionResult GridViewPartial_MovimientosSinCont(string TipoMovimiento, DateTime? fecha_ini, DateTime? fecha_fin)
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.TipoMovimiento = TipoMovimiento == "" ? "" : TipoMovimiento;
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_ini);
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_fin);

            var model = ListaIngEgrInvMovimientos.get_list(IdTransaccionSession);

            return PartialView("_GridViewPartial_MovimientosSinCont", model);
        }
        #endregion
        #endregion        

        #region Json
        public JsonResult CargarBodega(int IdEmpresa = 0, int IdSucursal = 0)
        {
            var resultado = bus_bodega.get_list(IdEmpresa, IdSucursal, false);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        #region CorreccionTransferencia
        public JsonResult BuscarTransferencia(int[] ListaSucursal, DateTime fecha_ini, int IdEmpresa = 0)
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_ini);

            var model = bus_transferencia.GetListRecosteoInventario(IdEmpresa, ViewBag.fecha_ini,ListaSucursal);
            ListaCorregirTransferencia.set_list(model, IdTransaccionSession);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CorregirTransferencia(DateTime fecha_ini, int IdEmpresa = 0)
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            Lista_CorregirTransferencia = ListaCorregirTransferencia.get_list(IdTransaccionSession);

            var Result = bus_transferencia.CorregirTransferencia(Lista_CorregirTransferencia, fecha_ini);

            List<in_transferencia_Info> model = new List<in_transferencia_Info>();            
            ListaCorregirTransferencia.set_list(model, IdTransaccionSession);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Movimientos sin contabilizar
        public JsonResult BuscarMovimientos(DateTime fecha_ini, DateTime fecha_fin, string TipoMovimiento)
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_ini);
            ViewBag.fecha_fin = fecha_ini == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_fin);
            ViewBag.TipoMovimiento = fecha_ini == null ? "" : TipoMovimiento;

            var model = bus_ing_egr_Inven.BuscarMovimientos(IdEmpresa, ViewBag.fecha_ini, ViewBag.fecha_fin, ViewBag.TipoMovimiento);
            ListaIngEgrInvMovimientos.set_list(model, IdTransaccionSession);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ContabilizarMovimientos(string Ids = "", decimal IdTransaccionSession = 0)
        {
            var lista = ListaIngEgrInvMovimientos.get_list(IdTransaccionSession);
            string[] arreglo = Ids.Split(',');
            foreach (var item in arreglo)
            {
                var movi = lista.Where(q => q.CodMoviInven == item).FirstOrDefault();
                if(movi != null)
                {
                    bus_ing_egr_Inven.ReContabilizar(movi.IdEmpresa, movi.IdSucursal, movi.IdMovi_inven_tipo, movi.IdNumMovi, movi.cm_observacion, movi.cm_fecha);
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Recosteo por sucursal 
        public JsonResult Recosteo_x_Sucursal(DateTime fecha_ini, int IdEmpresa = 0, int IdSucursal=0, int IdBodega=0)
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            Lista_Recosteo_x_Sucursal = bus_producto_bodega_historico.Recosteo_x_Sucursal(IdEmpresa, IdSucursal, IdBodega, fecha_ini);

            ListaRecosteoXSucursal.set_list(Lista_Recosteo_x_Sucursal, IdTransaccionSession);

            return Json(Lista_Recosteo_x_Sucursal, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }

    public class in_transferencia_Corregir_List
    {
        string Variable = "in_transferencia_Info";
        public List<in_transferencia_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_transferencia_Info> list = new List<in_transferencia_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_transferencia_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_transferencia_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class in_producto_x_tb_bodega_Costo_Historico_Recosteo_List
    {
        string Variable = "in_producto_x_tb_bodega_Costo_Historico_Info";
        public List<in_producto_x_tb_bodega_Costo_Historico_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_producto_x_tb_bodega_Costo_Historico_Info> list = new List<in_producto_x_tb_bodega_Costo_Historico_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_producto_x_tb_bodega_Costo_Historico_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_producto_x_tb_bodega_Costo_Historico_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class in_Ing_Egr_Inven_Movimientos_List
    {
        string Variable = "in_Ing_Egr_Inven_Info";
        public List<in_Ing_Egr_Inven_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_Ing_Egr_Inven_Info> list = new List<in_Ing_Egr_Inven_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_Ing_Egr_Inven_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_Ing_Egr_Inven_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}