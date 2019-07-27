using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_grupocble_Data
    {
        public List<ct_grupocble_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<ct_grupocble_Info> Lista;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    if (mostrar_anulados == true)
                    Lista = (from q in Context.ct_grupocble
                             select new ct_grupocble_Info
                             {
                                  IdGrupoCble = q.IdGrupoCble,
                                  IdGrupo_Mayor = q.IdGrupo_Mayor,
                                  gc_estado_financiero = q.gc_estado_financiero,
                                  gc_GrupoCble = q.gc_GrupoCble,
                                  gc_Orden = q.gc_Orden,
                                  gc_signo_operacion = q.gc_signo_operacion,
                                  Estado = q.Estado,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.ct_grupocble
                                 where q.Estado == "A"
                                 select new ct_grupocble_Info
                                 {
                                     IdGrupoCble = q.IdGrupoCble,
                                     IdGrupo_Mayor = q.IdGrupo_Mayor,
                                     gc_estado_financiero = q.gc_estado_financiero,
                                     gc_GrupoCble = q.gc_GrupoCble,
                                     gc_Orden = q.gc_Orden,
                                     gc_signo_operacion = q.gc_signo_operacion,
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

        public ct_grupocble_Info get_info(string IdGrupoCble)
        {
            try
            {
                ct_grupocble_Info info = new ct_grupocble_Info();
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_grupocble Entity = Context.ct_grupocble.FirstOrDefault(q => q.IdGrupoCble == IdGrupoCble);
                    if (Entity == null) return null;
                    info = new ct_grupocble_Info
                    {
                        IdGrupoCble = Entity.IdGrupoCble,
                        IdGrupo_Mayor = Entity.IdGrupo_Mayor,
                        gc_estado_financiero = Entity.gc_estado_financiero,
                        gc_GrupoCble = Entity.gc_GrupoCble,
                        gc_Orden = Entity.gc_Orden,
                        gc_signo_operacion = Entity.gc_signo_operacion,
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

        public bool guardarDB(ct_grupocble_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_grupocble Entity = new ct_grupocble
                    {
                        IdGrupoCble = info.IdGrupoCble,
                        IdGrupo_Mayor = info.IdGrupo_Mayor,
                        gc_estado_financiero = info.gc_estado_financiero,
                        gc_GrupoCble = info.gc_GrupoCble,
                        gc_Orden = info.gc_Orden,
                        gc_signo_operacion = info.gc_signo_operacion,
                        Estado = info.Estado = "A"
                    };
                    Context.ct_grupocble.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(ct_grupocble_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_grupocble Entity = Context.ct_grupocble.FirstOrDefault(q => q.IdGrupoCble == info.IdGrupoCble);
                    if (Entity == null)
                        return false;
                    Entity.gc_estado_financiero = info.gc_estado_financiero;
                    Entity.gc_GrupoCble = info.gc_GrupoCble;
                    Entity.gc_Orden = info.gc_Orden;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(ct_grupocble_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_grupocble Entity = Context.ct_grupocble.FirstOrDefault(q =>q.IdGrupoCble == info.IdGrupoCble);
                    if (Entity == null)
                        return false;
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
        
    }
}
