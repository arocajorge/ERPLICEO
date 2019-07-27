CREATE TABLE [dbo].[caj_catalogo] (
    [IdCatalogo_cj]      VARCHAR (25) NOT NULL,
    [nom_Catalogo_cj]    VARCHAR (50) NOT NULL,
    [IdCatalogo_cj_tipo] VARCHAR (25) NOT NULL,
    CONSTRAINT [PK_caj_catalogo] PRIMARY KEY CLUSTERED ([IdCatalogo_cj] ASC)
);

