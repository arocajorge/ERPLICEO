CREATE TABLE [dbo].[tb_TarjetaCredito] (
    [IdEmpresa]       INT           NOT NULL,
    [IdTarjeta]       INT           NOT NULL,
    [NombreTarjeta]   VARCHAR (500) NOT NULL,
    [Estado]          BIT           NOT NULL,
    [IdBanco]         INT           NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    CONSTRAINT [PK_tb_TarjetaCredito] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTarjeta] ASC),
    CONSTRAINT [FK_tb_TarjetaCredito_tb_banco] FOREIGN KEY ([IdBanco]) REFERENCES [dbo].[tb_banco] ([IdBanco])
);





