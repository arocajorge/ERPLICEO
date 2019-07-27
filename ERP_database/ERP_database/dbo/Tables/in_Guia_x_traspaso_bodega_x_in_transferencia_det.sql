CREATE TABLE [dbo].[in_Guia_x_traspaso_bodega_x_in_transferencia_det] (
    [IdEmpresa]        INT             NOT NULL,
    [IdGuia]           NUMERIC (18)    NOT NULL,
    [IdEmpresa_tras]   INT             NOT NULL,
    [IdSucursalOrigen] INT             NOT NULL,
    [IdBodegaOrigen]   INT             NOT NULL,
    [IdTransferencia]  NUMERIC (18)    NOT NULL,
    [dt_secuencia]     INT             NOT NULL,
    [cantidad]         NUMERIC (18, 2) NOT NULL,
    [observacion]      VARCHAR (1000)  NOT NULL,
    CONSTRAINT [PK_in_Guia_x_traspaso_bodega_x_in_transferencia_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpresa_tras] ASC, [IdGuia] ASC, [IdSucursalOrigen] ASC, [IdBodegaOrigen] ASC, [IdTransferencia] ASC, [dt_secuencia] ASC),
    CONSTRAINT [FK_in_Guia_x_traspaso_bodega_x_in_transferencia_det_in_Guia_x_traspaso_bodega] FOREIGN KEY ([IdEmpresa], [IdGuia]) REFERENCES [dbo].[in_Guia_x_traspaso_bodega] ([IdEmpresa], [IdGuia]),
    CONSTRAINT [FK_in_Guia_x_traspaso_bodega_x_in_transferencia_det_in_transferencia_det] FOREIGN KEY ([IdEmpresa_tras], [IdSucursalOrigen], [IdBodegaOrigen], [IdTransferencia], [dt_secuencia]) REFERENCES [dbo].[in_transferencia_det] ([IdEmpresa], [IdSucursalOrigen], [IdBodegaOrigen], [IdTransferencia], [dt_secuencia])
);

