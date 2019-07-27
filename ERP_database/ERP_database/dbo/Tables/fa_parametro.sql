CREATE TABLE [dbo].[fa_parametro] (
    [IdEmpresa]                         INT           NOT NULL,
    [IdMovi_inven_tipo_Factura]         INT           NOT NULL,
    [IdTipoCbteCble_Factura]            INT           NOT NULL,
    [IdTipoCbteCble_NC]                 INT           NOT NULL,
    [IdTipoCbteCble_ND]                 INT           NOT NULL,
    [NumeroDeItemFact]                  INT           NOT NULL,
    [IdCaja_Default_Factura]            INT           NOT NULL,
    [IdCtaCble_IVA]                     VARCHAR (20)  NULL,
    [IdCtaCble_SubTotal_Vtas_x_Default] VARCHAR (20)  NULL,
    [IdCtaCble_CXC_Vtas_x_Default]      VARCHAR (20)  NULL,
    [pa_Contabiliza_descuento]          BIT           NOT NULL,
    [pa_IdCtaCble_descuento]            VARCHAR (20)  NULL,
    [NumeroDeItemProforma]              INT           NOT NULL,
    [clave_desbloqueo_precios]          VARCHAR (200) NULL,
    [DiasTransaccionesAFuturo]          INT           NOT NULL,
    [IdClienteConsumidorFinal]          NUMERIC (18)  NULL,
    [MontoMaximoConsumidorFinal]        FLOAT (53)    NULL,
    CONSTRAINT [PK_fa_parametro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_fa_parametro_caj_Caja] FOREIGN KEY ([IdEmpresa], [IdCaja_Default_Factura]) REFERENCES [dbo].[caj_Caja] ([IdEmpresa], [IdCaja]),
    CONSTRAINT [FK_fa_parametro_ct_cbtecble_tipo] FOREIGN KEY ([IdEmpresa], [IdTipoCbteCble_Factura]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_fa_parametro_ct_cbtecble_tipo4] FOREIGN KEY ([IdEmpresa], [IdTipoCbteCble_NC]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_fa_parametro_ct_cbtecble_tipo6] FOREIGN KEY ([IdEmpresa], [IdTipoCbteCble_ND]) REFERENCES [dbo].[ct_cbtecble_tipo] ([IdEmpresa], [IdTipoCbte]),
    CONSTRAINT [FK_fa_parametro_ct_plancta1] FOREIGN KEY ([IdEmpresa], [IdCtaCble_IVA]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_fa_parametro_ct_plancta2] FOREIGN KEY ([IdEmpresa], [pa_IdCtaCble_descuento]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_fa_parametro_fa_cliente] FOREIGN KEY ([IdEmpresa], [IdClienteConsumidorFinal]) REFERENCES [dbo].[fa_cliente] ([IdEmpresa], [IdCliente]),
    CONSTRAINT [FK_fa_parametro_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);





