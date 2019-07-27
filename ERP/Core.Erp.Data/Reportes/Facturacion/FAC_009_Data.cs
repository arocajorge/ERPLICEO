using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
   public class FAC_009_Data
    {
        public List<FAC_009_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdGuiaRemision)
        {
            try
            {
                List<FAC_009_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWFAC_009
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega == IdBodega
                             && q.IdGuiaRemision == IdGuiaRemision
                             select new FAC_009_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdGuiaRemision = q.IdGuiaRemision,
                                 vt_autorizacion = q.vt_autorizacion,
                                 vt_NumFactura = q.vt_NumFactura,
                                 vt_tipoDoc = q.vt_tipoDoc,
                                 gi_cantidad = q.gi_cantidad,
                                 gi_FechaFinTraslado = q.gi_FechaFinTraslado,
                                 gi_FechaInicioTraslado = q.gi_FechaInicioTraslado,
                                 MotivoTraslado = q.MotivoTraslado,
                                 Direccion_Destino = q.Direccion_Destino,
                                 Direccion_Origen = q.Direccion_Origen,
                                 CedulaCliente = q.CedulaCliente,
                                 NombreTransportista = q.NombreTransportista,
                                 NomCliente = q.NomCliente,
                                 placa = q.placa,
                                 RucEmpresa = q.RucEmpresa,
                                 CedulaTransportista = q.CedulaTransportista,
                                 pr_descripcion = q.pr_descripcion

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
