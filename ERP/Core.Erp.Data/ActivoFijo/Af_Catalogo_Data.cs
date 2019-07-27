using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.ActivoFijo
{
    public class Af_Catalogo_Data
    {
        public List<Af_Catalogo_Info> get_list(string IdTipoCatalogo, bool mostrar_anulados)
        {
            try
            {
                List<Af_Catalogo_Info> Lista;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.Af_Catalogo
                                 where q.IdTipoCatalogo == IdTipoCatalogo
                                 select new Af_Catalogo_Info
                                 {
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     Descripcion = q.Descripcion,
                                     Estado = q.Estado,
                                     IdCatalogo = q.IdCatalogo,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.Af_Catalogo
                                 where q.IdTipoCatalogo == IdTipoCatalogo
                                 && q.Estado =="A"
                                 select new Af_Catalogo_Info
                                 {
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     Descripcion = q.Descripcion,
                                     Estado = q.Estado,
                                     IdCatalogo = q.IdCatalogo,

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
        public Af_Catalogo_Info get_info(string IdTipoCatalogo, string IdCatalogo)
        {
            try
            {
                Af_Catalogo_Info info = new Af_Catalogo_Info();
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Catalogo Entity = Context.Af_Catalogo.FirstOrDefault(q => q.IdTipoCatalogo == IdTipoCatalogo && q.IdCatalogo == IdCatalogo);
                    if (Entity == null) return null;
                    info = new Af_Catalogo_Info
                    {
                        IdTipoCatalogo = Entity.IdTipoCatalogo,
                        IdCatalogo = Entity.IdCatalogo,
                        Descripcion = Entity.Descripcion,
                        Estado = Entity.Estado,
                        
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(Af_Catalogo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Catalogo Entity = new Af_Catalogo
                    {
                        IdTipoCatalogo = info.IdTipoCatalogo,
                        IdCatalogo = info.IdCatalogo,
                        Descripcion = info.Descripcion,
                        Estado = info.Estado = "A",

                        IdUsuario = info.IdUsuario

                    };
                    Context.Af_Catalogo.Add(Entity);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(Af_Catalogo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_Catalogo Entity = Context.Af_Catalogo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo && q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null) return false;
                    Entity.IdTipoCatalogo = info.IdTipoCatalogo;
                    Entity.Descripcion = info.Descripcion;

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
        public bool anularDB(Af_Catalogo_Info info)
        {
            try
            {
                    using (Entities_activo_fijo Context = new Entities_activo_fijo())
                    {
                        Af_Catalogo Entity = Context.Af_Catalogo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo && q.IdCatalogo == info.IdCatalogo);
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
        public bool validar_existe_IdCatalogo(string IdCatalogo)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    var lst = from q in Context.Af_Catalogo
                              where q.IdCatalogo == IdCatalogo
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
