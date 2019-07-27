CREATE TABLE [dbo].[in_movi_inven_X_imp_OrdCompraExterna] (
    [imp_IdEmpresa]        INT          NOT NULL,
    [imp_IdSucursal]       INT          NOT NULL,
    [imp_IdOrdenCompraExt] NUMERIC (18) NOT NULL,
    [in_IdEmpresa]         INT          NOT NULL,
    [in_IdSucursal]        INT          NOT NULL,
    [in_IdBodega]          INT          NOT NULL,
    [in_IdMovi_inven_tipo] INT          NOT NULL,
    [in_IdNumMovi]         NUMERIC (10) NOT NULL,
    CONSTRAINT [PK_in_movi_inven_X_imp_OrdCompraLocal] PRIMARY KEY CLUSTERED ([imp_IdEmpresa] ASC, [imp_IdSucursal] ASC, [imp_IdOrdenCompraExt] ASC, [in_IdEmpresa] ASC, [in_IdSucursal] ASC, [in_IdBodega] ASC, [in_IdMovi_inven_tipo] ASC, [in_IdNumMovi] ASC)
);

