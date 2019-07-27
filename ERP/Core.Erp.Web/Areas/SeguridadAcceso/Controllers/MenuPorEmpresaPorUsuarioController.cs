using Core.Erp.Bus.General;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Info.SeguridadAcceso;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.SeguridadAcceso.Controllers
{
//    [SessionTimeout]
    public class MenuPorEmpresaPorUsuarioController : Controller
    {
        #region Index

        static seg_Menu_x_Empresa_x_Usuario_Bus bus_menu_x_empresa_x_usuario = new seg_Menu_x_Empresa_x_Usuario_Bus();
        public ActionResult Index()
        {
            seg_Menu_x_Empresa_x_Usuario_Info model = new seg_Menu_x_Empresa_x_Usuario_Info();
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(seg_Menu_x_Empresa_x_Usuario_Info model)
        {
            cargar_combos();
            return View(model);
        }
        public static void CreateTreeViewNodesRecursive(List<seg_Menu_x_Empresa_x_Usuario_Info> model, MVCxTreeViewNodeCollection nodesCollection, Int32 parentID, int IdEmpresa = 0, string IdUsuario = "")
        {
            var rows = bus_menu_x_empresa_x_usuario.get_list(IdEmpresa, IdUsuario, parentID);

            foreach (seg_Menu_x_Empresa_x_Usuario_Info row in rows)
            {
                var url = string.IsNullOrEmpty(row.info_menu.web_nom_Controller) ? null :
                    ("~/" + row.info_menu.web_nom_Area + "/" + row.info_menu.web_nom_Controller + "/" + row.info_menu.web_nom_Action + "/");
                MVCxTreeViewNode node = nodesCollection.Add(row.info_menu.DescripcionMenu, row.IdMenu.ToString(), null, url);
                CreateTreeViewNodesRecursive(model, node.Nodes, row.IdMenu,IdEmpresa,IdUsuario);
            }
        }
        private void cargar_combos()
        {
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var lst_empresa = bus_empresa.get_list(false);
            ViewBag.lst_empresa = lst_empresa;

            seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
            var lst_usuario = bus_usuario.get_list(false);
            ViewBag.lst_usuario = lst_usuario;
        }
        #endregion

        [ValidateInput(false)]
        public ActionResult TreeListPartial_menu_x_usuario(int IdEmpresa = 0, string IdUsuario = "")
        {
            var model = bus_menu_x_empresa_x_usuario.get_list(IdEmpresa, IdUsuario);
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdUsuario = IdUsuario;
            ViewData["selectedIDs"] = Request.Params["selectedIDs"];
            if (ViewData["selectedIDs"] == null)
            {
                int x = 0;
                string selectedIDs = "";
                foreach (var item in model.Where(q=>q.seleccionado == true).ToList())
                {
                    if (x == 0)
                        selectedIDs = item.IdMenu.ToString();
                    else
                        selectedIDs += "," + item.IdMenu.ToString();
                    x++;
                }
                ViewData["selectedIDs"] = selectedIDs;
            }
                
            return PartialView("_TreeListPartial_menu_x_usuario", model);
        }

        public JsonResult guardar(int IdEmpresa = 0,string IdUsuario = "", string Ids = "")
        {
            string[] array = Ids.Split(',');

            List<seg_Menu_x_Empresa_x_Usuario_Info> lista = new List<seg_Menu_x_Empresa_x_Usuario_Info>();
            var output = array.GroupBy(q => q).ToList();
            foreach (var item in output)
            {
                if (!string.IsNullOrEmpty(item.Key))
                {
                    seg_Menu_x_Empresa_x_Usuario_Info info = new seg_Menu_x_Empresa_x_Usuario_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdMenu = Convert.ToInt32(item.Key),
                        IdUsuario = IdUsuario
                    };
                    lista.Add(info);
                }                
            }
                bus_menu_x_empresa_x_usuario.eliminarDB(IdEmpresa, IdUsuario);
                var resultado = bus_menu_x_empresa_x_usuario.guardarDB(lista, IdEmpresa, IdUsuario);
            

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}