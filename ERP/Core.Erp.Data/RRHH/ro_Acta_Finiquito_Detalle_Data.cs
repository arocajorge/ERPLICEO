using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_Acta_Finiquito_Detalle_Data
    {

        public List<ro_Acta_Finiquito_Detalle_Info> get_list(int IdEmpresa, decimal IdActaFiniquito)
        {
            try
            {
                List<ro_Acta_Finiquito_Detalle_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.ro_Acta_Finiquito_Detalle
                             join p in Context.ro_rubro_tipo
                             on new {q.IdEmpresa,q.IdRubro}equals new {p.IdEmpresa, p.IdRubro}
                              
                             where q.IdEmpresa == IdEmpresa
                                   && q.IdActaFiniquito == IdActaFiniquito
                             select new ro_Acta_Finiquito_Detalle_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdActaFiniquito = q.IdActaFiniquito,
                                 IdSecuencia = q.IdSecuencia,
                                 IdRubro = q.IdRubro,
                                 Observacion = q.Observacion,
                                 Valor = q.Valor,
                                 ru_tipo=p.ru_tipo
                                 
                             }).ToList();

                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_Acta_Finiquito_Detalle_Info get_info(int IdEmpresa, decimal IdEmpleado, decimal IdActaFiniquito, int Secuencia)
        {
            try
            {
                ro_Acta_Finiquito_Detalle_Info info = new ro_Acta_Finiquito_Detalle_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Acta_Finiquito_Detalle Entity = Context.ro_Acta_Finiquito_Detalle.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdActaFiniquito == IdActaFiniquito && q.IdSecuencia == Secuencia);
                    if (Entity == null) return null;

                    info = new ro_Acta_Finiquito_Detalle_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdActaFiniquito = Entity.IdActaFiniquito,
                        Valor = Entity.Valor,
                        Observacion = Entity.Observacion,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(List<ro_Acta_Finiquito_Detalle_Info> info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    foreach (var item in info)
                    {
                        ro_Acta_Finiquito_Detalle Entity = new ro_Acta_Finiquito_Detalle
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdActaFiniquito = item.IdActaFiniquito,
                            IdSecuencia = item.IdSecuencia,
                            IdRubro = item.IdRubro,
                            Valor = item.Valor,
                            Observacion = item.Observacion,
                        };
                        Context.ro_Acta_Finiquito_Detalle.Add(Entity);
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
        public bool eliminarDB(ro_Acta_Finiquito_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    string sql = "delete ro_Acta_Finiquito_Detalle where IdEmpresa='" + info.IdEmpresa + "' and IdActaFiniquito='" + info.IdActaFiniquito + "'";
                    Context.Database.ExecuteSqlCommand(sql);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AnularD(ro_Acta_Finiquito_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    string sql = "update ro_Acta_Finiquito_Detalle set Estado='I' where IdEmpresa='" + info.IdEmpresa + "' and IdEmpleado='" + info.IdEmpleado + "' and IdActaFiniquito='" + info.IdActaFiniquito + "'";
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
