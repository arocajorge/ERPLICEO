﻿CREATE TABLE [dbo].[in_movi_inve] (
    [IdEmpresa]                      INT            NOT NULL,
    [IdSucursal]                     INT            NOT NULL,
    [IdBodega]                       INT            NOT NULL,
    [IdMovi_inven_tipo]              INT            NOT NULL,
    [IdNumMovi]                      NUMERIC (18)   NOT NULL,
    [CodMoviInven]                   VARCHAR (25)   NULL,
    [cm_tipo]                        CHAR (1)       NOT NULL,
    [cm_observacion]                 VARCHAR (max) NULL,
    [cm_fecha]                       DATETIME       NOT NULL,
    [IdUsuario]                      VARCHAR (20)   NULL,
    [Fecha_Transac]                  DATETIME       NULL,
    [IdUsuarioUltModi]               VARCHAR (20)   NULL,
    [Fecha_UltMod]                   DATETIME       NULL,
    [IdusuarioUltAnu]                VARCHAR (20)   NULL,
    [Fecha_UltAnu]                   DATETIME       NULL,
    [Estado]                         CHAR (1)       NOT NULL,
    [MotivoAnulacion]                VARCHAR (100)  NULL,
    [IdMotivo_Inv]                   INT            NULL,
    CONSTRAINT [PK_in_movi_inve] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdMovi_inven_tipo] ASC, [IdNumMovi] ASC),
    CONSTRAINT [FK_in_movi_inve_in_Motivo_Inven] FOREIGN KEY ([IdEmpresa], [IdMotivo_Inv]) REFERENCES [dbo].[in_Motivo_Inven] ([IdEmpresa], [IdMotivo_Inv]),
    CONSTRAINT [FK_in_movi_inve_in_movi_inven_tipo] FOREIGN KEY ([IdEmpresa], [IdMovi_inven_tipo]) REFERENCES [dbo].[in_movi_inven_tipo] ([IdEmpresa], [IdMovi_inven_tipo]),
    CONSTRAINT [FK_in_movi_inve_tb_bodega] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega])
);

