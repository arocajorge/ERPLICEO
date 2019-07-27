using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Data
    {
        public List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> get_list(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> Lista;

                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = (from q in Context.vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito
                             where q.mba_IdEmpresa == IdEmpresa
                             && q.mba_IdTipocbte == IdTipoCbte
                             && q.mba_IdCbteCble == IdCbteCble
                             select new ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info
                             {
                                 mcj_IdEmpresa = q.IdEmpresa,
                                 mcj_IdCbteCble = q.IdCbteCble,
                                 mcj_IdTipocbte = q.IdTipocbte,
                                 mba_IdEmpresa = q.mba_IdEmpresa,
                                 mba_IdCbteCble = q.mba_IdCbteCble,
                                 mba_IdTipocbte = q.mba_IdTipocbte,
                                 mcj_Secuencia = q.Secuencia,
                                 tc_descripcion = q.tc_descripcion,
                                 cr_Valor = q.cr_Valor,
                                 cm_fecha = q.cm_fecha,
                                 cm_observacion = q.cm_observacion,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 cr_NumDocumento = q.cr_NumDocumento,
                                 IdCtaCble = q.IdCtaCble,
                                 ca_Descripcion = q.ca_Descripcion
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> get_list_x_depositar(int IdEmpresa, int IdSucursal)
        {
            try
            {
                List<ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info> Lista;

                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = (from q in Context.vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_x_depositar
                             where q.IdEmpresa == IdEmpresa
                             && q.cbr_IdSucursal==IdSucursal
                             select new ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_Info
                             {
                                 mcj_IdEmpresa = q.IdEmpresa,
                                 mcj_IdTipocbte = q.IdTipocbte,
                                 mcj_IdCbteCble = q.IdCbteCble,
                                 mcj_Secuencia = q.Secuencia,
                                 tc_descripcion = q.tc_descripcion,
                                 cr_Valor = q.cr_Valor,
                                 cm_fecha = q.cm_fecha,
                                 cm_observacion = q.cm_observacion,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 cr_NumDocumento = q.cr_NumDocumento,
                                 IdCtaCble = q.IdCtaCble,
                                 ca_Descripcion = q.ca_Descripcion
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
