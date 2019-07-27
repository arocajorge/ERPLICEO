CREATE TABLE [dbo].[tb_TarjetaCredito] (
    [IdTarjeta]       INT           NOT NULL,
    [NombreTarjeta]   VARCHAR (500) NOT NULL,
    [Estado]          BIT           NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    CONSTRAINT [PK_tb_TarjetaCredito] PRIMARY KEY CLUSTERED ([IdTarjeta] ASC)
);

