using Core.Erp.Bus.Caja;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Caja;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Caja.Controllers
{
    [SessionTimeout]
    public class ParametroCajaController : Controller
    {
        #region MyRegion
        caj_parametro_Bus bus_parametro = new caj_parametro_Bus();
        ct_cbtecble_tipo_Bus bus_tipo_comprobante = new ct_cbtecble_tipo_Bus();
        caj_Caja_Movimiento_Tipo_Bus bus_tipomovimiento = new caj_Caja_Movimiento_Tipo_Bus();

        #endregion

        #region Index

        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            caj_parametro_Info model = bus_parametro.get_info(IdEmpresa);
            if (model == null)
                model = new caj_parametro_Info { IdEmpresa = IdEmpresa};
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(caj_parametro_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_parametro.guardarDB(model))
                ViewBag.mensaje = "No se pudieron actualizar los registros";
            cargar_combos(model.IdEmpresa);
            return View(model);
        }

        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            var lst_tipo_comprobante = bus_tipo_comprobante.get_list(IdEmpresa, false);
            ViewBag.lst_tipo_comprobante = lst_tipo_comprobante;

            var lst_tipomovimiento = bus_tipomovimiento.get_list(IdEmpresa, false);
            ViewBag.lst_tipomovimiento = lst_tipomovimiento;
           
        }

        #endregion
    }
}