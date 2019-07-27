using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Importacion;
namespace Core.Erp.Data.Importacion
{
   public class imp_ordencompra_ext_Data
    {
        #region funciones de consulta oc
        public List<imp_ordencompra_ext_Info> get_list(int IdEmpresa, DateTime fecha_inicio, DateTime Fecha_fin)
        {
            try
            {
                List<imp_ordencompra_ext_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Lista = (from q in Context.vwimp_orden_compra_ext
                             where q.IdEmpresa == IdEmpresa
                             && q.oe_fecha >= fecha_inicio
                             && q.oe_fecha <= Fecha_fin
                             select new imp_ordencompra_ext_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenCompra_ext = q.IdOrdenCompra_ext,
                                 IdProveedor = q.IdProveedor,
                                 IdPais_origen = q.IdPais_origen,
                                 IdPais_embarque = q.IdPais_embarque,
                                 IdCiudad_destino = q.IdCiudad_destino,
                                 IdCatalogo_via = q.IdCatalogo_via,
                                 IdCatalogo_forma_pago = q.IdCatalogo_forma_pago,
                                 oe_fecha = q.oe_fecha,
                                 oe_fecha_llegada_est = q.oe_fecha_llegada_est,
                                 oe_fecha_embarque_est = q.oe_fecha_embarque_est,
                                 oe_fecha_desaduanizacion_est = q.oe_fecha_desaduanizacion_est,
                                 IdCtaCble_importacion = q.IdCtaCble_importacion,
                                 oe_observacion = q.oe_observacion,
                                 oe_codigo = q.oe_codigo,
                                 estado = q.estado,
                                 oe_fecha_llegada = q.oe_fecha_llegada,
                                 oe_fecha_embarque = q.oe_fecha_embarque,
                                 oe_fecha_desaduanizacion = q.oe_fecha_desaduanizacion,
                                 cantidad_global = q.cantidad_global,
                                 cantidad_x_recibir = q.cantidad_x_recibir,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_nombreCompleto

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<imp_ordencompra_ext_Info> get_list(int IdEmpresa)
        {
            try
            {
                List<imp_ordencompra_ext_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Lista = (from q in Context.vwimp_orden_compra_ext
                             where q.IdEmpresa == IdEmpresa
                             && q.cantidad_x_recibir > 0
                             && q.estado == true
                             select new imp_ordencompra_ext_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenCompra_ext = q.IdOrdenCompra_ext,
                                 IdProveedor = q.IdProveedor,
                                 IdPais_origen = q.IdPais_origen,
                                 IdPais_embarque = q.IdPais_embarque,
                                 IdCiudad_destino = q.IdCiudad_destino,
                                 IdCatalogo_via = q.IdCatalogo_via,
                                 IdCatalogo_forma_pago = q.IdCatalogo_forma_pago,
                                 oe_fecha = q.oe_fecha,
                                 oe_fecha_llegada_est = q.oe_fecha_llegada_est,
                                 oe_fecha_embarque_est = q.oe_fecha_embarque_est,
                                 oe_fecha_desaduanizacion_est = q.oe_fecha_desaduanizacion_est,
                                 IdCtaCble_importacion = q.IdCtaCble_importacion,
                                 oe_observacion = q.oe_observacion,
                                 oe_codigo = q.oe_codigo,
                                 estado = q.estado,
                                 oe_fecha_llegada = q.oe_fecha_llegada,
                                 oe_fecha_embarque = q.oe_fecha_embarque,
                                 oe_fecha_desaduanizacion = q.oe_fecha_desaduanizacion,
                                 cantidad_global = q.cantidad_global,
                                 cantidad_x_recibir = q.cantidad_x_recibir,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_nombreCompleto

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public imp_ordencompra_ext_Info get_info(int IdEmpresa, decimal IdOrdenCompra_ext)
        {
            try
            {
                imp_ordencompra_ext_Info info = new imp_ordencompra_ext_Info();
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_orden_compra_ext Entity = Context.imp_orden_compra_ext.FirstOrDefault(q => q.IdOrdenCompra_ext == IdOrdenCompra_ext && q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new imp_ordencompra_ext_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdOrdenCompra_ext = Entity.IdOrdenCompra_ext,
                        IdProveedor = Entity.IdProveedor,
                        IdPais_origen = Entity.IdPais_origen,
                        IdPais_embarque = Entity.IdPais_embarque,
                        IdCiudad_destino = Entity.IdCiudad_destino,
                        IdCatalogo_via = Entity.IdCatalogo_via,
                        IdCatalogo_forma_pago = Entity.IdCatalogo_forma_pago,
                        oe_fecha = Entity.oe_fecha,
                        oe_fecha_llegada_est = Entity.oe_fecha_llegada_est,
                        oe_fecha_embarque = Entity.oe_fecha_embarque,
                        oe_fecha_desaduanizacion_est = Entity.oe_fecha_desaduanizacion_est,
                        IdCtaCble_importacion = Entity.IdCtaCble_importacion,
                        oe_observacion = Entity.oe_observacion,
                        oe_codigo = Entity.oe_codigo,
                        estado = Entity.estado,
                        oe_fecha_llegada = Entity.oe_fecha_llegada,
                        oe_fecha_embarque_est = Entity.oe_fecha_embarque_est,
                        oe_fecha_desaduanizacion = Entity.oe_fecha_desaduanizacion,
                        IdMoneda_destino = Entity.IdMoneda_destino,
                        IdMoneda_origen = Entity.IdMoneda_destino
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region acciones de oc
        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    var lst = from q in Context.imp_orden_compra_ext
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdOrdenCompra_ext) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(imp_ordencompra_ext_Info info)
        {
            try
            {
                int secuancia = 1;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_orden_compra_ext Entity = new imp_orden_compra_ext
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdOrdenCompra_ext = info.IdOrdenCompra_ext = get_id(info.IdEmpresa),
                        IdProveedor = info.IdProveedor,
                        IdPais_origen = info.IdPais_origen,
                        IdPais_embarque = info.IdPais_embarque,
                        IdCiudad_destino = info.IdCiudad_destino,
                        IdCatalogo_via = info.IdCatalogo_via,
                        IdCatalogo_forma_pago = info.IdCatalogo_forma_pago,
                        oe_fecha = info.oe_fecha.Date,
                        oe_fecha_llegada_est = info.oe_fecha_llegada_est,
                        oe_fecha_embarque = info.oe_fecha_embarque,
                        oe_fecha_desaduanizacion_est = info.oe_fecha_desaduanizacion_est,
                        IdCtaCble_importacion = info.IdCtaCble_importacion,
                        oe_observacion = info.oe_observacion,
                        oe_codigo = info.oe_codigo,
                        oe_fecha_llegada = info.oe_fecha_llegada,
                        oe_fecha_embarque_est = info.oe_fecha_embarque_est,
                        oe_fecha_desaduanizacion = info.oe_fecha_desaduanizacion,
                        estado = info.estado = true,
                        fecha_creacion = DateTime.Now,
                        IdUsuario_creacion = info.IdUsuario_creacion,
                        IdMoneda_destino = info.IdMoneda_destino,
                        IdMoneda_origen = info.IdMoneda_origen

                    };
                    Context.imp_orden_compra_ext.Add(Entity);

                    foreach (var item in info.lst_detalle)
                    {
                        Context.imp_orden_compra_ext_det.Add(new imp_orden_compra_ext_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdOrdenCompra_ext = info.IdOrdenCompra_ext,
                            Secuencia = secuancia,
                            IdProducto = item.IdProducto,
                            IdUnidadMedida = item.IdUnidadMedida,
                            od_cantidad = item.od_cantidad,
                            od_costo = item.od_costo,
                            od_por_descuento = item.od_por_descuento,
                            od_descuento = item.od_descuento,
                            od_costo_final = item.od_costo_final,
                            od_subtotal = item.od_subtotal,
                            od_cantidad_recepcion = item.od_cantidad_recepcion,
                            od_costo_convertido = item.od_costo_convertido,
                            od_total_fob = Convert.ToDouble(item.od_total_fob),
                            od_factor_costo = item.od_factor_costo,
                            od_costo_bodega = item.od_costo_bodega,
                            od_costo_total = item.od_costo_total

                        });
                        secuancia++;
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

        public bool modificarDB(imp_ordencompra_ext_Info info)
        {
            int secuancia = 1;
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_orden_compra_ext Entity = Context.imp_orden_compra_ext.FirstOrDefault(q => q.IdOrdenCompra_ext == info.IdOrdenCompra_ext);
                    if (Entity == null) return false;
                    Entity.IdPais_origen = info.IdPais_origen;
                    Entity.IdPais_embarque = info.IdPais_embarque;
                    Entity.IdCiudad_destino = info.IdCiudad_destino;
                    Entity.IdCatalogo_via = info.IdCatalogo_via;
                    Entity.IdCatalogo_forma_pago = info.IdCatalogo_forma_pago;
                    Entity.oe_fecha = info.oe_fecha.Date;
                    Entity.oe_fecha_llegada_est = info.oe_fecha_llegada_est;
                    Entity.oe_fecha_embarque = info.oe_fecha_embarque;
                    Entity.oe_fecha_desaduanizacion_est = info.oe_fecha_desaduanizacion_est;
                    Entity.IdCtaCble_importacion = info.IdCtaCble_importacion;
                    Entity.oe_observacion = info.oe_observacion;
                    Entity.oe_codigo = info.oe_codigo;
                    Entity.oe_fecha_llegada = info.oe_fecha_llegada;
                    Entity.oe_fecha_embarque_est = info.oe_fecha_embarque_est;
                    Entity.IdMoneda_origen = info.IdMoneda_origen;
                    Entity.IdMoneda_destino = info.IdMoneda_destino;
                    Entity.oe_fecha_desaduanizacion = info.oe_fecha_desaduanizacion;

                    foreach (var item in info.lst_detalle)
                    {
                        Context.imp_orden_compra_ext_det.Add(new imp_orden_compra_ext_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdOrdenCompra_ext = info.IdOrdenCompra_ext,
                            Secuencia = secuancia,
                            IdProducto = item.IdProducto,
                            IdUnidadMedida = item.IdUnidadMedida,
                            od_cantidad = item.od_cantidad,
                            od_costo = item.od_costo,
                            od_por_descuento = item.od_por_descuento,
                            od_descuento = item.od_descuento,
                            od_costo_final = item.od_costo_final,
                            od_subtotal = item.od_subtotal,
                            od_cantidad_recepcion = item.od_cantidad_recepcion,
                            od_costo_convertido = item.od_costo_convertido,
                            od_total_fob = Convert.ToDouble(item.od_total_fob),
                            od_factor_costo = item.od_factor_costo,
                            od_costo_bodega = item.od_costo_bodega,
                            od_costo_total = item.od_costo_total

                        });
                        secuancia++;
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
        public bool anularDB(imp_ordencompra_ext_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_orden_compra_ext Entity = Context.imp_orden_compra_ext.FirstOrDefault(q => q.IdOrdenCompra_ext == info.IdOrdenCompra_ext);
                    if (Entity == null) return false;
                    Entity.estado = info.estado = false;
                    Entity.fecha_anulacion = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarLiquidacionDB(imp_ordencompra_ext_Info info)
        {
            int secuancia = 1;
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_orden_compra_ext Entity = Context.imp_orden_compra_ext.FirstOrDefault(q => q.IdOrdenCompra_ext == info.IdOrdenCompra_ext & q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null) return false;
                    Entity.Estado_cierre = true;

                    var detalle = Context.imp_orden_compra_ext_det.Where(q => q.IdOrdenCompra_ext == info.IdOrdenCompra_ext & q.IdEmpresa == info.IdEmpresa);
                    Context.imp_orden_compra_ext_det.RemoveRange(detalle);
                    foreach (var item in info.lst_detalle)
                    {
                        Context.imp_orden_compra_ext_det.Add(new imp_orden_compra_ext_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdOrdenCompra_ext = info.IdOrdenCompra_ext,
                            Secuencia = secuancia,
                            IdProducto = item.IdProducto,
                            IdUnidadMedida = item.IdUnidadMedida,
                            od_cantidad = item.od_cantidad,
                            od_costo = item.od_costo,
                            od_por_descuento = item.od_por_descuento,
                            od_descuento = item.od_descuento,
                            od_costo_final = item.od_costo_final,
                            od_subtotal = item.od_subtotal,
                            od_cantidad_recepcion = item.od_cantidad_recepcion,
                            od_costo_convertido = item.od_costo_convertido,
                            od_total_fob = Convert.ToDouble(item.od_total_fob),
                            od_factor_costo = item.od_factor_costo,
                            od_costo_bodega = item.od_costo_bodega,
                            od_costo_total = item.od_costo_total
                        });
                        secuancia++;
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
        #endregion

        #region funciones para recepcion de mercaderia
        public List<imp_ordencompra_ext_Info> get_list_oc_con_recepcion_mercaderia(int IdEmpresa, DateTime fecha_inicio, DateTime Fecha_fin)
        {
            try
            {
                List<imp_ordencompra_ext_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Lista = (from q in Context.vwimp_orden_compra_ext_recepcion
                             where q.IdEmpresa == IdEmpresa
                             && q.or_fecha >= fecha_inicio
                             && q.or_fecha <= Fecha_fin
                             select new imp_ordencompra_ext_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenCompra_ext = q.IdOrdenCompraExt,
                                 oe_fecha = q.oe_fecha,
                                 oe_observacion = q.or_observacion,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 estado = q.estado

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public imp_ordencompra_ext_Info get_info_recepcion_merca(int IdEmpresa, decimal IdOrdenCompra_ext)
        {
            try
            {
                imp_ordencompra_ext_Info info = new imp_ordencompra_ext_Info();
                using (Entities_importacion Context = new Entities_importacion())
                {
                    vwimp_orden_compra_ext Entity = Context.vwimp_orden_compra_ext.FirstOrDefault(q => q.IdOrdenCompra_ext == IdOrdenCompra_ext && q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new imp_ordencompra_ext_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdOrdenCompra_ext = Entity.IdOrdenCompra_ext,
                        IdProveedor = Entity.IdProveedor,
                        IdPais_origen = Entity.IdPais_origen,
                        IdPais_embarque = Entity.IdPais_embarque,
                        IdCiudad_destino = Entity.IdCiudad_destino,
                        IdCatalogo_via = Entity.IdCatalogo_via,
                        IdCatalogo_forma_pago = Entity.IdCatalogo_forma_pago,
                        oe_fecha = Entity.oe_fecha,
                        oe_fecha_llegada_est = Entity.oe_fecha_llegada_est,
                        oe_fecha_embarque = Entity.oe_fecha_embarque,
                        oe_fecha_desaduanizacion_est = Entity.oe_fecha_desaduanizacion_est,
                        IdCtaCble_importacion = Entity.IdCtaCble_importacion,
                        oe_observacion = Entity.oe_observacion,
                        oe_codigo = Entity.oe_codigo,
                        estado = Entity.estado,
                        oe_fecha_llegada = Entity.oe_fecha_llegada,
                        oe_fecha_embarque_est = Entity.oe_fecha_embarque_est,
                        oe_fecha_desaduanizacion = Entity.oe_fecha_desaduanizacion,
                        IdMoneda_destino = Entity.IdMoneda_destino,
                        IdMoneda_origen = Entity.IdMoneda_destino,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region funciones para liquidar oc
        public List<imp_ordencompra_ext_Info> get_list_oc_por_liquidar(int IdEmpresa)
        {
            try
            {
                List<imp_ordencompra_ext_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Lista = (from q in Context.vwimp_orden_compra_ext_por_liquidar
                             where q.IdEmpresa == IdEmpresa
                             select new imp_ordencompra_ext_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenCompra_ext = q.IdOrdenCompra_ext,
                                 oe_fecha = q.oe_fecha,
                                 oe_observacion = q.oe_observacion,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 oe_fecha_llegada=q.oe_fecha_llegada,
                                 oe_fecha_embarque=q.oe_fecha_embarque,
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

        #endregion


    }
}
