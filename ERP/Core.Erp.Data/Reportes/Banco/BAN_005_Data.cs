using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_005_Data
    {
        public List<BAN_005_Info> get_list(int IdEmpresa, int IdTipocbte, decimal IdCbteCble, int NumDesde, int NumHasta, int IdBanco)
        {
            try
            {
                int IdBancoIni = IdBanco;
                int IdBancoFin = IdBanco == 0 ? 9999 : IdBanco;
                List<BAN_005_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    if(NumDesde==0)
                    Lista = (from q in Context.VWBAN_005
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTipocbte == IdTipocbte
                             && q.IdCbteCble == IdCbteCble
                             select new BAN_005_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipocbte = q.IdTipocbte,
                                 IdCbteCble = q.IdCbteCble,
                                 cb_Fecha = q.cb_Fecha,
                                 cb_giradoA = q.cb_giradoA,
                                 cb_Valor  = q.cb_Valor,
                                 Descripcion_Ciudad = q.Descripcion_Ciudad,
                                 ValorEnLetras = q.ValorEnLetras,
                                 cb_Cheque = q.cb_Cheque,
                                 cb_Cheque_numero = q.cb_Cheque_numero,
                                 Estado = q.Estado,
                                 cb_Observacion = q.cb_Observacion,
                                 IdBanco = q.IdBanco,
                                 NombreUsuario = q.NombreUsuario
                             }).ToList();
                    else
                        Lista = (from q in Context.VWBAN_005
                                 where q.IdEmpresa == IdEmpresa
                                 && NumDesde <= q.cb_Cheque_numero
                                 && q.cb_Cheque_numero <= NumHasta
                                 && IdBancoIni <= q.IdBanco
                                 && q.IdBanco <= IdBancoFin
                                 select new BAN_005_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipocbte = q.IdTipocbte,
                                     IdCbteCble = q.IdCbteCble,
                                     cb_Fecha = q.cb_Fecha,
                                     cb_giradoA = q.cb_giradoA,
                                     cb_Valor = q.cb_Valor,
                                     Descripcion_Ciudad = q.Descripcion_Ciudad,
                                     ValorEnLetras = q.ValorEnLetras,
                                     cb_Cheque = q.cb_Cheque,
                                     cb_Cheque_numero = q.cb_Cheque_numero,
                                     Estado = q.Estado,
                                     cb_Observacion = q.cb_Observacion,
                                     IdBanco = q.IdBanco,
                                     NombreUsuario = q.NombreUsuario
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
