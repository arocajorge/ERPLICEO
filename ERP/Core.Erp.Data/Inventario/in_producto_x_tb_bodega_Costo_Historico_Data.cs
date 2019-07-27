using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Inventario;
namespace Core.Erp.Data.Inventario
{
   public class in_producto_x_tb_bodega_Costo_Historico_Data
    {

        public double get_ultimo_costo(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdProducto, DateTime Fecha)
        {
           
                double costo = 0;
                int IdFechaMax = 0;
                int SecuenciaMax = 0;
                Fecha = Fecha.Date;

                using (Entities_inventario Contex = new Entities_inventario())
                {

                    try
                    {
                         IdFechaMax = (from q in Contex.in_producto_x_tb_bodega_Costo_Historico
                                          where q.IdEmpresa == IdEmpresa
                                          && q.IdSucursal == IdSucursal
                                          && q.IdBodega == IdBodega
                                          && q.IdProducto == IdProducto
                                          && q.fecha <= Fecha
                                          select q).Max(v => v.IdFecha);
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                         SecuenciaMax = (from q in Contex.in_producto_x_tb_bodega_Costo_Historico
                                            where q.IdEmpresa == IdEmpresa
                                            && q.IdSucursal == IdSucursal
                                            && q.IdBodega == IdBodega
                                            && q.IdProducto == IdProducto
                                            && q.IdFecha == IdFechaMax
                                            select q).Max(v => v.Secuencia);
                    }
                    catch (Exception)
                    {

                    }


                    try
                    {
                        costo = (from q in Contex.in_producto_x_tb_bodega_Costo_Historico
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.IdBodega == IdBodega && q.IdProducto == IdProducto
                                 && q.IdFecha == IdFechaMax && q.Secuencia == SecuenciaMax
                                 select q.costo).Max();

                    }
                    catch (Exception)
                    {

                    }
                    }

                return costo;
              
           
        }


        public List<in_producto_x_tb_bodega_Costo_Historico_Info> Recosteo_x_Sucursal(int IdEmpresa, int IdSucursal, int IdBodega, DateTime fecha_ini)
        {
            try
            {
                List<in_producto_x_tb_bodega_Costo_Historico_Info> Lista = new List<in_producto_x_tb_bodega_Costo_Historico_Info>();

                using (Entities_inventario contex = new Entities_inventario())
                {
                    contex.SetCommandTimeOut(5000);

                    contex.spSys_Inv_Recosteo_Inventario(IdEmpresa, IdSucursal, IdBodega, fecha_ini,5);

                    Lista = contex.vwin_producto_x_tb_bodega_Costo_Historico.Where(q => 
                    q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.fecha >= fecha_ini                    
                    ).Select(q => new in_producto_x_tb_bodega_Costo_Historico_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        IdBodega = q.IdBodega,
                        IdProducto = q.IdProducto,
                        cod_producto = q.cod_producto,
                        nom_producto = q.nom_producto,
                        IdFecha = q.IdFecha,
                        fecha = q.fecha,
                        Secuencia = q.Secuencia,
                        costo = q.costo,
                        Stock_a_la_fecha = q.Stock_a_la_fecha

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
