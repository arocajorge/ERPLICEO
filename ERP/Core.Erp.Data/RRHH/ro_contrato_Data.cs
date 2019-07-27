using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_contrato_Data
    {
        public List<ro_contrato_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
        {
            try
            {
                var IdSucursalIni = IdSucursal == 0 ? 0 : IdSucursal;
                var IdSucursalfin = IdSucursal == 0 ? 9999 : IdSucursal;
                List<ro_contrato_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        Lista = (from cont in Context.ro_contrato
                                 join emp in Context.vwro_empleado_combo
                                 on new { cont.IdEmpresa, cont.IdEmpleado } equals new { emp.IdEmpresa, emp.IdEmpleado }
                                 join cat in Context.ro_catalogo
                                 on cont.IdContrato_Tipo equals cat.CodCatalogo
                                 where cont.IdEmpresa==IdEmpresa
                                 && emp.IdSucursal >= IdSucursalIni
                                 && emp.IdSucursal <= IdSucursalfin
                                 && emp.IdEmpresa==IdEmpresa
                                 && cat.IdTipoCatalogo == 2
                                 select new ro_contrato_Info
                                 {
                                     IdEmpresa = cont.IdEmpresa,
                                     IdEmpleado=cont.IdEmpleado,
                                     IdContrato = cont.IdContrato,
                                     IdContrato_Tipo = cont.IdContrato_Tipo,
                                     Observacion= cont.Observacion,
                                     FechaInicio= cont.FechaInicio,
                                     FechaFin= cont.FechaFin,
                                     NumDocumento= cont.NumDocumento,
                                     Estado = cont.Estado,
                                     Empleado=emp.Empleado,
                                     Contrato=cat.ca_descripcion,
                                     EstadoContrato=cont.EstadoContrato,

                                     EstadoBool = cont.Estado == "A" ? true : false

                                 }).ToList();
                   
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_contrato_Info> get_list_contratos_por_vencer(int IdEmpresa, DateTime FechaCorte)
        {
            try
            {
                FechaCorte = FechaCorte.AddMonths(1).Date;
                string Estado_contado = cl_enumeradores.eEstadoContratoRRHH.ECT_ACT.ToString();
                List<ro_contrato_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from cont in Context.ro_contrato
                             join emp in Context.vwro_empleado_combo
                             on new { cont.IdEmpresa, cont.IdEmpleado } equals new { emp.IdEmpresa, emp.IdEmpleado }
                             join cat in Context.ro_catalogo
                             on cont.IdContrato_Tipo equals cat.CodCatalogo
                             where cont.IdEmpresa == IdEmpresa
                             && emp.IdEmpresa == IdEmpresa
                             && cat.CodCatalogo != "CTR08"// me trae todos los contratos que no sean indefinidos
                             && cont.EstadoContrato==Estado_contado
                             && cont.FechaFin<=FechaCorte
                             select new ro_contrato_Info
                             {
                                 IdEmpresa = cont.IdEmpresa,
                                 IdEmpleado = cont.IdEmpleado,
                                 IdContrato = cont.IdContrato,
                                 IdContrato_Tipo = cont.IdContrato_Tipo,
                                 Observacion = cont.Observacion,
                                 FechaInicio = cont.FechaInicio,
                                 FechaFin = cont.FechaFin,
                                 NumDocumento = cont.NumDocumento,
                                 Estado = cont.Estado,
                                 Empleado = emp.Empleado,
                                 Contrato = cat.ca_descripcion,
                                 EstadoContrato = cont.EstadoContrato,

                                 EstadoBool = cont.Estado == "A" ? true : false

                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_contrato_Info> get_list(int IdEmpresa, decimal IdEmpleado, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<ro_contrato_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from cont in Context.ro_contrato
                             where cont.IdEmpresa == IdEmpresa
                             && cont.IdEmpleado == IdEmpleado
                             && cont.FechaInicio>=FechaInicio
                             && cont.FechaInicio<=FechaFin
                             select new ro_contrato_Info
                             {
                                 IdEmpresa = cont.IdEmpresa,
                                 IdEmpleado = cont.IdEmpleado,
                                 IdContrato = cont.IdContrato,
                                 IdContrato_Tipo = cont.IdContrato_Tipo,
                                 Observacion = cont.Observacion,
                                 FechaInicio = cont.FechaInicio,
                                 FechaFin = cont.FechaFin,
                                 NumDocumento = cont.NumDocumento,
                                 Estado = cont.Estado,
                                 EstadoContrato = cont.EstadoContrato,

                                 EstadoBool = cont.Estado == "A" ? true : false

                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_contrato_Info get_info(int IdEmpresa, decimal IdEmpleado, int IdContrato)
        {
            try
            {
                ro_contrato_Info info = new ro_contrato_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_contrato Entity = Context.ro_contrato.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado== IdEmpleado && q.IdContrato == IdContrato);
                    if (Entity == null) return null;

                    info = new ro_contrato_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEmpleado=Entity.IdEmpleado,
                        IdContrato = Entity.IdContrato,
                        IdContrato_Tipo = Entity.IdContrato_Tipo,
                        Observacion = Entity.Observacion,
                        FechaInicio = Entity.FechaInicio.Date,
                        FechaFin = Entity.FechaFin.Date,
                        NumDocumento=Entity.NumDocumento,
                        EstadoContrato=Entity.EstadoContrato,
                        Estado = Entity.Estado,
                        Sueldo=Entity.Sueldo,
                        IdNomina=Entity.IdNomina,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal get_id(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                decimal ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_contrato
                              where q.IdEmpresa == IdEmpresa
                              && q.IdEmpleado==IdEmpleado
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdContrato) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_contrato_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado Entity_Empleado = Context.ro_empleado.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado);
                    if (Entity_Empleado == null)
                        return false;

                    Entity_Empleado.em_fechaIngaRol = info.FechaInicio.Date;

                    ro_contrato Entity = new ro_contrato
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpleado=info.IdEmpleado,
                        IdContrato = get_id(info.IdEmpresa, info.IdEmpleado),
                        IdContrato_Tipo = info.IdContrato_Tipo,
                        Observacion = info.Observacion,
                        FechaInicio = info.FechaInicio.Date,
                        NumDocumento = info.NumDocumento,
                        FechaFin = info.FechaFin.Date,
                        Sueldo=info.Sueldo,
                        IdNomina=info.IdNomina,
                        Estado = info.Estado = "A",
                        IdUsuario = info.IdUsuario,
                        EstadoContrato=info.EstadoContrato,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    if (info.EstadoContrato == cl_enumeradores.eEstadoContratoRRHH.ECT_ACT.ToString())
                    {
                        Entity_Empleado.em_fechaSalida = null;
                    }
                    else
                    {
                        Entity_Empleado.em_fechaSalida = info.FechaFin.Date;
                    }
                    Context.ro_contrato.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_contrato_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_contrato Entity = Context.ro_contrato.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado && q.IdContrato == info.IdContrato);
                    if (Entity == null)
                        return false;
                    Entity.IdContrato_Tipo = info.IdContrato_Tipo;
                    Entity.Observacion = info.Observacion;
                    Entity.FechaInicio = info.FechaInicio.Date;
                    Entity.FechaFin = info.FechaFin.Date;
                    Entity.NumDocumento = info.NumDocumento;
                    Entity.Sueldo = info.Sueldo;
                    Entity.IdNomina = info.IdNomina;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.EstadoContrato = info.EstadoContrato;
                    Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;

                    ro_empleado Entity_Empleado = Context.ro_empleado.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado);
                    if (Entity_Empleado == null)
                        return false;
                    if(info.EstadoContrato == cl_enumeradores.eEstadoContratoRRHH.ECT_ACT.ToString())
                    {
                        Entity_Empleado.em_fechaSalida = null;
                    }
                    else
                    {
                        Entity_Empleado.em_fechaSalida = info.FechaFin.Date;
                    }
                    Entity_Empleado.em_fechaIngaRol = info.FechaInicio.Date;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_contrato_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_contrato Entity = Context.ro_contrato.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado==info.IdEmpleado && q.IdContrato == info.IdContrato);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.FechaHoraAnul  = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public double get_ultimo_sueldo(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                Double SUELDO = 0;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_contrato
                              where q.IdEmpresa == IdEmpresa
                              && q.IdEmpleado == IdEmpleado
                              && q.EstadoContrato == "ECT_ACT"
                              select q;

                    if (lst.Count() > 0)
                        SUELDO =Convert.ToDouble( lst.Max(q => q.Sueldo));
                }

                return SUELDO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_contrato_Info get_info_contato_a_liquidar(int IdEmpresa, decimal IdEmpleado)
        {

            try
            {
                List<ro_contrato_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from cont in Context.ro_contrato
                             join emp in Context.ro_empleado
                             on new { cont.IdEmpresa, cont.IdEmpleado } equals new { emp.IdEmpresa, emp.IdEmpleado }
                             //join cat in Context.ro_catalogo
                             //on cont.IdContrato_Tipo equals cat.CodCatalogo
                             where cont.IdEmpresa == IdEmpresa
                             && emp.IdEmpleado == IdEmpleado
                             && cont.EstadoContrato!= "ECT_LIQ"
                             && cont.Estado=="A"
                             select new ro_contrato_Info
                             {
                                 IdEmpresa = cont.IdEmpresa,
                                 IdEmpleado = cont.IdEmpleado,
                                 IdContrato = cont.IdContrato,
                                 IdContrato_Tipo = cont.IdContrato_Tipo,
                                 Observacion = cont.Observacion,
                                 FechaInicio = cont.FechaInicio,
                                 FechaFin = cont.FechaFin,
                                 NumDocumento = cont.NumDocumento,
                                 Estado = cont.Estado,
                                 EstadoContrato = cont.EstadoContrato,
                                 em_fechaSalida=emp.em_fechaSalida,
                                 Sueldo=cont.Sueldo
                             }).ToList();

                }

                return Lista.FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal get_sueldo_actual(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                decimal sueldo = 0;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_contrato
                              where q.IdEmpresa == IdEmpresa
                              && q.IdEmpleado == IdEmpleado
                              && q.EstadoContrato!= "ECT_LIQ"
                              && q.Estado=="A"
                              select q;

                    if (lst.Count() > 0)
                        sueldo =Convert.ToDecimal( lst.Max(q => q.Sueldo));
                }

                return sueldo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_contrato_Info get_info_contrato_empleado(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                ro_contrato_Info info = new ro_contrato_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_contrato Entity = Context.ro_contrato.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado == IdEmpleado && q.Estado == "A");
                    if (Entity == null) return null;

                    info = new ro_contrato_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEmpleado = Entity.IdEmpleado,
                        IdContrato = Entity.IdContrato,
                        IdContrato_Tipo = Entity.IdContrato_Tipo,
                        Observacion = Entity.Observacion,
                        FechaInicio = Entity.FechaInicio.Date,
                        FechaFin = Entity.FechaFin.Date,
                        NumDocumento = Entity.NumDocumento,
                        EstadoContrato = Entity.EstadoContrato,
                        Estado = Entity.Estado,
                        Sueldo = Entity.Sueldo,
                        IdNomina = Entity.IdNomina,
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
