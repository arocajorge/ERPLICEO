CREATE TABLE [dbo].[tb_moneda] (
    [IdMoneda]       INT          NOT NULL,
    [im_descripcion] VARCHAR (50) NULL,
    [im_simbolo]     CHAR (2)     NULL,
    [im_nemonico]    CHAR (5)     NULL,
    [Estado]         CHAR (1)     NULL,
    CONSTRAINT [PK_tb_moneda] PRIMARY KEY CLUSTERED ([IdMoneda] ASC)
);

