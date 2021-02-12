CREATE TABLE [dbo].[aca_parametro] (
    [IdEmpresa]              INT           NOT NULL,
    [RutaImagen_Alumno]      VARCHAR (500) NULL,
    [RutaImagen_Profesor]    VARCHAR (500) NULL,
    [RutaImagen_Seguimiento] VARCHAR (500) NULL,
    [FtpUser]                VARCHAR (500) NULL,
    [FtpPassword]            VARCHAR (500) NULL,
    [FtpUrl]                 VARCHAR (500) NULL,
    CONSTRAINT [PK_aca_parametro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC)
);



