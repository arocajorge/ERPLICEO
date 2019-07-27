using Core.Erp.Bus.Compras;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Compras;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using System;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Compras.Controllers
{
    [SessionTimeout]
    public class ParametrosComprasController : Controller
    {
        #region Variables
        com_parametro_Bus bus_parametro = new com_parametro_Bus();
        com_estado_cierre_Bus bus_estado = new com_estado_cierre_Bus();
        com_catalogo_Bus bus_catalogo = new com_catalogo_Bus();
        in_movi_inven_tipo_Bus bus_movi = new in_movi_inven_tipo_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            com_parametro_Info model = bus_parametro.get_info(IdEmpresa);
            if (model == null)
                model = new com_parametro_Info { IdEmpresa = IdEmpresa };
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(com_parametro_Info model)
        {
            if (!bus_parametro.guardarDB(model))
                ViewBag.mensaje = "No se pudieron actualizar los registros";
            cargar_combos(model.IdEmpresa);
            return View(model);
        }

        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            var lst_est_aprob= bus_catalogo.get_list(Convert.ToString(cl_enumeradores.eTipoCatalogoCOM.EST_APRO), false);
            ViewBag.lst_est_aprob = lst_est_aprob;

            var lst_est_anu = bus_catalogo.get_list(Convert.ToString(cl_enumeradores.eTipoCatalogoCOM.EST_APRO), false);
            ViewBag.lst_est_anu = lst_est_anu;

            var lst_estado = bus_estado.get_list(false);
            ViewBag.lst_estado = lst_estado;

            var lst_movi = bus_movi.get_list(IdEmpresa,"+", false);
            ViewBag.lst_movi = lst_movi;
        }

        #endregion
    }
}