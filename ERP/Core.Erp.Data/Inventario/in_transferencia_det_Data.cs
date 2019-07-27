using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Inventario;
namespace Core.Erp.Data.Inventario
{
   public class in_transferencia_det_Data
    {

        public List<in_transferencia_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdTransferencia)
        {
            try
            {
                List<in_transferencia_det_Info> Lista = null;

                using (Entities_inventario Context = new Entities_inventario())
                {
                    Lista = (from q in Context.in_Producto
                              join p in Context.in_transferencia_det
                              on new {q.IdEmpresa, q.IdProducto}equals new {p.IdEmpresa,p.IdProducto}
                              join t in Context.in_ProductoTipo
                              on new { q.IdEmpresa, q.IdProductoTipo} equals new { t.IdEmpresa, t.IdProductoTipo}
                             where q.IdEmpresa == IdEmpresa
                           && p.IdSucursalOrigen == IdSucursal
                           && p.IdBodegaOrigen == IdBodega
                           && p.IdTransferencia == IdTransferencia
                           && q.IdEmpresa==IdEmpresa
                             select new in_transferencia_det_Info
                             {
                                 IdEmpresa = p.IdEmpresa,
                                 IdSucursalOrigen = p.IdSucursalOrigen,
                                 IdBodegaOrigen = p.IdBodegaOrigen,
                                 IdTransferencia = p.IdTransferencia,
                                 dt_secuencia = p.dt_secuencia,
                                 IdProducto = p.IdProducto,
                                 dt_cantidad = p.dt_cantidad,
                                 tr_Observacion = p.tr_Observacion,
                                 IdUnidadMedida = p.IdUnidadMedida,
                                 pr_descripcion=q.pr_descripcion,
                                 tp_ManejaInven = t.tp_ManejaInven,
                                 se_distribuye = q.se_distribuye ?? false,
                                 CantidadAnterior = p.dt_cantidad
                             }).ToList();

                }

                return Lista;
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
                int c = 1;
                using (Entities_inventario contex = new Entities_inventario())
                {
                    
                    foreach (var item in info.list_detalle)
                    {
                        in_transferencia_det addressDeta = new in_transferencia_det
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursalOrigen = item.IdSucursalOrigen,
                            IdBodegaOrigen = item.IdBodegaOrigen,
                            IdTransferencia = item.IdTransferencia,
                            dt_secuencia = c,
                            IdProducto = item.IdProducto,
                            dt_cantidad = item.dt_cantidad,
                            tr_Observacion = item.tr_Observacion,
                            IdUnidadMedida = item.IdUnidadMedida,
                           
                        };
                        c++;
                        contex.in_transferencia_det.Add(addressDeta);
                    }
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
                    string sql = "delete in_transferencia_det where IdEmpresa='"+info.IdEmpresa+ "' and IdSucursalOrigen='"+info.IdSucursalOrigen + "' and IdBodegaOrigen='"+info.IdBodegaOrigen + "' and IdTransferencia='"+info.IdTransferencia + "'";
                    contex.Database.ExecuteSqlCommand(sql);
                    return true;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
