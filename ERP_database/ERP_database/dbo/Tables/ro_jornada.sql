CREATE TABLE [dbo].[ro_jornada] (
    [IdEmpresa]       INT           NOT NULL,
    [IdJornada]       INT           NOT NULL,
    [codigo]          VARCHAR (100) NULL,
    [Descripcion]     VARCHAR (MAX) NOT NULL,
    [estado]          BIT           NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NOT NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    CONSTRAINT [PK_ro_jornada] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdJornada] ASC)
);



