CREATE TABLE [dbo].[in_Guia_x_traspaso_bodega_det] (
    [IdEmpresa]        INT            NOT NULL,
    [IdGuia]           NUMERIC (18)   NOT NULL,
    [secuencia]        INT            NOT NULL,
    [IdEmpresa_OC]     INT            NULL,
    [IdSucursal_OC]    INT            NULL,
    [IdOrdenCompra_OC] NUMERIC (18)   NULL,
    [Secuencia_OC]     INT            NULL,
    [observacion]      VARCHAR (1000) NULL,
    [Cantidad_enviar]  FLOAT (53)     NULL,
    [Referencia]       VARCHAR (50)   NULL,
    CONSTRAINT [PK_in_Guia_x_traspaso_bodega_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdGuia] ASC, [secuencia] ASC),
    CONSTRAINT [FK_in_Guia_x_traspaso_bodega_det_com_ordencompra_local_det] FOREIGN KEY ([IdEmpresa_OC], [IdSucursal_OC], [IdOrdenCompra_OC], [Secuencia_OC]) REFERENCES [dbo].[com_ordencompra_local_det] ([IdEmpresa], [IdSucursal], [IdOrdenCompra], [Secuencia]),
    CONSTRAINT [FK_in_Guia_x_traspaso_bodega_det_in_Guia_x_traspaso_bodega] FOREIGN KEY ([IdEmpresa], [IdGuia]) REFERENCES [dbo].[in_Guia_x_traspaso_bodega] ([IdEmpresa], [IdGuia])
);

