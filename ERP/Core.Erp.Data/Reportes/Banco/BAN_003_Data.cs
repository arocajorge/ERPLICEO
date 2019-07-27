using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
   public class BAN_003_Data
    {
        public List<BAN_003_Info> get_list(int IdEmpresa, int IdTipocbte, decimal IdCbteCble)
        {
            try
            {
                List<BAN_003_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWBAN_003
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTipocbte == IdTipocbte
                             && q.IdCbteCble == IdCbteCble
                             select new BAN_003_Info
                             {
                                 CodTipoCbteBan = q.CodTipoCbteBan,
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipocbte = q.IdTipocbte,
                                 IdCbteCble = q.IdCbteCble,
                                 IdBanco = q.IdBanco,
                                 ba_descripcion = q.ba_descripcion,
                                 cb_Fecha = q.cb_Fecha,
                                 cb_Observacion = q.cb_Observacion,
                                 Estado = q.Estado,
                                 IdTipoNota = q.IdTipoNota,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 IdCtaCble = q.IdCtaCble,
                                 pc_Cuenta = q.pc_Cuenta,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor_Haber = q.dc_Valor_Haber,
                                 cb_Cheque = q.cb_Cheque,
                                 cb_giradoA = q.cb_giradoA,
                                 NombreUsuario = q.NombreUsuario,
                                 Su_Descripcion = q.Su_Descripcion
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
