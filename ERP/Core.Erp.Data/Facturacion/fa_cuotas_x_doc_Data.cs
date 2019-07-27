using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Facturacion
{
    public class fa_cuotas_x_doc_Data
    {
        public List<fa_cuotas_x_doc_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                List<fa_cuotas_x_doc_Info> Lista;

                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.fa_cuotas_x_doc
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega == IdBodega
                             && q.IdCbteVta == IdCbteVta
                             select new fa_cuotas_x_doc_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,
                                 secuencia = q.secuencia,
                                 num_cuota = q.num_cuota,
                                 fecha_vcto_cuota = q.fecha_vcto_cuota,
                                 valor_a_cobrar = q.valor_a_cobrar,
                                 Estado = q.Estado,
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
