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
    public class CatalogoRRHHController : Controller
    {
        ro_catalogo_Bus bus_cargo = new ro_catalogo_Bus();
        List<ro_catalogoTipo_Info> lista_tipo = new List<ro_catalogoTipo_Info>();
        ro_catalogoTipo_Bus bus_tipo = new ro_catalogoTipo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogos(int IdTipoCatalogo=0)
        {
            try
            {
                ViewBag.IdTipoCatalogo = IdTipoCatalogo;
                List<ro_catalogo_Info> model = bus_cargo.get_list_x_tipo(IdTipoCatalogo);
                return PartialView("_GridViewPartial_catalogos", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_catalogo_Info info)
        {
            try
            {

                info.IdUsuario = SessionFixed.IdUsuario;
                ViewBag.IdTipoCatalogo = info.IdTipoCatalogo;
                cargar_combos();
                if (ModelState.IsValid)
                {
                    if (bus_cargo.si_existe_codigo(info.CodCatalogo))
                    {
                        ViewBag.mensaje = "El código ya se encuentra registrado";
                        return View(info);
                    }
                    else
                    {
                        info.Fecha_Transac = DateTime.Now;
                        info.IdUsuario = Session["IdUsuario"].ToString();
                        if (!bus_cargo.guardarDB(info))
                            return View(info);
                        else
                            return RedirectToAction("Index", new { IdTipoCatalogo = info.IdTipoCatalogo });
                    }
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo( int IdTipoCatalogo=0)
        {
            try
            {
                ro_catalogo_Info model = new ro_catalogo_Info
                {
                    IdTipoCatalogo = IdTipoCatalogo
                };
                ViewBag.IdTipoCatalogo = IdTipoCatalogo;
                cargar_combos();
                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_catalogo_Info info)
        {
            try
            {
                info.IdUsuarioUltMod = SessionFixed.IdUsuario;
                info.Fecha_UltMod = DateTime.Now;
                info.IdUsuarioUltMod = Session["IdUsuario"].ToString();
                if (!bus_cargo.modificarDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index", new { IdTipoCatalogo = info.IdTipoCatalogo });

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Modificar(int IdTipoCatalogo,int IdCatalogo = 0)
        {
            try
            {
                cargar_combos();
                ViewBag.IdTipoCatalogo = IdTipoCatalogo;
                return View(bus_cargo.get_info(IdTipoCatalogo, IdCatalogo));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_catalogo_Info info)
        {
            try
            {
                info.Fecha_UltAnu = DateTime.Now;
                info.IdUsuarioUltAnu = SessionFixed.IdUsuario;
                if (!bus_cargo.anularDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index");


            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdTipoCatalogo, int IdCatalogo = 0)
        {
            try
            {
                cargar_combos();
                ViewBag.IdTipoCatalogo = IdTipoCatalogo;
                return View(bus_cargo.get_info(IdTipoCatalogo, IdCatalogo));

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
                lista_tipo = bus_tipo.get_list(false);
                ViewBag.lst_tipos = lista_tipo;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}