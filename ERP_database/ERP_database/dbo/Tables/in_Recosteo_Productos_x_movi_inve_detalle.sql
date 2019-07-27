CREATE TABLE [dbo].[in_Recosteo_Productos_x_movi_inve_detalle] (
    [IdEmpresa]   INT          NOT NULL,
    [IdSucursal]  INT          NOT NULL,
    [IdBodega]    INT          NOT NULL,
    [IdProducto]  NUMERIC (18) NOT NULL,
    [observacion] VARCHAR (50) NULL,
    CONSTRAINT [PK_in_Recosteo_Productos_x_movi_inve_detalle] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdProducto] ASC)
);

