using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_plancta_nivel_Data
    {
        public List<ct_plancta_nivel_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ct_plancta_nivel_Info> Lista;

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ct_plancta_nivel
                                 where q.IdEmpresa == IdEmpresa
                                 select new ct_plancta_nivel_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdNivelCta = q.IdNivelCta,
                                     nv_NumDigitos = q.nv_NumDigitos,
                                     nv_Descripcion = q.nv_Descripcion,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.ct_plancta_nivel
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new ct_plancta_nivel_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdNivelCta = q.IdNivelCta,
                                     nv_NumDigitos = q.nv_NumDigitos,
                                     nv_Descripcion = q.nv_Descripcion,
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

        public ct_plancta_nivel_Info get_info(int IdEmpresa, int IdNivelCta)
        {
            try
            {
                ct_plancta_nivel_Info info = new ct_plancta_nivel_Info();

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_plancta_nivel Entity = Context.ct_plancta_nivel.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdNivelCta == IdNivelCta);
                    if (Entity == null) return null;
                    info = new ct_plancta_nivel_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdNivelCta = Entity.IdNivelCta,
                        nv_NumDigitos = Entity.nv_NumDigitos,
                        nv_Descripcion = Entity.nv_Descripcion,
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

        public bool guardarDB(ct_plancta_nivel_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_plancta_nivel Entity = new ct_plancta_nivel
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdNivelCta = info.IdNivelCta,
                        nv_NumDigitos = info.nv_NumDigitos,
                        nv_Descripcion = info.nv_Descripcion,
                        Estado = info.Estado = "A",

                        IdUsuario = info.IdUsuario,
                        Fecha_Transaccion = DateTime.Now
                    };
                    Context.ct_plancta_nivel.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ct_plancta_nivel_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_plancta_nivel Entity = Context.ct_plancta_nivel.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNivelCta == info.IdNivelCta);
                    if (Entity == null) return false;
                    Entity.nv_NumDigitos = info.nv_NumDigitos;
                    Entity.nv_Descripcion = info.nv_Descripcion;

                    Entity.IdUsuarioUltModi = info.IdUsuarioUltModi;
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

        public bool anularDB(ct_plancta_nivel_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_plancta_nivel Entity = Context.ct_plancta_nivel.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNivelCta == info.IdNivelCta);
                    if (Entity == null) return false;
                    Entity.Estado = "I";

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

        public bool validar_existe_nivel(int IdEmpresa, int IdNivelCta)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var lst = from q in Context.ct_plancta_nivel
                              where q.IdEmpresa == IdEmpresa
                              && q.IdNivelCta == IdNivelCta
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
