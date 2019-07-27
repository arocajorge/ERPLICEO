CREATE TABLE [dbo].[tb_banco] (
    [IdBanco]                   INT           NOT NULL,
    [ba_descripcion]            VARCHAR (100) NOT NULL,
    [Estado]                    CHAR (1)      NOT NULL,
    [CodigoLegal]               VARCHAR (10)  NOT NULL,
    [TieneFormatoTransferencia] BIT           NOT NULL,
    CONSTRAINT [PK_tb_banco] PRIMARY KEY CLUSTERED ([IdBanco] ASC)
);

