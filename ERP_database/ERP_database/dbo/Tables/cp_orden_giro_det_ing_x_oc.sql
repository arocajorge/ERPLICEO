CREATE TABLE [dbo].[cp_orden_giro_det_ing_x_oc] (
    [IdEmpresa]             INT          NOT NULL,
    [IdCbteCble_Ogiro]      NUMERIC (18) NOT NULL,
    [IdTipoCbte_Ogiro]      INT          NOT NULL,
    [Secuencia]             INT          NOT NULL,
    [inv_IdSucursal]        INT          NOT NULL,
    [inv_IdMovi_inven_tipo] INT          NOT NULL,
    [inv_IdNumMovi]         NUMERIC (18) NOT NULL,
    [inv_Secuencia]         INT          NOT NULL,
    [oc_IdSucursal]         INT          NOT NULL,
    [oc_IdOrdenCompra]      NUMERIC (18) NOT NULL,
    [oc_Secuencia]          INT          NOT NULL,
    [IdCtaCble]             VARCHAR (20) NOT NULL,
    [dm_cantidad]           FLOAT (53)   NOT NULL,
    [do_porc_des]           FLOAT (53)   NOT NULL,
    [do_descuento]          FLOAT (53)   NOT NULL,
    [do_precioCompra]       FLOAT (53)   NOT NULL,
    [do_precioFinal]        FLOAT (53)   NOT NULL,
    [do_subtotal]           FLOAT (53)   NOT NULL,
    [IdCod_Impuesto]        VARCHAR (25) NOT NULL,
    [do_iva]                FLOAT (53)   NOT NULL,
    [Por_Iva]               FLOAT (53)   NOT NULL,
    [do_total]              FLOAT (53)   NOT NULL,
    [IdUnidadMedida]        VARCHAR (25) NOT NULL,
    [IdProducto]            NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_cp_orden_giro_det_ing_x_oc] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCbteCble_Ogiro] ASC, [IdTipoCbte_Ogiro] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_orden_giro_det_ing_x_oc_com_ordencompra_local_det] FOREIGN KEY ([IdEmpresa], [oc_IdSucursal], [oc_IdOrdenCompra], [oc_Secuencia]) REFERENCES [dbo].[com_ordencompra_local_det] ([IdEmpresa], [IdSucursal], [IdOrdenCompra], [Secuencia]),
    CONSTRAINT [FK_cp_orden_giro_det_ing_x_oc_cp_orden_giro] FOREIGN KEY ([IdEmpresa], [IdCbteCble_Ogiro], [IdTipoCbte_Ogiro]) REFERENCES [dbo].[cp_orden_giro] ([IdEmpresa], [IdCbteCble_Ogiro], [IdTipoCbte_Ogiro]),
    CONSTRAINT [FK_cp_orden_giro_det_ing_x_oc_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_cp_orden_giro_det_ing_x_oc_in_Ing_Egr_Inven_det] FOREIGN KEY ([IdEmpresa], [inv_IdSucursal], [inv_IdMovi_inven_tipo], [inv_IdNumMovi], [inv_Secuencia]) REFERENCES [dbo].[in_Ing_Egr_Inven_det] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi], [Secuencia])
);



