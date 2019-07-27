using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.CuentasPorCobrar;
using Core.Erp.Bus.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorCobrar.Controllers
{
    public class MotivoLiquidacionController : Controller
    {
        #region Variables

        cxc_MotivoLiquidacionTarjeta_Bus bus_motivo = new cxc_MotivoLiquidacionTarjeta_Bus();
        cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Bus bus_motivo_det = new cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Bus();

        cxc_Motivo_Liquidacion_Det_List List_Det = new cxc_Motivo_Liquidacion_Det_List();
        #endregion
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_Motivo_Liquidacion()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_motivo.GetList(IdEmpresa, true);
            return PartialView("_GridViewPartial_Motivo_Liquidacion", model);
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
            cxc_MotivoLiquidacionTarjeta_Info model = new cxc_MotivoLiquidacionTarjeta_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                Lst_det = new List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info>()
            };
            List_Det.set_list(model.Lst_det, model.IdTransaccionSession);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(cxc_MotivoLiquidacionTarjeta_Info model)
        {
            model.IdUsuarioCreacion = Convert.ToString(SessionFixed.IdUsuario);
            model.Lst_det = List_Det.get_list(model.IdTransaccionSession);
            if (!bus_motivo.GuardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0 , decimal IdMotivo = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cxc_MotivoLiquidacionTarjeta_Info model = bus_motivo.GEtInfo(IdEmpresa, IdMotivo);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.Lst_det = bus_motivo_det.GetList(IdEmpresa, IdMotivo);
            List_Det.set_list(model.Lst_det, model.IdTransaccionSession);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(cxc_MotivoLiquidacionTarjeta_Info model)
        {
            model.IdUsuarioModificacion = Convert.ToString(SessionFixed.IdUsuario);
            model.Lst_det = List_Det.get_list(model.IdTransaccionSession);
            if (!bus_motivo.ModificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0, decimal IdMotivo = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cxc_MotivoLiquidacionTarjeta_Info model = bus_motivo.GEtInfo(IdEmpresa, IdMotivo);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.Lst_det = bus_motivo_det.GetList(IdEmpresa, IdMotivo);
            List_Det.set_list(model.Lst_det, model.IdTransaccionSession);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(cxc_MotivoLiquidacionTarjeta_Info model)
        {
            model.IdUsuarioAnulacion = Convert.ToString(SessionFixed.IdUsuario);
            if (!bus_motivo.AnularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
        #region Metodos ComboBox bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_Motivo_Det()
        {
            cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info model = new cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info();
            return PartialView("_CmbCuenta_Motivo_Det", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Detalle
        private void cargar_combos_Detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;
            
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_motivo_x_sucursal()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            cargar_combos_Detalle();
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_motivo_x_sucursal", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (info_det != null)
                if (info_det.IdCtaCble != "")
                {
                    ct_plancta_Info info_cuenta = bus_plancta.get_info(IdEmpresa, info_det.IdCtaCble);
                    if (info_cuenta != null)
                    {
                        info_det.pc_Cuenta = info_cuenta.pc_Cuenta;
                    }
                }
            if (ModelState.IsValid)
                List_Det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_motivo_x_sucursal", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info info_det)
        {

            if (ModelState.IsValid)
                List_Det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_motivo_x_sucursal", model);
        }
        public ActionResult EditingDelete(int Secuencia)
        {
            List_Det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_Detalle();
            return PartialView("_GridViewPartial_motivo_x_sucursal", model);
        }
        #endregion

    }
    public class cxc_Motivo_Liquidacion_Det_List
    {
        string Variable = "cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info";
        public List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info> list = new List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info info_det, decimal IdTransaccionSession)
        {
            List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.IdSucursal = info_det.IdSucursal;
            info_det.IdCtaCble = info_det.IdCtaCble;
            info_det.pc_Cuenta = info_det.pc_Cuenta;
            info_det.Su_Descripcion = info_det.Su_Descripcion;
            list.Add(info_det);
        }

        public void UpdateRow(cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info info_det, decimal IdTransaccionSession)
        {
            cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdSucursal = info_det.IdSucursal;
            edited_info.Su_Descripcion = info_det.Su_Descripcion;
            edited_info.IdCtaCble = info_det.IdCtaCble;
            edited_info.pc_Cuenta = info_det.pc_Cuenta;
            edited_info.Secuencia = info_det.Secuencia;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

}