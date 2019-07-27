using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
   public class ro_NominasPagosCheques_Data
    {
        ro_empleado_novedad_Data odata_novedad = new ro_empleado_novedad_Data();

        public List<ro_NominasPagosCheques_Info> get_list(int IdEmpresa, DateTime Fechainicio, DateTime FechaFin, bool mostrar_anulados)
        {
            try
            {
                List<ro_NominasPagosCheques_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        Lista = (from q in Context.vwro_NominasPagosCheques
                                 where q.IdEmpresa == IdEmpresa
                                 select new ro_NominasPagosCheques_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTransaccion = q.IdTransaccion,
                                     IdNomina_Tipo = q.IdNomina_Tipo,
                                     IdNomina_TipoLiqui = q.IdNomina_TipoLiqui,
                                     IdPeriodo = q.IdPeriodo,
                                     Observacion=q.Observacion,
                                     Estado=q.Estado,
                                     Descripcion=q.Descripcion,
                                     DescripcionProcesoNomina=q.DescripcionProcesoNomina,
                                     pe_FechaFin=q.pe_FechaFin,
                                     pe_FechaIni=q.pe_FechaIni


                                 }).ToList();
                    
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_NominasPagosCheques_Info get_info(int IdEmpresa, decimal IdTransaccion)
        {
            try
            {

                ro_NominasPagosCheques_Info info = new ro_NominasPagosCheques_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_NominasPagosCheques Entity = Context.ro_NominasPagosCheques.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdTransaccion == IdTransaccion);
                    if (Entity == null) return null;

                    info = new ro_NominasPagosCheques_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTransaccion = Entity.IdTransaccion,
                        IdNomina_Tipo = Entity.IdNomina_Tipo,
                        IdNomina_TipoLiqui = Entity.IdNomina_TipoLiqui,
                        IdPeriodo = Entity.IdPeriodo,
                        Observacion = Entity.Observacion,
                        Estado = Entity.Estado

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
                    var lst = from q in Context.ro_NominasPagosCheques
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdTransaccion) + 1;
                }

                return ID;
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
                int secuencia = 1;
                decimal IdNovedad = odata_novedad.get_id(info.IdEmpresa);

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var info_rubros_calculados = Context.ro_rubros_calculados.Where(v => v.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                    ro_NominasPagosCheques Entity = new ro_NominasPagosCheques
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTransaccion = info.IdTransaccion =Convert.ToInt32( get_id(info.IdEmpresa)),
                        IdNomina_Tipo = info.IdNomina_Tipo,
                        IdNomina_TipoLiqui = info.IdNomina_TipoLiqui,
                        IdPeriodo = info.IdPeriodo,
                        Observacion = info.Observacion,
                        Estado = info.Estado = true,
                        IdUsuario = info.IdUsuario,
                        FechaTransac = info.FechaTransac = DateTime.Now
                    };
                    Context.ro_NominasPagosCheques.Add(Entity);

                    foreach (var item in info.detalle)
                    {
                       
                        ro_NominasPagosCheques_det Entity_ = new ro_NominasPagosCheques_det
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdTransaccion = item.IdTransaccion = info.IdTransaccion,
                            IdSucursal = item.IdSucursal,
                            IdEmpleado = item.IdEmpleado,
                            Valor = item.Valor,
                            Secuencia = secuencia,
                            IdEmpresa_op=info.IdEmpresa,
                            IdOrdenPago=item.IdOrdenPago,
                            Secuancia_op=item.Secuancia_op,
                            IdEmpresa_dc=item.IdEmpresa_dc,
                            IdTipoCbte=item.IdTipoCbte,
                            IdCbteCble=item.IdCbteCble
                        };
                        Context.ro_NominasPagosCheques_det.Add(Entity_);
                        secuencia++;


                        if (info_rubros_calculados != null)
                        {
                            if (info_rubros_calculados.IdRubro_novedad_proceso != null)
                            {

                                ro_empleado_Novedad Entity_nov = new ro_empleado_Novedad
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdSucursal = item.IdSucursal,
                                    IdNovedad  = IdNovedad,
                                    IdNomina_Tipo = info.IdNomina_Tipo,
                                    IdNomina_TipoLiqui = info.IdNomina_TipoLiqui,
                                    IdEmpleado =Convert.ToInt32( item.IdEmpleado),
                                    Fecha = DateTime.Now.Date,

                                    Observacion = info.Observacion == null ? "" : info.Observacion,
                                    Estado = "A",
                                    IdUsuario = info.IdUsuario,
                                    Fecha_Transac  = DateTime.Now
                                };
                                Context.ro_empleado_Novedad.Add(Entity_nov);

                                ro_empleado_novedad_det Entity_nov_det = new ro_empleado_novedad_det
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdNovedad =IdNovedad,
                                    FechaPago = DateTime.Now.Date,
                                    IdRubro = info_rubros_calculados.IdRubro_novedad_proceso,
                                    Valor = item.Valor,
                                    Observacion = item.Observacion,
                                    EstadoCobro = "PEN",
                                    Secuencia = 1
                                };
                                Context.ro_empleado_novedad_det.Add(Entity_nov_det);
                                IdNovedad++;



                            }
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
        public bool modificarDB(ro_NominasPagosCheques_Info info)
        {
            try
            {
                int secuencia = 1;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_NominasPagosCheques Entity = Context.ro_NominasPagosCheques.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTransaccion == info.IdTransaccion);
                    if (Entity == null)
                        return false;

                    Entity.IdNomina_Tipo = info.IdNomina_Tipo;
                    Entity.IdNomina_TipoLiqui = info.IdNomina_TipoLiqui;
                    Entity.IdPeriodo = info.IdPeriodo;
                    Entity.Observacion = info.Observacion;
                    var detalle = Context.ro_NominasPagosCheques_det.Where(v => v.IdEmpresa == info.IdEmpresa && v.IdTransaccion == info.IdTransaccion);
                    Context.ro_NominasPagosCheques_det.RemoveRange(detalle);
                    foreach (var item in info.detalle)
                    {
                        ro_NominasPagosCheques_det Entity_ = new ro_NominasPagosCheques_det
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdTransaccion = item.IdTransaccion,
                            IdSucursal = item.IdSucursal,
                            Secuencia = secuencia,
                            IdEmpleado = item.IdEmpleado,
                            Valor = item.Valor,
                        };
                        Context.ro_NominasPagosCheques_det.Add(Entity_);
                        secuencia++;
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
        public bool anularDB(ro_NominasPagosCheques_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_NominasPagosCheques Entity = Context.ro_NominasPagosCheques.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTransaccion == info.IdTransaccion);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;
                    Entity.IdUsuarioAnu = info.IdUsuarioAnu;
                    Entity.FechaAnu = info.FechaAnu = DateTime.Now;
                    string sql = "Update ro_NominasPagosCheques_det set Estado=0 where IdEmpresa='"+info.IdEmpresa+ "'  and IdTransaccion='"+info.IdTransaccion+"'";
                    Context.Database.ExecuteSqlCommand(sql);
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
