using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_004_Data
    {
        public List<BAN_004_Info> get_list(int IdEmpresa, int IdBanco, decimal IdConciliacion)
        {
            try
            {
                List<BAN_004_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPBAN_004(IdEmpresa, IdBanco, IdConciliacion)
                             select new BAN_004_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion = q.IdConciliacion, 
                                 IdBanco = q.IdBanco,
                                 IdPeriodo = q.IdPeriodo,
                                 nom_banco = q.nom_banco,
                                 ba_Num_Cuenta = q.ba_Num_Cuenta,
                                 IdCbteCble = q.IdCbteCble,
                                 Fecha = q.Fecha,
                                 CodTipoCbte = q.CodTipoCbte,
                                 Tipo_Cbte = q.Tipo_Cbte,
                                 IdCtaCble = q.IdCtaCble,
                                 IdTipoCbte = q.IdTipoCbte,
                                 SecuenciaCbte = q.SecuenciaCbte,
                                 Valor = q.Valor ?? 0,
                                 Observacion = q.Observacion,
                                 Cheque = q.Cheque,
                                 SaldoInicial = q.SaldoInicial ?? 0,
                                 SaldoFinal = q.SaldoFinal ?? 0,
                                 SaldoBanco_EstCta = q.SaldoBanco_EstCta,
                                 Titulo_grupo = q.Titulo_grupo,
                                 referencia = q.referencia,
                                 ruc_empresa = q.ruc_empresa,
                                 nom_empresa = q.nom_empresa,
                                 Estado_Conciliacion = q.Estado_Conciliacion,
                                 GiradoA = q.GiradoA,
                                 IdTipoFlujo = q.IdTipoFlujo,
                                 nom_tipo_flujo = q.nom_tipo_flujo,
                                 Total_Conciliado = q.Total_Conciliado ?? 0,
                                 FechaFin = q.FechaFin,
                                 FechaIni = q.FechaIni,
                                 co_SaldoBanco_anterior = q.co_SaldoBanco_anterior,
                                 TotalConciliadoNoContable = q.TotalConciliadoNoContable
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
