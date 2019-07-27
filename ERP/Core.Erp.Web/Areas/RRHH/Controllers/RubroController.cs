using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Web.Helps;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class RubroController : Controller
    {
        List<ro_catalogo_Info> lst_tipo_rubro = new List<ro_catalogo_Info>();
        List<ro_catalogo_Info> lst_tipo_campo = new List<ro_catalogo_Info>();
        List<ro_catalogo_Info> lst_grupo = new List<ro_catalogo_Info>();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_rubro_tipo_x_jornada_Bus bus_rubro_jornada = new ro_rubro_tipo_x_jornada_Bus();
        ro_jornada_Bus bus_jornada = new ro_jornada_Bus();
        List<ct_plancta_Info> lst_plancuenta = new List<ct_plancta_Info>();
        List<ro_catalogo_Info> lst_grupo_rep_gene = new List<ro_catalogo_Info>();
        ro_rubro_tipo_x_jornada_List ListaDetalle = new ro_rubro_tipo_x_jornada_List();
        Bus.Contabilidad.ct_plancta_Bus bus_plancuenta = new Bus.Contabilidad.ct_plancta_Bus();
        public ActionResult Index()
        {
            return View();
        }

        #region Combo bajo demanda
        public ActionResult CmbRubro()
        {
            decimal model = new decimal();
            return PartialView("_CmbRubro", model);
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

        #region Rubro por Jornada
        [ValidateInput(false)]
        public ActionResult GridViewPartial_rubros_x_jornada()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            carga_combo_detalle();
            List<ro_rubro_tipo_x_jornada_Info> lista = new List<ro_rubro_tipo_x_jornada_Info>();
            lista = ListaDetalle.get_list(IdTransaccionSession);
            return PartialView("_GridViewPartial_rubros_x_jornada", lista);
        }
        private void carga_combo_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_jornada = bus_jornada.get_list(IdEmpresa, false);
            ViewBag.lst_jornada = lst_jornada;
        }
        #endregion

        [ValidateInput(false)]
        public ActionResult GridViewPartial_rubro()
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa); 

                List<ro_rubro_tipo_Info> model = bus_rubro.get_list(IdEmpresa, true);
                return PartialView("_GridViewPartial_rubro", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_rubro_tipo_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.lst_rubro_jornada = ListaDetalle.get_list(info.IdTransaccionSession);
                    if (!bus_rubro.guardarDB(info))
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
                ro_rubro_tipo_Info info = new ro_rubro_tipo_Info
                {
                    rub_GrupoResumen = "",
                    rub_grupo = "",
                    IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                    lst_rubro_jornada = new List<ro_rubro_tipo_x_jornada_Info>(),
                    IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };

                ListaDetalle.set_list(info.lst_rubro_jornada, info.IdTransaccionSession);
                cargar_combo();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_rubro_tipo_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.lst_rubro_jornada = ListaDetalle.get_list(info.IdTransaccionSession);
                    info.IdUsuarioUltMod = SessionFixed.IdUsuario;

                    if (!bus_rubro.modificarDB(info))
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

        public ActionResult Modificar(string IdRubro )
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                ro_rubro_tipo_Info model = bus_rubro.get_info(IdEmpresa, IdRubro);

                if (model == null)
                    return RedirectToAction("Index");

                model.rub_grupo = model.rub_grupo == null ? "" : model.rub_grupo;
                model.rub_GrupoResumen = model.rub_GrupoResumen == null ? "" : model.rub_GrupoResumen;
                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
                model.lst_rubro_jornada = bus_rubro_jornada.get_list(IdEmpresa, IdRubro);
                ListaDetalle.set_list(model.lst_rubro_jornada, model.IdTransaccionSession);

                cargar_combo();
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_rubro_tipo_Info info)
        {
            try
            {
                    if (!bus_rubro.anularDB(info))
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
        public ActionResult Anular(string IdRubro )
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                cargar_combo();

                ro_rubro_tipo_Info model = bus_rubro.get_info(IdEmpresa, IdRubro);
                model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
                model.lst_rubro_jornada = bus_rubro_jornada.get_list(IdEmpresa, IdRubro);
                ListaDetalle.set_list(model.lst_rubro_jornada, model.IdTransaccionSession);

                return View(model);

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
                lst_tipo_rubro = bus_catalogo.get_list_x_tipo(22);
                ViewBag.lst_tipo_rubro = lst_tipo_rubro;
                lst_tipo_campo = bus_catalogo.get_list_x_tipo(13);
                lst_grupo = bus_catalogo.get_list_x_tipo(14);
                lst_grupo.Add(new ro_catalogo_Info
                {
                    IdCatalogo = 0,
                    ca_descripcion = ""
                });
                lst_plancuenta = bus_plancuenta.get_list(GetIdEmpresa(), false, true);
                lst_grupo_rep_gene = bus_catalogo.get_list_x_tipo(43);
                lst_grupo_rep_gene.Add(new ro_catalogo_Info
                {
                    IdCatalogo = 0,
                    ca_descripcion = ""
                });

                ViewBag.lst_tipo_campo = lst_tipo_campo;
                ViewBag.lst_grupo = lst_grupo;
                ViewBag.lst_grupo_rep_gene = lst_grupo_rep_gene;
                ViewBag.lst_plancuenta = lst_plancuenta;

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

        #region Funciones del Detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ro_rubro_tipo_x_jornada_Info info_det)
        {
            if (ModelState.IsValid)
                ListaDetalle.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            List<ro_rubro_tipo_x_jornada_Info> model = new List<ro_rubro_tipo_x_jornada_Info>();
            model = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            carga_combo_detalle();
            return PartialView("_GridViewPartial_rubros_x_jornada", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_rubro_tipo_x_jornada_Info info_det)
        {
            if (ModelState.IsValid)
            {
                ListaDetalle.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
               

            List<ro_rubro_tipo_x_jornada_Info> model = new List<ro_rubro_tipo_x_jornada_Info>();
            model = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            carga_combo_detalle();
            return PartialView("_GridViewPartial_rubros_x_jornada", model);
        }

        public ActionResult EditingDelete(int secuencia)
        {
            ListaDetalle.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<ro_rubro_tipo_x_jornada_Info> model = new List<ro_rubro_tipo_x_jornada_Info>();
            model = ListaDetalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            carga_combo_detalle();
            return PartialView("_GridViewPartial_rubros_x_jornada", model);
        }
        #endregion
    }


    public class ro_rubro_tipo_Info_list
    {
        string variable = "ro_rubro_tipo_Info";
        public List<ro_rubro_tipo_Info> get_list()
        {
            if (HttpContext.Current.Session[variable] == null)
            {
                List<ro_rubro_tipo_Info> list = new List<ro_rubro_tipo_Info>();

                HttpContext.Current.Session[variable] = list;
            }
            return (List<ro_rubro_tipo_Info>)HttpContext.Current.Session[variable];
        }

        public void set_list(List<ro_rubro_tipo_Info> list)
        {
            HttpContext.Current.Session[variable] = list;
        }


    }

    public class ro_rubro_tipo_x_jornada_List
    {
        ro_jornada_Bus bus_jornada = new ro_jornada_Bus();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();

        string variable = "ro_rubro_tipo_x_jornada_Info";
        public List<ro_rubro_tipo_x_jornada_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_rubro_tipo_x_jornada_Info> list = new List<ro_rubro_tipo_x_jornada_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_rubro_tipo_x_jornada_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_rubro_tipo_x_jornada_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ro_rubro_tipo_x_jornada_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            List<ro_rubro_tipo_x_jornada_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;

            var existe = list.Where(q=> q.IdJornada == info_det.IdJornada && q.IdRubroContabilizacion == info_det.IdRubroContabilizacion).ToList();

            if (existe.Count() == 0)
            {
                if (info_det.IdRubroContabilizacion != null)
                {
                    var info_rubro = bus_rubro.get_info(IdEmpresa, info_det.IdRubroContabilizacion);
                    if (!string.IsNullOrEmpty(info_rubro.ToString()))
                        info_det.ru_descripcion = info_rubro.ru_descripcion;
                }

                if (info_det.IdJornada != 0)
                {
                    var info_jornada = bus_jornada.get_info(IdEmpresa, info_det.IdJornada);
                    if (!string.IsNullOrEmpty(info_jornada.ToString()))
                        info_det.Descripcion = info_jornada.Descripcion;
                }

                list.Add(info_det);
            }
            
        }

        public void UpdateRow(ro_rubro_tipo_x_jornada_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            ro_rubro_tipo_x_jornada_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();            

            List<ro_rubro_tipo_x_jornada_Info> list = get_list(IdTransaccionSession);
            var existe = list.Where(q => q.IdJornada == info_det.IdJornada && q.IdRubroContabilizacion == info_det.IdRubroContabilizacion).ToList();

            if (existe.Count() == 0)
            {
                if (info_det.IdRubroContabilizacion != null)
                {
                    var info_rubro = bus_rubro.get_info(IdEmpresa, info_det.IdRubroContabilizacion);
                    if (!string.IsNullOrEmpty(info_rubro.ToString()))
                        info_det.ru_descripcion = info_rubro.ru_descripcion;
                }

                if (info_det.IdJornada != 0)
                {
                    var info_jornada = bus_jornada.get_info(IdEmpresa, info_det.IdJornada);
                    if (!string.IsNullOrEmpty(info_jornada.ToString()))
                        info_det.Descripcion = info_jornada.Descripcion;
                }

                edited_info.IdJornada = info_det.IdJornada;
                edited_info.IdRubroContabilizacion = info_det.IdRubroContabilizacion;
                edited_info.ru_descripcion = info_det.ru_descripcion;
                edited_info.Descripcion = info_det.Descripcion;
            }            
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<ro_rubro_tipo_x_jornada_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == secuencia).First());
        }
    }
}