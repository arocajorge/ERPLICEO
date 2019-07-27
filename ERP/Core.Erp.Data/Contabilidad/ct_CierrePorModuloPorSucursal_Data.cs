using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_CierrePorModuloPorSucursal_Data
    {
        public List<ct_CierrePorModuloPorSucursal_Info> GetList(int IdEmpresa, int IdSucursal, bool MostrarCerrado)
        {
            try
            {
                List<ct_CierrePorModuloPorSucursal_Info> Lista = new List<ct_CierrePorModuloPorSucursal_Info>();
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 99999 : IdSucursal;
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    if (MostrarCerrado == false)
                    {
                        Lista = db.vwct_CierrePorModuloPorSucursal.Where(q => q.Cerrado == false && q.IdEmpresa == IdEmpresa && q.IdSucursal >= IdSucursalIni
                                     && q.IdSucursal <= IdSucursalFin).Select(q => new ct_CierrePorModuloPorSucursal_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdCierre = q.IdCierre,
                            CodModulo = q.CodModulo,
                            FechaIni = q.FechaIni,
                            FechaFin = q.FechaFin,
                            Cerrado = q.Cerrado,
                            Su_Descripcion = q.Su_Descripcion,
                            Descripcion = q.Descripcion
                        }).ToList();
                    }
                    else
                    {
                        Lista = db.vwct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal >= IdSucursalIni
                                     && q.IdSucursal <= IdSucursalFin).Select(q => new ct_CierrePorModuloPorSucursal_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdCierre = q.IdCierre,
                            CodModulo = q.CodModulo,
                            FechaIni = q.FechaIni,
                            FechaFin = q.FechaFin,
                            Cerrado = q.Cerrado,
                            Su_Descripcion = q.Su_Descripcion,
                            Descripcion = q.Descripcion
                        }).ToList();
                    }
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ct_CierrePorModuloPorSucursal_Info GetInfo(int IdEmpresa, int IdCierre)
        {
            try
            {
                ct_CierrePorModuloPorSucursal_Info info = new ct_CierrePorModuloPorSucursal_Info();

                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_CierrePorModuloPorSucursal Entity = Context.ct_CierrePorModuloPorSucursal.Where(q => q.IdCierre == IdCierre && q.IdEmpresa == IdEmpresa).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new ct_CierrePorModuloPorSucursal_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdCierre = Entity.IdCierre,
                        CodModulo = Entity.CodModulo,
                        FechaIni = Entity.FechaIni,
                        FechaFin = Entity.FechaFin,
                        Cerrado = Entity.Cerrado
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
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    var Lista = db.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdCierre);

                    if (Lista.Count() > 0)
                        ID = Lista.Max() + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarBD(ct_CierrePorModuloPorSucursal_Info info)
        {
            try
            {
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    db.ct_CierrePorModuloPorSucursal.Add(new ct_CierrePorModuloPorSucursal
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdCierre = info.IdCierre = get_id(info.IdEmpresa),
                        CodModulo = info.CodModulo,
                        FechaIni = info.FechaIni,
                        FechaFin = info.FechaFin,
                        Cerrado = info.Cerrado
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

        public bool ModificarBD(ct_CierrePorModuloPorSucursal_Info info)
        {
            try
            {
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    ct_CierrePorModuloPorSucursal entity = db.ct_CierrePorModuloPorSucursal.Where(q => q.IdCierre == info.IdCierre && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.IdEmpresa = info.IdEmpresa;
                    entity.IdSucursal = info.IdSucursal;
                    entity.IdCierre = info.IdCierre;
                    entity.CodModulo = info.CodModulo;
                    entity.FechaIni = info.FechaIni;
                    entity.FechaFin = info.FechaFin;
                    entity.Cerrado = info.Cerrado;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public bool AnularDB(ct_CierrePorModuloPorSucursal_Info info)
        {
            try
            {
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    var Entity = Context.ct_CierrePorModuloPorSucursal.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCierre == info.IdCierre).FirstOrDefault();
                    if (Entity != null)
                        Context.ct_CierrePorModuloPorSucursal.Remove(Entity);
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
