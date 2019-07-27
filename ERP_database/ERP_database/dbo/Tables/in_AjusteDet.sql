CREATE TABLE [dbo].[in_AjusteDet] (
    [IdEmpresa]      INT          NOT NULL,
    [IdAjuste]       NUMERIC (18) NOT NULL,
    [Secuencia]      INT          NOT NULL,
    [IdProducto]     NUMERIC (18) NOT NULL,
    [IdUnidadMedida] VARCHAR (25) NOT NULL,
    [StockSistema]   FLOAT (53)   NOT NULL,
    [StockFisico]    FLOAT (53)   NOT NULL,
    [Ajuste]         FLOAT (53)   NOT NULL,
    [Costo]          FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_in_AjusteDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAjuste] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_in_AjusteDet_in_Ajuste] FOREIGN KEY ([IdEmpresa], [IdAjuste]) REFERENCES [dbo].[in_Ajuste] ([IdEmpresa], [IdAjuste]),
    CONSTRAINT [FK_in_AjusteDet_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_in_AjusteDet_in_UnidadMedida] FOREIGN KEY ([IdUnidadMedida]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida])
);

