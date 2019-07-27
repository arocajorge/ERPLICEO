using Core.Erp.Data.General;
using Core.Erp.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Contabilidad
{
   public class CONTA_004_Data
    {

        //tb_sucursal_Data data_sucursal = new tb_sucursal_Data();
        //public List<CONTA_004_Info> get_list(int IdEmpresa, int IdAnio, DateTime fechaIni1, DateTime fechaFin1, string IdUsuario, int IdNivel, bool mostrarSaldo0, string balance, bool MostrarSaldoAcumulado, DateTime fechaIni2, DateTime fechaFin2)
        //{
        //    try
        //    {
        //        List<CONTA_004_Info> Lista;
        //        List<CONTA_003_balances_Info> Lista1;

        //        Entities_contabilidad db = new Entities_contabilidad();
        //        Lista = db.ct_plancta.Where(q => q.IdEmpresa == IdEmpresa && q.pc_Estado == "A").Select(q => new CONTA_004_Info
        //        {
        //            IdEmpresa = q.IdEmpresa,
        //            IdCtaCble = q.IdCtaCble,
        //            IdCtaCblePadre = q.IdCtaCblePadre,
        //            pc_Cuenta = q.pc_Cuenta
        //        }).ToList();

        //        using (Entities_reportes Context = new Entities_reportes())
        //        {
        //            Lista1 = (from q in Context.SPCONTA_003_balances(IdEmpresa, IdAnio, fechaIni1, fechaFin1, IdUsuario, IdNivel, mostrarSaldo0, balance,MostrarSaldoAcumulado)
        //                     select new CONTA_003_balances_Info
        //                     {
        //                         IdUsuario = q.IdUsuario,
        //                         IdEmpresa = q.IdEmpresa,
        //                         IdCtaCble = q.IdCtaCble,
        //                         pc_Cuenta = q.pc_Cuenta,
        //                         IdCtaCblePadre = q.IdCtaCblePadre,
        //                         EsCtaUtilidad = q.EsCtaUtilidad,
        //                         IdNivelCta = q.IdNivelCta,
        //                         IdGrupoCble = q.IdGrupoCble,
        //                         gc_estado_financiero = q.gc_estado_financiero,
        //                         gc_GrupoCble = q.gc_GrupoCble,
        //                         gc_Orden = q.gc_Orden,
        //                         Debitos = q.Debitos,
        //                         DebitosSaldoInicial = q.DebitosSaldoInicial,
        //                         SaldoDebitos = q.SaldoDebitos,
        //                         SaldoDebitosCreditos = q.SaldoDebitosCreditos,
        //                         Creditos = q.Creditos,
        //                         CreditosSaldoInicial = q.CreditosSaldoInicial,
        //                         SaldoCreditos = q.SaldoCreditos,
        //                         SaldoFinal = q.SaldoFinal,
        //                         SaldoInicial = q.SaldoInicial,
        //                         EsCuentaMovimiento = q.EsCuentaMovimiento,
        //                         Naturaleza = q.Naturaleza,
        //                         SaldoCreditosNaturaleza = q.SaldoCreditosNaturaleza,
        //                         SaldoDebitosCreditosNaturaleza = q.SaldoDebitosCreditosNaturaleza,
        //                         SaldoDebitosNaturaleza = q.SaldoDebitosNaturaleza,
        //                         SaldoFinalNaturaleza = q.SaldoFinalNaturaleza,
        //                         SaldoInicialNaturaleza = q.SaldoInicialNaturaleza                                 
        //                     }).ToList();
        //        }
        //        return Lista;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public List<CONTA_004_Info> GetList(int IdEmpresa, int IdAnio, int IdPeriodoIni, int IdPeriodoFin, bool mostrarSaldo0, string IdUsuario, int IdNivel, bool mostrarAcumulado, string balance)
        {
            try
            {
                //int IdPeriodoIni = IdPeriodo;
                //int IdPeriodoFin = IdPeriodo == 0 ? 9999 : IdPeriodo;
                List<CONTA_004_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPCONTA_004(IdEmpresa, IdAnio, IdPeriodoIni, IdPeriodoFin, mostrarSaldo0, IdUsuario, 
                        IdNivel, mostrarAcumulado, balance).Select(q => new CONTA_004_Info
                    {
                        IdCtaCble = q.IdCtaCble,
                        EsCtaUtilidad = q.EsCtaUtilidad,
                        EsCuentaMovimiento = q.EsCuentaMovimiento,
                        gc_estado_financiero = q.gc_estado_financiero,
                        gc_GrupoCble = q.gc_GrupoCble,
                        gc_Orden = q.gc_Orden,
                        IdCtaCblePadre = q.IdCtaCblePadre,
                        IdEmpresa = q.IdEmpresa,
                        IdGrupoCble = q.IdGrupoCble,
                        IdNivelCta = q.IdNivelCta,
                        IdUsuario = q.IdUsuario,
                        Naturaleza = q.Naturaleza,
                        pc_Cuenta = q.pc_Cuenta,
                        Signo = q.Signo,
                        Su_Descripcion = q.Signo,
                        Valor1 = q.Valor1,
                        Valor2 = q.Valor2,
                        Variacion = q.Variacion
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
