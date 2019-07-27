using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_empleado_novedad_det_Data
    {
        
        public List<ro_empleado_novedad_det_Info> get_list(int IdEmpresa,  decimal IdNovedad)
        {
            try
            {
                List<ro_empleado_novedad_det_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_empleado_novedad_det
                             where q.IdEmpresa == IdEmpresa
                                   && q.IdNovedad==IdNovedad
                             select new ro_empleado_novedad_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdNovedad = q.IdNovedad,
                                 FechaPago = q.FechaPago,
                                 Observacion = q.Observacion,
                                 Valor = q.Valor,
                                 IdRubro = q.IdRubro ,
                                 Secuencia=q.Secuencia ,
                                 ru_descripcion = q.ru_descripcion,
                                 CantidadHoras = q.CantidadHoras
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_empleado_novedad_det_Info> get_list_nov_liq_empleado(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                List<ro_empleado_novedad_det_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.ro_empleado_Novedad

                             join s in Context.ro_empleado_novedad_det
                             on new { q.IdEmpresa,q.IdNovedad } equals new { s.IdEmpresa, s.IdNovedad }


                             join p in Context.ro_rubro_tipo
                             on new { s.IdEmpresa, s.IdRubro} equals new {p.IdEmpresa, p.IdRubro}
                                 where q.IdEmpresa == IdEmpresa
                                   && s.EstadoCobro=="PEN"
                                   && q.IdEmpleado==IdEmpleado
                                   && q.IdEmpresa==IdEmpresa
                                   && s.IdEmpresa==IdEmpresa
                                   && p.IdEmpresa==IdEmpresa
                             select new ro_empleado_novedad_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdNovedad = q.IdNovedad,
                                 FechaPago = s.FechaPago,
                                 Observacion = q.Observacion,
                                 Valor = s.Valor,
                                 IdRubro = p.IdRubro,
                                 Secuencia = s.Secuencia,
                                 rub_tipo=p.ru_tipo,
                                 ru_descripcion = p.ru_descripcion
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_empleado_novedad_det_Info get_info(int IdEmpresa, decimal IdEmpleado, decimal IdNovedad, int Secuencia)
        {
            try
            {
                ro_empleado_novedad_det_Info info = new ro_empleado_novedad_det_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_novedad_det Entity = Context.ro_empleado_novedad_det.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdNovedad == IdNovedad && q.Secuencia==Secuencia);
                    if (Entity == null) return null;

                    info = new ro_empleado_novedad_det_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdNovedad = Entity.IdNovedad,
                        FechaPago = Entity.FechaPago,
                        CantidadHoras = Entity.CantidadHoras
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(List<ro_empleado_novedad_det_Info> info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    foreach (var item in info)
                    {
                        ro_empleado_novedad_det Entity = new ro_empleado_novedad_det
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdNovedad = item.IdNovedad ,
                            FechaPago = item.FechaPago.Date,
                            Secuencia=item.Secuencia,
                            IdRubro=item.IdRubro,
                            CantidadHoras = item.CantidadHoras,
                            Valor =item.Valor,
                            Observacion=item.Observacion,
                            EstadoCobro = item.EstadoCobro="PEN",
                            
                    };
                        Context.ro_empleado_novedad_det.Add(Entity);
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
        public bool eliminarDB(ro_empleado_novedad_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    string sql = "delete ro_empleado_novedad_det where IdEmpresa='"+info.IdEmpresa+ "' and  IdNovedad='"+info.IdNovedad+"'";
                    Context.Database.ExecuteSqlCommand(sql);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
  

        public decimal get_valor_acumulado_del_mes_x_rubro(int IdEmpresa, decimal IdEmpleado, string IdRubro, DateTime Fi, DateTime Ff)
        {
            try
            {
                decimal valor = 0;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_empleado_novedad_det
                              where q.IdEmpresa == IdEmpresa
                              && q.IdRubro==IdRubro
                              && q.FechaPago>=Fi
                              && q.FechaPago <=Ff

                              select q;

                    if (lst.Count() > 0)
                        valor =Convert.ToDecimal( lst.Sum(q => q.Valor));
                }

                return valor;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
