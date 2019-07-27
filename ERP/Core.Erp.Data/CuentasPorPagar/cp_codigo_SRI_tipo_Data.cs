using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_codigo_SRI_tipo_Data
    {
        public List<cp_codigo_SRI_tipo_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<cp_codigo_SRI_tipo_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    if(mostrar_anulados == true)
                    Lista = (from q in Context.cp_codigo_SRI_tipo
                             select new cp_codigo_SRI_tipo_Info
                             {
                                 IdTipoSRI = q.IdTipoSRI,
                                 descripcion = q.descripcion,
                                 estado = q.estado,

                                 EstadoBool = q.estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.cp_codigo_SRI_tipo
                                 where q.estado == "A"
                                 select new cp_codigo_SRI_tipo_Info
                                 {
                                     IdTipoSRI = q.IdTipoSRI,
                                     descripcion = q.descripcion,
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

        public cp_codigo_SRI_tipo_Info get_info(string IdTipoSRI)
        {
            try
            {
                cp_codigo_SRI_tipo_Info info = new cp_codigo_SRI_tipo_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI_tipo Entity = Context.cp_codigo_SRI_tipo.FirstOrDefault(q => q.IdTipoSRI == IdTipoSRI);
                    if (Entity == null) return null;
                    info = new cp_codigo_SRI_tipo_Info
                    {
                        IdTipoSRI = Entity.IdTipoSRI,
                        descripcion = Entity.descripcion,
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

        public bool guardarDB(cp_codigo_SRI_tipo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI_tipo Entity = new cp_codigo_SRI_tipo
                    {
                        IdTipoSRI = info.IdTipoSRI,
                        descripcion = info.descripcion,
                        estado = info.estado="A"
                    };
                    Context.cp_codigo_SRI_tipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool  validar_existe_codigo_tipo(string IdTipoSRI)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var lst = from q in Context.cp_codigo_SRI_tipo
                              where q.IdTipoSRI == IdTipoSRI
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

        public bool modificarDB(cp_codigo_SRI_tipo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI_tipo Entity = Context.cp_codigo_SRI_tipo.FirstOrDefault(q => q.IdTipoSRI == info.IdTipoSRI);
                    if (Entity == null) return false;
                    Entity.descripcion = info.descripcion;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(cp_codigo_SRI_tipo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI_tipo Entity = Context.cp_codigo_SRI_tipo.FirstOrDefault(q => q.IdTipoSRI == info.IdTipoSRI);
                    if (Entity == null) return false;
                    Entity.estado = "I";
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
