CREATE TABLE [dbo].[fa_catalogo_tipo] (
    [IdCatalogo_tipo] INT          NOT NULL,
    [Descripcion]     VARCHAR (50) NOT NULL,
    [Estado]          CHAR (1)     NOT NULL,
    CONSTRAINT [PK_fa_catalogo_tipo] PRIMARY KEY CLUSTERED ([IdCatalogo_tipo] ASC)
);

