using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_rol_detalle_x_rubro_acumulado_Data
    {
        public double get_valor_x_rubro_acumulado(int IdEmpresa, decimal IdEmpleado, string IdRubro)
        {
            try
            {
                double valor_cuotas = 0;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                 var   datos = (from q in Context.ro_rol_detalle_x_rubro_acumulado                                  
                                    where q.IdEmpresa == IdEmpresa
                                          & q.IdEmpleado == IdEmpleado
                                          && q.IdRubro==IdRubro
                                          && q.Estado=="PEN"
                                    select q.Valor);
                    if (datos.Count() > 0)
                        valor_cuotas = datos.Sum();
                }

                return valor_cuotas;
            }
            catch (Exception )
            {

                throw;
            }
        }


        public double get_vac_x_mes_x_anio(int IdEmpresa, decimal IdEmpleado,int Anio, int mes)
        {
            try
            {
                double valor_cuotas = 0;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var datos = (from q in Context.ro_rol
                                 join r in Context.ro_rol_detalle_x_rubro_acumulado
                               on new { q.IdEmpresa, q.IdRol } equals new { r.IdEmpresa, r.IdRol }
                                 join p in Context.ro_periodo
                                 on new { q.IdEmpresa, q.IdPeriodo} equals new { p.IdEmpresa,p.IdPeriodo}
                                
                                 where q.IdEmpresa == IdEmpresa
                                       & r.IdEmpleado == IdEmpleado
                                        &p.pe_anio==Anio
                                        && p.pe_mes==mes
                                       && r.IdRubro == "18"
                                       && r.Estado == "PEN"
                                 select r.Valor);
                    if (datos.Count() > 0)
                        valor_cuotas = datos.Sum();
                }

                return valor_cuotas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_rol_detalle_x_rubro_acumulado_Info> GetList_BeneficiosSociales (int IdEmpresa, int IdSucursal, int IdNomina_Tipo, string IdRubro, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<ro_rol_detalle_x_rubro_acumulado_Info> Lista;

                int IdSucursalInicio = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                int IdNomina_TipoInicio = IdNomina_Tipo;
                int IdNomina_TipoFin = IdNomina_Tipo == 0 ? 9999 : IdNomina_Tipo;

                int Secuencial = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = Context.vwro_rol_detalle_x_rubro_acumulado.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdSucursal >= IdSucursalInicio
                    && q.IdSucursal <= IdSucursalFin
                    && q.IdNominaTipo >= IdNomina_TipoInicio
                    && q.IdNominaTipo <= IdNomina_TipoFin
                    && q.IdRubro == IdRubro
                    && q.pe_FechaIni >= FechaIni
                    && q.pe_FechaFin <= FechaFin).OrderBy(q=> q.Empleado).Select(q => new ro_rol_detalle_x_rubro_acumulado_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdNominaTipo = q.IdNominaTipo,
                            IdRol = q.IdRol,
                            IdRubro = q.IdRubro,
                            pe_FechaIni = q.pe_FechaIni,
                            pe_FechaFin = q.pe_FechaFin,
                            Valor = q.Valor,
                            IdEmpleado = q.IdEmpleado,
                            Empleado = q.Empleado                            
                        }).ToList();                                       
                }                

                Lista.ForEach(q => q.Secuencial = Secuencial++);

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarBD(List<ro_rol_detalle_x_rubro_acumulado_Info> Lista)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {                    
                    foreach (var item in Lista)
                    {
                        ro_rol_detalle_x_rubro_acumulado entity = Context.ro_rol_detalle_x_rubro_acumulado.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdSucursal == item.IdSucursal 
                        && q.IdRubro == item.IdRubro && q.IdRol== item.IdRol && q.IdEmpleado == item.IdEmpleado).FirstOrDefault();

                        if (entity == null)
                        {
                            return false;
                        }

                        entity.Valor = item.ValorAjustado;                            
                        Context.SaveChanges();
                    }                    
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
