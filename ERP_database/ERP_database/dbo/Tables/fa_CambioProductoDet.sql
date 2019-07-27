CREATE TABLE [dbo].[fa_CambioProductoDet] (
    [IdEmpresa]        INT          NOT NULL,
    [IdSucursal]       INT          NOT NULL,
    [IdBodega]         INT          NOT NULL,
    [IdCambio]         NUMERIC (18) NOT NULL,
    [Secuencia]        INT          NOT NULL,
    [IdCbteVta]        NUMERIC (18) NOT NULL,
    [SecuenciaFact]    INT          NOT NULL,
    [IdProductoFact]   NUMERIC (18) NOT NULL,
    [IdProductoCambio] NUMERIC (18) NOT NULL,
    [CantidadFact]     FLOAT (53)   NOT NULL,
    [CantidadCambio]   FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_fa_CambioProductoDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdCambio] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_fa_CambioProductoDet_fa_CambioProducto] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdCambio]) REFERENCES [dbo].[fa_CambioProducto] ([IdEmpresa], [IdSucursal], [IdBodega], [IdCambio]),
    CONSTRAINT [FK_fa_CambioProductoDet_fa_factura_det] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta], [SecuenciaFact]) REFERENCES [dbo].[fa_factura_det] ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta], [Secuencia]),
    CONSTRAINT [FK_fa_CambioProductoDet_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProductoFact]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_fa_CambioProductoDet_in_Producto1] FOREIGN KEY ([IdEmpresa], [IdProductoCambio]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_fa_CambioProductoDet_tb_bodega] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega])
);











