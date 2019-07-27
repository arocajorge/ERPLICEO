using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class PrestamosConsultasController : Controller
    {
        #region variables
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        ro_prestamo_Bus bus_prestamos = new ro_prestamo_Bus();
        ro_prestamo_detalle_lst ro_prestamo_detalle_lst = new ro_prestamo_detalle_lst();
        #endregion
        public ActionResult Index()
        {
            return View();
        }

        #region metodo bajo demanda
        public ActionResult CmbEmpleado_novedades()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_prestamo_consulta", model);
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

        [ValidateInput(false)]
        public ActionResult CmbEmpleado_prestamo_consulta(decimal IdEmpleado=0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ro_prestamo_Info> model = bus_prestamos.get_list(IdEmpresa, IdEmpleado);
            return PartialView("_CmbEmpleado_prestamo_consulta", model);
        }

        [ValidateInput(false)]
        public ActionResult CmbEmpleado_prestamo_consulta_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ro_prestamo_detalle_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_CmbEmpleado_prestamo_consulta_det", model);
        }
    }
}