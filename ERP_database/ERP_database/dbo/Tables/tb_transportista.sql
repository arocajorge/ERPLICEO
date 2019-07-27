CREATE TABLE [dbo].[tb_transportista] (
    [IdEmpresa]         INT           NOT NULL,
    [IdTransportista]   NUMERIC (18)  NOT NULL,
    [Cedula]            VARCHAR (20)  NULL,
    [Nombre]            VARCHAR (500) NULL,
    [Estado]            CHAR (1)      NOT NULL,
    [IdUsuario]         VARCHAR (20)  NULL,
    [Fecha_Transaccion] DATETIME      NULL,
    [IdUsuarioUltModi]  VARCHAR (20)  NULL,
    [Fecha_UltMod]      DATETIME      NULL,
    [IdUsuarioUltAnu]   VARCHAR (20)  NULL,
    [Fecha_UltAnu]      DATETIME      NULL,
    [MotivoAnulacion]   VARCHAR (100) NULL,
    [Placa]             VARCHAR (20)  NULL,
    CONSTRAINT [PK_tb_transportista] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTransportista] ASC)
);

