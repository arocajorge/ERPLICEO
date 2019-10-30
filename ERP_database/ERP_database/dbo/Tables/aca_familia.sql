CREATE TABLE [dbo].[aca_familia] (
    [IdEmpresa]             INT            NOT NULL,
    [IdAlumno]              NUMERIC (18)   NOT NULL,
    [IdCatalogoPAREN]       INT            NOT NULL,
    [IdPersona]             NUMERIC (18)   NOT NULL,
    [Direccion]             VARCHAR (1000) NULL,
    [Celular]               VARCHAR (500)  NULL,
    [Correo]                VARCHAR (500)  NULL,
    [SeFactura]             BIT            NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200)  NULL,
    [FechaCreacion]         DATETIME       NULL,
    [IdUsuarioModificacion] VARCHAR (200)  NULL,
    [FechaModificacion]     DATETIME       NULL,
    [IdUsuarioAnulacion]    VARCHAR (200)  NULL,
    [FechaAnulacion]        DATETIME       NULL,
    [MotivoAnulacion]       VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_aca_familia] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAlumno] ASC, [IdCatalogoPAREN] ASC),
    CONSTRAINT [FK_aca_familia_aca_alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_familia_tb_persona] FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[tb_persona] ([IdPersona])
);

