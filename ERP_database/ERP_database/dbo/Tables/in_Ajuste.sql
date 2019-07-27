﻿CREATE TABLE [dbo].[in_Ajuste] (
    [IdEmpresa]             INT           NOT NULL,
    [IdAjuste]              NUMERIC (18)  NOT NULL,
    [IdSucursal]            INT           NOT NULL,
    [IdBodega]              INT           NOT NULL,
    [IdMovi_inven_tipo_ing] INT           NULL,
    [IdMovi_inven_tipo_egr] INT           NULL,
    [IdNumMovi_ing]         NUMERIC (18)  NULL,
    [IdNumMovi_egr]         NUMERIC (18)  NULL,
    [IdCatalogo_Estado]     VARCHAR (15)  NOT NULL,
    [Estado]                BIT           NOT NULL,
    [Fecha]                 DATE          NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_in_Ajuste] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAjuste] ASC),
    CONSTRAINT [FK_in_Ajuste_in_Catalogo] FOREIGN KEY ([IdCatalogo_Estado]) REFERENCES [dbo].[in_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_in_Ajuste_in_Ing_Egr_Inven] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo_ing], [IdNumMovi_ing]) REFERENCES [dbo].[in_Ing_Egr_Inven] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi]),
    CONSTRAINT [FK_in_Ajuste_in_Ing_Egr_Inven1] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo_egr], [IdNumMovi_egr]) REFERENCES [dbo].[in_Ing_Egr_Inven] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi]),
    CONSTRAINT [FK_in_Ajuste_in_movi_inven_tipo] FOREIGN KEY ([IdEmpresa], [IdMovi_inven_tipo_ing]) REFERENCES [dbo].[in_movi_inven_tipo] ([IdEmpresa], [IdMovi_inven_tipo]),
    CONSTRAINT [FK_in_Ajuste_in_movi_inven_tipo1] FOREIGN KEY ([IdEmpresa], [IdMovi_inven_tipo_egr]) REFERENCES [dbo].[in_movi_inven_tipo] ([IdEmpresa], [IdMovi_inven_tipo]),
    CONSTRAINT [FK_in_Ajuste_tb_bodega] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega]),
    CONSTRAINT [FK_in_Ajuste_tb_bodega1] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega])
);

