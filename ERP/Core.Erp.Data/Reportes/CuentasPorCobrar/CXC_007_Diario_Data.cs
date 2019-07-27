using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorCobrar
{
    public class CXC_007_Diario_Data
    {
        public List<CXC_007_Diario_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
        {
            try
            {

                List<CXC_007_Diario_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWCXC_007_Diario.Where(q => q.IdEmpresa == IdEmpresa
                   && q.IdSucursal == IdSucursal
                   && q.IdLiquidacion == IdLiquidacion
                   ).Select(q => new CXC_007_Diario_Info
                   {
                       IdLiquidacion = q.IdLiquidacion,
                       IdEmpresa = q.IdEmpresa,
                       IdSucursal = q.IdSucursal,
                       Debe = q.Debe,
                       Haber = q.Haber,
                       IdCtaCble = q.IdCtaCble,
                       pc_Cuenta = q.pc_Cuenta,
                       secuencia = q.secuencia
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