using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_codigo_SRI_Data
    {
        public List<cp_codigo_SRI_Info> get_list(string IdTipoSRI, bool mostrar_anulados)
        {
            try
            {
                List<cp_codigo_SRI_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    if (mostrar_anulados == true)
                        Lista = (from q in Context.cp_codigo_SRI
                                 where q.IdTipoSRI == IdTipoSRI
                                 select new cp_codigo_SRI_Info
                                 {
                                     IdTipoSRI = q.IdTipoSRI,
                                     IdCodigo_SRI = q.IdCodigo_SRI,
                                     codigoSRI = q.codigoSRI,
                                     co_descripcion = q.co_descripcion,
                                     co_codigoBase = q.co_codigoBase,
                                     co_porRetencion = q.co_porRetencion,
                                     co_estado = q.co_estado,

                                     EstadoBool = q.co_estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.cp_codigo_SRI
                                 where q.co_estado == "A"
                                 select new cp_codigo_SRI_Info
                                 {
                                     IdCodigo_SRI = q.IdCodigo_SRI,
                                     codigoSRI = q.codigoSRI,
                                     co_descripcion = q.co_descripcion,
                                     co_codigoBase = q.co_codigoBase,
                                     co_porRetencion = q.co_porRetencion,
                                     co_estado = q.co_estado,
                                     IdTipoSRI = q.IdTipoSRI,

                                     EstadoBool = q.co_estado == "A" ? true : false
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<cp_codigo_SRI_Info> get_list_cod_ret( bool mostrar_anulados, int IdEmpresa)
        {
            try
            {
                List<cp_codigo_SRI_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    if (mostrar_anulados == true)
                        Lista = (from q in Context.vwcp_codigo_SRI
                                 where (q.IdTipoSRI == "COD_RET_FUE" || q.IdTipoSRI == "COD_RET_IVA")
                                 && q.IdEmpresa == IdEmpresa
                                 select new cp_codigo_SRI_Info
                                 {
                                     IdTipoSRI = q.IdTipoSRI,
                                     IdCodigo_SRI = q.IdCodigo_SRI,
                                     codigoSRI = q.codigoSRI,
                                     co_descripcion = q.co_descripcion,
                                     co_codigoBase = q.co_codigoBase,
                                     co_porRetencion = q.co_porRetencion,
                                     co_estado = q.co_estado,
                                     info_codigo_ctacble = new cp_codigo_SRI_x_CtaCble_Info
                                     {
                                         IdCtaCble = q.IdCtaCble,
                                     }

                                 }).ToList();
                    else
                        Lista = (from q in Context.vwcp_codigo_SRI
                                 where q.co_estado == "A"
                                 && (q.IdTipoSRI == "COD_RET_FUE" || q.IdTipoSRI == "COD_RET_IVA")
                                 && q.IdEmpresa == IdEmpresa
                                 select new cp_codigo_SRI_Info
                                 {
                                     IdCodigo_SRI = q.IdCodigo_SRI,
                                     codigoSRI = q.codigoSRI,
                                     co_descripcion = q.co_descripcion,
                                     co_codigoBase = q.co_codigoBase,
                                     co_porRetencion = q.co_porRetencion,
                                     co_estado = q.co_estado,
                                     IdTipoSRI = q.IdTipoSRI,
                                     info_codigo_ctacble = new cp_codigo_SRI_x_CtaCble_Info
                                     {
                                         IdCtaCble = q.IdCtaCble,
                                     }
                                 }).ToList();
                }  
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public cp_codigo_SRI_Info get_info(int IdCodigoSRI)
        {
            try
            {
                cp_codigo_SRI_Info info = new cp_codigo_SRI_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI Entity = Context.cp_codigo_SRI.FirstOrDefault(q => q.IdCodigo_SRI == IdCodigoSRI);
                    if (Entity == null) return null;
                    info = new cp_codigo_SRI_Info
                    {
                        IdCodigo_SRI = Entity.IdCodigo_SRI,
                        codigoSRI = Entity.codigoSRI,
                        co_descripcion = Entity.co_descripcion,
                        co_codigoBase = Entity.co_codigoBase,
                        co_porRetencion = Entity.co_porRetencion,
                        co_estado = Entity.co_estado,
                        IdTipoSRI = Entity.IdTipoSRI,
                        co_f_valides_desde = Entity.co_f_valides_desde,
                        co_f_valides_hasta = Entity.co_f_valides_hasta,                        
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public cp_codigo_SRI_Info get_info(int IdEmpresa, int IdCodigoSRI)
        {
            try
            {
                cp_codigo_SRI_Info info = new cp_codigo_SRI_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI Entity = Context.cp_codigo_SRI.FirstOrDefault(q => q.IdCodigo_SRI == IdCodigoSRI);
                    if (Entity == null) return null;
                    info = new cp_codigo_SRI_Info
                    {
                        IdCodigo_SRI = Entity.IdCodigo_SRI,
                        codigoSRI = Entity.codigoSRI,
                        co_descripcion = Entity.co_descripcion,
                        co_codigoBase = Entity.co_codigoBase,
                        co_porRetencion = Entity.co_porRetencion,
                        co_estado = Entity.co_estado,
                        IdTipoSRI = Entity.IdTipoSRI,
                        co_f_valides_desde = Entity.co_f_valides_desde,
                        co_f_valides_hasta = Entity.co_f_valides_hasta,
                        info_codigo_ctacble = new cp_codigo_SRI_x_CtaCble_Info
                        {
                            IdCtaCble = Context.cp_codigo_SRI_x_CtaCble.Where(q=>q.IdEmpresa == IdEmpresa && q.idCodigo_SRI == IdCodigoSRI).FirstOrDefault() == null ? null : Context.cp_codigo_SRI_x_CtaCble.Where(q => q.IdEmpresa == IdEmpresa && q.idCodigo_SRI == IdCodigoSRI).FirstOrDefault().IdCtaCble
                        }

                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cp_codigo_SRI_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI Entity = new cp_codigo_SRI
                    {
                        IdCodigo_SRI = info.IdCodigo_SRI = get_id(),
                        codigoSRI = info.codigoSRI,
                        co_descripcion = info.co_descripcion,
                        co_codigoBase = info.co_codigoBase,
                        co_porRetencion = info.co_porRetencion,
                        co_estado = info.co_estado = "A",
                        IdTipoSRI = info.IdTipoSRI,
                        co_f_valides_desde = info.co_f_valides_desde.Date,
                        co_f_valides_hasta = info.co_f_valides_hasta.Date
                    };
                    Context.cp_codigo_SRI.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(cp_codigo_SRI_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI Entity = Context.cp_codigo_SRI.FirstOrDefault(q => q.IdCodigo_SRI == info.IdCodigo_SRI);
                    if (Entity == null) return false;
                    Entity.codigoSRI = info.codigoSRI;
                    Entity.co_codigoBase = info.co_codigoBase;
                    Entity.co_descripcion = info.co_descripcion;
                    Entity.co_porRetencion = info.co_porRetencion;
                    
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(cp_codigo_SRI_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_codigo_SRI Entity = Context.cp_codigo_SRI.FirstOrDefault(q => q.IdCodigo_SRI == info.IdCodigo_SRI);
                    if (Entity == null) return false;
                    Entity.co_estado = info.co_estado = "I";

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private int get_id()
        {
            try
            {
                int ID = 1;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var lst = from q in Context.cp_codigo_SRI
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCodigo_SRI) + 1;
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
