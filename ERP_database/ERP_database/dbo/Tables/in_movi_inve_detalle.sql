CREATE TABLE [dbo].[in_movi_inve_detalle] (
    [IdEmpresa]                    INT           NOT NULL,
    [IdSucursal]                   INT           NOT NULL,
    [IdBodega]                     INT           NOT NULL,
    [IdMovi_inven_tipo]            INT           NOT NULL,
    [IdNumMovi]                    NUMERIC (18)  NOT NULL,
    [Secuencia]                    INT           NOT NULL,
    [mv_tipo_movi]                 NVARCHAR (1)  NOT NULL,
    [IdProducto]                   NUMERIC (18)  NOT NULL,
    [dm_cantidad]                  FLOAT (53)    NOT NULL,
    [dm_observacion]               VARCHAR (MAX) NULL,
    [mv_costo]                     FLOAT (53)    NOT NULL,
    [IdUnidadMedida]               VARCHAR (25)  NOT NULL,
    [dm_cantidad_sinConversion]    FLOAT (53)    NOT NULL,
    [IdUnidadMedida_sinConversion] VARCHAR (25)  NOT NULL,
    [mv_costo_sinConversion]       FLOAT (53)    NULL,
    [IdMotivo_Inv]                 INT           NULL,
    [Costeado]                     BIT           NOT NULL,
    [IdCentroCosto]                VARCHAR (200) NULL,
    [IdPunto_cargo_grupo]          INT           NULL,
    [IdPunto_cargo]                INT           NULL,
    CONSTRAINT [PK_in_movi_inve_detalle] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdMovi_inven_tipo] ASC, [IdNumMovi] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_in_movi_inve_detalle_ct_CentroCosto] FOREIGN KEY ([IdEmpresa], [IdCentroCosto]) REFERENCES [dbo].[ct_CentroCosto] ([IdEmpresa], [IdCentroCosto]),
    CONSTRAINT [FK_in_movi_inve_detalle_ct_punto_cargo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo]) REFERENCES [dbo].[ct_punto_cargo] ([IdEmpresa], [IdPunto_cargo]),
    CONSTRAINT [FK_in_movi_inve_detalle_ct_punto_cargo_grupo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo_grupo]) REFERENCES [dbo].[ct_punto_cargo_grupo] ([IdEmpresa], [IdPunto_cargo_grupo]),
    CONSTRAINT [FK_in_movi_inve_detalle_in_movi_inve] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdMovi_inven_tipo], [IdNumMovi]) REFERENCES [dbo].[in_movi_inve] ([IdEmpresa], [IdSucursal], [IdBodega], [IdMovi_inven_tipo], [IdNumMovi]),
    CONSTRAINT [FK_in_movi_inve_detalle_in_Producto] FOREIGN KEY ([IdEmpresa], [IdProducto]) REFERENCES [dbo].[in_Producto] ([IdEmpresa], [IdProducto]),
    CONSTRAINT [FK_in_movi_inve_detalle_in_UnidadMedida] FOREIGN KEY ([IdUnidadMedida]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida]),
    CONSTRAINT [FK_in_movi_inve_detalle_in_UnidadMedida1] FOREIGN KEY ([IdUnidadMedida_sinConversion]) REFERENCES [dbo].[in_UnidadMedida] ([IdUnidadMedida])
);



