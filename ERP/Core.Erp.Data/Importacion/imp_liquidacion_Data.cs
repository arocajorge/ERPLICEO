using Core.Erp.Info.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Importacion
{
   public class imp_liquidacion_Data
    {

        public List<imp_liquidacion_Info> get_list(int IdEmpresa, DateTime fecha_inicio, DateTime Fecha_fin)
        {
            try
            {
                List<imp_liquidacion_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Lista = (from q in Context.vwimp_liquidacion
                             where q.IdEmpresa == IdEmpresa
                             && q.li_fecha >= fecha_inicio
                             && q.li_fecha <= Fecha_fin
                             select new imp_liquidacion_Info
                             {
                                 IdEmpresa=q.IdEmpresa,
                                 IdLiquidacion = q.IdLiquidacion,
                                 IdOrdenCompra_ext = q.IdOrdenCompra_ext,
                                 oe_observacion = q.oe_observacion,
                                 oe_fecha = q.oe_fecha,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 li_fecha = q.li_fecha,
                                 li_observacion = q.li_observacion,
                                 estado=q.estado


                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_liquidacion_Info get_info(int IdEmpresa, decimal IdLiquidacion)
        {
            try
            {
                imp_liquidacion_Info info = new imp_liquidacion_Info();
                using (Entities_importacion Context = new Entities_importacion())
                {
                    vwimp_liquidacion Entity = Context.vwimp_liquidacion.FirstOrDefault(q => q.IdLiquidacion == IdLiquidacion && q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new imp_liquidacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdLiquidacion = Entity.IdLiquidacion,
                        IdOrdenCompra_ext = Entity.IdOrdenCompra_ext,
                        oe_observacion = Entity.oe_observacion,
                        oe_fecha = Entity.oe_fecha,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        li_fecha= Entity.li_fecha,
                        li_observacion= Entity.li_observacion,
                        

                        IdEmpresa_ct = Entity.IdEmpresa,
                        IdTipoCbte_ct = Entity.IdTipoCbte_ct,
                        IdCbteCble_ct = Entity.IdCbteCble_ct,

                        IdEmpresa_inv = Entity.IdEmpresa,
                        IdSucursal_inv = Entity.IdSucursal_inv,
                        IdBodega_inv = Entity.IdBodega_inv,
                        IdNumMovi_inv = Entity.IdNumMovi_inv,

                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    var lst = from q in Context.imp_liquidacion
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdLiquidacion) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(imp_liquidacion_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_liquidacion Entity = new imp_liquidacion
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdLiquidacion = info.IdLiquidacion = get_id(info.IdEmpresa),
                        IdOrdenCompra_ext = info.IdOrdenCompra_ext,
                        li_codigo = info.li_codigo,
                        li_num_DAU = info.li_num_DAU,
                        li_fecha = info.li_fecha.Date,
                        li_observacion = info.li_observacion,
                        IdEmpresa_ct=info.IdEmpresa,
                        IdTipoCbte_ct=info.IdTipoCbte_ct,
                        IdCbteCble_ct=info.IdCbteCble_ct,

                        IdEmpresa_inv=info.IdEmpresa,
                        IdSucursal_inv=info.IdSucursal_inv,
                        IdBodega_inv=info.IdBodega_inv,
                        IdNumMovi_inv=info.IdNumMovi_inv,
                        estado = true

                        

                    };
                    Context.imp_liquidacion.Add(Entity);
                  
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception )
            {

                throw;
            }
        }

        public bool modificarDB(imp_liquidacion_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_liquidacion Entity = Context.imp_liquidacion.FirstOrDefault(q => q.IdLiquidacion == info.IdLiquidacion);
                    if (Entity == null) return false;

                   
                    Context.SaveChanges();
                   
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(imp_liquidacion_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_liquidacion Entity = Context.imp_liquidacion.FirstOrDefault(q => q.IdLiquidacion == info.IdLiquidacion);
                    if (Entity == null) return false;
                    Entity.estado = false;
                    Entity.fecha_anulacion = DateTime.Now;
                    Entity.IdUsuario_anulacion = info.IdUsuario_anulacion;
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
