CREATE TABLE [dbo].[aca_Familia] (
    [IdEmpresa]             INT            NOT NULL,
    [IdAlumno]              NUMERIC (18)   NOT NULL,
    [Secuencia]             INT            NOT NULL,
    [IdCatalogoPAREN]       INT            NOT NULL,
    [IdPersona]             NUMERIC (18)   NOT NULL,
    [Direccion]             VARCHAR (1000) NULL,
    [Celular]               VARCHAR (500)  NULL,
    [Correo]                VARCHAR (500)  NULL,
    [IdCatalogoFichaInst]   INT            NOT NULL,
    [EmpresaTrabajo]        VARCHAR (200)  NULL,
    [DireccionTrabajo]      VARCHAR (500)  NULL,
    [TelefonoTrabajo]       VARCHAR (200)  NULL,
    [CargoTrabajo]          VARCHAR (200)  NULL,
    [AniosServicio]         INT            NULL,
    [IngresoMensual]        FLOAT (53)     NULL,
    [VehiculoPropio]        BIT            NOT NULL,
    [Marca]                 VARCHAR (50)   NULL,
    [Modelo]                VARCHAR (50)   NULL,
    [CasaPropia]            BIT            NOT NULL,
    [SeFactura]             BIT            NOT NULL,
    [EsRepresentante]       BIT            NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200)  NULL,
    [FechaCreacion]         DATETIME       NULL,
    [IdUsuarioModificacion] VARCHAR (200)  NULL,
    [FechaModificacion]     DATETIME       NULL,
    [IdUsuarioAnulacion]    VARCHAR (200)  NULL,
    [FechaAnulacion]        DATETIME       NULL,
    [MotivoAnulacion]       VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_aca_familia] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAlumno] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_aca_familia_aca_alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_Familia_aca_CatalogoFicha] FOREIGN KEY ([IdCatalogoFichaInst]) REFERENCES [dbo].[aca_CatalogoFicha] ([IdCatalogoFicha]),
    CONSTRAINT [FK_aca_familia_tb_persona] FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[tb_persona] ([IdPersona])
);





