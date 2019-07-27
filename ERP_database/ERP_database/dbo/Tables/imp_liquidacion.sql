CREATE TABLE [dbo].[imp_liquidacion] (
    [IdEmpresa]              INT           NOT NULL,
    [IdLiquidacion]          NUMERIC (18)  NOT NULL,
    [IdOrdenCompra_ext]      DECIMAL (18)  NOT NULL,
    [li_num_documento]       VARCHAR (100) NULL,
    [li_codigo]              VARCHAR (30)  NULL,
    [li_num_DAU]             VARCHAR (50)  NULL,
    [li_fecha]               DATETIME      NOT NULL,
    [li_observacion]         VARCHAR (500) NULL,
    [estado]                 BIT           NOT NULL,
    [IdEmpresa_inv]          INT           NULL,
    [IdSucursal_inv]         INT           NULL,
    [IdMovi_inven_tipo_inv]  INT           NULL,
    [IdNumMovi_inv]          NUMERIC (18)  NULL,
    [IdEmpresa_ct]           INT           NULL,
    [IdTipoCbte_ct]          INT           NULL,
    [IdCbteCble_ct]          NUMERIC (18)  NULL,
    [IdBodega_inv]           INT           NULL,
    [IdUsuario_creacion]     VARCHAR (20)  NULL,
    [fecha_creacion]         DATETIME      NULL,
    [IdUsuario_modificacion] VARCHAR (20)  NULL,
    [fecha_modificacion]     DATETIME      NULL,
    [IdUsuario_anulacion]    VARCHAR (20)  NULL,
    [fecha_anulacion]        DATETIME      NULL,
    CONSTRAINT [PK_imp_liquidacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdLiquidacion] ASC),
    CONSTRAINT [FK_imp_liquidacion_imp_orden_compra_ext] FOREIGN KEY ([IdEmpresa], [IdOrdenCompra_ext]) REFERENCES [dbo].[imp_orden_compra_ext] ([IdEmpresa], [IdOrdenCompra_ext])
);





