CREATE TABLE [dbo].[imp_catalogo] (
    [IdCatalogo]      INT           NOT NULL,
    [IdCatalogo_tipo] INT           NOT NULL,
    [ca_descripcion]  VARCHAR (200) NOT NULL,
    [estado]          BIT           NOT NULL,
    CONSTRAINT [PK_imp_catalogo] PRIMARY KEY CLUSTERED ([IdCatalogo] ASC),
    CONSTRAINT [FK_imp_catalogo_imp_catalogo_tipo] FOREIGN KEY ([IdCatalogo_tipo]) REFERENCES [dbo].[imp_catalogo_tipo] ([IdCatalogo_tipo])
);

