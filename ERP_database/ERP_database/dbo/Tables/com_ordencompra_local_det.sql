CREATE TABLE [dbo].[com_ordencompra_local_det] (
    [IdEmpresa]           INT           NOT NULL,
    [IdSucursal]          INT           NOT NULL,
    [IdOrdenCompra]       NUMERIC (18)  NOT NULL,
    [Secuencia]           INT           NOT NULL,
    [IdProducto]          NUMERIC (18)  NOT NULL,
    [do_Cantidad]         FLOAT (53)    NOT NULL,
    [do_precioCompra]     FLOAT (53)    NOT NULL,
    [do_porc_des]         FLOAT (53)    NOT NULL,
    [do_descuento]        FLOAT (53)    NOT NULL,
    [do_precioFinal]      FLOAT (53)    NOT NULL,
    [do_subtotal]         FLOAT (53)    NOT NULL,
    [do_iva]              FLOAT (53)    NOT NULL,
    [do_total]            FLOAT (53)    NOT NULL,
    [do_observacion]      VARCHAR (MAX) NULL,
    [IdCentroCosto]       VARCHAR (200) NULL,
    [IdPunto_cargo_grupo] INT           NULL,
    [IdPunto_cargo]       INT           NULL,
    [IdUnidadMedida]      VARCHAR (25)  NOT NULL,
    [Por_Iva]             FLOAT (53)    NOT NULL,
    [IdCod_Impuesto]      VARCHAR (25)  NOT NULL,
    CONSTRAINT [PK_com_ordencompra_local_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdOrdenCompra] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_com_ordencompra_local_det_com_ordencompra_local] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdOrdenCompra]) REFERENCES [dbo].[com_ordencompra_local] ([IdEmpresa], [IdSucursal], [IdOrdenCompra]),
    CONSTRAINT [FK_com_ordencompra_local_det_ct_CentroCosto] FOREIGN KEY ([IdEmpresa], [IdCentroCosto]) REFERENCES [dbo].[ct_CentroCosto] ([IdEmpresa], [IdCentroCosto]),
    CONSTRAINT [FK_com_ordencompra_local_det_ct_punto_cargo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo]) REFERENCES [dbo].[ct_punto_cargo] ([IdEmpresa], [IdPunto_cargo]),
    CONSTRAINT [FK_com_ordencompra_local_det_ct_punto_cargo_grupo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo_grupo]) REFERENCES [dbo].[ct_punto_cargo_grupo] ([IdEmpresa], [IdPunto_cargo_grupo]),
    CONSTRAINT [FK_com_ordencompra_local_det_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_com_ordencompra_local_det_in_UnidadMedida] FOREIGN KEY ([IdUnidadMedida]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida]),
    CONSTRAINT [FK_com_ordencompra_local_det_tb_sis_Impuesto] FOREIGN KEY ([IdCod_Impuesto]) REFERENCES [dbo].[tb_sis_Impuesto] ([IdCod_Impuesto])
);



