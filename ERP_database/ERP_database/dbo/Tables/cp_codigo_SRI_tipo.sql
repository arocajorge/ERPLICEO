CREATE TABLE [dbo].[cp_codigo_SRI_tipo] (
    [IdTipoSRI]       VARCHAR (20) NOT NULL,
    [descripcion]     VARCHAR (50) NULL,
    [IdUsuario]       VARCHAR (20) NULL,
    [Fecha_Transac]   DATETIME     NULL,
    [IdUsuarioUltMod] VARCHAR (20) NULL,
    [Fecha_UltMod]    DATETIME     NULL,
    [estado]          VARCHAR (1)  NULL,
    [IdUsuarioUltAnu] VARCHAR (20) NULL,
    [Fecha_UltAnu]    DATETIME     NULL,
    [nom_pc]          VARCHAR (50) NULL,
    [ip]              VARCHAR (25) NULL,
    CONSTRAINT [PK_cp_codigo_SRI_tipo_1] PRIMARY KEY CLUSTERED ([IdTipoSRI] ASC)
);

