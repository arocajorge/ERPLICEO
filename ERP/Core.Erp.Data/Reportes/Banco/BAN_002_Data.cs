using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_002_Data
    {
        public List<BAN_002_Info> get_list(int IdEmpresa, int IdTipocbte, decimal IdCbteCble)
        {
            try
            {
                List<BAN_002_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWBAN_002
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTipocbte == IdTipocbte
                             && q.IdCbteCble == IdCbteCble
                             select new BAN_002_Info
                             {
                                 CodTipoCbteBan = q.CodTipoCbteBan,
                                 IdEmpresa = q.IdEmpresa,
                                 IdCbteCble = q.IdCbteCble,
                                 IdTipocbte = q.IdTipocbte,
                                 IdBanco = q.IdBanco,
                                 ba_descripcion = q.ba_descripcion,
                                 cb_Fecha = q.cb_Fecha,
                                 cb_Observacion = q.cb_Observacion,
                                 Estado = q.Estado,
                                 IdTipoNota = q.IdTipoNota,
                                 Descripcion_TipoNota = q.Descripcion_TipoNota,
                                 NomBeneficiario = q.NomBeneficiario,
                                 IdCtaCble = q.IdCtaCble,
                                 pc_Cuenta = q.pc_Cuenta,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor_Haber = q.dc_Valor_Haber,
                                 Nombre = q.Nombre,
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
