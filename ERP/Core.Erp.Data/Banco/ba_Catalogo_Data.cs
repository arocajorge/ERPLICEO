using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
   public class ba_Catalogo_Data
    {
        public List<ba_Catalogo_Info> get_list(string IdTipoCatalogo, bool mostrar_anulados)
        {
            try
            {
                List<ba_Catalogo_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ba_Catalogo
                                 where q.IdTipoCatalogo == IdTipoCatalogo
                                 select new ba_Catalogo_Info
                                 {
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_descripcion =  q.ca_descripcion,
                                     ca_estado =q.ca_estado,
                                     IdCatalogo = q.IdCatalogo,

                                     EstadoBool = q.ca_estado == "A" ? true : false

                                 }).ToList();
                    else
                        Lista = (from q in Context.ba_Catalogo
                                 where q.IdTipoCatalogo == IdTipoCatalogo
                                 && q.ca_estado == "A"
                                 select new ba_Catalogo_Info
                                 {
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_descripcion = q.ca_descripcion,
                                     ca_estado = q.ca_estado,
                                     IdCatalogo = q.IdCatalogo,

                                     EstadoBool = q.ca_estado == "A" ? true : false

                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ba_Catalogo_Info get_info(string IdTipoCatalogo, string IdCatalogo)
        {
            try
            {
                ba_Catalogo_Info info = new ba_Catalogo_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Catalogo Entity = Context.ba_Catalogo.FirstOrDefault(q => q.IdTipoCatalogo == IdTipoCatalogo && q.IdCatalogo == IdCatalogo);
                    if (Entity == null) return null;
                    info = new ba_Catalogo_Info
                    {
                        IdTipoCatalogo = Entity.IdTipoCatalogo,
                        IdCatalogo = Entity.IdCatalogo,
                        ca_descripcion = Entity.ca_descripcion,
                        ca_estado = Entity.ca_estado
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ba_Catalogo_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Catalogo Entity = new ba_Catalogo
                    {
                        IdTipoCatalogo = info.IdTipoCatalogo,
                        IdCatalogo = info.IdCatalogo,
                        ca_descripcion = info.ca_descripcion,
                        ca_estado = info.ca_estado="A"

                    };
                    Context.ba_Catalogo.Add(Entity);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ba_Catalogo_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Catalogo Entity = Context.ba_Catalogo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo && q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null) return false;
                    Entity.IdTipoCatalogo = info.IdTipoCatalogo;
                    Entity.ca_descripcion = info.ca_descripcion;

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
        public bool anularDB(ba_Catalogo_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_Catalogo Entity = Context.ba_Catalogo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo && q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null) return false;
                    Entity.ca_estado = info.ca_estado="I";

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
                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_Catalogo
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
