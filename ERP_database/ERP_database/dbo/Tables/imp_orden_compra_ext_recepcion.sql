CREATE TABLE [dbo].[imp_orden_compra_ext_recepcion] (
    [IdEmpresa]              INT            NOT NULL,
    [IdRecepcion]            DECIMAL (18)   NOT NULL,
    [or_fecha]               DATETIME       NOT NULL,
    [or_observacion]         VARCHAR (1000) NULL,
    [IdEmpresa_oc]           INT            NULL,
    [IdOrdenCompraExt]       DECIMAL (18)   NOT NULL,
    [estado]                 BIT            NOT NULL,
    [IdUsuario_creacion]     VARCHAR (20)   NULL,
    [fecha_creacion]         DATETIME       NULL,
    [IdUsuario_modificacion] VARCHAR (20)   NULL,
    [fecha_modificacion]     DATETIME       NULL,
    [IdUsuario_anulacion]    VARCHAR (20)   NULL,
    [fecha_anulacion]        DATETIME       NULL,
    CONSTRAINT [PK_imp_orden_compra_ext_recepcion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRecepcion] ASC),
    CONSTRAINT [FK_imp_orden_compra_ext_recepcion_imp_orden_compra_ext] FOREIGN KEY ([IdEmpresa_oc], [IdOrdenCompraExt]) REFERENCES [dbo].[imp_orden_compra_ext] ([IdEmpresa], [IdOrdenCompra_ext])
);

