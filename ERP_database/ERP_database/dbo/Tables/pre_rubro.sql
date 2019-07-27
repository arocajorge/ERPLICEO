CREATE TABLE [dbo].[pre_Rubro] (
    [IdEmpresa]             INT           NOT NULL,
    [IdRubro]               INT           NOT NULL,
    [IdRubroTipo]           INT           NOT NULL,
    [Descripcion]           VARCHAR (MAX) NOT NULL,
    [IdCtaCble]             VARCHAR (20)  NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ct_gasto] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRubro] ASC),
    CONSTRAINT [FK_ct_gasto_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_pre_Rubro_pre_RubroTipo] FOREIGN KEY ([IdEmpresa], [IdRubroTipo]) REFERENCES [dbo].[pre_RubroTipo] ([IdEmpresa], [IdRubroTipo])
);



