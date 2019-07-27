using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.SeguridadAcceso;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Bus.General;
using Core.Erp.Web.Helps;
using Core.Erp.Info.General;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.SeguridadAcceso.Controllers
{
    //[SessionTimeout]
    public class UsuarioController : Controller
    {
        #region Index/Metodos
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        seg_Usuario_x_Empresa_Bus bus_usuario_x_empresa = new seg_Usuario_x_Empresa_Bus();
        seg_Menu_Bus bus_menu = new seg_Menu_Bus();
        seg_usuario_x_sucursal_list List_det = new seg_usuario_x_sucursal_list();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        seg_usuario_x_tb_sucursal_Bus bus_usuario_x_sucursal = new seg_usuario_x_tb_sucursal_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_usuario()
        {
            List<seg_usuario_Info> model = bus_usuario.get_list(true);
            return PartialView("_GridViewPartial_usuario", model);
        }

        private void cargar_combos(seg_usuario_Info model)
        {
            //if (!string.IsNullOrEmpty(model.IdUsuario))
            //    model.lst_usuario_x_empresa = bus_usuario_x_empresa.get_list(model.IdUsuario);
            //else
            //    model.lst_usuario_x_empresa = new List<seg_Usuario_x_Empresa_Info>();

            //tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            //var lst_empresa = bus_empresa.get_list(false);
            //if (model.lst_usuario_x_empresa.Count == 0)
            //{                
            //    foreach (var item in lst_empresa)
            //    {
            //        model.lst_usuario_x_empresa.Add(new seg_Usuario_x_Empresa_Info { IdEmpresa = item.IdEmpresa, em_nombre = item.em_nombre});
            //    }
            //}else
            //{
            //    model.lst_usuario_x_empresa = (from e in lst_empresa
            //                                   join pp in model.lst_usuario_x_empresa
            //                                   on e.IdEmpresa equals pp.IdEmpresa into temp_emp
            //                                   from pp in temp_emp.DefaultIfEmpty()
            //                                   select new seg_Usuario_x_Empresa_Info
            //                                   {
            //                                       IdEmpresa = e.IdEmpresa,
            //                                       em_nombre = e.em_nombre,
            //                                       seleccionado =  pp == null ? false : true                                                  
            //                                   }).ToList();
            //}
            var lst_menu = bus_menu.get_list_combo(false);
            lst_menu.Add(new seg_Menu_Info { IdMenu = 0, DescripcionMenu_combo = "== Seleccione ==" });
            ViewBag.lst_menu = lst_menu;
        }

        #endregion
        #region ComboBox Bajo demanda

        public ActionResult CmbEmpresa_det()
        {
            seg_usuario_x_tb_sucursal_Info model = new seg_usuario_x_tb_sucursal_Info();
            return PartialView("_CmbEmpresa_det", model);
        }
        public List<tb_empresa_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_empresa.get_list_bajo_demanda(args);
        }

        public tb_empresa_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_empresa.get_info_bajo_demanda(args);
        }
        #endregion
        #region Acciones

        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            seg_usuario_Info model = new seg_usuario_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
            };
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            var lst = bus_usuario_x_sucursal.GetList(Convert.ToString(SessionFixed.IdUsuario));
            List_det.set_list(lst, model.IdTransaccionSession);

            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(seg_usuario_Info model)
        {
            if(bus_usuario.validar_existe_usuario(model.IdUsuario))
            {
                ViewBag.mensaje = "Usuario ya se encuentra registrado";
                return View(model);
            }

            model.lst_usuario_x_sucursal = List_det.get_list(model.IdTransaccionSession);
            if (!bus_usuario.guardarDB(model))
                return View(model);
            cargar_combos(model);
            //#region Guardar usuario_x_empresa
            //bus_usuario_x_empresa.eliminarDB(model.IdUsuario);
            //model.lst_usuario_x_empresa = model.lst_usuario_x_empresa.Where(q => q.seleccionado == true).ToList();
            //model.lst_usuario_x_empresa.ForEach(q => q.IdUsuario = model.IdUsuario);
            //bus_usuario_x_empresa.guardarDB(model.lst_usuario_x_empresa);
            //#endregion

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(string IdUsuario = "")
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            seg_usuario_Info model = bus_usuario.get_info(IdUsuario);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_usuario_x_sucursal = bus_usuario_x_sucursal.GetList(model.IdUsuario);
            List_det.set_list(model.lst_usuario_x_sucursal, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(seg_usuario_Info model)
        {
            model.lst_usuario_x_sucursal = List_det.get_list(model.IdTransaccionSession);
            if (!bus_usuario.modificarDB(model))
                return View(model);

        /*    #region Guardar usuario_x_empresa
            bus_usuario_x_empresa.eliminarDB(model.IdUsuario);
            model.lst_usuario_x_empresa = model.lst_usuario_x_empresa.Where(q => q.seleccionado == true).ToList();
            model.lst_usuario_x_empresa.ForEach(q => q.IdUsuario = model.IdUsuario);
            bus_usuario_x_empresa.eliminarDB(model.IdUsuario);
            bus_usuario_x_empresa.guardarDB(model.lst_usuario_x_empresa);
            #endregion*/

            return RedirectToAction("Index");
        }

        public ActionResult Anular(string IdUsuario = "")
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            seg_usuario_Info model = bus_usuario.get_info(IdUsuario);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_usuario_x_sucursal = bus_usuario_x_sucursal.GetList(model.IdUsuario);
            List_det.set_list(model.lst_usuario_x_sucursal, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(seg_usuario_Info model)
        {
            if (!bus_usuario.anularDB(model))
                return View(model);

            return RedirectToAction("Index");
        }
        #endregion
        #region Json
        public JsonResult ResetearContrasena(string IdUsuario = "")
        {
            int resultado = 0;

            if(bus_usuario.ResetearContrasenia(IdUsuario,"1234"))
                resultado = 1;

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Detalle
        public ActionResult CargarSucursal()
        {
            int IdEmpresa = 0;
            if (Request.Params["var_IdEmpresa"] == null || !Int32.TryParse(Request.Params["var_IdEmpresa"].ToString(), out IdEmpresa))
                IdEmpresa = 0;

            return GridViewExtension.GetComboBoxCallbackResult(p =>
            {
                p.TextField = "Su_Descripcion";
                p.ValueField = "IdString";
                p.Columns.Add("Su_Descripcion", "Sucursal");
                p.TextFormatString = "{0}";
                p.ValueType = typeof(string);
                p.BindList(bus_sucursal.get_list(IdEmpresa, false));
            });
        }

        private void cargar_combos_det()
        {
            var lst_sucursal = bus_sucursal.GetListSinEmpresa(false);
            ViewBag.lst_sucursal = lst_sucursal;
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_Usuario_x_Sucursal()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_Usuario_x_Sucursal", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] seg_usuario_x_tb_sucursal_Info info_det)
        {
            if (info_det != null)
            {
                var emp = bus_empresa.get_info(info_det.IdEmpresa);

                info_det.IdSucursal = string.IsNullOrEmpty(info_det.IdString) ? 0 : Convert.ToInt32(info_det.IdString.Substring(3, 3));

                var suc = bus_sucursal.get_info(info_det.IdEmpresa, info_det.IdSucursal);
                if (suc != null && emp != null)
                {
                    info_det.IdSucursal = info_det.IdSucursal;
                    info_det.Su_Descripcion = suc.Su_Descripcion;
                    info_det.IdEmpresa = info_det.IdEmpresa;
                    info_det.em_nombre = emp.em_nombre;
                }
            }
            if (ModelState.IsValid)
            {
                seg_usuario_x_tb_sucursal_Info info_= new seg_usuario_x_tb_sucursal_Info();
                info_.IdSucursal = info_det.IdSucursal;
                info_.Su_Descripcion = info_det.Su_Descripcion;
                info_.IdEmpresa = info_det.IdEmpresa;
                info_.em_nombre = info_det.em_nombre;
                var lista = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                List_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            cargar_combos_det();
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Usuario_x_Sucursal", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] seg_usuario_x_tb_sucursal_Info info_det)
        {
            if (info_det != null)
            {
                var emp = bus_empresa.get_info(info_det.IdEmpresa);

                info_det.IdSucursal = string.IsNullOrEmpty(info_det.IdString) ? 0 : Convert.ToInt32(info_det.IdString.Substring(3, 3));

                var suc = bus_sucursal.get_info(info_det.IdEmpresa, info_det.IdSucursal);
                if (suc != null && emp != null)
                {
                    info_det.IdSucursal = info_det.IdSucursal;
                    info_det.Su_Descripcion = suc.Su_Descripcion;
                    info_det.IdEmpresa = info_det.IdEmpresa;
                    info_det.em_nombre = emp.em_nombre;
                }
            }
            if (ModelState.IsValid)
            {
                List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_Usuario_x_Sucursal", model);
        }
        public ActionResult EditingDelete(int Secuencia = 0)
        {
            List_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Usuario_x_Sucursal", model);
        }

        #endregion

    }
    public class seg_usuario_x_sucursal_list
    {
        string Variable = "seg_usuario_x_tb_sucursal_Info";
        public List<seg_usuario_x_tb_sucursal_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<seg_usuario_x_tb_sucursal_Info> list = new List<seg_usuario_x_tb_sucursal_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<seg_usuario_x_tb_sucursal_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<seg_usuario_x_tb_sucursal_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(seg_usuario_x_tb_sucursal_Info info_det, decimal IdTransaccionSession)
        {
            List<seg_usuario_x_tb_sucursal_Info> list = get_list(IdTransaccionSession);

            if (list.Where(q => q.IdEmpresa == info_det.IdEmpresa && q.IdSucursal == info_det.IdSucursal).Count() == 0)
            {
                info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
                info_det.IdUsuario = info_det.IdUsuario;
                info_det.IdSucursal = info_det.IdSucursal;
                info_det.IdEmpresa = info_det.IdEmpresa;
                info_det.em_nombre = info_det.em_nombre;
                info_det.Su_Descripcion = info_det.Su_Descripcion;
                info_det.IdString = info_det.IdString;
                list.Add(info_det);
            }

        }

        public void UpdateRow(seg_usuario_x_tb_sucursal_Info info_det, decimal IdTransaccionSession)
        {
            seg_usuario_x_tb_sucursal_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdUsuario = info_det.IdUsuario;
            edited_info.IdSucursal = info_det.IdSucursal;
            edited_info.IdEmpresa = info_det.IdEmpresa;
            edited_info.em_nombre = info_det.em_nombre;
            edited_info.Su_Descripcion = info_det.Su_Descripcion;
            edited_info.IdString = info_det.IdString;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<seg_usuario_x_tb_sucursal_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

}