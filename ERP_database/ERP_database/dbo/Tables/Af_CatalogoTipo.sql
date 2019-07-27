CREATE TABLE [dbo].[Af_CatalogoTipo] (
    [IdTipoCatalogo] VARCHAR (25)  NOT NULL,
    [Descripcion]    NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_af_CatalogoTipo] PRIMARY KEY CLUSTERED ([IdTipoCatalogo] ASC)
);

