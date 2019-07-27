using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Contabilidad
{
    public class ct_cbtecble_Plantilla_Data
    {
        public List<ct_cbtecble_Plantilla_Info> get_list(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<ct_cbtecble_Plantilla_Info> Lista;

                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    if (MostrarAnulados == false)
                    {
                        Lista = db.ct_cbtecble_Plantilla.Where(q => q.cb_Estado == "A" && q.IdEmpresa == IdEmpresa).Select(q => new ct_cbtecble_Plantilla_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdPlantilla = q.IdPlantilla,
                            IdTipoCbte = q.IdEmpresa,
                            cb_Estado = q.cb_Estado,
                            EstadoBool = q.cb_Estado == "A" ? true : false,
                            cb_Observacion = q.cb_Observacion
                        }).ToList();
                    }
                    else
                    {
                        Lista = db.ct_cbtecble_Plantilla.Where(q => q.IdEmpresa == IdEmpresa).Select(q => new ct_cbtecble_Plantilla_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdPlantilla = q.IdPlantilla,
                            IdTipoCbte = q.IdEmpresa,
                            cb_Estado = q.cb_Estado,
                            EstadoBool = q.cb_Estado == "A" ? true : false,
                            cb_Observacion = q.cb_Observacion
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

        public int get_id(int IdEmpresa)
        {

            try
            {
                decimal ID = 1;
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    var Lista = db.ct_cbtecble_Plantilla.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdPlantilla);

                    if (Lista.Count() > 0)
                        ID = Lista.Max() + 1;
                }
                return Convert.ToInt32(ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ct_cbtecble_Plantilla_Info get_info(int IdEmpresa, decimal IdPlantilla)
        {
            try
            {
                ct_cbtecble_Plantilla_Info info = new ct_cbtecble_Plantilla_Info();
                using (Entities_contabilidad Context = new Entities_contabilidad())
                {
                    ct_cbtecble_Plantilla Entity = Context.ct_cbtecble_Plantilla.Where(q => q.IdPlantilla == IdPlantilla && q.IdEmpresa == IdEmpresa).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new ct_cbtecble_Plantilla_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPlantilla = Entity.IdPlantilla,
                        IdTipoCbte = Entity.IdTipoCbte,
                        cb_Observacion = Entity.cb_Observacion,
                        cb_Estado = Entity.cb_Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarBD(ct_cbtecble_Plantilla_Info info)
        {
            try
            {
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    db.ct_cbtecble_Plantilla.Add(new ct_cbtecble_Plantilla
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPlantilla = info.IdPlantilla = get_id(info.IdEmpresa),
                        IdTipoCbte = info.IdTipoCbte,
                        cb_Observacion = info.cb_Observacion,
                        cb_Estado = info.cb_Estado,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    });

                    //detalle
                    if (info.lst_cbtecble_plantilla_det != null)
                    {
                        int secuencia = 1;
                        foreach (var item in info.lst_cbtecble_plantilla_det)
                        {
                            db.ct_cbtecble_Plantilla_det.Add(new ct_cbtecble_Plantilla_det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPlantilla = info.IdPlantilla,
                                secuencia = secuencia,
                                IdCtaCble = item.IdCtaCble,
                                dc_Valor = item.dc_Valor,
                                dc_Observacion = item.dc_Observacion,
                                IdPunto_cargo_grupo = item.IdPunto_cargo_grupo,
                                IdPunto_cargo = item.IdPunto_cargo,
                                IdCentroCosto = item.IdCentroCosto
                            });
                            secuencia++;
                        }
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

        public bool ModificarBD(ct_cbtecble_Plantilla_Info info)
        {
            try
            {
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    ct_cbtecble_Plantilla entity = db.ct_cbtecble_Plantilla.Where(q => q.IdPlantilla == info.IdPlantilla && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.IdTipoCbte = info.IdTipoCbte;
                    entity.cb_Observacion = info.cb_Observacion;
                    entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    entity.FechaModificacion = DateTime.Now;

                    var lst_det = db.ct_cbtecble_Plantilla_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdPlantilla == info.IdPlantilla).ToList();
                    db.ct_cbtecble_Plantilla_det.RemoveRange(lst_det);

                    if (info.lst_cbtecble_plantilla_det != null)
                    {
                        int secuencia = 1;

                        foreach (var item in info.lst_cbtecble_plantilla_det)
                        {
                            db.ct_cbtecble_Plantilla_det.Add(new ct_cbtecble_Plantilla_det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPlantilla = info.IdPlantilla,
                                secuencia = secuencia,
                                IdCtaCble = item.IdCtaCble,
                                dc_Valor = item.dc_Valor,
                                dc_Observacion = item.dc_Observacion,
                                IdPunto_cargo_grupo = item.IdPunto_cargo_grupo,
                                IdPunto_cargo = item.IdPunto_cargo,
                                IdCentroCosto = item.IdCentroCosto
                            });
                            secuencia++;
                        }
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

        public bool AnularBD(ct_cbtecble_Plantilla_Info info)
        {
            try
            {
                using (Entities_contabilidad db = new Entities_contabilidad())
                {
                    ct_cbtecble_Plantilla entity = db.ct_cbtecble_Plantilla.Where(q => q.IdPlantilla == info.IdPlantilla && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.cb_Estado = "I";
                    entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
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
