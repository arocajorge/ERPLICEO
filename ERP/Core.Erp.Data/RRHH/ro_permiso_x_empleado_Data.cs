using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
    public class ro_permiso_x_empleado_Data
    {
        public List<ro_permiso_x_empleado_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_permiso_x_empleado_Info> Lista;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ro_permiso_x_empleado
                                 where q.IdEmpresa == IdEmpresa
                                 select new ro_permiso_x_empleado_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdEmpleado = q.IdEmpleado,
                                     IdEmpleadoAprueba = q.IdEmpleadoAprueba,
                                     estado = q.estado,
                                     Asunto = q.Asunto,
                                     Descripcion = q.Descripcion,
                                     DescuentaVacaciones = q.DescuentaVacaciones,
                                     FechaFin = q.FechaFin,
                                     FechaInicio = q.FechaInicio,
                                     HoraRegreso = q.HoraRegreso,
                                     HoraSalida =q.HoraSalida,
                                     IdPermiso = q.IdPermiso,
                                     Recuperable = q.Recuperable,
                                     TipoPermiso = q.TipoPermiso
                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_permiso_x_empleado
                                 where q.IdEmpresa == IdEmpresa
                                 && q.estado == true
                                 select new ro_permiso_x_empleado_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdEmpleado = q.IdEmpleado,
                                     IdEmpleadoAprueba = q.IdEmpleadoAprueba,
                                     estado = q.estado,
                                     Asunto = q.Asunto,
                                     Descripcion = q.Descripcion,
                                     DescuentaVacaciones = q.DescuentaVacaciones,
                                     FechaFin = q.FechaFin,
                                     FechaInicio = q.FechaInicio,
                                     HoraRegreso = q.HoraRegreso,
                                     HoraSalida = q.HoraSalida,
                                     IdPermiso = q.IdPermiso,
                                     Recuperable = q.Recuperable,
                                     TipoPermiso = q.TipoPermiso
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_permiso_x_empleado_Info get_info(int IdEmpresa, decimal IdPermiso)
        {
            try
            {
                ro_permiso_x_empleado_Info info = new ro_permiso_x_empleado_Info();
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_permiso_x_empleado Entity = Context.ro_permiso_x_empleado.Where(q => q.IdEmpresa == IdEmpresa && q.IdPermiso == IdPermiso).FirstOrDefault();

                    info = new ro_permiso_x_empleado_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEmpleado = Entity.IdEmpleado,
                        IdEmpleadoAprueba = Entity.IdEmpleadoAprueba,
                        estado = Entity.estado,
                        Asunto = Entity.Asunto,
                        Descripcion = Entity.Descripcion,
                        DescuentaVacaciones = Entity.DescuentaVacaciones,
                        FechaFin = Entity.FechaFin,
                        FechaInicio = Entity.FechaInicio,
                        HoraRegreso = Entity.HoraRegreso,
                        HoraSalida = Entity.HoraSalida,
                        IdPermiso = Entity.IdPermiso,
                        Recuperable = Entity.Recuperable,
                        TipoPermiso = Entity.TipoPermiso
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_permiso_x_empleado
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdPermiso) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_permiso_x_empleado_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_permiso_x_empleado Entity = new ro_permiso_x_empleado
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpleado = info.IdEmpleado,
                        IdEmpleadoAprueba = info.IdEmpleadoAprueba,
                        estado = true,
                        Asunto = info.Asunto,
                        Descripcion = info.Descripcion,
                        DescuentaVacaciones = info.DescuentaVacaciones,
                        FechaFin = info.FechaFin,
                        FechaInicio = info.FechaInicio,
                        HoraRegreso = info.HoraRegreso,
                        HoraSalida = info.HoraSalida,
                        IdPermiso = info.IdPermiso=get_id(info.IdEmpresa),
                        Recuperable = info.Recuperable,
                        TipoPermiso = info.TipoPermiso
                    };
                    Context.ro_permiso_x_empleado.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_permiso_x_empleado_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_permiso_x_empleado Entity = Context.ro_permiso_x_empleado.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdPermiso == info.IdPermiso).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.IdEmpleado = info.IdEmpleado;
                    Entity.IdEmpleadoAprueba = info.IdEmpleadoAprueba;
                    Entity.Asunto = info.Asunto;
                    Entity.Descripcion = info.Descripcion;
                    Entity.DescuentaVacaciones = info.DescuentaVacaciones;
                    Entity.FechaFin = info.FechaFin;
                    Entity.FechaInicio = info.FechaInicio;
                    Entity.HoraRegreso = info.HoraRegreso;
                    Entity.HoraSalida = info.HoraSalida;
                    Entity.Recuperable = info.Recuperable;
                    Entity.TipoPermiso = info.TipoPermiso;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(ro_permiso_x_empleado_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_permiso_x_empleado Entity = Context.ro_permiso_x_empleado.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdPermiso == info.IdPermiso).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.estado = false;

                    Context.SaveChanges();
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
