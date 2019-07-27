CREATE TABLE [dbo].[fa_notaCreDeb_det] (
    [IdEmpresa]           INT            NOT NULL,
    [IdSucursal]          INT            NOT NULL,
    [IdBodega]            INT            NOT NULL,
    [IdNota]              NUMERIC (18)   NOT NULL,
    [Secuencia]           INT            NOT NULL,
    [IdProducto]          NUMERIC (18)   NOT NULL,
    [sc_cantidad]         FLOAT (53)     NOT NULL,
    [sc_Precio]           FLOAT (53)     NOT NULL,
    [sc_descUni]          FLOAT (53)     NOT NULL,
    [sc_PordescUni]       FLOAT (53)     NOT NULL,
    [sc_precioFinal]      FLOAT (53)     NOT NULL,
    [sc_subtotal]         FLOAT (53)     NOT NULL,
    [sc_iva]              FLOAT (53)     NOT NULL,
    [sc_total]            FLOAT (53)     NOT NULL,
    [sc_costo]            FLOAT (53)     NOT NULL,
    [sc_observacion]      VARCHAR (1000) NULL,
    [vt_por_iva]          FLOAT (53)     NOT NULL,
    [IdCentroCosto]       VARCHAR (200)  NULL,
    [IdPunto_Cargo]       INT            NULL,
    [IdPunto_cargo_grupo] INT            NULL,
    [IdCod_Impuesto_Iva]  VARCHAR (25)   NOT NULL,
    [sc_cantidad_factura] FLOAT (53)     NULL,
    CONSTRAINT [PK_fa_notaCreDeb_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdNota] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_fa_notaCreDeb_det_ct_CentroCosto] FOREIGN KEY ([IdEmpresa], [IdCentroCosto]) REFERENCES [dbo].[ct_CentroCosto] ([IdEmpresa], [IdCentroCosto]),
    CONSTRAINT [FK_fa_notaCreDeb_det_ct_punto_cargo] FOREIGN KEY ([IdEmpresa], [IdPunto_Cargo]) REFERENCES [dbo].[ct_punto_cargo] ([IdEmpresa], [IdPunto_cargo]),
    CONSTRAINT [FK_fa_notaCreDeb_det_ct_punto_cargo_grupo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo_grupo]) REFERENCES [dbo].[ct_punto_cargo_grupo] ([IdEmpresa], [IdPunto_cargo_grupo]),
    CONSTRAINT [FK_fa_notaCreDeb_det_fa_notaCreDeb] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota]) REFERENCES [dbo].[fa_notaCreDeb] ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota]),
    CONSTRAINT [FK_fa_notaCreDeb_det_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_fa_notaCreDeb_det_tb_sis_Impuesto] FOREIGN KEY ([IdCod_Impuesto_Iva]) REFERENCES [dbo].[tb_sis_Impuesto] ([IdCod_Impuesto])
);







