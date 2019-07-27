using Core.Erp.Bus.Banco;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Banco;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    [SessionTimeout]
    public class TipoNotaBancoController : Controller
    {
        #region Variables

        ba_tipo_nota_Bus bus_tipo = new ba_tipo_nota_Bus();
        ct_plancta_Bus bus_cuenta = new ct_plancta_Bus();
        #endregion

        #region Index

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipo_nota()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_tipo.get_list(IdEmpresa,"", true);
            return PartialView("_GridViewPartial_tipo_nota", model);
        }
        #endregion

        #region Metodos

        private void cargar_combos(int IdEmpresa)
        {
            Dictionary<string, string> lst_tipo_nota = new Dictionary<string, string>();
            lst_tipo_nota.Add("CHEQ", "Cheque");
            lst_tipo_nota.Add("DEPO", "Depósito");
            lst_tipo_nota.Add("NCBA", "Nota de crédito");
            lst_tipo_nota.Add("NDBA", "Nota de débito");
            ViewBag.lst_tipo_nota = lst_tipo_nota;
            
            var lst_cuenta = bus_cuenta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuenta = lst_cuenta;
            
        }
        #endregion

        #region Acciones

        public ActionResult Nuevo(int IdEmpresa =0 )
        {
            ba_tipo_nota_Info model = new ba_tipo_nota_Info
            {
                IdEmpresa = IdEmpresa
            };
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ba_tipo_nota_Info model)
        {
            if(!bus_tipo.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdTipoNota = 0)
        {
            ba_tipo_nota_Info model = bus_tipo.get_info(IdEmpresa, IdTipoNota);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ba_tipo_nota_Info model)
        {
            if(!bus_tipo.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdTipoNota = 0)
        {
            ba_tipo_nota_Info model = bus_tipo.get_info(IdEmpresa, IdTipoNota);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(ba_tipo_nota_Info model)
        {
            if (!bus_tipo.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

    }
}