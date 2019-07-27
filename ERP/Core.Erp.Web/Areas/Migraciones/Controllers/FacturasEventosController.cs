using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.Migraciones;
using Core.Erp.Bus.Migraciones;
using Core.Erp.Info.Helps;

namespace Core.Erp.Web.Areas.Migraciones.Controllers
{
    [SessionTimeout]
    public class FacturasEventosController : Controller
    {
        FacturasEventos_Bus bus_facturas = new FacturasEventos_Bus();
        FacturasEventos_Info_list Lis_tb_comprobantes_sin_autorizacion_List = new FacturasEventos_Info_list();
        #region vistas 
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
        public ActionResult GridViewPartial_facturas_eventos(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {

            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);

            var model = bus_facturas.get_list(ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            Lis_tb_comprobantes_sin_autorizacion_List.set_list(model);
            return PartialView("_GridViewPartial_facturas_eventos", model);
        }

        #endregion


        #region json
        public JsonResult guardar_aprobacion(string Ids)
        {
            string[] array = Ids.Split(',');
            var output = array.GroupBy(q => q).ToList();
            foreach (var item in output)
            {
                if (item.Key != "")
                {
                    var info = Lis_tb_comprobantes_sin_autorizacion_List.get_list().Where(v => v.cod_fact == Convert.ToDecimal(item.Key)).FirstOrDefault();
                    if (info != null)
                        bus_facturas.ModificarEstado_aprobacion(info);
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }


    public class FacturasEventos_Info_list
    {
        string Variable = "FacturasEventos_Info";
        public List<FacturasEventos_Info> get_list()
        {
            if (HttpContext.Current.Session[Variable] == null)
            {
                List<FacturasEventos_Info> list = new List<FacturasEventos_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<FacturasEventos_Info>)HttpContext.Current.Session[Variable];
        }

        public void set_list(List<FacturasEventos_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }

    }
}