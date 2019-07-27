using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.Compras.Base;

namespace Core.Erp.Data.Compras
{
   public class com_comprador_Data
    {
        public List<com_comprador_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<com_comprador_Info> Lista;
                using (Entities_compras Context = new Entities_compras())
                {
                    if(mostrar_anulados)
                        Lista=(from q in Context.com_comprador
                               where q.IdEmpresa == IdEmpresa
                               select new com_comprador_Info
                               {
                                   IdEmpresa = q.IdEmpresa,
                                   IdComprador = q.IdComprador,
                                   Estado = q.Estado,
                                   Descripcion = q.Descripcion,
                                   IdUsuario_com = q.IdUsuario_com,

                                   EstadoBool = q.Estado == "A" ? true : false
                               }).ToList();
                   
                    else
                        Lista = (from q in Context.com_comprador
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new com_comprador_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdComprador = q.IdComprador,
                                     Estado = q.Estado,
                                     Descripcion = q.Descripcion,
                                     IdUsuario_com = q.IdUsuario_com,

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

        public com_comprador_Info get_info(int IdEmpresa, decimal IdComprador)
        {
            try
            {
                com_comprador_Info info = new com_comprador_Info();
                using (Entities_compras Context = new Entities_compras())
                {
                    com_comprador Entity = Context.com_comprador.Where(q => q.IdEmpresa == IdEmpresa && q.IdComprador == IdComprador).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new com_comprador_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdComprador = Entity.IdComprador,
                        Estado = Entity.Estado,
                        Descripcion = Entity.Descripcion,
                        IdUsuario_com = Entity.IdUsuario_com                        
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
                    var lst = from q in Context.com_comprador
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdComprador) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(com_comprador_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_comprador Entity = new com_comprador
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdComprador = info.IdComprador = get_id(info.IdEmpresa),
                        Estado = "A",
                        Descripcion = info.Descripcion,
                        IdUsuario_com = info.IdUsuario_com,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now

                    };
                    Context.com_comprador.Add(Entity);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(com_comprador_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_comprador Entity = Context.com_comprador.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdComprador == info.IdComprador).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Descripcion = info.Descripcion;
                    Entity.IdUsuario_com = info.IdUsuario_com;
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

        public bool anularDB(com_comprador_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_comprador Entity = Context.com_comprador.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdComprador == info.IdComprador).FirstOrDefault();
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

        public com_comprador_Info get_info_x_IdUsuario(int IdEmpresa, string IdUsuario)
        {
            try
            {
                com_comprador_Info info = new com_comprador_Info();
                using (Entities_compras Context = new Entities_compras())
                {
                    com_comprador Entity = Context.com_comprador.Where(q => q.IdEmpresa == IdEmpresa && q.IdUsuario_com == IdUsuario).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new com_comprador_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdComprador = Entity.IdComprador,
                        Estado = Entity.Estado,
                        Descripcion = Entity.Descripcion,
                        IdUsuario_com = Entity.IdUsuario_com
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
