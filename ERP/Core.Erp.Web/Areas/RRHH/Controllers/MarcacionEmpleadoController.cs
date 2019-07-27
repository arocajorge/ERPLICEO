using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class MarcacionEmpleadoController : Controller
    {
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        ro_marcaciones_x_empleado_Bus bus_marcaciones = new ro_marcaciones_x_empleado_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_marcaciones_tipo_Bus bus_tipo = new ro_marcaciones_tipo_Bus();
        int IdEmpresa = 0;
        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_Marcacion()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_Marcacion", model);
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
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);

        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_marcaciones_empleado(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
                ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);

                List<ro_marcaciones_x_empleado_Info> model = bus_marcaciones.get_list(IdEmpresa, ViewBag. Fecha_ini,ViewBag. Fecha_fin);
                return PartialView("_GridViewPartial_marcaciones_empleado", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_marcaciones_x_empleado_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdUsuario = Session["IdUsuario"].ToString();

                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_marcaciones.guardarDB(info))
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
                ro_marcaciones_x_empleado_Info info = new ro_marcaciones_x_empleado_Info();
                info.es_fechaRegistro = DateTime.Now.Date;
                info.es_Hora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                cargar_combo();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_marcaciones_x_empleado_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!bus_marcaciones.modificarDB(info))
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
        public ActionResult Modificar(decimal IdEmpleado = 0, decimal IdRegistro = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                cargar_combo();
                return View(bus_marcaciones.get_info(IdEmpresa, IdEmpleado, IdRegistro));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Anular(ro_marcaciones_x_empleado_Info info)
        {
            try
            {

                if (!bus_marcaciones.anularDB(info))
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
        public ActionResult Anular(decimal IdEmpleado = 0, decimal IdRegistro = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                cargar_combo();
                return View(bus_marcaciones.get_info(IdEmpresa, IdEmpleado, IdRegistro));

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void cargar_combo()
        {
            try
            {
                bus_tipo = new ro_marcaciones_tipo_Bus();
                ViewBag.lst_tipo = bus_tipo.get_list();

                bus_empleado = new ro_empleado_Bus();
                bus_catalogo = new ro_catalogo_Bus();
                ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
                IdEmpresa = GetIdEmpresa();
                ViewBag.lst_empleado = bus_empleado.get_list_combo(IdEmpresa);
                ViewBag.lst_tipomarcacion = bus_catalogo.get_list_x_tipo(18);

               
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