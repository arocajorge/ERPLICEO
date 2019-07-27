using Core.Erp.Info.Facturacion;
using Core.Erp.Bus.Facturacion;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.General;
using Core.Erp.Web.Helps;
using Core.Erp.Bus.Caja;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    [SessionTimeout]
    public class PuntoVentaController : Controller
    {
        #region Index

        fa_PuntoVta_Bus bus_punto = new fa_PuntoVta_Bus();
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal =  Convert.ToInt32(SessionFixed.IdSucursal),
                CodDocumentoTipo = ""
            };
            cargar_combos_consulta();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            cargar_combos_consulta();
            return View(model);
        }
        private void cargar_combos_consulta()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            tb_sis_Documento_Tipo_Bus bus_tipo = new tb_sis_Documento_Tipo_Bus();
            var lst_doc = bus_tipo.get_list(false);
            lst_doc.Add(new tb_sis_Documento_Tipo_Info
            {
                codDocumentoTipo = "",
                descripcion = "Todos"
            });
            ViewBag.lst_doc = lst_doc;

            tb_sucursal_Bus bus_suc = new tb_sucursal_Bus();
            var lst_sucursal = bus_suc.get_list(IdEmpresa, false);
            lst_sucursal.Add(new tb_sucursal_Info
            {
                IdSucursal = 0,
                Su_Descripcion = "Todos"
            });
            ViewBag.lst_sucursal = lst_sucursal;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_puntoventa(int IdSucursal = 0, string CodDocumentoTipo = "")
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            ViewBag.CodDocumentoTipo = CodDocumentoTipo;
            List<fa_PuntoVta_Info> model = bus_punto.get_list(IdEmpresa, IdSucursal, CodDocumentoTipo, true);
            return PartialView("_GridViewPartial_puntoventa", model);
        }
        private void cargar_combos( fa_PuntoVta_Info model)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(model.IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            var lst_bodega = bus_bodega.get_list(model.IdEmpresa, model.IdSucursal, false);
            ViewBag.lst_bodega = lst_bodega;

            Dictionary<string, string> lst_signos = new Dictionary<string, string>();
            lst_signos.Add("-", "-");
            lst_signos.Add("+", "+");
            ViewBag.lst_signos = lst_signos;

            caj_Caja_Bus bus_caja = new caj_Caja_Bus();
            var lst_caja = bus_caja.get_list(model.IdEmpresa, false);
            ViewBag.lst_caja = lst_caja;

            tb_sis_Documento_Tipo_Bus bus_doc = new tb_sis_Documento_Tipo_Bus();
            var lst_doc = bus_doc.get_list(false);
            ViewBag.lst_doc = lst_doc;

        }
        #endregion
        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            fa_PuntoVta_Info model = new fa_PuntoVta_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_PuntoVta_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_punto.guardarDB(model))
            {
                cargar_combos(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0 , int IdSucursal = 0, int IdPuntoVta = 0)
        {
            fa_PuntoVta_Info model = bus_punto.get_info(IdEmpresa,IdSucursal, IdPuntoVta);
            if (model == null)
            return RedirectToAction("Index");
                cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_PuntoVta_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_punto.modificarDB(model))
            {
                cargar_combos(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0 , int IdSucursal = 0, int IdPuntoVta = 0)
        {
            fa_PuntoVta_Info model = bus_punto.get_info(IdEmpresa, IdSucursal, IdPuntoVta);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
                cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_PuntoVta_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_punto.anularDB(model))
            {
                cargar_combos(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Json
        public JsonResult cargar_bodega(int IdEmpresa = 0 , int IdSucursal = 0)
        {
            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            var resultado = bus_bodega.get_list(IdEmpresa, IdSucursal, false);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}