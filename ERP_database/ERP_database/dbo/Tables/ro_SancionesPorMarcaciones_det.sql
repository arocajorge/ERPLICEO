CREATE TABLE [dbo].[ro_SancionesPorMarcaciones_det] (
    [IdEmpresa]     INT           NOT NULL,
    [IdAjuste]      INT           NOT NULL,
    [Secuencia]     INT           NOT NULL,
    [IdCalendario]  INT           NOT NULL,
    [IdEmpleado]    NUMERIC (18)  NOT NULL,
    [IdSucursal]    INT           NOT NULL,
    [EsHoraIngreso] TIME (7)      NOT NULL,
    [HoraIngreso]   TIME (7)      NOT NULL,
    [EsHoraSalida]  TIME (7)      NOT NULL,
    [HoraSalio]     TIME (7)      NOT NULL,
    [Minutos]       FLOAT (53)    NOT NULL,
    [Observacion]   VARCHAR (MAX) NULL,
    [FechaRegistro] DATE          NOT NULL,
    CONSTRAINT [PK_ro_SancionesPorMarcaciones_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAjuste] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_SancionesPorMarcaciones_det_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_SancionesPorMarcaciones_det_ro_SancionesPorMarcaciones] FOREIGN KEY ([IdEmpresa], [IdAjuste]) REFERENCES [dbo].[ro_SancionesPorMarcaciones] ([IdEmpresa], [IdAjuste])
);



