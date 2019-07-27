using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class LineaController : Controller
    {
        #region Index / Metodos
        in_linea_Bus bus_linea = new in_linea_Bus();
            in_categorias_Bus bus_categoria = new in_categorias_Bus();
        public ActionResult Index(int IdEmpresa = 0, string IdCategoria = "")
        {
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdCategoria = IdCategoria;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_linea(int IdEmpresa = 0 , string IdCategoria = "")
        {
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdCategoria = IdCategoria;
            var model = bus_linea.get_list(IdEmpresa, IdCategoria, true);
            return PartialView("_GridViewPartial_linea", model);
        }

        private void cargar_combos(int IdEmpresa)
        {
            var lst_categoria = bus_categoria.get_list(IdEmpresa, false);
            ViewBag.lst_categorias = lst_categoria;
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0 , string IdCategoria = "")
        {
            in_linea_Info model = new in_linea_Info
            {
                IdCategoria = IdCategoria,
                IdEmpresa = IdEmpresa
            };
            ViewBag.IdCategoria = IdCategoria;
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(in_linea_Info model)
        {
            model.IdUsuario = Session["IdUsuario"].ToString();
            if (!bus_linea.guardarDB(model))
            {
                ViewBag.IdCategoria = model.IdCategoria;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdCategoria = model.IdCategoria });
        }
        public ActionResult Modificar(int IdEmpresa = 0 , int IdLinea = 0, string IdCategoria = "")
        {
            in_linea_Info model = bus_linea.get_info(IdEmpresa, IdCategoria, IdLinea);
            if (model == null)
            {
                ViewBag.IdCategoria = IdCategoria;
                return RedirectToAction("Index",new { IdEmpresa = IdEmpresa, IdCategoria = IdCategoria });
            }
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(in_linea_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();
            if (!bus_linea.modificarDB(model))
            {
                ViewBag.IdCategoria = model.IdCategoria;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdCategoria = model.IdCategoria });
        }
        public ActionResult Anular(int IdEmpresa = 0 , int IdLinea = 0, string IdCategoria = "")
        {
            in_linea_Info model = bus_linea.get_info(IdEmpresa, IdCategoria, IdLinea);
            if (model == null)
            {
                ViewBag.IdCategoria = IdCategoria;
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdCategoria = model.IdCategoria });
            }
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(in_linea_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            if (!bus_linea.anularDB(model))
            {
                ViewBag.IdCategoria = model.IdCategoria;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa,  IdCategoria = model.IdCategoria });
        }

        #endregion
    }
    public class in_linea_List
    {
        string Variable = "in_linea_Info";
        public List<in_linea_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_linea_Info> list = new List<in_linea_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_linea_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_linea_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

}