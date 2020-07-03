CREATE TABLE [dbo].[tb_ColaCorreo] (
    [IdEmpresa]         INT            NOT NULL,
    [IdCorreo]          NUMERIC (18)   NOT NULL,
    [Codigo]            VARCHAR (50)   NOT NULL,
    [Destinatarios]     VARCHAR (1000) NOT NULL,
    [Asunto]            VARCHAR (1000) NOT NULL,
    [Cuerpo]            VARCHAR (MAX)  NOT NULL,
    [Parametros]        VARCHAR (200)  NOT NULL,
    [IdUsuarioCreacion] VARCHAR (50)   NOT NULL,
    [FechaCreacion]     DATE           NOT NULL,
    [FechaEnvio]        DATETIME       NULL,
    [Error]             VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_tb_ColaCorreo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCorreo] ASC)
);



