using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.Compras.Base;

namespace Core.Erp.Data.Compras
{
    public class com_estado_cierre_Data
    {
        public List<com_estado_cierre_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<com_estado_cierre_Info> Lista;
                using (Entities_compras Context = new Entities_compras())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.com_estado_cierre
                                 select new com_estado_cierre_Info
                                 {
                                 IdEstado_cierre = q.IdEstado_cierre,
                                 Descripcion = q.Descripcion,
                                 estado = q.estado,

                                     EstadoBool = q.estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.com_estado_cierre
                                 where q.estado == "A"
                                  select new com_estado_cierre_Info
                                  {
                                      IdEstado_cierre = q.IdEstado_cierre,
                                      Descripcion = q.Descripcion,
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

        public com_estado_cierre_Info get_info(string IdEstado_cierre)
        {
            try
            {
                com_estado_cierre_Info info = new com_estado_cierre_Info();
                using (Entities_compras Context = new Entities_compras())
                {
                    com_estado_cierre Entity = Context.com_estado_cierre.Where(q => q.IdEstado_cierre == IdEstado_cierre).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new com_estado_cierre_Info
                    {
                        IdEstado_cierre = Entity.IdEstado_cierre,
                        Descripcion = Entity.Descripcion,
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

        public bool validar_existe_IdEstado(string IdEstado_cierre)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    var lst = from q in Context.com_estado_cierre
                              where q.IdEstado_cierre == IdEstado_cierre
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

        public bool guardarDB(com_estado_cierre_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_estado_cierre Entity = new com_estado_cierre
                    {
                        IdEstado_cierre = info.IdEstado_cierre,
                        Descripcion = info.Descripcion,
                        estado = "A",
                        Fecha_Transac = DateTime.Now
                    };
                    Context.com_estado_cierre.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(com_estado_cierre_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_estado_cierre Entity = Context.com_estado_cierre.Where(q => q.IdEstado_cierre == info.IdEstado_cierre).FirstOrDefault();
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

        public bool anularDB(com_estado_cierre_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_estado_cierre Entity = Context.com_estado_cierre.Where(q => q.IdEstado_cierre == info.IdEstado_cierre).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.estado = "I";
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.FechaHoraAnul = DateTime.Now;
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
