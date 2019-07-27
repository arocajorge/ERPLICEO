CREATE TABLE [dbo].[in_Consignacion] (
    [IdEmpresa]         INT            NOT NULL,
    [IdConsignacion]    DECIMAL (18)   NOT NULL,
    [IdSucursal]        INT            NOT NULL,
    [IdBodega]          INT            NOT NULL,
    [Fecha]             DATETIME       NOT NULL,
    [IdProveedor]       NUMERIC (18)   NOT NULL,
    [Observacion]       VARCHAR (MAX)  NULL,
    [Estado]            BIT            NOT NULL,
    [IdUsuario]         VARCHAR (20)   NULL,
    [Fecha_Transac]     DATETIME       NULL,
    [IdUsuarioUltMod]   VARCHAR (20)   NULL,
    [Fecha_UltMod]      DATETIME       NULL,
    [IdUsuarioUltAnu]   VARCHAR (20)   NULL,
    [Fecha_UltAnu]      DATETIME       NULL,
    [MotivoAnulacion]   VARCHAR (5000) NULL,
    [IdMovi_inven_tipo] INT            NOT NULL,
    [IdNumMovi]         NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_In_consignacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConsignacion] ASC),
    CONSTRAINT [FK_in_Consignacion_cp_proveedor2] FOREIGN KEY ([IdEmpresa], [IdProveedor]) REFERENCES [dbo].[cp_proveedor] ([IdEmpresa], [IdProveedor]),
    CONSTRAINT [FK_in_Consignacion_in_Ing_Egr_Inven2] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi]) REFERENCES [dbo].[in_Ing_Egr_Inven] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi]),
    CONSTRAINT [FK_in_Consignacion_tb_bodega2] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega]),
    CONSTRAINT [FK_In_consignacion_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);



