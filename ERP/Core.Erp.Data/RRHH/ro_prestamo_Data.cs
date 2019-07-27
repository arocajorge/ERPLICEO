using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Data.Contabilidad;
using Core.Erp.Data.General;
using Core.Erp.Info.General;

namespace Core.Erp.Data.RRHH
{
  public  class ro_prestamo_Data
    {
        ro_Parametros_Data odata = new ro_Parametros_Data();
        cp_orden_pago_Data data_op = new cp_orden_pago_Data();
        ct_cbtecble_Data data_ct = new ct_cbtecble_Data();

        public List<ro_prestamo_Info> get_list_prestamo(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                List<ro_prestamo_Info> Lista;
                decimal IdEmpleadoIni = IdEmpleado;
                decimal IdEmpleadoFin = IdEmpleado==0 ? 9999999999999999 : IdEmpleado;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwRo_Prestamo
                             where q.IdEmpresa == IdEmpresa
                             && q.IdEmpleado >= IdEmpleadoIni
                             && q.IdEmpleado <= IdEmpleadoFin
                             select new ro_prestamo_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdPrestamo = q.IdPrestamo,
                                 descuento_mensual = q.descuento_mensual,
                                 descuento_men_quin = q.descuento_men_quin,
                                 descuento_quincena=q.descuento_quincena,
                                 IdEmpleado = q.IdEmpleado,
                                 IdRubro = q.IdRubro,
                                 Estado = q.Estado,
                                 Fecha = q.Fecha,
                                 MontoSol = q.MontoSol,
                                 NumCuotas = q.NumCuotas,
                                 Fecha_PriPago = q.Fecha_PriPago,
                                 Observacion = q.Observacion,
                                 IdTipoCbte = q.IdTipoCbte,
                                 IdCbteCble = q.IdCbteCble,
                                 IdOrdenPago = q.IdOrdenPago,
                                 pe_nombre_completo=q.pe_apellido+" "+q.pe_nombre,
                                 Valor_pendiente=q.Valor_pendiente,
                                 TotalCobrado=q.TotalCobrado,
                                 ru_descripcion=q.ru_descripcion,
                                 EstadoAprob = q.EstadoAprob,
                                 EstadoBool = q.Estado,
                                 Fecha_Transac = q.Fecha_Transac,
                                 EstadoCancelacion = q.EstadoCancelacion
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_prestamo_Info> get_list(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {


                List<ro_prestamo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwRo_Prestamo
                             where q.IdEmpresa == IdEmpresa
                             && q.IdEmpleado ==IdEmpleado
                             && q.Estado==true
                             select new ro_prestamo_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdPrestamo = q.IdPrestamo,
                                 descuento_mensual = q.descuento_mensual,
                                 descuento_men_quin = q.descuento_men_quin,
                                 descuento_quincena = q.descuento_quincena,
                                 IdEmpleado = q.IdEmpleado,
                                 IdRubro = q.IdRubro,
                                 Estado = q.Estado,
                                 Fecha = q.Fecha,
                                 MontoSol = q.MontoSol,
                                 NumCuotas = q.NumCuotas,
                                 Fecha_PriPago = q.Fecha_PriPago,
                                 Observacion = q.Observacion,
                                 IdTipoCbte = q.IdTipoCbte,
                                 IdCbteCble = q.IdCbteCble,
                                 IdOrdenPago = q.IdOrdenPago,
                                 pe_nombre_completo = q.pe_apellido + " " + q.pe_nombre,
                                 Valor_pendiente = q.Valor_pendiente,
                                 TotalCobrado = q.TotalCobrado,
                                 ru_descripcion = q.ru_descripcion,
                                 EstadoAprob = q.EstadoAprob,
                                 EstadoBool = q.Estado


                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_prestamo_Info> get_list_aprobacion(int IdEmpresa)
        {
            try
            {

                List<ro_prestamo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwRo_Prestamo
                             where q.IdEmpresa == IdEmpresa
                           
                             && q.EstadoAprob == "PEND"
                             select new ro_prestamo_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdPrestamo = q.IdPrestamo,
                                 descuento_mensual = q.descuento_mensual,
                                 descuento_men_quin = q.descuento_men_quin,
                                 descuento_quincena = q.descuento_quincena,
                                 IdEmpleado = q.IdEmpleado,
                                 IdRubro = q.IdRubro,
                                 Estado = q.Estado,
                                 EstadoAprob = q.EstadoAprob,
                                 Fecha = q.Fecha,
                                 MontoSol = q.MontoSol,
                                 NumCuotas = q.NumCuotas,
                                 Fecha_PriPago = q.Fecha_PriPago,
                                 Observacion = q.Observacion,
                                 IdTipoCbte = q.IdTipoCbte,
                                 IdCbteCble = q.IdCbteCble,
                                 IdOrdenPago = q.IdOrdenPago,
                                 pe_nombre_completo = q.pe_apellido + " " + q.pe_nombre,
                                 Valor_pendiente = q.Valor_pendiente,
                                 TotalCobrado = q.TotalCobrado,
                                 ru_descripcion = q.ru_descripcion,                                 
                                 EstadoBool = q.Estado
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }        

        public ro_prestamo_Info get_info(int IdEmpresa, decimal IdEmpleado, decimal IdPrestamo)
        {
            try
            {
                ro_prestamo_Info info = new ro_prestamo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_prestamo Entity = Context.ro_prestamo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado == IdEmpleado && q.IdPrestamo == IdPrestamo);
                    if (Entity == null) return null;

                    info = new ro_prestamo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPrestamo = Entity.IdPrestamo,
                        descuento_mensual = Entity.descuento_mensual,
                        descuento_men_quin = Entity.descuento_men_quin,
                        descuento_quincena = Entity.descuento_quincena,
                        IdEmpleado = Entity.IdEmpleado,
                        IdRubro = Entity.IdRubro,
                        Estado = Entity.Estado,
                        Fecha = Entity.Fecha,
                        MontoSol = Entity.MontoSol,
                        NumCuotas = Entity.NumCuotas,
                        Fecha_PriPago = Entity.Fecha_PriPago,
                        Observacion = Entity.Observacion,
                        IdTipoCbte = Entity.IdTipoCbte,
                        IdCbteCble = Entity.IdCbteCble,
                        IdOrdenPago = Entity.IdOrdenPago,
                        GeneraOP=Entity.GeneraOP
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_prestamo
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdPrestamo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_prestamo_Info info)
        {
            try
            {
                var ro_parametro = odata.get_info(info.IdEmpresa);
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_prestamo Entity = new ro_prestamo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPrestamo = info.IdPrestamo = get_id(info.IdEmpresa),
                        descuento_mensual = info.descuento_mensual,
                        descuento_men_quin = info.descuento_men_quin,
                        descuento_quincena = info.descuento_quincena,
                        IdEmpleado = info.IdEmpleado,
                        IdRubro = info.IdRubro,
                        Fecha = info.Fecha.Date,
                        MontoSol = info.MontoSol,
                        NumCuotas = info.NumCuotas,
                        Fecha_PriPago = info.Fecha_PriPago.Date,
                        Observacion = info.Observacion,
                        GeneraOP=info.GeneraOP,
                        Estado = info.Estado = true,
                        EstadoAprob = ro_parametro.EstadoCreacionPrestamos,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_prestamo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_prestamo_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(ro_prestamo_Info info)
        {
            try
            {
                var ro_parametro = odata.get_info(info.IdEmpresa);
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_prestamo Entity = Context.ro_prestamo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado && q.IdPrestamo == info.IdPrestamo);
                    if (Entity == null)
                        return false;
                        Entity.descuento_mensual = info.descuento_mensual;
                        Entity.descuento_men_quin = info.descuento_men_quin;
                        Entity.descuento_quincena = info.descuento_quincena;
                        Entity.IdEmpleado = info.IdEmpleado;
                        Entity.IdRubro = info.IdRubro;
                        Entity.Fecha = info.Fecha.Date;
                        Entity.MontoSol = info.MontoSol;
                        Entity.NumCuotas = info.NumCuotas;
                        Entity.Fecha_PriPago = info.Fecha_PriPago.Date;
                        Entity.Observacion = info.Observacion;
                        Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                        Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;
                        Entity.EstadoAprob = ro_parametro.EstadoCreacionPrestamos;
                    Entity.GeneraOP = info.GeneraOP;
                        Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_prestamo_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(ro_prestamo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_prestamo Entity = Context.ro_prestamo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado && q.IdPrestamo == info.IdPrestamo);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Abono(ro_prestamo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_prestamo Entity = Context.ro_prestamo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado && q.IdPrestamo == info.IdPrestamo);
                    if (Entity == null)
                        return false;
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    foreach (var item in info.lst_detalle)
                    {
                        ro_prestamo_detalle Entity_det = Context.ro_prestamo_detalle.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPrestamo == info.IdPrestamo
                        && q.NumCuota == item.NumCuota);
                        Entity_det.EstadoPago = item.EstadoPago;
                        Entity_det.Saldo = Entity_det.Saldo - item.ValorAplicado;
                        Context.SaveChanges();

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

        public bool aprobar_prestamo(int IdEmpresa, string[] Lista, string IdUsuarioAprueba)
        {
            Entities_rrhh Context = new Entities_rrhh();
            Entities_cuentas_por_pagar Context_cxp = new Entities_cuentas_por_pagar();
            Entities_contabilidad Context_ct = new Entities_contabilidad();

            try
            {



                ro_Parametros Entity_ro_parametros = Context.ro_Parametros.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                cp_orden_pago_tipo_x_empresa Entity_op_tipo = Context_cxp.cp_orden_pago_tipo_x_empresa.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipo_op == Entity_ro_parametros.IdTipo_op_prestamos).FirstOrDefault();


                decimal IdOrdenPago = 1;
                decimal IdCbteCble_OP = 1;

                foreach (var item in Lista)
                {

                    var IdPrestamo = Convert.ToDecimal(item);
                    ro_prestamo Entity_Prestamo = Context.ro_prestamo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdPrestamo == IdPrestamo);
                    if (Entity_Prestamo != null)
                    {
                        Entity_Prestamo.IdUsuarioAprueba = IdUsuarioAprueba;
                        Entity_Prestamo.EstadoAprob = "APROB";
                    }

                    if (Entity_Prestamo.GeneraOP)
                    {
                        
                            IdOrdenPago = data_op.get_id(Entity_Prestamo.IdEmpresa);
                            IdCbteCble_OP = data_ct.get_id(Entity_Prestamo.IdEmpresa, Convert.ToInt32(Entity_op_tipo.IdTipoCbte_OP));
                            ro_empleado Entity_Empleado = Context.ro_empleado.Where(q => q.IdEmpresa == Entity_Prestamo.IdEmpresa && q.IdEmpleado == Entity_Prestamo.IdEmpleado).FirstOrDefault();
                            var cuenta_x_rubro = Context.ro_rubro_tipo.Where(q => q.IdEmpresa == Entity_Prestamo.IdEmpresa && q.IdRubro == Entity_Prestamo.IdRubro).FirstOrDefault();

                            if (
                               Entity_Empleado.IdCtaCble_x_pagar_empleado!=null &&
                                Entity_Empleado.IdCtaCble_Emplea != null
                                )
                            {
                                cp_orden_pago op = new cp_orden_pago
                                {
                                    IdEmpresa = IdEmpresa,
                                    IdSucursal = Entity_Empleado.IdSucursal,
                                    IdOrdenPago = IdOrdenPago,
                                    Observacion = "Prestamo #" + Entity_Prestamo.IdPrestamo,
                                    IdTipo_op = Entity_ro_parametros.IdTipo_op_prestamos,
                                    IdTipo_Persona = cl_enumeradores.eTipoPersona.EMPLEA.ToString(),
                                    IdPersona = Entity_Empleado.IdPersona,
                                    IdEntidad = Entity_Prestamo.IdEmpleado,
                                    Fecha = DateTime.Now.Date,
                                    IdEstadoAprobacion = Entity_op_tipo.IdEstadoAprobacion,
                                    IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.CHEQUE.ToString(),
                                    Estado = "A"
                                };

                                Entity_Prestamo.IdEmpresa_op = op.IdEmpresa;
                                Entity_Prestamo.IdOrdenPago = op.IdOrdenPago;

                                Context_cxp.cp_orden_pago.Add(op);

                                ct_cbtecble diario = new ct_cbtecble
                                {
                                    IdEmpresa = IdEmpresa,
                                    IdTipoCbte = Convert.ToInt32(Entity_op_tipo.IdTipoCbte_OP),
                                    IdCbteCble = IdCbteCble_OP,
                                    cb_Fecha = DateTime.Now.Date,
                                    cb_Observacion = op.Observacion,
                                    IdPeriodo = Convert.ToInt32(DateTime.Now.Date.ToString("yyyyMM")),
                                    IdSucursal = Entity_Empleado.IdSucursal,
                                    cb_FechaTransac = DateTime.Now,
                                    cb_Estado = "A"
                                };

                                Entity_Prestamo.IdEmpresa_dc = diario.IdEmpresa;
                                Entity_Prestamo.IdTipoCbte = diario.IdTipoCbte;
                                Entity_Prestamo.IdCbteCble = diario.IdCbteCble;

                                Context_ct.ct_cbtecble.Add(diario);

                                ct_cbtecble_det diario_det = new ct_cbtecble_det
                                {
                                    IdEmpresa = diario.IdEmpresa,
                                    IdTipoCbte = diario.IdTipoCbte,
                                    IdCbteCble = diario.IdCbteCble,
                                    secuencia = 1,
                                    IdCtaCble = Entity_Empleado.IdCtaCble_Emplea,
                                    dc_Valor = Math.Round(Convert.ToDouble(Entity_Prestamo.MontoSol), 2, MidpointRounding.AwayFromZero),
                                };

                                Context_ct.ct_cbtecble_det.Add(diario_det);

                                ct_cbtecble_det diario_det_ = new ct_cbtecble_det
                                {
                                    IdEmpresa = diario.IdEmpresa,
                                    IdTipoCbte = diario.IdTipoCbte,
                                    IdCbteCble = diario.IdCbteCble,
                                    secuencia = 2,
                                    IdCtaCble = Entity_Empleado.IdCtaCble_x_pagar_empleado,
                                    dc_Valor = Math.Round(Convert.ToDouble(Entity_Prestamo.MontoSol), 2, MidpointRounding.AwayFromZero) * -1
                                };

                                Context_ct.ct_cbtecble_det.Add(diario_det_);

                                cp_orden_pago_det op_det = new cp_orden_pago_det
                                {
                                    IdEmpresa = op.IdEmpresa,
                                    IdOrdenPago = op.IdOrdenPago,
                                    Secuencia = 1,

                                    IdEmpresa_cxp = diario.IdEmpresa,
                                    IdTipoCbte_cxp = diario.IdTipoCbte,
                                    IdCbteCble_cxp = diario.IdCbteCble,

                                    Valor_a_pagar = Convert.ToDouble(Entity_Prestamo.MontoSol),
                                    IdEstadoAprobacion = Entity_op_tipo.IdEstadoAprobacion,
                                    IdFormaPago = cl_enumeradores.eFormaPagoOrdenPago.CHEQUE.ToString(),
                                    Fecha_Pago = op.Fecha
                                };

                                Context_cxp.cp_orden_pago_det.Add(op_det);
                            
                        }
                    
                   
                }
                    Context_ct.SaveChanges();
                    Context_cxp.SaveChanges();
                    Context.SaveChanges();
                }

                Context_ct.Dispose();
                Context_cxp.Dispose();
                Context.Dispose();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ro_prestamo_Info get_info(int IdEmpresa, decimal IdPrestamo)
        {
            try
            {
                ro_prestamo_Info info = new ro_prestamo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    vwRo_Prestamo Entity = Context.vwRo_Prestamo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdPrestamo == IdPrestamo);
                    if (Entity == null) return null;

                    info = new ro_prestamo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPrestamo = Entity.IdPrestamo,
                        descuento_mensual = Entity.descuento_mensual,
                        descuento_men_quin = Entity.descuento_men_quin,
                        descuento_quincena = Entity.descuento_quincena,
                        IdEmpleado = Entity.IdEmpleado,
                        IdRubro = Entity.IdRubro,
                        Estado = Entity.Estado,
                        Fecha = Entity.Fecha,
                        MontoSol = Entity.MontoSol,
                        NumCuotas = Entity.NumCuotas,
                        Fecha_PriPago = Entity.Fecha_PriPago,
                        Observacion = Entity.Observacion,
                        IdTipoCbte = Entity.IdTipoCbte,
                        IdCbteCble = Entity.IdCbteCble,
                        IdOrdenPago = Entity.IdOrdenPago,
                        IdCtaCble_Emplea = Entity.IdCtaCble_Emplea,
                        IdCtaCble_x_pagar_empleado = Entity.IdCtaCble_x_pagar_empleado,
                        rub_ctacon = Entity.rub_ctacon,
                        GeneraOP=Entity.GeneraOP
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
