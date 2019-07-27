using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
   public class ro_empleado_proyeccion_gastos_det_Data
    {
        public List<ro_empleado_proyeccion_gastos_det_Info> get_list(int IdEmpresa, decimal IdTransaccion)
        {
            try
            {
                List<ro_empleado_proyeccion_gastos_det_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.ro_empleado_proyeccion_gastos_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTransaccion== IdTransaccion
                             select new ro_empleado_proyeccion_gastos_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTransaccion=q.IdTransaccion,
                                 Secuencia = q.Secuencia,
                                 IdTipoGasto = q.IdTipoGasto,
                                 Valor=q.Valor

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
