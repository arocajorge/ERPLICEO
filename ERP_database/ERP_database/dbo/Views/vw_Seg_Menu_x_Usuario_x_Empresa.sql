
CREATE view [dbo].[vw_Seg_Menu_x_Usuario_x_Empresa]
as
SELECT        M.IdMenu, M.IdMenuPadre, M.DescripcionMenu, M.PosicionMenu, M.Habilitado, M.Tiene_FormularioAsociado, M.nom_Formulario, M.nom_Asembly, M.nivel, M_x_E_x_U.IdEmpresa, M_x_E_x_U.IdUsuario, 
                                                 M_x_E_x_U.Eliminacion, M_x_E_x_U.Escritura, M_x_E_x_U.Lectura
FROM            dbo.seg_Menu AS M INNER JOIN
                         dbo.seg_Menu_x_Empresa AS M_x_E ON M.IdMenu = M_x_E.IdMenu INNER JOIN
                         dbo.seg_Menu_x_Empresa_x_Usuario AS M_x_E_x_U ON M_x_E.IdMenu = M_x_E_x_U.IdMenu AND M_x_E.IdEmpresa = M_x_E_x_U.IdEmpresa