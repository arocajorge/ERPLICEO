using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_proveedor_clase_Data
    {
        public List<cp_proveedor_clase_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<cp_proveedor_clase_Info> Lista;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.cp_proveedor_clase
                             where q.IdEmpresa == IdEmpresa
                             select new cp_proveedor_clase_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdClaseProveedor = q.IdClaseProveedor,
                                 cod_clase_proveedor = q.cod_clase_proveedor,
                                 descripcion_clas_prove = q.descripcion_clas_prove,
                                 Estado = q.Estado,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.cp_proveedor_clase
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new cp_proveedor_clase_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdClaseProveedor = q.IdClaseProveedor,
                                     cod_clase_proveedor = q.cod_clase_proveedor,
                                     descripcion_clas_prove = q.descripcion_clas_prove,
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

        public cp_proveedor_clase_Info get_info(int IdEmpresa, int IdClaseProveedor)
        {
            try
            {
                cp_proveedor_clase_Info info = new cp_proveedor_clase_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_proveedor_clase Entity = Context.cp_proveedor_clase.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdClaseProveedor == IdClaseProveedor);
                    if (Entity == null) return null;
                    info = new cp_proveedor_clase_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdClaseProveedor = Entity.IdClaseProveedor,
                        cod_clase_proveedor = Entity.cod_clase_proveedor,
                        descripcion_clas_prove = Entity.descripcion_clas_prove,
                        IdCtaCble_CXP = Entity.IdCtaCble_CXP,
                        IdCtaCble_gasto = Entity.IdCtaCble_gasto,
                        Estado = Entity.Estado,
                        IdCtaCble_Anticipo = Entity.IdCtaCble_Anticipo
                    };
                }
                return info;
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
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var lst = from q in Context.cp_proveedor_clase
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdClaseProveedor) + 1;

                }
                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cp_proveedor_clase_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_proveedor_clase Entity = new cp_proveedor_clase
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdClaseProveedor = info.IdClaseProveedor = get_id(info.IdEmpresa),
                        cod_clase_proveedor = info.cod_clase_proveedor,
                        descripcion_clas_prove = info.descripcion_clas_prove,
                        IdCtaCble_CXP = info.IdCtaCble_CXP,
                        IdCtaCble_gasto = info.IdCtaCble_gasto,
                        Estado = info.Estado = "A",
                        IdCtaCble_Anticipo = info.IdCtaCble_Anticipo

                    };
                    Context.cp_proveedor_clase.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(cp_proveedor_clase_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_proveedor_clase Entity = Context.cp_proveedor_clase.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdClaseProveedor == info.IdClaseProveedor);
                    if (Entity == null)
                        return false;
                    Entity.cod_clase_proveedor = info.cod_clase_proveedor;
                    Entity.descripcion_clas_prove = info.descripcion_clas_prove;
                    Entity.IdCtaCble_CXP = info.IdCtaCble_CXP;
                    Entity.IdCtaCble_gasto = info.IdCtaCble_gasto;
                    Entity.IdCtaCble_Anticipo = info.IdCtaCble_Anticipo;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(cp_proveedor_clase_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_proveedor_clase Entity = Context.cp_proveedor_clase.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdClaseProveedor == info.IdClaseProveedor);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";

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
