using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.CuentasPorPagar;
namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_orden_pago_tipo_Data
    {

        public List<cp_orden_pago_tipo_Info> get_list()
        {
            try
            {
                List<cp_orden_pago_tipo_Info> Lista;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.cp_orden_pago_tipo
                             select new cp_orden_pago_tipo_Info
                             {
                                 IdTipo_op = q.IdTipo_op,
                                 Descripcion = q.Descripcion,
                                 Estado = q.Estado,
                                 GeneraDiario = q.GeneraDiario,

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

        public bool guardarDB(cp_orden_pago_tipo_Info item)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {

                    cp_orden_pago_tipo Entity = new cp_orden_pago_tipo
                    {
                        IdTipo_op = item.IdTipo_op,
                        Descripcion = item.Descripcion,
                        GeneraDiario = item.GeneraDiario,
                        Estado = "A"
                    };
                    Context.cp_orden_pago_tipo.Add(Entity);
                    Context.SaveChanges();

                }


                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(cp_orden_pago_tipo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago_tipo Entity = Context.cp_orden_pago_tipo.FirstOrDefault(q => q.IdTipo_op == info.IdTipo_op);
                    if (Entity == null) return false;
                    Entity.Descripcion = info.Descripcion;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(cp_orden_pago_tipo_Info info)
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

        public bool si_existe(cp_orden_pago_tipo_Info info)
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
