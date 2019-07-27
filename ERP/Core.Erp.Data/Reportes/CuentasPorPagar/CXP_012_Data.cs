using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_012_Data
    {
        public List<CXP_012_Info> get_list(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                List<CXP_012_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_012
                             where q.IdEmpresa == IdEmpresa
                             && q.IdRetencion == IdRetencion
                             select new CXP_012_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdRetencion = q.IdRetencion,
                                 CodDocumentoTipo = q.CodDocumentoTipo,
                                 serie1 = q.serie1,
                                 serie2 = q.serie2,
                                 NumRetencion = q.NumRetencion,
                                 NAutorizacion = q.NAutorizacion,
                                 Fecha_Autorizacion = q.Fecha_Autorizacion,
                                 fecha = q.fecha,
                                 observacion = q.observacion,
                                 co_serie = q.co_factura,
                                 co_factura = q.co_factura,
                                 co_FechaFactura = q.co_FechaFactura,
                                 pe_razonSocial = q.pe_nombreCompleto,
                                 re_tipoRet = q.re_tipoRet,
                                 re_baseRetencion = q.re_baseRetencion,
                                 IdCodigo_SRI = q.IdCodigo_SRI,
                                 re_Codigo_impuesto = q.re_Codigo_impuesto,
                                 re_Porcen_retencion = q.re_Porcen_retencion,
                                 re_valor_retencion = q.re_valor_retencion,
                                 co_descripcion = q.co_descripcion ,
                                 pe_nombreCompleto=q.pe_nombreCompleto,
                                 pe_cedulaRuc=q.pe_cedulaRuc,
                                 Descripcion=q.Descripcion,
                                 pr_direccion=q.pr_direccion
                                 
                                 
                             }).ToList();
                    foreach (var item in Lista)
                    {
                        item.Fecha_rep = item.fecha.ToString().Substring(6,4);
                    }
                    int cont = 4;
                    if(Lista.Count()!=4)
                    {
                        cont =cont-Lista.Count();
                        while (cont>0)
                        {
                            CXP_012_Info info = new CXP_012_Info();
                            info.Fecha_rep = "";
                            Lista.Add(info);
                            cont = cont -1;
                        }
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
