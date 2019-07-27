using Core.Erp.Bus.General;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class ReportesPorUsuarioController : Controller
    {
        #region Index / Metodo

        tb_sis_reporte_x_seg_usuario_List List_det = new tb_sis_reporte_x_seg_usuario_List();
        tb_sis_reporte_x_seg_usuario_Bus bus_reporte_x_usuario = new tb_sis_reporte_x_seg_usuario_Bus();
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            tb_sis_reporte_x_seg_usuario_Info model = new tb_sis_reporte_x_seg_usuario_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(tb_sis_reporte_x_seg_usuario_Info model)
        {
            cargar_combos();
            List_det.set_list(bus_reporte_x_usuario.get_list(model.IdEmpresa, model.IdUsuario,true), model.IdTransaccionSession);
            return View(model);
        }
        private void cargar_combos()
        {
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var lst_empresa = bus_empresa.get_list(false);
            ViewBag.lst_empresa = lst_empresa;

            seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
            var lst_usuario = bus_usuario.get_list(false);
            ViewBag.lst_usuario = lst_usuario;
        }
        #endregion
        #region Acciones
        public ActionResult Consulta()
        {
            return View();
        }

        #endregion
        #region Grids

        [ValidateInput(false)]
        public ActionResult GridViewPartial_ReportesPorUsuario()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = bus_reporte_x_usuario.get_list(Convert.ToInt32(SessionFixed.IdEmpresa),SessionFixed.IdUsuario,false);
            return PartialView("_GridViewPartial_ReportesPorUsuario", model);
        }
        public ActionResult GridViewPartial_ReportesPorAsignar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToInt32(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == false).ToList();
            return PartialView("_GridViewPartial_ReportesPorAsignar", model);
        }
        public ActionResult GridViewPartial_ReportesAsignados()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToInt32(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList();
            return PartialView("_GridViewPartial_ReportesAsignados", model);
        }
        public void EditingUpdate(string CodReporte = "")
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List_det.UpdateRow(CodReporte, Convert.ToInt32(SessionFixed.IdTransaccionSessionActual));
        }
        #endregion
        #region Json
        public JsonResult guardar(int IdEmpresa = 0, string IdUsuario = "")
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            bus_reporte_x_usuario.eliminarDB(IdEmpresa, IdUsuario);
            var resultado = bus_reporte_x_usuario.guardarDB(List_det.get_list(Convert.ToInt32(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList(), IdEmpresa, IdUsuario);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }

    public class tb_sis_reporte_x_seg_usuario_List
    {
        string variable = "tb_sis_reporte_x_seg_usuario_Info";
        public List<tb_sis_reporte_x_seg_usuario_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<tb_sis_reporte_x_seg_usuario_Info> list = new List<tb_sis_reporte_x_seg_usuario_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<tb_sis_reporte_x_seg_usuario_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<tb_sis_reporte_x_seg_usuario_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(string CodReporte, decimal IdTransaccionSession)
        {
            tb_sis_reporte_x_seg_usuario_Info edited_info = get_list(IdTransaccionSession).Where(m => m.CodReporte == CodReporte).FirstOrDefault();
            if (edited_info != null)
                edited_info.seleccionado = !edited_info.seleccionado;
        }
    }
}