using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
    public class ro_AjusteImpuestoRentaDet_Data
    {
        public List<ro_AjusteImpuestoRentaDet_Info> get_list(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                List<ro_AjusteImpuestoRentaDet_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        Lista = (from q in Context.vwro_AjusteImpuestoRentaDet
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdAjuste == IdAjuste
                                 orderby q.pe_nombreCompleto ascending
                                 select new ro_AjusteImpuestoRentaDet_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdAjuste = q.IdAjuste,
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
                                     ImpuestoFraccionBasica= q.ImpuestoFraccionBasica,
                                     ImpuestoRentaCausado =q.ImpuestoRentaCausado,
                                     DescontadoFechaCorte = q.DescontadoFechaCorte,
                                     LiquidacionFinal = q.LiquidacionFinal,
                                     pe_nombreCompleto = q.pe_nombreCompleto
                                 }).ToList();

                    Lista.ForEach(q => q.IdString = q.IdEmpresa.ToString("000") + q.IdAjuste.ToString("000000") + q.Secuencia.ToString("000000") + q.IdEmpleado.ToString("000000"));
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_AjusteImpuestoRentaDet_Info get_info(int IdEmpresa, int IdAjuste, int Secuencia)
        {
            try
            {
                ro_AjusteImpuestoRentaDet_Info info = new ro_AjusteImpuestoRentaDet_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_AjusteImpuestoRentaDet Entity = Context.ro_AjusteImpuestoRentaDet.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdAjuste == IdAjuste);
                    if (Entity == null) return null;

                    info = new ro_AjusteImpuestoRentaDet_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAjuste = Entity.IdAjuste,
                        Secuencia = Entity.Secuencia,
                        IdEmpleado = Entity.IdEmpleado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
