CREATE TABLE [dbo].[com_catalogo_tipo] (
    [IdCatalogocompra_tipo] VARCHAR (15) NOT NULL,
    [Descripcion]           VARCHAR (50) NOT NULL,
    [Estado]                CHAR (1)     NOT NULL,
    CONSTRAINT [PK_com_catalogo_tipo] PRIMARY KEY CLUSTERED ([IdCatalogocompra_tipo] ASC)
);

