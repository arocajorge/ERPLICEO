CREATE VIEW [dbo].[vwAf_Depreciacion] AS 
SELECT        depre.IdEmpresa, depre.IdDepreciacion,  depre.Cod_Depreciacion, depre.IdPeriodo, depre.Descripcion, depre.Fecha_Depreciacion, 
                         depre.IdUsuario, sum(depre_Det.Valor_Depreciacion) AS Valor_Depreciacion , 
                         sum(depre_Det.Valor_Depre_Acum) as Valor_Depre_Acum, depre.Estado
FROM            dbo.Af_Depreciacion AS depre INNER JOIN
                         dbo.Af_Depreciacion_Det AS depre_Det ON depre.IdEmpresa = depre_Det.IdEmpresa AND depre.IdDepreciacion = depre_Det.IdDepreciacion
                         
GROUP BY depre.IdEmpresa, depre.IdDepreciacion, depre.Cod_Depreciacion, depre.IdPeriodo, depre.Descripcion, depre.Fecha_Depreciacion, 
                         depre.IdUsuario, depre.Estado
