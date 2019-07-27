using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using Core.Erp.Web.Reportes.Contabilidad;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Reportes.Controllers
{
    [SessionTimeout]
    public class ContabilidadReportesController : Controller
    {
        #region Combos

        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_contable_Conta()
        {
            cl_filtros_Info model = new cl_filtros_Info();

            return PartialView("_CmbCuenta_contable_Conta", model);
        }

        public ActionResult CmbCuenta_contable_ContaFin()
        {
            cl_filtros_Info model = new cl_filtros_Info();

            return PartialView("_CmbCuenta_contable_ContaFin", model);
        }

        public List<ct_plancta_Info> get_list_bajo_demanda_cta(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), true);
        }
        public ct_plancta_Info get_info_bajo_demanda_cta(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }


        #endregion
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Facturacion.repx";
        public ActionResult CONTA_001(int IdTipoCbte = 0, decimal IdCbteCble = 0)
        {
            CONTA_001_Rpt model = new CONTA_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CONTA_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdTipoCbte.Value = IdTipoCbte;
            model.p_IdCbteCble.Value = IdCbteCble;
            model.usuario = SessionFixed.IdUsuario;
            model.empresa = SessionFixed.NomEmpresa;
            model.RequestParameters = false;
            return View(model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            ct_cbtecble_tipo_Bus bus_tipo = new ct_cbtecble_tipo_Bus();
            var lst_tipo = bus_tipo.get_list(IdEmpresa, false);
            ViewBag.lst_tipo = lst_tipo;

            ct_punto_cargo_grupo_Bus bus_punto = new ct_punto_cargo_grupo_Bus();
            var lst_punto = bus_punto.GetList(IdEmpresa, false);
            ViewBag.lst_punto = lst_punto;
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
        private void cargar_nivel()
        {
            Dictionary<int, string> lst_nivel = new Dictionary<int, string>();
            lst_nivel.Add(6, "Nivel 6");
            lst_nivel.Add(5, "Nivel 5");
            lst_nivel.Add(4, "Nivel 4");
            lst_nivel.Add(3, "Nivel 3");
            lst_nivel.Add(2, "Nivel 2");
            lst_nivel.Add(1, "Nivel 1");
            ViewBag.lst_nivel = lst_nivel;

            Dictionary<string, string> lst_balance = new Dictionary<string, string>();
            lst_balance.Add("BG", "Balance general");
            lst_balance.Add("ER", "Estado de resultado");
            lst_balance.Add("", "Balance de comprobación");
            ViewBag.lst_balance = lst_balance;
        }


        private void cargar_nivel_CONTA006()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            Dictionary<int, string> lst_nivel = new Dictionary<int, string>();
            lst_nivel.Add(6, "Nivel 6");
            lst_nivel.Add(5, "Nivel 5");
            lst_nivel.Add(4, "Nivel 4");
            lst_nivel.Add(3, "Nivel 3");
            lst_nivel.Add(2, "Nivel 2");
            lst_nivel.Add(1, "Nivel 1");
            ViewBag.lst_nivel = lst_nivel;

            Dictionary<string, string> lst_balance = new Dictionary<string, string>();
            lst_balance.Add("BG", "Balance general");
            lst_balance.Add("ER", "Estado de resultado");
            ViewBag.lst_balance = lst_balance;

            ct_anio_fiscal_Bus bus_anio = new ct_anio_fiscal_Bus();
            var lst_anio = bus_anio.get_list(false);
            ViewBag.lst_anio = lst_anio;

            ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
            var lst_periodo = bus_periodo.get_list(IdEmpresa, false);
            ViewBag.lst_periodo = lst_periodo;
        }

        public ActionResult CONTA_002()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdCtaCble = "",
                IdCtaCbleFin = "",
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdTipoCbte = 1,
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            cargar_combos(model.IdEmpresa);
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            CONTA_002_Rpt report = new CONTA_002_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CONTA_002");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdCtaCble.Value = model.IdCtaCble;
            report.p_IdCtaCbleFin.Value = model.IdCtaCbleFin;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult CONTA_002(cl_filtros_Info model)
        {
            CONTA_002_Rpt report = new CONTA_002_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CONTA_002");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdCtaCble.Value = model.IdCtaCble;
            report.p_IdCtaCbleFin.Value = model.IdCtaCbleFin;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            cargar_combos(model.IdEmpresa);
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult CONTA_003()
        {
            cl_filtros_contabilidad_Info model = new cl_filtros_contabilidad_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdNivel = 6,
                balance = "ER",
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };

            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            model.IdAnio = model.fecha_fin.Year;
            model.MostrarSaldoAcumulado = false;
            CONTA_003_ER_Rpt report = new CONTA_003_ER_Rpt();
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.p_IdUsuario.Value = SessionFixed.IdUsuario;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
            report.p_balance.Value = model.balance;
            report.p_IdSucursal.Value = model.IdSucursal;
            report.p_MostrarSaldoAcumulado.Value = model.MostrarSaldoAcumulado;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            report.RequestParameters = false;
            ViewBag.Report = report;

            cargar_nivel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CONTA_003(cl_filtros_contabilidad_Info model)
        {
            model.IdAnio = model.fecha_fin.Year;
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);

            if (model.balance == "BG")
            {
                CONTA_003_BG_Rpt report = new CONTA_003_BG_Rpt();
                report.IntArray = model.IntArray;
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdAnio.Value = model.IdAnio;
                report.p_fechaIni.Value = model.fecha_ini;
                report.p_fechaFin.Value = model.fecha_fin;
                report.p_IdUsuario.Value = SessionFixed.IdUsuario;
                report.p_IdNivel.Value = model.IdNivel;
                report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
                report.p_balance.Value = model.balance;
                report.p_IdSucursal.Value = model.IdSucursal;
                report.p_MostrarSaldoAcumulado.Value = model.MostrarSaldoAcumulado;
                report.usuario = SessionFixed.IdUsuario;
                report.empresa = SessionFixed.NomEmpresa;
                report.RequestParameters = false;
                ViewBag.Report = report;
            }
            if (model.balance == "ER")
            {
                CONTA_003_ER_Rpt report = new CONTA_003_ER_Rpt();
                report.IntArray = model.IntArray;
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdAnio.Value = model.IdAnio;
                report.p_fechaIni.Value = model.fecha_ini;
                report.p_fechaFin.Value = model.fecha_fin;
                report.p_IdUsuario.Value = SessionFixed.IdUsuario;
                report.p_IdNivel.Value = model.IdNivel;
                report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
                report.p_balance.Value = model.balance;
                report.p_IdSucursal.Value = model.IdSucursal;
                report.p_MostrarSaldoAcumulado.Value = model.MostrarSaldoAcumulado;
                report.usuario = SessionFixed.IdUsuario;
                report.empresa = SessionFixed.NomEmpresa;
                report.RequestParameters = false;
                ViewBag.Report = report;
            }

            if (string.IsNullOrEmpty(model.balance))
            {
                CONTA_003_BC_Rpt report = new CONTA_003_BC_Rpt();
                report.IntArray = model.IntArray;
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdAnio.Value = model.IdAnio;
                report.p_fechaIni.Value = model.fecha_ini;
                report.p_fechaFin.Value = model.fecha_fin;
                report.p_IdUsuario.Value = SessionFixed.IdUsuario;
                report.p_IdNivel.Value = model.IdNivel;
                report.p_IdSucursal.Value = model.IdSucursal;
                report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
                report.p_balance.Value = model.balance;
                report.p_MostrarSaldoAcumulado.Value = model.MostrarSaldoAcumulado;
                report.usuario = SessionFixed.IdUsuario;
                report.empresa = SessionFixed.NomEmpresa;
                report.RequestParameters = false;
                ViewBag.Report = report;
            }
            cargar_nivel();
            return View(model);
        }


        public ActionResult CONTA_004()
        {
            cl_filtros_contabilidad_Info model = new cl_filtros_contabilidad_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdNivel = 6,
                balance = "ER",
                IdPeriodoFin = Convert.ToInt32(DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString("00")),
                IdPeriodoIni = Convert.ToInt32(DateTime.Now.AddMonths(-1).Year.ToString() + DateTime.Now.AddMonths(-1).Month.ToString("00")),
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            model.IdAnio = Convert.ToInt32(model.IdPeriodoFin.ToString().Substring(0,4));
            model.MostrarSaldoAcumulado = false;
            CONTA_004_ER_Rpt report = new CONTA_004_ER_Rpt();
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdPeriodoIni.Value = model.IdPeriodoIni;
            report.p_IdPeriodoFin.Value = model.IdPeriodoFin;
            report.p_IdUsuario.Value = model.IdUsuario;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
            report.p_balance.Value = model.balance;
            report.p_mostrarAcumulado.Value = model.MostrarSaldoAcumulado;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            cargar_nivel_CONTA006();
            return View(model);
        }
        [HttpPost]
        public ActionResult CONTA_004(cl_filtros_contabilidad_Info model)
        {
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            model.IdAnio = Convert.ToInt32(model.IdPeriodoFin.ToString().Substring(0, 4));
            if (model.balance == "BG")
            {
                CONTA_004_BG_Rpt report = new CONTA_004_BG_Rpt();
                report.IntArray = model.IntArray;
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdAnio.Value = model.IdAnio;
                report.p_IdPeriodoIni.Value = model.IdPeriodoIni;
                report.p_IdPeriodoFin.Value = model.IdPeriodoFin;
                report.p_IdUsuario.Value = model.IdUsuario;
                report.p_IdNivel.Value = model.IdNivel;
                report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
                report.p_balance.Value = model.balance;
                report.p_mostrarAcumulado.Value = model.MostrarSaldoAcumulado;
                report.usuario = SessionFixed.IdUsuario;
                report.empresa = SessionFixed.NomEmpresa;
                ViewBag.Report = report;
            }
            if (model.balance == "ER")
            {
                CONTA_004_ER_Rpt report = new CONTA_004_ER_Rpt();
                report.IntArray = model.IntArray;
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdAnio.Value = model.IdAnio;
                report.p_IdPeriodoIni.Value = model.IdPeriodoIni;
                report.p_IdPeriodoFin.Value = model.IdPeriodoFin;
                report.p_IdUsuario.Value = model.IdUsuario;
                report.p_IdNivel.Value = model.IdNivel;
                report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
                report.p_balance.Value = model.balance;
                report.p_mostrarAcumulado.Value = model.MostrarSaldoAcumulado;
                report.usuario = SessionFixed.IdUsuario;
                report.empresa = SessionFixed.NomEmpresa;
                ViewBag.Report = report;
            }
                cargar_nivel_CONTA006();
            return View(model);
        }

        public ActionResult CONTA_005()
        {
            cl_filtros_contabilidad_Info model = new cl_filtros_contabilidad_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            cargar_combos(model.IdEmpresa);
            CONTA_005_Rpt report = new CONTA_005_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CONTA_005");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdPunto_cargo_grupo.Value = model.IdPunto_cargo_grupo;
            report.p_IdUsuario.Value = SessionFixed.IdUsuario;
            report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult CONTA_005(cl_filtros_contabilidad_Info model)
        {
            CONTA_005_Rpt report = new CONTA_005_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CONTA_005");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdPunto_cargo_grupo.Value = model.IdPunto_cargo_grupo;
            report.p_IdUsuario.Value = SessionFixed.IdUsuario;
            report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            cargar_combos(model.IdEmpresa);
            ViewBag.Report = report;
            return View(model);
        }
        public ActionResult CONTA_006()
        {
            cl_filtros_contabilidad_Info model = new cl_filtros_contabilidad_Info
            {
                IdEmpresa =  Convert.ToInt32(SessionFixed.IdEmpresa),
                IdNivel = 6,
                balance = "ER",
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            model.IdAnio = model.fecha_fin.Year;
            model.MostrarSaldoAcumulado = false;
            CONTA_006_ER_Rpt report = new CONTA_006_ER_Rpt();
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdUsuario.Value = model.IdUsuario;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
            report.p_balance.Value = model.balance;
            report.p_mostrarAcumulado.Value = model.MostrarSaldoAcumulado;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            cargar_nivel_CONTA006();
            return View(model);
        }
        [HttpPost]
        public ActionResult CONTA_006(cl_filtros_contabilidad_Info model)
        {
            model.IdAnio = model.fecha_fin.Year;
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);

            if (model.balance == "BG")
            {
                CONTA_006_BG_Rpt report = new CONTA_006_BG_Rpt();
                report.IntArray = model.IntArray;
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdAnio.Value = model.IdAnio;
                report.p_IdUsuario.Value = model.IdUsuario;
                report.p_IdNivel.Value = model.IdNivel;
                report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
                report.p_balance.Value = model.balance;
                report.p_mostrarAcumulado.Value = model.MostrarSaldoAcumulado;
                report.usuario = SessionFixed.IdUsuario;
                report.empresa = SessionFixed.NomEmpresa;
                ViewBag.Report = report;
            }
            if (model.balance == "ER")
            {
                CONTA_006_ER_Rpt report = new CONTA_006_ER_Rpt();
                report.IntArray = model.IntArray;
                report.p_IdEmpresa.Value = model.IdEmpresa;
                report.p_IdAnio.Value = model.IdAnio;
                report.p_IdUsuario.Value = model.IdUsuario;
                report.p_IdNivel.Value = model.IdNivel;
                report.p_mostrarSaldo0.Value = model.mostrar_saldos_en_0;
                report.p_balance.Value = model.balance;
                report.p_mostrarAcumulado.Value = model.MostrarSaldoAcumulado;
                report.usuario = SessionFixed.IdUsuario;
                report.empresa = SessionFixed.NomEmpresa;
                ViewBag.Report = report;
            }
            cargar_nivel_CONTA006();
            return View(model);
        }
    }
}