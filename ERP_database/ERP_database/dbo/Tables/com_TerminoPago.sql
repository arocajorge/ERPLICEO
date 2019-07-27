CREATE TABLE [dbo].[com_TerminoPago] (
    [IdEmpresa]       INT           NOT NULL,
    [IdTerminoPago]   INT           NOT NULL,
    [Descripcion]     VARCHAR (500) NOT NULL,
    [Dias]            INT           NOT NULL,
    [Estado]          VARCHAR (1)   NOT NULL,
    [IdUsuario]       VARCHAR (50)  NULL,
    [IdUsuarioUltMod] VARCHAR (50)  NULL,
    [FechaUltMod]     DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (50)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [MotiAnula]       VARCHAR (50)  NULL,
    CONSTRAINT [PK_in_TerminoPago] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTerminoPago] ASC)
);





