CREATE TABLE [dbo].[ct_punto_cargo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdPunto_cargo]         INT           NOT NULL,
    [cod_punto_cargo]       VARCHAR (50)  NOT NULL,
    [nom_punto_cargo]       VARCHAR (MAX) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdPunto_cargo_grupo]   INT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ct_punto_cargo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPunto_cargo] ASC),
    CONSTRAINT [FK_ct_punto_cargo_ct_punto_cargo_grupo] FOREIGN KEY ([IdEmpresa], [IdPunto_cargo_grupo]) REFERENCES [dbo].[ct_punto_cargo_grupo] ([IdEmpresa], [IdPunto_cargo_grupo])
);



