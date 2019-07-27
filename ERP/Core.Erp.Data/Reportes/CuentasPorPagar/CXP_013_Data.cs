using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_013_Data
    {
        public List<CXP_013_Info> get_list(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                List<CXP_013_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_013
                             where q.IdEmpresa == IdEmpresa
                             && q.IdRetencion == IdRetencion
                             select new CXP_013_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdRetencion = q.IdRetencion,
                                 Idsecuencia = q.Idsecuencia,
                                 re_TipoRet = q.re_TipoRet,
                                 co_factura = q.co_factura,
                                 NumRetencion = q.NumRetencion,
                                 TipoComprobante = q.TipoComprobante,
                                 FechaDeEmision = q.FechaDeEmision,
                                 EjercicioFiscal = q.EjercicioFiscal,
                                 re_baseRetencion = q.re_baseRetencion,
                                 re_Porcen_retencion = q.re_Porcen_retencion,
                                 re_valor_retencion = q.re_valor_retencion,
                                 NombreProveedor = q.NombreProveedor,
                                 pr_direccion = q.pr_direccion,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pr_correo = q.pr_correo,
                                 pr_telefonos = q.pr_telefonos,
                                 NAutorizacion = q.NAutorizacion,
                                 Fecha_Autorizacion = q.Fecha_Autorizacion,
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
