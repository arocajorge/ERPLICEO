using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorCobrar
{
    public class cxc_Catalogo_Data
    {
        public List<cxc_Catalogo_Info> get_list(string IdCatalogo_tipo, bool mostrar_anulados)
        {
            try
            {
                List<cxc_Catalogo_Info> Lista;
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.cxc_Catalogo
                                 where q.IdCatalogo_tipo == IdCatalogo_tipo
                                 select new cxc_Catalogo_Info
                                 {
                                     IdCatalogo_tipo = q.IdCatalogo_tipo,
                                     IdCatalogo = q.IdCatalogo,
                                     Estado = q.Estado,
                                     Orden = q.Orden,
                                     Nombre = q.Nombre,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.cxc_Catalogo
                                 where q.IdCatalogo_tipo == IdCatalogo_tipo
                                 && q.Estado == "A"
                                 select new cxc_Catalogo_Info
                                 {
                                     IdCatalogo_tipo = q.IdCatalogo_tipo,
                                     IdCatalogo = q.IdCatalogo,
                                     Estado = q.Estado,
                                     Orden = q.Orden,
                                     Nombre = q.Nombre,

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

        public cxc_Catalogo_Info get_info(string IdCatalogo_tipo , string IdCatalogo)
        {
            try
            {
                cxc_Catalogo_Info info = new cxc_Catalogo_Info();
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_Catalogo Entity = Context.cxc_Catalogo.FirstOrDefault(q => q.IdCatalogo_tipo == IdCatalogo_tipo && q.IdCatalogo == IdCatalogo);
                    if (Entity == null) return null;
                    info = new cxc_Catalogo_Info
                    {
                        IdCatalogo_tipo = Entity.IdCatalogo_tipo,
                        IdCatalogo = Entity.IdCatalogo,
                        Estado = Entity.Estado,
                        Nombre = Entity.Nombre,
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

        public bool guardarDB(cxc_Catalogo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_Catalogo Entity = new cxc_Catalogo
                    {
                        IdCatalogo_tipo = info.IdCatalogo_tipo,
                        IdCatalogo = info.IdCatalogo,
                        Estado = info.Estado="A",
                        Orden = info.Orden,
                        Nombre = info.Nombre,
                        IdUsuario = info.IdUsuario
                        
                    };
                    Context.cxc_Catalogo.Add(Entity);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(cxc_Catalogo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_Catalogo Entity = Context.cxc_Catalogo.FirstOrDefault(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo && q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null) return false;
                    Entity.IdCatalogo_tipo = info.IdCatalogo_tipo;
                    Entity.Nombre = info.Nombre;
                    Entity.Orden = info.Orden;

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

        public bool anularDB(cxc_Catalogo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_Catalogo Entity = Context.cxc_Catalogo.FirstOrDefault(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo && q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null) return false;
                    Entity.Estado = info.Estado="I";
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
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    var lst = from q in Context.cxc_Catalogo
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
