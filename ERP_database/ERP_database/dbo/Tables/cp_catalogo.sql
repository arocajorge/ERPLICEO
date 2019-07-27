CREATE TABLE [dbo].[cp_catalogo] (
    [IdCatalogo]      VARCHAR (25) NOT NULL,
    [IdCatalogo_tipo] VARCHAR (15) NOT NULL,
    [Nombre]          VARCHAR (50) NOT NULL,
    [Estado]          CHAR (1)     NOT NULL,
    [Abrebiatura]     VARCHAR (10) NULL,
    [NombreIngles]    VARCHAR (50) NULL,
    [Orden]           INT          NULL,
    [IdUsuario]       VARCHAR (20) NULL,
    [IdUsuarioUltMod] VARCHAR (20) NULL,
    [FechaUltMod]     DATETIME     NULL,
    [nom_pc]          VARCHAR (50) NULL,
    [ip]              VARCHAR (25) NULL,
    CONSTRAINT [PK_cp_catalogo] PRIMARY KEY CLUSTERED ([IdCatalogo] ASC),
    CONSTRAINT [FK_cp_catalogo_cp_catalogo_tipo] FOREIGN KEY ([IdCatalogo_tipo]) REFERENCES [dbo].[cp_catalogo_tipo] ([IdCatalogo_tipo])
);

