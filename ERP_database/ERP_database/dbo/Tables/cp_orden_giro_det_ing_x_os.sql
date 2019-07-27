CREATE TABLE [dbo].[cp_orden_giro_det_ing_x_os] (
    [IdEmpresa]        INT          NOT NULL,
    [IdCbteCble_Ogiro] NUMERIC (18) NOT NULL,
    [IdTipoCbte_Ogiro] INT          NOT NULL,
    [Secuencia]        INT          NOT NULL,
    [oc_IdSucursal]    INT          NOT NULL,
    [oc_IdOrdenCompra] NUMERIC (18) NOT NULL,
    [oc_Secuencia]     INT          NOT NULL,
    [dm_cantidad]      FLOAT (53)   NOT NULL,
    [do_porc_des]      FLOAT (53)   NOT NULL,
    [do_descuento]     FLOAT (53)   NOT NULL,
    [do_precioCompra]  FLOAT (53)   NOT NULL,
    [do_precioFinal]   FLOAT (53)   NOT NULL,
    [do_subtotal]      FLOAT (53)   NOT NULL,
    [IdCod_Impuesto]   VARCHAR (25) NOT NULL,
    [do_iva]           FLOAT (53)   NOT NULL,
    [Por_Iva]          FLOAT (53)   NOT NULL,
    [do_total]         FLOAT (53)   NOT NULL,
    [IdUnidadMedida]   VARCHAR (25) NOT NULL,
    [IdProducto]       NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_cp_orden_giro_det_ing_x_os] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCbteCble_Ogiro] ASC, [IdTipoCbte_Ogiro] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_orden_giro_det_ing_x_os_cp_orden_giro] FOREIGN KEY ([IdEmpresa], [IdCbteCble_Ogiro], [IdTipoCbte_Ogiro]) REFERENCES [dbo].[cp_orden_giro] ([IdEmpresa], [IdCbteCble_Ogiro], [IdTipoCbte_Ogiro])
);

