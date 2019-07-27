CREATE TABLE [dbo].[in_Marca] (
    [IdEmpresa]       INT           NOT NULL,
    [IdMarca]         INT           NOT NULL,
    [Descripcion]     VARCHAR (100) NOT NULL,
    [Estado]          CHAR (1)      NOT NULL,
    [IdUsuario]       VARCHAR (50)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (50)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (50)  NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [MotiAnula]       VARCHAR (200) NULL,
    CONSTRAINT [PK_in_Marca] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMarca] ASC),
    CONSTRAINT [FK_in_Marca_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);

