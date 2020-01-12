CREATE TABLE [dbo].[aca_AnioLectivo_Rubro] (
    [IdEmpresa]          INT             NOT NULL,
    [IdAnio]             INT             NOT NULL,
    [IdRubro]            INT             NOT NULL,
    [AplicaProntoPago]   BIT             NOT NULL,
    [NomRubro]           VARCHAR (1000)  NOT NULL,
    [IdProducto]         NUMERIC (18)    NOT NULL,
    [Subtotal]           NUMERIC (18, 2) NOT NULL,
    [IdCod_Impuesto_Iva] VARCHAR (50)    NOT NULL,
    [Porcentaje]         NUMERIC (18, 2) NOT NULL,
    [ValorIVA]           NUMERIC (18, 2) NOT NULL,
    [Total]              NUMERIC (18, 2) NOT NULL,
    [NumeroCuotas]       INT             NOT NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Rubro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdRubro] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_Rubro_aca_rubro] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[aca_Rubro] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_aca_AnioLectivo_Rubro_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto])
);





