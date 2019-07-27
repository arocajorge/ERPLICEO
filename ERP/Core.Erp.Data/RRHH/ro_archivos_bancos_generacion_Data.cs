using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
  public  class ro_archivos_bancos_generacion_Data
    {
        public List<ro_archivos_bancos_generacion_Info> get_list(int IdEmpresa,  DateTime fecha_ini, DateTime fecha_fin, int IdSucursal, bool mostrar_anulados)
        {
            try
            {
                int IdSucursalIni =  IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;
                List<ro_archivos_bancos_generacion_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        Lista = (from q in Context.vwro_archivos_bancos_generacion
                                 where q.IdEmpresa == IdEmpresa
                                 && IdSucursalIni <= q.IdSucursal
                                 && q.IdSucursal <= IdSucursalFin
                                 && fecha_ini <= q.pe_FechaFin
                                 && q.pe_FechaFin <= fecha_fin
                                 select new ro_archivos_bancos_generacion_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdArchivo = q.IdArchivo,
                                     IdNomina = q.IdNomina,
                                     IdNominaTipo =q.IdNominaTipo,
                                     IdCuentaBancaria=q.IdCuentaBancaria,
                                     IdProceso=q.IdProceso,
                                     estado = q.estado,
                                     EstadoBool = q.estado == "A" ? true : false,
                                     Descripcion=q.Descripcion,
                                     DescripcionProcesoNomina=q.DescripcionProcesoNomina,
                                     pe_FechaIni=q.pe_FechaIni,
                                     pe_FechaFin=q.pe_FechaFin,
                                     NombreProceso = q.NombreProceso,
                                     IdPeriodo = q.IdPeriodo,
                                     IdSucursal = q.IdSucursal,
                                     IdProceso_bancario_tipo=q.IdProceso_bancario_tipo,
                                     Su_Descripcion=q.Su_Descripcion

                                 }).ToList();
                    
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_archivos_bancos_generacion_Info get_info(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                
                ro_archivos_bancos_generacion_Info info = new ro_archivos_bancos_generacion_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    vwro_archivos_bancos_generacion Entity = Context.vwro_archivos_bancos_generacion.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo);
                    if (Entity == null) return null;

                    info = new ro_archivos_bancos_generacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdArchivo = Entity.IdArchivo,
                        IdNomina = Entity.IdNomina,
                        IdNominaTipo = Entity.IdNominaTipo,
                        IdPeriodo=Entity.IdPeriodo,
                        IdCuentaBancaria = Entity.IdCuentaBancaria,
                        IdProceso = Entity.IdProceso,
                        estado = Entity.estado,
                        EstadoBool = Entity.estado == "A" ? true : false,
                        IdSucursal=Entity.IdSucursal
                        
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ro_archivos_bancos_generacion_Info get_info_generar_file(int IdEmpresa, decimal IdArchivo)
        {
            try
            {

                ro_archivos_bancos_generacion_Info info = new ro_archivos_bancos_generacion_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_archivos_bancos_generacion Entity = Context.ro_archivos_bancos_generacion.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo);
                    if (Entity == null) return null;

                    info = new ro_archivos_bancos_generacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdArchivo = Entity.IdArchivo,
                        IdNomina = Entity.IdNomina,
                        IdNominaTipo = Entity.IdNominaTipo,
                        IdCuentaBancaria = Entity.IdCuentaBancaria,
                        IdProceso = Entity.IdProceso,
                        estado = Entity.estado,
                        EstadoBool = Entity.estado == "A" ? true : false,

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
                    var lst = from q in Context.ro_archivos_bancos_generacion
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdArchivo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_archivos_bancos_generacion_Info info)
        {
            try
            {
                int secuencia = 1;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_archivos_bancos_generacion Entity = new ro_archivos_bancos_generacion
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdArchivo = info.IdArchivo = get_id(info.IdEmpresa),
                        IdNomina = info.IdNomina,
                        IdNominaTipo = info.IdNominaTipo ,
                        IdPeriodo = info.IdPeriodo,
                        IdProceso=info.IdProceso,
                        IdCuentaBancaria=info.IdCuentaBancaria,
                        estado=info.estado="A",
                        IdUsuario=info.IdUsuario,
                        IdSucursal=info.IdSucursal,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_archivos_bancos_generacion.Add(Entity);
                    foreach (var item in info.detalle)
                    {
                        ro_archivos_bancos_generacion_x_empleado Entity_ = new ro_archivos_bancos_generacion_x_empleado
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdArchivo = item.IdArchivo=info.IdArchivo,
                            IdSucursal = item.IdSucursal,
                            IdEmpleado = item.IdEmpleado,
                            Valor =Math.Round( item.Valor,2),
                            pagacheque = item.pagacheque,
                            Secuencia=secuencia
                        };
                        Context.ro_archivos_bancos_generacion_x_empleado.Add(Entity_);
                        secuencia++;
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception )
            {

                throw;
            }
        }
        public bool modificarDB(ro_archivos_bancos_generacion_Info info)
        {
            try
            {
                int secuencia = 1;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_archivos_bancos_generacion Entity = Context.ro_archivos_bancos_generacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo);
                    if (Entity == null)
                        return false;
                    
                    Entity.IdCuentaBancaria = info.IdCuentaBancaria;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;
                    Entity.IdSucursal = info.IdSucursal;
                    var detalle = Context.ro_archivos_bancos_generacion_x_empleado.Where(v =>v.IdEmpresa==info.IdEmpresa&& v.IdArchivo==info.IdArchivo);
                    Context.ro_archivos_bancos_generacion_x_empleado.RemoveRange(detalle);
                    foreach (var item in info.detalle)
                    {
                        ro_archivos_bancos_generacion_x_empleado Entity_ = new ro_archivos_bancos_generacion_x_empleado
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdArchivo = item.IdArchivo,
                            IdSucursal = item.IdSucursal,
                            Secuencia = secuencia,
                            IdEmpleado = item.IdEmpleado,
                            Valor = item.Valor,
                            pagacheque = item.pagacheque,
                        };
                        Context.ro_archivos_bancos_generacion_x_empleado.Add(Entity_);
                        secuencia++;
                    }

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception )
            {

                throw;
            }
        }
        public bool anularDB(ro_archivos_bancos_generacion_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_archivos_bancos_generacion Entity = Context.ro_archivos_bancos_generacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo);
                    if (Entity == null)
                        return false;
                    Entity.estado = info.estado = "I";
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    var detalle = Context.ro_archivos_bancos_generacion_x_empleado.Where(v => v.IdEmpresa == info.IdEmpresa && v.IdArchivo == info.IdArchivo);
                    Context.ro_archivos_bancos_generacion_x_empleado.RemoveRange(detalle);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public int get_secuencia_file(int IdEmpresa, int IdProceso, DateTime FechaActual)
        {
            try
            {
                int ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_archivos_bancos_generacion
                              where q.IdEmpresa == IdEmpresa
                              && q.IdProceso == IdProceso
                               && q.Fecha_Transac == FechaActual
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Count()+1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
