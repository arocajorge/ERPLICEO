using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
 public   class FAC_012_Data
    {
        public List<FAC_012_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCambio)
        {
            try
            {
                List<FAC_012_Info> Lista;

                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWFAC_012
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega == IdBodega
                             && q.IdCambio == IdCambio
                             select new FAC_012_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCambio = q.IdCambio,
                                 bo_Descripcion =q.bo_Descripcion,
                                 CantidadCambio =q.CantidadCambio,
                                 CantidadFact =q.CantidadFact,
                                 Estado =q.Estado,
                                 Fecha =q.Fecha,
                                  IdCbteVta =q.IdCbteVta,
                                  IdMovi_inven_tipo = q.IdMovi_inven_tipo,
                                  IdNumMovi = q.IdNumMovi,
                                  NombreCliente = q.NombreCliente,
                                  Observacion = q.Observacion,
                                  pr_descripcionCambio = q.pr_descripcionCambio,
                                  pr_descripcionFact = q.pr_descripcionFact,
                                  Secuencia = q.Secuencia,
                                  SecuenciaFact = q.SecuenciaFact,
                                  Su_Descripcion = q.Su_Descripcion,
                                  vt_NumFactura = q.vt_NumFactura
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
