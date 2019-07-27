CREATE TABLE [dbo].[fa_cuotas_x_doc] (
    [IdEmpresa]        INT          NOT NULL,
    [IdSucursal]       INT          NOT NULL,
    [IdBodega]         INT          NOT NULL,
    [IdCbteVta]        NUMERIC (18) NOT NULL,
    [secuencia]        INT          NOT NULL,
    [num_cuota]        INT          NOT NULL,
    [fecha_vcto_cuota] DATETIME     NOT NULL,
    [valor_a_cobrar]   FLOAT (53)   NOT NULL,
    [Estado]           BIT          NOT NULL,
    CONSTRAINT [PK_fa_cuotas_x_doc] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdCbteVta] ASC, [secuencia] ASC),
    CONSTRAINT [FK_fa_cuotas_x_doc_fa_factura] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta]) REFERENCES [dbo].[fa_factura] ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta])
);

