using Core.Erp.Info.Contabilidad;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_plancta_Data
    {

        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool MostrarCtaPadre)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ct_plancta_Info> Lista = new List<ct_plancta_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter, MostrarCtaPadre);
            return Lista;
        }
        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool MostrarCtaMovimiento, string cta_padre)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ct_plancta_Info> Lista = new List<ct_plancta_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter, MostrarCtaMovimiento, cta_padre);
            return Lista;
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            //La variable args del devexpress ya trae el ID seleccionado en la propiedad Value, se pasa el IdEmpresa porque es un filtro que no tiene
            return get_info(IdEmpresa, args.Value == null ? "" : args.Value.ToString());
        }
        public List<ct_plancta_Info> get_list(int IdEmpresa, int skip, int take, string filter, bool MostrarCtaPadre)
        {
            try
            {
                List<ct_plancta_Info> Lista = new List<ct_plancta_Info>();

                Entities_contabilidad context_g = new Entities_contabilidad();

                {
                    List<ct_plancta> lstg;
                    if(!MostrarCtaPadre)
                        lstg = context_g.ct_plancta.Include("ct_plancta2").Where(q => q.IdEmpresa == IdEmpresa && q.pc_EsMovimiento == "S" && q.pc_Estado == "A" && (q.IdCtaCble + " " + q.pc_Cuenta).Contains(filter)).OrderBy(q => q.IdCtaCble).Skip(skip).Take(take).ToList();
                    else
                        lstg = context_g.ct_plancta.Include("ct_plancta2").Where(q => q.IdEmpresa == IdEmpresa && q.pc_Estado == "A" && (q.IdCtaCble +" "+ q.pc_Cuenta).Contains(filter)).OrderBy(q => q.IdCtaCble).Skip(skip).Take(take).ToList();
                    foreach (var q in lstg)
                        {
                            Lista.Add(new ct_plancta_Info
                            {
                                IdCtaCble = q.IdCtaCble,
                                pc_Cuenta = q.pc_Cuenta,
                                IdCtaCblePadre = q.IdCtaCblePadre,
                                pc_Cuenta_padre = q.ct_plancta2 == null ? null : q.ct_plancta2.pc_Cuenta
                            });
                        }
                }

                context_g.Dispose();
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ct_plancta_Info> get_list(int IdEmpresa, int skip, int take, string filter, bool MostrarCtaMovimiento, string cta_padre)
        {
            try
            {
                List<ct_plancta_Info> Lista = new List<ct_plancta_Info>();

                Entities_contabilidad context_g = new Entities_contabilidad();

                {
                    List<ct_plancta> lstg;
                    if (!MostrarCtaMovimiento)
                        lstg = context_g.ct_plancta.Where(q => q.IdEmpresa == IdEmpresa && q.pc_EsMovimiento == "S" && q.pc_Estado == "A" && (q.IdCtaCble + " " + q.pc_Cuenta).Contains(filter)&& q.IdCtaCble.Contains(cta_padre)).OrderBy(q => q.IdCtaCble).Skip(skip).Take(take).ToList();
                    else
                        lstg = context_g.ct_plancta.Where(q => q.IdEmpresa == IdEmpresa && q.pc_Estado == "A" && (q.IdCtaCble + " " + q.pc_Cuenta).Contains(filter) && q.IdCtaCble.Contains(cta_padre)).OrderBy(q => q.IdCtaCble).Skip(skip).Take(take).ToList();
                    foreach (var q in lstg)
                    {
                        Lista.Add(new ct_plancta_Info
                        {
                            IdCtaCble = q.IdCtaCble,
                            pc_Cuenta = q.pc_Cuenta
                        });
                    }
                }

                context_g.Dispose();
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ct_plancta_Info> get_list(int IdEmpresa, bool mostrar_anulados, bool mostrar_solo_cuentas_movimiento)
        {
            try
            {
                List<ct_plancta_Info> Lista;

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    if (mostrar_anulados && mostrar_solo_cuentas_movimiento)
                        Lista = (from q in Context.ct_plancta
                                 join padre in Context.ct_plancta
                                 on new { q.IdEmpresa, q.IdCtaCblePadre } equals new { padre.IdEmpresa, IdCtaCblePadre = padre.IdCtaCble } into temp_padre
                                 from padre in temp_padre.DefaultIfEmpty()
                                 where q.pc_EsMovimiento == "S"
                                 && q.IdEmpresa == IdEmpresa
                                 orderby q.IdCtaCble
                                 select new ct_plancta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCtaCble = q.IdCtaCble,
                                     pc_Cuenta = q.pc_Cuenta,
                                     IdCtaCblePadre = q.IdCtaCblePadre,
                                     pc_Estado = q.pc_Estado,
                                     pc_EsMovimiento = q.pc_EsMovimiento,
                                     IdGrupoCble = q.IdGrupoCble,
                                     pc_Cuenta_padre = padre.pc_Cuenta,
                                     IdNivelCta = q.IdNivelCta,

                                     EstadoBool = q.pc_Estado == "A" ? true : false
                                 }).ToList();
                    else
                        if (!mostrar_anulados && mostrar_solo_cuentas_movimiento)
                        Lista = (from q in Context.ct_plancta
                                 join padre in Context.ct_plancta
                                 on new { q.IdEmpresa, q.IdCtaCblePadre } equals new { padre.IdEmpresa, IdCtaCblePadre = padre.IdCtaCble } into temp_padre
                                 from padre in temp_padre.DefaultIfEmpty()
                                 where q.pc_EsMovimiento == "S"
                                 && q.pc_Estado == "A"
                                 && q.IdEmpresa == IdEmpresa
                                 orderby q.IdCtaCble
                                 select new ct_plancta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCtaCble = q.IdCtaCble,
                                     pc_Cuenta = q.pc_Cuenta,
                                     IdCtaCblePadre = q.IdCtaCblePadre,
                                     pc_Estado = q.pc_Estado,
                                     pc_EsMovimiento = q.pc_EsMovimiento,
                                     IdGrupoCble = q.IdGrupoCble,
                                     pc_Cuenta_padre = padre.pc_Cuenta,
                                     IdNivelCta = q.IdNivelCta,

                                     EstadoBool = q.pc_Estado == "A" ? true : false
                                 }).ToList();
                    else
                        if (mostrar_anulados && !mostrar_solo_cuentas_movimiento)
                        Lista = (from q in Context.ct_plancta
                                 join padre in Context.ct_plancta
                             on new { q.IdEmpresa, q.IdCtaCblePadre } equals new { padre.IdEmpresa, IdCtaCblePadre = padre.IdCtaCble } into temp_padre
                                 from padre in temp_padre.DefaultIfEmpty()
                                 where q.IdEmpresa == IdEmpresa
                                 orderby q.IdCtaCble
                                 select new ct_plancta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCtaCble = q.IdCtaCble,
                                     pc_Cuenta = q.pc_Cuenta,
                                     IdCtaCblePadre = q.IdCtaCblePadre,
                                     pc_Estado = q.pc_Estado,
                                     pc_EsMovimiento = q.pc_EsMovimiento,
                                     IdGrupoCble = q.IdGrupoCble,
                                     IdNivelCta = q.IdNivelCta,
                                     pc_Cuenta_padre = padre.pc_Cuenta,

                                     EstadoBool = q.pc_Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.ct_plancta
                                 join padre in Context.ct_plancta
                             on new { q.IdEmpresa, q.IdCtaCblePadre } equals new { padre.IdEmpresa, IdCtaCblePadre = padre.IdCtaCble } into temp_padre
                                 from padre in temp_padre.DefaultIfEmpty()
                                 where q.pc_Estado == "A"
                                 && q.IdEmpresa == IdEmpresa
                                 orderby q.IdCtaCble
                                 select new ct_plancta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCtaCble = q.IdCtaCble,
                                     pc_Cuenta = q.pc_Cuenta,
                                     IdCtaCblePadre = q.IdCtaCblePadre,
                                     pc_Estado = q.pc_Estado,
                                     pc_EsMovimiento = q.pc_EsMovimiento,
                                     IdGrupoCble = q.IdGrupoCble,
                                     IdNivelCta = q.IdNivelCta,
                                     pc_Cuenta_padre = padre.pc_Cuenta,

                                     EstadoBool = q.pc_Estado == "A" ? true : false
                                 }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ct_plancta_Info get_info(int IdEmpresa, string IdCtaCble)
        {
            try
            {
                ct_plancta_Info info = new ct_plancta_Info();

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_plancta Entity = Context.ct_plancta.Include("ct_plancta2").FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCtaCble == IdCtaCble);
                    if (Entity == null) return null;
                    info = new ct_plancta_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCtaCble = Entity.IdCtaCble,
                        pc_Cuenta = Entity.pc_Cuenta,
                        IdCtaCblePadre = Entity.IdCtaCblePadre,
                        pc_Cuenta_padre = Entity.ct_plancta2.pc_Cuenta,
                        pc_Naturaleza = Entity.pc_Naturaleza,
                        IdNivelCta = Entity.IdNivelCta,
                        IdGrupoCble = Entity.IdGrupoCble,
                        pc_Estado = Entity.pc_Estado,
                        pc_EsMovimiento = Entity.pc_EsMovimiento,
                        pc_clave_corta = Entity.pc_clave_corta,
                        pc_EsMovimiento_bool = Entity.pc_EsMovimiento == "S" ? true : false,
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(ct_plancta_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var plancta = Context.ct_plancta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCtaCble == info.IdCtaCble).FirstOrDefault();
                    if (plancta != null)
                        return false;

                    ct_plancta Entity = new ct_plancta
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCtaCble = info.IdCtaCble,
                        IdCtaCblePadre = info.IdCtaCblePadre,
                        pc_Cuenta = info.pc_Cuenta,
                        pc_clave_corta = info.pc_clave_corta,
                        IdNivelCta = info.IdNivelCta,
                        IdGrupoCble = info.IdGrupoCble,
                        pc_Estado = info.pc_Estado = "A",
                        pc_Naturaleza = info.pc_Naturaleza,
                        pc_EsMovimiento = info.pc_EsMovimiento_bool == true ? "S" : "N"
                    };
                    Context.ct_plancta.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ct_plancta_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_plancta Entity = Context.ct_plancta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCtaCble == info.IdCtaCble);
                    if (Entity == null) return false;
                    Entity.pc_Cuenta = info.pc_Cuenta;
                    Entity.pc_clave_corta = info.pc_clave_corta;
                    Entity.pc_EsMovimiento = info.pc_EsMovimiento_bool == true ? "S" : "N";
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ct_plancta_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_plancta Entity = Context.ct_plancta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCtaCble == info.IdCtaCble);
                    if (Entity == null) return false;
                    Entity.pc_Estado = info.pc_Estado = "I";
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ct_plancta_Info get_info_nuevo(int IdEmpresa, string IdCtaCble_padre)
        {
            try
            {
                ct_plancta_Info info = new ct_plancta_Info();
                string ID = IdCtaCble_padre;

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_plancta Entity_padre = Context.ct_plancta.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCtaCble == IdCtaCble_padre);
                    if (Entity_padre == null) return info;
                    int IdNivel_hijo = Entity_padre.IdNivelCta + 1;
                    ct_plancta_nivel Entity_nivel_hijo = Context.ct_plancta_nivel.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdNivelCta == IdNivel_hijo);
                    if (Entity_nivel_hijo == null) return info;

                    var lst = from q in Context.ct_plancta
                              where q.IdCtaCblePadre == IdCtaCble_padre
                              && q.IdEmpresa == IdEmpresa
                              select q;

                    string relleno = "";
                    string digitos = relleno.PadLeft(Entity_nivel_hijo.nv_NumDigitos, '0');

                    if (lst.Count() > 0)
                    {
                        ID += (Convert.ToInt32(lst.Max(q => q.IdCtaCble.Substring(q.IdCtaCble.Length - Entity_nivel_hijo.nv_NumDigitos, Entity_nivel_hijo.nv_NumDigitos)))+1).ToString(digitos);
                    }
                    else
                        ID += Convert.ToInt32(1).ToString(digitos);

                    info = new ct_plancta_Info
                    {
                        IdCtaCble = ID,
                        pc_Cuenta = Entity_padre.pc_Cuenta,
                        IdGrupoCble = Entity_padre.IdGrupoCble,
                        IdNivelCta = Entity_nivel_hijo.IdNivelCta,
                        pc_Naturaleza = Entity_padre.pc_Naturaleza
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool validar_existe_id(int IdEmpresa, string IdCtaCble)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var lst = from q in Context.ct_plancta
                              where q.IdEmpresa == IdEmpresa
                              && q.IdCtaCble == IdCtaCble
                              select q;

                    if (lst.Count() == 0)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public double get_saldo_anterior(int IdEmpresa, string IdCtaCble, DateTime Fecha_corte)
        {
            try
            {
                double saldo_anterior = 0;

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var lst = from d in Context.ct_cbtecble_det
                              join c in Context.ct_cbtecble
                              on new { d.IdEmpresa, d.IdTipoCbte, d.IdCbteCble } equals new { c.IdEmpresa, c.IdTipoCbte, c.IdCbteCble }
                              where d.IdEmpresa == IdEmpresa
                              && d.IdCtaCble == IdCtaCble
                              && c.cb_Fecha < Fecha_corte
                              group d by new
                              {
                                  d.IdEmpresa,
                                  d.IdCtaCble
                              } into grouping
                              select grouping.Sum(q => q.dc_Valor);
                    if (lst.Count() > 0)
                        saldo_anterior = lst.First();
                }

                return saldo_anterior;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string get_CtaCble_acreedora(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                string IdCtaCble = string.Empty;
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var cta = Context.vwct_cbtecble_con_ctacble_acreedora.Where(q => q.IdEmpresa == IdEmpresa
                              && q.IdTipoCbte == IdTipoCbte
                              && q.IdCbteCble == IdCbteCble).FirstOrDefault();
                    if (cta == null) return null;
                    IdCtaCble = cta.IdCtaCble_Acreedora;
                }
                return IdCtaCble;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ct_plancta_Info> Lista = new List<ct_plancta_Info>();
            Lista = get_list(skip, take, args.Filter, IdEmpresa);

            return Lista;
        }

        public ct_plancta_Info get_info_demanda(string IdCtaCble, int IdEmpresa)
        {
            ct_plancta_Info info = new ct_plancta_Info();
            using (Entities_contabilidad Contex = new Entities_contabilidad())
            {
                info = (from q in Contex.ct_plancta
                        where q.IdCtaCble == IdCtaCble
                        && q.IdEmpresa == IdEmpresa
                        select new ct_plancta_Info
                        {
                            IdCtaCble = q.IdCtaCble,
                            pc_Cuenta = q.pc_Cuenta
                        }).FirstOrDefault();
            }
            return info;
        }

        public List<ct_plancta_Info> get_list(int skip, int take, string filter, int IdEmpresa)
        {
            try
            {
                List<ct_plancta_Info> Lista = new List<ct_plancta_Info>();
                Entities_contabilidad Context = new Entities_contabilidad();
                var lst = (from
                          p in Context.ct_plancta
                           where p.IdEmpresa == IdEmpresa
                           && (p.IdCtaCble.ToString() + " " + p.pc_Cuenta).Contains(filter)                          
                           select new
                           {
                               p.IdCtaCble,
                               p.pc_Cuenta,
                           })
                             .OrderBy(p => p.IdCtaCble)
                             .Skip(skip)
                             .Take(take)
                             .ToList();
                foreach (var q in lst)
                {
                    Lista.Add(new ct_plancta_Info
                    {
                        IdCtaCble = q.IdCtaCble,
                        pc_Cuenta = q.pc_Cuenta,
                    });
                }
                Context.Dispose();
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ct_plancta_Info> get_list_rango_cta(int IdEmpresa, string IdCtaCbleIni, string IdCtaCbleFin)
        {
            try
            {
                if (string.IsNullOrEmpty(IdCtaCbleIni))
                    return new List<ct_plancta_Info>();

                var sec = 1;
                List<ct_plancta_Info> Lista = new List<ct_plancta_Info>();
                List<ct_plancta_Info> lst = new List<ct_plancta_Info>();

                var IdCtaCbleFin_ = string.IsNullOrEmpty(IdCtaCbleFin) ? IdCtaCbleIni : IdCtaCbleFin;

                Entities_contabilidad Context = new Entities_contabilidad();

                if (IdCtaCbleIni != "" && IdCtaCbleFin_!="")
                {
                    lst = Context.ct_plancta.Where(q => q.IdEmpresa == IdEmpresa && q.pc_EsMovimiento == "S").Select(q => new ct_plancta_Info { IdEmpresa = q.IdEmpresa, IdCtaCble = q.IdCtaCble }).ToList();
                }

                lst.ForEach(q => q.Secuencia = sec++);

                var SecuenciaInicio = lst.Where(q=>q.IdCtaCble == IdCtaCbleIni).FirstOrDefault().Secuencia;
                var SecuenciaFin = lst.Where(q => q.IdCtaCble == IdCtaCbleFin_).FirstOrDefault().Secuencia;

                Lista = lst.Where(q => q.Secuencia >= SecuenciaInicio && q.Secuencia <= SecuenciaFin).ToList();

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
