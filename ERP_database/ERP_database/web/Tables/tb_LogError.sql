CREATE TABLE [web].[tb_LogError] (
    [IdError]        NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [Fecha]          DATETIME       NOT NULL,
    [Descripcion]    VARCHAR (MAX)  NULL,
    [IdUsuario]      VARCHAR (50)   NULL,
    [Clase]          VARCHAR (1000) NULL,
    [Metodo]         VARCHAR (1000) NULL,
    [InnerException] VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_tb_LogError] PRIMARY KEY CLUSTERED ([IdError] ASC)
);



