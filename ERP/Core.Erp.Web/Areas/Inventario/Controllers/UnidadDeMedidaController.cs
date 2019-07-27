using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.Inventario;
using Core.Erp.Bus.Inventario;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class UnidadDeMedidaController : Controller
    {
        #region Index/Metodos

        in_UnidadMedida_Bus bus_unidad_medida = new in_UnidadMedida_Bus();
        in_UnidadMedida_Equiv_conversion_Bus bus_unidad_medida_equiv = new in_UnidadMedida_Equiv_conversion_Bus();
        in_UnidadMedida_Equiv_conversion_List list_unidad_medida_equiv = new in_UnidadMedida_Equiv_conversion_List();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_unidad_medida()
        {
            List<in_UnidadMedida_Info> model = bus_unidad_medida.get_list(true);
            return PartialView("_GridViewPartial_unidad_medida", model);
        }
        private void cargar_combos()
        {
            var lst_unidad_medida = bus_unidad_medida.get_list(false);
            ViewBag.lst_unidad_medida = lst_unidad_medida;
        }
        #endregion

        #region Acciones

        public ActionResult Nuevo()
        {
            in_UnidadMedida_Info model = new in_UnidadMedida_Info();
            model.lst_unidad_medida_equiv = new List<in_UnidadMedida_Equiv_conversion_Info>();
            list_unidad_medida_equiv.set_list(model.lst_unidad_medida_equiv);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(in_UnidadMedida_Info model)
        {
            if (bus_unidad_medida.validar_existe_IdUnidadMedida(model.IdUnidadMedida))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                return View(model);
            }
            if (!bus_unidad_medida.guardarDB(model))
            {
                return View(model);
            }
            model.lst_unidad_medida_equiv = list_unidad_medida_equiv.get_list();
            model.lst_unidad_medida_equiv.ForEach(q => q.IdUnidadMedida = model.IdUnidadMedida);
            if (!bus_unidad_medida_equiv.guardarDB(model.lst_unidad_medida_equiv))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(string IdUnidadMedida = "")
        {
            in_UnidadMedida_Info model = bus_unidad_medida.get_info(IdUnidadMedida);
            if(model == null)
                return RedirectToAction("Index");
            model.lst_unidad_medida_equiv = bus_unidad_medida_equiv.get_list(IdUnidadMedida);
            list_unidad_medida_equiv.set_list(model.lst_unidad_medida_equiv);            
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(in_UnidadMedida_Info model)
        {
            if (!bus_unidad_medida.modificarDB(model))
            {
                return View(model);
            }
            model.lst_unidad_medida_equiv = list_unidad_medida_equiv.get_list();
            model.lst_unidad_medida_equiv.ForEach(q => q.IdUnidadMedida = model.IdUnidadMedida);
            if (bus_unidad_medida_equiv.eliminarDB(model.IdUnidadMedida))
            {
                if(!bus_unidad_medida_equiv.guardarDB(model.lst_unidad_medida_equiv))
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(string IdUnidadMedida = "")
        {
            in_UnidadMedida_Info model = bus_unidad_medida.get_info(IdUnidadMedida);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_unidad_medida_equiv = bus_unidad_medida_equiv.get_list(IdUnidadMedida);
            list_unidad_medida_equiv.set_list(model.lst_unidad_medida_equiv);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(in_UnidadMedida_Info model)
        {
            if (!bus_unidad_medida.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Grids

        [ValidateInput(false)]
        public ActionResult GridViewPartial_unidad_medida_det(string IdUnidadMedida = "")
        {
            in_UnidadMedida_Info model = new in_UnidadMedida_Info();
            model.lst_unidad_medida_equiv = bus_unidad_medida_equiv.get_list(IdUnidadMedida);
            if (model.lst_unidad_medida_equiv.Count == 0)
                model.lst_unidad_medida_equiv = list_unidad_medida_equiv.get_list();
            cargar_combos();
            return PartialView("_GridViewPartial_unidad_medida_det", model);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] in_UnidadMedida_Equiv_conversion_Info info_det)
        {
            if (ModelState.IsValid)
                list_unidad_medida_equiv.AddRow(info_det);
            in_UnidadMedida_Info model = new in_UnidadMedida_Info();
            model.lst_unidad_medida_equiv = list_unidad_medida_equiv.get_list();
            cargar_combos();
            return PartialView("_GridViewPartial_unidad_medida_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] in_UnidadMedida_Equiv_conversion_Info info_det)
        {
            if (ModelState.IsValid)
                list_unidad_medida_equiv.UpdateRow(info_det);
            in_UnidadMedida_Info model = new in_UnidadMedida_Info();
            model.lst_unidad_medida_equiv = list_unidad_medida_equiv.get_list();
            cargar_combos();
            return PartialView("_GridViewPartial_unidad_medida_det", model);
        }

        public ActionResult EditingDelete(int secuencia)
        {
            list_unidad_medida_equiv.DeleteRow(secuencia);
            in_UnidadMedida_Info model = new in_UnidadMedida_Info();
            model.lst_unidad_medida_equiv = list_unidad_medida_equiv.get_list();
            cargar_combos();
            return PartialView("_GridViewPartial_unidad_medida_det", model);
        }
        #endregion
    }

    public class in_UnidadMedida_Equiv_conversion_List
    {
        public List<in_UnidadMedida_Equiv_conversion_Info> get_list()
        {
            if (HttpContext.Current.Session["in_UnidadMedida_Equiv_conversion_Info"] == null)
            {
                List<in_UnidadMedida_Equiv_conversion_Info> list = new List<in_UnidadMedida_Equiv_conversion_Info>();

                HttpContext.Current.Session["in_UnidadMedida_Equiv_conversion_Info"] = list;
            }
            return (List<in_UnidadMedida_Equiv_conversion_Info>)HttpContext.Current.Session["in_UnidadMedida_Equiv_conversion_Info"];
        }

        public void set_list(List<in_UnidadMedida_Equiv_conversion_Info> list)
        {
            HttpContext.Current.Session["in_UnidadMedida_Equiv_conversion_Info"] = list;
        }

        public void AddRow(in_UnidadMedida_Equiv_conversion_Info info_det)
        {
            List<in_UnidadMedida_Equiv_conversion_Info> list = get_list();
            info_det.secuencia = list.Count == 0 ? 1 : list.Max(q => q.secuencia) + 1;
            list.Add(info_det);
        }

        public void UpdateRow(in_UnidadMedida_Equiv_conversion_Info info_det)
        {
            in_UnidadMedida_Equiv_conversion_Info edited_info = get_list().Where(m => m.secuencia == info_det.secuencia).First();
            edited_info.IdUnidadMedida_equiva = info_det.IdUnidadMedida_equiva;
            edited_info.valor_equiv = info_det.valor_equiv;
        }

        public void DeleteRow(int secuencia)
        {
            List<in_UnidadMedida_Equiv_conversion_Info> list = get_list();
            list.Remove(list.Where(m => m.secuencia == secuencia).First());
        }
    }
}