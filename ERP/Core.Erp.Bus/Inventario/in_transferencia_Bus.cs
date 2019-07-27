using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;

namespace Core.Erp.Bus.Inventario
{
    public class in_transferencia_Bus
    {
        in_transferencia_Data odata = new in_transferencia_Data();
        in_transferencia_det_Data odata_det = new in_transferencia_det_Data();
        in_Ing_Egr_Inven_Bus bus_ingreso = new in_Ing_Egr_Inven_Bus();
        in_producto_x_tb_bodega_Costo_Historico_Bus bus_costo = new in_producto_x_tb_bodega_Costo_Historico_Bus();
        in_Motivo_Inven_Bus bus_motivo = new in_Motivo_Inven_Bus();
        public List<in_transferencia_Info> get_list(int IdEmpresa, int IdSucursal, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, FechaInicio, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public in_transferencia_Info get_info(int IdEmpresa, int IdSucursa, int IdBodega, decimal IdTransferencia)
        {
            try
            {
                in_transferencia_Info info = new in_transferencia_Info();
                info= odata.get_info(IdEmpresa, IdSucursa,IdBodega,IdTransferencia);
                info.list_detalle = odata_det.get_list(IdEmpresa, IdSucursa, IdBodega, IdTransferencia);
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_transferencia_Info info)
        {
            try
            {
                info.IdEstadoAprobacion_cat = "XAPRO";
                if (odata.guardarDB(info))
                {
                    get_info_ing_egr(info);
                    bus_ingreso.guardarDB(info.info_ingreso,"+");
                    bus_ingreso.guardarDB(info.info_egreso,"-");

                    info.IdEmpresa_Ing_Egr_Inven_Origen = info.info_egreso.IdEmpresa;
                    info.IdSucursal_Ing_Egr_Inven_Origen = info.info_egreso.IdSucursal;
                    info.IdMovi_inven_tipo_SucuOrig = info.info_egreso.IdMovi_inven_tipo;
                    info.IdNumMovi_Ing_Egr_Inven_Origen = info.info_egreso.IdNumMovi;

                    info.IdEmpresa_Ing_Egr_Inven_Destino = info.info_ingreso.IdEmpresa;
                    info.IdSucursal_Ing_Egr_Inven_Destino = info.info_ingreso.IdSucursal;
                    info.IdMovi_inven_tipo_SucuDest = info.info_ingreso.IdMovi_inven_tipo;
                    info.IdNumMovi_Ing_Egr_Inven_Destino = info.info_ingreso.IdNumMovi;
                    odata.modificar_id_ing_egrDB(info);
                }


                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "in_transferencia_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(in_transferencia_Info info)
        {
            try
            {
                odata = new in_transferencia_Data();
                odata_det = new in_transferencia_det_Data();
                odata_det.anularDB(info);
                if (odata.modificarDB(info))
                {
                    get_info_ing_egr(info);
                    bus_ingreso.modificarDB(info.info_ingreso);
                    bus_ingreso.modificarDB(info.info_egreso);
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "in_transferencia_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(in_transferencia_Info info)
        {
            try
            {
                if (odata.anularDB(info))
                {
                    get_info_ing_egr(info);
                    bus_ingreso.anularDB(info.info_ingreso);
                    bus_ingreso.anularDB(info.info_egreso);
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void get_info_ing_egr(in_transferencia_Info info)
        {
            try
            {
                // armando ingreso
                in_Ing_Egr_Inven_Info ingreso = new in_Ing_Egr_Inven_Info();
                ingreso.IdEmpresa = info.IdEmpresa;
                ingreso.IdNumMovi = info.IdNumMovi_Ing_Egr_Inven_Destino == null ? 0 : Convert.ToDecimal(info.IdNumMovi_Ing_Egr_Inven_Destino);
                ingreso.CodMoviInven = "TRI";
                ingreso.cm_fecha = info.tr_fecha.Date;
                ingreso.IdUsuario = info.IdUsuario;
                ingreso.Fecha_Transac = DateTime.Now;
                ingreso.signo = "+";
                ingreso.IdSucursal = info.IdSucursalDest;
                ingreso.IdBodega = info.IdBodegaDest;
                ingreso.cm_observacion = "Ingreso x Trans." + info.tr_Observacion;
                ingreso.IdMovi_inven_tipo = info.IdMovi_inven_tipo_SucuDest == null ? 0 : Convert.ToInt32(info.IdMovi_inven_tipo_SucuDest);
                ingreso.IdMotivo_Inv = bus_motivo.get_id_movimiento(info.IdEmpresa, "+");
                ingreso.lst_in_Ing_Egr_Inven_det = new List<in_Ing_Egr_Inven_det_Info>();
                info.list_detalle.ForEach(q => { q.IdEmpresa = info.IdEmpresa; q.IdSucursalOrigen = info.IdSucursalOrigen; q.IdBodegaOrigen = info.IdBodegaOrigen; });
                ingreso.lst_in_Ing_Egr_Inven_det = get_detalle(info.list_detalle, info.IdSucursalDest, info.IdBodegaDest, "+", ingreso.cm_fecha);
                info.info_ingreso=ingreso;

                // armando egreso
                in_Ing_Egr_Inven_Info egreso = new in_Ing_Egr_Inven_Info();
                egreso.IdEmpresa = info.IdEmpresa;
                egreso.IdNumMovi = info.IdNumMovi_Ing_Egr_Inven_Origen == null ? 0 : Convert.ToDecimal(info.IdNumMovi_Ing_Egr_Inven_Origen);
                egreso.CodMoviInven = "TRE";
                egreso.cm_fecha = info.tr_fecha.Date;
                egreso.IdUsuario = info.IdUsuario;
                egreso.Fecha_Transac = DateTime.Now;
                egreso.signo = "-";
                egreso.IdSucursal = info.IdSucursalOrigen;
                egreso.IdBodega = info.IdBodegaOrigen;
                egreso.cm_observacion = "Egreso x Trans."  + info.tr_Observacion;
                egreso.IdMovi_inven_tipo = info.IdMovi_inven_tipo_SucuOrig == null ? 0 : Convert.ToInt32(info.IdMovi_inven_tipo_SucuOrig);
                egreso.IdMotivo_Inv = bus_motivo.get_id_movimiento(info.IdEmpresa, "-");

                egreso.lst_in_Ing_Egr_Inven_det = new List<in_Ing_Egr_Inven_det_Info>();
                egreso.lst_in_Ing_Egr_Inven_det = get_detalle(info.list_detalle, info.IdSucursalOrigen, info.IdBodegaOrigen, "-", ingreso.cm_fecha);
                info.info_egreso = egreso;

            }
            catch (Exception )
            {
                throw;

            }

        }
        private List<in_Ing_Egr_Inven_det_Info> get_detalle(List<in_transferencia_det_Info> listDetalle, int IdSucursal, int IdBodega, string Signo, DateTime fecha)
        {
            try
            {
                List<in_Ing_Egr_Inven_det_Info> list_IngEgrDet = new List<in_Ing_Egr_Inven_det_Info>();
                foreach (var item in listDetalle)
                {
                    double costo = bus_costo.get_ultimo_costo(item.IdEmpresa, item.IdSucursalOrigen, item.IdBodegaOrigen, item.IdProducto, fecha);

                    in_Ing_Egr_Inven_det_Info info = new in_Ing_Egr_Inven_det_Info
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdSucursal = IdSucursal,
                        IdNumMovi = 0,                        
                        Secuencia = item.dt_secuencia,
                        IdBodega = IdBodega,
                        IdProducto = item.IdProducto,
                        dm_observacion = item.tr_Observacion,
                        mv_costo = costo,
                        mv_costo_sinConversion = costo,
                        dm_cantidad_sinConversion = Math.Abs(item.dt_cantidad) * (Signo == "-" ? -1 : 1),
                        dm_cantidad = Math.Abs(item.dt_cantidad) * (Signo == "-" ? -1 : 1),
                        IdUnidadMedida = item.IdUnidadMedida,
                        IdUnidadMedida_sinConversion = item.IdUnidadMedida,
                    };
                    list_IngEgrDet.Add(info);
                }
                return list_IngEgrDet;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public string validar(in_transferencia_Info info)
        {
            string mensaje = "";
            if (info.list_detalle.Count == 0)
            {
                mensaje = "Debe ingresar al menos un producto";
            }

            foreach (var item in info.list_detalle)
            {
                if(item.dt_cantidad==0)
                {
                    mensaje = "No existe cantidad";

                }

                if (item.IdProducto == 0)
                {
                    mensaje = "No existe producto en el detalle";

                }
            }
            return mensaje;

        }

        public List<in_transferencia_Info> GetListRecosteoInventario(int IdEmpresa, DateTime FechaInicio, int[] ListaSucursales)
        {
            try
            {
                return odata.GetListRecosteoInventario(IdEmpresa, FechaInicio, ListaSucursales);
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
                return odata.CorregirTransferencia(Lista_CorregirTransferencia, fecha_ini);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
