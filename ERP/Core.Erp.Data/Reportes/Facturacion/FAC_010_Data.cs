using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_010_Data
    {
        public List<FAC_010_Info> get_list(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin, string IdCatalogo_FormaPago)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;
                var fecha_inicio = fecha_ini.Date;
                var fecha_fin_ = fecha_fin.Date;
                List<FAC_010_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPFAC_010(IdEmpresa, IdSucursalIni, IdSucursalFin, fecha_inicio, fecha_fin_, IdCatalogo_FormaPago)
                             select new FAC_010_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,
                                 Estado = q.Estado,
                                 vt_NumFactura = q.vt_NumFactura,
                                 IdCliente = q.IdCliente,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 NombreFormaPago = q.NombreFormaPago,
                                 IdCatalogo_FormaPago = q.IdCatalogo_FormaPago,
                                 vt_fecha = q.vt_fecha,
                                 Ve_Vendedor = q.Ve_Vendedor,
                                 IdVendedor = q.IdVendedor,
                                 Su_Descripcion = q.Su_Descripcion,
                                 Su_Telefonos = q.Su_Telefonos,
                                 Su_Direccion = q.Su_Direccion,
                                 Su_Ruc = q.Su_Ruc,
                                 SubtotalIVAConDscto = q.SubtotalIVAConDscto,
                                 SubtotalSinIVAConDscto = q.SubtotalSinIVAConDscto,
                                 ValorIVA = q.ValorIVA,
                                 Total = q.Total,
                                 FacturasAnuladas = q.FacturasAnuladas,
                                 FacturasEmitidas = q.FacturasEmitidas
                                 
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
