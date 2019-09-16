using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Inventario;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Contabilidad;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class CategoriaController : Controller
    {
        #region Index / Metodos

        in_categorias_Bus bus_categoria = new in_categorias_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_categoria()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_categoria.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_categoria", model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_ctacble = bus_plancta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_ctacble;
        }
        #endregion
        #region Metodos ComboBox bajo demanda
        public ActionResult CmbCuenta_Categ1()
        {
            in_categorias_Info model = new in_categorias_Info();
            return PartialView("_CmbCuenta_Categ1", model);
        }
        public ActionResult CmbCuenta_Categ2()
        {
            in_categorias_Info model = new in_categorias_Info();
            return PartialView("_CmbCuenta_Categ2", model);
        }
        public ActionResult CmbCuenta_Categ3()
        {
            in_categorias_Info model = new in_categorias_Info();
            return PartialView("_CmbCuenta_Categ3", model);
        }

        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Variables
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            in_categorias_Info model = new in_categorias_Info
            {
                IdEmpresa = IdEmpresa
            };
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(in_categorias_Info model)
        {
            if (bus_categoria.validar_existe_IdCategoria(model.IdEmpresa, model.IdCategoria))
            {
                ViewBag.mensaje = "El id ya se encuentra registrado";
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            model.IdUsuario = Session["IdUsuario"].ToString();
            if (!bus_categoria.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
            }
        public ActionResult Modificar(int IdEmpresa = 0 , string IdCategoria = "")
        {
            in_categorias_Info model = bus_categoria.get_info(IdEmpresa, IdCategoria);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(in_categorias_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();
            if (!bus_categoria.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0 , string IdCategoria = "")
        {
            in_categorias_Info model = bus_categoria.get_info(IdEmpresa, IdCategoria);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(in_categorias_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            if (!bus_categoria.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

    }

    public class in_categorias_List
    {
        string Variable = "in_categorias_Info";
        public List<in_categorias_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_categorias_Info> list = new List<in_categorias_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_categorias_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_categorias_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

}