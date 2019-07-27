using Core.Erp.Bus.Compras;
using Core.Erp.Bus.General;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Info.Compras;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Compras.Controllers
{
    [SessionTimeout]
    public class CompradorController : Controller
    {
        #region Variables
        com_comprador_Bus bus_comprador = new com_comprador_Bus();
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();


        #endregion
        #region Combo box bajo demanda
        public ActionResult CmbPersona_Compras()
        {
            SessionFixed.TipoPersona = Request.Params["IdTipoPersona"] != null ? Request.Params["IdTipoPersona"].ToString() : "PERSONA";
            decimal model = new decimal();
            return PartialView("_CmbPersona_Compras", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.TipoPersona);
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.TipoPersona);
        }

        #endregion
        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_comprador()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_comprador.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_comprador", model);
        }

        private void cargar_combos()
        {
            var lst_usuario = bus_usuario.get_list(false);
            ViewBag.lst_usuario = lst_usuario;
            
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo()
        {
            com_comprador_Info model = new com_comprador_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(com_comprador_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_comprador.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0, decimal IdComprador = 0)
        {
            com_comprador_Info model = bus_comprador.get_info(IdEmpresa, IdComprador);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(com_comprador_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;

            if (!bus_comprador.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0, decimal IdComprador = 0)
        {
            com_comprador_Info model = bus_comprador.get_info(IdEmpresa, IdComprador);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(com_comprador_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;

            if (!bus_comprador.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}