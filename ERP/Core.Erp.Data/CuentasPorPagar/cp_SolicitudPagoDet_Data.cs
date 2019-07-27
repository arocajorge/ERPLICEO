using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_SolicitudPagoDet_Data
    {
        public List<cp_SolicitudPagoDet_Info> GetListPorPagar(int IdEmpresa, int IdSucursal)

        {
            try
            {
                int secuencia = 1;
                List<cp_SolicitudPagoDet_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_orden_giro_x_pagar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.Saldo_OG > 0
                             orderby q.co_fechaOg descending
                             select new cp_SolicitudPagoDet_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpresa_cxp = q.IdEmpresa,
                                 IdTipoCbte_cxp = q.IdTipoCbte_Ogiro,
                                 IdCbteCble_cxp = q.IdCbteCble_Ogiro,
                                 TipoDocumento = q.cod_Documento,
                                 ValorAPagar = q.co_valorpagar,
                                 Fecha = q.co_fechaOg,
                                 info_proveedor = new cp_proveedor_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdPersona = q.IdPersona,
                                     info_persona = new Info.General.tb_persona_Info
                                     {
                                         IdPersona = q.IdPersona,
                                         pe_nombreCompleto = q.nom_proveedor,
                                         pe_razonSocial = q.nom_proveedor

                                     }
                                 }


                             }).ToList();
                }
                Lista.ForEach(v=>v.Secuencia=secuencia++);
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
