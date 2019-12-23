using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
    public class ro_AjusteImpuestoRenta_Data
    {
        public List<ro_AjusteImpuestoRenta_Info> get_list(int IdEmpresa, int IdAnio, bool mostrar_anulados)
        {
            try
            {
                var IdAnio_ini = IdAnio;
                var IdAnio_fin = (IdAnio == 0 ? 999999 : IdAnio);
                List<ro_AjusteImpuestoRenta_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ro_AjusteImpuestoRenta
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdAnio >= IdAnio_ini
                                 && q.IdAnio<=IdAnio_fin
                                 select new ro_AjusteImpuestoRenta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdAjuste = q.IdAjuste,
                                     IdAnio = q.IdAnio,
                                     IdSucursal = q.IdSucursal,
                                     Fecha = q.Fecha,
                                     FechaCorte = q.FechaCorte,
                                     Observacion = q.Observacion,
                                     Estado = q.Estado
                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_AjusteImpuestoRenta
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdAnio >= IdAnio_ini
                                 && q.IdAnio <= IdAnio_fin
                                 && q.Estado == true
                                 select new ro_AjusteImpuestoRenta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdAjuste = q.IdAjuste,
                                     IdAnio = q.IdAnio,
                                     IdSucursal = q.IdSucursal,
                                     Fecha = q.Fecha,
                                     FechaCorte = q.FechaCorte,
                                     Observacion = q.Observacion,
                                     Estado = q.Estado
                                 }).ToList();
                }

                
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_AjusteImpuestoRenta_Info get_info(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                ro_AjusteImpuestoRenta_Info info = new ro_AjusteImpuestoRenta_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_AjusteImpuestoRenta Entity = Context.ro_AjusteImpuestoRenta.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdAjuste == IdAjuste);
                    if (Entity == null) return null;

                    info = new ro_AjusteImpuestoRenta_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAjuste = Entity.IdAjuste,
                        IdAnio = Entity.IdAnio,
                        IdSucursal = Entity.IdSucursal,
                        Fecha = Entity.Fecha,
                        FechaCorte = Entity.FechaCorte,
                        Observacion = Entity.Observacion,
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
        public decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_AjusteImpuestoRenta
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdAjuste) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool procesarDB(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Context.spRo_procesa_AjusteIR(info.IdEmpresa, info.IdAnio, info.IdAjuste, info.IdEmpleado, info.IdSucursal??0, info.IdUsuario, info.Fecha.Date, info.FechaCorte.Date, info.Observacion);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool anularDB(ro_AjusteImpuestoRenta_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_AjusteImpuestoRenta Entity = Context.ro_AjusteImpuestoRenta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAjuste == info.IdAjuste);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;

                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
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
