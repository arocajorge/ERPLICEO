using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_cliente_tipo_Data
    {
        public List<fa_cliente_tipo_Info> get_list (int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<fa_cliente_tipo_Info> Lista;
                using (Entities_facturacion Context =  new Entities_facturacion())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.fa_cliente_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 select new fa_cliente_tipo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     Idtipo_cliente = q.Idtipo_cliente,
                                     Cod_cliente_tipo = q.Cod_cliente_tipo,
                                     Descripcion_tip_cliente = q.Descripcion_tip_cliente,
                                     estado = q.Estado,
                                     IdCtaCble_CXC_Cred = q.IdCtaCble_CXC_Cred,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.fa_cliente_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new fa_cliente_tipo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     Idtipo_cliente = q.Idtipo_cliente,
                                     Cod_cliente_tipo = q.Cod_cliente_tipo,
                                     Descripcion_tip_cliente = q.Descripcion_tip_cliente,
                                     estado = q.Estado,
                                     IdCtaCble_CXC_Cred = q.IdCtaCble_CXC_Cred,

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

        public fa_cliente_tipo_Info get_info(int IdEmpresa , int Idtipo_cliente)
        {
            try
            {
                fa_cliente_tipo_Info info = new fa_cliente_tipo_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_cliente_tipo Entity = Context.fa_cliente_tipo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.Idtipo_cliente == Idtipo_cliente);
                    if (Entity == null) return null;
                    info = new fa_cliente_tipo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        Idtipo_cliente = Entity.Idtipo_cliente,
                        Cod_cliente_tipo = Entity.Cod_cliente_tipo,
                        Descripcion_tip_cliente = Entity.Descripcion_tip_cliente,
                        IdCtaCble_CXC_Cred = Entity.IdCtaCble_CXC_Cred,
                        estado = Entity.Estado

                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private int get_id(int IdEmpresa)
        {

            try
            {
                int ID = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_cliente_tipo
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.Idtipo_cliente) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_cliente_tipo_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_cliente_tipo Entity = new fa_cliente_tipo
                    {
                        IdEmpresa = info.IdEmpresa,
                        Idtipo_cliente = info.Idtipo_cliente=get_id(info.IdEmpresa),
                        Cod_cliente_tipo = info.Cod_cliente_tipo,
                        Descripcion_tip_cliente = info.Descripcion_tip_cliente,
                        IdCtaCble_CXC_Cred = info.IdCtaCble_CXC_Cred,
                        Estado = info.estado="A",

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.fa_cliente_tipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(fa_cliente_tipo_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_cliente_tipo Entity = Context.fa_cliente_tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.Idtipo_cliente == info.Idtipo_cliente);
                    if (Entity == null) return false;

                    Entity.Cod_cliente_tipo = info.Cod_cliente_tipo;
                    Entity.Descripcion_tip_cliente = info.Descripcion_tip_cliente;
                    Entity.IdCtaCble_CXC_Cred = info.IdCtaCble_CXC_Cred;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
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

        public bool anularDB(fa_cliente_tipo_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_cliente_tipo Entity = Context.fa_cliente_tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.Idtipo_cliente == info.Idtipo_cliente);
                    if (Entity == null) return false;

                    Entity.Estado = info.estado="I";

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
    }
}
