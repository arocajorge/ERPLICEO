DELETE aca_Menu_x_seg_usuario
DELETE aca_Menu_x_aca_Sede
DELETE [dbo].[aca_Menu]
DELETE [dbo].[aca_Sede]

BEGIN --MENU
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (1, 3, N'Sede', 4, 1, 1, N'Academico', N'Sede', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (2, 0, N'Academico', 2, 1, 1, NULL, N'', NULL)
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (3, 2, N'Mantenimientos', 1, 1, 1, NULL, N'', NULL)
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (4, 0, N'Seguridad y Acceso', 1, 1, 1, NULL, N'', NULL)
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (5, 4, N'Mantenimientos', 1, 1, 1, NULL, N'', NULL)
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (6, 3, N'Menu', 1, 1, 1, N'Academico', N'Menu', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (7, 3, N'Menu por sede', 2, 1, 1, N'Academico', N'MenuPorSede', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (8, 3, N'Menu por sede por usuario', 3, 1, 1, N'Academico', N'MenuPorSedePorUsuario', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (9, 5, N'Usuarios', 1, 1, 1, N'SeguridadAcceso', N'Usuario', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (10, 3, N'Jornada', 6, 1, 1, N'Academico', N'Jornada', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (11, 3, N'Nivel Academico', 5, 1, 1, N'Academico', N'NivelAcademico', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (12, 3, N'Curso', 7, 1, 1, N'Academico', N'Curso', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (13, 3, N'Paralelo', 8, 1, 1, N'Academico', N'Paralelo', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (14, 3, N'Grupos de materia', 10, 1, 1, N'Academico', N'MateriaGrupo', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (15, 3, N'Materia', 11, 1, 1, N'Academico', N'Materia', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (16, 3, N'Año lectivo', 9, 1, 1, N'Academico', N'AnioLectivo', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (17, 2, N'Configuracion', 2, 1, 1, NULL, N'', NULL)
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (18, 17, N'Asignación de nivel académico por sede', 1, 1, 1, N'Academico', N'NivelPorSede', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (19, 17, N'Asignación de jornada por  nivel académico', 2, 1, 1, N'Academico', N'JornadaPorNivel', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (20, 17, N'Asignación de curso por jornada ', 3, 1, 1, N'Academico', N'CursoPorJornada', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (21, 17, N'Asignación de paralelo por curso ', 4, 1, 1, N'Academico', N'ParaleloPorCurso', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (22, 17, N'Asignación de materia por curso ', 5, 1, 1, N'Academico', N'MateriaPorCurso', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (23, 17, N'Asignación de profesor por materia', 6, 1, 1, N'Academico', N'ProfesorPorMateria', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (24, 3, N'Alumno', 12, 1, 1, N'Academico', N'Alumno', N'Index')
	INSERT [dbo].[aca_Menu] ([IdMenu], [IdMenuPadre], [DescripcionMenu], [PosicionMenu], [Estado], [nivel], [web_nom_Area], [web_nom_Controller], [web_nom_Action]) VALUES (25, 3, N'Profesor', 13, 1, 1, N'Academico', N'Profesor', N'Index')
END

BEGIN --SEDE
	INSERT [dbo].[aca_Sede] ([IdEmpresa], [IdSede], [IdSucursal], [NomSede], [Direccion], [NombreRector], [NombreSecretaria], [Estado], [IdUsuarioCreacion], [FechaCreacion], [IdUsuarioModificacion], [FechaModificacion], [IdUsuarioAnulacion], [FechaAnulacion], [MotivoAnulacion]) VALUES (1, 1, 1, N'CAMPUS 1', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
	INSERT [dbo].[aca_Sede] ([IdEmpresa], [IdSede], [IdSucursal], [NomSede], [Direccion], [NombreRector], [NombreSecretaria], [Estado], [IdUsuarioCreacion], [FechaCreacion], [IdUsuarioModificacion], [FechaModificacion], [IdUsuarioAnulacion], [FechaAnulacion], [MotivoAnulacion]) VALUES (1, 2, 1, N'CAMPUS 2', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

BEGIN --PERMISOS
	insert into aca_Menu_x_aca_Sede
	select b.IdEmpresa, b.IdSede, a.IdMenu, '' from aca_Menu a, aca_Sede b

	insert into aca_Menu_x_seg_usuario
	select a.IdEmpresa, a.IdSede, a.IdMenu, b.IdUsuario, '' from aca_Menu_x_aca_Sede a, seg_usuario b
	where b.IdUsuario = 'admin'
END