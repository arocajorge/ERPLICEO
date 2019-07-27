CREATE TABLE [dbo].[in_ConsignacionDet] (
    [IdEmpresa]      INT           NOT NULL,
    [IdConsignacion] DECIMAL (18)  NOT NULL,
    [Secuencia]      INT           NOT NULL,
    [IdProducto]     NUMERIC (18)  NOT NULL,
    [IdUnidadMedida] VARCHAR (25)  NOT NULL,
    [Cantidad]       FLOAT (53)    NOT NULL,
    [Costo]          FLOAT (53)    NOT NULL,
    [Observacion]    VARCHAR (MAX) NULL,
    CONSTRAINT [PK_in_consignacion_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConsignacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_in_consignacion_det_In_consignacion] FOREIGN KEY ([IdEmpresa], [IdConsignacion]) REFERENCES [dbo].[in_Consignacion] ([IdEmpresa], [IdConsignacion]),
    CONSTRAINT [FK_in_consignacion_det_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_in_ConsignacionDet_in_UnidadMedida] FOREIGN KEY ([IdUnidadMedida]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida])
);

