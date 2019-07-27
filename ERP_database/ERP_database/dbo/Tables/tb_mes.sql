CREATE TABLE [dbo].[tb_mes] (
    [idMes]      INT          NOT NULL,
    [smes]       VARCHAR (10) NOT NULL,
    [Nemonico]   VARCHAR (50) NOT NULL,
    [smesIngles] VARCHAR (20) NULL,
    CONSTRAINT [PK_tb_mes] PRIMARY KEY CLUSTERED ([idMes] ASC)
);

