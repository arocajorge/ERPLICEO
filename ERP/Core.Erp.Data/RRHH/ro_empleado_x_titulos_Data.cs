using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_empleado_x_titulos_Data
    {
        public List<ro_empleado_x_titulos_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<ro_empleado_x_titulos_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_empleado_x_titulos
                             where q.IdEmpresa == IdEmpresa
                             select new ro_empleado_x_titulos_Info
                             {
                                  IdEmpresa = q.IdEmpresa,
                                  IdEmpleado = q.IdEmpleado,
                                  IdInstitucion = q.IdInstitucion,
                                  IdTitulo = q.IdTitulo,
                                  fecha = q.fecha,
                                  Observacion = q.Observacion,
                                  pe_cedulaRuc=q.pe_cedulaRuc,
                                  titulo=q.titulo,
                                  institucion=q.institucion,
                                  Secuencia=q.Secuencia,
                                  pe_nombreCompleto=q.pe_apellido+" "+q.pe_nombre,
                                  estado=q.estado


                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_empleado_x_titulos_Info get_info(int IdEmpresa, decimal IdEmpleado, int Secuencia)
        {
            try
            {
                ro_empleado_x_titulos_Info info = new ro_empleado_x_titulos_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_titulos Entity = Context.ro_empleado_x_titulos.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado == IdEmpleado &&q.Secuencia==Secuencia);
                    if (Entity == null) return null;

                    info = new ro_empleado_x_titulos_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEmpleado = Entity.IdEmpleado,
                        IdInstitucion = Entity.IdInstitucion,
                        IdTitulo = Entity.IdTitulo,
                        Secuencia=Entity.Secuencia,
                        fecha = Entity.fecha,
                        Observacion = Entity.Observacion
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_empleado_x_titulos_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_titulos Entity = new ro_empleado_x_titulos
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpleado = info.IdEmpleado,
                        Secuencia = get_id(info.IdEmpresa, info.IdEmpleado),
                        IdInstitucion = info.IdInstitucion,
                        IdTitulo = info.IdTitulo,
                        fecha = info.fecha,
                        Observacion = info.Observacion,
                        estado = "A",
                        IdUsuario=info.IdUsuario,
                        Fecha_Transac=DateTime.Now
                       
                    };
                    Context.ro_empleado_x_titulos.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_empleado_x_titulos_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_titulos Entity = Context.ro_empleado_x_titulos.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado==info.IdEmpleado && q.Secuencia == info.Secuencia);
                    if (Entity == null)
                        return false;
                    Entity.IdInstitucion = info.IdInstitucion;
                    Entity.IdTitulo = info.IdTitulo;
                    Entity.Observacion = info.Observacion;
                    Entity.fecha = info.fecha;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                int ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_empleado_x_titulos
                              where q.IdEmpresa == IdEmpresa
                              && q.IdEmpleado==IdEmpleado
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.Secuencia) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_empleado_x_titulos_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_titulos Entity = Context.ro_empleado_x_titulos.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado && q.Secuencia == info.Secuencia);
                    if (Entity == null)
                        return false;
                    Entity.estado = "I";
                    Entity.Fecha_UltAnu = DateTime.Now;
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
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
