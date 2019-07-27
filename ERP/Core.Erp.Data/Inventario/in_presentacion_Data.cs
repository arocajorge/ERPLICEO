using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_presentacion_Data
    {
        public List<in_presentacion_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<in_presentacion_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.in_presentacion
                                 where q.IdEmpresa == IdEmpresa
                                 select new in_presentacion_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdPresentacion = q.IdPresentacion,
                                     nom_presentacion = q.nom_presentacion,
                                     estado  = q.estado,

                                     EstadoBool = q.estado == "A" ? true : false
                                 }).ToList();

                    else Lista = (from q in Context.in_presentacion
                                  where q.IdEmpresa == IdEmpresa
                                  && q.estado == "A"
                                  select new in_presentacion_Info
                                  {
                                      IdEmpresa = q.IdEmpresa,
                                      IdPresentacion = q.IdPresentacion,
                                      nom_presentacion = q.nom_presentacion,
                                      estado = q.estado,

                                      EstadoBool = q.estado == "A" ? true : false
                                  }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public in_presentacion_Info get_info(int IdEmpresa, string IdPresentacion)
        {
            try
            {
                in_presentacion_Info info = new in_presentacion_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_presentacion Entity = Context.in_presentacion.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdPresentacion == IdPresentacion);
                    if (Entity == null) return null;
                    info = new in_presentacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPresentacion = Entity.IdPresentacion,
                        nom_presentacion = Entity.nom_presentacion,
                        estado = Entity.estado
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardarDB(in_presentacion_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_presentacion Entity = new in_presentacion
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPresentacion = info.IdPresentacion,
                        nom_presentacion = info.nom_presentacion,
                        estado = info.estado="A"
                    };
                    Context.in_presentacion.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(in_presentacion_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_presentacion Entity = Context.in_presentacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPresentacion == info.IdPresentacion);
                    if (Entity == null) return false;
                    Entity.nom_presentacion = info.nom_presentacion;
                    
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool anularDB(in_presentacion_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_presentacion Entity = Context.in_presentacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPresentacion == info.IdPresentacion);
                    if (Entity == null) return false;
                    Entity.estado = info.estado="I";
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool validar_existe_IdPresentacion(string IdPresentacion)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_presentacion
                              where IdPresentacion == q.IdPresentacion
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
