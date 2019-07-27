using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_cbtecble_tipo_Data
    {
        public List<ct_cbtecble_tipo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ct_cbtecble_tipo_Info> Lista;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    if (mostrar_anulados == true)
                        Lista = (from q in Context.ct_cbtecble_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 select new ct_cbtecble_tipo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoCbte = q.IdTipoCbte,
                                     CodTipoCbte = q.CodTipoCbte,
                                     tc_TipoCbte = q.tc_TipoCbte,
                                     tc_Estado = q.tc_Estado,

                                     EstadoBool = q.tc_Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.ct_cbtecble_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.tc_Estado == "A"
                                 select new ct_cbtecble_tipo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoCbte = q.IdTipoCbte,
                                     CodTipoCbte = q.CodTipoCbte,
                                     tc_TipoCbte = q.tc_TipoCbte,
                                     tc_Estado = q.tc_Estado,

                                     EstadoBool = q.tc_Estado == "A" ? true : false
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ct_cbtecble_tipo_Info get_info(int IdTipoCbte)
        {
            try
            {
                ct_cbtecble_tipo_Info info = new ct_cbtecble_tipo_Info();
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_cbtecble_tipo Entity = Context.ct_cbtecble_tipo.FirstOrDefault(q => q.IdTipoCbte == IdTipoCbte);
                    if (Entity == null) return null;
                    info = new ct_cbtecble_tipo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoCbte = Entity.IdTipoCbte,
                        CodTipoCbte = Entity.CodTipoCbte,
                        tc_TipoCbte = Entity.tc_TipoCbte,
                        tc_Interno_bool = Entity.tc_Interno == "S" ? true : false,
                        tc_Estado = Entity.tc_Estado,
                        tc_Nemonico = Entity.tc_Nemonico,
                        IdTipoCbte_Anul = Entity.IdTipoCbte_Anul
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(ct_cbtecble_tipo_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_cbtecble_tipo Entity = new ct_cbtecble_tipo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoCbte = info.IdTipoCbte= get_id(info.IdEmpresa),
                        CodTipoCbte = info.CodTipoCbte,
                        tc_TipoCbte = info.tc_TipoCbte,
                        tc_Interno = info.tc_Interno_bool == true ? "S" : "N",
                        tc_Estado = info.tc_Estado = "A",
                        tc_Nemonico = info.tc_Nemonico,
                        IdTipoCbte_Anul = info.IdTipoCbte_Anul
                        
                    };
                    Context.ct_cbtecble_tipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(ct_cbtecble_tipo_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_cbtecble_tipo Entity = Context.ct_cbtecble_tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte == info.IdTipoCbte);
                    if (Entity == null)
                        return false;
                    Entity.CodTipoCbte = info.CodTipoCbte;
                    Entity.tc_TipoCbte = info.tc_TipoCbte;
                    Entity.tc_Nemonico = info.tc_Nemonico;
                    Entity.tc_Interno = info.tc_Interno_bool == true ? "S" : "N";
                    Entity.IdTipoCbte_Anul = info.IdTipoCbte_Anul;



                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(ct_cbtecble_tipo_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_cbtecble_tipo Entity = Context.ct_cbtecble_tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoCbte == info.IdTipoCbte);
                    if (Entity == null)
                        return false;
                    Entity.tc_Estado = info.tc_Estado = "I";

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var lst = from q in Context.ct_cbtecble_tipo
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdTipoCbte) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
