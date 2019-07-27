CREATE TABLE [dbo].[tb_dia] (
    [idDia]      TINYINT      NOT NULL,
    [sdia]       VARCHAR (15) NULL,
    [nemonico]   NCHAR (5)    NULL,
    [sdiaIngles] VARCHAR (15) NULL,
    CONSTRAINT [PK_tb_dia] PRIMARY KEY CLUSTERED ([idDia] ASC)
);

