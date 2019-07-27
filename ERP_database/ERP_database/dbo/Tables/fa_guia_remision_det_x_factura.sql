CREATE TABLE [dbo].[fa_guia_remision_det_x_factura] (
    [IdEmpresa_guia]      INT          NOT NULL,
    [IdSucursal_guia]     INT          NOT NULL,
    [IdBodega_guia]       INT          NOT NULL,
    [IdGuiaRemision_guia] NUMERIC (18) NOT NULL,
    [Secuencia_guia]      INT          NOT NULL,
    [IdEmpresa_fact]      INT          NOT NULL,
    [IdSucursal_fact]     INT          NOT NULL,
    [IdBodega_fact]       INT          NOT NULL,
    [IdCbteVta_fact]      NUMERIC (18) NOT NULL,
    [Secuencia_fact]      INT          NOT NULL,
    [observacion]         VARCHAR (50) NULL,
    CONSTRAINT [PK_fa_guia_remision_det_x_fa_factura_det] PRIMARY KEY CLUSTERED ([IdEmpresa_guia] ASC, [IdSucursal_guia] ASC, [IdBodega_guia] ASC, [IdGuiaRemision_guia] ASC, [Secuencia_guia] ASC, [IdEmpresa_fact] ASC, [IdSucursal_fact] ASC, [IdBodega_fact] ASC, [IdCbteVta_fact] ASC, [Secuencia_fact] ASC),
    CONSTRAINT [FK_fa_guia_remision_det_x_fa_factura_det_fa_factura_det] FOREIGN KEY ([IdEmpresa_fact], [IdSucursal_fact], [IdBodega_fact], [IdCbteVta_fact], [Secuencia_fact]) REFERENCES [dbo].[fa_factura_det] ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta], [Secuencia]),
    CONSTRAINT [FK_fa_guia_remision_det_x_fa_factura_det_fa_guia_remision_det] FOREIGN KEY ([IdEmpresa_guia], [IdSucursal_guia], [IdBodega_guia], [IdGuiaRemision_guia], [Secuencia_guia]) REFERENCES [dbo].[fa_guia_remision_det] ([IdEmpresa], [IdSucursal], [IdBodega], [IdGuiaRemision], [Secuencia])
);

