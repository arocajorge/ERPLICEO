using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
  public  class ro_nomina_x_horas_extras_Data
    {

        public List<ro_nomina_x_horas_extras_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<ro_nomina_x_horas_extras_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from he in Context.ro_nomina_x_horas_extras
                             join n in Context.ro_Nomina_Tipo
                             on he.IdEmpresa equals n.IdEmpresa
                             join nt in Context.ro_Nomina_Tipoliqui
                             on new { n.IdEmpresa, n.IdNomina_Tipo } equals new { nt.IdEmpresa,nt.IdNomina_Tipo }
                             join pn in Context.ro_periodo_x_ro_Nomina_TipoLiqui
                             on new { nt.IdEmpresa, nt.IdNomina_Tipo, nt.IdNomina_TipoLiqui } equals new { pn.IdEmpresa, pn.IdNomina_Tipo,pn.IdNomina_TipoLiqui }
                             join p in Context.ro_periodo
                             on new { pn.IdEmpresa, pn.IdPeriodo } equals new { p.IdEmpresa, p.IdPeriodo}
                             where he.IdEmpresa==IdEmpresa
                             && pn.IdNomina_Tipo==he.IdNominaTipo
                             && pn.IdNomina_TipoLiqui==he.IdNominaTipoLiqui
                             && pn.IdPeriodo==he.IdPeriodo
                             select new ro_nomina_x_horas_extras_Info
                             {
                                 IdEmpresa = he.IdEmpresa,
                                 IdHorasExtras = he.IdHorasExtras,
                                 IdNomina_Tipo = he.IdNominaTipo,
                                 IdNomina_TipoLiqui=he.IdNominaTipoLiqui,
                                 IdPeriodo=he.IdPeriodo,
                                 Descripcion=n.Descripcion,
                                 DescripcionProcesoNomina=nt.DescripcionProcesoNomina,
                                 pe_FechaIni=p.pe_FechaIni,
                                 pe_FechaFin=p.pe_FechaFin,
                                 Estado=he.Estado,

                                 EstadoBool = he.Estado == "A" ? true : false



                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_nomina_x_horas_extras_Info get_info(int IdEmpresa, int IdHorasExtras)
        {
            try
            {
                ro_nomina_x_horas_extras_Info info = new ro_nomina_x_horas_extras_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_nomina_x_horas_extras Entity = Context.ro_nomina_x_horas_extras.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdHorasExtras == IdHorasExtras);
                    if (Entity == null) return null;

                    info = new ro_nomina_x_horas_extras_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdHorasExtras = Entity.IdHorasExtras,
                        IdNomina_Tipo = Entity.IdNominaTipo,
                        IdNomina_TipoLiqui=Entity.IdNominaTipoLiqui,
                        IdPeriodo=Entity.IdPeriodo,
                        
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_nomina_x_horas_extras_Info get_info(int IdEmpresa, int IdNomina_Tipo, int IdNomina_TipoLiqui, int IdPeriodo)
        {
            try
            {
                ro_nomina_x_horas_extras_Info info = new ro_nomina_x_horas_extras_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_nomina_x_horas_extras Entity = Context.ro_nomina_x_horas_extras.FirstOrDefault(q => q.IdEmpresa == IdEmpresa 
                    && q.IdNominaTipo == IdNomina_Tipo
                    && q.IdNominaTipoLiqui==IdNomina_TipoLiqui
                    && q.IdPeriodo==IdPeriodo
                    && q.Estado=="A");
                    if (Entity == null) return null;

                    info = new ro_nomina_x_horas_extras_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdHorasExtras = Entity.IdHorasExtras,
                        IdNomina_Tipo = Entity.IdNominaTipo,
                        IdNomina_TipoLiqui = Entity.IdNominaTipoLiqui,
                        IdPeriodo = Entity.IdPeriodo,

                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_nomina_x_horas_extras
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdHorasExtras) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Procesar(ro_nomina_x_horas_extras_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Context.spRo_nomina_calculo_he(info.IdEmpresa, info.IdNomina_Tipo, info.IdNomina_TipoLiqui, info.IdPeriodo, info.IdUsuario, " ");
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_nomina_x_horas_extras_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_nomina_x_horas_extras Entity = Context.ro_nomina_x_horas_extras.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdHorasExtras == info.IdHorasExtras);
                    if (Entity == null)
                        return false;
                    Entity.IdNominaTipo = info.IdNomina_Tipo;
                    Entity.IdNominaTipoLiqui = info.IdNomina_TipoLiqui;
                    Entity.IdPeriodo = info.IdPeriodo;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_nomina_x_horas_extras_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    
                    var detalle = Context.ro_nomina_x_horas_extras_det.Where(v=>v.IdEmpresa==info.IdEmpresa&& v.IdHorasExtras== info.IdHorasExtras);
                    Context.ro_nomina_x_horas_extras_det.RemoveRange(detalle);
                    foreach (var item in info.lst_nomina_horas_extras)
                    {
                        ro_nomina_x_horas_extras_det content_det = new ro_nomina_x_horas_extras_det()
                        {
                            IdEmpresa=info.IdEmpresa,
                            IdHorasExtras=info.IdHorasExtras,
                            IdEmpleado=item.IdEmpleado,
                            IdCalendario=item.IdCalendario,
                            IdTurno=item.IdTurno,
                            IdHorario=item.IdHorario,
                            FechaRegistro=item.FechaRegistro,
                            time_entrada1=item.time_entrada1,
                            time_entrada2=item.time_entrada2,
                            time_salida1=item.time_salida1,
                            time_salida2=item.time_salida2,
                            hora_extra25=item.hora_extra25,
                            hora_extra50=item.hora_extra50,
                            hora_extra100=item.hora_extra100,
                            Valor25= Math.Round(  ((item.Sueldo_base/240)*1.25)  *item.hora_extra25,2),
                            Valor50=Math.Round(   ((item.Sueldo_base / 240)*1.5) * item.hora_extra50,2),
                            Valor100=Math.Round(  ((item.Sueldo_base / 240)*2) * item.hora_extra100,2),
                            Sueldo_base=item.Sueldo_base,
                            hora_atraso=item.hora_atraso,
                            hora_temprano=item.hora_temprano,
                            hora_trabajada=item.hora_trabajada,
                            es_HorasExtrasAutorizadas=item.es_HorasExtrasAutorizadas

                        };
                        Context.ro_nomina_x_horas_extras_det.Add(content_det);
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

        public bool anularDB(ro_nomina_x_horas_extras_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_nomina_x_horas_extras Entity = Context.ro_nomina_x_horas_extras.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdHorasExtras == info.IdHorasExtras);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";
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

        public bool AprobarHE(ro_nomina_x_horas_extras_Info info)
        {
            try
            {
                ro_empleado_novedad_Data odata_novedad = new ro_empleado_novedad_Data();
                decimal IdNovedad = odata_novedad.get_id(info.IdEmpresa);
                ro_nomina_x_horas_extras_det_Data odata = new ro_nomina_x_horas_extras_det_Data();
                ro_periodo_Data bus_periodo = new ro_periodo_Data();
                ro_periodo_Info info_periodo = new ro_periodo_Info();
                info_periodo = bus_periodo.get_info(info.IdEmpresa, info.IdPeriodo);
                var  lst_horas_extras_aprobar = odata.get_lst_horas_extras_aprobar(info.IdEmpresa, info.IdNomina_Tipo, info.IdNomina_TipoLiqui, info.IdPeriodo);
                using (Entities_rrhh content=new Entities_rrhh())
                {
                    foreach (var item in lst_horas_extras_aprobar)
                    {
                        if (item.Valor25 > 0)
                        {
                            IdNovedad++;
                            ro_empleado_Novedad info_novedad_25 = new ro_empleado_Novedad()
                            {
                                IdNovedad=IdNovedad,
                                IdEmpresa = info.IdEmpresa,
                                IdSucursal=item.IdSucursal,
                                IdEmpleado = item.IdEmpleado,
                                IdNomina_Tipo = info.IdNomina_Tipo,
                                IdNomina_TipoLiqui = info.IdNomina_TipoLiqui,
                                Observacion = "Hora extra al 25 % corrspondiente al periodo " + info.IdPeriodo.ToString(),
                                Fecha_Transac = DateTime.Now,
                                IdUsuario = info.IdUsuario,
                                Fecha = info_periodo.pe_FechaFin,
                                Estado = "A"
                            };
                            content.ro_empleado_Novedad.Add(info_novedad_25);
                            ro_empleado_novedad_det info_det_25 = new ro_empleado_novedad_det()
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdNovedad = IdNovedad,
                                Secuencia = 1,
                                Valor = item.Valor25,
                                FechaPago = info_periodo.pe_FechaFin,
                                IdRubro = "7",
                                Observacion = "Hora extra al 25 % corrspondiente al periodo " + info.IdPeriodo.ToString(),
                                EstadoCobro = "PEN"

                            };
                            content.ro_empleado_novedad_det.Add(info_det_25);

                        };


                        if (item.Valor50 > 0)
                        {
                            IdNovedad++;
                            ro_empleado_Novedad info_novedad_50 = new ro_empleado_Novedad()
                            {
                                IdNovedad=IdNovedad,
                                IdSucursal = item.IdSucursal,
                                IdEmpresa = info.IdEmpresa,
                                IdEmpleado = item.IdEmpleado,
                                IdNomina_Tipo = info.IdNomina_Tipo,
                                IdNomina_TipoLiqui = info.IdNomina_TipoLiqui,
                                Observacion = "Hora extra al 50 % corrspondiente al periodo " + info.IdPeriodo.ToString(),
                                Fecha_Transac = DateTime.Now,
                                IdUsuario = info.IdUsuario,
                                Fecha = info_periodo.pe_FechaFin,
                                Estado = "A"
                            };
                            content.ro_empleado_Novedad.Add(info_novedad_50);
                            ro_empleado_novedad_det info_det_50 = new ro_empleado_novedad_det()
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdNovedad = IdNovedad,
                                Secuencia = 1,
                                Valor = item.Valor50,
                                FechaPago = info_periodo.pe_FechaFin,
                                IdRubro = "8",
                                Observacion = "Hora extra al 50 % corrspondiente al periodo " + info.IdPeriodo.ToString(),
                                EstadoCobro = "PEN"

                            };
                            content.ro_empleado_novedad_det.Add(info_det_50);

                        };

                        if (item.Valor50 > 0)
                        {
                            ro_empleado_Novedad info_novedad_100 = new ro_empleado_Novedad()
                            {
                                IdNovedad=IdNovedad,
                                IdSucursal = item.IdSucursal,
                                IdEmpresa = info.IdEmpresa,
                                IdEmpleado = item.IdEmpleado,
                                IdNomina_Tipo = info.IdNomina_Tipo,
                                IdNomina_TipoLiqui = info.IdNomina_TipoLiqui,
                                Observacion = "Hora extra al 50 % corrspondiente al periodo " + info.IdPeriodo.ToString(),
                                Fecha_Transac = DateTime.Now,
                                IdUsuario = info.IdUsuario,
                                Fecha = info_periodo.pe_FechaFin,
                                Estado = "A"
                            };
                            content.ro_empleado_Novedad.Add(info_novedad_100);
                            ro_empleado_novedad_det info_det_100 = new ro_empleado_novedad_det()
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdNovedad = IdNovedad,
                                Secuencia = 1,
                                Valor = item.Valor100,
                                FechaPago = info_periodo.pe_FechaFin,
                                IdRubro = "8",
                                Observacion = "Hora extra al 100 % corrspondiente al periodo " + info.IdPeriodo.ToString(),
                                EstadoCobro="PEN"
                            };
                            content.ro_empleado_novedad_det.Add(info_det_100);

                        };
                    }
                    content.SaveChanges();
                    odata.Modificar_estado_aprobacion(info.IdHorasExtras, 1);


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
