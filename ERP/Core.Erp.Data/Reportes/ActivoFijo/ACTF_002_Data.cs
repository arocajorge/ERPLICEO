using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.ActivoFijo
{
    public class ACTF_002_Data
    {
        public List<ACTF_002_Info> get_list(int IdEmpresa, decimal IdVtaActivo )
        {
            try
            {
                List<ACTF_002_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWACTF_002
                             where q.IdEmpresa == IdEmpresa
                             && q.IdVtaActivo == IdVtaActivo
                             select new ACTF_002_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdVtaActivo = q.IdVtaActivo,
                                 IdActivoFijo = q.IdActivoFijo,
                                 Af_Nombre = q.Af_Nombre,
                                 ValorActivo = q.ValorActivo,
                                 Valor_Tot_Bajas = q.Valor_Tot_Bajas,
                                 Valor_Tot_Mejora = q.Valor_Tot_Mejora,
                                 Valor_Depre_Acu = q.Valor_Depre_Acu,
                                 Valor_Neto  = q.Valor_Neto,
                                 Valor_Venta = q.Valor_Venta,
                                 Fecha_Venta = q.Fecha_Venta,
                                 Estado = q.Estado,
                                 Concepto_Vta = q.Concepto_Vta,
                                 NumComprobante = q.NumComprobante,
                                 IdCtaCble = q.IdCtaCble,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor_Haber = q.dc_Valor_Haber,
                                 pc_Cuenta = q.pc_Cuenta                                                                  
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
