CREATE TABLE [dbo].[aca_Plantilla_Rubro] (
    [IdEmpresa]          INT             NOT NULL,
    [IdAnio]             INT             NOT NULL,
    [IdPlantilla]        INT             NOT NULL,
    [IdRubro]            INT             NOT NULL,
    [IdProducto]         NUMERIC (18)    NOT NULL,
    [Subtotal]           NUMERIC (18, 2) NOT NULL,
    [IdCod_Impuesto_Iva] VARCHAR (50)    NOT NULL,
    [Porcentaje]         NUMERIC (18, 2) NOT NULL,
    [Total]              NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_aca_Plantilla_Rubro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdPlantilla] ASC, [IdRubro] ASC),
    CONSTRAINT [FK_aca_Plantilla_Rubro_aca_Plantilla] FOREIGN KEY ([IdEmpresa], [IdAnio], [IdPlantilla]) REFERENCES [dbo].[aca_Plantilla] ([IdEmpresa], [IdAnio], [IdPlantilla])
);

