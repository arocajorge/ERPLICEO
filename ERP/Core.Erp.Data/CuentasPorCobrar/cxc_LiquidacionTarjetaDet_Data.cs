using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjetaDet_Data
    {
        public List<cxc_LiquidacionTarjetaDet_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
        {
            try
            {
                List<cxc_LiquidacionTarjetaDet_Info> Lista;

                using (Entities_cuentas_por_cobrar db = new Entities_cuentas_por_cobrar())
                {
                    Lista = db.cxc_LiquidacionTarjetaDet.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdSucursal == IdSucursal && q.IdLiquidacion == IdLiquidacion).Select(q => new cxc_LiquidacionTarjetaDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        IdLiquidacion = q.IdLiquidacion,
                        Secuencia = q.Secuencia,
                        IdMotivo = q.IdMotivo,
                        Valor = q.Valor
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
