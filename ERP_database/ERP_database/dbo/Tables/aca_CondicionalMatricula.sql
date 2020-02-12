CREATE TABLE [dbo].[aca_CondicionalMatricula] (
    [IdEmpresa]             INT           NOT NULL,
    [IdCondicional]         NUMERIC (18)  NOT NULL,
    [IdAlumno]              NUMERIC (18)  NOT NULL,
    [IdAnio]                INT           NOT NULL,
    [IdCatalogoCONDIC]      INT           NOT NULL,
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
    CONSTRAINT [PK_aca_CondicionalMatricula] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCondicional] ASC),
    CONSTRAINT [FK_aca_CondicionalMatricula_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_CondicionalMatricula_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_CondicionalMatricula_aca_Catalogo] FOREIGN KEY ([IdCatalogoCONDIC]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo])
);

