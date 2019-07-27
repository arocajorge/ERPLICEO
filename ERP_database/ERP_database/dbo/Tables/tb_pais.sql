CREATE TABLE [dbo].[tb_pais] (
    [IdPais]          VARCHAR (10)  NOT NULL,
    [CodPais]         VARCHAR (50)  NOT NULL,
    [Nombre]          VARCHAR (50)  NOT NULL,
    [Nacionalidad]    VARCHAR (50)  NOT NULL,
    [estado]          CHAR (1)      NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [MotivoAnula]     VARCHAR (100) NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (25)  NULL,
    CONSTRAINT [PK_tb_pais] PRIMARY KEY CLUSTERED ([IdPais] ASC)
);

