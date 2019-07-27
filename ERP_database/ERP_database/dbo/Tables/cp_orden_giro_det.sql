CREATE TABLE [dbo].[cp_orden_giro_det] (
    [IdEmpresa]          INT          NOT NULL,
    [IdCbteCble_Ogiro]   NUMERIC (18) NOT NULL,
    [IdTipoCbte_Ogiro]   INT          NOT NULL,
    [Secuencia]          INT          NOT NULL,
    [IdProducto]         NUMERIC (18) NOT NULL,
    [IdUnidadMedida]     VARCHAR (25) NOT NULL,
    [Cantidad]           FLOAT (53)   NOT NULL,
    [CostoUni]           FLOAT (53)   NOT NULL,
    [PorDescuento]       FLOAT (53)   NOT NULL,
    [DescuentoUni]       FLOAT (53)   NOT NULL,
    [CostoUniFinal]      FLOAT (53)   NOT NULL,
    [Subtotal]           FLOAT (53)   NOT NULL,
    [IdCod_Impuesto_Iva] VARCHAR (25) NOT NULL,
    [PorIva]             FLOAT (53)   NOT NULL,
    [ValorIva]           FLOAT (53)   NOT NULL,
    [Total]              FLOAT (53)   NOT NULL,
    [IdCtaCbleInv]       VARCHAR (20) NULL,
    [IdEmpresa_oc]       INT          NULL,
    [IdSucursal_oc]      INT          NULL,
    [IdOrdenCompra]      NUMERIC (18) NULL,
    [Secuencia_oc]       INT          NULL,
    CONSTRAINT [PK_cp_orden_giro_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCbteCble_Ogiro] ASC, [IdTipoCbte_Ogiro] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_orden_giro_det_com_ordencompra_local_det] FOREIGN KEY ([IdEmpresa_oc], [IdSucursal_oc], [IdOrdenCompra], [Secuencia_oc]) REFERENCES [dbo].[com_ordencompra_local_det] ([IdEmpresa], [IdSucursal], [IdOrdenCompra], [Secuencia]),
    CONSTRAINT [FK_cp_orden_giro_det_cp_orden_giro] FOREIGN KEY ([IdEmpresa], [IdCbteCble_Ogiro], [IdTipoCbte_Ogiro]) REFERENCES [dbo].[cp_orden_giro] ([IdEmpresa], [IdCbteCble_Ogiro], [IdTipoCbte_Ogiro]),
    CONSTRAINT [FK_cp_orden_giro_det_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCbleInv]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_cp_orden_giro_det_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto])
);







