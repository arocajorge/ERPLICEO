using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
using DevExpress.Web;

namespace Core.Erp.Data.RRHH
{
   public class ro_rubro_tipo_Data
    {


        public List<ro_rubro_tipo_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ro_rubro_tipo_Info> Lista = new List<ro_rubro_tipo_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter);
            return Lista;
        }

        public ro_rubro_tipo_Info get_info_bajo_demanda(int IdEmpresa, ListEditItemRequestedByValueEventArgs args)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda(IdEmpresa,Convert.ToString( args.Value));
        }

        public List<ro_rubro_tipo_Info> get_list(int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<ro_rubro_tipo_Info> Lista = new List<ro_rubro_tipo_Info>();

                Entities_rrhh context_g = new Entities_rrhh();

                var lstg = context_g.ro_rubro_tipo.Where(q => q.ru_estado == "A" && q.IdEmpresa == IdEmpresa && (q.IdRubro.ToString() + " " + q.ru_descripcion).Contains(filter)).OrderBy(q => q.IdRubro).Skip(skip).Take(take);
                foreach (var q in lstg)
                {
                    Lista.Add(new ro_rubro_tipo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdRubro=q.IdRubro,
                        rub_codigo = q.rub_codigo,
                        ru_codRolGen = q.ru_codRolGen,
                        ru_descripcion=q.ru_descripcion,
                        NombreCorto=q.NombreCorto
                    });
                }

                context_g.Dispose();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_rubro_tipo_Info get_info_demanda(int IdEmpresa, string value)
        {
            ro_rubro_tipo_Info info = new ro_rubro_tipo_Info();
            using (Entities_rrhh Contex = new Entities_rrhh())
            {
                info = (from q in Contex.ro_rubro_tipo
                        where q.IdEmpresa == IdEmpresa
                        && q.IdRubro==value
                        select new ro_rubro_tipo_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdRubro=q.IdRubro,
                            rub_codigo = q.rub_codigo,
                            ru_codRolGen = q.ru_codRolGen,
                            ru_descripcion = q.ru_descripcion,
                            NombreCorto = q.NombreCorto
                        }).FirstOrDefault();
            }
            return info;
        }



        public List<ro_rubro_tipo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_rubro_tipo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ro_rubro_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 select new ro_rubro_tipo_Info
                                 {
                                    IdRubro=q.IdRubro,
                                    rub_codigo =q.rub_codigo,
                                    ru_codRolGen =q.ru_codRolGen,
                                    ru_descripcion = q.ru_descripcion,
                                    NombreCorto = q.NombreCorto,
                                    ru_tipo = q.ru_tipo,
                                    ru_orden = q.ru_orden,
                                    rub_grupo = q.rub_grupo,
                                     rub_concep = q.rub_concep ,
                                     rub_nocontab = q.rub_nocontab,
                                     rub_ctacon = q.rub_ctacon,
                                    ru_estado = q.ru_estado,
                                     rub_acumula_descuento = q.rub_acumula_descuento,
                                     rub_AplicaIR=q.rub_AplicaIR,
                                     rub_GrupoResumen=q.rub_GrupoResumen,
                                     EstadoBool = q.ru_estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_rubro_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.ru_estado == "A"
                                 select new ro_rubro_tipo_Info
                                 {
                                     IdRubro = q.IdRubro,
                                     rub_codigo = q.rub_codigo,
                                     ru_codRolGen = q.ru_codRolGen,
                                     ru_descripcion = q.ru_descripcion,
                                     NombreCorto = q.NombreCorto,
                                     ru_tipo = q.ru_tipo,
                                     ru_orden = q.ru_orden,
                                     rub_grupo = q.rub_grupo,
                                     rub_concep = q.rub_concep,
                                     rub_nocontab = q.rub_nocontab,
                                     rub_ctacon = q.rub_ctacon,
                                     ru_estado = q.ru_estado,
                                     rub_acumula_descuento = q.rub_acumula_descuento,
                                     rub_AplicaIR = q.rub_AplicaIR,
                                     rub_GrupoResumen = q.rub_GrupoResumen,
                                     EstadoBool = q.ru_estado == "A" ? true : false

                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_rubro_tipo_Info> get_list_rub_acumula(int IdEmpresa)
        {
            try
            {
                List<ro_rubro_tipo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                   
                        Lista = (from q in Context.ro_rubro_tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.ru_estado == "A"
                                 && q.rub_acumula==true
                                 select new ro_rubro_tipo_Info
                                 {
                                     IdRubro = q.IdRubro,
                                     rub_codigo = q.rub_codigo,
                                     ru_codRolGen = q.ru_codRolGen,
                                     ru_descripcion = q.ru_descripcion,
                                     NombreCorto = q.NombreCorto,
                                     ru_tipo = q.ru_tipo,
                                     ru_orden = q.ru_orden,
                                     rub_grupo = q.rub_grupo,
                                     rub_concep = q.rub_concep,
                                     rub_nocontab = q.rub_nocontab,
                                     rub_ctacon = q.rub_ctacon,
                                     ru_estado = q.ru_estado,
                                     rub_acumula_descuento = q.rub_acumula_descuento


                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_rubro_tipo_Info> get_list_rub_concepto(int IdEmpresa)
        {
            try
            {
                List<ro_rubro_tipo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {

                    Lista = (from q in Context.ro_rubro_tipo
                             where q.IdEmpresa == IdEmpresa
                             && q.ru_estado == "A"
                             && q.rub_concep == true
                             select new ro_rubro_tipo_Info
                             {
                                 IdRubro = q.IdRubro,
                                 rub_codigo = q.rub_codigo,
                                 ru_codRolGen = q.ru_codRolGen,
                                 ru_descripcion = q.ru_descripcion,
                                 NombreCorto = q.NombreCorto,
                                 ru_tipo = q.ru_tipo,
                                 ru_orden = q.ru_orden,
                                 rub_grupo = q.rub_grupo,
                                 rub_concep = q.rub_concep,
                                 rub_nocontab = q.rub_nocontab,
                                 rub_ctacon = q.rub_ctacon,
                                 ru_estado = q.ru_estado,
                                 rub_acumula_descuento=q.rub_acumula_descuento

                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_rubro_tipo_Info get_info(int IdEmpresa, string IdRubro)
        {
            try
            {
                ro_rubro_tipo_Info info = new ro_rubro_tipo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_rubro_tipo Entity = Context.ro_rubro_tipo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdRubro == IdRubro);
                    if (Entity == null) return null;

                    info = new ro_rubro_tipo_Info
                    {
                        IdEmpresa =Entity.IdEmpresa,
                        IdRubro=Entity.IdRubro,
                        rub_codigo = Entity.rub_codigo,
                        ru_codRolGen = Entity.ru_codRolGen,
                        ru_descripcion = Entity.ru_descripcion,
                        NombreCorto = Entity.NombreCorto,
                        ru_tipo = Entity.ru_tipo,
                        ru_orden = Entity.ru_orden,
                        rub_grupo = Entity.rub_grupo,
                        rub_concep = Entity.rub_concep,
                        rub_nocontab = Entity.rub_nocontab,
                        rub_ctacon = Entity.rub_ctacon,
                        ru_estado = Entity.ru_estado,
                        rub_aplica_IESS=Entity.rub_aplica_IESS,
                        rub_acumula_descuento=Entity.rub_acumula_descuento,
                        se_distribuye = Entity.se_distribuye,
                        rub_provision = Entity.rub_provision,
                        rub_acumula = Entity.rub_acumula,
                        rub_AplicaIR =Entity.rub_AplicaIR,
                        rub_GrupoResumen=Entity.rub_GrupoResumen,
                        rub_ContPorEmpleado = Entity.rub_ContPorEmpleado,
                        rub_ContPorJornada = Entity.rub_ContPorJornada
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
                    var lst = from q in Context.vwro_rubro_tipo
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID =Convert.ToInt32( lst.Max(q => q.IdRubro)) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_rubro_tipo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_rubro_tipo Entity = new ro_rubro_tipo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdRubro = get_id(info.IdEmpresa).ToString(),
                        rub_codigo = info.rub_codigo,
                        ru_codRolGen = info.ru_codRolGen,
                        ru_descripcion = info.ru_descripcion,
                        NombreCorto = info.NombreCorto,
                        ru_tipo = info.ru_tipo,
                        ru_orden = info.ru_orden,
                        rub_grupo = info.rub_grupo == "" ? null : info.rub_grupo,
                        rub_concep = info.rub_concep,
                        rub_nocontab = info.rub_nocontab,
                        rub_ctacon = info.rub_ctacon,
                        IdUsuario = info.IdUsuario,
                        rub_acumula_descuento = info.rub_acumula_descuento,
                        ru_estado = "A",
                        Fecha_Transac = DateTime.Now,
                        se_distribuye = info.se_distribuye,
                        rub_AplicaIR = info.rub_AplicaIR,
                        rub_acumula = info.rub_acumula,
                        rub_provision = info.rub_provision,
                        rub_aplica_IESS = info.rub_aplica_IESS,                         
                        rub_GrupoResumen = info.rub_GrupoResumen == "" ? null : info.rub_GrupoResumen,
                        rub_ContPorEmpleado = info.rub_ContPorEmpleado,
                        rub_ContPorJornada = info.rub_ContPorJornada
                    };
                    Context.ro_rubro_tipo.Add(Entity);

                    var Secuencia = 1;
                    foreach (var item in info.lst_rubro_jornada)
                    {
                        ro_rubro_tipo_x_jornada Entity_Det = new ro_rubro_tipo_x_jornada
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdRubro = info.IdRubro,
                            IdRubroContabilizacion = item.IdRubroContabilizacion,
                            IdJornada = item.IdJornada,
                            Secuencia = Secuencia++

                        };
                        Context.ro_rubro_tipo_x_jornada.Add(Entity_Det);
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

        public bool modificarDB(ro_rubro_tipo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_rubro_tipo Entity = Context.ro_rubro_tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdRubro == info.IdRubro);
                    if (Entity == null)
                        return false;
                         Entity.rub_codigo = info.rub_codigo;
                    Entity.ru_codRolGen = info.ru_codRolGen;
                    Entity.ru_descripcion = info.ru_descripcion;
                    Entity.NombreCorto = info.NombreCorto;
                    Entity.ru_tipo = info.ru_tipo;
                    Entity.ru_orden = info.ru_orden;
                    Entity.rub_grupo = info.rub_grupo == "" ? null : info.rub_grupo;
                    Entity.rub_concep = info.rub_concep;
                    Entity.rub_nocontab = info.rub_nocontab;
                    Entity.rub_ctacon = info.rub_ctacon;
                    Entity.rub_aplica_IESS = info.rub_aplica_IESS;
                    Entity.rub_provision = info.rub_provision;
                    Entity.rub_acumula_descuento = info.rub_acumula_descuento;
                    Entity.se_distribuye = info.se_distribuye;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;
                    Entity.rub_AplicaIR = info.rub_AplicaIR;
                    Entity.rub_GrupoResumen = info.rub_GrupoResumen == "" ? null : info.rub_GrupoResumen;
                    Entity.rub_ContPorEmpleado = info.rub_ContPorEmpleado;
                    Entity.rub_ContPorJornada = info.rub_ContPorJornada;


                    var lst_det = Context.ro_rubro_tipo_x_jornada.Where(v => v.IdEmpresa == info.IdEmpresa && v.IdRubro == info.IdRubro);
                    Context.ro_rubro_tipo_x_jornada.RemoveRange(lst_det);

                    if (info.lst_rubro_jornada.Count() > 0)
                    {
                        foreach (var item in info.lst_rubro_jornada)
                        {
                            Context.ro_rubro_tipo_x_jornada.Add(new ro_rubro_tipo_x_jornada
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdRubro = info.IdRubro,
                                Secuencia = item.Secuencia,
                                IdJornada = item.IdJornada,
                                IdRubroContabilizacion = item.IdRubroContabilizacion
                            });
                        }
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

        public bool anularDB(ro_rubro_tipo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_rubro_tipo Entity = Context.ro_rubro_tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdRubro == info.IdRubro);
                    if (Entity == null)
                        return false;
                    Entity.ru_estado = info.ru_estado = "I";
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
    }
}
