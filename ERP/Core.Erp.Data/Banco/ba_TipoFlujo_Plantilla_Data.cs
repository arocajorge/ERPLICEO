using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_TipoFlujo_Plantilla_Data
    {
        public List<ba_TipoFlujo_Plantilla_Info> get_list(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<ba_TipoFlujo_Plantilla_Info> Lista;

                using (Entities_banco db = new Entities_banco())
                {
                    if (MostrarAnulados == false)
                    {
                        Lista = db.ba_TipoFlujo_Plantilla.Where(q => q.Estado == true && q.IdEmpresa == IdEmpresa).Select(q => new ba_TipoFlujo_Plantilla_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdPlantilla = q.IdPlantilla,
                            Descripcion = q.Descripcion,                            
                            Estado = q.Estado
                        }).ToList();
                    }
                    else
                    {
                        Lista = db.ba_TipoFlujo_Plantilla.Where(q => q.IdEmpresa == IdEmpresa).Select(q => new ba_TipoFlujo_Plantilla_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdPlantilla = q.IdPlantilla,
                            Descripcion = q.Descripcion,
                            Estado = q.Estado
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
                using (Entities_banco db = new Entities_banco())
                {
                    var Lista = db.ba_TipoFlujo_Plantilla.Where(q => q.IdEmpresa == IdEmpresa).Select(q => q.IdPlantilla);

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

        public ba_TipoFlujo_Plantilla_Info get_info(int IdEmpresa, decimal IdPlantilla)
        {
            try
            {
                ba_TipoFlujo_Plantilla_Info info = new ba_TipoFlujo_Plantilla_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_TipoFlujo_Plantilla Entity = Context.ba_TipoFlujo_Plantilla.Where(q => q.IdPlantilla == IdPlantilla && q.IdEmpresa == IdEmpresa).FirstOrDefault();

                    if (Entity == null) return null;
                    info = new ba_TipoFlujo_Plantilla_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPlantilla = Entity.IdPlantilla,
                        Descripcion = Entity.Descripcion,
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

        public bool GuardarBD(ba_TipoFlujo_Plantilla_Info info)
        {
            try
            {
                using (Entities_banco db = new Entities_banco())
                {
                    db.ba_TipoFlujo_Plantilla.Add(new ba_TipoFlujo_Plantilla
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPlantilla = info.IdPlantilla = get_id(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    });

                    //detalle
                    if (info.Lista_TipoFlujo_PlantillaDet != null)
                    {
                        int Secuencia = 1;
                        foreach (var item in info.Lista_TipoFlujo_PlantillaDet)
                        {
                            db.ba_TipoFlujo_PlantillaDet.Add(new ba_TipoFlujo_PlantillaDet
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPlantilla = info.IdPlantilla,
                                Secuencia = Secuencia++,
                                IdTipoFlujo = item.IdTipoFlujo,
                                Porcentaje = item.Porcentaje                                
                            });

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

        public bool ModificarBD(ba_TipoFlujo_Plantilla_Info info)
        {
            try
            {
                using (Entities_banco db = new Entities_banco())
                {
                    ba_TipoFlujo_Plantilla entity = db.ba_TipoFlujo_Plantilla.Where(q => q.IdPlantilla == info.IdPlantilla && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Descripcion = info.Descripcion;
                    entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    entity.FechaModificacion = DateTime.Now;

                    var lst_det = db.ba_TipoFlujo_PlantillaDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdPlantilla == info.IdPlantilla).ToList();
                    db.ba_TipoFlujo_PlantillaDet.RemoveRange(lst_det);

                    if (info.Lista_TipoFlujo_PlantillaDet != null)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.Lista_TipoFlujo_PlantillaDet)
                        {
                            db.ba_TipoFlujo_PlantillaDet.Add(new ba_TipoFlujo_PlantillaDet
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPlantilla = info.IdPlantilla,
                                Secuencia = Secuencia++,
                                IdTipoFlujo = item.IdTipoFlujo,
                                Porcentaje = item.Porcentaje
                            });
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

        public bool AnularBD(ba_TipoFlujo_Plantilla_Info info)
        {
            try
            {
                using (Entities_banco db = new Entities_banco())
                {
                    ba_TipoFlujo_Plantilla entity = db.ba_TipoFlujo_Plantilla.Where(q => q.IdPlantilla == info.IdPlantilla && q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                    if (entity == null)
                    {
                        return false;
                    }

                    entity.Estado = false;
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
