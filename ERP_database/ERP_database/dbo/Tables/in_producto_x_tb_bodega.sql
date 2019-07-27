CREATE TABLE [dbo].[in_producto_x_tb_bodega] (
    [IdEmpresa]             INT          NOT NULL,
    [IdSucursal]            INT          NOT NULL,
    [IdBodega]              INT          NOT NULL,
    [IdProducto]            NUMERIC (18) NOT NULL,
    [IdCtaCble_Inven]       VARCHAR (20) NULL,
    [IdCtaCble_Costo]       VARCHAR (20) NULL,
    [IdCtaCble_Gasto_x_cxp] VARCHAR (20) NULL,
    [IdCtaCble_Vta]         VARCHAR (20) NULL,
    [Stock_minimo]          FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_in_producto_x_tb_puntoVenta] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdProducto] ASC),
    CONSTRAINT [FK_in_producto_x_tb_bodega_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Inven]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_in_producto_x_tb_bodega_ct_plancta10] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Gasto_x_cxp]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_in_producto_x_tb_bodega_ct_plancta11] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Vta]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_in_producto_x_tb_bodega_ct_plancta9] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Costo]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_in_producto_x_tb_bodega_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_in_producto_x_tb_bodega_tb_bodega] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega])
);



