using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Inventario
{
    public class in_Ing_Egr_Inven_det_Data
    {
        public List<in_Ing_Egr_Inven_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)
        {
            try
            {
                List<in_Ing_Egr_Inven_det_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from d in Context.in_Ing_Egr_Inven_det
                             join p in Context.in_Producto
                             on new { d.IdEmpresa, d.IdProducto } equals new { p.IdEmpresa, p.IdProducto }
                             join c in Context.in_ProductoTipo
                             on new { p.IdEmpresa, p.IdProductoTipo } equals new { c.IdEmpresa, c.IdProductoTipo }
                             where d.IdEmpresa == IdEmpresa && d.IdSucursal == IdSucursal
                             && d.IdMovi_inven_tipo == IdMovi_inven_tipo && d.IdNumMovi == IdNumMovi
                             select new in_Ing_Egr_Inven_det_Info
                             {
                                 IdEmpresa = d.IdEmpresa,
                                 IdSucursal = d.IdSucursal,
                                 IdMovi_inven_tipo = d.IdMovi_inven_tipo,
                                 IdNumMovi = d.IdNumMovi,
                                 IdBodega = d.IdBodega,
                                 dm_cantidad = d.dm_cantidad,
                                 dm_observacion = d.dm_observacion,
                                 IdMotivo_Inv = d.IdMotivo_Inv,
                                 IdOrdenCompra = d.IdOrdenCompra,
                                 IdProducto = d.IdProducto,
                                 IdUnidadMedida = d.IdUnidadMedida,
                                 mv_costo = d.mv_costo,
                                 Secuencia = d.Secuencia,

                                 IdSucursal_inv = d.IdSucursal_inv,
                                 IdSucursal_oc = d.IdSucursal_oc,
                                 IdEmpresa_inv = d.IdEmpresa_inv,
                                 IdEmpresa_oc = d.IdEmpresa_oc,
                                 IdBodega_inv = d.IdBodega_inv,
                                 IdMovi_inven_tipo_inv = d.IdMovi_inven_tipo_inv,
                                 IdNumMovi_inv = d.IdNumMovi_inv,
                                 dm_cantidad_sinConversion = d.dm_cantidad_sinConversion,
                                 IdUnidadMedida_sinConversion = d.IdUnidadMedida_sinConversion,
                                 mv_costo_sinConversion = d.mv_costo_sinConversion ?? 0,
                                 secuencia_inv = d.secuencia_inv,
                                 Secuencia_oc = d.Secuencia_oc,
                                 pr_descripcion = p.pr_descripcion,
                                 CantidadAnterior = d.dm_cantidad_sinConversion,
                                 lote_fecha_vcto = p.lote_fecha_vcto,
                                 lote_num_lote = p.lote_num_lote,
                                 se_distribuye = p.se_distribuye ?? false,
                                 tp_ManejaInven = c.tp_ManejaInven,                                 
                             }).ToList();
                }
                Lista.ForEach(V =>
                {
                    V.pr_descripcion = V.pr_descripcion;
                    V.dm_cantidad_sinConversion = Math.Abs(V.dm_cantidad_sinConversion);
                    V.dm_cantidad = Math.Abs(V.dm_cantidad);
                    V.CantidadAnterior = Math.Abs(V.CantidadAnterior);
                });
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public in_Ing_Egr_Inven_det_Info get_info(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)
        {
            try
            {
                in_Ing_Egr_Inven_det_Info info = new in_Ing_Egr_Inven_det_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Ing_Egr_Inven_det Entity = Context.in_Ing_Egr_Inven_det.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdMovi_inven_tipo == IdMovi_inven_tipo && q.IdNumMovi == IdNumMovi);
                    if (Entity == null) return null;
                    info = new in_Ing_Egr_Inven_det_Info
                    {

                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdMovi_inven_tipo = Entity.IdMovi_inven_tipo,
                        IdNumMovi = Entity.IdNumMovi,
                        IdBodega = Entity.IdBodega,
                        dm_cantidad = Entity.dm_cantidad,
                        dm_observacion = Entity.dm_observacion,
                        IdMotivo_Inv = Entity.IdMotivo_Inv,
                        IdOrdenCompra = Entity.IdOrdenCompra,
                        IdProducto = Entity.IdProducto,
                        IdUnidadMedida = Entity.IdUnidadMedida,
                        mv_costo = Entity.mv_costo,
                        Secuencia = Entity.Secuencia,

                        IdSucursal_inv = Entity.IdSucursal_inv,
                        IdSucursal_oc = Entity.IdSucursal_oc,
                        IdEmpresa_inv = Entity.IdEmpresa_inv,
                        IdEmpresa_oc = Entity.IdEmpresa_oc,
                        IdBodega_inv = Entity.IdBodega_inv,
                        IdMovi_inven_tipo_inv =Entity.IdMovi_inven_tipo_inv,
                        IdNumMovi_inv = Entity.IdNumMovi_inv,
                        dm_cantidad_sinConversion =Entity.dm_cantidad_sinConversion,
                        IdUnidadMedida_sinConversion = Entity.IdUnidadMedida_sinConversion,
                        mv_costo_sinConversion = Entity.mv_costo_sinConversion ?? 0,
                        secuencia_inv = Entity.secuencia_inv,
                        Secuencia_oc = Entity.Secuencia_oc
                        
                    };
                }
                    return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_Ing_Egr_Inven_det_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Ing_Egr_Inven_det Entity = new in_Ing_Egr_Inven_det
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdMovi_inven_tipo = info.IdMovi_inven_tipo,
                        IdNumMovi = info.IdNumMovi,
                        Secuencia = info.Secuencia,
                        IdBodega = info.IdBodega,
                        IdProducto = info.IdProducto,                        

                        dm_observacion = info.dm_observacion,
                        IdMotivo_Inv = info.IdMotivo_Inv,

                        IdEmpresa_oc = info.IdEmpresa_oc,
                        IdSucursal_oc = info.IdSucursal_oc,
                        IdOrdenCompra = info.IdOrdenCompra,
                        Secuencia_oc = info.Secuencia_oc,

                        IdEmpresa_inv = info.IdEmpresa_inv,
                        IdSucursal_inv = info.IdSucursal_inv,
                        IdBodega_inv = info.IdBodega_inv,
                        IdMovi_inven_tipo_inv = info.IdMovi_inven_tipo_inv,
                        IdNumMovi_inv = info.IdNumMovi_inv,
                        secuencia_inv = info.secuencia_inv,

                        dm_cantidad_sinConversion = info.dm_cantidad_sinConversion,
                        dm_cantidad = info.dm_cantidad = info.dm_cantidad_sinConversion,//
                        IdUnidadMedida_sinConversion = info.IdUnidadMedida_sinConversion,
                        IdUnidadMedida = info.IdUnidadMedida = info.IdUnidadMedida_sinConversion,//
                        mv_costo_sinConversion = info.mv_costo_sinConversion,
                        mv_costo = info.mv_costo =(double) info.mv_costo_sinConversion//,
                    };
                    Context.in_Ing_Egr_Inven_det.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)
        {
            try
            {
                string sql = "Delete in_Ing_Egr_Inven_det where IdEmpresa = '" + IdEmpresa + "'and IdSucursal = '" + IdSucursal + "' and IdMovi_inven_tipo = '" + IdMovi_inven_tipo + "' and IdNumMovi = " + IdNumMovi;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Context.Database.ExecuteSqlCommand(sql);
                }
                return true;
            }
            catch (Exception )
            {

                throw;
            }
        }
    }
}
