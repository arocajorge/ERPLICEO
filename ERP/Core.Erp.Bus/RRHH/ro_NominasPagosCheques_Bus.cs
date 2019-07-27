using Core.Erp.Data.RRHH;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Bus.Contabilidad;

namespace Core.Erp.Bus.RRHH
{
   public class ro_NominasPagosCheques_Bus
    {
        ro_NominasPagosCheques_Data odata = new ro_NominasPagosCheques_Data();
        cp_orden_pago_Bus bus_orden = new cp_orden_pago_Bus();
        ct_cbtecble_Bus bus_comprobante = new ct_cbtecble_Bus();
        ct_cbtecble_det_Bus bus_comprobante_det = new ct_cbtecble_det_Bus();
        public List<ro_NominasPagosCheques_Info> get_list(int IdEmpresa, DateTime Fechainicio, DateTime fechafin, bool estado)
        {
            try
            {
                return odata.get_list(IdEmpresa, Fechainicio, fechafin, estado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_NominasPagosCheques_Info get_info(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdArchivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_NominasPagosCheques_Info info)
        {
            try
            {

                 get_op_x_empleados(info);

                foreach (var item in info.detalle)
                {
                    bus_orden.guardarDB(item.info_orden_pago);
                    item.IdEmpresa_op = info.IdEmpresa;
                    item.IdOrdenPago = item.info_orden_pago.IdOrdenPago;
                    item.Secuancia_op = 1;
                    item.IdEmpresa_dc = info.IdEmpresa;
                    item.IdTipoCbte = item.info_orden_pago.info_comprobante.IdTipoCbte;
                    item.IdCbteCble = item.info_orden_pago.info_comprobante.IdCbteCble;

                }
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_NominasPagosCheques_Info info)
        {
            try
            {

                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_NominasPagosCheques_Info info)
        {
            try
            {
                foreach (var item in info.detalle)
                {
                    item.info_orden_pago = new cp_orden_pago_Info();
                    item.info_orden_pago.IdEmpresa = info.IdEmpresa;
                    item.info_orden_pago.IdOrdenPago = item.IdOrdenPago;
                    item.info_orden_pago.info_comprobante = bus_comprobante.get_info(item.IdEmpresa, item.IdTipoCbte, item.IdCbteCble);
                    item.info_orden_pago.info_comprobante.lst_ct_cbtecble_det = bus_comprobante_det.get_list(item.IdEmpresa, item.IdTipoCbte, item.IdCbteCble);
                    bus_orden.anularDB(item.info_orden_pago);
                }
                return odata.anularDB(info);
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        // generar ordenes de pagos
        public ro_NominasPagosCheques_Info get_op_x_empleados(ro_NominasPagosCheques_Info info)
        {
            try
            {
                ro_Parametros_Data data_param = new ro_Parametros_Data();
                var parametro = data_param.get_info(info.IdEmpresa);
                cp_orden_pago_Info info_orden = new cp_orden_pago_Info();
                ro_periodo_Bus bus_periodo = new ro_periodo_Bus();
                var periodo = bus_periodo.get_info(info.IdEmpresa, info.IdPeriodo);
                cp_orden_pago_tipo_x_empresa_Bus bus_tipo_op = new cp_orden_pago_tipo_x_empresa_Bus();
                cp_orden_pago_tipo_x_empresa_Info info_tipo_op = new cp_orden_pago_tipo_x_empresa_Info();
                info_tipo_op = bus_tipo_op.get_info(info.IdEmpresa, cl_enumeradores.eTipoOrdenPago.ANTI_EMPLE.ToString());
                info.detalle.ForEach(item =>

                {

                    info_orden=new cp_orden_pago_Info
                        {

                            IdEmpresa = item.IdEmpresa,
                            Observacion = "Cancelacion sueldo y salarios de " + item.pe_nombreCompleto,
                            IdTipo_op = cl_enumeradores.eTipoOrdenPago.ANTI_EMPLE.ToString(),
                            IdTipo_Persona = cl_enumeradores.eTipoPersona.EMPLEA.ToString(),
                            IdPersona = item.IdPersona,
                            IdEntidad =Convert.ToDecimal( item.IdEmpleado),
                            Fecha = DateTime.Now,
                            IdEstadoAprobacion = cl_enumeradores.eEstadoAprobacionOrdenPago.APRO.ToString(),
                            IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.CHEQUE.ToString(),
                            Estado = "A",
                            Fecha_Transac = DateTime.Now,
                            IdSucursal=item.IdSucursal,
                            detalle = new List<cp_orden_pago_det_Info>
                            {
                                new cp_orden_pago_det_Info
                                {
                                    IdEmpresa=item.IdEmpresa,
                                    Secuencia=1,
                                    Valor_a_pagar=item.Valor,
                                    Referencia="Periodo "+info.IdPeriodo,
                                    Fecha_Pago=periodo.pe_FechaFin,
                                    IdEstadoAprobacion = info_tipo_op.IdEstadoAprobacion,
                                    IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.CHEQUE.ToString(),
                                }
                            },
                            info_comprobante = new ct_cbtecble_Info
                            {
                                IdEmpresa = info.IdEmpresa,
                                cb_Fecha = DateTime.Now,

                                //REVISA CARLOS FALTA IDSUCURSAL

                                IdTipoCbte = Convert.ToInt32(info_tipo_op.IdTipoCbte_OP),
                                cb_Estado = "A",
                                IdPeriodo = Convert.ToInt32(periodo.pe_FechaFin.Year.ToString() + periodo.pe_FechaFin.Month.ToString().PadLeft(2, '0')),
                                cb_Observacion = "Cancelación de sueldo del " + info.IdPeriodo + " a " + item.pe_nombreCompleto,
                                lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>
                                {
                                    new ct_cbtecble_det_Info
                                    {
                                        IdEmpresa=item.IdEmpresa,
                                        IdTipoCbte = Convert.ToInt32(info_tipo_op.IdTipoCbte_OP),
                                        IdCtaCble=item.IdCtaCble_Emplea,
                                        dc_Valor=item.Valor,
                                        dc_Observacion="Cancelación de sueldo del "+periodo.IdPeriodo+" a "+item.pe_nombreCompleto,
                                        secuencia=1,
                                    },
                                    new ct_cbtecble_det_Info
                                    {
                                         IdEmpresa=item.IdEmpresa,
                                        IdTipoCbte = Convert.ToInt32(info_tipo_op.IdTipoCbte_OP),
                                        IdCtaCble=item.IdCtaCble_x_pagar_empleado,
                                        dc_Valor=item.Valor*-1,
                                        dc_Observacion="Cancelación de sueldo del "+info.IdPeriodo+" a "+item.pe_nombreCompleto,
                                        secuencia=2,
                                    }

                                }

                            }

                        };
                    item.info_orden_pago = info_orden;
                });
                return info;
            }
            catch (Exception)
            {


                throw;
            }
        }


    }
}
