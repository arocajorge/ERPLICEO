using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_CentroCostoNivel_Data
    {
        public List<ct_CentroCostoNivel_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ct_CentroCostoNivel_Info> Lista;

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ct_CentroCostoNivel
                                 where q.IdEmpresa == IdEmpresa
                                 select new ct_CentroCostoNivel_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdNivel = q.IdNivel,
                                     nv_NumDigitos = q.nv_NumDigitos,
                                     nv_Descripcion = q.nv_Descripcion,
                                     Estado = q.Estado
                                 }).ToList();
                    else
                        Lista = (from q in Context.ct_CentroCostoNivel
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == true
                                 select new ct_CentroCostoNivel_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdNivel = q.IdNivel,
                                     nv_NumDigitos = q.nv_NumDigitos,
                                     nv_Descripcion = q.nv_Descripcion,
                                     Estado = q.Estado
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ct_CentroCostoNivel_Info get_info(int IdEmpresa, int IdNivel)
        {
            try
            {
                ct_CentroCostoNivel_Info info = new ct_CentroCostoNivel_Info();

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_CentroCostoNivel Entity = Context.ct_CentroCostoNivel.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdNivel == IdNivel);
                    if (Entity == null) return null;
                    info = new ct_CentroCostoNivel_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdNivel = Entity.IdNivel,
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

        public bool guardarDB(ct_CentroCostoNivel_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_CentroCostoNivel Entity = new ct_CentroCostoNivel
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdNivel = info.IdNivel,
                        nv_NumDigitos = info.nv_NumDigitos,
                        nv_Descripcion = info.nv_Descripcion,
                        Estado = info.Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.ct_CentroCostoNivel.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ct_CentroCostoNivel_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_CentroCostoNivel Entity = Context.ct_CentroCostoNivel.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNivel == info.IdNivel);
                    if (Entity == null) return false;
                    Entity.nv_NumDigitos = info.nv_NumDigitos;
                    Entity.nv_Descripcion = info.nv_Descripcion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ct_CentroCostoNivel_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_CentroCostoNivel Entity = Context.ct_CentroCostoNivel.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNivel == info.IdNivel);
                    if (Entity == null) return false;
                    Entity.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_nivel(int IdEmpresa, int IdNivel)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var lst = from q in Context.ct_CentroCostoNivel
                              where q.IdEmpresa == IdEmpresa
                              && q.IdNivel == IdNivel
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
