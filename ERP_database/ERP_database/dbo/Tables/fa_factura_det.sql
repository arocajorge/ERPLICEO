﻿CREATE TABLE [dbo].[fa_factura_det] (
    [IdEmpresa]           INT            NOT NULL,
    [IdSucursal]          INT            NOT NULL,
    [IdBodega]            INT            NOT NULL,
    [IdCbteVta]           NUMERIC (18)   NOT NULL,
    [Secuencia]           INT            NOT NULL,
    [IdProducto]          NUMERIC (18)   NOT NULL,
    [vt_cantidad]         FLOAT (53)     NOT NULL,
    [vt_Precio]           FLOAT (53)     NOT NULL,
    [vt_PorDescUnitario]  FLOAT (53)     NOT NULL,
    [vt_DescUnitario]     FLOAT (53)     NOT NULL,
    [vt_PrecioFinal]      FLOAT (53)     NOT NULL,
    [vt_Subtotal]         FLOAT (53)     NOT NULL,
    [vt_iva]              FLOAT (53)     NOT NULL,
    [vt_total]            FLOAT (53)     NOT NULL,
    [vt_detallexItems]    NVARCHAR (MAX) NULL,
    [vt_por_iva]          FLOAT (53)     NOT NULL,
    [IdCentroCosto]       VARCHAR (200)  NULL,
    [IdPunto_Cargo]       INT            NULL,
    [IdPunto_cargo_grupo] INT            NULL,
    [IdCod_Impuesto_Iva]  VARCHAR (25)   NOT NULL,
    [IdEmpresa_pf]        INT            NULL,
    [IdSucursal_pf]       INT            NULL,
    [IdProforma]          NUMERIC (18)   NULL,
    [Secuencia_pf]        INT            NULL,
    CONSTRAINT [PK_fa_factura_detalle] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdCbteVta] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_fa_factura_det_ct_CentroCosto] FOREIGN KEY ([IdEmpresa], [IdCentroCosto]) REFERENCES [dbo].[ct_CentroCosto] ([IdEmpresa], [IdCentroCosto]),
    CONSTRAINT [FK_fa_factura_det_ct_punto_cargo] FOREIGN KEY ([IdEmpresa], [IdPunto_Cargo]) REFERENCES [dbo].[ct_punto_cargo] ([IdEmpresa], [IdPunto_cargo]),
    CONSTRAINT [FK_fa_factura_det_ct_punto_cargo_grupo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo_grupo]) REFERENCES [dbo].[ct_punto_cargo_grupo] ([IdEmpresa], [IdPunto_cargo_grupo]),
    CONSTRAINT [FK_fa_factura_det_fa_factura] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta]) REFERENCES [dbo].[fa_factura] ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta]),
    CONSTRAINT [FK_fa_factura_det_fa_proforma_det] FOREIGN KEY ([IdEmpresa_pf], [IdSucursal_pf], [IdProforma], [Secuencia_pf]) REFERENCES [dbo].[fa_proforma_det] ([IdEmpresa], [IdSucursal], [IdProforma], [Secuencia]),
    CONSTRAINT [FK_fa_factura_det_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_fa_factura_det_tb_sis_Impuesto] FOREIGN KEY ([IdCod_Impuesto_Iva]) REFERENCES [dbo].[tb_sis_Impuesto] ([IdCod_Impuesto])
);









