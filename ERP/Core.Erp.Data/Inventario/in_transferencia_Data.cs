using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Inventario;
using Core.Erp.Data.General;
using Core.Erp.Info.General;

namespace Core.Erp.Data.Inventario
{
   public class in_transferencia_Data
    {


        public List<in_transferencia_Info> get_list(int IdEmpresa, int IdSucursal, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                FechaInicio = FechaInicio.Date;
                FechaFin = FechaFin.Date;
                List<in_transferencia_Info> Lista=null;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.vwin_Transferencias
                             where q.IdEmpresa == IdEmpresa
                             && q.tr_fecha>=FechaInicio
                             && q.tr_fecha<=FechaFin
                             && q.IdSucursalOrigen==IdSucursal
                             select new in_transferencia_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                IdTransferencia = q.IdTransferencia,
                                tr_fecha = q.tr_fecha,
                                Estado = q.Estado,
                                 BodegDest = q.BodegDest,
                                 BodegaORIG = q.BodegaORIG,
                                 SucuDEST = q.SucuDEST,
                                 SucuOrigen = q.SucuOrigen,
                                tr_fechaAnulacion = q.tr_fechaAnulacion,
                                tr_Observacion = q.tr_Observacion,
                                IdBodegaDest = q.IdBodegaDest,
                                IdBodegaOrigen = q.IdBodegaOrigen,
                                IdSucursalDest = q.IdSucursalDest,
                                IdSucursalOrigen = q.IdSucursalOrigen,
                                Codigo = q.Codigo,
                                IdEstadoAprobacion_cat = q.IdEstadoAprobacion_cat,
                                IdMovi_inven_tipo_SucuDest = q.IdMovi_inven_tipo_SucuDest,
                                IdMovi_inven_tipo_SucuOrig = q.IdMovi_inven_tipo_SucuOrig,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception )
            {

                throw;
            }
        }

