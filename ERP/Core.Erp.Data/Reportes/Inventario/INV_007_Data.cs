using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Inventario
{
    public class INV_007_Data
    {
        public List<INV_007_Info> get_list(int IdEmpresa, int IdSucursalOrigen, int IdBodegaOrigen, decimal IdTransferencia)
        {
            try
            {
                List<INV_007_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWINV_007
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursalOrigen == IdSucursalOrigen
                             && q.IdBodegaOrigen == IdBodegaOrigen
                             && q.IdTransferencia == IdTransferencia
                             select new INV_007_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursalOrigen = q.IdSucursalOrigen,
                                 IdBodegaOrigen = q.IdBodegaOrigen,
                                 IdTransferencia = q.IdTransferencia,
                                 dt_secuencia = q.dt_secuencia,
                                 IdProducto = q.IdProducto,
                                 pr_codigo = q.pr_codigo,
                                 pr_descripcion = q.pr_descripcion,
                                 dt_cantidad = q.dt_cantidad,
                                 IdUnidadMedida = q.IdUnidadMedida,
                                 nom_unidad_medida = q.nom_unidad_medida,
                                 cod_sucursal_destino = q.cod_sucursal_destino,
                                 cod_bodega_destino = q.cod_bodega_destino,
                                 cod_bodega_origen =q.cod_bodega_origen,
                                 cod_sucursal_origen = q.cod_sucursal_origen,
                                 nom_bodega_destino = q.nom_bodega_destino,
                                 nom_bodega_origen = q.nom_bodega_origen,
                                 nom_sucursal_destino = q.nom_sucursal_destino,
                                 nom_sucursal_origen = q.nom_sucursal_origen,
                                 tr_fecha = q.tr_fecha,
                                 tr_Observacion = q.tr_Observacion,
                                 Estado = q.Estado,
                                 Codigo = q.Codigo
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
