using Core.Erp.Info.Caja;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Caja
{
    public class caj_Caja_Movimiento_Tipo_Data
    {
        public List<caj_Caja_Movimiento_Tipo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<caj_Caja_Movimiento_Tipo_Info> Lista;
                using (Entities_caja Context = new Entities_caja())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.caj_Caja_Movimiento_Tipo
                             where q.IdEmpresa == IdEmpresa
                             orderby q.tm_Signo
                             select new caj_Caja_Movimiento_Tipo_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipoMovi = q.IdTipoMovi,
                                 Estado = q.Estado,
                                 tm_descripcion = q.tm_descripcion,
                                 SeDeposita = q.SeDeposita,
                                 tm_Signo = q.tm_Signo,
                                 IdCtaCble = q.IdCtaCble,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.caj_Caja_Movimiento_Tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 orderby q.tm_Signo
                                 select new caj_Caja_Movimiento_Tipo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoMovi = q.IdTipoMovi,
                                     Estado = q.Estado,
                                     tm_descripcion = q.tm_descripcion,
                                     SeDeposita = q.SeDeposita,
                                     tm_Signo = q.tm_Signo,
                                     IdCtaCble = q.IdCtaCble,

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
        
        public List<caj_Caja_Movimiento_Tipo_Info> get_list(int IdEmpresa, string signo, bool mostrar_anulados, bool mostrar_sin_ctaCble)
        {
            try
            {
                List<caj_Caja_Movimiento_Tipo_Info> Lista;
                using (Entities_caja Context = new Entities_caja())
                {
                    if (mostrar_anulados)
                    {
                        if(mostrar_sin_ctaCble)
                        Lista = (from q in Context.caj_Caja_Movimiento_Tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.tm_Signo == signo
                                 select new caj_Caja_Movimiento_Tipo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoMovi = q.IdTipoMovi,
                                     Estado = q.Estado,
                                     tm_descripcion = q.tm_descripcion,
                                     SeDeposita = q.SeDeposita,
                                     tm_Signo = q.tm_Signo,
                                     IdCtaCble = q.IdCtaCble
                                 }).ToList();
                        else
                            Lista = (from q in Context.caj_Caja_Movimiento_Tipo
                                     where q.IdEmpresa == IdEmpresa
                                     && q.tm_Signo == signo
                                     && q.IdCtaCble != null
                                     select new caj_Caja_Movimiento_Tipo_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         IdTipoMovi = q.IdTipoMovi,
                                         Estado = q.Estado,
                                         tm_descripcion = q.tm_descripcion,
                                         SeDeposita = q.SeDeposita,
                                         tm_Signo = q.tm_Signo,
                                         IdCtaCble = q.IdCtaCble
                                     }).ToList();
                    }
                    else
                    {
                        if (mostrar_sin_ctaCble)
                            Lista = (from q in Context.caj_Caja_Movimiento_Tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.tm_Signo == signo
                                 && q.Estado == "A"
                                 select new caj_Caja_Movimiento_Tipo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoMovi = q.IdTipoMovi,
                                     Estado = q.Estado,
                                     tm_descripcion = q.tm_descripcion,
                                     SeDeposita = q.SeDeposita,
                                     tm_Signo = q.tm_Signo,
                                     IdCtaCble = q.IdCtaCble,

                                 }).ToList();
                        else
                            Lista = (from q in Context.caj_Caja_Movimiento_Tipo
                                     where q.IdEmpresa == IdEmpresa
                                     && q.tm_Signo == signo
                                     && q.Estado == "A"
                                     && q.IdCtaCble != null
                                     select new caj_Caja_Movimiento_Tipo_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         IdTipoMovi = q.IdTipoMovi,
                                         Estado = q.Estado,
                                         tm_descripcion = q.tm_descripcion,
                                         SeDeposita = q.SeDeposita,
                                         tm_Signo = q.tm_Signo,
                                         IdCtaCble = q.IdCtaCble,

                                     }).ToList();
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public caj_Caja_Movimiento_Tipo_Info get_info(int IdEmpresa, int IdTipoMovi)
        {
            try
            {
                caj_Caja_Movimiento_Tipo_Info info = new caj_Caja_Movimiento_Tipo_Info();
                using (Entities_caja Context = new Entities_caja())
                {
                    caj_Caja_Movimiento_Tipo Entity = Context.caj_Caja_Movimiento_Tipo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdTipoMovi == IdTipoMovi);
                    if (Entity == null) return null;
                    info = new caj_Caja_Movimiento_Tipo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoMovi = Entity.IdTipoMovi,
                        Estado = Entity.Estado,
                        tm_descripcion = Entity.tm_descripcion,
                        SeDeposita = Entity.SeDeposita,
                        tm_Signo = Entity.tm_Signo,
                        IdCtaCble = Entity.IdCtaCble,
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int  get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;
                using (Entities_caja Context = new Entities_caja())
                {
                    var lst = from q in Context.caj_Caja_Movimiento_Tipo
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdTipoMovi) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(caj_Caja_Movimiento_Tipo_Info info)
        {
            try
            {
                using (Entities_caja Context = new Entities_caja())
                {
                    caj_Caja_Movimiento_Tipo Entity = new caj_Caja_Movimiento_Tipo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoMovi = info.IdTipoMovi=get_id(info.IdEmpresa),
                        Estado = info.Estado="A",
                        tm_descripcion = info.tm_descripcion,
                        SeDeposita = info.SeDeposita,
                        tm_Signo = info.tm_Signo,
                        IdCtaCble = info.IdCtaCble,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now

                    };
                    Context.caj_Caja_Movimiento_Tipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(caj_Caja_Movimiento_Tipo_Info info)
        {
            try
            {
                using (Entities_caja Context = new Entities_caja())
                {
                    caj_Caja_Movimiento_Tipo Entity = Context.caj_Caja_Movimiento_Tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoMovi == info.IdTipoMovi);
                    if (Entity == null) return false;

                    Entity.tm_descripcion = info.tm_descripcion;
                    Entity.SeDeposita = info.SeDeposita;
                    Entity.tm_Signo = info.tm_Signo;
                    Entity.IdCtaCble = info.IdCtaCble;
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

        public bool anularDB(caj_Caja_Movimiento_Tipo_Info info)
        {
            try
            {
                using (Entities_caja Context = new Entities_caja())
                {
                    caj_Caja_Movimiento_Tipo Entity = Context.caj_Caja_Movimiento_Tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoMovi == info.IdTipoMovi);
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado="I";


                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<caj_Caja_Movimiento_Tipo_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, string signo)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<caj_Caja_Movimiento_Tipo_Info> Lista = new List<caj_Caja_Movimiento_Tipo_Info>();
            Lista = get_list(skip, take, args.Filter, IdEmpresa, signo);
            return Lista;
        }

        public caj_Caja_Movimiento_Tipo_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            //La variable args del devexpress ya trae el ID seleccionado en la propiedad Value, se pasa el IdEmpresa porque es un filtro que no tiene
            var IdTipoMovi = args.Value == null ? "" : args.Value.ToString();
            return get_info(IdEmpresa, string.IsNullOrEmpty(IdTipoMovi.ToString()) ? 0 : Convert.ToInt32(IdTipoMovi) );
        }

        public List<caj_Caja_Movimiento_Tipo_Info> get_list(int skip, int take, string filter, int IdEmpresa, string signo)
        {
            try
            {
                List<caj_Caja_Movimiento_Tipo_Info> Lista = new List<caj_Caja_Movimiento_Tipo_Info>();

                Entities_caja Context = new Entities_caja();
                {
                    List<caj_Caja_Movimiento_Tipo> ListaTipoMovimiento;
                    ListaTipoMovimiento = Context.caj_Caja_Movimiento_Tipo.Where(q => q.IdEmpresa == IdEmpresa && q.tm_Signo == signo && q.Estado == "A" && (q.IdTipoMovi + " " + q.tm_descripcion).Contains(filter)).OrderBy(q => q.IdTipoMovi).Skip(skip).Take(take).ToList();
                    foreach (var q in ListaTipoMovimiento)
                    {
                        Lista.Add(new caj_Caja_Movimiento_Tipo_Info
                        {
                            IdTipoMovi = q.IdTipoMovi,
                            tm_descripcion = q.tm_descripcion
                        });
                    }
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
