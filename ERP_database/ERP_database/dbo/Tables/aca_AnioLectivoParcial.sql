CREATE TABLE [dbo].[aca_AnioLectivoParcial] (
    [IdEmpresa]             INT           NOT NULL,
    [IdAnio]                INT           NOT NULL,
    [IdSede]                INT           NOT NULL,
    [IdCatalogoParcial]     INT           NOT NULL,
    [EsExamen]              BIT           NULL,
    [FechaInicio]           DATETIME      NULL,
    [FechaFin]              DATETIME      NULL,
    [ValidaEstadoAlumno]    BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_AnioLectivoParcial] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [IdCatalogoParcial] ASC),
    CONSTRAINT [FK_aca_AnioLectivoParcial_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_AnioLectivoParcial_aca_Catalogo] FOREIGN KEY ([IdCatalogoParcial]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_aca_AnioLectivoParcial_aca_Sede] FOREIGN KEY ([IdEmpresa], [IdSede]) REFERENCES [dbo].[aca_Sede] ([IdEmpresa], [IdSede])
);







