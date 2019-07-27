CREATE TABLE [dbo].[tb_TarjetaCredito_x_cp_proveedor] (
    [IdEmpresa]       INT          NOT NULL,
    [IdTransaccion]   INT          NOT NULL,
    [IdTarjeta]       INT          NOT NULL,
    [IdProveedor]     NUMERIC (18) NOT NULL,
    [Estado]          BIT          NOT NULL,
    [IdUsuario]       VARCHAR (20) NULL,
    [Fecha_Transac]   DATETIME     NULL,
    [IdUsuarioUltMod] VARCHAR (20) NULL,
    [Fecha_UltMod]    DATETIME     NULL,
    [IdUsuarioUltAnu] VARCHAR (20) NULL,
    [Fecha_UltAnu]    DATETIME     NULL,
    CONSTRAINT [PK_tb_TarjetaCredito_x_cp_proveedor] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTransaccion] ASC),
    CONSTRAINT [FK_tb_TarjetaCredito_x_cp_proveedor_cp_proveedor] FOREIGN KEY ([IdEmpresa], [IdProveedor]) REFERENCES [dbo].[cp_proveedor] ([IdEmpresa], [IdProveedor]),
    CONSTRAINT [FK_tb_TarjetaCredito_x_cp_proveedor_tb_TarjetaCredito] FOREIGN KEY ([IdTarjeta]) REFERENCES [dbo].[tb_TarjetaCredito] ([IdTarjeta])
);

