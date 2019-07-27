using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ContratoController : Controller
    {
        List<ro_empleado_Info> lis_empleado = new List<ro_empleado_Info>();
        List<ro_catalogo_Info> lst_tipo_contrato =new List<ro_catalogo_Info>();
        List<ro_catalogo_Info> lst_esta_contrato = new List<ro_catalogo_Info>();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        ro_contrato_Bus bus_contrato = new ro_contrato_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();

        int IdEmpresa = 0;

        tb_persona_Bus bus_persona = new tb_persona_Bus();
        #region Metodos ComboBox bajo demanda


        public tb_persona_Bus Bus_persona
        {
            get
            {
                return bus_persona;
            }

            set
            {
                bus_persona = value;
            }
        }

        public ActionResult CmbEmpleado_contrato()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_contrato", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return Bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return Bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }


        public ActionResult CmbSucursal()
        {
            int model = new int();
            return PartialView("_CmbSucursal", model);
        }
        public List<tb_sucursal_Info> get_list_bajo_demanda_sucursal(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_sucursal.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public tb_sucursal_Info get_info_bajo_demanda_sucursal(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_sucursal.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }
        #endregion


        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);

        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_contrato(int IdSucursal = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                ViewBag.IdSucursal = IdSucursal;

                List<ro_contrato_Info> model = bus_contrato.get_list(IdEmpresa, ViewBag.IdSucursal, true);
                return PartialView("_GridViewPartial_contrato", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult GridViewPartial_contratos_por_vencer( DateTime FechaCorte)
        {
            try
            {
                ViewBag.FechaCorte = FechaCorte;
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                List<ro_contrato_Info> model = bus_contrato.get_list_contratos_por_vencer(IdEmpresa, FechaCorte);
                return PartialView("_GridViewPartial_contratos_por_vencer", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_contrato_Info info)
        {
            try
            {
                info.IdUsuario = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_contrato.guardarDB(info))
                        {
                        cargar_combo();
                        return View(info);
                        }
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo()
        {
            try
            {
                ro_contrato_Info info = new ro_contrato_Info();
                info.IdNomina = 1;
                info.FechaInicio = DateTime.Now.Date;
                info.FechaFin = DateTime.Now.Date;
                cargar_combo();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_contrato_Info info)
        {
            try
            {
                info.IdUsuarioUltMod = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    if (!bus_contrato.modificarDB(info))
                    {
                        cargar_combo();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Modificar(int IdContrato = 0, decimal IdEmpleado=0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                cargar_combo();
                return View(bus_contrato.get_info(IdEmpresa,IdEmpleado, IdContrato));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_contrato_Info info)
        {
            try
            {
                info.IdUsuarioUltAnu = SessionFixed.IdUsuario;
                if (!bus_contrato.anularDB(info))
                    {
                        cargar_combo();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index");
              
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdContrato = 0, decimal IdEmpleado=0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                cargar_combo();
                return View(bus_contrato.get_info(IdEmpresa,IdEmpleado, IdContrato));

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cargar_combo()
        {
            try
            { ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
                IdEmpresa = GetIdEmpresa();
                lis_empleado = bus_empleado.get_list_combo(IdEmpresa);
                lst_tipo_contrato = bus_catalogo.get_list_x_tipo(2);
                lst_esta_contrato = bus_catalogo.get_list_x_tipo(33);

                ViewBag.lst_empleado = lis_empleado;
                ViewBag.lst_tipo_contrato = lst_tipo_contrato;
                ViewBag.lst_estado_contrato = lst_esta_contrato;
                ViewBag.lst_nomina = bus_nomina.get_list(IdEmpresa, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int GetIdEmpresa()
        {
            try
            {
                if (Session["IdEmpresa"] != null)
                    return Convert.ToInt32(Session["IdEmpresa"]);
                else
                    return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult get_info_contato_a_liquidar(Decimal IdEmpleado)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);

            var resultado = bus_contrato.get_info_contato_a_liquidar(IdEmpresa, IdEmpleado);

            resultado.anio_ing = resultado.FechaInicio.Year;
            resultado.mes_ing = resultado.FechaInicio.Month;
            resultado.dia_in = resultado.FechaInicio.Day;

            resultado.anio_sal = resultado.FechaFin.Year;
            resultado.mes_sal = resultado.FechaFin.Month;
            resultado.dia_sal = resultado.FechaFin.Day;

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }

    public class ro_contrato_List
    {
        string Variable = "ro_contrato_Info";
        public List<ro_contrato_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_contrato_Info> list = new List<ro_contrato_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_contrato_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_contrato_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

}