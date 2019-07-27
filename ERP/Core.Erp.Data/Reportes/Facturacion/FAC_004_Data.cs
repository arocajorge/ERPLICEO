using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_004_Data
    {
        public List<FAC_004_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                List<FAC_004_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWFAC_004
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega == IdBodega
                             && q.IdNota == IdNota
                             select new FAC_004_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdNota = q.IdNota,
                                 Secuencia = q.Secuencia,
                                 CodTipoNota = q.CodTipoNota,
                                 IdTipoDocumento = q.IdTipoDocumento,
                                 numDocumento = q.numDocumento,
                                 IdCliente = q.IdCliente,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_direccion = q.pe_direccion,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 pe_telefonoCasa = q.pe_telefonoCasa,
                                 nombreProducto = q.nombreProducto,
                                 no_fecha = q.no_fecha,
                                 no_fecha_venc = q.no_fecha_venc,
                                 IdProducto = q.IdProducto,
                                 IdTipoNota = q.IdTipoNota,
                                 sc_cantidad = q.sc_cantidad,
                                 sc_iva = q.sc_iva,
                                 sc_observacion = q.sc_observacion,
                                 sc_Precio = q.sc_Precio,
                                 sc_subtotal = q.sc_subtotal,
                                 sc_total = q.sc_total,
                                 bo_Descripcion = q.bo_Descripcion,
                                 Plazo = q.Plazo,
                                 Su_Descripcion = q.Su_Descripcion,
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
