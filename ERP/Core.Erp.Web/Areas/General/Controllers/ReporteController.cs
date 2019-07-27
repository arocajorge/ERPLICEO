using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using Core.Erp.Web.Reportes.Facturacion;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class ReporteController : Controller
    {
        #region Variables
        tb_sis_reporte_x_tb_empresa_Bus bus_reporte_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        
        #endregion
        #region Index / Metodo

        tb_sis_reporte_Bus bus_reporte = new tb_sis_reporte_Bus();
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_reporte()
        {
            List<tb_sis_reporte_Info> model = new List<tb_sis_reporte_Info>();
            model = bus_reporte.get_list();
            return PartialView("_GridViewPartial_reporte", model);
        }
        private void cargar_combos()
        {
            tb_modulo_Bus bus_modulo = new tb_modulo_Bus();
            var lst_modulo = bus_modulo.get_list();
            ViewBag.lst_modulo = lst_modulo;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            tb_sis_reporte_Info model = new tb_sis_reporte_Info();
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(tb_sis_reporte_Info model)
        {

            if (bus_reporte.validar_existe_CodReporte(model.CodReporte))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                cargar_combos();
                return View(model);
            }

            if (!bus_reporte.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(string CodReporte = "")
        {
            tb_sis_reporte_Info model = bus_reporte.get_info(CodReporte);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(tb_sis_reporte_Info model)
        {
            if (!bus_reporte.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }

        #endregion

        #region json

        public JsonResult get_id(string CodModulo)
        {
            var resultado = bus_reporte.get_id(CodModulo);
            return Json(resultado, JsonRequestBehavior.AllowGet);
            
        }

        #endregion

        #region Diseñador
        public ActionResult Disenar(int IdEmpresa = 0, string CodReporte = "")
        {
            var reporte = bus_reporte.get_info(CodReporte);
            var model = bus_reporte_x_emp.GetInfo(IdEmpresa, CodReporte);

            XtraReport rpt = GetReport(reporte.Nom_Carpeta + "." + reporte.rpt_clase_rpt);
            if (model == null)
            {
                MemoryStream ms = new MemoryStream();
                rpt.SaveLayoutToXml(ms);
                ms.Position = 0;
                model = new tb_sis_reporte_x_tb_empresa_Info
                {
                    IdEmpresa = IdEmpresa,
                    CodReporte = CodReporte,
                    ReporteDisenio = ms.ToArray()
                };
            }
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Disenar(tb_sis_reporte_x_tb_empresa_Info model)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            string CodReporte = Request.Params["fx_CodReporte"] != null ? Request.Params["fx_CodReporte"].ToString() : "";
            model.ReporteDisenio = ReportDesignerExtension.GetReportXml("ReportDesigner");
            model.IdEmpresa = IdEmpresa;
            model.CodReporte = CodReporte; 
            bus_reporte_x_emp.GuardarDB(model);
            return View(model);
        }
        #endregion

        #region Get XtraReport
        public XtraReport GetReport(string Reporte)
        {
            Assembly Ensamblado;
            
            Ensamblado = Assembly.GetExecutingAssembly();            

            Object ObjFrm;
            Type tipo = Ensamblado.GetType("Core.Erp.Web.Reportes." + Reporte);

            AssemblyName assemName = Ensamblado.GetName();
            
            if (tipo == null)
            {
                return null;
            }
            else
            {
                ObjFrm = Activator.CreateInstance(tipo);
                XtraReport Rpt = (XtraReport)ObjFrm;
                return Rpt;
            }
        }
        #endregion
    }
}