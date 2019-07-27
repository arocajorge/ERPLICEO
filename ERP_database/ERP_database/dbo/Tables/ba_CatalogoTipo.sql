CREATE TABLE [dbo].[ba_CatalogoTipo] (
    [IdTipoCatalogo] VARCHAR (10)  NOT NULL,
    [tc_Descripcion] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ba_CatalogoTipo] PRIMARY KEY CLUSTERED ([IdTipoCatalogo] ASC)
);

