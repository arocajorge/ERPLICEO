using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Erp.Info.CuentasPorPagar;

namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_orden_pago_tipo_x_empresa_Data
    {

        public List<cp_orden_pago_tipo_x_empresa_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<cp_orden_pago_tipo_x_empresa_Info> Lista;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {

                    Lista = (from p in Context.cp_orden_pago_tipo
                             join q in Context.cp_orden_pago_tipo_x_empresa                             
                             on new {p.IdTipo_op} equals new {q.IdTipo_op}
                             where q.IdEmpresa == IdEmpresa
                             select new cp_orden_pago_tipo_x_empresa_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipo_op = q.IdTipo_op,
                                 IdCtaCble = q.IdCtaCble,
                                 IdTipoCbte_OP = q.IdTipoCbte_OP,
                                 IdEstadoAprobacion = q.IdEstadoAprobacion,
                                 Buscar_FactxPagar = q.Buscar_FactxPagar,
                                 IdCtaCble_Credito = q.IdCtaCble_Credito,
                                 Dispara_Alerta = q.Dispara_Alerta,
                                 Descripcion=p.Descripcion,
                                 Estado=p.Estado,

                                 EstadoBool = p.Estado == "A" ? true : false


                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public cp_orden_pago_tipo_x_empresa_Info get_info(int IdEmpresa, string IdTipo_op)
        {
            try
            {
                cp_orden_pago_tipo_x_empresa_Info info = new cp_orden_pago_tipo_x_empresa_Info();
                List<cp_orden_pago_tipo_x_empresa_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.cp_orden_pago_tipo_x_empresa
                             join p in Context.cp_orden_pago_tipo                             
                             on new { q.IdTipo_op } equals new { p.IdTipo_op }
                             join r in Context.cp_orden_pago_estado_aprob
                             on new { q.IdEstadoAprobacion } equals new { r.IdEstadoAprobacion }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTipo_op == p.IdTipo_op
                             &&q.IdTipo_op==IdTipo_op
                             && p.IdTipo_op==IdTipo_op
                             select new cp_orden_pago_tipo_x_empresa_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipo_op = q.IdTipo_op,
                                 IdCtaCble = q.IdCtaCble,
                                 IdTipoCbte_OP = q.IdTipoCbte_OP,
                                 IdEstadoAprobacion = q.IdEstadoAprobacion,
                                 Buscar_FactxPagar = q.Buscar_FactxPagar,
                                 IdCtaCble_Credito = q.IdCtaCble_Credito,
                                 Dispara_Alerta = q.Dispara_Alerta,
                                 Descripcion = p.Descripcion,
                                 Estado = p.Estado,
                                 DescripcionAprobacion=r.Descripcion



                             }).ToList();
                }
                if(Lista.Count()>0)
                 info = Lista.FirstOrDefault();
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool guardarDB(cp_orden_pago_tipo_x_empresa_Info item)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    
                        cp_orden_pago_tipo_x_empresa Entity = new cp_orden_pago_tipo_x_empresa
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdTipo_op = item.IdTipo_op,
                            IdCtaCble = item.IdCtaCble,
                            IdTipoCbte_OP = item.IdTipoCbte_OP,
                            IdEstadoAprobacion = item.IdEstadoAprobacion,
                            Buscar_FactxPagar = item.Buscar_FactxPagar,
                            IdCtaCble_Credito = item.IdCtaCble_Credito,
                            Dispara_Alerta = item.Dispara_Alerta
                        };
                        Context.cp_orden_pago_tipo_x_empresa.Add(Entity);
                    Context.SaveChanges();

                }


                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(cp_orden_pago_tipo_x_empresa_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago_tipo_x_empresa Entity = Context.cp_orden_pago_tipo_x_empresa.FirstOrDefault(q => q.IdTipo_op == info.IdTipo_op && q.IdEmpresa==info.IdEmpresa);
                    if (Entity == null) return false;
                    Entity.IdCtaCble = info.IdCtaCble;
                    Entity.IdCtaCble_Credito = info.IdCtaCble_Credito;
                    Entity.IdEstadoAprobacion = info.IdEstadoAprobacion;
                    Entity.IdTipoCbte_OP = info.IdTipoCbte_OP;
            
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(cp_orden_pago_tipo_x_empresa_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago_tipo Entity = Context.cp_orden_pago_tipo.FirstOrDefault(q => q.IdTipo_op == info.IdTipo_op);
                    if (Entity == null) return false;
                    Entity.Estado = "I";
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool si_existe(cp_orden_pago_tipo_x_empresa_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago_tipo Entity = Context.cp_orden_pago_tipo.FirstOrDefault(q => q.IdTipo_op == info.IdTipo_op);
                    if (Entity == null)
                        return
                        false;
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
