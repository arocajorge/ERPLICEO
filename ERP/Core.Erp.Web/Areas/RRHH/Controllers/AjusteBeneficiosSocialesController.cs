using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class AjusteBeneficiosSocialesController : Controller
    {
        #region Variables
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ro_nomina_tipo_Bus bus_nomina_tipo = new ro_nomina_tipo_Bus();
        ro_rol_detalle_x_rubro_acumulado_Bus bus_detalle_rubro_acumulado = new ro_rol_detalle_x_rubro_acumulado_Bus();
        ro_rol_detalle_x_rubro_acumulado_Lista Lista_DetalleRubrosAcumulados = new ro_rol_detalle_x_rubro_acumulado_Lista();
        #endregion

        // GET: RRHH/AjusteBeneficiosSociales
        #region Index
        public ActionResult Index()
        {            
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal),
                IdNomina = 1,
                IdRubro = "11",
                IdSigno = "+",
                Valor= 0,
            };

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            model.IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            cargar_combos(model.IdEmpresa);
            List<ro_rol_detalle_x_rubro_acumulado_Info> lst_detalle = bus_detalle_rubro_acumulado.GetList_BeneficiosSociales(model.IdEmpresa, model.IdSucursal, model.IdNomina, model.IdRubro, Convert.ToDateTime(model.fecha_ini), Convert.ToDateTime(model.fecha_fin));
            Lista_DetalleRubrosAcumulados.set_list(lst_detalle, model.IdTransaccionSession);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_AjusteBeneficiosSociales(DateTime? FechaIni, DateTime? FechaFin, int IdSucursal = 0, int IdNomina_Tipo = 0, string IdRubro="")
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdSucursal = IdSucursal == 0 ? 0 : Convert.ToInt32(IdSucursal);
            ViewBag.IdNomina_Tipo = IdNomina_Tipo == 0 ? 0 : Convert.ToInt32(IdNomina_Tipo);
            ViewBag.IdRubro = IdRubro == "" ? "" : Convert.ToString(IdRubro);
            ViewBag.FechaIni = FechaIni == null ? DateTime.Now : FechaIni;
            ViewBag.FechaFin = FechaFin == null ? DateTime.Now : FechaFin;
            
            var lst_detalle = Lista_DetalleRubrosAcumulados.get_list(IdTransaccionSession);
            return PartialView("_GridViewPartial_AjusteBeneficiosSociales", lst_detalle);
        }
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            try
            {
                var lst_Sucursal = bus_sucursal.get_list(IdEmpresa, false);
                ViewBag.lst_Sucursal = lst_Sucursal;

                var lst_tipo_nomina = bus_nomina_tipo.get_list(IdEmpresa, false);
                ViewBag.lst_tipo_nomina = lst_tipo_nomina;

                Dictionary<string, string> lst_Rubro = new Dictionary<string, string>();
                lst_Rubro.Add("11", "Décimo tercer sueldo");
                lst_Rubro.Add("12", "Décimo cuarto sueldo");
                ViewBag.lst_Rubro = lst_Rubro;

                Dictionary<string, string> lst_Tipo = new Dictionary<string, string>();
                lst_Tipo.Add("+", "+");
                lst_Tipo.Add("-", "-");
                ViewBag.lst_Tipo = lst_Tipo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Json
        public JsonResult ActualizarValores(string Ids = "", string IdSigno = "", double Valor = 0)
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
        
            string[] array = Ids.Split(',');

            if (string.IsNullOrEmpty(Ids))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {                
                List<ro_rol_detalle_x_rubro_acumulado_Info> Lista = Lista_DetalleRubrosAcumulados.get_list(IdTransaccionSession);

                foreach (var item in array)
                {
                    foreach (var item2 in Lista)
                    {
                        if (IdEmpresa == item2.IdEmpresa && item2.Secuencial == Convert.ToInt32(item))
                        {
                            if (IdSigno == "+")
                            {
                                item2.ValorAjustado = item2.Valor + Valor;
                            }
                            else if (IdSigno == "-")
                            {
                                item2.ValorAjustado = item2.Valor - Valor;
                            }

                            break;
                        }                        
                    }

                    Lista_DetalleRubrosAcumulados.set_list(Lista, IdTransaccionSession);
                }
                return Json(Lista, JsonRequestBehavior.AllowGet);
            }                                                
        }

        public JsonResult ModificarBD(string Ids = "")
        {
            var IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var modificar = true;
            string[] array = Ids.Split(',');

            if (string.IsNullOrEmpty(Ids))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<ro_rol_detalle_x_rubro_acumulado_Info> Lista = Lista_DetalleRubrosAcumulados.get_list(IdTransaccionSession);
                List<ro_rol_detalle_x_rubro_acumulado_Info> ListaModificar = new List<ro_rol_detalle_x_rubro_acumulado_Info>();
                ro_rol_detalle_x_rubro_acumulado_Info info = new ro_rol_detalle_x_rubro_acumulado_Info();
                foreach (var item in array)
                {
                    info = Lista.Where(q => q.IdEmpresa == IdEmpresa && q.Secuencial == Convert.ToInt32(item)).FirstOrDefault();

                    if (info != null)
                    {
                        ListaModificar.Add(info);
                    }
                }

                if (!bus_detalle_rubro_acumulado.ModificarBD(ListaModificar))
                {
                    modificar = false;
                }

                Lista_DetalleRubrosAcumulados.set_list(new List<ro_rol_detalle_x_rubro_acumulado_Info>(), IdTransaccionSession);
                return Json(modificar, JsonRequestBehavior.AllowGet);
            }            
        }
        #endregion
    }

    public class ro_rol_detalle_x_rubro_acumulado_Lista
    {
        string variable = "ro_rol_detalle_x_rubro_acumulado_Info";
        public List<ro_rol_detalle_x_rubro_acumulado_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_rol_detalle_x_rubro_acumulado_Info> list = new List<ro_rol_detalle_x_rubro_acumulado_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_rol_detalle_x_rubro_acumulado_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_rol_detalle_x_rubro_acumulado_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

    }
}