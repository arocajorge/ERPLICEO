CREATE TABLE [dbo].[tb_ColaCorreoCodigo] (
    [IdEmpresa] INT            NOT NULL,
    [Codigo]    VARCHAR (50)   NOT NULL,
    [Asunto]    VARCHAR (1000) NOT NULL,
    [Cuerpo]    VARCHAR (MAX)  NOT NULL,
    CONSTRAINT [PK_tb_ColaCorreoCodigo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [Codigo] ASC)
);

