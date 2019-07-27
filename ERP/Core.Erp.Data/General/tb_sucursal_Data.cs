using Core.Erp.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_sucursal_Data
    {
        public List<tb_sucursal_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<tb_sucursal_Info> Lista = new List<tb_sucursal_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter);
            return Lista;
        }

        public tb_sucursal_Info get_info_bajo_demanda(int IdEmpresa, ListEditItemRequestedByValueEventArgs args)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda(IdEmpresa, (int)args.Value);
        }

        public List<tb_sucursal_Info> get_list(int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<tb_sucursal_Info> Lista = new List<tb_sucursal_Info>();

                Entities_general context_g = new Entities_general();

                var lstg = context_g.tb_sucursal.Where(q => q.Estado == "A" && q.IdEmpresa == IdEmpresa && (q.IdSucursal.ToString() + " " + q.Su_Descripcion).Contains(filter)).OrderBy(q => q.IdSucursal).Skip(skip).Take(take);
                foreach (var q in lstg)
                {
                    Lista.Add(new tb_sucursal_Info
                    {
                        IdSucursal = q.IdSucursal,
                        codigo = q.codigo,
                        Su_Descripcion = q.Su_Descripcion
                    });
                }

                context_g.Dispose();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_sucursal_Info get_info_demanda(int IdEmpresa, int value)
        {
            tb_sucursal_Info info = new tb_sucursal_Info();
            using (Entities_general Contex = new Entities_general())
            {
                info = (from q in Contex.tb_sucursal
                        where q.IdEmpresa == IdEmpresa   
                        && q.IdSucursal == value      
                        select new tb_sucursal_Info
                        {
                            IdSucursal = q.IdSucursal,
                            codigo = q.codigo,
                            Su_Descripcion = q.Su_Descripcion
                        }).FirstOrDefault();
            }
            return info;
        }

        public tb_sucursal_Info get_info(int IdEmpresa, int IdSucursal)
        {
            tb_sucursal_Info info = new tb_sucursal_Info();

            Entities_general context_g = new Entities_general();

            info = (from q in context_g.tb_sucursal
                    where q.Estado == "A"
                    && q.IdEmpresa == IdEmpresa
                    && q.IdSucursal == IdSucursal
                    select new tb_sucursal_Info
                    {
                        IdSucursal = q.IdSucursal,
                        Su_Descripcion = q.Su_Descripcion,
                        codigo = q.codigo,
                        Estado = q.Estado,
                        Es_establecimiento = q.Es_establecimiento,
                        IdEmpresa = q.IdEmpresa,
                        Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                        Su_Direccion = q.Su_Direccion,
                        Su_JefeSucursal = q.Su_JefeSucursal,
                        Su_Ruc = q.Su_Ruc,
                        Su_Telefonos = q.Su_Telefonos,
                        IdCtaCble_cxp = q.IdCtaCble_cxp,
                        IdCtaCble_vtaIVA = q.IdCtaCble_vtaIVA,
                        IdCtaCble_vtaIVA0 = q.IdCtaCble_vtaIVA0
                        
                    }).FirstOrDefault();

            context_g.Dispose();

            return info;
        }

        public List<tb_sucursal_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<tb_sucursal_Info> Lista;

                using (Entities_general Context = new Entities_general())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.tb_sucursal
                             where q.IdEmpresa == IdEmpresa                             
                             select new tb_sucursal_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 Su_Descripcion = q.Su_Descripcion,
                                 Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                                 Su_Ruc = q.Su_Ruc,
                                 Estado = q.Estado,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.tb_sucursal
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new tb_sucursal_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     Su_Descripcion = q.Su_Descripcion,
                                     Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                                     Su_Ruc = q.Su_Ruc,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }

                Lista.ForEach(v => {v.IdString = v.IdEmpresa.ToString("000") + v.IdSucursal.ToString("000"); });
                return Lista;
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

                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_sucursal
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdSucursal) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_sucursal_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sucursal Entity = new tb_sucursal
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal = get_id(info.IdEmpresa),
                        codigo = info.codigo,
                        Su_Descripcion = info.Su_Descripcion,
                        Su_CodigoEstablecimiento = info.Su_CodigoEstablecimiento,
                        Su_Ruc = info.Su_Ruc,
                        Su_JefeSucursal = info.Su_JefeSucursal,
                        Su_Telefonos = info.Su_Telefonos,
                        Su_Direccion = info.Su_Direccion,
                        Es_establecimiento = info.Es_establecimiento,
                        Estado = info.Estado = "A",
                        IdCtaCble_cxp = info.IdCtaCble_cxp,
                        IdCtaCble_vtaIVA = info.IdCtaCble_vtaIVA,
                        IdCtaCble_vtaIVA0 = info.IdCtaCble_vtaIVA0,

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.tb_sucursal.Add(Entity);

                    if (info.ListaNivelDescuento != null)
                    {
                        int Secuencia = 1;
                        foreach (var item in info.ListaNivelDescuento)
                        {
                            Context.tb_sucursal_FormaPago_x_fa_NivelDescuento.Add(new tb_sucursal_FormaPago_x_fa_NivelDescuento
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdSucursal = info.IdSucursal,
                                Secuencia = Secuencia++,
                                IdCatalogo = item.IdCatalogo,
                                IdNivel = item.IdNivel
                            });

                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_sucursal_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sucursal Entity = Context.tb_sucursal.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal);
                    if (Entity == null)
                        return false;
                    Entity.codigo = info.codigo;
                    Entity.Su_Descripcion = info.Su_Descripcion;
                    Entity.Su_CodigoEstablecimiento = info.Su_CodigoEstablecimiento;
                    Entity.Su_Ruc = info.Su_Ruc;
                    Entity.Su_JefeSucursal = info.Su_JefeSucursal;
                    Entity.Su_Telefonos = info.Su_Telefonos;
                    Entity.Su_Direccion = info.Su_Direccion;
                    Entity.Es_establecimiento = info.Es_establecimiento;
                    Entity.IdCtaCble_cxp = info.IdCtaCble_cxp;
                    Entity.IdCtaCble_vtaIVA = info.IdCtaCble_vtaIVA;
                    Entity.IdCtaCble_vtaIVA0 = info.IdCtaCble_vtaIVA0;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;

                    var lst_det_grupo = Context.tb_sucursal_FormaPago_x_fa_NivelDescuento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal).ToList();
                    Context.tb_sucursal_FormaPago_x_fa_NivelDescuento.RemoveRange(lst_det_grupo);

                    if (info.ListaNivelDescuento != null)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.ListaNivelDescuento)
                        {
                            Context.tb_sucursal_FormaPago_x_fa_NivelDescuento.Add(new tb_sucursal_FormaPago_x_fa_NivelDescuento
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdSucursal = info.IdSucursal,
                                Secuencia = Secuencia++,
                                IdCatalogo = item.IdCatalogo,
                                IdNivel = item.IdNivel
                            });
                        }
                    }

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(tb_sucursal_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sucursal Entity = Context.tb_sucursal.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_sucursal_Info GetInfo(int IdEmpresa, string CodigoEstablecimiento)
        {
            try
            {
                tb_sucursal_Info info = new tb_sucursal_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_sucursal Entity = Context.tb_sucursal.Where(q => q.IdEmpresa == IdEmpresa && q.Su_CodigoEstablecimiento == CodigoEstablecimiento).FirstOrDefault();
                    if (Entity == null) return null;
                     info = new tb_sucursal_Info
                    {
                         IdEmpresa = Entity.IdEmpresa,
                         IdSucursal = Entity.IdSucursal,
                         Su_Descripcion = Entity.Su_Descripcion,
                         codigo = Entity.codigo,
                         Estado = Entity.Estado,
                         Es_establecimiento = Entity.Es_establecimiento,
                         Su_CodigoEstablecimiento = Entity.Su_CodigoEstablecimiento,
                         Su_Direccion = Entity.Su_Direccion,
                         Su_JefeSucursal = Entity.Su_JefeSucursal,
                         Su_Ruc = Entity.Su_Ruc,
                         Su_Telefonos = Entity.Su_Telefonos                         
                     };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<tb_sucursal_Info> GetList(int IdEmpresa, string IdUsuario, bool MostrarTodos)
        {
            try
            {
                List<tb_sucursal_Info> Lista;
                using (Entities_seguridad_acceso Context = new Entities_seguridad_acceso())
                {
                    Lista = Context.vwseg_usuario_x_tb_sucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdUsuario == IdUsuario).Select(q => new tb_sucursal_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdUsuario = q.IdUsuario,
                        IdSucursal = q.IdSucursal,
                        Su_Descripcion = q.Su_Descripcion,
                    }).ToList();
                    if (MostrarTodos)
                    {
                        Lista.Add(new tb_sucursal_Info
                        {
                            IdEmpresa = IdEmpresa, 
                            IdSucursal = 0,
                            Su_Descripcion = "TODAS"
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

        public List<tb_sucursal_Info> GetListSinEmpresa( bool mostrar_anulados)
        {
            try
            {
                List<tb_sucursal_Info> Lista;

                using (Entities_general Context = new Entities_general())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.tb_sucursal
                                 select new tb_sucursal_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     Su_Descripcion = q.Su_Descripcion,
                                     Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                                     Su_Ruc = q.Su_Ruc,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.tb_sucursal
                               where  q.Estado == "A"
                                 select new tb_sucursal_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     Su_Descripcion = q.Su_Descripcion,
                                     Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                                     Su_Ruc = q.Su_Ruc,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }

                Lista.ForEach(v => { v.IdString = v.IdEmpresa.ToString("000") + v.IdSucursal.ToString("000"); });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
