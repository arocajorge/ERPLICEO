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
        #region Variables
        static seg_Menu_x_Empresa_x_Usuario_Bus bus_menu_x_empresa_x_usuario = new seg_Menu_x_Empresa_x_Usuario_Bus();
        seg_Menu_x_Empresa_x_Usuario_Lista_Memoria Lista_menu_usuario = new seg_Menu_x_Empresa_x_Usuario_Lista_Memoria();
        seg_Menu_x_Empresa_x_Usuario_AccesoUsuario_List ListaUsuario = new seg_Menu_x_Empresa_x_Usuario_AccesoUsuario_List();
        #endregion

        #region Index
        public ActionResult Index()
        {
            seg_Menu_x_Empresa_x_Usuario_Info model = new seg_Menu_x_Empresa_x_Usuario_Info();
            Lista_menu_usuario.set_list(new List<seg_Menu_x_Empresa_x_Usuario_Info>());
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(seg_Menu_x_Empresa_x_Usuario_Info model)
        {
            var lista_menu = bus_menu_x_empresa_x_usuario.get_list(model.IdEmpresa, model.IdUsuario, true);
            var lista_usuario = bus_menu_x_empresa_x_usuario.get_list(model.IdEmpresa, model.IdUsuario, false);

            ViewBag.IdEmpresa = model.IdEmpresa;
            ViewBag.IdUsuario = model.IdUsuario;

            Lista_menu_usuario.set_list(lista_menu);
            ListaUsuario.set_list(lista_usuario);

            cargar_combos();
            return View(model);
        }
        public static void CreateTreeViewNodesRecursive(List<seg_Menu_x_Empresa_x_Usuario_Info> model, MVCxTreeViewNodeCollection nodesCollection, Int32 parentID, int IdEmpresa = 0, string IdUsuario = "")
        {
            //var rows = bus_menu_x_empresa_x_usuario.get_list(IdEmpresa, IdUsuario, parentID);

            //foreach (seg_Menu_x_Empresa_x_Usuario_Info row in rows)
            //{
            //    var url = string.IsNullOrEmpty(row.info_menu.web_nom_Controller) ? null :
            //        ("~/" + row.info_menu.web_nom_Area + "/" + row.info_menu.web_nom_Controller + "/" + row.info_menu.web_nom_Action + "/");
            //    MVCxTreeViewNode node = nodesCollection.Add(row.info_menu.DescripcionMenu, row.IdMenu.ToString(), null, url);
            //    CreateTreeViewNodesRecursive(model, node.Nodes, row.IdMenu,IdEmpresa,IdUsuario);
            //}

            var rows = seg_Menu_x_Empresa_x_Usuario_Lista.get_list().Where(q => q.IdEmpresa == IdEmpresa && q.IdUsuario.ToLower() == IdUsuario.ToLower() && q.info_menu.IdMenuPadre == parentID).ToList();

            foreach (seg_Menu_x_Empresa_x_Usuario_Info row in rows)
            {
                var url = string.IsNullOrEmpty(row.info_menu.web_nom_Controller) ? null :
                    ("~/" + row.info_menu.web_nom_Area + "/" + row.info_menu.web_nom_Controller + "/" + row.info_menu.web_nom_Action + "/");
                MVCxTreeViewNode node = nodesCollection.Add(row.info_menu.DescripcionMenu, row.IdMenu.ToString(), null, url);
                CreateTreeViewNodesRecursive(model, node.Nodes, row.IdMenu, IdEmpresa, IdUsuario);
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
            //var model = bus_menu_x_empresa_x_usuario.get_list(IdEmpresa, IdUsuario);
            //ViewBag.IdEmpresa = IdEmpresa;
            //ViewBag.IdUsuario = IdUsuario;
            //ViewData["selectedIDs"] = Request.Params["selectedIDs"];
            //if (ViewData["selectedIDs"] == null)
            //{
            //    int x = 0;
            //    string selectedIDs = "";
            //    foreach (var item in model.Where(q=>q.seleccionado == true).ToList())
            //    {
            //        if (x == 0)
            //            selectedIDs = item.IdMenu.ToString();
            //        else
            //            selectedIDs += "," + item.IdMenu.ToString();
            //        x++;
            //    }
            //    ViewData["selectedIDs"] = selectedIDs;
            //}
                
            //return PartialView("_TreeListPartial_menu_x_usuario", model);

            var model = Lista_menu_usuario.get_list();

            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdUsuario = IdUsuario;

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

            return PartialView("_TreeListPartial_menu_x_usuario", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] seg_Menu_x_Empresa_x_Usuario_Info info)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            info.IdEmpresa = IdEmpresa;
            if (info != null)
                Lista_menu_usuario.UpdateRow(info);

            var model = Lista_menu_usuario.get_list();
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
            return PartialView("_TreeListPartial_menu_x_usuario", model);
        }
        public JsonResult guardar(int IdEmpresa = 0,string IdUsuario = "", int Modificado = 0, string Ids = "")
        {
            string[] array = Ids.Split(',');

            List<seg_Menu_x_Empresa_x_Usuario_Info> lista = new List<seg_Menu_x_Empresa_x_Usuario_Info>();
            if (Modificado == 0)
            {
                var lst_menu_usuario = Lista_menu_usuario.get_list();
                foreach (var item in lst_menu_usuario)
                {

                    if (item != null)
                        lista.Add(item);
                }
            }
            else
            {
                var lst_menu = Lista_menu_usuario.get_list();
                var output = array.GroupBy(q => q).ToList();
                foreach (var item in lst_menu)
                {
                    foreach (var item2 in output)
                    {
                        if (item.IdMenu == Convert.ToInt32(item2.Key))
                        {
                            lista.Add(item);
                        }
                    }
                }
            }

            bus_menu_x_empresa_x_usuario.eliminarDB(IdEmpresa, IdUsuario);
            var resultado = bus_menu_x_empresa_x_usuario.guardarDB(lista, IdEmpresa, IdUsuario);


            return Json(resultado, JsonRequestBehavior.AllowGet);
            //string[] array = Ids.Split(',');

            //List<seg_Menu_x_Empresa_x_Usuario_Info> lista = new List<seg_Menu_x_Empresa_x_Usuario_Info>();
            //var output = array.GroupBy(q => q).ToList();
            //foreach (var item in output)
            //{
            //    if (!string.IsNullOrEmpty(item.Key))
            //    {
            //        seg_Menu_x_Empresa_x_Usuario_Info info = new seg_Menu_x_Empresa_x_Usuario_Info
            //        {
            //            IdEmpresa = IdEmpresa,
            //            IdMenu = Convert.ToInt32(item.Key),
            //            IdUsuario = IdUsuario
            //        };
            //        lista.Add(info);
            //    }                
            //}
            //    bus_menu_x_empresa_x_usuario.eliminarDB(IdEmpresa, IdUsuario);
            //    var resultado = bus_menu_x_empresa_x_usuario.guardarDB(lista, IdEmpresa, IdUsuario);


            //return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }

    public static class seg_Menu_x_Empresa_x_Usuario_Lista
    {
        static string Variable = "fx_MenuXEmpresaXUsuarioFixed";
        public static List<seg_Menu_x_Empresa_x_Usuario_Info> get_list()
        {
            if (HttpContext.Current.Session[Variable] == null)
            {
                List<seg_Menu_x_Empresa_x_Usuario_Info> list = new List<seg_Menu_x_Empresa_x_Usuario_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<seg_Menu_x_Empresa_x_Usuario_Info>)HttpContext.Current.Session[Variable];
        }

        public static void set_list(List<seg_Menu_x_Empresa_x_Usuario_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }
    }

    public class seg_Menu_x_Empresa_x_Usuario_Lista_Memoria
    {
        static string Variable = "fx_MenuXEmpresaXUsuarioFixed_Lista";
        public List<seg_Menu_x_Empresa_x_Usuario_Info> get_list()
        {
            if (HttpContext.Current.Session[Variable] == null)
            {
                List<seg_Menu_x_Empresa_x_Usuario_Info> list = new List<seg_Menu_x_Empresa_x_Usuario_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<seg_Menu_x_Empresa_x_Usuario_Info>)HttpContext.Current.Session[Variable];
        }

        public void set_list(List<seg_Menu_x_Empresa_x_Usuario_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }

        public void UpdateRow(seg_Menu_x_Empresa_x_Usuario_Info info)
        {
            seg_Menu_x_Empresa_x_Usuario_Info edited_info = get_list().Where(q => q.IdMenu == info.IdMenu).FirstOrDefault();
            edited_info.Nuevo = info.Nuevo;
            edited_info.Modificar = info.Modificar;
            edited_info.Anular = info.Anular;
        }
    }

    public class seg_Menu_x_Empresa_x_Usuario_AccesoUsuario_List
    {
        static string Variable = "seg_Menu_x_Empresa_x_Usuario_AccesoUsuario";
        public List<seg_Menu_x_Empresa_x_Usuario_Info> get_list()
        {
            if (HttpContext.Current.Session[Variable] == null)
            {
                List<seg_Menu_x_Empresa_x_Usuario_Info> list = new List<seg_Menu_x_Empresa_x_Usuario_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<seg_Menu_x_Empresa_x_Usuario_Info>)HttpContext.Current.Session[Variable];
        }

        public void set_list(List<seg_Menu_x_Empresa_x_Usuario_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }
    }
}