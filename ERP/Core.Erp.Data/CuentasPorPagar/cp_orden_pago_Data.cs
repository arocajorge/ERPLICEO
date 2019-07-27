using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.Helps;
using Core.Erp.Data.General;
using Core.Erp.Info.General;

namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_orden_pago_Data
    {
        public List<cp_orden_pago_Info> get_list(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin, int IdSucursal)
        {
            try
            {
                List<cp_orden_pago_Info> Lista;
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_orden_pago
                             where IdEmpresa == q.IdEmpresa
                             && Fecha_ini <= q.Fecha
                             && q.Fecha <= Fecha_fin
                             && IdSucursalIni <= q.IdSucursal
                             && q.IdSucursal <= IdSucursalFin
                             orderby q.IdOrdenPago descending
                             select new cp_orden_pago_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 Su_Descripcion = q.Su_Descripcion,
                                 IdOrdenPago = q.IdOrdenPago,
                                 Observacion = q.Observacion,
                                 IdTipo_op = q.IdTipo_op,
                                 IdTipo_Persona = q.IdTipo_Persona,
                                 IdPersona = q.IdPersona,
                                 IdEntidad = q.IdEntidad,
                                 Fecha = q.Fecha,
                                 IdEstadoAprobacion = q.IdEstadoAprobacion,
                                 Descripcion = q.Descripcion,
                                 IdFormaPago = q.IdFormaPago,
                                 Estado =q.Estado,
                                 Nom_Beneficiario=q.pe_nombreCompleto,
                                 Total_OP=q.Total_OP,

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

        public List<cp_orden_pago_Info> get_list_aprobacion(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin, int IdSucursal)
        {
            try
            {
                List<cp_orden_pago_Info> Lista;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                        Lista = (from q in Context.vwcp_orden_pago
                                 where IdEmpresa == q.IdEmpresa
                                 && q.Fecha >= Fecha_ini
                                 && q.Fecha <= Fecha_fin
                                 && q.IdSucursal == IdSucursal
                                 && q.IdEstadoAprobacion == cl_enumeradores.eEstadoAprobacionOrdenPago.PENDI.ToString()
                                 && q.Estado == "A"
                                 select new cp_orden_pago_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     Su_Descripcion = q.Su_Descripcion,
                                     IdOrdenPago = q.IdOrdenPago,
                                     Observacion = q.Observacion,
                                     IdTipo_op = q.IdTipo_op,
                                     IdTipo_Persona = q.IdTipo_Persona,
                                     IdPersona = q.IdPersona,
                                     IdEntidad = q.IdEntidad,
                                     Fecha = q.Fecha,
                                     IdEstadoAprobacion = q.IdEstadoAprobacion,
                                     Descripcion = q.Descripcion,
                                     IdFormaPago = q.IdFormaPago,
                                     Estado = q.Estado,
                                     Nom_Beneficiario = q.pe_nombreCompleto,
                                     Total_OP = q.Total_OP,
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

        public cp_orden_pago_Info get_info(int IdEmpresa, decimal IdOrdenPago)
        {
            try
            {
                cp_orden_pago_Info info = new cp_orden_pago_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago Entity = Context.cp_orden_pago.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdOrdenPago == IdOrdenPago);
                    if (Entity == null) return null;
                    info = new cp_orden_pago_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdOrdenPago = Entity.IdOrdenPago,
                        Observacion = Entity.Observacion,
                        IdTipo_op = Entity.IdTipo_op,
                        IdTipo_Persona = Entity.IdTipo_Persona,
                        IdPersona = Entity.IdPersona,
                        IdEntidad = Entity.IdEntidad,
                        Fecha = Entity.Fecha,
                        IdEstadoAprobacion = Entity.IdEstadoAprobacion,
                        IdFormaPago = Entity.IdFormaPago,
                        Estado = Entity.Estado,
                        IdSucursal = Entity.IdSucursal,
                        ReferenciaGen = Entity.ReferenciaGen
                    };
                    info.detalle
                          = (from q in Context.cp_orden_pago_det
                             where q.IdOrdenPago == info.IdOrdenPago
                             && q.IdEmpresa == IdEmpresa
                             select new cp_orden_pago_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenPago = q.IdOrdenPago,
                                 Secuencia = q.Secuencia,
                                 IdEmpresa_cxp = q.IdEmpresa_cxp,
                                 IdCbteCble_cxp = q.IdCbteCble_cxp,
                                 IdTipoCbte_cxp = q.IdTipoCbte_cxp,
                                 Valor_a_pagar = q.Valor_a_pagar,
                                 Referencia = q.Referencia,
                                 IdFormaPago = q.IdFormaPago,
                                 Fecha_Pago = q.Fecha_Pago,
                                 IdEstadoAprobacion = q.IdEstadoAprobacion,
                                 IdBanco = q.IdBanco,
                                 IdUsuario_Aprobacion = q.IdUsuario_Aprobacion,
                                 fecha_hora_Aproba = q.fecha_hora_Aproba,
                                 Motivo_aproba=q.Motivo_aproba
                                 
                             }).ToList();
                }
                if (info.detalle != null)
                {
                    if (info.detalle.Count > 0)
                    {
                        info.info_comprobante.IdTipoCbte =Convert.ToInt32( info.detalle.FirstOrDefault().IdTipoCbte_cxp);
                        info.info_comprobante.IdCbteCble =Convert.ToDecimal( info.detalle.FirstOrDefault().IdCbteCble_cxp);
                    }
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
                decimal ID = 0;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    var lst = from q in Context.cp_orden_pago
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() == 0)
                    {
                        ID = 1;
                    }
                    else
                    {
                        ID = lst.Max(q => q.IdOrdenPago) + 1;
                    }
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean guardarDB(cp_orden_pago_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {


                    cp_orden_pago Entity = new cp_orden_pago
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdOrdenPago =info.IdOrdenPago= get_id(info.IdEmpresa),
                        Observacion = info.Observacion,
                        IdTipo_op = info.IdTipo_op,
                        IdTipo_Persona = info.IdTipo_Persona,
                        IdPersona = info.IdPersona,
                        IdEntidad = info.IdEntidad,
                        Fecha = info.Fecha.Date,
                        IdEstadoAprobacion = info.IdEstadoAprobacion,
                        IdFormaPago = info.IdFormaPago,
                        Estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now,
                        ReferenciaGen = info.ReferenciaGen
                    };

                    Context.cp_orden_pago.Add(Entity);
              

                    cp_orden_pago_det_Data oData_det = new cp_orden_pago_det_Data();
                    foreach (var item in info.detalle)
                    {
                    cp_orden_pago_det Entity_det= new cp_orden_pago_det
                        {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpresa_cxp=info.IdEmpresa,
                        IdOrdenPago = info.IdOrdenPago,
                        IdFormaPago = info.IdFormaPago,
                        Secuencia = 1,
                        IdTipoCbte_cxp = (item.IdTipoCbte_cxp==0| item.IdTipoCbte_cxp == null)? info.info_comprobante.IdTipoCbte:item.IdTipoCbte_cxp,
                        IdCbteCble_cxp = (item.IdCbteCble_cxp == 0 | item.IdCbteCble_cxp == null) ? info.info_comprobante.IdCbteCble : item.IdCbteCble_cxp,
                        Fecha_Pago =info.Fecha,
                        IdEstadoAprobacion=info.IdEstadoAprobacion,
                        Valor_a_pagar=(item.Valor_a_pagar)==0?info.Valor_a_pagar:item.Valor_a_pagar,
                        Referencia=item.Referencia

                    };
                        if (item.Referencia == null)
                        {
                            if (info.Observacion.Length > 50)
                                Entity_det.Referencia = info.Observacion.Substring(0, 49);
                            else
                                Entity_det.Referencia = info.Observacion;
                          };
                        Context.cp_orden_pago_det.Add(Entity_det);
                    }
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_orden_pago_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public Boolean modificarDB(cp_orden_pago_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago Entity = Context.cp_orden_pago.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdOrdenPago == info.IdOrdenPago);
                    if (Entity != null)
                    {
                        Entity.Observacion = info.Observacion;
                        Entity.IdTipo_op = info.IdTipo_op;
                        Entity.IdTipo_Persona = info.IdTipo_Persona;
                        Entity.IdPersona = info.IdPersona;
                        Entity.IdEntidad = info.IdEntidad;
                        Entity.Fecha = info.Fecha.Date;
                        Entity.IdEstadoAprobacion = info.IdEstadoAprobacion;
                        Entity.IdFormaPago = info.IdFormaPago;
                        Entity.IdUsuarioUltMod = info.IdUsuario;
                        Entity.IdSucursal = info.IdSucursal;
                        Entity.ReferenciaGen = info.ReferenciaGen;
                        Context.SaveChanges();        
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cp_orden_pago_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public Boolean anularDB(cp_orden_pago_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago Entity = Context.cp_orden_pago.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdOrdenPago == info.IdOrdenPago);
                    if (Entity != null)
                    {
                        Entity.Estado = "I";
                        Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                        Entity.Fecha_UltAnu = info.Fecha_UltAnu=DateTime.Now;
                        Entity.MotivoAnu = info.MotivoAnu;
                        Entity.MotivoAprobacion = info.MotivoAnu;
                        Entity.IdEstadoAprobacion = cl_enumeradores.eEstadoAprobacionOrdenPago.REPRO.ToString();

                        cp_orden_pago_det Entity_de = Context.cp_orden_pago_det.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdOrdenPago == info.IdOrdenPago);
                        if (Entity != null)
                        {
                            Entity_de.IdEmpresa_cxp = null;
                            Entity_de.IdTipoCbte_cxp = null;
                            Entity_de.IdCbteCble_cxp = null;
                        }
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

        public bool aprobarOP(int IdEmpresa, string[] Lista, string MotivoAprobacion, string IdFormaPago, string IdUsuarioAprobacion)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    foreach (var item in Lista)
                    {
                        var IdOrdenPagoAprobacion = Convert.ToDecimal(item);
                        cp_orden_pago Entity = Context.cp_orden_pago.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdOrdenPago == IdOrdenPagoAprobacion);
                        if (Entity != null)
                        {
                            if (IdFormaPago!="")
                            {
                                Entity.IdFormaPago = IdFormaPago;
                            }

                            Entity.IdEstadoAprobacion = cl_enumeradores.eEstadoAprobacionOrdenPago.APRO.ToString();
                            Entity.MotivoAprobacion = MotivoAprobacion;
                            Entity.IdUsuarioAprobacion = IdUsuarioAprobacion;
                            Entity.FechaAprobacion = DateTime.Now;
                        }
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

        public bool rechazarOP(int IdEmpresa, string[] Lista, string MotivoAprobacion, string IdUsuarioAprobacion)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    foreach (var item in Lista)
                    {
                        var IdOrdenPagoAprobacion = Convert.ToDecimal(item);
                        cp_orden_pago Entity = Context.cp_orden_pago.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdOrdenPago == IdOrdenPagoAprobacion);
                        if (Entity != null)
                        {
                            Entity.IdEstadoAprobacion = cl_enumeradores.eEstadoAprobacionOrdenPago.REPRO.ToString();
                            Entity.MotivoAprobacion = MotivoAprobacion;
                            Entity.IdUsuarioAprobacion = IdUsuarioAprobacion;
                            Entity.FechaAprobacion = DateTime.Now;

                            Entity.Estado = "I";

                            Entity.IdUsuarioUltAnu = IdUsuarioAprobacion;
                            Entity.Fecha_UltAnu = DateTime.Now;
                            Entity.MotivoAnu = MotivoAprobacion;

                        }
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

        public Boolean modificar_estado_aprobacion(cp_orden_pago_Info info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_orden_pago Entity = Context.cp_orden_pago.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdOrdenPago == info.IdOrdenPago);
                    if (Entity != null)
                    {
                        Entity.IdEstadoAprobacion = info.IdEstadoAprobacion;
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
        
        public List<cp_orden_pago_det_Info> Get_List_orden_pago_con_saldo(int IdEmpresa, string IdTipo_op, decimal IdProveedor, string IdEstado_Aprobacion, string IdUsuario, int IdSucursal)
        {
            try
            {

                List<cp_orden_pago_det_Info> Lista = new List<cp_orden_pago_det_Info>();

                using (Entities_cuentas_por_pagar Contex= new Entities_cuentas_por_pagar())
                {

                    try
                    {
                        Lista = (from q in Contex.spcp_Get_Data_orden_pago_con_cancelacion_data(IdEmpresa, 1, 99999999, "PROVEE", IdProveedor, IdProveedor, IdEstado_Aprobacion, IdUsuario,IdSucursal, false, false)
                                 where q.IdTipo_op == IdTipo_op
                                 select new
                                 cp_orden_pago_det_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdOrdenPago = q.IdOrdenPago,
                                     IdTipoCbte_cxp=q.IdTipoCbte_cxp,
                                     IdCbteCble_cxp=q.IdCbteCble_cxp,
                                     IdEntidad = q.IdEntidad,
                                     IdPersona = q.IdPersona,
                                     IdTipo_op = q.IdTipo_op,
                                     IdEstadoAprobacion = q.IdEstadoAprobacion,
                                     IdTipo_Persona = q.IdTipoPersona,
                                     Valor_a_pagar = q.Valor_a_pagar,
                                     Valor_estimado_a_pagar_OP = q.Valor_estimado_a_pagar_OP,
                                     Total_cancelado_OP = q.Total_cancelado_OP,
                                     Saldo_x_Pagar_OP = q.Saldo_x_Pagar_OP,
                                     Nom_Beneficiario = q.Nom_Beneficiario

                                 }).ToList();

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                   
                    return Lista;

                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
