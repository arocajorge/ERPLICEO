using Core.Erp.Data.General;
using Core.Erp.Info.General;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_devolucion_inven_Data
    {
        public List<in_devolucion_inven_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                List<in_devolucion_inven_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.in_devolucion_inven
                             where q.IdEmpresa == IdEmpresa
                             && q.dev_IdSucursal == IdSucursal
                             && Fecha_ini <= q.Fecha && q.Fecha <= Fecha_fin
                             select new in_devolucion_inven_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdDev_Inven = q.IdDev_Inven,
                                 cod_Dev_Inven = q.cod_Dev_Inven,
                                 Fecha = q.Fecha,
                                 observacion = q.observacion,
                                 Estado = q.Estado,
                                 dev_IdEmpresa = q.dev_IdEmpresa,
                                 dev_IdSucursal = q.dev_IdSucursal,
                                 dev_IdMovi_inven_tipo = q.dev_IdMovi_inven_tipo,
                                 dev_IdNumMovi = q.dev_IdNumMovi,
                                 dev_signo = q.dev_signo
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public in_devolucion_inven_Info get_info(int IdEmpresa, decimal IdDev_Inven)
        {
            try
            {
                in_devolucion_inven_Info info;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_devolucion_inven Entity = Context.in_devolucion_inven.Where(q => q.IdEmpresa == IdEmpresa && q.IdDev_Inven == IdDev_Inven).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new in_devolucion_inven_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdDev_Inven = Entity.IdDev_Inven,
                        cod_Dev_Inven = Entity.cod_Dev_Inven,
                        Fecha = Entity.Fecha,
                        IdEmpresa_inv = Entity.IdEmpresa_inv,
                        IdSucursal_inv = Entity.IdSucursal_inv,
                        IdMovi_inven_tipo_inv = Entity.IdMovi_inven_tipo_inv,
                        IdNumMovi_inv = Entity.IdNumMovi_inv,
                        dev_IdEmpresa = Entity.dev_IdEmpresa,
                        dev_IdSucursal = Entity.dev_IdSucursal,
                        dev_IdMovi_inven_tipo = Entity.dev_IdMovi_inven_tipo,
                        dev_IdNumMovi = Entity.dev_IdNumMovi,
                        dev_signo = Entity.dev_signo,
                        observacion = Entity.observacion,
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

        public decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_devolucion_inven
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdDev_Inven) + 1;
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(in_devolucion_inven_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    info.IdEmpresa_inv = info.lst_det[0].inv_IdEmpresa;
                    info.IdSucursal_inv = info.lst_det[0].inv_IdSucursal;
                    info.IdMovi_inven_tipo_inv = info.lst_det[0].inv_IdMovi_inven_tipo;
                    info.IdNumMovi_inv = info.lst_det[0].inv_IdNumMovi;

                    int Secuencia = 1;
                    in_Ing_Egr_Inven_Data odata_inv = new in_Ing_Egr_Inven_Data();
                    var parametros = Context.in_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                    var motivo = Context.in_Motivo_Inven.Where(q => q.IdEmpresa == info.IdEmpresa && q.Genera_Movi_Inven == "S" && q.Tipo_Ing_Egr == (info.dev_signo == "+" ? "ING" : "EGR")).FirstOrDefault();

                    #region Creo movimiento de devolución
                    in_Ing_Egr_Inven_Info dev = armar_movi(info, info.dev_signo == "+" ? Convert.ToInt32(parametros.IdMovi_Inven_tipo_x_Dev_Inv_x_Erg) : Convert.ToInt32(parametros.IdMovi_Inven_tipo_x_Dev_Inv_x_Ing), motivo.IdMotivo_Inv);

                    if (odata_inv.guardarDB(dev, info.dev_signo))
                    {
                        info.dev_IdEmpresa = dev.IdEmpresa;
                        info.dev_IdSucursal = dev.IdSucursal;
                        info.dev_IdMovi_inven_tipo = dev.IdMovi_inven_tipo;
                        info.dev_IdNumMovi = dev.IdNumMovi;

                        Context.in_devolucion_inven.Add(new in_devolucion_inven
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdDev_Inven = info.IdDev_Inven = get_id(info.IdEmpresa),
                            cod_Dev_Inven = info.cod_Dev_Inven,
                            Fecha = info.Fecha.Date,
                            IdEmpresa_inv = info.IdEmpresa_inv,
                            IdSucursal_inv = info.IdSucursal_inv,
                            IdMovi_inven_tipo_inv = info.IdMovi_inven_tipo_inv,
                            IdNumMovi_inv = info.IdNumMovi_inv,
                            dev_IdEmpresa = info.dev_IdEmpresa,
                            dev_IdSucursal = info.dev_IdSucursal,
                            dev_IdMovi_inven_tipo = info.dev_IdMovi_inven_tipo,
                            dev_IdNumMovi = info.dev_IdNumMovi,
                            dev_signo = info.dev_signo,
                            observacion = info.observacion,
                            Estado = info.Estado = true,
                            IdUsuario = info.IdUsuario,
                            Fecha_Transac = DateTime.Now
                        });
                        Secuencia = 1;
                        foreach (var item in info.lst_det)
                        {
                            Context.in_devolucion_inven_det.Add(new in_devolucion_inven_det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdDev_Inven = info.IdDev_Inven,
                                secuencia = Secuencia++,
                                
                                inv_IdEmpresa = item.inv_IdEmpresa,
                                inv_IdSucursal = item.inv_IdSucursal,
                                inv_IdMovi_inven_tipo = item.inv_IdMovi_inven_tipo,
                                inv_IdNumMovi = item.inv_IdNumMovi,
                                inv_Secuencia = item.inv_Secuencia,

                                cant_devuelta = item.cant_devuelta
                            });
                        }
                        Context.SaveChanges();
                    }
                    
                    #endregion

                    
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "in_devolucion_inven_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public in_Ing_Egr_Inven_Info armar_movi(in_devolucion_inven_Info info, int IdMoviInven_tipo, int IdMotivo_inv)
        {
            try
            {
                in_Ing_Egr_Inven_Info movi = new in_Ing_Egr_Inven_Info
                {
                    IdEmpresa = info.IdEmpresa_inv,
                    IdSucursal = info.IdSucursal_inv,
                    IdBodega = info.lst_det[0].IdBodega,
                    IdMovi_inven_tipo = IdMoviInven_tipo,
                    IdNumMovi = 0,
                    signo = info.dev_signo,
                    cm_fecha = info.Fecha.Date,
                    cm_observacion = info.observacion,
                    CodMoviInven = "DEV",
                    Estado = "A",
                    IdMotivo_Inv = IdMotivo_inv,
                    IdUsuario = info.IdUsuario,
                    lst_in_Ing_Egr_Inven_det = new List<in_Ing_Egr_Inven_det_Info>()
                };
                int secuencia = 1;
                foreach (var item in info.lst_det)
                {
                    movi.lst_in_Ing_Egr_Inven_det.Add(new in_Ing_Egr_Inven_det_Info
                    {
                        IdBodega = item.IdBodega,
                        Secuencia = secuencia++,
                        IdProducto = item.IdProducto,
                        IdUnidadMedida = item.IdUnidadMedida,
                        IdUnidadMedida_sinConversion = item.IdUnidadMedida,
                        mv_costo = item.mv_costo,
                        mv_costo_sinConversion = item.mv_costo,
                        dm_cantidad = Math.Abs(item.cant_devuelta) * (info.dev_signo == "+" ? 1 : -1),
                        dm_cantidad_sinConversion = Math.Abs(item.cant_devuelta) * (info.dev_signo == "+" ? 1 : -1),
                    });
                }
                return movi;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(in_devolucion_inven_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    int Secuencia = 1;
                    in_Ing_Egr_Inven_Data odata_inv = new in_Ing_Egr_Inven_Data();
                    var parametros = Context.in_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                    var motivo = Context.in_Motivo_Inven.Where(q => q.IdEmpresa == info.IdEmpresa && q.Genera_Movi_Inven == "S" && q.Tipo_Ing_Egr == (info.dev_signo == "+" ? "ING" : "EGR")).FirstOrDefault();

                    #region Creo movimiento de devolución

                    in_Ing_Egr_Inven_Info dev = armar_movi(info, info.IdMovi_inven_tipo_inv, motivo.IdMotivo_Inv);
                    dev.IdNumMovi = info.dev_IdNumMovi;
                    if (odata_inv.modificarDB(dev))
                    {
                        var Entity = Context.in_devolucion_inven.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdDev_Inven == info.IdDev_Inven).FirstOrDefault();

                        Entity.cod_Dev_Inven = info.cod_Dev_Inven;
                        Entity.Fecha = info.Fecha.Date;
                        Entity.observacion = info.observacion;

                        Entity.IdUsuarioUltModi = info.IdUsuarioUltModi;
                        Entity.Fecha_UltMod = DateTime.Now;

                        Secuencia = 1;

                        var lst = Context.in_devolucion_inven_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdDev_Inven == info.IdDev_Inven).ToList();
                        Context.in_devolucion_inven_det.RemoveRange(lst);

                        foreach (var item in info.lst_det)
                        {
                            Context.in_devolucion_inven_det.Add(new in_devolucion_inven_det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdDev_Inven = info.IdDev_Inven,
                                secuencia = Secuencia++,

                                inv_IdEmpresa = item.inv_IdEmpresa,
                                inv_IdSucursal = item.inv_IdSucursal,
                                inv_IdMovi_inven_tipo = item.inv_IdMovi_inven_tipo,
                                inv_IdNumMovi = item.inv_IdNumMovi,
                                inv_Secuencia = item.inv_Secuencia,

                                cant_devuelta = item.cant_devuelta
                            });
                        }
                        Context.SaveChanges();
                    }

                    #endregion


                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "in_devolucion_inven_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool anularDB(in_devolucion_inven_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var Entity = Context.in_devolucion_inven.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdDev_Inven == info.IdDev_Inven).FirstOrDefault();

                    Entity.Estado = false;
                    Entity.IdusuarioUltAnu = info.IdusuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
                    in_Ing_Egr_Inven_Data odata_inv = new in_Ing_Egr_Inven_Data();
                    if (!odata_inv.anularDB(new in_Ing_Egr_Inven_Info { IdEmpresa = info.dev_IdEmpresa, IdSucursal = info.dev_IdSucursal, IdMovi_inven_tipo = info.dev_IdMovi_inven_tipo, IdNumMovi = info.dev_IdNumMovi, IdusuarioUltAnu = info.IdusuarioUltAnu }))
                    {
                        Entity.IdusuarioUltAnu = null;
                        Entity.Fecha_UltAnu = null;
                        Entity.Estado = true;
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
    }
}
