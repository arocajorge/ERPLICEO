CREATE TABLE [dbo].[in_Ing_Egr_Inven] (
    [IdEmpresa]         INT            NOT NULL,
    [IdSucursal]        INT            NOT NULL,
    [IdMovi_inven_tipo] INT            NOT NULL,
    [IdNumMovi]         NUMERIC (18)   NOT NULL,
    [IdBodega]          INT            NULL,
    [signo]             CHAR (1)       NOT NULL,
    [CodMoviInven]      VARCHAR (25)   NULL,
    [cm_observacion]    VARCHAR (1000) NULL,
    [cm_fecha]          DATETIME       NOT NULL,
    [IdUsuario]         VARCHAR (20)   NULL,
    [Estado]            CHAR (1)       NOT NULL,
    [MotivoAnulacion]   VARCHAR (100)  NULL,
    [Fecha_Transac]     DATETIME       NULL,
    [IdUsuarioUltModi]  VARCHAR (20)   NULL,
    [Fecha_UltMod]      DATETIME       NULL,
    [IdusuarioUltAnu]   VARCHAR (20)   NULL,
    [Fecha_UltAnu]      DATETIME       NULL,
    [IdMotivo_Inv]      INT            NULL,
    [IdResponsable]     NUMERIC (18)   NULL,
	[IdEstadoAproba]   VARCHAR (15)    NULL,
	[IdUsuarioAR]         VARCHAR (20)   NULL,
	[FechaAR]      DATETIME       NULL,
	[IdUsuarioDespacho]         VARCHAR (20)   NULL,
	[FechaDespacho]      DATETIME       NULL,
    CONSTRAINT [PK_in_Ing_Egr_Inven] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdMovi_inven_tipo] ASC, [IdNumMovi] ASC),
    CONSTRAINT [FK_in_Ing_Egr_Inven_in_Motivo_Inven] FOREIGN KEY ([IdEmpresa], [IdMotivo_Inv]) REFERENCES [dbo].[in_Motivo_Inven] ([IdEmpresa], [IdMotivo_Inv]),
    CONSTRAINT [FK_in_Ing_Egr_Inven_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);



