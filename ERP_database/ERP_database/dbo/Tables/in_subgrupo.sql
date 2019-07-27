CREATE TABLE [dbo].[in_subgrupo] (
    [IdEmpresa]       INT           NOT NULL,
    [IdCategoria]     VARCHAR (25)  NOT NULL,
    [IdLinea]         INT           NOT NULL,
    [IdGrupo]         INT           NOT NULL,
    [IdSubgrupo]      INT           NOT NULL,
    [cod_subgrupo]    VARCHAR (50)  NULL,
    [nom_subgrupo]    VARCHAR (150) NOT NULL,
    [observacion]     VARCHAR (150) NULL,
    [Estado]          CHAR (1)      NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (25)  NULL,
    [MotiAnula]       VARCHAR (200) NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    CONSTRAINT [PK_in_subgrupo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCategoria] ASC, [IdLinea] ASC, [IdGrupo] ASC, [IdSubgrupo] ASC),
    CONSTRAINT [FK_in_subgrupo_in_grupo] FOREIGN KEY ([IdEmpresa], [IdCategoria], [IdLinea], [IdGrupo]) REFERENCES [dbo].[in_grupo] ([IdEmpresa], [IdCategoria], [IdLinea], [IdGrupo])
);

