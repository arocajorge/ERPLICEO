using Core.Erp.Bus.RRHH;
using Core.Erp.Info.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.General;
using Core.Erp.Bus.General;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class AnticipoDeBeneficiosController : Controller
    {
        #region Variables
        ro_EmpleadoAnticipoBeneficio_Bus bus_anticipo = new ro_EmpleadoAnticipoBeneficio_Bus();
        #endregion

        #region Index
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
        public ActionResult GridViewPartial_anticipo(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            var model = bus_anticipo.GetList(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_anticipo", model);
        }
        #endregion

        #region Accion
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            ro_EmpleadoAnticipoBeneficio_Info model = new ro_EmpleadoAnticipoBeneficio_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa ?? "0"),
                FechaDesde = new DateTime(DateTime.Now.Date.Year, 1, 1),
                FechaHasta = new DateTime(DateTime.Now.Date.Year, 12, 31),
            };
            CargarCombos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ro_EmpleadoAnticipoBeneficio_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_anticipo.GuardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Modificar",new { IdEmpresa = model.IdEmpleado, IdAnticipo = model.IdAnticipo, Exito = true});
        }

        public ActionResult Modificar(int IdEmpresa = 0, decimal IdAnticipo = 0, bool Exito = false)
        {
            ro_EmpleadoAnticipoBeneficio_Info model = bus_anticipo.GetInfo(IdEmpresa, IdAnticipo);
            if (model == null)
                return RedirectToAction("Index");
            CargarCombos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ro_EmpleadoAnticipoBeneficio_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_anticipo.ModificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpleado, IdAnticipo = model.IdAnticipo, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdAnticipo = 0, bool Exito = false)
        {
            ro_EmpleadoAnticipoBeneficio_Info model = bus_anticipo.GetInfo(IdEmpresa, IdAnticipo);
            if (model == null)
                return RedirectToAction("Index");
            CargarCombos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(ro_EmpleadoAnticipoBeneficio_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_anticipo.AnularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_anticipo()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_anticipo", model);
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

        #region Metodos
        private void CargarCombos()
        {
            Dictionary<string, string> lst_Rubro = new Dictionary<string, string>();
            lst_Rubro.Add("11", "Décimo tercer sueldo");
            lst_Rubro.Add("12", "Décimo cuarto sueldo");
            ViewBag.lst_Rubro = lst_Rubro;
        }
        #endregion
    }
}