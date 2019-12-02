using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
    public class ro_EmpleadoAnticipoBeneficio_Data
    {
        public List<ro_EmpleadoAnticipoBeneficio_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;
                List<ro_EmpleadoAnticipoBeneficio_Info> Lista = new List<ro_EmpleadoAnticipoBeneficio_Info>();

                using (Entities_rrhh db = new Entities_rrhh())
                {
                    var lst = db.vwro_EmpleadoAnticipoBeneficio.Where(q => q.IdEmpresa == IdEmpresa && FechaIni <= q.FechaCreacion && q.FechaCreacion <= FechaFin).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new ro_EmpleadoAnticipoBeneficio_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdAnticipo = item.IdAnticipo,
                            FechaDesde = item.FechaDesde,
                            FechaHasta = item.FechaHasta,
                            Valor = item.Valor,
                            FechaCreacion = item.FechaCreacion,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            ru_descripcion = item.ru_descripcion,
                            Estado = item.Estado
                        });
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_EmpleadoAnticipoBeneficio_Info GetInfo(int IdEmpresa, decimal IdAnticipo)
        {
            try
            {
                ro_EmpleadoAnticipoBeneficio_Info info = new ro_EmpleadoAnticipoBeneficio_Info();

                using (Entities_rrhh db = new Entities_rrhh())
                {
                    var entity = db.ro_EmpleadoAnticipoBeneficio.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnticipo == IdAnticipo).FirstOrDefault();
                    if (entity == null)
                        return null;

                    info = new ro_EmpleadoAnticipoBeneficio_Info
                    {
                        IdEmpresa = entity.IdEmpresa,
                        IdAnticipo = entity.IdAnticipo,
                        IdEmpleado = entity.IdEmpleado,
                        FechaDesde = entity.FechaDesde,
                        FechaHasta = entity.FechaHasta,
                        IdRubro = entity.IdRubro,
                        Valor = entity.Valor
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal GetId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (Entities_rrhh db =new Entities_rrhh())
                {
                    int Cont = db.ro_EmpleadoAnticipoBeneficio.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (Cont > 0)
                        ID = db.ro_EmpleadoAnticipoBeneficio.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdAnticipo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(ro_EmpleadoAnticipoBeneficio_Info info)
        {
            try
            {
                using (Entities_rrhh db = new Entities_rrhh())
                {
                    db.ro_EmpleadoAnticipoBeneficio.Add(new ro_EmpleadoAnticipoBeneficio
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnticipo = info.IdAnticipo = GetId(info.IdEmpresa),
                        IdEmpleado = info.IdEmpleado,
                        FechaDesde = info.FechaDesde,
                        FechaHasta = info.FechaHasta,
                        IdRubro = info.IdRubro,
                        Valor = info.Valor,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now,
                        Estado = true
                    });
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModificarDB(ro_EmpleadoAnticipoBeneficio_Info info)
        {
            try
            {
                using (Entities_rrhh db = new Entities_rrhh())
                {
                    var entity = db.ro_EmpleadoAnticipoBeneficio.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnticipo == info.IdAnticipo).FirstOrDefault();
                    if (entity == null)
                        return false;

                    entity.IdEmpleado = info.IdEmpleado;
                    entity.Valor = info.Valor;
                    entity.FechaDesde = info.FechaDesde;
                    entity.FechaHasta = info.FechaHasta;
                    entity.IdUsuarioModificacion = info.IdUsuarioCreacion;
                    entity.FechaModificacion = DateTime.Now;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AnularDB(ro_EmpleadoAnticipoBeneficio_Info info)
        {
            try
            {
                using (Entities_rrhh db = new Entities_rrhh())
                {
                    var entity = db.ro_EmpleadoAnticipoBeneficio.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnticipo == info.IdAnticipo).FirstOrDefault();
                    if (entity == null)
                        return false;

                    entity.Estado = false;
                    entity.IdUsuarioAnulacion = info.IdUsuarioCreacion;
                    entity.FechaAnulacion = DateTime.Now;
                    entity.MotivoAnulacion = info.MotivoAnulacion;

                    db.SaveChanges();
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
