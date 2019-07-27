CREATE TABLE [dbo].[in_Guia_x_traspaso_bodega_det_sin_oc] (
    [IdEmpresa]       INT            NOT NULL,
    [IdGuia]          NUMERIC (18)   NOT NULL,
    [secuencia]       INT            NOT NULL,
    [Num_Fact]        VARCHAR (50)   NULL,
    [IdProveedor]     NUMERIC (18)   NULL,
    [nom_proveedor]   VARCHAR (250)  NULL,
    [IdProducto]      NUMERIC (18)   NULL,
    [nom_producto]    VARCHAR (250)  NULL,
    [Cantidad_enviar] FLOAT (53)     NOT NULL,
    [observacion]     VARCHAR (1000) NOT NULL,
    CONSTRAINT [PK_in_Guia_x_traspaso_bodega_det_sin_oc] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdGuia] ASC, [secuencia] ASC),
    CONSTRAINT [FK_in_Guia_x_traspaso_bodega_det_sin_oc_cp_proveedor] FOREIGN KEY ([IdEmpresa], [IdProveedor]) REFERENCES [dbo].[cp_proveedor] ([IdEmpresa], [IdProveedor]),
    CONSTRAINT [FK_in_Guia_x_traspaso_bodega_det_sin_oc_in_Guia_x_traspaso_bodega] FOREIGN KEY ([IdEmpresa], [IdGuia]) REFERENCES [dbo].[in_Guia_x_traspaso_bodega] ([IdEmpresa], [IdGuia]),
    CONSTRAINT [FK_in_Guia_x_traspaso_bodega_det_sin_oc_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto])
);

