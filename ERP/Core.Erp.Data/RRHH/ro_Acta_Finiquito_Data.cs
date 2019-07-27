using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
    public class ro_Acta_Finiquito_Data
    {
        public List<ro_Acta_Finiquito_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<ro_Acta_Finiquito_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwRo_ActaFiniquito
                             where q.IdEmpresa == IdEmpresa
                             && q.EstadoContrato!= "ECT_ACT"
                             select new ro_Acta_Finiquito_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdActaFiniquito = q.IdActaFiniquito,
                                 IdEmpleado = q.IdEmpleado,
                                 ca_descripcion = q.ca_descripcion,
                                 Contrato = q.Contrato,
                                 pe_nombre_completo = q.pe_apellido + "" + q.pe_nombre,
                                 em_codigo = q.em_codigo,
                                 FechaIngreso = q.FechaIngreso,
                                 FechaSalida = q.FechaSalida,
                                 Ingresos = q.Ingresos,
                                 Egresos = q.Egresos,
                                 EstadoContrato = q.EstadoContrato,
                                 Contrato_tipo = q.CodCatalogo,

                                 EstadoBool = q.EstadoActa == "A" ? true : false
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_Acta_Finiquito_Info> get_list_pre_liquidacion(int IdEmpresa)
        {
            try
            {
                List<ro_Acta_Finiquito_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwRo_ActaFiniquito
                             where q.IdEmpresa == IdEmpresa
                             && q.EstadoContrato == "ECT_ACT"
                             select new ro_Acta_Finiquito_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdActaFiniquito = q.IdActaFiniquito,
                                 IdEmpleado = q.IdEmpleado,
                                 ca_descripcion = q.ca_descripcion,
                                 Contrato = q.Contrato,
                                 pe_nombre_completo = q.pe_apellido + "" + q.pe_nombre,
                                 em_codigo = q.em_codigo,
                                 FechaIngreso = q.FechaIngreso,
                                 FechaSalida = q.FechaSalida,
                                 Ingresos = q.Ingresos,
                                 Egresos = q.Egresos,
                                 EstadoContrato = q.EstadoContrato,
                                 Contrato_tipo = q.CodCatalogo,

                                 EstadoBool = q.EstadoActa == "A" ? true : false
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_Acta_Finiquito_Info get_info(int IdEmpresa, decimal IdActaFiniquito)
        {
            try
            {
                ro_Acta_Finiquito_Info info = new ro_Acta_Finiquito_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    vwRo_ActaFiniquito Entity = Context.vwRo_ActaFiniquito.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdActaFiniquito == IdActaFiniquito);
                    if (Entity == null) return null;

                    info = new ro_Acta_Finiquito_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdActaFiniquito = Entity.IdActaFiniquito,
                        IdEmpleado = Entity.IdEmpleado,
                        IdCausaTerminacion = Entity.IdCausaTerminacion,
                        FechaIngreso = Entity.FechaIngreso,
                        FechaSalida = Entity.FechaSalida,
                        UltimaRemuneracion = Entity.UltimaRemuneracion,
                        Observacion = Entity.ObservacionFiniquito,
                        Ingresos = Entity.Ingresos,
                        Egresos = Entity.Egresos,
                        IdCodSectorial = Entity.IdCodSectorial,
                        EsMujerEmbarazada = Entity.EsMujerEmbarazada,
                        EsDirigenteSindical = Entity.EsDirigenteSindical,
                        EsPorDiscapacidad = Entity.EsPorDiscapacidad,
                        EsPorEnfermedadNoProfesional = Entity.EsPorEnfermedadNoProfesional,
                        IdTipoCbte = Entity.IdTipoCbte,
                        IdCbteCble = Entity.IdCbteCble,
                        IdOrdenPago = Entity.IdOrdenPago,
                        Contrato_tipo = Entity.CodCatalogo

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
                    var lst = from q in Context.ro_Acta_Finiquito
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdActaFiniquito) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_Acta_Finiquito_Info info)
        {
            try
            {
                int secuencia = 1;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Acta_Finiquito Entity = new ro_Acta_Finiquito
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdActaFiniquito = info.IdActaFiniquito = get_id(info.IdEmpresa),
                        IdEmpleado = info.IdEmpleado,
                        IdCausaTerminacion = info.IdCausaTerminacion,
                        IdContrato = info.IdContrato,
                        IdCargo = info.IdCargo,
                        FechaIngreso = info.FechaIngreso.Date,
                        FechaSalida = info.FechaSalida.Date,
                        UltimaRemuneracion = info.UltimaRemuneracion,
                        Observacion = (info.Observacion)==null?" ":info.Observacion,
                        Ingresos = info.Ingresos,
                        Egresos = info.Egresos,
                        IdCodSectorial = info.IdCodSectorial,
                        EsMujerEmbarazada = info.EsMujerEmbarazada,
                        EsDirigenteSindical = info.EsDirigenteSindical,
                        EsPorDiscapacidad = info.EsPorDiscapacidad,
                        EsPorEnfermedadNoProfesional = info.EsPorEnfermedadNoProfesional,
                        IdTipoCbte = info.IdTipoCbte,
                        IdCbteCble = info.IdCbteCble,
                        IdOrdenPago = info.IdOrdenPago,
                        Estado = info.Estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_Acta_Finiquito.Add(Entity);

                    foreach (var item in info.lst_detalle)
                    {
                        ro_Acta_Finiquito_Detalle Entity_det = new ro_Acta_Finiquito_Detalle
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdActaFiniquito = info.IdActaFiniquito,
                            IdSecuencia = secuencia,
                            IdRubro = item.IdRubro,
                            Valor = item.Valor,
                            Observacion = (item.Observacion) == null ? " " : item.Observacion,
                        };
                        Context.ro_Acta_Finiquito_Detalle.Add(Entity_det);
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
        public bool modificarDB(ro_Acta_Finiquito_Info info)
        {
            try
            {
                int secuencia = 1;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Acta_Finiquito Entity = Context.ro_Acta_Finiquito.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado && q.IdActaFiniquito == info.IdActaFiniquito);
                    if (Entity == null)
                        return false;
                    Entity.FechaIngreso = info.FechaIngreso;
                    Entity.FechaSalida = info.FechaSalida;
                    Entity.Observacion = info.Observacion;
                    Entity.UltimaRemuneracion = info.UltimaRemuneracion;
                    Entity.EsMujerEmbarazada = info.EsMujerEmbarazada;
                    Entity.EsDirigenteSindical = info.EsDirigenteSindical;
                    Entity.EsPorDiscapacidad = info.EsPorDiscapacidad;
                    Entity.EsPorEnfermedadNoProfesional = info.EsPorEnfermedadNoProfesional;
                    Entity.IdCausaTerminacion = info.IdCausaTerminacion;

                    Entity.UltimaRemuneracion = info.UltimaRemuneracion;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    
                    Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;
                    var detalle = Context.ro_Acta_Finiquito_Detalle.Where(v => v.IdEmpresa == info.IdEmpresa && v.IdActaFiniquito == info.IdActaFiniquito);
                    Context.ro_Acta_Finiquito_Detalle.RemoveRange(detalle);
                    foreach (var item in info.lst_detalle)
                    {
                        ro_Acta_Finiquito_Detalle Entity_det = new ro_Acta_Finiquito_Detalle
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdActaFiniquito = info.IdActaFiniquito,
                            IdSecuencia = secuencia,
                            IdRubro = item.IdRubro,
                            Valor = item.Valor,
                            Observacion = (item.Observacion)==null?" ": item.Observacion,
                        };
                        Context.ro_Acta_Finiquito_Detalle.Add(Entity_det);
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
        public bool anularDB(ro_Acta_Finiquito_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Acta_Finiquito Entity = Context.ro_Acta_Finiquito.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdActaFiniquito == info.IdActaFiniquito);
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
        public bool Liquidar(ro_Acta_Finiquito_Info info)
        {
            {
                try
                {
                    using (Entities_rrhh Contex = new Entities_rrhh())
                    {
                        Contex.spRo_LiquidarEmpleado(info.IdEmpresa, info.IdActaFiniquito);
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
}