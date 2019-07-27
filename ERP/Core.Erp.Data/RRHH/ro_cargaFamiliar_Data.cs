using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_cargaFamiliar_Data
    {
        public List<ro_cargaFamiliar_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_cargaFamiliar_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if(mostrar_anulados)
                        Lista = (from q in Context.ro_cargaFamiliar
                                 where q.IdEmpresa == IdEmpresa
                                 select new ro_cargaFamiliar_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdEmpleado = q.IdEmpleado,
                                     IdCargaFamiliar = q.IdCargaFamiliar,
                                     Cedula=q.Cedula,
                                     Nombres=q.Nombres,
                                     capacidades_especiales=q.capacidades_especiales,
                                     Sexo=q.Sexo,
                                     FechaDefucion=q.FechaDefucion,
                                     FechaNacimiento=q.FechaNacimiento,
                                      TipoFamiliar=q.TipoFamiliar,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_cargaFamiliar
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new ro_cargaFamiliar_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdEmpleado = q.IdEmpleado,
                                     IdCargaFamiliar = q.IdCargaFamiliar,
                                     Cedula = q.Cedula,
                                     Nombres = q.Nombres,
                                     capacidades_especiales = q.capacidades_especiales,
                                     Sexo = q.Sexo,
                                     FechaDefucion = q.FechaDefucion,
                                     FechaNacimiento = q.FechaNacimiento,
                                     TipoFamiliar = q.TipoFamiliar,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_cargaFamiliar_Info> get_list(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                List<ro_cargaFamiliar_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                   
                        Lista = (from q in Context.ro_cargaFamiliar
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdEmpleado==IdEmpleado
                                 && q.Estado == "A"
                                 select new ro_cargaFamiliar_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdEmpleado = q.IdEmpleado,
                                     IdCargaFamiliar = q.IdCargaFamiliar,
                                     Cedula = q.Cedula,
                                     Nombres = q.Nombres,
                                     capacidades_especiales = q.capacidades_especiales,
                                     Sexo = q.Sexo,
                                     FechaDefucion = q.FechaDefucion,
                                     FechaNacimiento = q.FechaNacimiento,
                                     TipoFamiliar = q.TipoFamiliar,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_cargaFamiliar_Info get_info(int IdEmpresa, decimal IdEmpleado,int IdCargaFamiliar)
        {
            try
            {
                ro_cargaFamiliar_Info info = new ro_cargaFamiliar_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_cargaFamiliar Entity = Context.ro_cargaFamiliar.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado==IdEmpleado && q.IdCargaFamiliar == IdCargaFamiliar);
                    if (Entity == null) return null;

                    info = new ro_cargaFamiliar_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEmpleado = Entity.IdEmpleado,
                        IdCargaFamiliar = Entity.IdCargaFamiliar,
                        Cedula = Entity.Cedula,
                        Nombres = Entity.Nombres,
                        capacidades_especiales = Entity.capacidades_especiales,
                        Sexo = Entity.Sexo,
                        FechaDefucion = Entity.FechaDefucion,
                        FechaNacimiento = Entity.FechaNacimiento,
                        TipoFamiliar = Entity.TipoFamiliar,
                        Estado = Entity.Estado
                    };
                }

                return info;
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
                    var lst = from q in Context.ro_cargaFamiliar
                              where q.IdEmpresa == IdEmpresa
                              && q.IdEmpleado==IdEmpleado
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCargaFamiliar) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_cargaFamiliar_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_cargaFamiliar Entity = new ro_cargaFamiliar
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpleado = info.IdEmpleado,
                        IdCargaFamiliar = get_id(info.IdEmpresa,info.IdEmpleado),
                        Cedula = info.Cedula,
                        Nombres = info.Nombres,
                        capacidades_especiales = info.capacidades_especiales,
                        Sexo = info.Sexo,
                        FechaDefucion = info.FechaDefucion,
                        FechaNacimiento = info.FechaNacimiento,
                        TipoFamiliar = info.TipoFamiliar,
                        Estado = info.Estado="A"
                    };
                    Context.ro_cargaFamiliar.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_cargaFamiliar_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_cargaFamiliar Entity = Context.ro_cargaFamiliar.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa &&q.IdEmpleado==info.IdEmpleado && q.IdCargaFamiliar == info.IdCargaFamiliar);
                    if (Entity == null)
                        return false;
                    Entity.Nombres = info.Nombres;
                    Entity.Cedula = info.Cedula;
                    Entity.Sexo = info.Sexo;
                    Entity.TipoFamiliar = info.TipoFamiliar;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(ro_cargaFamiliar_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_cargaFamiliar Entity = Context.ro_cargaFamiliar.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado && q.IdCargaFamiliar == info.IdCargaFamiliar);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";
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
