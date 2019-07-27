CREATE TABLE [dbo].[ro_Tipo_Prestamo] (
    [IdEmpresa]       INT           NOT NULL,
    [IdTipoPrestamo]  INT           NOT NULL,
    [tp_Descripcion]  VARCHAR (50)  NULL,
    [tp_Monto]        INT           NULL,
    [tp_Estado]       VARCHAR (1)   NULL,
    [IdUsuario]       VARCHAR (50)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (25)  NULL,
    [FechaHoraAnul]   DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (25)  NULL,
    [MotivoAnulacion] VARCHAR (100) NULL,
    CONSTRAINT [PK_ro_Tipo_Prestamo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipoPrestamo] ASC),
    CONSTRAINT [FK_ro_Tipo_Prestamo_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);

