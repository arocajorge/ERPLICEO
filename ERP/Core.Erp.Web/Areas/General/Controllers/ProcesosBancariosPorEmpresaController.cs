using Core.Erp.Bus.Banco;
using Core.Erp.Info.Banco;

using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General
{
    public class ProcesosBancariosPorEmpresaController : Controller
    {
        #region Index

        tb_banco_procesos_bancarios_x_empresa_Bus bus_proceso_x_empresa = new tb_banco_procesos_bancarios_x_empresa_Bus();
        tb_banco_Bus bus_bancos = new tb_banco_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_procesos_bancarios()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<tb_banco_procesos_bancarios_x_empresa_Info> model = new List<tb_banco_procesos_bancarios_x_empresa_Info>();
            model = bus_proceso_x_empresa.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_procesos_bancarios", model);
        }
        #endregion

        #region Acciones

        public ActionResult Nuevo(int IdEmpresa)
        {
            tb_banco_procesos_bancarios_x_empresa_Info model = new tb_banco_procesos_bancarios_x_empresa_Info();
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(tb_banco_procesos_bancarios_x_empresa_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (!bus_proceso_x_empresa.guardarDB(model))
                return View(model);
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa=0, int IdProceso = 0)
        {
            cargar_combos();
            tb_banco_procesos_bancarios_x_empresa_Info model = bus_proceso_x_empresa.get_info(IdEmpresa, IdProceso);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(tb_banco_procesos_bancarios_x_empresa_Info model)
        {
            if (!bus_proceso_x_empresa.modificarDB(model))
                return View(model);

            return RedirectToAction("Index");
        }
        public ActionResult Anular(int  IdEmpresa=0, int IdProceso = 0)
        {
            cargar_combos();
            tb_banco_procesos_bancarios_x_empresa_Info model = bus_proceso_x_empresa.get_info(IdEmpresa, IdProceso);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_banco_procesos_bancarios_x_empresa_Info model)
        {
            if (!bus_proceso_x_empresa.anularDB(model))
                return View(model);
            return RedirectToAction("Index");
        }
        #endregion
        private void cargar_combos()
        {



            var lst_banco = bus_bancos.get_list(false);
            ViewBag.lst_banco = lst_banco;


            var list_tipo_proceso = from cl_enumeradores.eTipoProcesoBancario s in Enum.GetValues(typeof(cl_enumeradores.eTipoProcesoBancario))
                                    select s;
            ViewBag.list_tipo_permiso = list_tipo_proceso;
        }


        public JsonResult get_list_procesos(int IdBanco = 0, int SeContabiliza = 0)
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ba_Banco_Cuenta_Bus bus_cta_bancaria = new ba_Banco_Cuenta_Bus();
                ba_Banco_Cuenta_Info info = new ba_Banco_Cuenta_Info();
                info = bus_cta_bancaria.get_info(IdEmpresa, IdBanco);
                if (info == null)
                    info = new ba_Banco_Cuenta_Info();
                bool SeContabilizaBool = SeContabiliza == 1 ? true : false;
                List<tb_banco_procesos_bancarios_x_empresa_Info> lst_periodos_x_nominas = new List<tb_banco_procesos_bancarios_x_empresa_Info>();
                lst_periodos_x_nominas = bus_proceso_x_empresa.get_list(IdEmpresa,Convert.ToInt32( info.IdBanco_Financiero), SeContabilizaBool);
                return Json(lst_periodos_x_nominas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

   


}