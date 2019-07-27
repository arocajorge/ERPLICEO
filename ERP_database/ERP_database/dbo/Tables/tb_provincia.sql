CREATE TABLE [dbo].[tb_provincia] (
    [IdProvincia]      VARCHAR (25)  NOT NULL,
    [Cod_Provincia]    VARCHAR (25)  NULL,
    [Descripcion_Prov] VARCHAR (50)  NOT NULL,
    [Estado]           CHAR (1)      NOT NULL,
    [IdPais]           VARCHAR (10)  NOT NULL,
    [IdUsuario]        VARCHAR (20)  NULL,
    [Fecha_Transac]    DATETIME      NULL,
    [IdUsuarioUltMod]  VARCHAR (20)  NULL,
    [Fecha_UltMod]     DATETIME      NULL,
    [IdUsuarioUltAnu]  VARCHAR (20)  NULL,
    [Fecha_UltAnu]     DATETIME      NULL,
    [MotivoAnula]      VARCHAR (100) NULL,
    [nom_pc]           VARCHAR (50)  NULL,
    [ip]               VARCHAR (25)  NULL,
    [Cod_Region]       VARCHAR (10)  NULL,
    CONSTRAINT [PK_tb_provincia] PRIMARY KEY CLUSTERED ([IdProvincia] ASC),
    CONSTRAINT [FK_tb_provincia_tb_pais] FOREIGN KEY ([IdPais]) REFERENCES [dbo].[tb_pais] ([IdPais]),
    CONSTRAINT [FK_tb_provincia_tb_region] FOREIGN KEY ([Cod_Region]) REFERENCES [dbo].[tb_region] ([Cod_Region])
);

