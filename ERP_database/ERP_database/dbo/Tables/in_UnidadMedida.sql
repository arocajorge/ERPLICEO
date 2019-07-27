CREATE TABLE [dbo].[in_UnidadMedida] (
    [IdUnidadMedida]  VARCHAR (25)  NOT NULL,
    [cod_alterno]     VARCHAR (25)  NULL,
    [Descripcion]     VARCHAR (500) NOT NULL,
    [Estado]          CHAR (1)      NOT NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    CONSTRAINT [PK_in_Unidad_Medida] PRIMARY KEY CLUSTERED ([IdUnidadMedida] ASC)
);

