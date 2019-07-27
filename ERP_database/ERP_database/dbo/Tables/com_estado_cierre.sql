CREATE TABLE [dbo].[com_estado_cierre] (
    [IdEstado_cierre] VARCHAR (25)  NOT NULL,
    [Descripcion]     VARCHAR (50)  NOT NULL,
    [estado]          VARCHAR (1)   NOT NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (25)  NULL,
    [FechaHoraAnul]   DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (25)  NULL,
    [MotivoAnulacion] VARCHAR (100) NULL,
    CONSTRAINT [PK_com_estado_orden_compra] PRIMARY KEY CLUSTERED ([IdEstado_cierre] ASC)
);





