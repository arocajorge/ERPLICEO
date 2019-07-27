using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
   public class CXP_005_Data
    {
        public List<CXP_005_Info> get_list(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                List<CXP_005_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_005
                             where q.IdEmpresa == IdEmpresa
                             && q.IdConciliacion == IdConciliacion
                             select new CXP_005_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion = q.IdConciliacion,
                                 IdTipoCbte = q.IdTipoCbte,
                                 IdCbteCble = q.IdCbteCble,
                                 secuencia = q.secuencia,
                                 IdCtaCble = q.IdCtaCble,
                                 pc_Cuenta = q.pc_Cuenta,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor_Haber = q.dc_Valor_Haber,
                                 Fecha = q.Fecha,
                                 Observacion = q.Observacion,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Estado = q.Estado,
                                 IdProveedor=q.IdProveedor,
                                 IdSucursal = q.IdSucursal,
                                 NomUsuario = q.NomUsuario,
                                 Su_Descripcion =q.Su_Descripcion
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
