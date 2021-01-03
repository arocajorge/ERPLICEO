CREATE TABLE [dbo].[aca_MecanismoDePago] (
    [IdEmpresa]                 INT           NOT NULL,
    [IdMecanismo]               NUMERIC (18)  NOT NULL,
    [NombreMecanismo]           VARCHAR (500) NULL,
    [IdTerminoPago]             VARCHAR (20)  NOT NULL,
    [IdEmpresa_rol]             INT           NULL,
    [Estado]                    BIT           NULL,
    [IdUsuarioCreacion]         VARCHAR (200) NULL,
    [FechaCreacion]             DATETIME      NULL,
    [IdUsuarioModificacion]     VARCHAR (200) NULL,
    [FechaModificacion]         DATETIME      NULL,
    [IdUsuarioAnulacion]        VARCHAR (200) NULL,
    [FechaAnulacion]            DATETIME      NULL,
    [MotivoAnulacion]           VARCHAR (MAX) NULL,
    [IdTipoNotaDescuentoPorRol] INT           NULL,
    CONSTRAINT [PK_aca_MecanismoDePago] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMecanismo] ASC),
    CONSTRAINT [FK_aca_MecanismoDePago_fa_TerminoPago] FOREIGN KEY ([IdTerminoPago]) REFERENCES [dbo].[fa_TerminoPago] ([IdTerminoPago]),
    CONSTRAINT [FK_aca_MecanismoDePago_tb_empresa] FOREIGN KEY ([IdEmpresa_rol]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);







