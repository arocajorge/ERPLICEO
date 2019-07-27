using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_empleado_x_horario_Data
    {
        public List< ro_empleado_x_horario_Info> get_list(int IdEmpresa)
        {
            try
            {
                List< ro_empleado_x_horario_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        Lista = (from q in Context. ro_empleado_x_horario
                                 where q.IdEmpresa == IdEmpresa
                                 select new  ro_empleado_x_horario_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdEmpleado = q.IdEmpleado,
                                     IdHorario = q.IdHorario,
                                     EsPredeterminado=q.EsPredeterminado
                                 }).ToList();                
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public  ro_empleado_x_horario_Info get_info(int IdEmpresa, int IdEmpleado)
        {
            try
            {
                 ro_empleado_x_horario_Info info = new  ro_empleado_x_horario_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                     ro_empleado_x_horario Entity = Context. ro_empleado_x_horario.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado == IdEmpleado);
                    if (Entity == null) return null;

                    info = new  ro_empleado_x_horario_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEmpleado = Entity.IdEmpleado,
                        IdHorario = Entity.IdHorario,
                        EsPredeterminado = Entity.EsPredeterminado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB( ro_empleado_x_horario_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                     ro_empleado_x_horario Entity = new  ro_empleado_x_horario
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpleado = info.IdEmpleado,
                        IdHorario = info.IdHorario,
                        FechaIngresa = info.FechaIngresa,
                        UsuarioIngresa=info.UsuarioIngresa
                    };
                    Context. ro_empleado_x_horario.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
         public bool anularDB( ro_empleado_x_horario_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Context.Database.ExecuteSqlCommand("delete ro_empleado_x_horario where Idempresa='"+info.IdEmpresa+ "' and IdEmpleado ='" + info.IdEmpleado+"'");
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
