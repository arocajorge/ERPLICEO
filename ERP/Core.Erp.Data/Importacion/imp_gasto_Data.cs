using Core.Erp.Info.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Importacion
{
    public class imp_gasto_Data
    {
        public List<imp_gasto_Info> get_list()
        {
            try
            {
                List<imp_gasto_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Lista = (from q in Context.imp_gasto
                             select new imp_gasto_Info
                             {
                                 IdGasto_tipo = q.IdGasto_tipo,
                                 gt_descripcion = q.gt_descripcion,
                                 observacion = q.observacion,
                                 gt_orden=q.gt_orden,
                                 estado = q.estado==true
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_gasto_Info get_info(int IdGasto_tipo)
        {
            try
            {
                imp_gasto_Info info = new imp_gasto_Info();
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_gasto Entity = Context.imp_gasto.FirstOrDefault(q => q.IdGasto_tipo == IdGasto_tipo);
                    if (Entity == null) return null;
                        info = new imp_gasto_Info
                        {
                            IdGasto_tipo = Entity.IdGasto_tipo,
                            gt_descripcion = Entity.gt_descripcion,
                            observacion = Entity.observacion,
                            gt_orden=Entity.gt_orden,
                            estado = Entity.estado
                        };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int get_id()
        {
            try
            {
                int ID = 1;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    var lst = from q in Context.imp_gasto
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdGasto_tipo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(imp_gasto_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_gasto Entity = new imp_gasto
                    {
                        IdGasto_tipo = info.IdGasto_tipo=get_id(),
                        gt_descripcion = info.gt_descripcion,
                        observacion = info.observacion,
                        gt_orden=info.gt_orden,
                        estado = info.estado = true,                        
                    };
                    Context.imp_gasto.Add(Entity);
                    
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(imp_gasto_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    //TRes el info del gasto - OK
                    imp_gasto Entity = Context.imp_gasto.FirstOrDefault(q => q.IdGasto_tipo == info.IdGasto_tipo);
                    if (Entity == null) return false;
                    //Modificar los campos si encuentra un gasto - OK
                    Entity.gt_descripcion = info.gt_descripcion;
                    Entity.observacion = info.observacion;
                    Entity.gt_orden = info.gt_orden;
                    //aqui tienes que hacer la parte del detalle
                    //Primero busco si este gasto tiene cuenta en esta empresa
                    var Entity_cta = Context.imp_gasto_x_ct_plancta.Where(q => q.IdEmpresa == info.info_gasto_cta.IdEmpresa && q.IdGasto_tipo == info.IdGasto_tipo).FirstOrDefault();
                    if (Entity_cta == null)
                    {
                        //Si no tiene creo uno nuevo
                        Context.imp_gasto_x_ct_plancta.Add(new imp_gasto_x_ct_plancta
                        {
                            IdEmpresa = info.info_gasto_cta.IdEmpresa,
                            IdCtaCble = info.IdCtaCble,
                            IdGasto_tipo = info.IdGasto_tipo
                        });
                    }else
                    {
                        //Caso contrario le actualizo la cuenta contable
                        Entity_cta.IdCtaCble = info.IdCtaCble;
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
        public bool anularDB(imp_gasto_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_gasto Entity = Context.imp_gasto.FirstOrDefault(q => q.IdGasto_tipo == info.IdGasto_tipo);
                    if (Entity == null) return false;

                    Entity.estado = info.estado = false;

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
