CREATE TABLE [dbo].[pro_Fabricacion] (
    [IdEmpresa]             INT           NOT NULL,
    [IdFabricacion]         DECIMAL (18)  NOT NULL,
    [egr_IdSucursal]        INT           NOT NULL,
    [egr_IdBodega]          INT           NOT NULL,
    [egr_IdMovi_inven_tipo] INT           NULL,
    [egr_IdNumMovi]         NUMERIC (18)  NULL,
    [ing_IdSucursal]        INT           NOT NULL,
    [ing_IdBodega]          INT           NOT NULL,
    [ing_IdMovi_inven_tipo] INT           NULL,
    [ing_IdNumMovi]         NUMERIC (18)  NULL,
    [Fecha]                 DATE          NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_pro_Fabricacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdFabricacion] ASC),
    CONSTRAINT [FK_pro_Fabricacion_in_Ing_Egr_Inven] FOREIGN KEY ([IdEmpresa], [egr_IdSucursal], [egr_IdMovi_inven_tipo], [egr_IdNumMovi]) REFERENCES [dbo].[in_Ing_Egr_Inven] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi]),
    CONSTRAINT [FK_pro_Fabricacion_in_Ing_Egr_Inven1] FOREIGN KEY ([IdEmpresa], [ing_IdSucursal], [ing_IdMovi_inven_tipo], [ing_IdNumMovi]) REFERENCES [dbo].[in_Ing_Egr_Inven] ([IdEmpresa], [IdSucursal], [IdMovi_inven_tipo], [IdNumMovi])
);



