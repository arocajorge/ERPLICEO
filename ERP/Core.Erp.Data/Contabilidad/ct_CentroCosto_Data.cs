using Core.Erp.Info.Contabilidad;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_CentroCosto_Data
    {
        public List<ct_CentroCosto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool MostrarPadre)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ct_CentroCosto_Info> Lista = new List<ct_CentroCosto_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter, MostrarPadre);
            return Lista;
        }
        public List<ct_CentroCosto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool MostrarCentroCostoMovimiento, string CentroCosto_padre)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ct_CentroCosto_Info> Lista = new List<ct_CentroCosto_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter, MostrarCentroCostoMovimiento, CentroCosto_padre);
            return Lista;
        }
        public ct_CentroCosto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            //La variable args del devexpress ya trae el ID seleccionado en la propiedad Value, se pasa el IdEmpresa porque es un filtro que no tiene
            return get_info(IdEmpresa, args.Value == null ? "" : args.Value.ToString());
        }
        public List<ct_CentroCosto_Info> get_list(int IdEmpresa, int skip, int take, string filter, bool MostrarCentroCostoPadre)
        {
            try
            {
                List<ct_CentroCosto_Info> Lista = new List<ct_CentroCosto_Info>();

                Entities_contabilidad context_g = new Entities_contabilidad();

                {
                    List<ct_CentroCosto> lstg = new List<ct_CentroCosto>();

                    if (!MostrarCentroCostoPadre)
                        lstg = context_g.ct_CentroCosto.Include("ct_CentroCosto2").Where(q => q.IdEmpresa == IdEmpresa && q.EsMovimiento == true && q.Estado == true && (q.IdCentroCosto + " " + q.cc_Descripcion).Contains(filter)).OrderBy(q => q.IdCentroCosto).Skip(skip).Take(take).ToList();
                    else
                        lstg = context_g.ct_CentroCosto.Include("ct_CentroCosto2").Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true && (q.IdCentroCosto + " " + q.cc_Descripcion).Contains(filter)).OrderBy(q => q.IdCentroCosto).Skip(skip).Take(take).ToList();
                    foreach (var q in lstg)
                    {
                        Lista.Add(new ct_CentroCosto_Info
                        {
                            IdCentroCosto = q.IdCentroCosto,
                            cc_Descripcion = q.cc_Descripcion,
                            IdCentroCostoPadre = q.IdCentroCostoPadre,
                            cc_Descripcion_Padre = q.ct_CentroCosto2 == null ? null : q.ct_CentroCosto2.cc_Descripcion
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
        public List<ct_CentroCosto_Info> get_list(int IdEmpresa, int skip, int take, string filter, bool MostrarCentroCostoMovimiento, string CentroCosto_padre)
        {
            try
            {
                List<ct_CentroCosto_Info> Lista = new List<ct_CentroCosto_Info>();

                Entities_contabilidad context_g = new Entities_contabilidad();

                {
                    List<ct_CentroCosto> lstg;
                    if (!MostrarCentroCostoMovimiento)
                        lstg = context_g.ct_CentroCosto.Where(q => q.IdEmpresa == IdEmpresa && q.EsMovimiento == true && q.Estado == true && (q.IdCentroCosto + " " + q.cc_Descripcion).Contains(filter) && q.IdCentroCosto.Contains(CentroCosto_padre)).OrderBy(q => q.IdCentroCosto).Skip(skip).Take(take).ToList();
                    else
                        lstg = context_g.ct_CentroCosto.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true && (q.IdCentroCosto + " " + q.cc_Descripcion).Contains(filter) && q.IdCentroCosto.Contains(CentroCosto_padre)).OrderBy(q => q.IdCentroCosto).Skip(skip).Take(take).ToList();
                    foreach (var q in lstg)
                    {
                        Lista.Add(new ct_CentroCosto_Info
                        {
                            IdCentroCosto = q.IdCentroCosto,
                            cc_Descripcion = q.cc_Descripcion
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

        public List<ct_CentroCosto_Info> get_list(int IdEmpresa, bool mostrar_anulados, bool mostrar_solo_cuentas_movimiento)
        {
            try
            {
                List<ct_CentroCosto_Info> Lista;

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    if (mostrar_anulados && mostrar_solo_cuentas_movimiento)

                        Lista = (from q in Context.vwct_CentroCosto
                                 where q.EsMovimiento == true
                                 && q.IdEmpresa == IdEmpresa
                                 orderby q.IdCentroCosto
                                 select new ct_CentroCosto_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCentroCosto = q.IdCentroCosto,
                                     IdCentroCostoPadre = q.IdCentroCostoPadre,
                                     cc_Descripcion = q.cc_Descripcion,
                                     cc_Descripcion_Padre = q.cc_Descripcion_Padre,
                                     nv_Descripcion = q.nv_Descripcion,                                    
                                     IdNivel = q.IdNivel,
                                     EsMovimiento = q.EsMovimiento,
                                     Estado = q.Estado
                                 }).ToList();
                    else
                        if (!mostrar_anulados && mostrar_solo_cuentas_movimiento)
                        Lista = (from q in Context.vwct_CentroCosto
                                 where q.EsMovimiento == true
                                 && q.Estado == true
                                 && q.IdEmpresa == IdEmpresa
                                 orderby q.IdCentroCosto
                                 select new ct_CentroCosto_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCentroCosto = q.IdCentroCosto,
                                     IdCentroCostoPadre = q.IdCentroCostoPadre,
                                     cc_Descripcion = q.cc_Descripcion,
                                     cc_Descripcion_Padre = q.cc_Descripcion_Padre,
                                     nv_Descripcion = q.nv_Descripcion,
                                     IdNivel = q.IdNivel,
                                     EsMovimiento = q.EsMovimiento,
                                     Estado = q.Estado
                                 }).ToList();
                    else
                        if (mostrar_anulados && !mostrar_solo_cuentas_movimiento)
                        Lista = (from q in Context.vwct_CentroCosto
                                 where q.IdEmpresa == IdEmpresa
                                 orderby q.IdCentroCosto
                                 select new ct_CentroCosto_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCentroCosto = q.IdCentroCosto,
                                     IdCentroCostoPadre = q.IdCentroCostoPadre,
                                     cc_Descripcion = q.cc_Descripcion,
                                     cc_Descripcion_Padre = q.cc_Descripcion_Padre,
                                     nv_Descripcion = q.nv_Descripcion,
                                     IdNivel = q.IdNivel,
                                     EsMovimiento = q.EsMovimiento,
                                     Estado = q.Estado
                                 }).ToList();
                    else
                        Lista = (from q in Context.vwct_CentroCosto
                                 where q.Estado == true
                                 && q.IdEmpresa == IdEmpresa
                                 orderby q.IdCentroCosto
                                 select new ct_CentroCosto_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCentroCosto = q.IdCentroCosto,
                                     IdCentroCostoPadre = q.IdCentroCostoPadre,
                                     cc_Descripcion = q.cc_Descripcion,
                                     cc_Descripcion_Padre = q.cc_Descripcion_Padre,
                                     nv_Descripcion = q.nv_Descripcion,
                                     IdNivel = q.IdNivel,
                                     EsMovimiento = q.EsMovimiento,
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

        public ct_CentroCosto_Info get_info(int IdEmpresa, string IdCentroCosto)
        {
            try
            {
                ct_CentroCosto_Info info = new ct_CentroCosto_Info();

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_CentroCosto Entity = Context.ct_CentroCosto.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCentroCosto == IdCentroCosto);
                    if (Entity == null) return null;
                    info = new ct_CentroCosto_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCentroCosto = Entity.IdCentroCosto,
                        IdCentroCostoPadre = Entity.IdCentroCostoPadre,
                        cc_Descripcion = Entity.cc_Descripcion,
                        IdNivel = Entity.IdNivel,
                        EsMovimiento = Entity.EsMovimiento,
                        Estado = Entity.Estado,
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(ct_CentroCosto_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var plancta = Context.ct_CentroCosto.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCentroCosto == info.IdCentroCosto).FirstOrDefault();
                    if (plancta != null)
                        return false;

                    ct_CentroCosto Entity = new ct_CentroCosto
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCentroCosto = info.IdCentroCosto,
                        IdCentroCostoPadre = info.IdCentroCostoPadre,
                        IdNivel = info.IdNivel,
                        cc_Descripcion = info.cc_Descripcion,
                        EsMovimiento = info.EsMovimiento,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.ct_CentroCosto.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ct_CentroCosto_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_CentroCosto Entity = Context.ct_CentroCosto.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCentroCosto == info.IdCentroCosto);
                    if (Entity == null) return false;
                    Entity.EsMovimiento = info.EsMovimiento;
                    Entity.IdNivel = info.IdNivel;
                    Entity.cc_Descripcion = info.cc_Descripcion;
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

        public bool anularDB(ct_CentroCosto_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_CentroCosto Entity = Context.ct_CentroCosto.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCentroCosto == info.IdCentroCosto);
                    if (Entity == null) return false;
                    Entity.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioModificacion;
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

        public ct_CentroCosto_Info get_info_nuevo(int IdEmpresa, string IdCentroCostoPadre)
        {
            try
            {
                ct_CentroCosto_Info info = new ct_CentroCosto_Info();
                string ID = IdCentroCostoPadre;

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_CentroCosto Entity_padre = Context.ct_CentroCosto.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCentroCosto == IdCentroCostoPadre);
                    if (Entity_padre == null) return info;
                    int IdNivel_hijo = Convert.ToInt32( Entity_padre.IdCentroCosto )+ 1 ;
                    ct_CentroCostoNivel Entity_nivel_hijo = Context.ct_CentroCostoNivel.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdNivel == IdNivel_hijo);
                    if (Entity_nivel_hijo == null) return info;

                    var lst = from q in Context.ct_CentroCosto
                              where q.IdCentroCostoPadre == IdCentroCostoPadre
                              && q.IdEmpresa == IdEmpresa
                              select q;

                    string relleno = "";
                    string digitos = relleno.PadLeft(Entity_nivel_hijo.nv_NumDigitos, '0');

                    if (lst.Count() > 0)
                    {
                        ID += (Convert.ToInt32(lst.Max(q => q.IdCentroCosto.Substring(q.IdCentroCosto.Length - Entity_nivel_hijo.nv_NumDigitos, Entity_nivel_hijo.nv_NumDigitos))) + 1).ToString(digitos);
                    }
                    else
                        ID += Convert.ToInt32(1).ToString(digitos);

                    info = new ct_CentroCosto_Info
                    {
                        IdCentroCosto = ID,
                        IdNivel = Entity_padre.IdNivel,
                        EsMovimiento = Entity_padre.EsMovimiento
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool validar_existe_id(int IdEmpresa, string IdCentroCosto)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var lst = from q in Context.ct_CentroCosto
                              where q.IdEmpresa == IdEmpresa
                              && q.IdCentroCosto == IdCentroCosto
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
                    
    }
}
