using Core.Erp.Info.Reportes.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Caja
{
   public  class CAJ_002_Data
    {
        public List<CAJ_002_Info> get_list(int IdEmpresa, decimal IdConciliacionCaja)
        {
            try
            {
                List<CAJ_002_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCAJ_002
                             where q.IdEmpresa == IdEmpresa
                             && q.IdConciliacion_Caja == IdConciliacionCaja
                             select new CAJ_002_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdRow = q.IdRow,
                                 IdConciliacion_Caja = q.IdConciliacion_Caja,
                                 Secuencia = q.Secuencia,
                                 IdEmpresa_OGiro = q.IdEmpresa_OGiro,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro ?? 0,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 co_factura = q.co_factura,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 co_FechaFactura = q.co_FechaFactura,
                                 co_total = q.co_total,
                                 valor_retencion = q.valor_retencion,
                                 valor_a_pagar = q.valor_a_pagar,
                                 valor_a_reponer = q.valor_a_reponer,
                                 Valor_a_aplicar = q.Valor_a_aplicar,
                                 co_observacion = q.co_observacion,
                                 Saldo_cont_al_periodo = q.Saldo_cont_al_periodo,
                                 Ingresos = q.Ingresos,
                                 Total_fact_vale = q.Total_fact_vale,
                                 Dif_x_pagar_o_cobrar = q.Dif_x_pagar_o_cobrar,
                                 TIPO = q.TIPO,
                                 Fecha_fin = q.Fecha_fin,
                                 Fecha_ini = q.Fecha_ini,
                                 NombreCaja = q.NombreCaja,
                                 tm_descripcion = q.tm_descripcion,
                                 IdUsuarioCreacion = q.IdUsuarioCreacion,
                                 NombreUsuario = q.NombreUsuario,
                                 Su_Descripcion =  q.Su_Descripcion,
                                 SecuenciaCaja = q.SecuenciaCaja,
                                 SecuenciaVale = q.SecuenciaVale
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
