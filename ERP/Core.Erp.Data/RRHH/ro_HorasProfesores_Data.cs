using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
   public class ro_HorasProfesores_Data
    {
        ro_empleado_novedad_Data odata_novedad = new ro_empleado_novedad_Data();
        public List<ro_HorasProfesores_Info> get_list(int IdEmpresa, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                FechaFin = Convert.ToDateTime(FechaFin.Date.ToShortDateString());
                FechaInicio = Convert.ToDateTime(FechaInicio.Date.ToShortDateString());
                List<ro_HorasProfesores_Info> lista;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    lista = (from q in Context.vwro_HorasProfesores
                             where q.IdEmpresa == IdEmpresa
                             && q.FechaCarga >= FechaInicio
                             && q.FechaCarga <= FechaFin
                             select new ro_HorasProfesores_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCarga = q.IdCarga,
                                 FechaCarga = q.FechaCarga,
                                 Observacion = q.Observacion,
                                 EstadoBool = q.Estado,
                                 Descripcion = q.Descripcion,
                                 DescripcionProcesoNomina = q.DescripcionProcesoNomina,

                             }
                           ).ToList();


                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool GuardarDB(ro_HorasProfesores_Info info)
        {
            try
            {
                decimal IdNovedad = odata_novedad.get_id(info.IdEmpresa);


                using (Entities_rrhh Contex = new Entities_rrhh())
                {

                    ro_HorasProfesores entity = new ro_HorasProfesores
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCarga = info.IdCarga = Get_id(info.IdEmpresa),
                        FechaCarga = info.FechaCarga.Date,
                        IdPeriodo=info.IdPeriodo=Convert.ToInt32( info.FechaCarga.Year.ToString()+info.FechaCarga.Month.ToString().PadLeft(2,'0')),
                        Observacion = info.Observacion,
                        IdNomina = info.IdNomina,
                        IdNominaTipo = info.IdNominaTipo,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now,
                        Estado = true
                    };
                    Contex.ro_HorasProfesores.Add(entity);

                    foreach (var item in info.detalle)
                    {


                        ro_empleado_Novedad Entity = new ro_empleado_Novedad
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdNovedad = item.IdNovedad = IdNovedad,
                            IdNomina_Tipo = info.IdNomina,
                            IdNomina_TipoLiqui = info.IdNominaTipo,
                            IdEmpleado = item.IdEmpleado,
                            Fecha = info.FechaCarga.Date,
                            IdSucursal=item.IdSucursal,
                            Observacion =item.ru_descripcion,
                            Estado = "A",
                            IdUsuario = info.IdUsuario,
                            Fecha_Transac = info.Fecha_Transac = DateTime.Now,
                            IdJornada=item.IdJornada
                        };
                        Contex.ro_empleado_Novedad.Add(Entity);

                        ro_empleado_novedad_det Entity_det = new ro_empleado_novedad_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdNovedad = item.IdNovedad,
                            FechaPago = info.FechaCarga.Date,
                            IdRubro = item.IdRubro,
                            Valor = item.Valor,
                            Observacion = item.Observacion + " del " + info.FechaCarga.Date.ToString().Substring(0, 10),
                             EstadoCobro = "PEN",
                             CantidadHoras = item.NumHoras,
                            Secuencia = 1
                        };
                           
                        Contex.ro_empleado_novedad_det.Add(Entity_det);

                        ro_HorasProfesores_det Entity_det_ = new ro_HorasProfesores_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdCarga = info.IdCarga,
                            IdNovedad = item.IdNovedad,
                            IdRubro=item.IdRubro,
                            IdEmpleado = item.IdEmpleado,
                            Observacion = item.Observacion,
                            Secuencia = item.Secuencia,
                            IdEmpresa_nov = info.IdEmpresa,
                            ValorHora=item.ValorHora,
                            NumHoras=item.NumHoras,
                            IdSucursal=item.IdSucursal
                        };
                        Contex.ro_HorasProfesores_det.Add(Entity_det_);
                        IdNovedad++;
                    }
                    Contex.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AnularDB(ro_HorasProfesores_Info info)
        {
            try
            {
                ro_HorasProfesores_det_Data oda_det = new ro_HorasProfesores_det_Data();
                var lista = oda_det.get_list(info.IdEmpresa, info.IdCarga);
                using (Entities_rrhh contex = new Entities_rrhh())
                {
                    foreach (var item in lista)
                    {
                        ro_empleado_Novedad entity_det = contex.ro_empleado_Novedad.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == item.IdEmpleado && q.IdNovedad == item.IdNovedad);
                        if (entity_det != null)
                        {
                            string sql = "update ro_empleado_Novedad set Estado='I' where IdEmpresa='" + info.IdEmpresa + "' and IdEmpleado='" + item.IdEmpleado + "' and IdNovedad='" + item.IdNovedad + "'";
                            contex.Database.ExecuteSqlCommand(sql);
                            entity_det.Estado = "I";
                            entity_det.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                            entity_det.Fecha_UltAnu = DateTime.Now;
                            contex.SaveChanges();

                        }

                    }

                }

                using (Entities_rrhh contex = new Entities_rrhh())
                {
                    ro_HorasProfesores entity = contex.ro_HorasProfesores.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCarga == info.IdCarga);

                    if (entity == null)
                        return false;
                    entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    entity.Estado = false;
                    contex.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_HorasProfesores_Info get_info(int IdEmpresa, decimal IdCarga)
        {
            try
            {
                ro_HorasProfesores_Info info = new ro_HorasProfesores_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_HorasProfesores Entity = Context.ro_HorasProfesores.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCarga == IdCarga);
                    if (Entity == null) return null;

                    info = new ro_HorasProfesores_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCarga = Entity.IdCarga,
                        FechaCarga = Entity.FechaCarga,
                        Estado = Entity.Estado,
                        Observacion = Entity.Observacion,
                        IdNomina = Entity.IdNomina,
                        IdNominaTipo = Entity.IdNominaTipo,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private decimal Get_id(int IdEmpresa)
        {
            try
            {
                decimal Id = 1;
                using (Entities_rrhh Contex = new Entities_rrhh())
                {
                    var list = from q in Contex.ro_HorasProfesores
                               select q;
                    if (list.Count() > 0)
                        Id = list.Max(v => v.IdCarga) + 1;

                    return Id;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
