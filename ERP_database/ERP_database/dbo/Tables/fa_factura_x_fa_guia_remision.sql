CREATE TABLE [dbo].[fa_factura_x_fa_guia_remision] (
    [fa_IdEmpresa]      INT          NOT NULL,
    [fa_IdSucursal]     INT          NOT NULL,
    [fa_IdBodega]       INT          NOT NULL,
    [fa_IdCbteVta]      NUMERIC (18) NOT NULL,
    [gi_IdEmpresa]      INT          NOT NULL,
    [gi_IdSucursal]     INT          NOT NULL,
    [gi_IdBodega]       INT          NOT NULL,
    [gi_IdGuiaRemision] NUMERIC (18) NOT NULL,
    [Observacion]       VARCHAR (50) NULL,
    CONSTRAINT [PK_fa_factura_x_fa_guia_remision_1] PRIMARY KEY CLUSTERED ([fa_IdEmpresa] ASC, [fa_IdSucursal] ASC, [fa_IdBodega] ASC, [fa_IdCbteVta] ASC, [gi_IdEmpresa] ASC, [gi_IdSucursal] ASC, [gi_IdBodega] ASC, [gi_IdGuiaRemision] ASC),
    CONSTRAINT [FK_fa_factura_x_fa_guia_remision_fa_factura] FOREIGN KEY ([fa_IdEmpresa], [fa_IdSucursal], [fa_IdBodega], [fa_IdCbteVta]) REFERENCES [dbo].[fa_factura] ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta]),
    CONSTRAINT [FK_fa_factura_x_fa_guia_remision_fa_guia_remision] FOREIGN KEY ([gi_IdEmpresa], [gi_IdSucursal], [gi_IdBodega], [gi_IdGuiaRemision]) REFERENCES [dbo].[fa_guia_remision] ([IdEmpresa], [IdSucursal], [IdBodega], [IdGuiaRemision])
);

