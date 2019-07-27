CREATE TABLE [dbo].[fa_factura_resumen] (
    [IdEmpresa]              INT             NOT NULL,
    [IdSucursal]             INT             NOT NULL,
    [IdBodega]               INT             NOT NULL,
    [IdCbteVta]              NUMERIC (18)    NOT NULL,
    [SubtotalIVASinDscto]    NUMERIC (18, 2) NOT NULL,
    [SubtotalSinIVASinDscto] NUMERIC (18, 2) NOT NULL,
    [SubtotalSinDscto]       NUMERIC (18, 2) NOT NULL,
    [Descuento]              NUMERIC (18, 2) NOT NULL,
    [SubtotalIVAConDscto]    NUMERIC (18, 2) NOT NULL,
    [SubtotalSinIVAConDscto] NUMERIC (18, 2) NOT NULL,
    [SubtotalConDscto]       NUMERIC (18, 2) NOT NULL,
    [ValorIVA]               NUMERIC (18, 2) NOT NULL,
    [Total]                  NUMERIC (18, 2) NOT NULL,
    [ValorEfectivo]          NUMERIC (18, 2) NOT NULL,
    [Cambio]                 NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_fa_factura_resumen] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdCbteVta] ASC)
);

