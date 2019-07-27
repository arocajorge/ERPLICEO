CREATE TABLE [dbo].[tb_parroquia] (
    [IdParroquia]     VARCHAR (25)  NOT NULL,
    [cod_parroquia]   VARCHAR (50)  NOT NULL,
    [nom_parroquia]   VARCHAR (150) NOT NULL,
    [estado]          BIT           NOT NULL,
    [IdCiudad_Canton] VARCHAR (25)  NOT NULL,
    [IdUsuario]       VARCHAR (50)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (50)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (50)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [MotivoAnula]     VARCHAR (150) NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (50)  NULL,
    CONSTRAINT [PK_tb_parroquia] PRIMARY KEY CLUSTERED ([IdParroquia] ASC),
    CONSTRAINT [FK_tb_parroquia_tb_ciudad] FOREIGN KEY ([IdCiudad_Canton]) REFERENCES [dbo].[tb_ciudad] ([IdCiudad])
);

