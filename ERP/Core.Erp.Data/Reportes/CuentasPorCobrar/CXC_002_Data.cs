using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorCobrar
{
   public class CXC_002_Data
    {
        public List<CXC_002_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega_Cbte, decimal IdCbte_vta_nota, string dc_TipoDocumento)
        {
            try
            {
                List<CXC_002_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXC_002
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega_Cbte == IdBodega_Cbte
                             && q.IdCbte_vta_nota == IdCbte_vta_nota
                             && q.dc_TipoDocumento == dc_TipoDocumento
                             select new CXC_002_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdCobro = q.IdCobro,
                                 secuencial = q.secuencial,
                                 dc_TipoDocumento = q.dc_TipoDocumento,
                                 IdBodega_Cbte = q.IdBodega_Cbte,
                                 IdCbte_vta_nota = q.IdCbte_vta_nota,
                                 tc_descripcion = q.tc_descripcion,
                                 dc_ValorPago = q.dc_ValorPago,
                                 Su_Descripcion = q.Su_Descripcion,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 cr_fecha = q.cr_fecha,
                                 cr_TotalCobro = q.cr_TotalCobro,
                                 cr_observacion = q.cr_observacion,
                                 cr_NumDocumento = q.cr_NumDocumento,
                                 vt_NumFactura = q.vt_NumFactura,
                                 vt_fecha = q.vt_fecha,
                                 vt_iva = q.vt_iva,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_total = q.vt_total,
                                 PorcentajeRet = q.PorcentajeRet

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
