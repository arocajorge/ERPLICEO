CREATE TABLE [dbo].[in_Producto_imagenes] (
    [IdEmpresa]  INT          NOT NULL,
    [IdProducto] NUMERIC (18) NOT NULL,
    [Secuencia]  INT          NOT NULL,
    [Imagen]     IMAGE        NOT NULL,
    CONSTRAINT [PK_in_Producto_imagenes] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdProducto] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_in_Producto_imagenes_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto])
);

