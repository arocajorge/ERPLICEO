CREATE TABLE [dbo].[in_Producto_x_fa_NivelDescuento] (
    [IdEmpresa]  INT          NOT NULL,
    [IdProducto] NUMERIC (18) NOT NULL,
    [IdNivel]    INT          NOT NULL,
    [Secuencia]  INT          NOT NULL,
    [Porcentaje] FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_in_Producto_x_fa_NivelDescuento] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdProducto] ASC, [IdNivel] ASC),
    CONSTRAINT [FK_in_Producto_x_fa_NivelDescuento_fa_NivelDescuento] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[fa_NivelDescuento] ([IdEmpresa], [IdNivel]),
    CONSTRAINT [FK_in_Producto_x_fa_NivelDescuento_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto])
);

