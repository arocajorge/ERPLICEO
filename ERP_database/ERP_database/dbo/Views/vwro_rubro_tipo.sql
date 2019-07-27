CREATE view vwro_rubro_tipo as 
SELECT        MAX(CAST(IdRubro AS int)) AS IdRubro, ru_descripcion, IdEmpresa
FROM            dbo.ro_rubro_tipo
GROUP BY ru_descripcion, IdEmpresa