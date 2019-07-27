using Core.Erp.Bus.Caja;
using Core.Erp.Bus.General;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Info.General;
using Core.Erp.Info.SeguridadAcceso;
using Core.Erp.Web.Helps;
using Core.Erp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Core.Erp.Web.Controllers
{
    public class AccountController : Controller
    {
        seg_Usuario_x_Empresa_Bus bus_usuario_x_empresa = new seg_Usuario_x_Empresa_Bus();
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        seg_Menu_Bus bus_menu = new seg_Menu_Bus();
        caj_Caja_Bus bus_caja = new caj_Caja_Bus();
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            seg_usuario_Info info_usuario = bus_usuario.validar_login(model.IdUsuario, model.Contrasena);
            if(info_usuario != null)
            {
                if (info_usuario.CambiarContraseniaSgtSesion)
                    return RedirectToAction("CambiarContrasena", new { IdUsuario = model.IdUsuario });
                return RedirectToAction("LoginEmpresa",new {IdUsuario = model.IdUsuario});
            }
            ViewBag.mensaje = "Credenciales incorrectas";
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult LoginEmpresa(string IdUsuario = "")
        {
            var lst = bus_usuario_x_empresa.get_list(IdUsuario);
            if (lst.Count == 0)
                return RedirectToAction("Login");
             ViewBag.lst_empresas = lst;
            LoginModel model = new LoginModel
            {
                IdUsuario = IdUsuario,
                IdEmpresa = lst.FirstOrDefault().IdEmpresa
            };
            cargar_combos(model.IdEmpresa, IdUsuario);
            return View(model);
        }
        [HttpPost]
        public ActionResult LoginEmpresa(LoginModel model)
        {
            var info_empresa = bus_empresa.get_info(model.IdEmpresa);
            if (info_empresa == null)
            {
                cargar_combos(model.IdEmpresa, model.IdUsuario);
                return View(model);
            }
            Session["IdUsuario"] = model.IdUsuario;            
            Session["IdEmpresa"] = model.IdEmpresa;
            Session["nom_empresa"] = info_empresa.em_nombre;
            Session["IdSucursal"] = model.IdSucursal;
            Session["em_direccion"] = info_empresa.em_direccion;
            SessionFixed.NomEmpresa = info_empresa.em_nombre;
            SessionFixed.Ruc = info_empresa.em_ruc;
            SessionFixed.IdEmpresa = model.IdEmpresa.ToString();
            SessionFixed.IdSucursal = model.IdSucursal.ToString();
            SessionFixed.em_direccion = info_empresa.em_direccion;
            SessionFixed.IdTransaccionSession = string.IsNullOrEmpty(SessionFixed.IdTransaccionSession) ? "1" : (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;            
            
            var usuario = bus_usuario.get_info(model.IdUsuario);
            if (usuario != null )
            {
                SessionFixed.IdUsuario = usuario.IdUsuario;
                SessionFixed.EsSuperAdmin = usuario.es_super_admin.ToString();
                SessionFixed.IdCaja = bus_caja.GetIdCajaPorUsuario(model.IdEmpresa, SessionFixed.IdUsuario).ToString();

                if (usuario.IdMenu != null)
                {
                    var menu = bus_menu.get_info((int)usuario.IdMenu);
                    if (menu != null && !string.IsNullOrEmpty(menu.web_nom_Action))
                        return RedirectToAction(menu.web_nom_Action, menu.web_nom_Controller, new { Area = menu.web_nom_Area });
                }                
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Contents.RemoveAll();
            return RedirectToAction("Login");
        }

        public ActionResult CambiarContrasena(string IdUsuario = "")
        {
            LoginModel model = new LoginModel { IdUsuario = IdUsuario};
            return View(model);
        }
        [HttpPost]
        public ActionResult CambiarContrasena(LoginModel model)
        {
            model.Contrasena = string.IsNullOrEmpty(model.Contrasena) ? "" : model.Contrasena.Trim();
            model.new_Contrasena = string.IsNullOrEmpty(model.new_Contrasena) ? "" : model.new_Contrasena.Trim();
            if (model.Contrasena == model.new_Contrasena)
            {
                ViewBag.mensaje = "La nueva contraseña no debe ser igual a la anterior";
                return View(model);
            }
            if (!bus_usuario.modificarDB(model.IdUsuario,model.Contrasena,model.new_Contrasena))
            {
                ViewBag.mensaje = "Credenciales incorrectas, por favor corrija";
                return View(model);
            }
            return RedirectToAction("Login");
        }
        private void cargar_combos(int IdEmpresa, string IdUsuario)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;
        }
        #region Json
        public JsonResult cargar_sucursal_x_empresa(int IdEmpresa = 0, string IdUsuario = "")
        {           
            var login = bus_sucursal.GetList(IdEmpresa, IdUsuario, false);
            return Json(login, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}