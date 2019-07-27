using DevExpress.Web.Mvc;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System;
using Core.Erp.Bus.Banco;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class BancoController : Controller
    {
        #region Index

        tb_banco_Bus bus_banco = new tb_banco_Bus();
        tb_banco_procesos_bancarios_x_empresa_List List_Det = new tb_banco_procesos_bancarios_x_empresa_List();
        tb_banco_procesos_bancarios_x_empresa_Bus bus_banco_det = new tb_banco_procesos_bancarios_x_empresa_Bus();

        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_banco()
        {
            List<tb_banco_Info> model = new List<tb_banco_Info>();
            model = bus_banco.get_list(true);
            return PartialView("_GridViewPartial_banco", model);
        }
        #endregion

        #region Acciones

        public ActionResult Nuevo()
        {
            #region Validar Session
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            tb_banco_Info model = new tb_banco_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                Lst_det = new List<tb_banco_procesos_bancarios_x_empresa_Info>()
            };
            List_Det.set_list(model.Lst_det, model.IdTransaccionSession);
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(tb_banco_Info model)
        {
            model.Lst_det = List_Det.get_list(model.IdTransaccionSession);
            if (!bus_banco.guardarDB(model))
            { 
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdBanco = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            tb_banco_Info model = bus_banco.get_info(IdBanco);
                if(model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.Lst_det = bus_banco_det.get_list(Convert.ToInt32(SessionFixed.IdEmpresa), model.IdBanco);
            Session["IdBancoPro"] = IdBanco;
            List_Det.set_list(model.Lst_det, model.IdTransaccionSession);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(tb_banco_Info model)
        {
            model.Lst_det = List_Det.get_list(model.IdTransaccionSession);
            if (!bus_banco.modificarDB(model))
                return View(model);

            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdBanco = 0)
        {
            tb_banco_Info model = bus_banco.get_info(IdBanco);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_banco_Info model)
        {
            if (!bus_banco.anularDB(model))
                return View(model);
            return RedirectToAction("Index");
        }
        #endregion

        #region Detalle

        private void cargar_combos_Detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ba_tipo_nota_Bus bus_nota = new ba_tipo_nota_Bus();
            var lst_nota = bus_nota.get_list(IdEmpresa, cl_enumeradores.eTipoCbteBancario.NDBA.ToString(), false);
            ViewBag.lst_nota = lst_nota;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_proceso_bancario()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            cargar_combos_Detalle();
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_proceso_bancario", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] tb_banco_procesos_bancarios_x_empresa_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (ModelState.IsValid)
            {
                info_det.IdEmpresa = IdEmpresa;
                info_det.IdBanco = Convert.ToInt32(Session["IdBancoPro"]);
                bus_banco_det.modificarDB(info_det);
                List_Det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_proceso_bancario", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] tb_banco_procesos_bancarios_x_empresa_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (ModelState.IsValid)
            {
                info_det.IdEmpresa = IdEmpresa;
                info_det.IdBanco = Convert.ToInt32(Session["IdBancoPro"]);
                bus_banco_det.modificarDB(info_det);
                List_Det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_proceso_bancario", model);
        }
        public ActionResult EditingDelete(int IdProceso)
        {
            List_Det.DeleteRow(IdProceso, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_flujo_det", model);
        }
        #endregion
    }

    public class tb_banco_List
    {
        string Variable = "tb_banco_Info";
        public List<tb_banco_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<tb_banco_Info> list = new List<tb_banco_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<tb_banco_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<tb_banco_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class tb_banco_procesos_bancarios_x_empresa_List
    {
        string Variable = "tb_banco_procesos_bancarios_x_empresa_Info";
        public List<tb_banco_procesos_bancarios_x_empresa_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<tb_banco_procesos_bancarios_x_empresa_Info> list = new List<tb_banco_procesos_bancarios_x_empresa_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<tb_banco_procesos_bancarios_x_empresa_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<tb_banco_procesos_bancarios_x_empresa_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(tb_banco_procesos_bancarios_x_empresa_Info info_det, decimal IdTransaccionSession)
        {
            List<tb_banco_procesos_bancarios_x_empresa_Info> list = get_list(IdTransaccionSession);
            info_det.IdProceso = info_det.IdProceso;
            info_det.IdBanco = info_det.IdBanco;
            info_det.IdEmpresa = info_det.IdEmpresa;
            info_det.IdTipoNota = info_det.IdTipoNota;
            info_det.NombreProceso = info_det.NombreProceso;
            info_det.Se_contabiliza = info_det.Se_contabiliza;
            info_det.CodigoLegal = info_det.CodigoLegal;
            info_det.Codigo_Empresa = info_det.Codigo_Empresa;
            info_det.Descripcion = info_det.Descripcion;
            list.Add(info_det);            
        }

        public void UpdateRow(tb_banco_procesos_bancarios_x_empresa_Info info_det, decimal IdTransaccionSession)
        {
            tb_banco_procesos_bancarios_x_empresa_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdProceso == info_det.IdProceso).First();
            edited_info.IdBanco = info_det.IdBanco;
            edited_info.IdEmpresa = info_det.IdEmpresa;
            edited_info.IdTipoNota = info_det.IdTipoNota;
            edited_info.NombreProceso = info_det.NombreProceso;
            edited_info.Se_contabiliza = info_det.Se_contabiliza;
            edited_info.CodigoLegal = info_det.CodigoLegal;
            edited_info.Codigo_Empresa = info_det.Codigo_Empresa;
            edited_info.Descripcion = info_det.Descripcion;

        }

        public void DeleteRow(int IdProceso, decimal IdTransaccionSession)
        {
            List<tb_banco_procesos_bancarios_x_empresa_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.IdProceso == IdProceso).First());
        }
    }
}