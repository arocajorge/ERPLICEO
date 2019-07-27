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
  public  class ro_empleado_x_rubro_acumulado_Data
  {
        public List<ro_empleado_x_rubro_acumulado_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<ro_empleado_x_rubro_acumulado_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_empleado_x_rubro_acumulado
                             where q.IdEmpresa == IdEmpresa
                             select new ro_empleado_x_rubro_acumulado_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 IdRubro = q.IdRubro,
                                 Fec_Inicio_Acumulacion = q.Fec_Inicio_Acumulacion,
                                 Fec_Fin_Acumulacion = q.Fec_Fin_Acumulacion,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_apellido + " " + q.pe_nombre,
                                 ru_descripcion = q.ru_descripcion,
                                 em_codigo = q.em_codigo
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_empleado_x_rubro_acumulado_Info get_info(int IdEmpresa, decimal IdEmpleado, string IdRubro)
        {
            try
            {
                ro_empleado_x_rubro_acumulado_Info info = new ro_empleado_x_rubro_acumulado_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_rubro_acumulado Entity = Context.ro_empleado_x_rubro_acumulado.FirstOrDefault(q => q.IdEmpresa == IdEmpresa
                    && q.IdEmpleado == IdEmpleado
                    && q.IdRubro == IdRubro);
                    if (Entity == null) return null;

                    info = new ro_empleado_x_rubro_acumulado_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEmpleado = Entity.IdEmpleado,
                        IdRubro = Entity.IdRubro,
                        Fec_Inicio_Acumulacion = Entity.Fec_Inicio_Acumulacion,
                        Fec_Fin_Acumulacion = Entity.Fec_Fin_Acumulacion
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool si_existe(int IdEmpresa, decimal IdEmpleado, string IdRubro)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_empleado_x_rubro_acumulado
                              where q.IdEmpresa == IdEmpresa
                              && q.IdEmpleado == IdEmpleado
                              && q.IdRubro == IdRubro
                              select q;

                    if (lst.Count() > 0)
                        return true;
                    else
                        return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_empleado_x_rubro_acumulado_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_rubro_acumulado Entity = new ro_empleado_x_rubro_acumulado
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpleado = info.IdEmpleado,
                        IdRubro = info.IdRubro,
                        Fec_Inicio_Acumulacion = info.Fec_Inicio_Acumulacion,
                        Fec_Fin_Acumulacion = info.Fec_Inicio_Acumulacion,
                        FechaIngresa = info.FechaIngresa = DateTime.Now,
                        UsuarioIngresa = info.UsuarioIngresa

                    };
                    Context.ro_empleado_x_rubro_acumulado.Add(Entity);

                    //var Secuencia = 1;
                    //foreach (var item in info.lst_empleado_x_rubro_acumulado_detalle)
                    //{
                    //    ro_empleado_x_rubro_acumulado_detalle Entity_Det = new ro_empleado_x_rubro_acumulado_detalle
                    //    {
                    //        IdEmpresa = info.IdEmpresa,
                    //        IdEmpleado = info.IdEmpleado,
                    //        IdRubro = info.IdRubro,
                    //        IdRubroContabilizacion = item.IdRubroContabilizacion,
                    //        IdJornada = item.IdJornada,
                    //        Secuencia = Secuencia++

                    //    };
                    //    Context.ro_empleado_x_rubro_acumulado_detalle.Add(Entity_Det);
                    //}

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "(ro_empleado_x_rubro_acumulado_Data", Metodo = "guardarDB", IdUsuario = "" });
                return false;
            }
        }

        public bool modificarDB(ro_empleado_x_rubro_acumulado_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_rubro_acumulado Entity = Context.ro_empleado_x_rubro_acumulado.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdRubro == info.IdRubro
                    && q.IdEmpleado == info.IdEmpleado);
                    if (Entity == null)
                        return false;

                    //var lst_det = Context.ro_empleado_x_rubro_acumulado_detalle.Where(v => v.IdEmpresa == info.IdEmpresa && v.IdEmpleado == info.IdEmpleado && v.IdRubro == info.IdRubro);
                    //Context.ro_empleado_x_rubro_acumulado_detalle.RemoveRange(lst_det);

                    //if (info.lst_empleado_x_rubro_acumulado_detalle.Count() > 0)
                    //{
                    //    foreach (var item in info.lst_empleado_x_rubro_acumulado_detalle)
                    //    {
                    //        Context.ro_empleado_x_rubro_acumulado_detalle.Add(new ro_empleado_x_rubro_acumulado_detalle
                    //        {
                    //            IdEmpresa = info.IdEmpresa,
                    //            IdEmpleado = info.IdEmpleado,
                    //            IdRubro = info.IdRubro,
                    //            Secuencia = item.Secuencia,
                    //            IdJornada = item.IdJornada,
                    //            IdRubroContabilizacion = item.IdRubroContabilizacion
                    //        });
                    //    }
                    //}

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "(ro_empleado_x_rubro_acumulado_Data", Metodo = "modificarDB", IdUsuario = "" });
                return false;
            }
        }

        public bool anularDB(ro_empleado_x_rubro_acumulado_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_rubro_acumulado Entity = Context.ro_empleado_x_rubro_acumulado.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdRubro == info.IdRubro
                    && q.IdEmpleado == info.IdEmpleado);
                    if (Entity == null)
                        return false;

                    //var lst_det = Context.ro_empleado_x_rubro_acumulado_detalle.Where(v => v.IdEmpresa == info.IdEmpresa && v.IdEmpleado == info.IdEmpleado && v.IdRubro == info.IdRubro);

                    //Context.ro_empleado_x_rubro_acumulado_detalle.RemoveRange(lst_det);
                    Context.ro_empleado_x_rubro_acumulado.Remove(Entity);

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
