using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class RubrosCalculadosController : Controller
    {
        ro_rubros_calculados_Bus bus_cargo = new ro_rubros_calculados_Bus();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        public ActionResult Index()
        {
            ro_rubros_calculados_Info model = new ro_rubros_calculados_Info();
            model= bus_cargo.get_info(Convert.ToInt32( SessionFixed.IdEmpresa));
            cargar_combos();
            return View(model);
        }

       
        [HttpPost]
        public ActionResult Index(ro_rubros_calculados_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_cargo.guardarDB(info))
                    {
                        cargar_combos();
                        return View(info);
                    }
                    else
                    {
                        cargar_combos();

                        return View(info);

                    }
                }
                else
                {
                    cargar_combos();

                    return View(info);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
       
       
        private int GetIdEmpresa()
        {
            try
            {
                if (Session["IdEmpresa"] != null)
                    return Convert.ToInt32(Session["IdEmpresa"]);
                else
                    return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cargar_combos()
        {
            try
            {
                ViewBag.lst_rubro = bus_rubro.get_list(GetIdEmpresa(), false);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}