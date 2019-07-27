using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;
using Core.Erp.Web.Reportes.Importacion;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Reportes.Controllers
{
    [SessionTimeout]
    public class ImportacionReportesController : Controller
    {
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        #region ProductoPadre
        public ActionResult CmbProductoPadre_Importacion()
        {
            cl_filtros_importacion_Info model = new cl_filtros_importacion_Info();
            return PartialView("_CmbProductoPadre_Importacion", model);
        }

        public List<in_Producto_Info> get_list_ProductoPadre_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.SOLOPADRES, cl_enumeradores.eModulo.INV, 0,0);
        }
        public in_Producto_Info get_info_producto_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProveedor_Importacion()
        {
            cl_filtros_importacion_Info model = new cl_filtros_importacion_Info();
            return PartialView("_CmbProveedor_Importacion", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        #endregion
        private void cargar_combos(int IdEmpresa)
        {
            tb_pais_Bus bus_pais = new tb_pais_Bus();
            var lst_pais = bus_pais.get_list(false);
            lst_pais.Add(new Info.General.tb_pais_Info
            {
                IdPais = "",
                Nombre = "Todos"
            });
            ViewBag.lst_pais = lst_pais;

            in_Marca_Bus bus_marca = new in_Marca_Bus();
            var lst_marca = bus_marca.get_list(IdEmpresa, false);
            lst_marca.Add(new Info.Inventario.in_Marca_Info
            {
                IdMarca = 0,
                Descripcion = "Todas"
            });
            ViewBag.lst_marca = lst_marca;
        }
        public ActionResult IMP_001(int IdOrdenCompra_ext = 0)
        {
            IMP_001_Rpt model = new IMP_001_Rpt();
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdOrdenCompra_ext.Value = IdOrdenCompra_ext;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult IMP_002(int IdOrdenCompra_ext = 0)
        {
            IMP_002_Rpt model = new IMP_002_Rpt();
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdOrdenCompra_ext.Value = IdOrdenCompra_ext;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult IMP_003()
        {
            cl_filtros_importacion_Info model = new cl_filtros_importacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdPais_embarque = "",
                IdMarca = 0,
                IdProveedor = 0,
                IdProductoPadre = 0
            };
            cargar_combos(model.IdEmpresa);
            IMP_003_Rpt report = new IMP_003_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdProveedor.Value = model.IdProveedor;
            report.p_IdProducto.Value = model.IdProductoPadre;
            report.p_IdMarca.Value = model.IdMarca;
            report.p_IdPais_embarque.Value = model.IdPais_embarque;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult IMP_003(cl_filtros_importacion_Info model)
        {
            IMP_003_Rpt report = new IMP_003_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdProveedor.Value = model.IdProveedor;
            report.p_IdProducto.Value = model.IdProductoPadre;
            report.p_IdMarca.Value = model.IdMarca;
            report.p_IdPais_embarque.Value = model.IdPais_embarque;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            cargar_combos(model.IdEmpresa);
            ViewBag.Report = report;
            return View(model);
        }
    }
}