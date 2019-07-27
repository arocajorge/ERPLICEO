CREATE TABLE [dbo].[ct_CentroCosto] (
    [IdEmpresa]             INT           NOT NULL,
    [IdCentroCosto]         VARCHAR (200) NOT NULL,
    [IdCentroCostoPadre]    VARCHAR (200) NULL,
    [IdNivel]               INT           NOT NULL,
    [cc_Descripcion]        VARCHAR (500) NOT NULL,
    [EsMovimiento]          BIT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ct_CentroCosto] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCentroCosto] ASC),
    CONSTRAINT [FK_ct_CentroCosto_ct_CentroCosto] FOREIGN KEY ([IdEmpresa], [IdCentroCostoPadre]) REFERENCES [dbo].[ct_CentroCosto] ([IdEmpresa], [IdCentroCosto]),
    CONSTRAINT [FK_ct_CentroCosto_ct_CentroCostoNivel] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[ct_CentroCostoNivel] ([IdEmpresa], [IdNivel])
);



