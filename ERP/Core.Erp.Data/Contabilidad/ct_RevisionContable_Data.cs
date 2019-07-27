using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_RevisionContable_Data
    {
        public List<ct_RevisionContableFacturas_Info> get_list_facturas(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<ct_RevisionContableFacturas_Info> Lista;
                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    Lista = db.vwct_RevisionContableFacturas.Where(q => q.IdEmpresa == IdEmpresa && FechaIni <= q.vt_fecha && q.vt_fecha <= FechaFin).Select(q => new ct_RevisionContableFacturas_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        IdBodega = q.IdBodega,
                        IdCbteVta = q.IdCbteVta,
                        ct_IdEmpresa = q.ct_IdEmpresa,
                        ct_IdTipoCbte = q.ct_IdTipoCbte,
                        ct_IdCbteCble = q.ct_IdCbteCble,
                        Nombres = q.Nombres,
                        Referencia = q.Referencia,
                        vt_fecha = q.vt_fecha,
                        TotalModulo = q.TotalModulo,
                        TotalContabilidad = q.TotalContabilidad,
                        Diferencia = q.Diferencia
                    }).ToList();
                }
                Lista.ForEach(q => q.IdSecuencia = Convert.ToDecimal(q.IdEmpresa.ToString("00") + q.IdSucursal.ToString("00") + q.IdBodega.ToString("00") + q.IdCbteVta.ToString("00000000")));
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
