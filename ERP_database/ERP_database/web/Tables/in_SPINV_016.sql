CREATE TABLE [web].[in_SPINV_016] (
    [IdEmpresa]         INT          NOT NULL,
    [IdSucursal]        INT          NOT NULL,
    [IdProducto]        INT          NOT NULL,
    [IdUsuario]         VARCHAR (50) NOT NULL,
    [SaldoInicial]      FLOAT (53)   NOT NULL,
    [CantidadIngresada] FLOAT (53)   NOT NULL,
    [CantidadVendida]   FLOAT (53)   NOT NULL,
    [CostoPromedio]     FLOAT (53)   NOT NULL,
    [CostoTotal]        FLOAT (53)   NOT NULL,
    [Stock]             FLOAT (53)   NOT NULL,
    [PrecioVenta]       FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_in_SPINV_016] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdProducto] ASC, [IdUsuario] ASC)
);

