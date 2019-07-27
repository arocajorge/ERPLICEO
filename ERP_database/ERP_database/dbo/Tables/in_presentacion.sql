CREATE TABLE [dbo].[in_presentacion] (
    [IdEmpresa]        INT           NOT NULL,
    [IdPresentacion]   VARCHAR (25)  NOT NULL,
    [nom_presentacion] VARCHAR (150) NOT NULL,
    [estado]           CHAR (1)      NOT NULL,
    CONSTRAINT [PK_in_presentacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPresentacion] ASC)
);

