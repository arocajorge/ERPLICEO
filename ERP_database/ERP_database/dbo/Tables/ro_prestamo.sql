
CREATE TABLE [dbo].[ro_prestamo] (
    [IdEmpresa]          INT           NOT NULL,
    [IdPrestamo]         NUMERIC (18)  NOT NULL,
    [IdEmpleado]         NUMERIC (18)  NOT NULL,
    [IdRubro]            VARCHAR (50)  NOT NULL,
    [descuento_mensual]  BIT           NOT NULL,
    [descuento_quincena] BIT           NOT NULL,
    [descuento_men_quin] BIT           NOT NULL,
    [descuento_ben_soc]  BIT           NOT NULL,
    [Estado]             BIT           NOT NULL,
    [Fecha]              DATETIME      NOT NULL,
    [MontoSol]           FLOAT (53)    NOT NULL,
    [NumCuotas]          INT           NOT NULL,
    [Fecha_PriPago]      DATE          NOT NULL,
    [Observacion]        VARCHAR (MAX) NOT NULL,
    [IdEmpresa_dc]       INT           NULL,
    [IdTipoCbte]         INT           NULL,
    [IdCbteCble]         NUMERIC (18)  NULL,
    [IdEmpresa_op]       INT           NULL,
    [IdOrdenPago]        NUMERIC (18)  NULL,
    [IdUsuarioAprueba]   VARCHAR (50)  NULL,
    [EstadoAprob]        VARCHAR (10)  NULL,
    [IdUsuario]          VARCHAR (20)  NOT NULL,
    [Fecha_Transac]      DATETIME      NOT NULL,
    [IdUsuarioUltMod]    VARCHAR (20)  NULL,
    [Fecha_UltMod]       DATETIME      NULL,
    [IdUsuarioUltAnu]    VARCHAR (20)  NULL,
    [Fecha_UltAnu]       DATETIME      NULL,
    [MotiAnula]          VARCHAR (200) NULL,
    [cod_prestamo]       FLOAT (53)    NULL,
    [IdTipo_Persona]     VARCHAR (20)  NULL,
    [IdEntidad]          NUMERIC (18)  NULL,
    [IdPersona]          NUMERIC (18)  NULL,
    [GeneraOP]           BIT           NOT NULL,
    CONSTRAINT [PK_ro_prestamo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPrestamo] ASC),
    CONSTRAINT [FK_ro_prestamo_cp_orden_pago] FOREIGN KEY ([IdEmpresa_op], [IdOrdenPago]) REFERENCES [dbo].[cp_orden_pago] ([IdEmpresa], [IdOrdenPago]),
    CONSTRAINT [FK_ro_prestamo_ct_cbtecble] FOREIGN KEY ([IdEmpresa_dc], [IdTipoCbte], [IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_ro_prestamo_ro_catalogo] FOREIGN KEY ([EstadoAprob]) REFERENCES [dbo].[ro_catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_ro_prestamo_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_prestamo_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);







GO


GO


GO


GO


GO


GO


GO


GO


GO


GO


GO


