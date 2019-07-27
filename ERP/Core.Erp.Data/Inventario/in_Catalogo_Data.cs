using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_Catalogo_Data
    {
        public List<in_Catalogo_Info> get_list(int IdCatalogo_tipo, bool mostrar_anulados)
        {
            try
            {
                List<in_Catalogo_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados == true)
                        Lista = (from q in Context.in_Catalogo
                                 where q.IdCatalogo_tipo == IdCatalogo_tipo
                                 select new in_Catalogo_Info
                                 {
                                     IdCatalogo_tipo = q.IdCatalogo_tipo,
                                     IdCatalogo = q.IdCatalogo,
                                     Nombre = q.Nombre,
                                     Estado = q.Estado,
                                     Orden = q.Orden,

                                     EstadoBool = q.Estado == "A" ? true : false

                                 }).ToList();
                    else
                        Lista = (from q in Context.in_Catalogo
                                 where q.IdCatalogo_tipo == IdCatalogo_tipo
                                 && q.Estado == "A"
                                 select new in_Catalogo_Info
                                 {
                                     IdCatalogo_tipo = q.IdCatalogo_tipo,
                                     IdCatalogo = q.IdCatalogo,
                                     Nombre = q.Nombre,
                                     Estado = q.Estado,
                                     Orden = q.Orden,

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

        public in_Catalogo_Info get_info(string IdCatalogo)
        {
            try
            {
                in_Catalogo_Info info = new in_Catalogo_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Catalogo Entity = Context.in_Catalogo.FirstOrDefault(q => q.IdCatalogo == IdCatalogo);
                    if (Entity == null) return null;
                    info = new in_Catalogo_Info
                    {
                        IdCatalogo_tipo = Entity.IdCatalogo_tipo,
                        IdCatalogo = Entity.IdCatalogo,
                        Nombre = Entity.Nombre,
                        Estado = Entity.Estado,
                        Orden = Entity.Orden

                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_Catalogo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Catalogo Entity = new in_Catalogo
                    {
                        IdCatalogo_tipo = info.IdCatalogo_tipo,
                        IdCatalogo = info.IdCatalogo,
                        Nombre = info.Nombre,
                        Estado = info.Estado="A",
                        Orden = info.Orden
                    };
                    Context.in_Catalogo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(in_Catalogo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Catalogo Entity = Context.in_Catalogo.FirstOrDefault(q => q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null) return false;

                    Entity.Nombre = info.Nombre;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool anularDB(in_Catalogo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Catalogo Entity = Context.in_Catalogo.FirstOrDefault(q => q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null) return false;

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
        public bool validar_existe_IdCatalogo(string IdCatalogo)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_Catalogo
                              where IdCatalogo == q.IdCatalogo
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
