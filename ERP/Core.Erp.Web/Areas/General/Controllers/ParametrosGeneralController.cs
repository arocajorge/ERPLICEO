using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    public class ParametrosGeneralController : Controller
    {
        #region Variables
        tb_parametro_Bus bus_parametro = new tb_parametro_Bus();
        tb_sis_Impuesto_Bus bus_imp = new tb_sis_Impuesto_Bus();
        
        #endregion

        #region Index
        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_parametro_Info model = bus_parametro.GetInfo(IdEmpresa);
            if (model == null)
                model = new tb_parametro_Info { IdEmpresa = IdEmpresa };
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(tb_parametro_Info model)
        {
            if (!bus_parametro.GuardarDB(model))
                ViewBag.mensaje = "No se pudieron actualizar los registros";
            cargar_combos();
            return View(model);
        }

        #endregion
        #region Metodos
        private void cargar_combos(string IdTipoImpuesto ="")
        {
            var lst_imp = bus_imp.get_list(IdTipoImpuesto, false);
            ViewBag.lst_imp = lst_imp;
        }

        #endregion

    }
}