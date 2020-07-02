CREATE TABLE [dbo].[tb_ColaCorreoParametros] (
    [IdEmpresa]             INT           NOT NULL,
    [Usuario]               VARCHAR (500) NOT NULL,
    [Contrasenia]           VARCHAR (500) NOT NULL,
    [Puerto]                INT           NOT NULL,
    [Host]                  VARCHAR (500) NOT NULL,
    [PermitirSSL]           BIT           NOT NULL,
    [CorreoCopia]           VARCHAR (200) NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    CONSTRAINT [PK_tb_ColaCorreoParametros] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC)
);

