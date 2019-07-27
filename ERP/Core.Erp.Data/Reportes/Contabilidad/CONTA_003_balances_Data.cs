using Core.Erp.Data.General;
using Core.Erp.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Contabilidad
{
   public class CONTA_003_balances_Data
    {

        tb_sucursal_Data data_sucursal = new tb_sucursal_Data();
        string Su_Descripcion = "";
        public List<CONTA_003_balances_Info> get_list(int IdEmpresa, int IdAnio, DateTime fechaIni, DateTime fechaFin, string IdUsuario, int IdNivel, bool mostrarSaldo0, string balance, bool MostrarSaldoAcumulado)
        {
            try
            {
                List<CONTA_003_balances_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPCONTA_003_balances(IdEmpresa, IdAnio, fechaIni, fechaFin, IdUsuario, IdNivel, mostrarSaldo0, balance,MostrarSaldoAcumulado)
                             select new CONTA_003_balances_Info
                             {
                                 IdUsuario = q.IdUsuario,
                                 IdEmpresa = q.IdEmpresa,
                                 IdCtaCble = q.IdCtaCble,
                                 pc_Cuenta = q.pc_Cuenta,
                                 IdCtaCblePadre = q.IdCtaCblePadre,
                                 EsCtaUtilidad = q.EsCtaUtilidad,
                                 IdNivelCta = q.IdNivelCta,
                                 IdGrupoCble = q.IdGrupoCble,
                                 gc_estado_financiero = q.gc_estado_financiero,
                                 gc_GrupoCble = q.gc_GrupoCble,
                                 gc_Orden = q.gc_Orden,
                                 Debitos = q.Debitos,
                                 DebitosSaldoInicial = q.DebitosSaldoInicial,
                                 SaldoDebitos = q.SaldoDebitos,
                                 SaldoDebitosCreditos = q.SaldoDebitosCreditos,
                                 Creditos = q.Creditos,
                                 CreditosSaldoInicial = q.CreditosSaldoInicial,
                                 SaldoCreditos = q.SaldoCreditos,
                                 SaldoFinal = q.SaldoFinal,
                                 SaldoInicial = q.SaldoInicial,
                                 EsCuentaMovimiento = q.EsCuentaMovimiento,
                                 Naturaleza = q.Naturaleza,
                                 SaldoCreditosNaturaleza = q.SaldoCreditosNaturaleza,
                                 SaldoDebitosCreditosNaturaleza = q.SaldoDebitosCreditosNaturaleza,
                                 SaldoDebitosNaturaleza = q.SaldoDebitosNaturaleza,
                                 SaldoFinalNaturaleza = q.SaldoFinalNaturaleza,
                                 SaldoInicialNaturaleza = q.SaldoInicialNaturaleza,
                                 Su_Descripcion = Su_Descripcion
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
