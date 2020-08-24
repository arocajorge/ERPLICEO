CREATE TABLE [dbo].[aca_MatriculaCalificacionCualitativa] (
    [IdEmpresa]                 INT           NOT NULL,
    [IdMatricula]               NUMERIC (18)  NOT NULL,
    [IdMateria]                 INT           NOT NULL,
    [IdCatalogoParcial]         INT           NOT NULL,
    [IdProfesor]                NUMERIC (18)  NULL,
    [IdCalificacionCualitativa] INT           NULL,
    [Conducta]                  INT           NULL,
    [MotivoConducta]            VARCHAR (MAX) NULL,
    [IdUsuarioCreacion]         VARCHAR (200) NULL,
    [FechaCreacion]             DATETIME      NULL,
    [IdUsuarioModificacion]     VARCHAR (200) NULL,
    [FechaModificacion]         DATETIME      NULL,
    CONSTRAINT [PK_aca_MatriculaCalificacionCualitativa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC, [IdMateria] ASC, [IdCatalogoParcial] ASC),
    CONSTRAINT [FK_aca_MatriculaCalificacionCualitativa_aca_AnioLectivoCalificacionCualitativa] FOREIGN KEY ([IdEmpresa], [IdCalificacionCualitativa]) REFERENCES [dbo].[aca_AnioLectivoCalificacionCualitativa] ([IdEmpresa], [IdCalificacionCualitativa]),
    CONSTRAINT [FK_aca_MatriculaCalificacionCualitativa_aca_Catalogo] FOREIGN KEY ([IdCatalogoParcial]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_aca_MatriculaCalificacionCualitativa_aca_Materia] FOREIGN KEY ([IdEmpresa], [IdMateria]) REFERENCES [dbo].[aca_Materia] ([IdEmpresa], [IdMateria]),
    CONSTRAINT [FK_aca_MatriculaCalificacionCualitativa_aca_Matricula1] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula]),
    CONSTRAINT [FK_aca_MatriculaCalificacionCualitativa_aca_Profesor] FOREIGN KEY ([IdEmpresa], [IdProfesor]) REFERENCES [dbo].[aca_Profesor] ([IdEmpresa], [IdProfesor])
);



