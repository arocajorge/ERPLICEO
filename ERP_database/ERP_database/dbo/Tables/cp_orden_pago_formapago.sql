CREATE TABLE [dbo].[cp_orden_pago_formapago] (
    [IdFormaPago]       VARCHAR (20) NOT NULL,
    [descripcion]       VARCHAR (50) NOT NULL,
    [IdTipoTransaccion] VARCHAR (20) NULL,
    [CodModulo]         VARCHAR (20) NULL,
    [IdTipoMovi_caj]    INT          NULL,
    CONSTRAINT [PK_cp_orden_pago_tipopago] PRIMARY KEY CLUSTERED ([IdFormaPago] ASC),
    CONSTRAINT [FK_cp_orden_pago_formapago_tb_modulo] FOREIGN KEY ([CodModulo]) REFERENCES [dbo].[tb_modulo] ([CodModulo])
);



