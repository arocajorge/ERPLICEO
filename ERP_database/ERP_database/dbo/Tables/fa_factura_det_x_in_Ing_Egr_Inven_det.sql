CREATE TABLE [dbo].[fa_factura_det_x_in_Ing_Egr_Inven_det] (
    [IdEmpresa_fa]         INT          NOT NULL,
    [IdSucursal_fa]        INT          NOT NULL,
    [IdBodega_fa]          INT          NOT NULL,
    [IdCbteVta_fa]         NUMERIC (18) NOT NULL,
    [Secuencia_fa]         INT          NOT NULL,
    [IdEmpresa_eg]         INT          NOT NULL,
    [IdSucursal_eg]        INT          NOT NULL,
    [IdMovi_inven_tipo_eg] INT          NOT NULL,
    [IdNumMovi_eg]         NUMERIC (18) NOT NULL,
    [Secuencia_eg]         INT          NOT NULL,
    [Observacion]          VARCHAR (1)  NULL,
    CONSTRAINT [PK_fa_factura_det_x_in_Ing_Egr_Inven_det] PRIMARY KEY CLUSTERED ([IdEmpresa_fa] ASC, [IdSucursal_fa] ASC, [IdBodega_fa] ASC, [IdCbteVta_fa] ASC, [Secuencia_fa] ASC, [IdEmpresa_eg] ASC, [IdSucursal_eg] ASC, [IdMovi_inven_tipo_eg] ASC, [IdNumMovi_eg] ASC, [Secuencia_eg] ASC)
);

