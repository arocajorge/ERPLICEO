

CREATE VIEW [dbo].[vwAf_Mej_Baj_Activo]
AS
SELECT        af.IdEmpresa, af.Id_Mejora_Baja_Activo, af.Id_Tipo, act.IdActivoFijo, act.Af_Nombre, '' AS NomCompleto, pro.IdProveedor, '' pr_nombre, 
                         af.ValorActivo, af.Valor_Mej_Baj_Activo, af.Compr_Mej_Baj, af.DescripcionTecnica, af.Motivo, af.Estado, af.Fecha_Transac
FROM            dbo.Af_Mej_Baj_Activo AS af INNER JOIN
                         dbo.Af_Activo_fijo AS act ON af.IdEmpresa = act.IdEmpresa AND af.IdActivoFijo = act.IdActivoFijo INNER JOIN
                         dbo.cp_proveedor AS pro ON pro.IdEmpresa = af.IdEmpresa AND pro.IdProveedor = 0