CREATE TABLE [dbo].[aca_MecanismoDePago] (
    [IdEmpresa]                 INT           NOT NULL,
    [IdMecanismo]               NUMERIC (18)  NOT NULL,
    [NombreMecanismo]           VARCHAR (500) NULL,
    [IdTerminoPago]             VARCHAR (20)  NOT NULL,
    [IdTipoNotaDescuentoPorRol] INT           NULL,
    [Estado]                    BIT           NULL,
    [IdUsuarioCreacion]         VARCHAR (200) NULL,
    [FechaCreacion]             DATETIME      NULL,
    [IdUsuarioModificacion]     VARCHAR (200) NULL,
    [FechaModificacion]         DATETIME      NULL,
    [IdUsuarioAnulacion]        VARCHAR (200) NULL,
    [FechaAnulacion]            DATETIME      NULL,
    [MotivoAnulacion]           VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_MecanismoDePago] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMecanismo] ASC),
    CONSTRAINT [FK_aca_MecanismoDePago_fa_TerminoPago] FOREIGN KEY ([IdTerminoPago]) REFERENCES [dbo].[fa_TerminoPago] ([IdTerminoPago]),
    CONSTRAINT [FK_aca_MecanismoDePago_fa_TipoNota] FOREIGN KEY ([IdEmpresa], [IdTipoNotaDescuentoPorRol]) REFERENCES [dbo].[fa_TipoNota] ([IdEmpresa], [IdTipoNota])
);



