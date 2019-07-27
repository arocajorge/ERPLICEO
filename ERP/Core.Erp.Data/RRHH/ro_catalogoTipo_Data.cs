using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;

namespace Core.Erp.Data.RRHH
{
   public class ro_catalogoTipo_Data
    {
        public List<ro_catalogoTipo_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<ro_catalogoTipo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if(mostrar_anulados)
                        Lista = (from q in Context.ro_catalogoTipo
                                 select new ro_catalogoTipo_Info
                                 {
                                     Codigo = q.Codigo,
                                     tc_Descripcion = q.tc_Descripcion,
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_estado=q.ca_estado,

                                     EstadoBool = q.ca_estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_catalogoTipo
                                 where q.ca_estado=="A"
                                 select new ro_catalogoTipo_Info
                                 {
                                     Codigo = q.Codigo,
                                     tc_Descripcion = q.tc_Descripcion,
                                     IdTipoCatalogo = q.IdTipoCatalogo,
                                     ca_estado = q.ca_estado,

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
        public ro_catalogoTipo_Info get_info(int IdTipoCatalogo)
        {
            try
            {
                ro_catalogoTipo_Info info = new ro_catalogoTipo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_catalogoTipo Entity = Context.ro_catalogoTipo.FirstOrDefault(q => q.IdTipoCatalogo == IdTipoCatalogo );
                    if (Entity == null) return null;

                    info = new ro_catalogoTipo_Info
                    {
                        Codigo = Entity.Codigo,
                        tc_Descripcion = Entity.tc_Descripcion,
                        IdTipoCatalogo = Entity.IdTipoCatalogo,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id()
        {
            try
            {
                int ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_catalogoTipo
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdTipoCatalogo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_catalogoTipo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_catalogoTipo Entity = new ro_catalogoTipo();
                    {
                        Entity.Codigo = info.Codigo;
                        Entity.tc_Descripcion = info.tc_Descripcion;
                        Entity.IdTipoCatalogo = get_id();
                        Entity.ca_estado = "A";
                        Entity.Fecha_Transac = DateTime.Now;
                        Entity.IdUsuario = info.IdUsuario;
                    };
                    Context.ro_catalogoTipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_catalogoTipo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_catalogoTipo Entity = Context.ro_catalogoTipo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo );
                    if (Entity == null)
                        return false;
                    Entity.Codigo = info.Codigo;
                    Entity.tc_Descripcion = info.tc_Descripcion;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = info.Fecha_UltMod;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_catalogoTipo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_catalogoTipo Entity = Context.ro_catalogoTipo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo);
                    if (Entity == null)
                        return false;
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu;
                    Entity.ca_estado = "I";
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
