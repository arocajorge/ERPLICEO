using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.General;
using Core.Erp.Bus.General;
using Core.Erp.Info.SeguridadAcceso;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.SeguridadAcceso.Controllers
{
    //[SessionTimeout]
    public class MenuPorEmpresaController : Controller
    {
        #region Index

        seg_Menu_x_Empresa_Bus bus_menu_x_empresa = new seg_Menu_x_Empresa_Bus();
        public ActionResult Index()
        {
            seg_Menu_x_Empresa_Info model = new seg_Menu_x_Empresa_Info();
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(seg_Menu_x_Empresa_Info model)
        {
            cargar_combos();
            return View(model);
        }

        private void cargar_combos()
        {
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var lst_empresa = bus_empresa.get_list(false);
            ViewBag.lst_empresa = lst_empresa;
        }
        #endregion

        [ValidateInput(false)]
        public ActionResult TreeListPartial_menu_x_empresa(int IdEmpresa = 0)
        {
            List<seg_Menu_x_Empresa_Info> model = bus_menu_x_empresa.get_list(IdEmpresa);
            ViewBag.IdEmpresa = IdEmpresa;
            ViewData["selectedIDs"] = Request.Params["selectedIDs"];
            if (ViewData["selectedIDs"] == null)
            {
                int x = 0;
                string selectedIDs = "";
                foreach (var item in model.Where(q => q.seleccionado == true).ToList())
                {
                    if (x == 0)
                        selectedIDs = item.IdMenu.ToString();
                    else
                        selectedIDs += "," + item.IdMenu.ToString();
                    x++;
                }
                ViewData["selectedIDs"] = selectedIDs;
            }
            return PartialView("_TreeListPartial_menu_x_empresa", model);
        }

        public JsonResult guardar(int IdEmpresa = 0, string Ids = "")
        {            
            string[] array = Ids.Split(',');
            List<seg_Menu_x_Empresa_Info> lista = new List<seg_Menu_x_Empresa_Info>();
            var output = array.GroupBy(q => q).ToList();
            foreach (var item in output)
            {
                seg_Menu_x_Empresa_Info info = new seg_Menu_x_Empresa_Info
                {
                    IdEmpresa = IdEmpresa,
                    IdMenu = Convert.ToInt32(item.Key),                    
                };
                lista.Add(info);
            }
            bus_menu_x_empresa.eliminarDB(IdEmpresa);
            var resultado = bus_menu_x_empresa.guardarDB(lista);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

       
    }
}