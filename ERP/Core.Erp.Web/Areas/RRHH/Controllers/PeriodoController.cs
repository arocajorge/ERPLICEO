using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Bus.General;
namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class PeriodoController : Controller
    {
        ro_periodo_Bus bus_periodo = new ro_periodo_Bus();
        tb_region_Bus bus_region = new tb_region_Bus();
        List<tb_region_Info> lst_region = new List<tb_region_Info>();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        List<ro_catalogo_Info> lista_catalogo = new List<ro_catalogo_Info>();

        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_periodo()
        {
            try
            {
                List<ro_periodo_Info> model = bus_periodo.get_list(GetIdEmpresa(), true);
                return PartialView("_GridViewPartial_periodo", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_periodo_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_periodo.guardarDB(info))
                        return View(info);
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo()
        {
            try
            {
                ro_periodo_Info info = new ro_periodo_Info();
                cargar_combo();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_periodo_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdUsuarioUltMod = GetIdUsuario();

                    if (!bus_periodo.modificarDB(info))
                        return View(info);
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Modificar(int IdPeriodo = 0)
        {
            try
            {
                cargar_combo();
                return View(bus_periodo.get_info(GetIdEmpresa(), IdPeriodo));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Anular(ro_periodo_Info info)
        {
            try
            {
                    info.IdUsuarioUltAnu = GetIdUsuario();
                    if (!bus_periodo.anularDB(info))
                        return View(info);
                    else
                        return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdPeriodo = 0)
        {
            try
            {
                cargar_combo();
                return View(bus_periodo.get_info(GetIdEmpresa(), IdPeriodo));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Generar_Periodos(ro_periodo_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdEmpresa = GetIdEmpresa();
                    info.IdUsuario = GetIdUsuario();
                    if (!bus_periodo.Generar_Periodos(info))
                    {
                        cargar_combo();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                {
                    cargar_combo();
                    return View(info);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Generar_Periodos()
        {
            try
            {
                ro_periodo_Info info = new ro_periodo_Info();
                cargar_combo();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
    
        private void cargar_combo()
        {
            try
            {
                lista_catalogo = bus_catalogo.get_list_x_tipo(17);
                lst_region = bus_region.get_list("1", false);
                lst_region = bus_region.get_list( "1", false);
                ViewBag.lst_region = lst_region;
                ViewBag.lista_catalogo = lista_catalogo;
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
        private string GetIdUsuario()
        {
            try
            {
                if (Session["IdUsuario"] != null)
                    return Convert.ToString(Session["IdUsuario"]);
                else
                    return "";
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}