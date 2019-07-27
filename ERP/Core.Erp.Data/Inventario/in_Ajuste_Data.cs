using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_Ajuste_Data
    {        
        in_parametro_Data data_parametro = new in_parametro_Data();
        in_Ing_Egr_Inven_Data data_in_Ing_Egr_Inven = new in_Ing_Egr_Inven_Data();
        in_Ing_Egr_Inven_det_Data data_in_Ing_Egr_Inven_det = new in_Ing_Egr_Inven_det_Data();

        public List<in_Ajuste_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                int IdSucursal_ini = IdSucursal;
                int IdSucursal_fin = IdSucursal == 0 ? 999999 : IdSucursal;
                List<in_Ajuste_Info> Lista = new List<in_Ajuste_Info>();

                using (Entities_inventario db = new Entities_inventario())
                {
                    if (mostrar_anulados == false)
                    {
                        Lista = db.vwin_Ajuste.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal >= IdSucursal_ini && q.IdSucursal <= IdSucursal_fin && q.Estado == true && q.Fecha >= fecha_ini && q.Fecha <= fecha_fin).Select(q => new in_Ajuste_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdAjuste = q.IdAjuste,
                            IdBodega = q.IdBodega,
                            IdMovi_inven_tipo_ing = q.IdMovi_inven_tipo_ing,
                            IdMovi_inven_tipo_egr = q.IdMovi_inven_tipo_egr,
                            IdNumMovi_ing = q.IdNumMovi_ing,
                            IdNumMovi_egr = q.IdNumMovi_egr,
                            IdCatalogo_Estado = q.IdCatalogo_Estado,
                            Fecha = q.Fecha,
                            NombreEstado = q.NombreEstado,
                            Su_Descripcion = q.Su_Descripcion,
                            bo_Descripcion = q.bo_Descripcion,
                            Observacion = q.Observacion,
                            Estado = q.Estado
                        }).ToList();
                    }
                    else
                    {
                        Lista = db.vwin_Ajuste.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.Fecha >= fecha_ini && q.Fecha <= fecha_fin).Select(q => new in_Ajuste_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdAjuste = q.IdAjuste,
                            IdBodega = q.IdBodega,
                            IdMovi_inven_tipo_ing = q.IdMovi_inven_tipo_ing,
                            IdMovi_inven_tipo_egr = q.IdMovi_inven_tipo_egr,
                            IdNumMovi_ing = q.IdNumMovi_ing,
                            IdNumMovi_egr = q.IdNumMovi_egr,
                            IdCatalogo_Estado = q.IdCatalogo_Estado,
                            Fecha = q.Fecha,
                            NombreEstado = q.NombreEstado,
                            Su_Descripcion = q.Su_Descripcion,
                            bo_Descripcion = q.bo_Descripcion,
                            Observacion = q.Observacion,
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

        public in_Ajuste_Info get_info(int IdEmpresa, int IdSucursal, decimal IdAjuste)
        {
            try
            {
                in_Ajuste_Info info = new in_Ajuste_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Ajuste Entity = Context.in_Ajuste.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdAjuste == IdAjuste);
                    if (Entity == null) return null;
                    info = new in_Ajuste_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdAjuste = Entity.IdAjuste,
                        IdBodega = Entity.IdBodega,
                        IdMovi_inven_tipo_ing = Entity.IdMovi_inven_tipo_ing,
                        IdMovi_inven_tipo_egr = Entity.IdMovi_inven_tipo_egr,
                        IdNumMovi_ing = Entity.IdNumMovi_ing,
                        IdNumMovi_egr = Entity.IdNumMovi_egr,
                        IdCatalogo_Estado = Entity.IdCatalogo_Estado,
                        Fecha = Entity.Fecha,
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

        private decimal get_id(int IdEmpresa)
        {

            try
            {
                decimal ID = 1;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_Ajuste
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdAjuste) + 1;

                    return ID;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_Ajuste_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    int secuencia = 1;
                    in_parametro_Info info_parametro = data_parametro.get_info(info.IdEmpresa);

                    in_Ajuste Entity = new in_Ajuste
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdAjuste = info.IdAjuste = get_id(info.IdEmpresa),
                        IdBodega = info.IdBodega,
                        IdMovi_inven_tipo_ing = info.IdMovi_inven_tipo_ing = info_parametro.IdMovi_inven_tipo_ajuste_ing,
                        IdMovi_inven_tipo_egr = info.IdMovi_inven_tipo_egr = info_parametro.IdMovi_inven_tipo_ajuste_egr,
                        IdNumMovi_ing = info.IdNumMovi_ing,
                        IdNumMovi_egr = info.IdNumMovi_egr,
                        IdCatalogo_Estado = info.IdCatalogo_Estado,
                        Fecha = info.Fecha,
                        Observacion = info.Observacion,
                        Estado = info.Estado,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                        
                    };

                    Context.in_Ajuste.Add(Entity);

                    foreach (var item in info.lst_detalle)
                    {
                        in_AjusteDet entity_det = new in_AjusteDet
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdAjuste = info.IdAjuste,
                            Secuencia = item.Secuencia = secuencia,
                            IdProducto = item.IdProducto,
                            IdUnidadMedida = item.IdUnidadMedida,
                            StockFisico = item.StockFisico,
                            StockSistema = item.StockSistema,
                            Ajuste = item.Ajuste,
                            Costo = item.Costo
                        };
                        secuencia++;
                        Context.in_AjusteDet.Add(entity_det);                      
                       
                    }

                    Context.SaveChanges();

                    #region Movimiento Inventario
                    Entities_inventario dbi = new Entities_inventario();
                    in_Ing_Egr_Inven_Data odata_i = new in_Ing_Egr_Inven_Data();
                    int IdMovi_inven_tipo = 0;
                    List<in_AjusteDet_Info> lst_ingreso = info.lst_detalle.Where(q => q.Ajuste > 0).ToList();
                    List<in_AjusteDet_Info> lst_egreso = info.lst_detalle.Where(q => q.Ajuste < 0).ToList();

                    if (info_parametro == null)
                        return false;

                    if (info.IdCatalogo_Estado == "APRO")
                    {
                        if (lst_ingreso.Count() > 0)
                        {
                            in_Ing_Egr_Inven_Info info_movi = new in_Ing_Egr_Inven_Info();
                            IdMovi_inven_tipo = info_parametro.IdMovi_inven_tipo_ajuste_ing ?? 0;
                            info.lst_movimiento = lst_ingreso;

                            info_movi = GenerarMoviInven(info, "+", info_parametro.IdMotivo_Inv_ajuste_ing);
                            if (info_movi == null)
                                return true;

                            if (info.IdNumMovi_ing == null && odata_i.guardarDB(info_movi, "+"))
                            {
                                info.IdNumMovi_ing = info_movi.IdNumMovi;
                                Entity.IdNumMovi_ing = info.IdNumMovi_ing;

                                Context.SaveChanges();
                            }
                        }

                        if (lst_egreso.Count() > 0)
                        {
                            in_Ing_Egr_Inven_Info info_movi = new in_Ing_Egr_Inven_Info();
                            IdMovi_inven_tipo = info_parametro.IdMovi_inven_tipo_ajuste_ing ?? 0;
                            info.lst_movimiento = lst_egreso;

                            info_movi = GenerarMoviInven(info, "-", info_parametro.IdMotivo_Inv_ajuste_egr);
                            if (info_movi == null)
                                return true;

                            if (info.IdNumMovi_egr == null && odata_i.guardarDB(info_movi, "-"))
                            {
                                info.IdNumMovi_egr = info_movi.IdNumMovi;
                                Entity.IdNumMovi_egr = info.IdNumMovi_egr;

                                Context.SaveChanges();
                            }
                        }
                    }                                       
                    #endregion

                    Context.Dispose();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(in_Ajuste_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    int secuencia = 1;
                    in_parametro_Info info_parametro = data_parametro.get_info(info.IdEmpresa);
                    in_Ajuste Entity = Context.in_Ajuste.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAjuste == info.IdAjuste);

                    if (Entity == null)
                        return false;

                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.IdSucursal = info.IdSucursal;
                    Entity.IdAjuste = info.IdAjuste;
                    Entity.IdBodega = info.IdBodega;
                    Entity.IdMovi_inven_tipo_ing = info.IdMovi_inven_tipo_ing;
                    Entity.IdMovi_inven_tipo_egr = info.IdMovi_inven_tipo_egr;
                    Entity.IdNumMovi_ing = info.IdNumMovi_ing;
                    Entity.IdNumMovi_egr = info.IdNumMovi_egr;
                    Entity.IdCatalogo_Estado = info.IdCatalogo_Estado;
                    Entity.Fecha = info.Fecha;
                    Entity.Observacion = info.Observacion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    var detalle = Context.in_AjusteDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAjuste == info.IdAjuste);
                    Context.in_AjusteDet.RemoveRange(detalle);

                    if (info.lst_detalle.Count() > 0)
                    {
                        foreach (var item in info.lst_detalle)
                        {
                            Context.in_AjusteDet.Add(new in_AjusteDet
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAjuste = info.IdAjuste,
                                Secuencia = item.Secuencia = secuencia,
                                IdProducto = item.IdProducto,
                                IdUnidadMedida = item.IdUnidadMedida,
                                StockFisico = item.StockFisico,
                                StockSistema = item.StockSistema,
                                Ajuste = item.Ajuste,
                                Costo = item.Costo
                            });

                            secuencia++;
                        }
                    }

                    Context.SaveChanges();

                    #region Movimiento Inventario
                    Entities_inventario dbi = new Entities_inventario();
                    in_Ing_Egr_Inven_Data odata_i = new in_Ing_Egr_Inven_Data();
                    int IdMovi_inven_tipo = 0;
                    List<in_AjusteDet_Info> lst_ingreso = info.lst_detalle.Where(q => q.Ajuste > 0).ToList();
                    List<in_AjusteDet_Info> lst_egreso = info.lst_detalle.Where(q => q.Ajuste < 0).ToList();

                    if (info_parametro == null)
                        return false;

                    if (info.IdCatalogo_Estado == "APRO")
                    {
                        if (lst_ingreso.Count() > 0)
                        {
                            in_Ing_Egr_Inven_Info info_movi = new in_Ing_Egr_Inven_Info();
                            IdMovi_inven_tipo = info_parametro.IdMovi_inven_tipo_ajuste_ing ?? 0;
                            info.lst_movimiento = lst_ingreso;

                            info_movi = GenerarMoviInven(info, "+", info_parametro.IdMotivo_Inv_ajuste_ing);
                            if (info_movi == null)
                                return true;

                            if (info.IdNumMovi_ing == null && odata_i.guardarDB(info_movi, "+"))
                            {
                                info.IdNumMovi_ing = info_movi.IdNumMovi;
                                Entity.IdNumMovi_ing = info.IdNumMovi_ing;

                                Context.SaveChanges();
                            }
                        }

                        if (lst_egreso.Count() > 0)
                        {
                            in_Ing_Egr_Inven_Info info_movi = new in_Ing_Egr_Inven_Info();
                            IdMovi_inven_tipo = info_parametro.IdMovi_inven_tipo_ajuste_ing ?? 0;
                            info.lst_movimiento = lst_egreso;

                            info_movi = GenerarMoviInven(info, "-", info_parametro.IdMotivo_Inv_ajuste_egr);
                            if (info_movi == null)
                                return true;

                            if (info.IdNumMovi_egr == null && odata_i.guardarDB(info_movi, "-"))
                            {
                                info.IdNumMovi_egr = info_movi.IdNumMovi;
                                Entity.IdNumMovi_egr = info.IdNumMovi_egr;

                                Context.SaveChanges();
                            }
                        }
                    }                    
                    #endregion
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool anularDB(in_Ajuste_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_Ajuste Entity = Context.in_Ajuste.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAjuste == info.IdAjuste);
                    if (Entity == null)
                        return false;

                    Entity.Estado = info.Estado;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;

                    in_Ing_Egr_Inven Entity_Ingreso = Context.in_Ing_Egr_Inven.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo_ing && q.IdNumMovi == info.IdNumMovi_ing);
                    if (Entity_Ingreso == null)
                        return false;

                    Entity_Ingreso.Estado = "I";
                    Entity_Ingreso.MotivoAnulacion = info.MotivoAnulacion;
                    Entity_Ingreso.IdusuarioUltAnu = info.IdUsuarioAnulacion;
                    Entity_Ingreso.Fecha_UltAnu = DateTime.Now;

                    in_Ing_Egr_Inven Entity_Egreso = Context.in_Ing_Egr_Inven.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdMovi_inven_tipo == info.IdMovi_inven_tipo_egr && q.IdNumMovi == info.IdNumMovi_egr);
                    if (Entity_Egreso == null)
                        return false;

                    Entity_Egreso.Estado = "I";
                    Entity_Egreso.MotivoAnulacion = info.MotivoAnulacion;
                    Entity_Egreso.IdusuarioUltAnu = info.IdUsuarioAnulacion;
                    Entity_Egreso.Fecha_UltAnu = DateTime.Now;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private in_Ing_Egr_Inven_Info GenerarMoviInven(in_Ajuste_Info info, string Signo, int? IdMotivo_inv)
        {
            try
            {
                using (Entities_inventario db = new Entities_inventario())
                {
                    in_Ing_Egr_Inven_Info movi = new in_Ing_Egr_Inven_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        cm_fecha = info.Fecha.Date,
                        Estado = "A",
                        signo = Signo,
                        IdUsuario = info.IdUsuarioCreacion,
                        IdUsuarioUltModi = info.IdUsuarioModificacion,
                        IdMovi_inven_tipo = Convert.ToInt32(Signo == "+" ? info.IdMovi_inven_tipo_ing : info.IdMovi_inven_tipo_egr),
                        IdNumMovi = Convert.ToInt32(Signo == "+" ? info.IdNumMovi_ing : info.IdNumMovi_egr),
                        lst_in_Ing_Egr_Inven_det = new List<in_Ing_Egr_Inven_det_Info>(),
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        cm_observacion = info.Observacion,
                        IdMotivo_Inv = IdMotivo_inv
                    };

                    int secuencia = 1;
                    foreach (var item in info.lst_movimiento)
                    {
                        movi.lst_in_Ing_Egr_Inven_det.Add(new in_Ing_Egr_Inven_det_Info
                        {
                            IdEmpresa = movi.IdEmpresa,
                            IdSucursal = movi.IdSucursal,
                            IdBodega = Convert.ToInt32(movi.IdBodega),
                            IdMovi_inven_tipo = Convert.ToInt32(Signo == "+" ? info.IdMovi_inven_tipo_ing : info.IdMovi_inven_tipo_egr),
                            IdNumMovi = Convert.ToInt32(Signo == "+" ? info.IdNumMovi_ing : info.IdNumMovi_egr),
                            Secuencia = secuencia++,
                            IdProducto = item.IdProducto,
                            dm_cantidad = item.Ajuste,
                            dm_cantidad_sinConversion = item.Ajuste,
                            mv_costo = item.Costo,
                            mv_costo_sinConversion = item.Costo,
                            dm_observacion = info.Observacion,
                            IdUnidadMedida = item.IdUnidadMedida,
                            IdUnidadMedida_sinConversion = item.IdUnidadMedida,
                        });
                    }
                    return movi;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
