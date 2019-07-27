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
   public class ro_empleado_x_ro_rubro_Data
    {
        public List<ro_empleado_x_ro_rubro_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<ro_empleado_x_ro_rubro_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.vwro_empleado_x_ro_rubro
                             where q.IdEmpresa == IdEmpresa
                             select new ro_empleado_x_ro_rubro_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdRubroFijo=q.IdRubroFijo,
                                 IdEmpleado = q.IdEmpleado,
                                 IdNomina_Tipo = q.IdNomina_Tipo,
                                 IdNomina_TipoLiqui = q.IdNomina_TipoLiqui,
                                 Valor = q.Valor,
                                 IdRubro=q.IdRubro,
                                 pe_nombreCompleto=q.pe_nombreCompleto,
                                 Descripcion=q.Descripcion,
                                 DescripcionProcesoNomina=q.DescripcionProcesoNomina,
                                 ru_descripcion=q.ru_descripcion,
                                 pe_cedulaRuc=q.pe_cedulaRuc,
                                 FechaFin=q.FechaFin,
                                 FechaInicio=q.FechaInicio,
                                 es_indifinido=q.es_indifinido


                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_empleado_x_ro_rubro_Info get_info(int IdEmpresa, int IdRubroFijo)
        {
            try
            {
                ro_empleado_x_ro_rubro_Info info = new ro_empleado_x_ro_rubro_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_ro_rubro Entity = Context.ro_empleado_x_ro_rubro.FirstOrDefault(q => q.IdEmpresa == IdEmpresa 
                    && q.IdRubroFijo==IdRubroFijo);
                    if (Entity == null) return null;

                    info = new ro_empleado_x_ro_rubro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdRubroFijo=Entity.IdRubroFijo,
                        IdEmpleado = Entity.IdEmpleado,
                        IdNomina_Tipo = Entity.IdNomina_Tipo,
                        IdNomina_TipoLiqui = Entity.IdNomina_TipoLiqui,
                        Valor = Entity.Valor,
                        IdRubro = Entity.IdRubro,
                        FechaInicio=Entity.FechaInicio,
                        FechaFin=Entity.FechaFin,
                        es_indifinido= Entity.es_indifinido
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_empleado_x_ro_rubro_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_ro_rubro Entity = new ro_empleado_x_ro_rubro
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdRubroFijo=get_id(info.IdEmpresa),
                        IdEmpleado = info.IdEmpleado,
                        IdNomina_Tipo = info.IdNomina_Tipo,
                        IdNomina_TipoLiqui = info.IdNomina_TipoLiqui,
                        Valor = info.Valor,
                        IdRubro = info.IdRubro,
                        FechaInicio=info.FechaInicio,
                        FechaFin=info.FechaFin,
                        es_indifinido =info.es_indifinido,

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.ro_empleado_x_ro_rubro.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_empleado_x_ro_rubro_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(ro_empleado_x_ro_rubro_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_ro_rubro Entity = Context.ro_empleado_x_ro_rubro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdRubroFijo == info.IdRubroFijo);
                    if (Entity == null)
                        return false;
                    Entity.IdUsuarioUltAnu= info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
                    Context.ro_empleado_x_ro_rubro.Remove(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_empleado_x_ro_rubro_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_empleado_x_ro_rubro Entity = Context.ro_empleado_x_ro_rubro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdRubroFijo == info.IdRubroFijo);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpleado = info.IdEmpleado;
                    Entity.IdRubro = info.IdRubro;
                    Entity.IdNomina_Tipo = info.IdNomina_Tipo;
                    Entity.IdNomina_TipoLiqui = info.IdNomina_TipoLiqui;
                    Entity.Valor = info.Valor;
                    Entity.FechaFin = info.FechaFin;
                    Entity.FechaInicio = info.FechaInicio;
                    Entity.es_indifinido = info.es_indifinido;

                    Entity.IdUsuarioUltMod= info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_empleado_x_ro_rubro_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_empleado_x_ro_rubro
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdRubroFijo) + 1;
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
