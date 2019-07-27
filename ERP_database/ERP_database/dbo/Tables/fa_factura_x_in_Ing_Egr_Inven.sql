CREATE TABLE [dbo].[fa_factura_x_in_Ing_Egr_Inven] (
    [IdEmpresa_fa]                  INT          NOT NULL,
    [IdSucursal_fa]                 INT          NOT NULL,
    [IdBodega_fa]                   INT          NOT NULL,
    [IdCbteVta_fa]                  NUMERIC (18) NOT NULL,
    [IdEmpresa_in_eg_x_inv]         INT          NOT NULL,
    [IdSucursal_in_eg_x_inv]        INT          NOT NULL,
    [IdMovi_inven_tipo_in_eg_x_inv] INT          NOT NULL,
    [IdNumMovi_in_eg_x_inv]         NUMERIC (18) NOT NULL,
    [observacion]                   VARCHAR (50) NULL,
    CONSTRAINT [PK_fa_factura_x_in_Ing_Egr_Inven] PRIMARY KEY CLUSTERED ([IdEmpresa_fa] ASC, [IdSucursal_fa] ASC, [IdBodega_fa] ASC, [IdCbteVta_fa] ASC, [IdEmpresa_in_eg_x_inv] ASC, [IdSucursal_in_eg_x_inv] ASC, [IdMovi_inven_tipo_in_eg_x_inv] ASC, [IdNumMovi_in_eg_x_inv] ASC),
    CONSTRAINT [FK_fa_factura_x_in_Ing_Egr_Inven_fa_factura] FOREIGN KEY ([IdEmpresa_fa], [IdSucursal_fa], [IdBodega_fa], [IdCbteVta_fa]) REFERENCES [dbo].[fa_factura] ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta]),
    CONSTRAINT [FK_fa_factura_x_in_Ing_Egr_Inven_in_Ing_Egr_Inven] FOREIGN KEY ([IdEmpresa_in_eg_x_inv], [IdSucursal_in_eg_x_inv], [IdMovi_inven_tipo_in_eg_x_inv], [IdNumMovi_in_eg_x_inv]) REFERENCES [dbo].[in_Ing_Egr_Inven] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi])
);

