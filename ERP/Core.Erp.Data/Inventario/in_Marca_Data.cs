using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_Marca_Data
    {
        public List<in_Marca_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<in_Marca_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.in_Marca
                                 where q.IdEmpresa == IdEmpresa
                                 select new in_Marca_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     Descripcion = q.Descripcion,
                                     IdMarca = q.IdMarca,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.in_Marca
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new in_Marca_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     Descripcion = q.Descripcion,
                                     IdMarca = q.IdMarca,
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
        public in_Marca_Info get_info(int IdEmpresa, int IdMarca)
        {
            try
            {
                in_Marca_Info info = new in_Marca_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Marca Entity = Context.in_Marca.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdMarca == IdMarca);
                    if (Entity == null) return null;
                    info = new in_Marca_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        Descripcion = Entity.Descripcion,
                        IdMarca = Entity.IdMarca,
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
        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_Marca
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdMarca) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_Marca_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Marca Entity = new in_Marca
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMarca = info.IdMarca=get_id(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        Estado = info.Estado="A",

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.in_Marca.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(in_Marca_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Marca Entity = Context.in_Marca.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMarca == info.IdMarca);
                    if (Entity == null) return false;
                    Entity.Descripcion = info.Descripcion;

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
        public bool anularDB(in_Marca_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Marca Entity = Context.in_Marca.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMarca == info.IdMarca);
                    if (Entity == null) return false;
                    Entity.Estado = info.Estado = "I";

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

        public bool si_esta_en_uso(int IdEmpresa, int IdMarca)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_Producto
                              where q.IdEmpresa == IdEmpresa
                              && q.IdMarca==IdMarca
                              select q;
                    if (lst.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
