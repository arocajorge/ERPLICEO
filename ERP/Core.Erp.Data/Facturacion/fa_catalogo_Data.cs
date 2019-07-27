using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
   public class fa_catalogo_Data
    {
        public List<fa_catalogo_Info> get_list(int IdCatalogo_tipo, bool mostrar_anulados)
        {
            try
            {
                List<fa_catalogo_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.fa_catalogo
                                 where q.IdCatalogo_tipo == IdCatalogo_tipo
                                 select new fa_catalogo_Info
                                 {
                                     IdCatalogo_tipo = q.IdCatalogo_tipo,
                                     IdCatalogo = q.IdCatalogo,
                                     Nombre = q.Nombre,
                                     Orden = q.Orden,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else

                        Lista = (from q in Context.fa_catalogo
                                 where q.IdCatalogo_tipo == IdCatalogo_tipo
                                 && q.Estado == "A"
                                 select new fa_catalogo_Info
                                 {
                                     IdCatalogo_tipo = q.IdCatalogo_tipo,
                                     IdCatalogo = q.IdCatalogo,
                                     Nombre = q.Nombre,
                                     Orden = q.Orden,
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

        public fa_catalogo_Info get_info( string IdCatalogo)
        {
            try
            {
                fa_catalogo_Info info;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_catalogo Entity = Context.fa_catalogo.Where(q => q.IdCatalogo == IdCatalogo).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new fa_catalogo_Info
                    {
                        IdCatalogo_tipo = Entity.IdCatalogo_tipo,
                        IdCatalogo = Entity.IdCatalogo,
                        Abrebiatura = Entity.Abrebiatura,
                        Nombre = Entity.Nombre,
                        Orden = Entity.Orden,
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


        public bool guardarDB(fa_catalogo_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_catalogo Entity = new fa_catalogo
                    {
                        IdCatalogo_tipo = info.IdCatalogo_tipo,
                        IdCatalogo = info.IdCatalogo,
                        Nombre = info.Nombre,
                        Orden = info.Orden,
                        Abrebiatura = info.Abrebiatura,
                        Estado = info.Estado ="A",

                        IdUsuario = info.IdUsuario
                    };
                    Context.fa_catalogo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(fa_catalogo_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_catalogo Entity = Context.fa_catalogo.Where(q => q.IdCatalogo == info.IdCatalogo).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Nombre = info.Nombre;
                    Entity.Orden = info.Orden;
                    Entity.Abrebiatura = info.Abrebiatura;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.FechaUltMod = DateTime.Now;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(fa_catalogo_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_catalogo Entity = Context.fa_catalogo.Where(q => q.IdCatalogo == info.IdCatalogo).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado="I";

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

        public bool validar_existe_IdCatalogo(string IdCatalogo)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_catalogo
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
