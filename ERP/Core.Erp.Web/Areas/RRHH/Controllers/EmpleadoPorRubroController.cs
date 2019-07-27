using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Bus.General;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class EmpleadoPorRubroController : Controller
    {
        #region variables
        ro_empleado_x_ro_rubro_Bus bus_rubro_fijos = new ro_empleado_x_ro_rubro_Bus();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        int IdEmpresa = 0;
        #endregion

        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_rubros_fijos()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_rubros_fijos", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }

        public ActionResult CmbRubro_EmpxRub()
        {
            ro_empleado_x_ro_rubro_Info model = new ro_empleado_x_ro_rubro_Info();
            return PartialView("_CmbRubro_EmpxRub", model);
        }
        public List<ro_rubro_tipo_Info> get_list_bajo_demanda_rubro(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_rubro.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ro_rubro_tipo_Info get_info_bajo_demanda_rubro(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_rubro.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }
        #endregion

        #region Vistas
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_rubros_fijos()
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                bus_rubro_fijos = new ro_empleado_x_ro_rubro_Bus();
                List<ro_empleado_x_ro_rubro_Info> model = bus_rubro_fijos.get_list(IdEmpresa);
                return PartialView("_GridViewPartial_rubros_fijos", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Acciones
        [HttpPost]
        public ActionResult Nuevo(ro_empleado_x_ro_rubro_Info info)
        {
            try
            {

                IdEmpresa = GetIdEmpresa();
                info.IdEmpresa = IdEmpresa;
                info.IdUsuario = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    info.Valor = Math.Round(info.Valor,2);
                    if (!bus_rubro_fijos.guardarDB(info))
                    {
                        cargar_combos(0);
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index", new { IdEmpleado = info.IdEmpleado });

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
                ro_empleado_x_ro_rubro_Info model = new ro_empleado_x_ro_rubro_Info
                {
                    IdNomina_Tipo = 1,
                    FechaInicio=DateTime.Now.Date,
                    FechaFin = DateTime.Now.AddMonths(3)
                };
                cargar_combos(0);
                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_empleado_x_ro_rubro_Info info)
        {
            try
            {
                info.IdUsuarioUltMod = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    info.Valor = Math.Round(info.Valor,2);
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_rubro_fijos.modificarDB(info))
                    {
                        cargar_combos(info.IdNomina_Tipo);
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index", new { IdEmpleado = info.IdEmpleado });
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Modificar(int IdNomina_Tipo = 0, int IdRubroFijo = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                ro_empleado_x_ro_rubro_Info model = new ro_empleado_x_ro_rubro_Info();
                model = bus_rubro_fijos.get_info(IdEmpresa, IdRubroFijo);
                cargar_combos(IdNomina_Tipo);

                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_empleado_x_ro_rubro_Info info)
        {
            try
            {
                info.IdUsuarioUltAnu = SessionFixed.IdUsuario;
                if (!bus_rubro_fijos.anularDB(info))
                {
                    cargar_combos(info.IdNomina_Tipo);
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
        public ActionResult Anular(int IdNomina_Tipo = 0, int IdRubroFijo = 0)
        {
            try
            {
                ro_empleado_x_ro_rubro_Info model = new ro_empleado_x_ro_rubro_Info();
                cargar_combos(IdNomina_Tipo);
                IdEmpresa = GetIdEmpresa();
                model = bus_rubro_fijos.get_info(IdEmpresa, IdRubroFijo);
                if (model == null)
                    return RedirectToAction("Index");
                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        private void cargar_combos(int IdNominaTipo)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                ViewBag.lst_rubro = bus_rubro.get_list_rub_concepto(IdEmpresa);
                ViewBag.lst_nomina = bus_nomina.get_list(IdEmpresa, false);
                ViewBag.lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, IdNominaTipo);
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


    }
}