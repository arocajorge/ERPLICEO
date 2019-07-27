
CREATE view  [dbo].[vwro_periodo_x_ro_Nomina_TipoLiqui_Asignado]
as
SELECT     
B.IdEmpresa, B.IdNomina_Tipo, B.IdNomina_TipoLiqui, C.IdPeriodo, C.Cerrado, C.Procesado, C.Contabilizado
,'AsignadoPeriodo' as Tipo
FROM         ro_Nomina_Tipo AS A INNER JOIN
                      ro_Nomina_Tipoliqui AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdNomina_Tipo = B.IdNomina_Tipo INNER JOIN
                      ro_periodo_x_ro_Nomina_TipoLiqui AS C ON B.IdEmpresa = C.IdEmpresa AND B.IdNomina_Tipo = C.IdNomina_Tipo AND 
                      B.IdNomina_TipoLiqui = C.IdNomina_TipoLiqui

union                      
SELECT     
B.IdEmpresa, B.IdNomina_Tipo, B.IdNomina_TipoLiqui, pe.IdPeriodo ,'N','N','N','NO_AsignadoPeriodo' AS Tipo
FROM         ro_Nomina_Tipo AS A INNER JOIN
                      ro_Nomina_Tipoliqui AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdNomina_Tipo = B.IdNomina_Tipo INNER JOIN
                      ro_periodo AS pe ON B.IdEmpresa = pe.IdEmpresa
where                       
cast(B.IdEmpresa as varchar(20))+ '-'+ cast(B.IdNomina_Tipo as varchar(20)) + '-' + CAST( B.IdNomina_TipoLiqui as varchar(20)) + '-' + CAST( pe.IdPeriodo as varchar(20))
not in
(
	SELECT     
	cast(B.IdEmpresa as varchar(20))+'-'+ cast(B.IdNomina_Tipo as varchar(20)) + '-' + cast(B.IdNomina_TipoLiqui as varchar(20)) + '-' +  cast(C.IdPeriodo as varchar(20))
	FROM         ro_Nomina_Tipo AS A INNER JOIN
						  ro_Nomina_Tipoliqui AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdNomina_Tipo = B.IdNomina_Tipo INNER JOIN
						  ro_periodo_x_ro_Nomina_TipoLiqui AS C ON B.IdEmpresa = C.IdEmpresa AND B.IdNomina_Tipo = C.IdNomina_Tipo AND 
						  B.IdNomina_TipoLiqui = C.IdNomina_TipoLiqui
)