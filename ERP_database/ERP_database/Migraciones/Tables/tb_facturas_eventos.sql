CREATE TABLE [Migraciones].[tb_facturas_eventos] (
    [IdEmpresa]       INT             NOT NULL,
    [Establecimiento] VARCHAR (5)     NOT NULL,
    [Puntoemision]    VARCHAR (5)     NOT NULL,
    [NumFactura]      NUMERIC (18)    NOT NULL,
    [Cantidad]        DECIMAL (18, 2) NULL,
    [ValorUnitario]   DECIMAL (18, 2) NULL,
    [Subtotal]        DECIMAL (18, 2) NULL,
    [Iva]             DECIMAL (18, 2) NULL,
    [Total]           DECIMAL (18, 2) NULL,
    [Evento]          INT             NOT NULL,
    [Factura]         INT             NOT NULL,
    CONSTRAINT [PK_tb_facturas_eventos] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [Evento] ASC, [Factura] ASC)
);

