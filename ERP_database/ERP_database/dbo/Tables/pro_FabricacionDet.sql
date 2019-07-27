CREATE TABLE [dbo].[pro_FabricacionDet] (
    [IdEmpresa]         INT          NOT NULL,
    [IdFabricacion]     DECIMAL (18) NOT NULL,
    [Secuencia]         INT          NOT NULL,
    [Signo]             VARCHAR (1)  NOT NULL,
    [IdProducto]        NUMERIC (18) NOT NULL,
    [IdUnidadMedida]    VARCHAR (25) NOT NULL,
    [Cantidad]          FLOAT (53)   NOT NULL,
    [Costo]             FLOAT (53)   NOT NULL,
    [RealizaMovimiento] BIT          NOT NULL,
    CONSTRAINT [PK_pro_FabricacionDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdFabricacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_pro_FabricacionDet_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_pro_FabricacionDet_in_UnidadMedida] FOREIGN KEY ([IdUnidadMedida]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida]),
    CONSTRAINT [FK_pro_FabricacionDet_pro_Fabricacion] FOREIGN KEY ([IdEmpresa], [IdFabricacion]) REFERENCES [dbo].[pro_Fabricacion] ([IdEmpresa], [IdFabricacion])
);

