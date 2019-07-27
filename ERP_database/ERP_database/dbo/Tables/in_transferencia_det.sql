CREATE TABLE [dbo].[in_transferencia_det] (
    [IdEmpresa]                      INT            NOT NULL,
    [IdSucursalOrigen]               INT            NOT NULL,
    [IdBodegaOrigen]                 INT            NOT NULL,
    [IdTransferencia]                NUMERIC (18)   NOT NULL,
    [dt_secuencia]                   INT            NOT NULL,
    [IdProducto]                     NUMERIC (18)   NOT NULL,
    [dt_cantidad]                    FLOAT (53)     NOT NULL,
    [tr_Observacion]                 VARCHAR (MAX) NULL,
    [IdUnidadMedida]                 VARCHAR (25)   NOT NULL,
    CONSTRAINT [PK_in_transferencia_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursalOrigen] ASC, [IdBodegaOrigen] ASC, [IdTransferencia] ASC, [dt_secuencia] ASC),
    CONSTRAINT [FK_in_transferencia_det_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_in_transferencia_det_in_transferencia] FOREIGN KEY ([IdEmpresa], [IdSucursalOrigen], [IdBodegaOrigen], [IdTransferencia]) REFERENCES [dbo].[in_transferencia] ([IdEmpresa], [IdSucursalOrigen], [IdBodegaOrigen], [IdTransferencia]),
    CONSTRAINT [FK_in_transferencia_det_in_UnidadMedida] FOREIGN KEY ([IdUnidadMedida]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida])
);

