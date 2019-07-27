CREATE TABLE [dbo].[in_CatalogoTipo] (
    [IdCatalogo_tipo]   INT          NOT NULL,
    [cod_Catalogo_tipo] VARCHAR (50) NULL,
    [Descripcion]       VARCHAR (50) NOT NULL,
    [Estado]            CHAR (1)     NOT NULL,
    CONSTRAINT [PK_in_CatalogoTipo] PRIMARY KEY CLUSTERED ([IdCatalogo_tipo] ASC)
);

