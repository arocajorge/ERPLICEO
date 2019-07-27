using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_Vendedor_Data
    {
        public List<fa_Vendedor_Info> get_list(int IdEmpresa , bool mostrar_anulados)
        {
            try
            {
                List<fa_Vendedor_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.fa_Vendedor
                                 where q.IdEmpresa == IdEmpresa
                                 select new fa_Vendedor_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdVendedor = q.IdVendedor,
                                     Codigo = q.Codigo,
                                     ve_cedula = q.ve_cedula,
                                     Ve_Vendedor = q.Ve_Vendedor,
                                     PorComision = q.PorComision,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else Lista = (from q in Context.fa_Vendedor
                                  where q.IdEmpresa == IdEmpresa
                                  && q.Estado == "A"
                                  select new fa_Vendedor_Info
                                  {
                                      IdEmpresa = q.IdEmpresa,
                                      IdVendedor = q.IdVendedor,
                                      Codigo = q.Codigo,
                                      ve_cedula = q.ve_cedula,
                                      Ve_Vendedor = q.Ve_Vendedor,
                                      Estado = q.Estado,
                                      PorComision = q.PorComision,

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

        public fa_Vendedor_Info get_info(int IdEmpresa, int IdVendedor)
        {
            try
            {
                fa_Vendedor_Info info = new fa_Vendedor_Info();
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_Vendedor Entity = Context.fa_Vendedor.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdVendedor == IdVendedor);
                    if (Entity == null) return null;
                    info = new fa_Vendedor_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdVendedor = Entity.IdVendedor,
                        Codigo = Entity.Codigo,
                        ve_cedula = Entity.ve_cedula,
                        Ve_Vendedor = Entity.Ve_Vendedor,
                        NomInterno = Entity.NomInterno,
                        PorComision = Entity.PorComision,
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

        private int get_id(int IdEmpresa)
        {

            try
            {
                int ID = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_Vendedor
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdVendedor) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_Vendedor_Info info)
        {
            try
            {
                using (Entities_facturacion  Context = new Entities_facturacion())
                {
                    fa_Vendedor Entity = new fa_Vendedor
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdVendedor = info.IdVendedor=get_id(info.IdEmpresa),
                        Codigo = info.Codigo,
                        ve_cedula = info.ve_cedula,
                        Ve_Vendedor = info.Ve_Vendedor,
                        Estado = info.Estado="A",
                        NomInterno = info.NomInterno,
                        PorComision = info.PorComision,

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.fa_Vendedor.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(fa_Vendedor_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_Vendedor Entity = Context.fa_Vendedor.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdVendedor == info.IdVendedor);
                    if (Entity == null) return false;

                    Entity.Codigo = info.Codigo;
                    Entity.ve_cedula = info.ve_cedula;
                    Entity.Ve_Vendedor = info.Ve_Vendedor;
                    Entity.PorComision = info.PorComision;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.NomInterno = info.NomInterno;
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

        public bool anularDB(fa_Vendedor_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_Vendedor Entity = Context.fa_Vendedor.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdVendedor == info.IdVendedor);
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado="I";
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
