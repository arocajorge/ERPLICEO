using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
    public class ro_empleado_x_rubro_acumulado_detalle_Data
    {
        public List<ro_empleado_x_rubro_acumulado_detalle_Info> get_list(int IdEmpresa, decimal IdEmpleado, string IdRubro)
        {
            try
            {
                List<ro_empleado_x_rubro_acumulado_detalle_Info> Lista = new List<ro_empleado_x_rubro_acumulado_detalle_Info>();
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_empleado_x_rubro_acumulado_detalle
                             where q.IdEmpresa == IdEmpresa
                             && q.IdEmpleado == IdEmpleado
                             && q.IdRubro == IdRubro
                             select new ro_empleado_x_rubro_acumulado_detalle_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 IdRubro = q.IdRubro,
                                 IdRubroContabilizacion = q.IdRubroContabilizacion,
                                 IdJornada = q.IdJornada,
                                 Secuencia = q.Secuencia,
                                 ru_descripcion= q.ru_descripcion
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
