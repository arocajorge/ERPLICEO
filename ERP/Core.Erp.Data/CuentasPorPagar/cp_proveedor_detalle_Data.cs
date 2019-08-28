using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_proveedor_detalle_Data
    {
        public List<cp_proveedor_detalle_Info> get_list(int IdEmpresa, decimal IdProveedor, bool MostrarAnulados)
        {
            try
            {
                List<cp_proveedor_detalle_Info> Lista;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.cp_proveedor_detalle
                             where q.IdEmpresa == IdEmpresa
                              && q.IdProveedor == IdProveedor
                              && q.Estado == (MostrarAnulados== true ? q.Estado : true)
                             select new cp_proveedor_detalle_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdProveedor = q.IdProveedor,
                                 Secuencia = q.Secuencia,
                                 NombreAdicional = q.NombreAdicional,
                                 IdTipoCta_acreditacion_cat = q.IdTipoCta_acreditacion_cat,
                                 num_cta_acreditacion = q.num_cta_acreditacion,
                                 IdBanco_acreditacion = q.IdBanco_acreditacion,
                                 pr_correo = q.pr_correo,
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

        public cp_proveedor_detalle_Info get_info(int IdEmpresa, int IdProveedor, int Secuencia)
        {
            try
            {
                cp_proveedor_detalle_Info info = new cp_proveedor_detalle_Info();

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_proveedor_detalle Entity = Context.cp_proveedor_detalle.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdProveedor == IdProveedor && q.Secuencia == Secuencia);
                    if (Entity == null) return null;
                    info = new cp_proveedor_detalle_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdProveedor = Entity.IdProveedor,
                        Secuencia = Entity.Secuencia,
                        NombreAdicional = Entity.NombreAdicional,
                        IdTipoCta_acreditacion_cat = Entity.IdTipoCta_acreditacion_cat,
                        num_cta_acreditacion = Entity.num_cta_acreditacion,
                        IdBanco_acreditacion = Entity.IdBanco_acreditacion,
                        pr_correo = Entity.pr_correo,
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

        private int get_secuencia(int IdEmpresa, decimal IdProveedor)
        {
            try
            {
                int Secuencia = 1;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var lst = from q in Context.cp_proveedor_detalle
                              where q.IdEmpresa == IdEmpresa
                              && q.IdProveedor == IdProveedor
                              select q;

                    if (lst.Count() > 0)
                        Secuencia = lst.Max(q => q.Secuencia) + 1;
                }

                return Secuencia;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(cp_proveedor_detalle_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_proveedor_detalle Entity = new cp_proveedor_detalle
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdProveedor = info.IdProveedor,
                        Secuencia = info.Secuencia= get_secuencia(info.IdEmpresa, info.IdProveedor),
                        NombreAdicional = info.NombreAdicional,
                        IdTipoCta_acreditacion_cat = info.IdTipoCta_acreditacion_cat,
                        num_cta_acreditacion = info.num_cta_acreditacion,
                        IdBanco_acreditacion = info.IdBanco_acreditacion,
                        pr_correo = info.pr_correo,
                        Estado = true
                    };
                    Context.cp_proveedor_detalle.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(cp_proveedor_detalle_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_proveedor_detalle Entity = Context.cp_proveedor_detalle.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdProveedor == info.IdProveedor && q.Secuencia == info.Secuencia);
                    if (Entity == null) return false;

                    Entity.NombreAdicional = info.NombreAdicional;
                    Entity.IdTipoCta_acreditacion_cat = info.IdTipoCta_acreditacion_cat;
                    Entity.num_cta_acreditacion = info.num_cta_acreditacion;
                    Entity.IdBanco_acreditacion = info.IdBanco_acreditacion;
                    Entity.pr_correo = info.pr_correo;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(cp_proveedor_detalle_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_proveedor_detalle Entity = Context.cp_proveedor_detalle.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdProveedor == info.IdProveedor && q.Secuencia == info.Secuencia);
                    if (Entity == null) return false;
                    Entity.Estado = false;

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
