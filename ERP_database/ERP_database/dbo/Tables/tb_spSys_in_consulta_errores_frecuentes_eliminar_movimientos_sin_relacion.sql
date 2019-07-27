CREATE TABLE [dbo].[tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion] (
    [IdEmpresa]         INT          NOT NULL,
    [IdSucursal]        INT          NOT NULL,
    [IdBodega]          INT          NOT NULL,
    [IdMovi_inven_tipo] INT          NOT NULL,
    [IdNumMovi]         NUMERIC (18) NOT NULL,
    [IdUsuario]         VARCHAR (20) NOT NULL,
    [IdEmpresa_ct]      INT          NULL,
    [IdTipoCbte_ct]     INT          NULL,
    [IdCbteCble_ct]     NUMERIC (18) NULL,
    [IdEmpresa_anu]     INT          NULL,
    [IdTipoCbte_anu]    INT          NULL,
    [IdCbteCble_anu]    NUMERIC (18) NULL,
    CONSTRAINT [PK_tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdMovi_inven_tipo] ASC, [IdNumMovi] ASC, [IdUsuario] ASC)
);

