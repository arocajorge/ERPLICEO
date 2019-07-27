using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class DevolucionInventarioController : Controller
    {
        #region Variables
        in_devolucion_inven_Bus bus_devolucion = new in_devolucion_inven_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        in_devolucion_inven_det_List List_det = new in_devolucion_inven_det_List();
        in_Ing_Egr_Inven_Bus bus_inv = new in_Ing_Egr_Inven_Bus();
        in_devolucion_inven_det_Bus bus_det = new in_devolucion_inven_det_Bus();
        string mensaje = string.Empty;
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }
        public ActionResult GridViewPartial_devolucion( DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            ViewBag.IdSucursal = IdSucursal;
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            var model = bus_devolucion.get_list(IdEmpresa, IdSucursal, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_devolucion",model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            Dictionary<string, string> lst_signo = new Dictionary<string, string>();
            lst_signo.Add("+", "Ingreso por devolución");
            lst_signo.Add("-", "Egreso por devolución");            
            ViewBag.lst_signo = lst_signo;

            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;
        }
        private void CargarCombosConsulta(int IdEmpresa)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }


        private bool validar(in_devolucion_inven_Info i_validar, ref string msg)
        {
            i_validar.lst_det = List_det.get_list().Where(q=>q.cant_devuelta > 0).ToList();
            if (i_validar.lst_det.Count == 0)
            {
                msg = "No ha ingresado detalles a la devolución";
                return false;
            }
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.Fecha, cl_enumeradores.eModulo.INV, i_validar.dev_IdSucursal, ref msg))
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            in_devolucion_inven_Info model = new in_devolucion_inven_Info
            {
                IdEmpresa = IdEmpresa,
                Fecha_ini = DateTime.Now.Date.AddMonths(-1),
                Fecha_fin = DateTime.Now.Date,
                Fecha = DateTime.Now.Date,
                lst_det = new List<in_devolucion_inven_det_Info>()
            };
            List_det.set_list(new List<in_devolucion_inven_det_Info>());
            set_list(new List<in_Ing_Egr_Inven_Info>());
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(in_devolucion_inven_Info model)
        {
            if (!validar(model,ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            model.IdUsuario = Convert.ToString(SessionFixed.IdUsuario);
            if (!bus_devolucion.guardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0 , decimal IdDev_Inven = 0)
        {
            in_devolucion_inven_Info model = bus_devolucion.get_info(IdEmpresa, IdDev_Inven);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_det = bus_det.get_list(IdEmpresa, IdDev_Inven);
            List_det.set_list(model.lst_det);
            cargar_combos(IdEmpresa);
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.FAC, 0, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(in_devolucion_inven_Info model)
        {
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            model.IdUsuarioUltModi = Convert.ToString(SessionFixed.IdUsuario);
            if (!bus_devolucion.modificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0 , decimal IdDev_Inven = 0)
        {
            in_devolucion_inven_Info model = bus_devolucion.get_info(IdEmpresa, IdDev_Inven);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_det = bus_det.get_list(IdEmpresa, IdDev_Inven);
            List_det.set_list(model.lst_det);
            cargar_combos(IdEmpresa);
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.FAC, 0, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(in_devolucion_inven_Info model)
        {
            model.IdusuarioUltAnu = SessionFixed.IdUsuario;
            if (!bus_devolucion.anularDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Json
        public JsonResult GetMovimientos(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0, string signo = "")
        {
            bool resultado = false;
            DateTime Fechaini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            DateTime Fechafin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            var lst = bus_inv.get_list_por_devolver(Convert.ToInt32(SessionFixed.IdEmpresa), signo == "+" ? "-" : "+", Fechaini, Fechafin);
            set_list(lst);
            return Json(resultado,JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetMovimiento(string SecuencialID)
        {
            bool resultado = false;
            var lst = get_list();
            var mov = lst.Where(q => q.SecuencialID == SecuencialID).FirstOrDefault();
            if(mov != null)
            {
                List_det.set_list(bus_det.get_list_x_movimiento(mov.IdEmpresa, mov.IdSucursal, mov.IdMovi_inven_tipo, mov.IdNumMovi));
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detalle
        public ActionResult GridViewPartial_devolucion_det()
        {
            var model = List_det.get_list();
            return PartialView("_GridViewPartial_devolucion_det",model);
        }
        public ActionResult GridViewPartial_devolucion_det_x_cruzar()
        {
            var model = get_list();
            return PartialView("_GridViewPartial_devolucion_det_x_cruzar", model);
        }
        public List<in_Ing_Egr_Inven_Info> get_list()
        {
            if (Session["in_Ing_Egr_Inven_x_devolver_Info"] == null)
            {
                List<in_Ing_Egr_Inven_Info> list = new List<in_Ing_Egr_Inven_Info>();

                Session["in_Ing_Egr_Inven_x_devolver_Info"] = list;
            }
            return (List<in_Ing_Egr_Inven_Info>)Session["in_Ing_Egr_Inven_x_devolver_Info"];
        }
        public void set_list(List<in_Ing_Egr_Inven_Info> list)
        {
            Session["in_Ing_Egr_Inven_x_devolver_Info"] = list;
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] in_devolucion_inven_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det != null)
                List_det.UpdateRow(info_det);
            var model = List_det.get_list();
            return PartialView("_GridViewPartial_devolucion_det", model);
        }
        public ActionResult EditingDelete(int secuencia)
        {
            List_det.DeleteRow(secuencia);
            var model = List_det.get_list();
            return PartialView("_GridViewPartial_devolucion_det", model);
        }
        #endregion
    }

    public class in_devolucion_inven_det_List
    {
        public List<in_devolucion_inven_det_Info> get_list()
        {
            if (HttpContext.Current.Session["in_devolucion_inven_det_Info"] == null)
            {
                List<in_devolucion_inven_det_Info> list = new List<in_devolucion_inven_det_Info>();

                HttpContext.Current.Session["in_devolucion_inven_det_Info"] = list;
            }
            return (List<in_devolucion_inven_det_Info>)HttpContext.Current.Session["in_devolucion_inven_det_Info"];
        }

        public void set_list(List<in_devolucion_inven_det_Info> list)
        {
            HttpContext.Current.Session["in_devolucion_inven_det_Info"] = list;
        }

        public void AddRow(in_devolucion_inven_det_Info info_det)
        {
            List<in_devolucion_inven_det_Info> list = get_list();
            info_det.secuencia = list.Count == 0 ? 1 : list.Max(q => q.secuencia) + 1;
            info_det.cant_devuelta = info_det.cant_devuelta;
            list.Add(info_det);
        }

        public void UpdateRow(in_devolucion_inven_det_Info info_det)
        {
            in_devolucion_inven_det_Info edited_info = get_list().Where(m => m.secuencia == info_det.secuencia).First();
            edited_info.cant_devuelta = info_det.cant_devuelta;
        }

        public void DeleteRow(int secuencia)
        {
            List<in_devolucion_inven_det_Info> list = get_list();
            list.Remove(list.Where(m => m.secuencia == secuencia).First());
        }
    }
}