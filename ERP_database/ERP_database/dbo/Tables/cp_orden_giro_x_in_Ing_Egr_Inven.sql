CREATE TABLE [dbo].[cp_orden_giro_x_in_Ing_Egr_Inven] (
    [og_IdEmpresa]          INT          NOT NULL,
    [og_IdCbteCble_Ogiro]   NUMERIC (18) NOT NULL,
    [og_IdTipoCbte_Ogiro]   INT          NOT NULL,
    [inv_IdEmpresa]         INT          NOT NULL,
    [inv_IdSucursal]        INT          NOT NULL,
    [inv_IdMovi_inven_tipo] INT          NOT NULL,
    [inv_IdNumMovi]         NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_cp_orden_giro_x_in_Ing_Egr_Inven] PRIMARY KEY CLUSTERED ([og_IdEmpresa] ASC, [og_IdCbteCble_Ogiro] ASC, [og_IdTipoCbte_Ogiro] ASC, [inv_IdEmpresa] ASC, [inv_IdSucursal] ASC, [inv_IdMovi_inven_tipo] ASC, [inv_IdNumMovi] ASC),
    CONSTRAINT [FK_cp_orden_giro_x_in_Ing_Egr_Inven_cp_orden_giro] FOREIGN KEY ([og_IdEmpresa], [og_IdCbteCble_Ogiro], [og_IdTipoCbte_Ogiro]) REFERENCES [dbo].[cp_orden_giro] ([IdEmpresa], [IdCbteCble_Ogiro], [IdTipoCbte_Ogiro]),
    CONSTRAINT [FK_cp_orden_giro_x_in_Ing_Egr_Inven_in_Ing_Egr_Inven] FOREIGN KEY ([inv_IdEmpresa], [inv_IdSucursal], [inv_IdMovi_inven_tipo], [inv_IdNumMovi]) REFERENCES [dbo].[in_Ing_Egr_Inven] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi])
);

