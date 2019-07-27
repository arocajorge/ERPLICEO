using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_NominasPagosCheques_det_Data
    {
        public List<ro_NominasPagosCheques_det_Info> get_list(int IdEmpresa, decimal IdTransaccion)
        {
            try
            {
                List<ro_NominasPagosCheques_det_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_NominasPagosCheques_det
                             where q.IdEmpresa == IdEmpresa
                                   && q.IdTransaccion == IdTransaccion
                             select new ro_NominasPagosCheques_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTransaccion = q.IdTransaccion,
                                 IdEmpleado = q.IdEmpleado,
                                 IdSucursal = q.IdSucursal,
                                 Valor = q.Valor,
                                 ValorCancelado = q.Valor,
                                 em_tipoCta = q.em_tipoCta,
                                 em_NumCta = q.em_NumCta,
                                 pe_apellido = q.pe_apellido,
                                 pe_nombre = q.pe_nombre,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_apellido + " " + q.pe_nombre,
                                 IdEmpresa_op=q.IdEmpresa_op,
                                 IdOrdenPago=q.IdOrdenPago,
                                 Secuancia_op=q.Secuancia_op,
                                 IdEmpresa_dc=q.IdEmpresa_dc,
                                 IdTipoCbte=q.IdTipoCbte,
                                 IdCbteCble=q.IdCbteCble


                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_NominasPagosCheques_det_Info> get_list(int IdEmpresa, int IdNominaTipo, int IdNominaTipoLiqui, int IdPeriodo, string Tipocta)
        {
            try
            {
                List<ro_NominasPagosCheques_det_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwRo_rol_detalle_saldo_por_pagar
                             where q.IdEmpresa == IdEmpresa
                                   && q.IdEmpresa == IdEmpresa
                                   && q.IdNominaTipo == IdNominaTipo
                                   && q.IdNominaTipoLiqui == IdNominaTipoLiqui
                                   && q.IdPeriodo == IdPeriodo
                                   && Tipocta.Contains(q.em_tipoCta)
                                   && q.Saldo > 0
                             select new ro_NominasPagosCheques_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 pe_nombreCompleto = q.pe_apellido+" "+q.pe_nombre,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 Valor = q.ValorGanado,
                                 ValorCancelado = q.ValorCancelado,
                                 Saldo = q.Saldo,
                                 IdSucursal = q.IdSucursal,
                                 em_NumCta = q.em_NumCta,
                                 em_tipoCta = q.em_tipoCta,
                                 IdPersona=q.IdPersona,
                                 IdCtaCble_Emplea=q.IdCtaCble_Emplea,
                                 IdCtaCble_x_pagar_empleado = q.IdCtaCble_x_pagar_empleado


                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
