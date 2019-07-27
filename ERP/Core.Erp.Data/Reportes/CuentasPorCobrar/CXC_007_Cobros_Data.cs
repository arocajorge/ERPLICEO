using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorCobrar
{
    public class CXC_007_Cobros_Data
    {
        public List<CXC_007_Cobros_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
        {
            try
            {

                List<CXC_007_Cobros_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWCXC_007_Cobros.Where(q => q.IdEmpresa == IdEmpresa
                   && q.IdSucursal == IdSucursal
                   && q.IdLiquidacion == IdLiquidacion
                   ).Select(q => new CXC_007_Cobros_Info
                   {
                       IdLiquidacion = q.IdLiquidacion,
                       IdEmpresa = q.IdEmpresa,
                       IdSucursal = q.IdSucursal,
                       cr_fecha = q.cr_fecha,
                       cr_observacion = q.cr_observacion,
                       IdCobro = q.IdCobro,
                       pe_nombreCompleto = q.pe_nombreCompleto,
                       Secuencia = q.Secuencia,
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
