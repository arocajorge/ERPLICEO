CREATE TABLE [dbo].[tb_modulo] (
    [CodModulo]   VARCHAR (20) NOT NULL,
    [Descripcion] VARCHAR (50) NOT NULL,
    [Nom_Carpeta] VARCHAR (50) NULL,
    [Se_Cierra]   BIT          NULL,
    CONSTRAINT [PK_tb_modulo_1] PRIMARY KEY CLUSTERED ([CodModulo] ASC)
);

