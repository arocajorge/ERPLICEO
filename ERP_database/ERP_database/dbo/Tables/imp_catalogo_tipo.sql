CREATE TABLE [dbo].[imp_catalogo_tipo] (
    [IdCatalogo_tipo] INT           NOT NULL,
    [ct_descripcion]  VARCHAR (100) NOT NULL,
    [estado]          BIT           NOT NULL,
    CONSTRAINT [PK_imp_catalogo_tipo] PRIMARY KEY CLUSTERED ([IdCatalogo_tipo] ASC)
);

