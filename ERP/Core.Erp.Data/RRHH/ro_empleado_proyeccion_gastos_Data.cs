using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_empleado_proyeccion_gastos_Data
    {
        public List<ro_empleado_proyeccion_gastos_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<ro_empleado_proyeccion_gastos_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        Lista = (from q in Context.vwro_empleado_x_Proyeccion_Gastos_Personales
                                 where q.IdEmpresa == IdEmpresa
                                 select new ro_empleado_proyeccion_gastos_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTransaccion=q.IdTransaccion,
                                     IdEmpleado = q.IdEmpleado,
                                     AnioFiscal = q.AnioFiscal,
                                     pe_cedulaRuc=q.pe_cedulaRuc,
                                     pe_nombreCompleto=q.pe_nombreCompleto,
                                     estado=q.estado
                                    
                                 }).ToList();
                  
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_empleado_proyeccion_gastos_Info get_info(int IdEmpresa, decimal IdEmpleado, int IdTransaccion)
        {
            try
            {
                ro_empleado_proyeccion_gastos_Info info = new ro_empleado_proyeccion_gastos_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_proyeccion_gastos Entity = Context.ro_empleado_proyeccion_gastos.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado == IdEmpleado && q.IdTransaccion== IdTransaccion);
                    if (Entity == null) return null;

                    info = new ro_empleado_proyeccion_gastos_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTransaccion=Entity.IdTransaccion,
                        IdEmpleado = Entity.IdEmpleado,
                        AnioFiscal = Entity.AnioFiscal,
                        Observacion=Entity.Observacion,

                        
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
       public bool guardarDB(ro_empleado_proyeccion_gastos_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_proyeccion_gastos Entity = new ro_empleado_proyeccion_gastos
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTransaccion =info.IdTransaccion= get_id(info.IdEmpresa),
                        IdEmpleado = info.IdEmpleado,
                        AnioFiscal = info.AnioFiscal,
                        Observacion=info.Observacion,
                        estado = true,
                        Fecha_Transac=DateTime.Now
                        
                    };
                    Context.ro_empleado_proyeccion_gastos.Add(Entity);

                    foreach (var item in info.list_proyeciones)
                    {
                        ro_empleado_proyeccion_gastos_det Entity_det  = new ro_empleado_proyeccion_gastos_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdTransaccion = info.IdTransaccion,
                            IdTipoGasto = item.IdTipoGasto,
                            Valor = item.Valor,
                            Observacion = item.Observacion,
                            Secuencia = item.Secuencia
                        };
                        Context.ro_empleado_proyeccion_gastos_det.Add(Entity_det);
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
       public bool modificarDB(ro_empleado_proyeccion_gastos_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_proyeccion_gastos Entity = Context.ro_empleado_proyeccion_gastos.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTransaccion==info.IdTransaccion);
                    if (Entity == null)
                        return  false;
                    Entity.Observacion = info.Observacion;
                    Entity.IdEmpleado = info.IdEmpleado;
                    Entity.AnioFiscal = info.AnioFiscal;

                    var select = Context.ro_empleado_proyeccion_gastos_det.Where(q => q.IdEmpresa==info.IdEmpresa&& q.IdTransaccion==info.IdTransaccion);
                    Context.ro_empleado_proyeccion_gastos_det.RemoveRange(select);
                    foreach (var item in info.list_proyeciones)
                    {
                        ro_empleado_proyeccion_gastos_det Entity_det = new ro_empleado_proyeccion_gastos_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdTransaccion = info.IdTransaccion,
                            IdTipoGasto = item.IdTipoGasto,
                            Valor = item.Valor,
                            Observacion = item.Observacion,
                            Secuencia = item.Secuencia
                        };
                        Context.ro_empleado_proyeccion_gastos_det.Add(Entity_det);
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
        public bool anularDB(ro_empleado_proyeccion_gastos_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    //ro_empleado_proyeccion_gastos Entity = Context.ro_empleado_proyeccion_gastos.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTransaccion == info.IdTransaccion);

                    //ro_empleado_proyeccion_gastos_det EntityDet = Context.ro_empleado_proyeccion_gastos_det.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTransaccion == info.IdTransaccion);
                    //if (Entity == null)
                    //    return false;
                    //Entity.estado = info.estado = false;
                    //Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    //Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;

                    var sql_cab = "delete from ro_empleado_proyeccion_gastos_det where IdEmpresa ="+ info.IdEmpresa+ " and IdTransaccion = "+info.IdTransaccion;
                    var sql_det = "delete from ro_empleado_proyeccion_gastos where IdEmpresa =" + info.IdEmpresa + " and IdTransaccion = " + info.IdTransaccion;

                    Context.Database.ExecuteSqlCommand(sql_cab);
                    Context.Database.ExecuteSqlCommand(sql_det);
                    //Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }



        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_empleado_proyeccion_gastos
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
    }
}
