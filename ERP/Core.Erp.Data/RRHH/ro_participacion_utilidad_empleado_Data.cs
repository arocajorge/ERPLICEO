using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
  public  class ro_participacion_utilidad_empleado_Data
    {
        public List<ro_participacion_utilidad_empleado_Info> get_list(int IdEmpresa, int IdNomina, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<ro_participacion_utilidad_empleado_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.spro_nomina_x_pago_utilidad(IdEmpresa, IdNomina, FechaInicio, FechaFin)
                             where q.IdEmpresa == IdEmpresa
                             select new ro_participacion_utilidad_empleado_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado=q.IdEmpleado,
                                 em_status=q.em_status,
                                 ca_descripcion=q.ca_descripcion,
                                 pe_nombre=q.pe_nombre,
                                 pe_apellido=q.pe_apellido,
                                 pe_cedulaRuc=q.pe_cedulaRuc,
                                 CargasFamiliares=q.num_cargas,
                                 num_contratos=q.num_contratos,
                                 em_fechaIngaRol=q.em_fechaIngaRol,
                                 em_fechaSalida=q.em_fechaSalida,
                                 
                                  

                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_participacion_utilidad_empleado_Info> get_list(int IdEmpresa, int IdUtilidad)
        {
            try
            {
                List<ro_participacion_utilidad_empleado_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_participacion_utilidad_empleado
                             where q.IdEmpresa == IdEmpresa
                             && q.IdUtilidad== IdUtilidad
                             select new ro_participacion_utilidad_empleado_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdUtilidad = q.IdUtilidad,
                                 IdNomina = q.IdNomina,
                                 IdNominaTipo_liq=q.IdNominaTipo_liq,
                                 IdPeriodo=q.IdPeriodo,
                                 IdEmpleado = q.IdEmpleado,
                                 em_status = q.em_status,
                                 ca_descripcion = q.ca_descripcion,
                                 pe_nombre = q.pe_nombre,
                                 pe_apellido = q.pe_apellido,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 CargasFamiliares = q.CargasFamiliares,
                                 ValorCargaFamiliar=q.ValorCargaFamiliar,
                                 ValorExedenteIESS=q.ValorExedenteIESS,
                                 ValorIndividual=q.ValorIndividual,
                                 ValorTotal=q.ValorTotal,
                                 DiasTrabajados=q.DiasTrabajados,
                                 em_fechaIngaRol=q.em_fechaIngaRol,
                                 em_fechaSalida=q.em_fechaSalida,
                                 em_fecha_ingreso=q.em_fecha_ingreso,
                                 UtilidadDerechoIndividual=q.UtilidadDerechoIndividual,
                                 UtilidadCargaFamiliar=q.UtilidadCargaFamiliar

                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_participacion_utilidad_empleado_Info> get_list(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo)
        {
            try
            {
                List<ro_participacion_utilidad_empleado_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_participacion_utilidad_empleado
                             where q.IdEmpresa == IdEmpresa
                             && q.IdNomina == IdNomina
                             && q.IdNominaTipo_liq==IdNominaTipo
                             && q.IdPeriodo==IdPeriodo
                             select new ro_participacion_utilidad_empleado_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdNomina=q.IdNomina,
                                 IdNominaTipo_liq=q.IdNominaTipo_liq,
                                 IdPeriodo=q.IdPeriodo,
                                 IdUtilidad = q.IdUtilidad,
                                 IdEmpleado = q.IdEmpleado,
                                 em_status = q.em_status,
                                 ca_descripcion = q.ca_descripcion,
                                 pe_nombre = q.pe_nombre,
                                 pe_apellido = q.pe_apellido,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 CargasFamiliares = q.CargasFamiliares,
                                 ValorCargaFamiliar = q.ValorCargaFamiliar,
                                 ValorExedenteIESS = q.ValorExedenteIESS,
                                 ValorIndividual = q.ValorIndividual,
                                 ValorTotal = q.ValorTotal,
                                 DiasTrabajados = q.DiasTrabajados,
                                 em_fechaIngaRol = q.em_fechaIngaRol,
                                 em_fechaSalida = q.em_fechaSalida,
                                 em_fecha_ingreso = q.em_fecha_ingreso

                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<ro_participacion_utilidad_empleado_Info> lista)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    foreach (var info in lista)
                    {
                        ro_participacion_utilidad_empleado Entity = new ro_participacion_utilidad_empleado
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdUtilidad = info.IdUtilidad,
                            IdEmpleado=info.IdEmpleado,
                            CargasFamiliares=info.CargasFamiliares,
                            ValorCargaFamiliar=info.ValorCargaFamiliar,
                            ValorExedenteIESS=info.ValorExedenteIESS,
                            ValorIndividual=info.ValorIndividual,
                            ValorTotal=info.ValorTotal,
                            DiasTrabajados=info.DiasTrabajados
                        };
                        Context.ro_participacion_utilidad_empleado.Add(Entity);
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

        public bool anularDB( int IdEmpresa, int IdUtilidad)
        {
            try
            {
                using (Entities_rrhh Contex=new Entities_rrhh())
                {
                    string sql = "delete ro_participacion_utilidad_empleado where IdEmpresa='"+IdEmpresa+ "' and IdUtilidad='"+IdUtilidad+"'";
                    Contex.Database.ExecuteSqlCommand(sql);
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
