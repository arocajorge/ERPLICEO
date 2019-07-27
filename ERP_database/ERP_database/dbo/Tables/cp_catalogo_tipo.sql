CREATE TABLE [dbo].[cp_catalogo_tipo] (
    [IdCatalogo_tipo] VARCHAR (15) NOT NULL,
    [Descripcion]     VARCHAR (50) NOT NULL,
    [Estado]          CHAR (1)     NOT NULL,
    CONSTRAINT [PK_cp_catalogo_tipo] PRIMARY KEY CLUSTERED ([IdCatalogo_tipo] ASC)
);

