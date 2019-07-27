CREATE TABLE [dbo].[tb_region] (
    [Cod_Region] VARCHAR (10)  NOT NULL,
    [Nom_region] VARCHAR (100) NOT NULL,
    [codigo]     VARCHAR (10)  NULL,
    [estado]     BIT           NOT NULL,
    [IdPais]     VARCHAR (10)  NOT NULL,
    CONSTRAINT [PK_tb_region] PRIMARY KEY CLUSTERED ([Cod_Region] ASC),
    CONSTRAINT [FK_tb_region_tb_pais] FOREIGN KEY ([IdPais]) REFERENCES [dbo].[tb_pais] ([IdPais])
);

