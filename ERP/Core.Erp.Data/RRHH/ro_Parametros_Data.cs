using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
    public class ro_Parametros_Data
    {
        public ro_Parametros_Info get_info(int IdEmpresa)
        {
            try
            {
                ro_Parametros_Info info = new ro_Parametros_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Parametros q = Context.ro_Parametros.FirstOrDefault(v => v.IdEmpresa == IdEmpresa);
                    if (q == null) return null;

                    info = new ro_Parametros_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdTipoCbte_AsientoSueldoXPagar = q.IdTipoCbte_AsientoSueldoXPagar,
                        Genera_op_por_acta_finiquito = q.Genera_op_por_acta_finiquito,
                        Genera_op_por_liq_vacaciones = q.Genera_op_por_liq_vacaciones,
                        Genera_op_por_prestamos = q.Genera_op_por_prestamos,
                        genera_op_x_pago = q.genera_op_x_pago,
                        Genera_op_x_pago_x_empleao = q.Genera_op_x_pago_x_empleao,
                        IdTipo_op_acta_finiquito = q.IdTipo_op_acta_finiquito,
                        IdTipo_op_sueldo_por_pagar = q.IdTipo_op_sueldo_por_pagar,
                        IdTipo_op_prestamos = q.IdTipo_op_prestamos,
                        IdTipo_op_vacaciones = q.IdTipo_op_vacaciones,
                        Sueldo_basico = q.Sueldo_basico,
                        Porcentaje_aporte_patr = q.Porcentaje_aporte_patr,
                        Porcentaje_aporte_pers = q.Porcentaje_aporte_pers,
                        IdRubro_acta_finiquito = q.IdRubro_acta_finiquito,
                        EstadoCreacionPrestamos=q.EstadoCreacionPrestamos,
                        Porcentaje_anticipo=q.Porcentaje_anticipo
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool si_existe(int IdEmpresa)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_Parametros
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        return true;
                    else
                        return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_Parametros_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Parametros Entity = new ro_Parametros
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoCbte_AsientoSueldoXPagar =Convert.ToInt32( info.IdTipoCbte_AsientoSueldoXPagar),
                        Genera_op_por_acta_finiquito = info.Genera_op_por_acta_finiquito,
                        Genera_op_por_liq_vacaciones = info.Genera_op_por_liq_vacaciones,
                        Genera_op_por_prestamos = info.Genera_op_por_prestamos,
                        genera_op_x_pago = info.genera_op_x_pago,
                        Genera_op_x_pago_x_empleao = info.Genera_op_x_pago_x_empleao,
                        IdTipo_op_acta_finiquito=info.IdTipo_op_acta_finiquito,
                        IdTipo_op_sueldo_por_pagar=info.IdTipo_op_sueldo_por_pagar,
                        IdTipo_op_prestamos=info.IdTipo_op_prestamos,
                        IdTipo_op_vacaciones=info.IdTipo_op_vacaciones,
                        Sueldo_basico = info.Sueldo_basico,
                        Porcentaje_aporte_patr = info.Porcentaje_aporte_patr,
                        Porcentaje_aporte_pers = info.Porcentaje_aporte_pers,
                        IdRubro_acta_finiquito = info.IdRubro_acta_finiquito,
                        EstadoCreacionPrestamos=info.EstadoCreacionPrestamos,
                        Porcentaje_anticipo = info.Porcentaje_anticipo

                    };
                    Context.ro_Parametros.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_Parametros_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Parametros Entity = Context.ro_Parametros.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null)
                        return false;
                    Entity.IdTipoCbte_AsientoSueldoXPagar =Convert.ToInt32( info.IdTipoCbte_AsientoSueldoXPagar);
                    Entity.Genera_op_por_acta_finiquito = info.Genera_op_por_acta_finiquito;
                    Entity.Genera_op_por_liq_vacaciones = info.Genera_op_por_liq_vacaciones;
                    Entity.Genera_op_por_prestamos = info.Genera_op_por_prestamos;
                    Entity.genera_op_x_pago = info.genera_op_x_pago;
                    Entity.Genera_op_x_pago_x_empleao = info.Genera_op_x_pago_x_empleao;
                    Entity.IdTipo_op_acta_finiquito = info.IdTipo_op_acta_finiquito;
                    Entity.IdTipo_op_prestamos = info.IdTipo_op_prestamos;
                    Entity.IdTipo_op_sueldo_por_pagar = info.IdTipo_op_sueldo_por_pagar;
                    Entity.IdTipo_op_vacaciones = info.IdTipo_op_vacaciones;
                    Entity.Sueldo_basico = info.Sueldo_basico;
                    Entity.Porcentaje_aporte_pers = info.Porcentaje_aporte_pers;
                    Entity.Porcentaje_aporte_patr = info.Porcentaje_aporte_patr;
                    Entity.IdRubro_acta_finiquito = info.IdRubro_acta_finiquito;
                    Entity.genera_op_x_pago = info.genera_op_x_pago;
                    Entity.Genera_op_x_pago_x_empleao = info.Genera_op_x_pago_x_empleao;
                    Entity.EstadoCreacionPrestamos = info.EstadoCreacionPrestamos;
                    Entity.Porcentaje_anticipo = info.Porcentaje_anticipo;
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
