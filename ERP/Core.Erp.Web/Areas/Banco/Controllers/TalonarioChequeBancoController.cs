using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Banco;
using Core.Erp.Info.Banco;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    [SessionTimeout]
    public class TalonarioChequeBancoController : Controller
    {
        #region Variables
        ba_Talonario_cheques_x_banco_Bus bus_talonario = new ba_Talonario_cheques_x_banco_Bus();
        ba_Banco_Cuenta_Bus bus_bco_cuenta = new ba_Banco_Cuenta_Bus();
        ba_Banco_Cuenta_Bus bus_banco = new ba_Banco_Cuenta_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_talonario_cheque()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_talonario.get_list(IdEmpresa,Convert.ToInt32(SessionFixed.IdSucursal), true);
            return PartialView("_GridViewPartial_talonario_cheque", model);
        }

        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa, int IdSucursal )
        {
            var lst_banco = bus_banco.get_list(IdEmpresa,IdSucursal, false);
            ViewBag.lst_banco = lst_banco;
        }

        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            ba_Talonario_cheques_x_banco_Info model = new ba_Talonario_cheques_x_banco_Info
            {
               IdEmpresa = IdEmpresa,               
               Estado_bool = true
            };
            cargar_combos(IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal));
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ba_Talonario_cheques_x_banco_Info model)
        {
            decimal documento_inicial = Convert.ToDecimal(model.Num_cheque);
            decimal documento_final = Convert.ToDecimal(model.Documentofinal);
            int length = model.Num_cheque.Length;
            string relleno = string.Empty;
            for (int i = 0; i < length; i++)
            {
                relleno += "0";
            }
            decimal secuencia = documento_inicial;
            for (decimal i = documento_inicial; i < documento_final+1; i++)
            {

                ba_Talonario_cheques_x_banco_Info info = new ba_Talonario_cheques_x_banco_Info
                {
                    IdEmpresa = model.IdEmpresa,
                    IdBanco = model.IdBanco,
                    Num_cheque = secuencia.ToString(relleno),
                    Estado_bool = model.Estado_bool ,
                    Estado= model.Estado_bool == true ? "A" : "I",
                    Usado = model.Usado,
                };
                if (!bus_talonario.guardarDB(info))
                {
                    cargar_combos(model.IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal));
                    return View(model);
                }
                secuencia++;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdBanco = 0, string Num_cheque = "")
        {
            ba_Talonario_cheques_x_banco_Info model = bus_talonario.get_info(IdEmpresa, IdBanco, Num_cheque);
                if (model == null) 
            return RedirectToAction("Index");
            cargar_combos(IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal));
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ba_Talonario_cheques_x_banco_Info model)
        {
            if(!bus_talonario.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal));
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Json

        public JsonResult get_id(int IdEmpresa = 0, int IdBanco = 0)
        {
            var banco_cuenta = bus_bco_cuenta.get_info(IdEmpresa, IdBanco);
            var Numerocheque = bus_talonario.get_id(IdEmpresa, IdBanco, banco_cuenta.ba_num_digito_cheq);

            return Json(Numerocheque, JsonRequestBehavior.AllowGet);

        }

        public JsonResult get_num_x_bco(int IdEmpresa = 0, int IdBanco= 0)
        {
            var banco_cuenta = bus_bco_cuenta.get_info(IdEmpresa, IdBanco);
            string relleno = string.Empty;
            for (int i = 0; i < banco_cuenta.ba_num_digito_cheq; i++)
            {
                relleno += "0";
            }

            return Json(relleno, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}