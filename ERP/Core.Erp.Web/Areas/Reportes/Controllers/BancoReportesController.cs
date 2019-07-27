using Core.Erp.Bus.Banco;
using Core.Erp.Bus.General;
using Core.Erp.Bus.SeguridadAcceso;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using Core.Erp.Web.Reportes.Banco;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Reportes.Controllers
{
    [SessionTimeout]
    public class BancoReportesController : Controller
    {
        #region Variables
        ba_Cbte_Ban_Bus bus_cbte = new ba_Cbte_Ban_Bus();
        ba_Banco_Cuenta_Bus bus_banco = new ba_Banco_Cuenta_Bus();
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Cheque.repx";
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda

        public ActionResult CmbPersona_Banco()
        {
            cl_filtros_banco_Info model = new cl_filtros_banco_Info();
            return PartialView("_CmbPersona_Banco", model);
        }
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PERSONA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PERSONA.ToString());
        }


        #endregion
        private void cargar_sucursal_check(int IdEmpresa, int[] intArray)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();

            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            foreach (var item in lst_sucursal)
            {
                item.Seleccionado = intArray == null || intArray.Count() == 0 ? false : (intArray.Where(q => q == item.IdSucursal).Count() > 0 ? true : false);
            }
            ViewBag.lst_sucursal = lst_sucursal;
        }

        public ActionResult BAN_001( int IdTipoCbte = 0, decimal IdCbteCble = 0)
        {
            BAN_001_Rpt model = new BAN_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdTipoCbte.Value = IdTipoCbte;
            model.p_IdCbteCble.Value = IdCbteCble;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult BAN_002( int IdTipocbte = 0, decimal IdCbteCble = 0)
        {
            BAN_002_Rpt model = new BAN_002_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_002");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdTipocbte.Value = IdTipocbte;
            model.p_IdCbteCble.Value = IdCbteCble;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult BAN_003( int IdTipocbte = 0, decimal IdCbteCble = 0)
        {
            BAN_003_Rpt model = new BAN_003_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_003");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdTipocbte.Value = IdTipocbte;
            model.p_IdCbteCble.Value = IdCbteCble;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult BAN_004( int IdBanco = 0, decimal IdConciliacion = 0)
        {
            BAN_004_Rpt model = new BAN_004_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_004");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdBanco.Value = IdBanco;
            model.p_IdConciliacion.Value = IdConciliacion;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult BAN_005(int IdEmpresa = 0, int IdTipocbte = 0, decimal IdCbteCble = 0, int IdBanco = 0)
       {
            BAN_005_Rpt model = new BAN_005_Rpt();
            #region Cargo diseño desde base
            var banco = bus_banco.get_info(IdEmpresa, IdBanco);
            if (banco.ReporteCheque != null && banco.ReporteCheque.Length > 0)
            {
                System.IO.File.WriteAllBytes(RootReporte, banco.ReporteCheque);
                model.LoadLayout(RootReporte);
            }
            #endregion

            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_IdTipocbte.Value = IdTipocbte;
            model.p_IdCbteCble.Value = IdCbteCble;

            bus_cbte.modificarDB_EstadoCheque(IdEmpresa, IdTipocbte, IdCbteCble, "ESTCBENT");
            return View(model);
        }
        public ActionResult BAN_005_Masivo()
        {
            cl_filtros_banco_Info model = new cl_filtros_banco_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdBanco = 0,
                NumDesde = 0 , 
                NumHasta = 0
            };
            BAN_006_Rpt report = new BAN_006_Rpt();
            #region Cargo diseño desde base
            if (model.IdBanco != 0)
            {
                var banco = bus_banco.get_info(model.IdEmpresa, model.IdBanco);
                System.IO.File.WriteAllBytes(RootReporte, banco.ReporteChequeComprobante);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            report.p_IdTipoCbte.Value = 0;
            report.p_IdCbteCble.Value = 0;
            report.p_NumDesde.Value = model.NumDesde;
            report.p_NumHasta.Value = model.NumHasta;
            report.p_IdBanco.Value = model.IdBanco;
            cargar_banco(Convert.ToInt32(SessionFixed.IdEmpresa));
            bus_cbte.modificarDB_EstadoCheque(Convert.ToInt32(SessionFixed.IdEmpresa), 0, 0, "ESTCBENT");
           
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult BAN_005_Masivo(cl_filtros_banco_Info model)
        {
            BAN_006_Rpt report = new BAN_006_Rpt();
            #region Cargo diseño desde base
            if (model.IdBanco != 0)
            {
                var banco = bus_banco.get_info(model.IdEmpresa, model.IdBanco);
                System.IO.File.WriteAllBytes(RootReporte, banco.ReporteChequeComprobante);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdTipoCbte.Value = 0;
            report.p_IdCbteCble.Value = 0;
            report.p_NumDesde.Value = model.NumDesde;
            report.p_NumHasta.Value = model.NumHasta;
            report.p_IdBanco.Value = model.IdBanco;
            bus_cbte.modificarDB_EstadoCheque(Convert.ToInt32(SessionFixed.IdEmpresa), 0, 0, "ESTCBENT");
           
            ViewBag.Report = report;
            cargar_banco(Convert.ToInt32(SessionFixed.IdEmpresa));
            return View(model);
        }
        public ActionResult BAN_006(int IdEmpresa = 0, int IdTipoCbte = 0, decimal IdCbteCble = 0, int IdBanco = 0)
        {                        
            BAN_006_Rpt model = new BAN_006_Rpt();
            #region Cargo diseño desde base
            var banco = bus_banco.get_info(IdEmpresa, IdBanco);
            if (banco.ReporteChequeComprobante != null && banco.ReporteChequeComprobante.Length > 0)
            {
                System.IO.File.WriteAllBytes(RootReporte, banco.ReporteChequeComprobante);
                model.LoadLayout(RootReporte);
            }
            #endregion

            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_IdTipoCbte.Value = IdTipoCbte;
            model.p_IdCbteCble.Value = IdCbteCble;            

            bus_cbte.modificarDB_EstadoCheque(IdEmpresa, IdTipoCbte, IdCbteCble, "ESTCBENT");
            return View(model);
        }
        private void cargar_banco(int IdEmpresa)
        {
            var lst_banco = bus_banco.get_list(IdEmpresa,0, false);
            lst_banco.Add(new Info.Banco.ba_Banco_Cuenta_Info
            {
                IdBanco = 0,
                ba_descripcion = "Todos"

            });
            ViewBag.lst_banco = lst_banco;

            tb_persona_Bus bus_persona = new tb_persona_Bus();
            var lst_persona = bus_persona.get_list(false);
            ViewBag.lst_persona = lst_persona;

            ba_Catalogo_Bus bus_catalogo = new ba_Catalogo_Bus();
            var lst_catalogo = bus_catalogo.get_list(Convert.ToString(cl_enumeradores.eTipoCatalogoBanco.EST_CB_BA.ToString()), false);
            lst_catalogo.Add(new Info.Banco.ba_Catalogo_Info
            {
                IdCatalogo = "",
                ca_descripcion = "Todos"

            });
            ViewBag.lst_catalogo = lst_catalogo;

            tb_sucursal_Bus bus_suc = new tb_sucursal_Bus();
            var lst_suc = bus_suc.get_list(IdEmpresa, false);
            ViewBag.lst_suc = lst_suc;
        }
        public ActionResult BAN_007()
        {
            cl_filtros_banco_Info model = new cl_filtros_banco_Info
            {
                Estado = "",
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdPersona = 0,
                IdBanco = 0
            };
            cargar_banco(model.IdEmpresa);
            BAN_007_Rpt report = new BAN_007_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdBanco.Value = model.IdBanco;
            report.p_IdPersona.Value = model.IdPersona;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_Estado.Value = model.Estado;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult BAN_007(cl_filtros_banco_Info model)
        {
            BAN_007_Rpt report = new BAN_007_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdBanco.Value = model.IdBanco;
            report.p_IdPersona.Value = model.IdPersona;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_Estado.Value = model.Estado;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            cargar_banco(model.IdEmpresa);
            ViewBag.Report = report;

            return View(model);
        }
        public ActionResult BAN_008()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            cl_filtros_banco_Info model = new cl_filtros_banco_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_banco(model.IdEmpresa);
            BAN_008_Rpt report = new BAN_008_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_008");
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
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult BAN_008(cl_filtros_banco_Info model)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            BAN_008_Rpt report = new BAN_008_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_008");
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
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            cargar_banco(model.IdEmpresa);
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult BAN_009(bool mostrar_agrupado = false)
        {
            cl_filtros_banco_Info model = new cl_filtros_banco_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_banco(model.IdEmpresa);
            if(!mostrar_agrupado)
            {
                BAN_009_Rpt report = new BAN_009_Rpt();
                #region Cargo diseño desde base
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_009");
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
                report.p_mostrar_agrupado.Value = model.mostrar_agrupado;
                report.usuario = SessionFixed.IdUsuario.ToString();
                report.empresa = SessionFixed.NomEmpresa.ToString();
                ViewBag.Report = report;
            }
            else
            {
                BAN_009_Flujo_Rpt report_flujo = new BAN_009_Flujo_Rpt();
                #region Cargo diseño desde base
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_009");
                if (reporte != null)
                {
                    System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                    report_flujo.LoadLayout(RootReporte);
                }
                #endregion
                report_flujo.p_IdEmpresa.Value = model.IdEmpresa;
                report_flujo.p_IdSucursal.Value = model.IdSucursal;
                report_flujo.p_fecha_ini.Value = model.fecha_ini;
                report_flujo.p_fecha_fin.Value = model.fecha_fin;
                report_flujo.p_mostrar_agrupado.Value = model.mostrar_agrupado;
                report_flujo.usuario = SessionFixed.IdUsuario.ToString();
                report_flujo.empresa = SessionFixed.NomEmpresa.ToString();
                ViewBag.Report = report_flujo;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult BAN_009(cl_filtros_banco_Info model)
        {
            if(!model.mostrar_agrupado)
            {
                BAN_009_Rpt report = new BAN_009_Rpt();
                #region Cargo diseño desde base
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_009");
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
                report.p_mostrar_agrupado.Value = model.mostrar_agrupado;
                report.usuario = SessionFixed.IdUsuario.ToString();
                report.empresa = SessionFixed.NomEmpresa.ToString();
                cargar_banco(model.IdEmpresa);
                ViewBag.Report = report;
            }
            else
            {
                BAN_009_Flujo_Rpt report_flujo = new BAN_009_Flujo_Rpt();
                #region Cargo diseño desde base
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_009");
                if (reporte != null)
                {
                    System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                    report_flujo.LoadLayout(RootReporte);
                }
                #endregion
                report_flujo.p_IdEmpresa.Value = model.IdEmpresa;
                report_flujo.p_IdSucursal.Value = model.IdSucursal;
                report_flujo.p_fecha_fin.Value = model.fecha_fin;
                report_flujo.p_mostrar_agrupado.Value = model.mostrar_agrupado;
                report_flujo.usuario = SessionFixed.IdUsuario.ToString();
                report_flujo.empresa = SessionFixed.NomEmpresa.ToString();
                cargar_banco(model.IdEmpresa);
                ViewBag.Report = report_flujo;
            }
            return View(model);
        }
        public ActionResult BAN_010(int IdEmpresa = 0, decimal IdArchivo = 0)
        {
            BAN_010_Rpt model = new BAN_010_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_010");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_IdArchivo.Value = IdArchivo;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }


        public ActionResult BAN_011(bool mostrar_agrupado = false)
        {
            cl_filtros_banco_Info model = new cl_filtros_banco_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_banco(model.IdEmpresa);
            BAN_011_Rpt report = new BAN_011_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_011");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdUsuario.Value = model.IdUsuario;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult BAN_011(cl_filtros_banco_Info model)
        {
            BAN_011_Rpt report = new BAN_011_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_011");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
        #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdUsuario.Value = model.IdUsuario;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa.ToString();
            cargar_banco(model.IdEmpresa);
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult BAN_012(bool mostrar_agrupado = false)
        {
            cl_filtros_banco_Info model = new cl_filtros_banco_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdUsuario = Convert.ToString(SessionFixed.IdUsuario),
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            cargar_banco(model.IdEmpresa);
            BAN_012_Rpt report = new BAN_012_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_012");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdUsuario.Value = model.IdUsuario;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_mostrarSaldo0.Value = model.mostrarSaldo0;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult BAN_012(cl_filtros_banco_Info model)
        {
            cargar_banco(model.IdEmpresa);
            BAN_012_Rpt report = new BAN_012_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_012");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdUsuario.Value = model.IdUsuario;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_mostrarSaldo0.Value = model.mostrarSaldo0;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
    }
}