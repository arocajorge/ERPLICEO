using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_005_Data
    {
        public List<FAC_005_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCliente, DateTime Fecha_ini, DateTime Fecha_fin, ref List<FAC_005_resumen_Info> lst_resumen)
        {
            try
            {
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 9999 : IdSucursal;

                decimal IdCliente_ini = IdCliente;
                decimal IdCliente_fin = IdCliente == 0 ? 9999999 : IdCliente;

                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;

                List<FAC_005_Info> Lista;

                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPFAC_005(IdEmpresa, IdSucursal_ini, IdSucursal_fin, IdCliente_ini, IdCliente_fin, Fecha_ini, Fecha_fin)
                             select new FAC_005_Info
                             {
                                 IdEmpresa = q.IdEmpresa,   
                                 IdSucursal = q.IdSucursal,
                                 IdCliente = q.IdCliente,
                                 NomCliente = q.NomCliente,
                                 TipoDocumento = q.TipoDocumento,
                                 EsExportacion = q.EsExportacion,
                                 Su_Descripcion = q.Su_Descripcion,
                                 Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                                 SubtotalIVASinDscto = q.SubtotalIVASinDscto,
                                 SubtotalSinIVASinDscto = q.SubtotalSinIVASinDscto,
                                 SubtotalSinDscto = q.SubtotalSinDscto,
                                 Descuento = q.Descuento,
                                 SubtotalIVAConDscto = q.SubtotalIVAConDscto,
                                 SubtotalSinIVAConDscto = q.SubtotalSinIVAConDscto,
                                 SubtotalConDscto = q.SubtotalConDscto,
                                 ValorIVA = q.ValorIVA,
                                 Total = q.Total,
                                 Cantidad = q.Cantidad
                             }).ToList();
                }

                var TdebitosxCta = from Cb in Lista
                                   group Cb by new { Cb.IdSucursal, Cb.Su_Descripcion, Cb.Su_CodigoEstablecimiento }
                                       into grouping
                                   select new { grouping.Key,
                                       CantidadPorSucursal = grouping.Sum(q => q.Cantidad),
                                       CantidadVentasLocales = grouping.Sum(q => q.Cantidad),
                                       BaseImponible = grouping.Sum(q=> q.SubtotalConDscto),
                                       BaseImponible12= grouping.Sum(q=> q.SubtotalIVAConDscto),
                                       BaseImponible0 = grouping.Sum(q => q.SubtotalSinIVAConDscto),
                                       TotalPorSucursal = grouping.Sum(p => p.Total),
                                       TotalVentasLocales = grouping.Sum(p => p.Total),
                                      ValorIva = grouping.Sum(q=> q.ValorIVA) 
                                   };

                foreach (var item in TdebitosxCta)
                {
                    lst_resumen.Add(new FAC_005_resumen_Info
                    {
                        NomSucursal = "Total " + item.Key.Su_CodigoEstablecimiento + " - " + item.Key.Su_Descripcion,
                        CantidadVentasLocales = Convert.ToInt32(item.CantidadVentasLocales),
                        CantidadPorSucursal = Convert.ToInt32(item.CantidadPorSucursal),
                        TotalVentasLocales = item.TotalVentasLocales == null ? 0 : Convert.ToDouble(item.TotalVentasLocales),
                        TotalPorSucursal = item.TotalPorSucursal == null ? 0 : Convert.ToDouble(item.TotalPorSucursal),
                        BaseImponible = item.BaseImponible ?? 0,
                        BaseImponible0 = item.BaseImponible0 ?? 0,
                        BaseImponible12 = item.BaseImponible12 ?? 0,
                        ValorIva = item.ValorIva ?? 0

                    });
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
