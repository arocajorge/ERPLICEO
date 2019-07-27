CREATE view vwRo_Parametros_Contables as
SELECT        dbo.ro_area.IdEmpresa, dbo.ro_Config_Param_contable.IdDivision, dbo.ro_Config_Param_contable.IdArea, dbo.ro_Config_Param_contable.IdDepartamento, 
                         dbo.ro_Config_Param_contable.IdRubro, dbo.ro_Config_Param_contable.IdCtaCble, dbo.ro_Config_Param_contable.IdCentroCosto, 
                         dbo.ro_Config_Param_contable.DebCre, dbo.ro_Division.Descripcion AS DescripcionDiv, dbo.ro_area.Descripcion AS DescripcionArea, 
                         dbo.ro_Departamento.de_descripcion, dbo.ro_rubro_tipo.ru_descripcion, dbo.ro_rubro_tipo.ru_estado, dbo.ro_rubro_tipo.ru_tipo, dbo.ro_rubro_tipo.rub_nocontab, 
                         dbo.ro_rubro_tipo.rub_ctacon, dbo.ro_rubro_tipo.rub_provision,  dbo.ro_rubro_tipo.rub_aplica_IESS,
                           dbo.ro_rubro_tipo.rub_grupo
                        
FROM            dbo.ro_Division INNER JOIN
                         dbo.ro_area ON dbo.ro_Division.IdEmpresa = dbo.ro_area.IdEmpresa AND dbo.ro_Division.IdDivision = dbo.ro_area.IdDivision INNER JOIN
                         dbo.ro_Config_Param_contable ON dbo.ro_area.IdEmpresa = dbo.ro_Config_Param_contable.IdEmpresa AND 
                         dbo.ro_area.IdDivision = dbo.ro_Config_Param_contable.IdDivision AND dbo.ro_area.IdArea = dbo.ro_Config_Param_contable.IdArea INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_Config_Param_contable.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND 
                         dbo.ro_Config_Param_contable.IdDepartamento = dbo.ro_Departamento.IdDepartamento INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_Config_Param_contable.IdRubro = dbo.ro_rubro_tipo.IdRubro
