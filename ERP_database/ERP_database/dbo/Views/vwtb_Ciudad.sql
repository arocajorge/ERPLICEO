
CREATE VIEW [dbo].[vwtb_Ciudad] AS
SELECT       ciu.IdCiudad, ciu.Cod_Ciudad , ciu.Descripcion_Ciudad, ciu.Estado, 
                       pro.IdPais , ciu.IdProvincia 
FROM            tb_ciudad ciu INNER JOIN
                         tb_provincia pro ON ciu.IdProvincia = pro.IdProvincia