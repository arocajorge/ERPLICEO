CREATE TABLE [dbo].[aca_MatriculaCondicionalParrafo] (
    [Id]         INT           NOT NULL,
    [Nombre]     VARCHAR (50)  NOT NULL,
    [Parrafo]    VARCHAR (MAX) NOT NULL,
    [Orden]      INT           NOT NULL,
    [IdCatalogo] INT           NOT NULL,
    CONSTRAINT [PK_aca_MatriculaCondicionalParrafo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_aca_MatriculaCondicionalParrafo_aca_Catalogo] FOREIGN KEY ([IdCatalogo]) REFERENCES [dbo].[aca_Catalogo] ([IdCatalogo])
);

