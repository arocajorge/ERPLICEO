CREATE VIEW [dbo].[vwRo_Departamento_X_Area]
AS
SELECT        dbo.ro_area_x_departamento.IdEmpresa, dbo.ro_area_x_departamento.IdDivision, dbo.ro_area_x_departamento.IdArea, 
                         dbo.ro_area_x_departamento.IdDepartamento,  dbo.ro_Departamento.de_descripcion, dbo.ro_Departamento.IdUsuario, 
                         dbo.ro_Departamento.Fecha_Transac, dbo.ro_Departamento.IdUsuarioUltMod, dbo.ro_Departamento.Fecha_UltMod, dbo.ro_Departamento.IdUsuarioUltAnu, 
                         dbo.ro_Departamento.Fecha_UltAnu, dbo.ro_Departamento.nom_pc, dbo.ro_Departamento.ip, dbo.ro_Departamento.Estado, dbo.ro_Departamento.MotiAnula
FROM            dbo.ro_area_x_departamento INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_area_x_departamento.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND 
                         dbo.ro_area_x_departamento.IdDepartamento = dbo.ro_Departamento.IdDepartamento
