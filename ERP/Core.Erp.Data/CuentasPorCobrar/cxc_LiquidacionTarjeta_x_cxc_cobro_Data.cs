using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjeta_x_cxc_cobro_Data
    {
        public List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> GetList(int IdEmpresa, int IdSucursal, decimal? IdLiquidacion)
        {
            try
            {
                List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> Lista;

                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    Lista = (from q in Context.vwcxc_LiquidacionTarjeta_x_cxc_cobro
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdLiquidacion == IdLiquidacion
                             select new cxc_LiquidacionTarjeta_x_cxc_cobro_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 Valor = q.cr_TotalCobro,
                                 IdCobro = q.IdCobro,
                                 cr_fecha = q.cr_fecha,
                                 cr_observacion = q.cr_observacion,
                                 pe_nombreCompleto = q.pe_nombreCompleto 
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
