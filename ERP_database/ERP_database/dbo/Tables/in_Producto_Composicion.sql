CREATE TABLE [dbo].[in_Producto_Composicion] (
    [IdEmpresa]       INT          NOT NULL,
    [IdProductoPadre] NUMERIC (18) NOT NULL,
    [IdProductoHijo]  NUMERIC (18) NOT NULL,
    [IdUnidadMedida]  VARCHAR (25) NOT NULL,
    [Cantidad]        FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_in_Producto_Composicion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdProductoPadre] ASC, [IdProductoHijo] ASC),
    CONSTRAINT [FK_in_Producto_Composicion_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProductoPadre]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_in_Producto_Composicion_in_Producto1] FOREIGN KEY ([IdEmpresa], [IdProductoHijo]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_in_Producto_Composicion_in_UnidadMedida] FOREIGN KEY ([IdUnidadMedida]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida])
);

