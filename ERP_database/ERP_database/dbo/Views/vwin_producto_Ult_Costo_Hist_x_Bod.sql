CREATE VIEW [dbo].[vwin_producto_Ult_Costo_Hist_x_Bod] AS

SELECT        prod_cost_h.IdEmpresa, prod_cost_h.IdSucursal, prod_cost_h.IdBodega, prod_cost_h.IdProducto, prod_cost_h.costo
FROM            dbo.in_producto_x_tb_bodega_Costo_Historico AS prod_cost_h INNER JOIN
                             (
							
								SELECT  prod_x_costo_ag_x_fecha.IdEmpresa, prod_x_costo_ag_x_fecha.IdSucursal, prod_x_costo_ag_x_fecha.IdBodega, prod_x_costo_ag_x_fecha.IdProducto, 
								prod_x_costo_ag_x_fecha.IdFecha ,max(prod_x_costo.Secuencia) as Secuencia
								FROM in_producto_x_tb_bodega_Costo_Historico prod_x_costo ,
								(

									SELECT        IdEmpresa, IdSucursal, IdBodega, IdProducto, max(IdFecha) AS IdFecha 
									FROM            dbo.in_producto_x_tb_bodega_Costo_Historico AS A
									GROUP BY IdEmpresa, IdSucursal, IdBodega, IdProducto
									) as prod_x_costo_ag_x_fecha
								where prod_x_costo.IdEmpresa=prod_x_costo_ag_x_fecha.IdEmpresa
								 and prod_x_costo.IdSucursal=prod_x_costo_ag_x_fecha.IdSucursal
								  and prod_x_costo.IdBodega=prod_x_costo_ag_x_fecha.IdBodega
								   and prod_x_costo.IdProducto=prod_x_costo_ag_x_fecha.IdProducto
								   and prod_x_costo.IdFecha= prod_x_costo_ag_x_fecha.IdFecha
								group by 
								prod_x_costo_ag_x_fecha.IdEmpresa, prod_x_costo_ag_x_fecha.IdSucursal, prod_x_costo_ag_x_fecha.IdBodega, prod_x_costo_ag_x_fecha.IdProducto, 
								prod_x_costo_ag_x_fecha.IdFecha 



							   ) AS Ult_Reg
							    ON prod_cost_h.IdEmpresa = Ult_Reg.IdEmpresa AND 
                         prod_cost_h.IdSucursal = Ult_Reg.IdSucursal AND prod_cost_h.IdBodega = Ult_Reg.IdBodega AND prod_cost_h.IdProducto = Ult_Reg.IdProducto 
						 AND prod_cost_h.IdFecha = Ult_Reg.IdFecha 
						 AND prod_cost_h.Secuencia = Ult_Reg.Secuencia