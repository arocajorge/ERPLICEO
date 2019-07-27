CREATE TABLE [dbo].[imp_orden_compra_ext_det] (
    [IdEmpresa]             INT          NOT NULL,
    [IdOrdenCompra_ext]     DECIMAL (18) NOT NULL,
    [Secuencia]             INT          NOT NULL,
    [IdProducto]            NUMERIC (18) NOT NULL,
    [IdUnidadMedida]        VARCHAR (25) NOT NULL,
    [od_cantidad]           FLOAT (53)   NOT NULL,
    [od_costo]              FLOAT (53)   NOT NULL,
    [od_por_descuento]      FLOAT (53)   NOT NULL,
    [od_descuento]          FLOAT (53)   NOT NULL,
    [od_costo_final]        FLOAT (53)   NOT NULL,
    [od_subtotal]           FLOAT (53)   NOT NULL,
    [od_cantidad_recepcion] FLOAT (53)   NOT NULL,
    [od_costo_convertido]   FLOAT (53)   NOT NULL,
    [od_total_fob]          FLOAT (53)   NOT NULL,
    [od_factor_costo]       FLOAT (53)   NOT NULL,
    [od_costo_bodega]       FLOAT (53)   NOT NULL,
    [od_costo_total]        FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_imp_orden_compra_ext_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdOrdenCompra_ext] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_imp_orden_compra_ext_det_imp_orden_compra_ext] FOREIGN KEY ([IdEmpresa], [IdOrdenCompra_ext]) REFERENCES [dbo].[imp_orden_compra_ext] ([IdEmpresa], [IdOrdenCompra_ext]),
    CONSTRAINT [FK_imp_orden_compra_ext_det_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_imp_orden_compra_ext_det_in_UnidadMedida] FOREIGN KEY ([IdUnidadMedida]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida])
);

