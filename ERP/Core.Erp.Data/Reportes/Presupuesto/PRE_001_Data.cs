using Core.Erp.Info.Reportes.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Presupuesto
{
    public class PRE_001_Data
    {
        public List<PRE_001_Info> get_list(int IdEmpresa, decimal IdPresupuesto)
        {
            try
            {
                List<PRE_001_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWPRE_001
                             where q.IdEmpresa == IdEmpresa
                             && q.IdPresupuesto == IdPresupuesto
                             select new PRE_001_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdPresupuesto = q.IdPresupuesto,
                                 IdSucursal = q.IdSucursal,
                                 Su_Descripcion = q.Su_Descripcion,
                                 IdPeriodo = q.IdPeriodo,
                                 DescripciónPeriodo = q.DescripciónPeriodo,
                                 Observacion = q.Observacion,
                                 Estado = q.Estado,
                                 MontoSolicitado = q.MontoSolicitado,
                                 MontoAprobado = q.MontoAprobado,
                                 Secuencia = q.Secuencia,
                                 IdRubro = q.IdRubro,
                                 DescripcionRubro = q.DescripcionRubro,
                                 IdCtaCble = q.IdCtaCble,
                                 Monto = q.Monto,
                                 pc_Cuenta = q.pc_Cuenta,
                                 IdUsuarioAprobacion = q.IdUsuarioAprobacion,
                                 FechaAprobacion = q.FechaAprobacion

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
