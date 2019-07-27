using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
   public class ro_tipo_gastos_personales_Data
    {
        public List<ro_tipo_gastos_personales_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<ro_tipo_gastos_personales_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if(mostrar_anulados)
                    
                        Lista = (from q in Context.ro_tipo_gastos_personales
                                 select new ro_tipo_gastos_personales_Info
                                 {
                                     IdTipoGasto = q.IdTipoGasto,
                                     nom_tipo_gasto = q.nom_tipo_gasto,
                                     estado = q.estado,
                                     EstadoBool = q.estado == "A" ? true : false

                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_tipo_gastos_personales
                                 where q.estado=="A"
                                 select new ro_tipo_gastos_personales_Info
                                 {
                                     IdTipoGasto = q.IdTipoGasto,
                                     nom_tipo_gasto = q.nom_tipo_gasto,
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

        public ro_tipo_gastos_personales_Info get_info(string IdTipoGasto)
        {
            try
            {
                ro_tipo_gastos_personales_Info info = new ro_tipo_gastos_personales_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tipo_gastos_personales Entity = Context.ro_tipo_gastos_personales.FirstOrDefault(q => q.IdTipoGasto == IdTipoGasto);
                    if (Entity == null) return null;

                    info = new ro_tipo_gastos_personales_Info
                    {
                        IdTipoGasto = Entity.IdTipoGasto,
                        nom_tipo_gasto = Entity.nom_tipo_gasto,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

    
        public bool guardarDB(ro_tipo_gastos_personales_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tipo_gastos_personales Entity = new ro_tipo_gastos_personales
                    {
                        IdTipoGasto = info.IdTipoGasto,
                        nom_tipo_gasto = info.nom_tipo_gasto,
                        estado=info.estado="A",
                        Fecha_Transac=DateTime.Now,
                        IdUsuario=info.IdUsuario
                    };
                    Context.ro_tipo_gastos_personales.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_tipo_gastos_personales_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tipo_gastos_personales Entity = Context.ro_tipo_gastos_personales.FirstOrDefault(q => q.IdTipoGasto == info.IdTipoGasto);
                    if (Entity == null)
                        return false;
                    Entity.nom_tipo_gasto = info.nom_tipo_gasto;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_tipo_gastos_personales_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_tipo_gastos_personales Entity = Context.ro_tipo_gastos_personales.FirstOrDefault(q => q.IdTipoGasto == info.IdTipoGasto);
                    if (Entity == null)
                        return false;
                    Entity.estado = info.estado = "I";
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
