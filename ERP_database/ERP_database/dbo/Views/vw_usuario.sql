CREATE view [dbo].[vw_usuario]
as
SELECT     A_1.IdEmpresa, A_1.IdUsuario, 0 IdPersona, '' CodPersona, B_1.Nombre as pe_nombreCompleto, B_1.Nombre as pe_razonSocial,B_1.Nombre pe_apellido, B_1.Nombre  pe_nombre,B_1.estado 
FROM         dbo.seg_usuario AS B_1 INNER JOIN
                      dbo.seg_Usuario_x_Empresa AS A_1 ON B_1.IdUsuario = A_1.IdUsuario