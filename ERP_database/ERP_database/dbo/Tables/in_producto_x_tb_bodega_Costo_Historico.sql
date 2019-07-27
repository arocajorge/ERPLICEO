CREATE TABLE [dbo].[in_producto_x_tb_bodega_Costo_Historico] (
    [IdEmpresa]        INT          NOT NULL,
    [IdSucursal]       INT          NOT NULL,
    [IdBodega]         INT          NOT NULL,
    [IdProducto]       NUMERIC (18) NOT NULL,
    [IdFecha]          INT          NOT NULL,
    [Secuencia]        INT          NOT NULL,
    [fecha]            DATE         NOT NULL,
    [costo]            FLOAT (53)   NOT NULL,
    [Stock_a_la_fecha] FLOAT (53)   NOT NULL,
    [Observacion]      VARCHAR (50) NOT NULL,
    [fecha_trans]      DATETIME     NULL,
    CONSTRAINT [PK_in_producto_x_tb_bodega_Costo_Historico_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdProducto] ASC, [IdFecha] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_in_producto_x_tb_bodega_Costo_Historico_in_producto_x_tb_bodega] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdProducto]) REFERENCES [dbo].[in_producto_x_tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega], [IdProducto])
);

