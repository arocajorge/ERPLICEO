using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_013_Data
    {
        public List<BAN_013_Info> GetList(int IdEmpresa, int IdBanco, decimal IdPersona, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                int IdBanco_ini = IdBanco;
                int IdBanco_fin = IdBanco == 0 ? 9999999 : IdBanco;
                decimal IdPersona_ini = IdPersona;
                decimal IdPersona_fin = IdPersona == 0 ? 99999999 : IdPersona;
                List<BAN_013_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWBAN_013.Where(q => q.IdEmpresa_pago == IdEmpresa
                    && IdBanco_ini <= q.IdBanco && q.IdBanco <= IdBanco_fin
                    && IdPersona_ini <= q.IdPersona && q.IdPersona <= IdPersona_fin
                    && fecha_ini <= q.BAN_Fecha
                    && q.BAN_Fecha <= fecha_fin
                    ).Select(q => new BAN_013_Info
                    {
                        IdEmpresa_pago = q.IdEmpresa_pago,
                        BAN_Fecha = q.BAN_Fecha,
                        BAN_Observacion = q.BAN_Observacion,
                        ba_descripcion = q.ba_descripcion,
                        cb_Cheque = q.cb_Cheque,
                        CXP_Documento = q.CXP_Documento,
                        CXP_Fecha = q.CXP_Fecha,
                        CXP_Observacion = q.CXP_Observacion,
                        IdBanco = q.IdBanco,
                        IdCbteCble_cxp = q.IdCbteCble_cxp,
                        IdCbteCble_pago = q.IdCbteCble_pago,
                        IdPersona = q.IdPersona,
                        IdTipoCbte_pago = q.IdTipoCbte_pago,
                        MontoAplicado = q.MontoAplicado,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        TipoPago = q.TipoPago
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
