CREATE TABLE [dbo].[ro_area] (
    [IdEmpresa]       INT           NOT NULL,
    [IdDivision]      INT           NOT NULL,
    [IdArea]          INT           NOT NULL,
    [Descripcion]     VARCHAR (50)  NOT NULL,
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
    CONSTRAINT [PK_ro_Area] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdDivision] ASC, [IdArea] ASC),
    CONSTRAINT [FK_ro_area_ro_Division] FOREIGN KEY ([IdEmpresa], [IdDivision]) REFERENCES [dbo].[ro_Division] ([IdEmpresa], [IdDivision])
);

