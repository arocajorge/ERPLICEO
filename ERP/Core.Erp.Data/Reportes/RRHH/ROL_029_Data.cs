using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.RRHH
{
    public class ROL_029_Data
    {
        public List<ROL_029_Info> get_list(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                List<ROL_029_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWROL_029
                             where (q.IdEmpresa == IdEmpresa
                             && q.IdAjuste == IdAjuste)
                             select new ROL_029_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdAjuste = q.IdAjuste,
                                 IdAnio = q.IdAnio,
                                 IdSucursal = q.IdSucursal,
                                 Fecha = q.Fecha,
                                 FechaCorte = q.FechaCorte,
                                 Observacion = q.Observacion,
                                 Estado = q.Estado,
                                 Secuencia = q.Secuencia,
                                 IdEmpleado = q.IdEmpleado,
                                 SueldoProyectado = q.SueldoProyectado,
                                 SueldoFechaCorte = q.SueldoFechaCorte,
                                 OtrosIngresos = q.OtrosIngresos,
                                 IngresosLiquidos = q.IngresosLiquidos,
                                 GastosPersonales = q.GastosPersonales,
                                 AporteFechaCorte = q.AporteFechaCorte,
                                 BaseImponible = q.BaseImponible,
                                 FraccionBasica = q.FraccionBasica,
                                 Excedente = q.Excedente,
                                 ImpuestoFraccionBasica = q.ImpuestoFraccionBasica,
                                 ImpuestoRentaCausado = q.ImpuestoRentaCausado,
                                 DescontadoFechaCorte = q.DescontadoFechaCorte,
                                 LiquidacionFinal = q.LiquidacionFinal,
                                 pe_nombreCompleto = q.pe_nombreCompleto

                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ROL_029_Info> get_list_detalle_oi(int IdEmpresa, decimal IdAjuste, int IdEmpleado)
        {
            try
            {
                List<ROL_029_Info> Lista_OI;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista_OI = (from q in Context.ro_AjusteImpuestoRentaDetOI
                                      where (q.IdEmpresa == IdEmpresa
                                     && q.IdAjuste == IdAjuste
                                      && q.IdEmpleado == IdEmpleado
                                     )
                                      select new ROL_029_Info
                                      {
                                          IdEmpresa = q.IdEmpresa,
                                          IdAjuste = q.IdAjuste,
                                          Secuencia = q.Secuencia,
                                          IdEmpleado = q.IdEmpleado,
                                          DescripcionOI = q.DescripcionOI,
                                          Valor = q.Valor
                                      }).ToList();
                }

                return Lista_OI;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
