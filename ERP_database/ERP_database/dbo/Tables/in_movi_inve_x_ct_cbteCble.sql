CREATE TABLE [dbo].[in_movi_inve_x_ct_cbteCble] (
    [IdEmpresa]         INT          NOT NULL,
    [IdSucursal]        INT          NOT NULL,
    [IdBodega]          INT          NOT NULL,
    [IdMovi_inven_tipo] INT          NOT NULL,
    [IdNumMovi]         NUMERIC (18) NOT NULL,
    [IdTipoCbte]        INT          NOT NULL,
    [IdCbteCble]        NUMERIC (18) NOT NULL,
    [Observacion]       VARCHAR (50) NOT NULL,
    [IdEmpresa_ct]      INT          NOT NULL,
    CONSTRAINT [PK_in_movi_inve_x_ct_cbteCble] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdMovi_inven_tipo] ASC, [IdNumMovi] ASC, [IdTipoCbte] ASC, [IdCbteCble] ASC, [IdEmpresa_ct] ASC),
    CONSTRAINT [FK_in_movi_inve_x_ct_cbteCble_ct_cbtecble] FOREIGN KEY ([IdEmpresa_ct], [IdTipoCbte], [IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_in_movi_inve_x_ct_cbteCble_in_movi_inve] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdMovi_inven_tipo], [IdNumMovi]) REFERENCES [dbo].[in_movi_inve] ([IdEmpresa], [IdSucursal], [IdBodega], [IdMovi_inven_tipo], [IdNumMovi])
);

