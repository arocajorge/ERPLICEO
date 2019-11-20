CREATE TABLE [dbo].[aca_PermisoMatricula] (
    [IdEmpresa]             INT           NOT NULL,
    [IdPermiso]             NUMERIC (18)  NOT NULL,
    [IdAlumno]              NUMERIC (18)  NOT NULL,
    [IdAnio]                INT           NOT NULL,
    [IdCatalogoPERNEG]      INT           NOT NULL,
    [Fecha]                 DATE          NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [Estado]                BIT           NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_PermisoMatricula] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPermiso] ASC),
    CONSTRAINT [FK_aca_PermisoMatricula_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_PermisoMatricula_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_PermisoMatricula_aca_Catalogo] FOREIGN KEY ([IdCatalogoPERNEG]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo])
);

