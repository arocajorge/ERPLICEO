using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_009_Data
    {
        public List<CXP_009_Info> get_list(int IdEmpresa,int IdSucursal, DateTime FechaIni, DateTime FechaFin, bool mostrar_anulados)
        {
            try
            {
                List<CXP_009_Info> Lista;
                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                using (Entities_reportes Context = new Entities_reportes())
                {
                    if(mostrar_anulados==true)
                    {
                        Lista = (from q in Context.VWCXP_009
                                 where q.IdEmpresa == IdEmpresa
                                 && IdSucursalIni <= q.IdSucursal
                                 && q.IdSucursal <= IdSucursalFin
                                 && FechaIni <= q.fecha_retencion && q.fecha_retencion <= FechaFin
                                 && q.Estado == "I"
                                 select new CXP_009_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                     IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                     IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                                     IdProveedor = q.IdProveedor,
                                     nom_proveedor = q.nom_proveedor,
                                     ced_proveedor = q.ced_proveedor,
                                     dir_proveedor = q.dir_proveedor,
                                     co_fechaOg = q.co_fechaOg,
                                     co_serie = q.co_serie,
                                     num_factura = q.num_factura,
                                     co_FechaFactura = q.co_FechaFactura,
                                     Estado = q.Estado,
                                     TipoDocumento = q.TipoDocumento,
                                     fecha_retencion = q.fecha_retencion,
                                     ejercicio_fiscal = q.ejercicio_fiscal,
                                     IdRetencion = q.IdRetencion,
                                     Idsecuencia = q.Idsecuencia,
                                     Impuesto = q.Impuesto,
                                     base_retencion = q.base_retencion,
                                     IdCodigo_SRI = q.IdCodigo_SRI,
                                     cod_Impuesto_SRI = q.cod_Impuesto_SRI,
                                     por_Retencion_SRI = q.por_Retencion_SRI,
                                     valor_Retenido = q.valor_Retenido,
                                     IdEmpresa_Ogiro = q.IdEmpresa_Ogiro,
                                     serie = q.serie,
                                     NumRetencion = q.NumRetencion,
                                     co_descripcion = q.co_descripcion,
                                     IdCtaCble = q.IdCtaCble,
                                     co_observacion = q.co_observacion,
                                     IdCbteCbleRet = q.IdCbteCbleRet,
                                     IdSucursal = q.IdSucursal,
                                     Su_Descripcion = q.Su_Descripcion
                                 }).ToList();
                    }
                    else
                    {
                        Lista = (from q in Context.VWCXP_009
                                 where q.IdEmpresa == IdEmpresa
                                 && IdSucursalIni <= q.IdSucursal
                                 && q.IdSucursal <= IdSucursalFin
                                 && FechaIni <= q.fecha_retencion && q.fecha_retencion <= FechaFin
                                 && q.Estado == "A"
                                 select new CXP_009_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                     IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                     IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                                     IdProveedor = q.IdProveedor,
                                     nom_proveedor = q.nom_proveedor,
                                     ced_proveedor = q.ced_proveedor,
                                     dir_proveedor = q.dir_proveedor,
                                     co_fechaOg = q.co_fechaOg,
                                     co_serie = q.co_serie,
                                     num_factura = q.num_factura,
                                     co_FechaFactura = q.co_FechaFactura,
                                     Estado = q.Estado,
                                     TipoDocumento = q.TipoDocumento,
                                     fecha_retencion = q.fecha_retencion,
                                     ejercicio_fiscal = q.ejercicio_fiscal,
                                     IdRetencion = q.IdRetencion,
                                     Idsecuencia = q.Idsecuencia,
                                     Impuesto = q.Impuesto,
                                     base_retencion = q.base_retencion,
                                     IdCodigo_SRI = q.IdCodigo_SRI,
                                     cod_Impuesto_SRI = q.cod_Impuesto_SRI,
                                     por_Retencion_SRI = q.por_Retencion_SRI,
                                     valor_Retenido = q.valor_Retenido,
                                     IdEmpresa_Ogiro = q.IdEmpresa_Ogiro,
                                     serie = q.serie,
                                     NumRetencion = q.NumRetencion,
                                     co_descripcion = q.co_descripcion,
                                     IdCtaCble = q.IdCtaCble,
                                     co_observacion = q.co_observacion,
                                     IdCbteCbleRet = q.IdCbteCbleRet,
                                     IdSucursal = q.IdSucursal,
                                     Su_Descripcion = q.Su_Descripcion
                                 }).ToList();
                    }
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
