using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_007_Data
    {
        public List<CXP_007_Info> get_list(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, bool mostrar_agrupado, int IdSucursal)
        {
            try
            {
                List<CXP_007_Info> Lista;
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;

                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPCXP_007(IdEmpresa, fecha_ini, fecha_fin, mostrar_agrupado, IdSucursalIni, IdSucursalFin)
                             select new CXP_007_Info
                             {
                                 IdRow = q.IdRow,
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                                 Codigo = q.Codigo,
                                 Descripcion = q.Descripcion,
                                 IdProveedor = q.IdProveedor,
                                 pr_nombre = q.pr_nombre,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 serie_fact = q.serie_fact,
                                 num_factura = q.num_factura,
                                 co_FechaFactura = q.co_FechaFactura,
                                 subtotal_iva = q.subtotal_iva,
                                 subtotal_sin_iva = q.subtotal_sin_iva,
                                 valor_iva = q.valor_iva,
                                 NAutorizacion = q.NAutorizacion,
                                 serie_ret = q.serie_ret,
                                 NumRetencion = q.NumRetencion,
                                 re_baseRetencion = q.re_baseRetencion,
                                 re_Codigo_impuesto = q.re_Codigo_impuesto,
                                 re_Porcen_retencion = q.re_Porcen_retencion,
                                 re_valor_retencion = q.re_valor_retencion,
                                 RIVA_0 = q.RIVA_0,
                                 RIVA_10 = q.RIVA_10,
                                 RIVA_100 = q.RIVA_100,
                                 RIVA_20 = q.RIVA_20,
                                 RIVA_30 = q.RIVA_30,
                                 RIVA_70 = q.RIVA_70,
                                 RTF_0 = q.RTF_0,
                                 RTF_0_1 = q.RTF_0_1,
                                 RTF_1 = q.RTF_1,
                                 RTF_10 = q.RTF_10,
                                 RTF_100 = q.RTF_100,
                                 RTF_2 = q.RTF_2,
                                 RTF_8 = q.RTF_8,
                                 Documento = q.Documento,
                                 descripcion_cod_sri = q.descripcion_cod_sri,
                                 re_tipoRet = q.re_tipoRet,
                                 Num_Autorizacion_OG = q.Num_Autorizacion_OG,
                                 IdSucursal = q.IdSucursal,
                                 Su_Descripcion = q.Su_Descripcion
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
