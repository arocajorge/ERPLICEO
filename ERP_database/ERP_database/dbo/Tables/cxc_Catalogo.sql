CREATE TABLE [dbo].[cxc_Catalogo] (
    [IdCatalogo]      VARCHAR (20) NOT NULL,
    [IdCatalogo_tipo] VARCHAR (20) NOT NULL,
    [Nombre]          VARCHAR (50) NOT NULL,
    [Estado]          CHAR (1)     NOT NULL,
    [Orden]           INT          NULL,
    [IdUsuario]       VARCHAR (20) NULL,
    [IdUsuarioUltMod] VARCHAR (20) NULL,
    [FechaUltMod]     DATETIME     NULL,
    [nom_pc]          VARCHAR (50) NULL,
    [ip]              VARCHAR (25) NULL,
    CONSTRAINT [PK_cxc_Catalogo] PRIMARY KEY CLUSTERED ([IdCatalogo] ASC),
    CONSTRAINT [FK_cxc_Catalogo_cxc_CatalogoTipo] FOREIGN KEY ([IdCatalogo_tipo]) REFERENCES [dbo].[cxc_CatalogoTipo] ([IdCatalogo_tipo])
);

