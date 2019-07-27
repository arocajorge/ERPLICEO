using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
  public  class ro_Config_Param_contable_Data
    {
        public List< ro_Config_Param_contable_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<ro_Config_Param_contable_Info> Lista = new List<ro_Config_Param_contable_Info>();
                int secuencia = 1;
                using (Entities_rrhh Context = new Entities_rrhh())
                {

                    string sql = " select IdEmpresa,IdDivision,IdArea,IdDepartamento,IdRubro,IdCtaCble,IdCentroCosto,DebCre,IdCtaCble_Haber IdCtaCble_prov_credito,0 as Secuencia,rub_nocontab,rub_provision,DescripcionDiv,descripcionArea,de_descripcion,ru_descripcion,pc_Cuenta_prov_debito,pc_Cuenta, ru_tipo from vwRo_Division_Area_dep_rubro a where IdEmpresa=" + IdEmpresa+" ";
                    sql += "and exists(select e.IdEmpresa from vwro_rol_detalle as e where e.IdEmpresa = "+IdEmpresa+" and a.IdEmpresa = e.IdEmpresa and a.IdDivision = e.IdDivision AND a.IdArea = E.IdArea AND a.IdDepartamento = E.IdDepartamento and a.IdRubro = e.IdRubro)";
                    var result = Context.Database.SqlQuery<ro_Config_Param_contable_Info>(sql).ToList();
                    Lista = result;
                    Lista.ForEach(v =>
                    {
                        v.Secuencia = secuencia++;
                        if (v.IdCtaCble == null | v.IdCtaCble == "")
                            v.IdCtaCble = v.rub_ctacon;
                        v.IdCtaCble_prov_debito = v.IdCtaCble;
                        
                        v.pc_Cuenta_prov_credito = v.pc_Cuenta;
                    });

                }


                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_Config_Param_contable_Info> get_list(int IdEmpresa, string es_provision)
        {
            try
            {
                List<ro_Config_Param_contable_Info> Lista = new List<ro_Config_Param_contable_Info>();
                int secuencia = 1;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                      
                    string sql = " select IdEmpresa,IdRubro,IdCtaCble,IdCtaCble_Haber, ru_descripcion,ru_tipo,IdDepartamento, IdArea, IdDivision,DescripcionArea,de_descripcion,rub_ContPorEmpleado,pc_Cuenta_prov_debito,pc_Cuenta " +
                        "from vwRo_Division_Area_dep_rubro where IdEmpresa='" + IdEmpresa + "' and rub_provision='" + es_provision + "' and rub_nocontab='" + 1 + "' group by IdEmpresa,IdRubro,IdCtaCble, ru_descripcion,IdCtaCble_Haber,ru_tipo ,IdDepartamento, IdArea, IdDivision,DescripcionArea,de_descripcion,rub_ContPorEmpleado,pc_Cuenta_prov_debito,pc_Cuenta";
                    var result = Context.Database.SqlQuery<ro_Config_Param_contable_Info>(sql).ToList();
                    Lista = result;
                    Lista.ForEach(v =>
                    {
                        v.Secuencia = secuencia++;
                        if (v.IdCtaCble == null | v.IdCtaCble == "")
                            v.IdCtaCble = v.rub_ctacon;
                        v.IdCtaCble_prov_debito = v.IdCtaCble;

                        v.pc_Cuenta_prov_credito = v.pc_Cuenta;
                    });

                }


                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_Config_Param_contable_Info get_info(int IdEmpresa, int IdDivision, int IdArea, int IdDepartamento, string IdRubro)
        {
            try
            {
                 ro_Config_Param_contable_Info info = new  ro_Config_Param_contable_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                     ro_Config_Param_contable Entity = Context. ro_Config_Param_contable.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdDivision == IdDivision && q.IdArea == IdArea && q.IdDepartamento == IdDepartamento&& q.IdRubro==IdRubro);
                    if (Entity == null) return null;

                    info = new  ro_Config_Param_contable_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdDivision = Entity.IdDivision,
                        IdArea = Entity.IdArea,
                        IdDepartamento = Entity.IdDepartamento,
                        IdRubro = Entity.IdRubro,
                        IdCentroCosto = Entity.IdCentroCosto,
                        DebCre = Entity.DebCre,
                        IdCtaCble = Entity.IdCtaCble,
                        IdCtaCble_prov_credito = Entity.IdCtaCble_Haber
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(List< ro_Config_Param_contable_Info> info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    foreach (var item in info)
                    {
                       
                        if (item.IdCtaCble == "")
                            item.IdCtaCble = null;
                        if (item.IdCtaCble_prov_debito == "")
                            item.IdCtaCble_prov_debito = null;
                        if (item.rub_provision == false)
                        {
                            ro_Config_Param_contable Entity = new ro_Config_Param_contable
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdDivision = item.IdDivision,
                                IdArea = item.IdArea,
                                IdDepartamento = item.IdDepartamento,
                                IdRubro = item.IdRubro,
                                IdCentroCosto = item.IdCentroCosto,
                                DebCre = item.DebCre,
                                IdCtaCble_Haber = item.IdCtaCble
                            };
                            Context.ro_Config_Param_contable.Add(Entity);
                        }
                        else
                        {
                            ro_Config_Param_contable Entity_ = new ro_Config_Param_contable
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdDivision = item.IdDivision,
                                IdArea = item.IdArea,
                                IdDepartamento = item.IdDepartamento,
                                IdRubro = item.IdRubro,
                                IdCentroCosto = item.IdCentroCosto,
                                DebCre = item.DebCre,
                                IdCtaCble = item.IdCtaCble_prov_debito,
                                IdCtaCble_Haber=item.IdCtaCble_prov_credito
                            };
                            Context.ro_Config_Param_contable.Add(Entity_);
                        }
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

        public bool ModificarDB(ro_Config_Param_contable_Info info)
        {
            try
            {
                using (Entities_rrhh db = new Entities_rrhh())
                {
                    if (info.IdCtaCble == "")
                        info.IdCtaCble = null;
                    if (info.IdCtaCble_prov_debito == "")
                        info.IdCtaCble_prov_debito = null;

                    if (info.IdEmpresa == 0)
                        return false;

                    var entity = db.ro_Config_Param_contable.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdDivision == info.IdDivision && q.IdArea == info.IdArea && q.IdDepartamento == info.IdDepartamento && q.IdRubro == info.IdRubro).FirstOrDefault();
                    if(entity != null)
                    {
                        entity.IdCtaCble = info.rub_provision == true ? info.IdCtaCble_prov_debito : null;
                        entity.IdCtaCble_Haber = info.rub_provision == true ? info.IdCtaCble_prov_credito : info.IdCtaCble;
                    }else
                    {
                        entity = new ro_Config_Param_contable
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdDivision = info.IdDivision,
                            IdArea = info.IdArea,
                            IdDepartamento = info.IdDepartamento,
                            IdRubro = info.IdRubro,
                            IdCentroCosto = info.IdCentroCosto,
                            DebCre = info.DebCre,

                            IdCtaCble = info.rub_provision == true ? info.IdCtaCble_prov_debito : null,
                            IdCtaCble_Haber = info.rub_provision == true ? info.IdCtaCble_prov_credito : info.IdCtaCble
                        };
                        db.ro_Config_Param_contable.Add(entity);
                    }
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    string sql = "delete  ro_Config_Param_contable where IdEmpresa='" + IdEmpresa + "'";
                    Context.Database.ExecuteSqlCommand(sql);
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
