using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
  public  class ro_catalogo_Data
    {
        public List<ro_catalogo_Info> get_list( bool mostrar_anulados)
        {
            try
            {
                List<ro_catalogo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ro_catalogo
                                 select new ro_catalogo_Info
                                 {
                                     CodCatalogo = q.CodCatalogo,
                                     IdCatalogo = q.IdCatalogo,
                                     ca_descripcion = q.ca_descripcion,
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_estado=q.ca_estado,
                                     ca_orden=q.ca_orden,

                                     EstadoBool = q.ca_estado == "A" ? true : false

                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_catalogo
                                 where q.ca_estado == "A"
                                 select new ro_catalogo_Info
                                 {
                                     CodCatalogo = q.CodCatalogo,
                                     IdCatalogo = q.IdCatalogo,
                                     ca_descripcion = q.ca_descripcion,
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_estado = q.ca_estado,
                                     ca_orden = q.ca_orden,

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
        public List<ro_catalogo_Info> get_list_x_tipo(int IdTipoCatalogo)
        {
            try
            {
                List<ro_catalogo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        Lista = (from q in Context.ro_catalogo
                                 where q.IdTipoCatalogo==IdTipoCatalogo
                                 select new ro_catalogo_Info
                                 {
                                     CodCatalogo = q.CodCatalogo,
                                     IdCatalogo = q.IdCatalogo,
                                     ca_descripcion = q.ca_descripcion,
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_estado = q.ca_estado,
                                     ca_orden = q.ca_orden,

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

        public List<ro_catalogo_Info> get_list_x_tipo(int IdTipoCatalogo, bool MostrarAnulados)
        {
            try
            {
                List<ro_catalogo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (MostrarAnulados == true)
                    {
                        Lista = (from q in Context.ro_catalogo
                                 where q.IdTipoCatalogo == IdTipoCatalogo
                                 select new ro_catalogo_Info
                                 {
                                     CodCatalogo = q.CodCatalogo,
                                     IdCatalogo = q.IdCatalogo,
                                     ca_descripcion = q.ca_descripcion,
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_estado = q.ca_estado,
                                     ca_orden = q.ca_orden,

                                     EstadoBool = q.ca_estado == "A" ? true : false
                                 }).ToList();
                    }
                    else{
                        Lista = (from q in Context.ro_catalogo
                                 where q.IdTipoCatalogo == IdTipoCatalogo
                                 && q.ca_estado == "A"
                                 select new ro_catalogo_Info
                                 {
                                     CodCatalogo = q.CodCatalogo,
                                     IdCatalogo = q.IdCatalogo,
                                     ca_descripcion = q.ca_descripcion,
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_estado = q.ca_estado,
                                     ca_orden = q.ca_orden,

                                     EstadoBool = q.ca_estado == "A" ? true : false
                                 }).ToList();
                    }
                    

                }

                return Lista;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_catalogo_Info get_info(int IdTipoCatalogo, int IdCatalogo)
        {
            try
            {
                ro_catalogo_Info info = new ro_catalogo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_catalogo Entity = Context.ro_catalogo.FirstOrDefault(q => q.IdTipoCatalogo == IdTipoCatalogo && q.IdCatalogo == IdCatalogo);
                    if (Entity == null) return null;

                    info = new ro_catalogo_Info
                    {
                        CodCatalogo = Entity.CodCatalogo,
                        IdCatalogo = Entity.IdCatalogo,
                        ca_descripcion = Entity.ca_descripcion,
                        IdTipoCatalogo = Entity.IdTipoCatalogo,
                        ca_estado = Entity.ca_estado,
                        ca_orden = Entity.ca_orden
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_catalogo_Info get_info(int IdEmpresa, string CodCatalogo)
        {
            try
            {
                ro_catalogo_Info info = new ro_catalogo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_catalogo Entity = Context.ro_catalogo.FirstOrDefault(q => q.CodCatalogo == CodCatalogo);
                    if (Entity == null) return null;

                    info = new ro_catalogo_Info
                    {
                        CodCatalogo = Entity.CodCatalogo,
                        IdCatalogo = Entity.IdCatalogo,
                        ca_descripcion = Entity.ca_descripcion,
                        IdTipoCatalogo = Entity.IdTipoCatalogo,
                        ca_estado = Entity.ca_estado,
                        ca_orden = Entity.ca_orden
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id(int IdTipoCatalogo)
        {
            try
            {
                int ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_catalogo
                              where q.IdTipoCatalogo == IdTipoCatalogo
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCatalogo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_catalogo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_catalogo Entity = new ro_catalogo();
                    {
                    Entity.CodCatalogo = info.CodCatalogo;
                    Entity.IdCatalogo = get_id(info.IdTipoCatalogo);
                    Entity.ca_descripcion = info.ca_descripcion;
                    Entity.IdTipoCatalogo = info.IdTipoCatalogo;
                    Entity.ca_estado = "A";
                    Entity.ca_orden = info.ca_orden;
                    Entity.Fecha_Transac = info.Fecha_Transac;
                    Entity.IdUsuario = info.IdUsuario;
                    };
                    Context.ro_catalogo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_catalogo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_catalogo Entity = Context.ro_catalogo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo && q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null)
                        return false;
                    Entity.ca_descripcion = info.ca_descripcion;
                    Entity.Fecha_UltMod = info.Fecha_UltMod;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_catalogo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_catalogo Entity = Context.ro_catalogo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo && q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null)
                        return false;
                    Entity.ca_estado  = "I";
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool si_existe_codigo(string CodCatalogo)
        {
            try
            {

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_catalogo
                              where q.CodCatalogo == CodCatalogo
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

