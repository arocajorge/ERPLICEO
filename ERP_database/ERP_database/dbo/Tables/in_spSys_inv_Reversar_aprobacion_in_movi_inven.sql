CREATE TABLE [dbo].[in_spSys_inv_Reversar_aprobacion_in_movi_inven] (
    [IdEmpresa_inv]         INT          NOT NULL,
    [IdSucursal_inv]        INT          NOT NULL,
    [IdBodega_inv]          INT          NOT NULL,
    [IdMovi_inven_tipo_inv] INT          NOT NULL,
    [IdNumMovi_inv]         NUMERIC (18) NOT NULL,
    [Secuencia_inv]         INT          NOT NULL,
    CONSTRAINT [PK_[in_spSys_inv_Reversar_aprobacion_in_movi_inven] PRIMARY KEY CLUSTERED ([IdEmpresa_inv] ASC, [IdSucursal_inv] ASC, [IdBodega_inv] ASC, [IdMovi_inven_tipo_inv] ASC, [IdNumMovi_inv] ASC, [Secuencia_inv] ASC)
);

