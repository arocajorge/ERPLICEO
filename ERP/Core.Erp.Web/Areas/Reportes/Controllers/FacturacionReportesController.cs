
using Core.Erp.Bus.Facturacion;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Facturacion;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;
using Core.Erp.Web.Reportes.Facturacion;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Reportes.Controllers
{
    [SessionTimeout]
    public class FacturacionReportesController : Controller
    {
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        fa_factura_Bus bus_factura = new fa_factura_Bus();
        fa_catalogo_Bus bus_catalogo = new fa_catalogo_Bus();
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        in_Marca_Bus bus_marca = new in_Marca_Bus();
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Facturacion.repx";

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbCliente_Facturacion()
        {
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info();
            return PartialView("_CmbCliente_Facturacion", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
        
        public ActionResult CmbProductoHijo_Facturacion()
        {
            SessionFixed.IdProducto_padre_dist = (!string.IsNullOrEmpty(Request.Params["IdProductoPadre"])) ? Request.Params["IdProductoPadre"].ToString() : "-1";
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info();
            return PartialView("_CmbProductoHijo_Facturacion", model);
        }
        public List<in_Producto_Info> get_list_ProductoHijo_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORSUCURSAL, cl_enumeradores.eModulo.INV,0,Convert.ToInt32(SessionFixed.IdSucursal));
        }
        public in_Producto_Info get_info_producto_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }

        public ActionResult CmbClientePorTipo()
        {
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info();
            SessionFixed.Idtipo_cliente = Request.Params["Idtipo_cliente"] != null ? Request.Params["Idtipo_cliente"].ToString() : SessionFixed.Idtipo_cliente;
            return PartialView("_CmbClientePorTipo", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_cliente_x_tipo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda_cliente_x_tipo(args, Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.Idtipo_cliente));
        }
        public tb_persona_Info get_info_bajo_demanda_cliente_x_tipo(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda_cliente_x_tipo(args, Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.Idtipo_cliente));
        }
        #endregion

        #region Json

        public JsonResult cargar_lineas(int IdEmpresa = 0, string IdCategoria = "")
        {
            in_linea_Bus bus_linea = new in_linea_Bus();
            var resultado = bus_linea.get_list(IdEmpresa, IdCategoria, false);
            resultado.Add(new in_linea_Info
            {
                IdEmpresa = IdEmpresa,
                IdCategoria = IdCategoria,
                IdLinea = 0,
                nom_linea = "Todos"
            });
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult cargar_grupos(int IdEmpresa = 0, string IdCategoria = "", int IdLinea = 0)
        {
            in_grupo_Bus bus_grupo = new in_grupo_Bus();
            var resultado = bus_grupo.get_list(IdEmpresa, IdCategoria, IdLinea, false);
            resultado.Add(new in_grupo_Info
            {
                IdEmpresa = IdEmpresa,
                IdCategoria = IdCategoria,
                IdLinea = IdLinea,
                IdGrupo = 0,
                nom_grupo = "Todos"
            });
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult cargar_subgrupos(int IdEmpresa = 0, string IdCategoria = "", int IdLinea = 0, int IdGrupo = 0)
        {
            in_subgrupo_Bus bus_subgrupo = new in_subgrupo_Bus();
            var resultado = bus_subgrupo.get_list(IdEmpresa, IdCategoria, IdLinea, IdGrupo, false);
            resultado.Add(new in_subgrupo_Info
            {
                IdEmpresa = IdEmpresa,
                IdCategoria = IdCategoria,
                IdLinea = IdLinea,
                IdGrupo = IdGrupo,
                IdSubgrupo = 0,
                nom_subgrupo = "Todos"
            });
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        private void cargar_FAC010(cl_filtros_facturacion_Info model)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            lst_sucursal.Add(new tb_sucursal_Info
            {
                IdSucursal = 0,
                Su_Descripcion = "Todas"
            });
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_formapago = bus_catalogo.get_list((int)cl_enumeradores.eTipoCatalogoFact.FormaDePago, false);
            lst_formapago.Add(new fa_catalogo_Info
            {
                IdCatalogo = "",
                Nombre = "TODAS"
            });

            ViewBag.lst_formapago = lst_formapago;
            
        }

        private void cargar_FAC018(cl_filtros_facturacion_Info model)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            fa_TipoNota_Bus bus_nota = new fa_TipoNota_Bus();
            var lst_nota = bus_nota.get_list(IdEmpresa, false);
            ViewBag.lst_nota = lst_nota;


            Dictionary<string, string> lst_cre = new Dictionary<string, string>();
            lst_cre.Add("C", "Nota de crédito");
            lst_cre.Add("D", "Nota de débito");
            ViewBag.lst_cre = lst_cre;


        }

        private void cargar_combos(cl_filtros_facturacion_Info model)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            lst_sucursal.Add(new tb_sucursal_Info
            {
                IdSucursal = 0,
                Su_Descripcion = "Todas"
            });
            ViewBag.lst_sucursal = lst_sucursal;
            
            fa_Vendedor_Bus bus_vendedor = new fa_Vendedor_Bus();
            var lst_vendedor = bus_vendedor.get_list(IdEmpresa, false);
            lst_vendedor.Add(new Info.Facturacion.fa_Vendedor_Info
            {
                IdVendedor = 0,
                Ve_Vendedor = "Todos"
            });
            ViewBag.lst_vendedor = lst_vendedor;

            fa_cliente_tipo_Bus bus_cliente_tipo = new fa_cliente_tipo_Bus();
            var lst_cliente_tipo = bus_cliente_tipo.get_list(IdEmpresa, false);
            lst_cliente_tipo.Add(new Info.Facturacion.fa_cliente_tipo_Info
            {
                Idtipo_cliente = 0,
                Descripcion_tip_cliente = "Todos"
            });
            ViewBag.lst_cliente_tipo = lst_cliente_tipo;

        }
        private void cargar_sucursal_check(int IdEmpresa, int[] intArray)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            if (intArray == null || intArray.Count() == 0)
            {
                lst_sucursal.Where(q => q.IdSucursal == Convert.ToInt32(SessionFixed.IdSucursal)).FirstOrDefault().Seleccionado = true;
            }
            else
                foreach (var item in lst_sucursal)
                {
                    item.Seleccionado = (intArray.Where(q => q == item.IdSucursal).Count() > 0 ? true : false);
                }
            ViewBag.lst_sucursal = lst_sucursal;
        }

        private void cargar_marca_check(int IdEmpresa , int[] intArray)
        {
            var lst_marca = bus_marca.get_list(IdEmpresa, false);
            foreach (var item in lst_marca)
            {
                item.Seleccionado = intArray == null || intArray.Count() == 0 ? false : (intArray.Where(q => q == item.IdMarca).Count() > 0 ? true : false);
            }
            ViewBag.lst_marca = lst_marca;
        }

        public ActionResult FAC_001()
        {
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdCliente = 0,
                IdProducto = 0,
                Check1 = false
            };

            cargar_combos(model);
            FAC_001_Rpt report = new FAC_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdCliente.Value = model.IdCliente;
            report.p_IdVendedor.Value = model.IdVendedor;
            report.p_IdProducto.Value = model.IdProducto;
            report.p_mostrar_anulados.Value = model.Check1;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }

        [HttpPost]
        public ActionResult FAC_001(cl_filtros_facturacion_Info model)
        {
            FAC_001_Rpt report = new FAC_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdCliente.Value = model.IdCliente;
            report.p_IdVendedor.Value = model.IdVendedor;
            report.p_IdProducto.Value = model.IdProducto;
            report.p_mostrar_anulados.Value = model.Check1;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_combos(model);
            ViewBag.Report = report;
            return View(model);
        }

        public JsonResult cargar_cliente(decimal IdCliente = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            fa_cliente_contactos_Bus bus_contacto = new fa_cliente_contactos_Bus();
            var resultado = bus_contacto.get_list(IdEmpresa, IdCliente);
            resultado.Add(new Info.Facturacion.fa_cliente_contactos_Info
            {
                IdContacto = 0,
                Nombres = "Todos"
            });
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FAC_003(int IdSucursal = 0, int IdBodega= 0, decimal IdCbteVta= 0)
        {
            FAC_003_Rpt model = new FAC_003_Rpt();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_003");            
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion

            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_IdBodega.Value = IdBodega;
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdCbteVta.Value = IdCbteVta;
            model.p_mostrar_cuotas.Value = bus_factura.MostrarCuotasRpt(IdEmpresa,IdSucursal,IdBodega,IdCbteVta);
            model.RequestParameters = false;
            model.DefaultPrinterSettingsUsing.UsePaperKind = false;
            //bus_factura.modificarEstadoImpresion(Convert.ToInt32(SessionFixed.IdEmpresa), IdSucursal, IdBodega, IdCbteVta, true);


            return View(model);
        }

        public ActionResult FAC_004(int IdSucursal = 0, int IdBodega = 0, decimal IdNota = 0)
        {
            FAC_004_Rpt model = new FAC_004_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_004");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdBodega.Value = IdBodega;
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdNota.Value = IdNota;
            model.RequestParameters = false;
            return View(model);
        }

        public ActionResult FAC_005()
        {
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                Check1 = true,
                Check2 = false
            };

            cargar_combos(model);
            FAC_005_Rpt report = new FAC_005_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_005");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_Fecha_ini.Value = model.fecha_ini;
            report.p_Fecha_fin.Value = model.fecha_fin;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdCliente.Value = model.IdCliente == null ? 0 : Convert.ToDecimal(model.IdCliente);
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            report.RequestParameters = false;
            ViewBag.Report = report;
            return View(model);
        }

        [HttpPost]
        public ActionResult FAC_005(cl_filtros_facturacion_Info model)
        {
            FAC_005_Rpt report = new FAC_005_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_005");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_Fecha_ini.Value = model.fecha_ini;
            report.p_Fecha_fin.Value = model.fecha_fin;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdCliente.Value = model.IdCliente == null ? 0 : Convert.ToDecimal(model.IdCliente);
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_combos(model);
            report.RequestParameters = false;
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult FAC_006(int IdSucursal = 0, decimal IdProforma = 0, bool mostrar_imagen = false)
        {  
            FAC_006_Rpt model = new FAC_006_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_006");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdProforma.Value = IdProforma;
            model.RequestParameters = false;
            return View(model);
        }

        public ActionResult FAC_007(int IdSucursal = 0, int IdBodega = 0, decimal IdCbteVta = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            FAC_007_Rpt model = new FAC_007_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_007");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_IdBodega.Value = IdBodega;
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdCbteVta.Value = IdCbteVta;
            model.usuario = SessionFixed.IdUsuario;
            model.empresa = SessionFixed.NomEmpresa;
            model.RequestParameters = false;
            return View(model);
        }
        public ActionResult FAC_008(int IdSucursal = 0, int IdBodega = 0, decimal IdNota = 0)
        {
            FAC_008_Rpt model = new FAC_008_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_008");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdBodega.Value = IdBodega;
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdNota.Value = IdNota;
            model.usuario = SessionFixed.IdUsuario;
            model.empresa = SessionFixed.NomEmpresa;
            model.RequestParameters = false;
            return View(model);
        }

        public ActionResult FAC_009(int IdSucursal = 0, int IdBodega = 0, decimal IdGuiaRemision = 0)
        {
            FAC_009_Rpt model = new FAC_009_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_009");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdBodega.Value = IdBodega;
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdGuiaRemision.Value = IdGuiaRemision;
            model.RequestParameters = false;
            return View(model);
        }

        public ActionResult FAC_010()
        {

            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdCatalogo_FormaPago = ""
            };

            
            cargar_FAC010(model);
            FAC_010_Rpt report = new FAC_010_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_010");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_IdCatalogo_FormaPago.Value = model.IdCatalogo_FormaPago;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            FAC_010_detalle_forma_pago_Rpte report_detalle = new FAC_010_detalle_forma_pago_Rpte();
            #region Cargo diseño desde base
            var reporte_ = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_010");
            if (reporte_ != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte_.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report_detalle.p_IdEmpresa.Value = model.IdEmpresa;
            report_detalle.p_IdSucursal.Value = model.IdSucursal;
            report_detalle.p_fecha_ini.Value = model.fecha_ini;
            report_detalle.p_fecha_fin.Value = model.fecha_fin;
            report_detalle.p_IdCatalogo_FormaPago.Value = model.IdCatalogo_FormaPago;
            report_detalle.usuario = SessionFixed.IdUsuario;
            report_detalle.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report_detalle = report_detalle;

            return View(model);
        }
        [HttpPost]
        public ActionResult FAC_010(cl_filtros_facturacion_Info model)
        {
            FAC_010_Rpt report = new FAC_010_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_010");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_IdCatalogo_FormaPago.Value = model.IdCatalogo_FormaPago;
            cargar_FAC010(model);
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;



            FAC_010_detalle_forma_pago_Rpte report_detalle = new FAC_010_detalle_forma_pago_Rpte();
            #region Cargo diseño desde base
            var reporte_ = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_010");
            if (reporte_ != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte_.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report_detalle.p_IdEmpresa.Value = model.IdEmpresa;
            report_detalle.p_IdSucursal.Value = model.IdSucursal;
            report_detalle.p_fecha_ini.Value = model.fecha_ini;
            report_detalle.p_fecha_fin.Value = model.fecha_fin;
            report_detalle.p_IdCatalogo_FormaPago.Value = model.IdCatalogo_FormaPago;
            report_detalle.usuario = SessionFixed.IdUsuario;
            report_detalle.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report_detalle = report_detalle;
            return View(model);
        }


        public ActionResult FAC_011()
        {

            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdCliente = 0
            };
            cargar_combos(model);
            FAC_011_Rpt report = new FAC_011_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_011");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdCliente.Value = model.IdCliente;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.p_mostrarAnulados.Value = model.mostrarAnulados;
            report.p_mostrar_observacion_completa.Value = model.mostrar_observacion_completa;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult FAC_011(cl_filtros_facturacion_Info model)
        {
            FAC_011_Rpt report = new FAC_011_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_011");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdCliente.Value = model.IdCliente;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.p_mostrarAnulados.Value = model.mostrarAnulados;
            report.p_mostrar_observacion_completa.Value = model.mostrar_observacion_completa;
            cargar_combos(model);
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult FAC_012(int IdEmpresa = 0, int IdSucursal = 0, int IdBodega = 0, decimal IdCambio = 0)
        {
            FAC_012_Rpt model = new FAC_012_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_012");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdBodega.Value = IdBodega;
            model.p_IdCambio.Value =IdCambio;

            model.usuario = SessionFixed.IdUsuario;
            model.empresa = SessionFixed.NomEmpresa;
            return View(model);


        }

        public ActionResult FAC_013(int IdSucursal = 0, int IdBodega = 0, decimal IdCbteVta = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            FAC_013_Rpt model = new FAC_013_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_013");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_IdBodega.Value = IdBodega;
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdCbteVta.Value = IdCbteVta;
            model.RequestParameters = false;
            return View(model);
        }

        public ActionResult FAC_014()
        {

            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            
            FAC_014_Rpt report = new FAC_014_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_014");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult FAC_014(cl_filtros_facturacion_Info model)
        {
            FAC_014_Rpt report = new FAC_014_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_014");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult FAC_015()
        {

            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);

            FAC_015_Rpt report = new FAC_015_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_015");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult FAC_015(cl_filtros_facturacion_Info model)
        {
            FAC_015_Rpt report = new FAC_015_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_015");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            ViewBag.Report = report;
            return View(model);
        }


        public ActionResult FAC_016()
        {

            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            FAC_016_Rpt report = new FAC_016_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_016");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult FAC_016(cl_filtros_facturacion_Info model)
        {
            FAC_016_Rpt report = new FAC_016_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_016");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult FAC_017()
        {

            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            cargar_marca_check(model.IdEmpresa, model.IntArray);
            FAC_017_Rpt report = new FAC_017_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_017");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdMarca.Value = model.IdMarca;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult FAC_017(cl_filtros_facturacion_Info model)
        {
            FAC_017_Rpt report = new FAC_017_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_017");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdMarca.Value = model.IdMarca;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_marca_check(model.IdEmpresa, model.IntArray);
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult FAC_018()
        {

            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdCliente = 0,
                CreDeb = "C",
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            cargar_FAC018(model);
            FAC_018_Rpt report = new FAC_018_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_018");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdCliente.Value = model.IdCliente;
            report.p_IdTipoNota.Value = model.IdTipoNota;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_CreDeb.Value = model.CreDeb;
            report.p_mostrar_anulados.Value = model.mostrarAnulados;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult FAC_018(cl_filtros_facturacion_Info model)
        {
            FAC_018_Rpt report = new FAC_018_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_018");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_IdCliente.Value = model.IdCliente;
            report.p_IdTipoNota.Value = model.IdTipoNota;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_CreDeb.Value = model.CreDeb;
            report.p_mostrar_anulados.Value = model.mostrarAnulados;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            cargar_FAC018(model);
            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult FAC_019()
        {

            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdCliente = 0,
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            cargar_combos(model);
            FAC_019_Rpt report = new FAC_019_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_019");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdCliente.Value = model.IdCliente;
            report.p_IdVendedor.Value = model.IdVendedor;
            report.p_fechaCorte.Value = model.fecha_fin;
            report.p_mostrarSoloVencido.Value = model.mostrarSoloVencido;
            report.p_mostrarSaldo0.Value = model.mostrarSaldo0;
            report.p_IdUsuario.Value = Convert.ToString(SessionFixed.IdUsuario);
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult FAC_019(cl_filtros_facturacion_Info model)
        {
            FAC_019_Rpt report = new FAC_019_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "FAC_019");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdCliente.Value = model.IdCliente;
            report.p_IdVendedor.Value = model.IdVendedor;
            report.p_fechaCorte.Value = model.fecha_fin;
            report.p_mostrarSoloVencido.Value = model.mostrarSoloVencido;
            report.p_mostrarSaldo0.Value = model.mostrarSaldo0;
            report.p_IdUsuario.Value = Convert.ToString(SessionFixed.IdUsuario);
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            cargar_combos(model);
            ViewBag.Report = report;
            return View(model);
        }
    }
}