using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_014_Data
    {
        public List<BAN_014_Info> GetList(int IdEmpresa, int IdBanco, decimal IdPersona, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                decimal IdPersonaIni = IdPersona;
                decimal IdPersonaFin = IdPersona == 0 ? 999999999 : IdPersona;

                int IdBancoIni = IdBanco;
                int IdBancoFin = IdBanco == 0 ? 9999999 : IdBanco;

                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;

                List<BAN_014_Info> Lista = new List<BAN_014_Info>();

                using (Entities_reportes db = new Entities_reportes())
                {
                    Lista = db.VWBAN_014.Where(q => q.IdEmpresa_pago == IdEmpresa && IdBancoIni <= q.IdBanco && q.IdBanco <= IdBancoFin && IdPersonaIni <= q.IdPersona && q.IdPersona <= IdPersonaFin
                    && FechaIni <= q.BAN_Fecha && q.BAN_Fecha <= FechaFin).Select(q => new BAN_014_Info
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
