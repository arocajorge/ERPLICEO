using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
using Core.Erp.Data.General;
using Core.Erp.Info.General;

namespace Core.Erp.Data.RRHH
{
  public  class ro_Historico_Liquidacion_Vacaciones_Data
    {


        public List<ro_Historico_Liquidacion_Vacaciones_Info> get_list(int IdEmpresa,DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<ro_Historico_Liquidacion_Vacaciones_Info> Lista;
                FechaInicio = FechaInicio.Date;
                FechaFin = FechaFin.Date;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_Historico_Liquidacion_Vacaciones
                             where q.IdEmpresa == IdEmpresa
                              && q.FechaPago >= FechaInicio
                                 && q.FechaPago <= FechaFin
                             select new ro_Historico_Liquidacion_Vacaciones_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 IdSolicitud = q.IdLiquidacion,
                                 IdLiquidacion=q.IdLiquidacion,
                                 Dias_q_Corresponde = q.Dias_q_Corresponde,
                                 Dias_a_disfrutar = q.Dias_a_disfrutar,
                                 Dias_pendiente = q.Dias_pendiente,
                                 empleado=q.pe_apellido+" "+q.pe_nombre,
                                 ValorCancelado=q.ValorCancelado,
                                 Observaciones=q.Observaciones,
                                 Periodo=q.Periodo,
                                 FechaPago=q.FechaPago,
                                 Estado=q.Estado,

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
        public ro_Historico_Liquidacion_Vacaciones_Info get_info(int IdEmpresa, decimal IdEmpleado, decimal IdLiquidacion)
        {
            try
            {
                ro_Historico_Liquidacion_Vacaciones_Info info = new ro_Historico_Liquidacion_Vacaciones_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var q = Context.vwro_Historico_Liquidacion_Vacaciones.FirstOrDefault(v => v.IdEmpresa == IdEmpresa
                    && v.IdEmpleado == IdEmpleado
                    && v.IdLiquidacion == IdLiquidacion);
                    if (q == null) return null;
                    info = new ro_Historico_Liquidacion_Vacaciones_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdEmpleado = q.IdEmpleado,
                        IdSolicitud = q.IdLiquidacion,
                        Dias_q_Corresponde = q.Dias_q_Corresponde,
                        Dias_a_disfrutar = q.Dias_a_disfrutar,
                        Dias_pendiente = q.Dias_pendiente,
                        empleado = q.pe_apellido + " " + q.pe_nombre,
                        ValorCancelado = q.ValorCancelado,
                        Observaciones = q.Observaciones,
                        Periodo = q.Periodo,
                        FechaPago = q.FechaPago,
                        Estado = q.Estado,
                        Fecha_Desde=q.Fecha_Desde,
                        Fecha_Hasta=q.Fecha_Hasta,
                        Fecha_Retorno=q.Fecha_Retorno
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Boolean guardarDB(ro_Historico_Liquidacion_Vacaciones_Info Info)
        {
            try
            {

                using (Entities_rrhh db = new Entities_rrhh())
                {
                   
                        ro_Historico_Liquidacion_Vacaciones Data = new ro_Historico_Liquidacion_Vacaciones();
                        Data.IdEmpresa = Info.IdEmpresa;
                        Data.IdSolicitud = Info.IdSolicitud;
                        Data.IdLiquidacion =Info.IdLiquidacion= getId(Info.IdEmpresa, Convert.ToInt32(Info.IdEmpleado));
                        Data.IdEmpresa_OP = Info.IdEmpresa_OP;
                        Data.IdOrdenPago = Info.IdOrdenPago;
                        Data.IdEmpleado = Info.IdEmpleado;
                        Data.ValorCancelado = Info.ValorCancelado;
                        Data.FechaPago = DateTime.Now;
                        Data.Observaciones = Info.Observaciones;
                        Data.IdUsuario = Info.IdUsuario;
                        Data.Estado = "A";
                        Data.Fecha_Transac = DateTime.Now;
                        db.ro_Historico_Liquidacion_Vacaciones.Add(Data);
                        db.SaveChanges();
                    
                }
                return true;
            }
            catch (Exception ex )
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_Historico_Liquidacion_Vacaciones_Data", Metodo = "guardarDB", IdUsuario =Info.IdUsuario});
                return false;
            }

        }
        public Boolean modificarDB(ro_Historico_Liquidacion_Vacaciones_Info info)
        {
            try
            {
               
                    using (Entities_rrhh context = new Entities_rrhh())
                    {
                        var contact = context.ro_Historico_Liquidacion_Vacaciones.First(obj => obj.IdEmpresa == info.IdEmpresa &&
                        obj.IdLiquidacion == info.IdLiquidacion && obj.IdEmpleado == info.IdEmpleado);
                    if (contact == null)
                        return false;
                        contact.Observaciones = info.Observaciones;
                        contact.ValorCancelado = info.ValorCancelado;
                        contact.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                        contact.FechaHoraAnul = DateTime.Now;
                        contact.MotiAnula = info.MotiAnula;
                        context.SaveChanges();
                    }

                    return true;
               
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_Historico_Liquidacion_Vacaciones_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public Boolean anularDB(ro_Historico_Liquidacion_Vacaciones_Info info)
        {
            try
            {
               
                    using (Entities_rrhh context = new Entities_rrhh())
                    {
                        var contact = context.ro_Historico_Liquidacion_Vacaciones.First(obj => obj.IdEmpresa == info.IdEmpresa &&
                             obj.IdLiquidacion == info.IdLiquidacion && obj.IdEmpleado == info.IdEmpleado);
                        contact.Estado = "I";
                        contact.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                        contact.FechaHoraAnul = DateTime.Now;
                        contact.MotiAnula = info.MotiAnula;
                    context.SaveChanges();
                    }

                    return true;
               
            }
            catch (Exception )
            {
               
                throw ;
            }
        }
     
        public int getId(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                int Id;
                Entities_rrhh OEEmpleado = new Entities_rrhh();
                var select = from q in OEEmpleado.ro_Historico_Liquidacion_Vacaciones
                             where q.IdEmpresa == IdEmpresa 
                             && q.IdEmpleado == IdEmpleado
                             select q;
                if (select.ToList().Count() == 0)
                {
                    Id = 1;
                }
                else
                {
                    var select_em = (from q in OEEmpleado.ro_Historico_Liquidacion_Vacaciones
                                     where q.IdEmpresa == IdEmpresa && q.IdEmpleado == IdEmpleado
                                     select q.IdLiquidacion).Max();
                    Id = Convert.ToInt32(select_em.ToString()) + 1;
                }
                return Id;
            }
            catch (Exception )
            {
                
                throw;
            }
        }

       }
}
