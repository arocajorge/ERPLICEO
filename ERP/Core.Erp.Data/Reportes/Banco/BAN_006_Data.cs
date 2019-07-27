using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_006_Data
    {
        public List<BAN_006_Info> get_list(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble, int NumDesde, int NumHasta, int IdBanco)
        {
            try
            {
                int IdBancoIni = IdBanco;
                int IdBancoFin = IdBanco == 0 ? 9999 : IdBanco;
                List<BAN_006_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    if(NumDesde==0)
                    Lista = (from q in Context.VWBAN_006
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTipoCbte == IdTipoCbte
                             && q.IdCbteCble == IdCbteCble
                             select new BAN_006_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipoCbte = q.IdTipoCbte,
                                 IdCbteCble = q.IdCbteCble,
                                 secuencia = q.secuencia,
                                 cb_Cheque = q.cb_Cheque,
                                 cb_Fecha = q.cb_Fecha,
                                 cb_giradoA = q.cb_giradoA,
                                 cb_Observacion = q.cb_Observacion,
                                 cb_Valor = q.cb_Valor,
                                 IdCtaCble = q.IdCtaCble,
                                 pc_Cuenta = q.pc_Cuenta,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Haber = q.dc_Valor_Haber,
                                 Descripcion_Ciudad = q.Descripcion_Ciudad,
                                 ValorEnLetras = q.ValorEnLetras,
                                 cb_Cheque_numero = q.cb_Cheque_numero,
                                 IdBanco = q.IdBanco,
                                 NombreUsuario = q.NombreUsuario,
                                 ba_descripcion = q.ba_descripcion,
                                 ba_Num_Cuenta = q.ba_Num_Cuenta,
                                 ba_Tipo = q.ba_Tipo
                             }).ToList();

                    else
                        Lista = (from q in Context.VWBAN_006
                                 where q.IdEmpresa == IdEmpresa
                                 && NumDesde <= q.cb_Cheque_numero
                                 && q.cb_Cheque_numero <= NumHasta
                                 && IdBancoIni <= q.IdBanco
                                 && q.IdBanco <= IdBancoFin
                                 select new BAN_006_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoCbte = q.IdTipoCbte,
                                     IdCbteCble = q.IdCbteCble,
                                     secuencia = q.secuencia,
                                     cb_Cheque = q.cb_Cheque,
                                     cb_Fecha = q.cb_Fecha,
                                     cb_giradoA = q.cb_giradoA,
                                     cb_Observacion = q.cb_Observacion,
                                     cb_Valor = q.cb_Valor,
                                     IdCtaCble = q.IdCtaCble,
                                     pc_Cuenta = q.pc_Cuenta,
                                     dc_Valor_Debe = q.dc_Valor_Debe,
                                     dc_Valor = q.dc_Valor,
                                     dc_Valor_Haber = q.dc_Valor_Haber,
                                     Descripcion_Ciudad = q.Descripcion_Ciudad,
                                     ValorEnLetras = q.ValorEnLetras,
                                     cb_Cheque_numero = q.cb_Cheque_numero,
                                     IdBanco = q.IdBanco,
                                     NombreUsuario = q.NombreUsuario,
                                     ba_descripcion = q.ba_descripcion,
                                     ba_Num_Cuenta = q.ba_Num_Cuenta,
                                     ba_Tipo = q.ba_Tipo
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