        public bool guardarDB(in_transferencia_Info info)
        {
            try
            {
                int c = 1;
                using (Entities_inventario contex = new Entities_inventario())
                {
                    in_transferencia address = new in_transferencia
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursalOrigen = info.IdSucursalOrigen,
                        IdBodegaOrigen = info.IdBodegaOrigen,
                        IdTransferencia = info.IdTransferencia = get_id(info.IdEmpresa, info.IdSucursalOrigen, info.IdBodegaOrigen),
                        IdSucursalDest = info.IdSucursalDest,
                        IdBodegaDest = info.IdBodegaDest,
                        tr_Observacion = info.tr_Observacion,
                        IdMovi_inven_tipo_SucuOrig = info.IdMovi_inven_tipo_SucuOrig,
                        IdMovi_inven_tipo_SucuDest = info.IdMovi_inven_tipo_SucuDest,
                        tr_fecha = Convert.ToDateTime(info.tr_fecha.ToShortDateString()),
                        Estado = "A",
                        IdUsuario = (info.IdUsuario == null) ? "" : info.IdUsuario,
                        IdEstadoAprobacion_cat = info.IdEstadoAprobacion_cat,
                        Codigo = info.Codigo,
                    };
                    contex.in_transferencia.Add(address);


                    foreach (var item in info.list_detalle)//guardando detalle de transferencia
                    {
                        in_transferencia_det addressDeta = new in_transferencia_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursalOrigen = info.IdSucursalOrigen,
                            IdTransferencia = info.IdTransferencia,
                            IdBodegaOrigen = info.IdBodegaOrigen,
                            IdProducto = item.IdProducto,
                            dt_cantidad = item.dt_cantidad,
                            IdUnidadMedida = item.IdUnidadMedida,
                            tr_Observacion = item.tr_Observacion,
                            dt_secuencia = item.dt_secuencia = c,
                        };
                        c++;
                        contex.in_transferencia_det.Add(addressDeta);
                    }
                    contex.SaveChanges();



                    return true;

                }
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "in_transferencia_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(in_transferencia_Info info)
        {
            try
            {
                int c = 1;
                using (Entities_inventario contex = new Entities_inventario())
                {
                    in_transferencia Entity = contex.in_transferencia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa 
                    && q.IdBodegaOrigen == info.IdBodegaOrigen && q.IdSucursalOrigen == info.IdSucursalOrigen
                    && q.IdTransferencia == info.IdTransferencia );

                    Entity.tr_fecha = info.tr_fecha;
                    Entity.tr_Observacion = info.tr_Observacion;
                    Entity.Codigo = info.Codigo==null?"":info.Codigo;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    foreach (var item in info.list_detalle)
                    {
                        in_transferencia_det addressDeta = new in_transferencia_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursalOrigen = info.IdSucursalOrigen,
                            IdTransferencia = info.IdTransferencia,
                            IdBodegaOrigen = info.IdBodegaOrigen,
                            IdProducto = item.IdProducto,
                            dt_cantidad = item.dt_cantidad,
                            IdUnidadMedida = item.IdUnidadMedida,
                            tr_Observacion = item.tr_Observacion,
                            dt_secuencia = item.dt_secuencia = c,
                        };
                        c++;
                        contex.in_transferencia_det.Add(addressDeta);
                    }
                    contex.SaveChanges();
                    return true;

                }
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "in_transferencia_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificar_id_ing_egrDB(in_transferencia_Info info)
        {
            try
            {
                using (Entities_inventario contex = new Entities_inventario())
                {
                    in_transferencia Entity = contex.in_transferencia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdBodegaOrigen == info.IdBodegaOrigen && q.IdSucursalOrigen == info.IdSucursalOrigen
                    && q.IdTransferencia == info.IdTransferencia);

                    Entity.IdEmpresa_Ing_Egr_Inven_Origen = info.IdEmpresa_Ing_Egr_Inven_Origen;
                    Entity.IdSucursal_Ing_Egr_Inven_Origen = info.IdSucursal_Ing_Egr_Inven_Origen;
                    Entity.IdMovi_inven_tipo_SucuOrig = info.IdMovi_inven_tipo_SucuOrig;
                    Entity.IdNumMovi_Ing_Egr_Inven_Origen = info.IdNumMovi_Ing_Egr_Inven_Origen;

                    Entity.IdEmpresa_Ing_Egr_Inven_Destino = info.IdEmpresa_Ing_Egr_Inven_Destino;
                    Entity.IdSucursal_Ing_Egr_Inven_Destino = info.IdSucursal_Ing_Egr_Inven_Destino;
                    Entity.IdMovi_inven_tipo_SucuDest = info.IdMovi_inven_tipo_SucuDest;
                    Entity.IdNumMovi_Ing_Egr_Inven_Destino = info.IdNumMovi_Ing_Egr_Inven_Destino;
                   
                    contex.SaveChanges();



                    return true;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(in_transferencia_Info info)
        {
            try
            {
                using (Entities_inventario contex = new Entities_inventario())
                {
                    in_transferencia Entity = contex.in_transferencia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdBodegaOrigen == info.IdBodegaOrigen && q.IdSucursalOrigen == info.IdSucursalOrigen
                    && q.IdTransferencia == info.IdTransferencia);
                    Entity.tr_userAnulo = info.tr_userAnulo;
                    Entity.tr_fechaAnulacion = DateTime.Now;
                    Entity.Estado = "I";
                   
                    contex.SaveChanges();



                    return true;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal get_id(int idEmpresa, int idSucursal, int idBodega)
        {
            try
            {
                decimal id;
                using (Entities_inventario Contex = new Entities_inventario())
                {
                    var Select = from q in Contex.in_transferencia
                                 where q.IdEmpresa == idEmpresa && q.IdSucursalOrigen == idSucursal && q.IdBodegaOrigen == idBodega
                                 select q;
                    if (Select.ToList().Count == 0)
                    {
                        return 1;
                    }
                    else
                    {
                        var qmax = (from q in Contex.in_transferencia
                                    where q.IdEmpresa == idEmpresa && q.IdSucursalOrigen == idSucursal && q.IdBodegaOrigen == idBodega
                                    select q.IdTransferencia).Max();

                        id = Convert.ToInt32(qmax.ToString()) + 1;
                        return id;
                    }
                }

            }
            catch (Exception )
            {
               
                throw ;
            }
        }
        public in_transferencia_Info get_info(int IdEmpresa, int idSucursal, int idBodega, decimal IdTransferencia)
        {
            try
            {
                in_transferencia_Info info = new in_transferencia_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_transferencia Entity = Context.in_transferencia.FirstOrDefault(q => q.IdEmpresa == IdEmpresa 
                    && q.IdSucursalOrigen == idSucursal && q.IdBodegaOrigen == idBodega && q.IdTransferencia == IdTransferencia);
                    if (Entity == null) return null;
                    info = new in_transferencia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursalOrigen = Entity.IdSucursalOrigen,
                        IdBodegaOrigen = Entity.IdBodegaOrigen,
                        IdTransferencia = Entity.IdTransferencia ,
                        IdSucursalDest = Entity.IdSucursalDest,
                        IdBodegaDest = Entity.IdBodegaDest,
                        tr_Observacion = Entity.tr_Observacion,
                        IdMovi_inven_tipo_SucuOrig = Entity.IdMovi_inven_tipo_SucuOrig,
                        IdMovi_inven_tipo_SucuDest = Entity.IdMovi_inven_tipo_SucuDest,
                        tr_fecha = Entity.tr_fecha,
                        Estado = Entity.Estado,
                        IdEstadoAprobacion_cat = Entity.IdEstadoAprobacion_cat,
                        Codigo = Entity.Codigo,

                        IdNumMovi_Ing_Egr_Inven_Origen = Entity.IdNumMovi_Ing_Egr_Inven_Origen,
                        IdNumMovi_Ing_Egr_Inven_Destino = Entity.IdNumMovi_Ing_Egr_Inven_Destino,
                        IdSucursal_Ing_Egr_Inven_Destino = Entity.IdSucursal_Ing_Egr_Inven_Destino,
                        IdSucursal_Ing_Egr_Inven_Origen = Entity.IdSucursal_Ing_Egr_Inven_Origen,
                        IdEmpresa_Ing_Egr_Inven_Destino = Entity.IdEmpresa_Ing_Egr_Inven_Destino,
                        IdEmpresa_Ing_Egr_Inven_Origen = Entity.IdEmpresa_Ing_Egr_Inven_Origen
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<in_transferencia_Info> GetListRecosteoInventario(int IdEmpresa, DateTime FechaInicio, int[] ListaSucursales)
        {
            try
            {
                
                FechaInicio = FechaInicio.Date;
                List<in_transferencia_Info> Lista = new List<in_transferencia_Info>();

                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista.AddRange((from q in Context.vwin_transferencia_x_in_movi_inve_agrupada_para_recosteo
                                    where q.IdEmpresa == IdEmpresa
                                    && q.tr_fecha >= FechaInicio
                                    && (ListaSucursales.Contains(q.IdSucursalOrigen))
                                    group q by new { q.IdEmpresa, q.IdSucursalOrigen, q.cod_sucursal, q.nom_sucursal, q.IdBodegaOrigen, q.cod_bodega, q.nom_bodega, q.tr_fecha }
                              into Lista_agrupada
                                    orderby Lista_agrupada.Key.IdEmpresa, Lista_agrupada.Key.tr_fecha, Lista_agrupada.Key.IdSucursalOrigen, Lista_agrupada.Key.IdBodegaOrigen
                                    select new in_transferencia_Info
                                    {
                                        IdEmpresa = Lista_agrupada.Key.IdEmpresa,
                                        IdSucursalOrigen = Lista_agrupada.Key.IdSucursalOrigen,
                                        SucuOrigen = Lista_agrupada.Key.nom_sucursal,
                                        IdBodegaOrigen = Lista_agrupada.Key.IdBodegaOrigen,
                                        BodegaORIG = Lista_agrupada.Key.nom_bodega,
                                        tr_fecha = Lista_agrupada.Key.tr_fecha,

                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string CorregirTransferencia(List<in_transferencia_Info> Lista_CorregirTransferencia, DateTime fecha_ini)
        {
            try
            {
                using (Entities_inventario contex = new Entities_inventario())
                {
                    contex.SetCommandTimeOut(5000);
                    var mensaje = "";

                    if (Lista_CorregirTransferencia.Count == 0)
                    {
                        mensaje = "No existen transferencias desde esa fecha";
                    }

                    DateTime Fecha_ini = DateTime.Now;
                    in_transferencia_Info info_transferencia = new in_transferencia_Info();

                    var cont = 0;
                    foreach (var item in Lista_CorregirTransferencia)
                    {
                        info_transferencia = Lista_CorregirTransferencia.OrderBy(q => q.IdEmpresa).ThenBy(q => q.IdSucursalOrigen).ThenBy(q => q.IdBodegaOrigen).ThenByDescending(q => q.tr_fecha).ToList().FirstOrDefault(q => q.IdSucursalOrigen == item.IdSucursalOrigen && q.IdBodegaOrigen == item.IdBodegaOrigen && q.tr_fecha < item.tr_fecha);
                        Fecha_ini = info_transferencia == null ? Convert.ToDateTime(fecha_ini) : info_transferencia.tr_fecha.Date;

                        contex.spSys_Inv_Recosteo_Inventario_x_rango_fechas(item.IdEmpresa, item.IdSucursalOrigen, item.IdBodegaOrigen, Fecha_ini, item.tr_fecha, 5);
                        cont++;
                    }

                    if (Lista_CorregirTransferencia.Count == cont)
                        mensaje = "Corrección de transferencias completada";
                    else
                        mensaje = "No se pudieron corregir todas las transferencias";

                    return mensaje;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
