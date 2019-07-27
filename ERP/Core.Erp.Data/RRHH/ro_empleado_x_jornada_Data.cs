using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
    public class ro_empleado_x_jornada_Data
    {
        public List<ro_empleado_x_jornada_Info> GetList(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                List<ro_empleado_x_jornada_Info> Lista;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = Context.ro_empleado_x_jornada.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdEmpleado == IdEmpleado
                    ).Select(q => new ro_empleado_x_jornada_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdEmpleado = q.IdEmpleado,
                        IdJornada = q.IdJornada,
                        MaxNumHoras = q.MaxNumHoras,
                        Secuencia = q.Secuencia,
                        ValorHora = q.ValorHora
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_empleado_x_jornada_Info GetInfo_Empleado_Jornada(int IdEmpresa, decimal IdEmpleado, int IdJornada)
        {
            try
            {
                ro_empleado_x_jornada_Info info = new ro_empleado_x_jornada_Info();
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_jornada Entity = Context.ro_empleado_x_jornada.Where(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado == IdEmpleado && q.IdJornada == IdJornada).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new ro_empleado_x_jornada_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEmpleado = Entity.IdEmpleado,
                        IdJornada = Entity.IdJornada,
                        MaxNumHoras = Entity.MaxNumHoras,
                        Secuencia = Entity.Secuencia,
                        ValorHora = Entity.ValorHora
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
