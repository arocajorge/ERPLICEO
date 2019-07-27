using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;

namespace Core.Erp.Data.RRHH
{
   public class ro_horario_planificacion_det_Data
    {
        public List<ro_horario_planificacion_det_Info> get_list(int IdEmpresa, decimal IdPlanificacion)
        {
            try
            { int cont = 0;
                List<ro_horario_planificacion_det_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_horario_planificacion_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdPlanificacion == IdPlanificacion

                             select new ro_horario_planificacion_det_Info
                             {
                                 
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_apellido + " " + q.pe_nombre,
                                 ca_descripcion = q.ca_descripcion,
                                 de_descripcion = q.de_descripcion,
                                 di_descripcion = q.di_descripcion,
                                 ar_descripcion = q.ar_descripcion,
                                 Su_Descripcion = q.Su_Descripcion,
                                 IdHorario = q.IdHorario,
                                 fecha = q.fecha,
                                 IdCalendario = q.IdCalendario,
                                 Secuencia=cont

                             }).ToList();
                }
                Lista.ForEach(v => { cont++; v.Secuencia=cont; });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_horario_planificacion_det_Info> get_list(int IdEmpresa, int IdNomina, int IdSucursal, int IdDivision, int IdArea, int IdDepartamento, int IdCargo, decimal IdEmpleado)
        {
            try
            {
                #region variable
                int idnomina_inicio;
                int idnomina_fin;
                int idsucursal_inicio;
                int idsucursal_fin;
                int iddivision_inicio;
                int iddivision_fin;
                int idarea_inicio;
                int idara_fin;
                int iddepartamento_inicio;
                int iddepartamento_fin;
                int idcargo_inicio;
                int idcargo_fin;
                decimal IdEmpleado_inicio;
                decimal IdEmpleado_fin;
                #endregion
                if (IdNomina==0 )
                {idnomina_inicio = 0;idnomina_fin = 99; }else { idnomina_inicio = IdNomina; idnomina_fin = IdNomina; }

                if (IdSucursal == 0)
                { idsucursal_inicio = 0; idsucursal_fin = 999; }else { idsucursal_inicio = IdSucursal; idsucursal_fin = IdSucursal; }

                if (IdDivision == 0)
                { iddivision_inicio = 0; iddivision_fin = 999; }else { iddivision_inicio = IdDivision; iddivision_fin = IdDivision; }

                if (IdArea == 0)
                { idarea_inicio = 0; idara_fin = 999; }else { idarea_inicio = IdArea; idara_fin = IdArea; }

                if (IdDepartamento == 0)
                { iddepartamento_inicio = 0; iddepartamento_fin = 9999; }else { iddepartamento_inicio = IdDepartamento; iddepartamento_fin = IdDepartamento; }

                if (IdCargo == 0)
                { idcargo_inicio = 0; idcargo_fin = 9999; }else { idcargo_inicio = IdCargo; idcargo_fin = IdCargo; }

                if (IdEmpleado == 0)
                { IdEmpleado_inicio = 0; IdEmpleado_fin = 9999; }
                else { IdEmpleado_inicio = IdEmpleado; IdEmpleado_fin = IdEmpleado; }
                List<ro_horario_planificacion_det_Info> Lista;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_horario_planificacion_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdNomina >= idnomina_inicio
                             && q.IdNomina <= idnomina_fin

                              && q.IdSucursal >= idsucursal_inicio
                             && q.IdSucursal <= idsucursal_fin

                              && q.IdDivision >= iddivision_inicio
                             && q.IdDivision <= iddivision_fin

                              && q.IdArea >= idarea_inicio
                             && q.IdArea <= idara_fin

                              && q.IdDepartamento >= iddepartamento_inicio
                             && q.IdDepartamento <= iddepartamento_fin

                              && q.IdCargo >= idcargo_inicio
                             && q.IdCargo <= idcargo_fin

                              && q.IdEmpleado >= IdEmpleado_inicio
                             && q.IdEmpleado <= IdEmpleado_fin

                             select new ro_horario_planificacion_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_apellido + " " + q.pe_nombre,
                                 ca_descripcion=q.ca_descripcion,
                                 de_descripcion=q.de_descripcion,
                                 di_descripcion=q.di_descripcion,
                                 ar_descripcion=q.ar_descripcion,
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

        public ro_horario_planificacion_det_Info get_info(int IdEmpresa, decimal IdPlanificacion)
        {
            try
            {
                ro_horario_planificacion_det_Info info = new ro_horario_planificacion_det_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_horario_planificacion_det Entity = Context.ro_horario_planificacion_det.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdPlanificacion == IdPlanificacion);
                    if (Entity == null) return null;

                    info = new ro_horario_planificacion_det_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEmpleado = Entity.IdEmpleado,
                        IdHorario = Entity.IdHorario,
                        IdCalendario = Entity.IdCalendario,
                        IdPlanificacion = Entity.IdPlanificacion,
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
        public bool guardarDB(List<ro_horario_planificacion_det_Info> info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {

                    info.ForEach(item =>
                        {
                            ro_horario_planificacion_det Entity = new ro_horario_planificacion_det
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdEmpleado = item.IdEmpleado,
                                IdPlanificacion = item.IdPlanificacion,
                                IdCalendario =Convert.ToInt32( item.IdCalendario),
                                IdHorario =Convert.ToDecimal( item.IdHorario),
                                fecha=Convert.ToDateTime( item.fecha),
                                Estado = item.Estado = "A",
                                Observacion = item.Observacion
                            };
                            if (!si_existe(item))
                            {
                                Context.ro_horario_planificacion_det.Add(Entity);
                                Context.SaveChanges();
                            }

                        });
                    
                }
                return true;
            }
            catch (Exception )
            {

                throw;
            }
        }
        public bool modificarDB(ro_horario_planificacion_det_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_horario_planificacion_det Entity = Context.ro_horario_planificacion_det.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado && q.IdCalendario == info.IdCalendario && q.IdPlanificacion == info.IdPlanificacion);
                    if (Entity == null)
                        return false;
                    Entity.IdHorario =Convert.ToInt32( info.IdHorario);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(ro_horario_planificacion_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {

                    string sql = "delete ro_horario_planificacion_det where IdEmpresa='"+info.IdEmpresa+"' and IdPlanificacion='"+info.IdPlanificacion+"'";
                    Context.Database.ExecuteSqlCommand(sql);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool si_existe(ro_horario_planificacion_det_Info info)
        {
            try
            {

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_horario_planificacion_det
                              where q.IdEmpresa == info.IdEmpresa
                              && q.IdEmpleado == info.IdEmpleado
                              && q.IdCalendario == info.IdCalendario
                              select q;

                    if (lst.Count() > 0)
                        return true;
                    else return false;
                       
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
