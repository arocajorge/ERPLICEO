CREATE TABLE [dbo].[in_movi_inve_detalle_x_ct_cbtecble_det] (
    [IdEmpresa_inv]         INT          NOT NULL,
    [IdSucursal_inv]        INT          NOT NULL,
    [IdBodega_inv]          INT          NOT NULL,
    [IdMovi_inven_tipo_inv] INT          NOT NULL,
    [IdNumMovi_inv]         NUMERIC (18) NOT NULL,
    [Secuencia_inv]         INT          NOT NULL,
    [IdEmpresa_ct]          INT          NOT NULL,
    [IdTipoCbte_ct]         INT          NOT NULL,
    [IdCbteCble_ct]         NUMERIC (18) NOT NULL,
    [secuencia_ct]          INT          NOT NULL,
    [Secuencial_reg]        NUMERIC (18) NOT NULL,
    [observacion]           VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_in_movi_inve_detalle_x_ct_cbtecble_det] PRIMARY KEY CLUSTERED ([IdEmpresa_inv] ASC, [IdSucursal_inv] ASC, [IdBodega_inv] ASC, [IdMovi_inven_tipo_inv] ASC, [IdNumMovi_inv] ASC, [Secuencia_inv] ASC, [IdEmpresa_ct] ASC, [IdTipoCbte_ct] ASC, [IdCbteCble_ct] ASC, [secuencia_ct] ASC, [Secuencial_reg] ASC)
);

