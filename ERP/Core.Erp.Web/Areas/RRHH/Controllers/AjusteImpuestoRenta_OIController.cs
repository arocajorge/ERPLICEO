using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class AjusteImpuestoRenta_OIController : Controller
    {
        #region variables
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        ro_AjusteImpuestoRenta_Bus bus_ajuste = new ro_AjusteImpuestoRenta_Bus();
        ro_AjusteImpuestoRentaDetOI_Bus bus_ajuste_ir_det_oi = new ro_AjusteImpuestoRentaDetOI_Bus();
        ro_AjusteImpuestoRentaDetOI_List Lista_Ajuste_OI = new ro_AjusteImpuestoRentaDetOI_List();
        string mensaje = string.Empty;
        #endregion

        #region Combo bajo demanda
        public ActionResult CmbEmpleado()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0, int IdAjuste=0, decimal IdEmpleado = 0)
        {
            try
            {
                #region Validar Session
                if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                    return RedirectToAction("Login", new { Area = "", Controller = "Account" });
                SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
                SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
                #endregion

                ro_AjusteImpuestoRenta_Info info = new ro_AjusteImpuestoRenta_Info
                {
                    IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                    IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                    IdAjuste = IdAjuste,
                    IdEmpleado = IdEmpleado,
                    lst_det_oi = new List<ro_AjusteImpuestoRentaDetOI_Info>()
                };
                info.lst_det_oi = bus_ajuste_ir_det_oi.GetList(info.IdEmpresa, info.IdAjuste, info.IdEmpleado);
                Lista_Ajuste_OI.set_list(info.lst_det_oi, info.IdTransaccionSession);
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Nuevo(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                info.lst_det_oi = Lista_Ajuste_OI.get_list(info.IdTransaccionSession);
                var info_ajuste = bus_ajuste.get_info(info.IdEmpresa, info.IdAjuste);
                info.IdAnio = info_ajuste.IdAnio;
                info.IdSucursal = info_ajuste.IdSucursal;
                info.IdUsuario = SessionFixed.IdUsuario;
                info.Fecha = info_ajuste.Fecha;
                info.FechaCorte = info_ajuste.FechaCorte;
                info.Observacion = info_ajuste.Observacion;

                if (!bus_ajuste_ir_det_oi.guardarDB(info))
                {
                    mensaje = "No se puede guardar el registro";
                    ViewBag.mensaje = mensaje;
                    return View(info);
                }

                //return RedirectToAction("Index","AjusteImpuestoRenta");
                return RedirectToAction("Modificar", "AjusteImpuestoRenta", new { IdEmpresa = info.IdEmpresa, IdAjuste = info.IdAjuste });
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Batch
        [ValidateInput(false)]
        public ActionResult GridViewPartial_AjusteImpuestoRenta_OI()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Ajuste_OI.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            //ViewBag.BatchEditingOptions = options;
            return PartialView("_GridViewPartial_AjusteImpuestoRenta_OI", model);
        }

        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<ro_AjusteImpuestoRentaDetOI_Info, int> updateValues)
        {
            foreach (var ajuste in updateValues.Insert)
            {
                if (updateValues.IsValid(ajuste))
                    Lista_Ajuste_OI.InsertRow(ajuste, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            foreach (var ajuste in updateValues.Update)
            {
                if (updateValues.IsValid(ajuste))
                    Lista_Ajuste_OI.UpdateRow(ajuste, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            foreach (var ajuste in updateValues.DeleteKeys)
            {
                Lista_Ajuste_OI.DeleteRow(ajuste, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            return GridViewPartial_AjusteImpuestoRenta_OI();
        }
        #endregion
    }

    public class ro_AjusteImpuestoRentaDetOI_List
    {
        string Variable = "ro_AjusteImpuestoRentaDetOI_Info";
        public List<ro_AjusteImpuestoRentaDetOI_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_AjusteImpuestoRentaDetOI_Info> list = new List<ro_AjusteImpuestoRentaDetOI_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_AjusteImpuestoRentaDetOI_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_AjusteImpuestoRentaDetOI_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void InsertRow(ro_AjusteImpuestoRentaDetOI_Info info_det, decimal IdTransaccionSession)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ro_AjusteImpuestoRentaDetOI_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;

            list.Add(info_det);
        }
        public void UpdateRow(ro_AjusteImpuestoRentaDetOI_Info info_det, decimal IdTransaccionSession)
        {
            ro_AjusteImpuestoRentaDetOI_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).FirstOrDefault();
            edited_info.DescripcionOI = info_det.DescripcionOI;
            edited_info.Valor = info_det.Valor;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ro_AjusteImpuestoRentaDetOI_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }
}