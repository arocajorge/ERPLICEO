CREATE view [dbo].[vwin_categoria_lin_gr_subgr]
as
select  
 isnull(ROW_NUMBER() OVER(ORDER BY cat.IdEmpresa ASC),0) AS IdRegistro
,cat.IdEmpresa   ,cat.ID ,cat.IDPadre ,cat.Codigo ,cat.descripcion
,cat.Estado		,cat.IdCategoria     ,cat.IdLinea,cat.IdGrupo,cat.IdSubGrupo  
,cat.IdNivel
from (

		SELECT        IdEmpresa, IdCategoria AS ID, NULL AS IDPadre, NULL AS Codigo, '['+IdCategoria+'] '+ca_Categoria AS descripcion, Estado, IdCategoria, NULL AS IdLinea, NULL 
								 AS IdGrupo, NULL AS IdSubGrupo, 1 AS IdNivel
		FROM            in_categorias
		UNION
		SELECT        IdEmpresa, IdCategoria + '-' + cast(IdLinea AS varchar(20)) AS ID, IdCategoria AS IDPadre, cod_linea AS Codigo, 
						'['+IdCategoria + cast(IdLinea AS varchar(20))+'] '+ nom_linea AS descripcion, Estado, IdCategoria, 
								 IdLinea, NULL AS IdGrupo, NULL AS IdSubGrupo, 2 AS IdNivel
		FROM            in_linea
		UNION
		SELECT        IdEmpresa, IdCategoria + '-' + cast(IdLinea AS varchar(20)) + '-' + cast(IdGrupo AS varchar(20)) AS ID, IdCategoria + '-' + cast(IdLinea AS varchar(20)) AS IDPadre, 
								 cod_grupo AS Codigo, '['+IdCategoria + cast(IdLinea AS varchar(20)) + cast(IdGrupo AS varchar(20))+'] '+ nom_grupo AS descripcion, Estado, IdCategoria, IdLinea, IdGrupo, NULL AS IdSubGrupo, 3 AS IdNivel
		FROM            in_grupo
		UNION
		SELECT        IdEmpresa, IdCategoria + '-' + cast(IdLinea AS varchar(20)) + '-' + cast(IdGrupo AS varchar(20)) + '-' + cast(IdSubgrupo AS varchar(20)) AS ID, 
								 IdCategoria + '-' + cast(IdLinea AS varchar(20)) + '-' + cast(IdGrupo AS varchar(20)) AS IDPadre, cod_subgrupo AS Codigo, 
								 '['+IdCategoria +  cast(IdLinea AS varchar(20)) + cast(IdGrupo AS varchar(20)) + cast(IdSubgrupo AS varchar(20))+'] '+nom_subgrupo AS descripcion, Estado, 
								 IdCategoria, IdLinea, IdGrupo, IdSubgrupo, 4 AS IdNivel
		FROM            in_subgrupo
) cat