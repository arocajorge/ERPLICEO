CREATE TABLE [dbo].[ro_Division] (
    [IdEmpresa]       INT           NOT NULL,
    [IdDivision]      INT           NOT NULL,
    [Descripcion]     VARCHAR (100) NOT NULL,
    [estado]          CHAR (1)      NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (25)  NULL,
    [MotiAnula]       VARCHAR (200) NULL,
    CONSTRAINT [PK_ro_Division] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdDivision] ASC),
    CONSTRAINT [FK_ro_Division_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);

