CREATE VIEW [dbo].[vwtb_Bodega_x_Sucursal_TreeList] as
SELECT       IdEmpresa, 'SUC-' + CAST(IdSucursal AS VARCHAR(5)) AS Id, NULL AS IdPadre, Su_Descripcion AS Nombre, Estado AS Estado, 1 AS Nivel, IdSucursal AS IdSucursal, NULL AS IdBodega
FROM            tb_sucursal
UNION
SELECT		 IdEmpresa, 'BOD-' + CAST(IdSucursal AS VARCHAR(5)) + '-' + CAST(IdBodega AS VARCHAR(5)) AS Id,'SUC-' + CAST(IdSucursal AS VARCHAR(5))  AS IdPadre, bo_Descripcion AS Nombre, Estado  AS Estado, 2 AS Nivel, IdSucursal AS IdSucursal, IdBodega AS IdBodega
FROM	tb_bodega