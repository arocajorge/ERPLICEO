using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.Compras.Base;

namespace Core.Erp.Data.Compras
{
    public class com_departamento_Data
    {
        public List<com_departamento_Info> get_list(int IdEmpresa , bool mostrar_anulados)
        {
            try
            {
                List<com_departamento_Info> Lista;
                using (Entities_compras Context = new Entities_compras())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.com_departamento
                                 where q.IdEmpresa == IdEmpresa
                                 select new com_departamento_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     nom_departamento = q.nom_departamento,
                                     IdDepartamento = q.IdDepartamento,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.com_departamento
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new com_departamento_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     nom_departamento = q.nom_departamento,
                                     IdDepartamento = q.IdDepartamento,
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

        public com_departamento_Info get_info(int IdEmpresa, decimal IdDepartamento)
        {
            try
            {
                com_departamento_Info info = new com_departamento_Info();
                using (Entities_compras Context = new Entities_compras())
                {
                    com_departamento Entity = Context.com_departamento.Where(q => q.IdEmpresa == IdEmpresa && q.IdDepartamento == IdDepartamento).FirstOrDefault();
                    if (Entity == null) return null;

                    info = new com_departamento_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        nom_departamento = Entity.nom_departamento,
                        IdDepartamento = Entity.IdDepartamento,
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

        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;
                using (Entities_compras Context = new Entities_compras())
                {
                    var lst = from q in Context.com_departamento
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdDepartamento) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(com_departamento_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_departamento Entity = new com_departamento
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdDepartamento = info.IdDepartamento=get_id(info.IdEmpresa),
                        nom_departamento = info.nom_departamento,
                        Estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.com_departamento.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(com_departamento_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_departamento Entity = Context.com_departamento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdDepartamento == info.IdDepartamento).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.nom_departamento = info.nom_departamento;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();

            }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(com_departamento_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_departamento Entity = Context.com_departamento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdDepartamento == info.IdDepartamento).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
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
