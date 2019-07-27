CREATE TABLE [dbo].[fa_proforma_det] (
    [IdEmpresa]            INT           NOT NULL,
    [IdSucursal]           INT           NOT NULL,
    [IdProforma]           NUMERIC (18)  NOT NULL,
    [Secuencia]            INT           NOT NULL,
    [IdProducto]           NUMERIC (18)  NOT NULL,
    [pd_cantidad]          FLOAT (53)    NOT NULL,
    [pd_precio]            FLOAT (53)    NOT NULL,
    [pd_por_descuento_uni] FLOAT (53)    NOT NULL,
    [pd_descuento_uni]     FLOAT (53)    NOT NULL,
    [pd_precio_final]      FLOAT (53)    NOT NULL,
    [pd_subtotal]          FLOAT (53)    NOT NULL,
    [IdCod_Impuesto]       VARCHAR (25)  NOT NULL,
    [pd_por_iva]           FLOAT (53)    NOT NULL,
    [pd_iva]               FLOAT (53)    NOT NULL,
    [pd_total]             FLOAT (53)    NOT NULL,
    [anulado]              BIT           NOT NULL,
    [IdCentroCosto]        VARCHAR (200) NULL,
    [IdPunto_cargo_grupo]  INT           NULL,
    [IdPunto_cargo]        INT           NULL,
    CONSTRAINT [PK_fa_proforma_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdProforma] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_fa_proforma_det_ct_CentroCosto] FOREIGN KEY ([IdEmpresa], [IdCentroCosto]) REFERENCES [dbo].[ct_CentroCosto] ([IdEmpresa], [IdCentroCosto]),
    CONSTRAINT [FK_fa_proforma_det_ct_punto_cargo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo]) REFERENCES [dbo].[ct_punto_cargo] ([IdEmpresa], [IdPunto_cargo]),
    CONSTRAINT [FK_fa_proforma_det_ct_punto_cargo_grupo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo_grupo]) REFERENCES [dbo].[ct_punto_cargo_grupo] ([IdEmpresa], [IdPunto_cargo_grupo]),
    CONSTRAINT [FK_fa_proforma_det_fa_proforma] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdProforma]) REFERENCES [dbo].[fa_proforma] ([IdEmpresa], [IdSucursal], [IdProforma]),
    CONSTRAINT [FK_fa_proforma_det_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_fa_proforma_det_tb_sis_Impuesto] FOREIGN KEY ([IdCod_Impuesto]) REFERENCES [dbo].[tb_sis_Impuesto] ([IdCod_Impuesto])
);



