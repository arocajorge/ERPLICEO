create view [dbo].[vwin_Cate_Lin_Grup_SubGrup] AS
SELECT  dbo.in_subgrupo.IdEmpresa , dbo.in_subgrupo.IdCategoria, dbo.in_subgrupo.IdLinea, dbo.in_subgrupo.IdGrupo, dbo.in_subgrupo.IdSubgrupo,
		dbo.in_categorias.ca_Categoria, dbo.in_linea.nom_linea, dbo.in_grupo.nom_grupo, dbo.in_subgrupo .nom_subgrupo
FROM dbo.in_subgrupo INNER JOIN dbo.in_grupo ON dbo.in_grupo.IdCategoria = dbo.in_subgrupo.IdCategoria AND dbo.in_grupo.IdLinea = dbo.in_subgrupo.IdLinea AND
	 dbo.in_grupo.IdGrupo = dbo.in_subgrupo.IdGrupo AND dbo.in_grupo.IdEmpresa = dbo.in_subgrupo.IdEmpresa INNER JOIN 
	 dbo.in_linea ON dbo.in_grupo.IdCategoria = dbo.in_linea.IdCategoria AND dbo.in_grupo.IdLinea = dbo.in_linea.IdLinea  AND dbo.in_grupo.IdEmpresa = dbo.in_linea.IdEmpresa INNER JOIN 
	 dbo.in_categorias ON dbo.in_categorias.IdEmpresa = dbo.in_linea.IdEmpresa AND dbo.in_categorias.IdCategoria = dbo.in_linea.IdCategoria