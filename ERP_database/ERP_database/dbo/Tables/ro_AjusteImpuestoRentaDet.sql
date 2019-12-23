CREATE TABLE [dbo].[ro_AjusteImpuestoRentaDet] (
    [IdEmpresa]              INT             NOT NULL,
    [IdAjuste]               NUMERIC (18)    NOT NULL,
    [Secuencia]              INT             NOT NULL,
    [IdEmpleado]             NUMERIC (18)    NOT NULL,
    [SueldoFechaCorte]       NUMERIC (18, 2) NOT NULL,
    [SueldoProyectado]       NUMERIC (18, 2) NOT NULL,
    [OtrosIngresos]          NUMERIC (18, 2) NOT NULL,
    [IngresosLiquidos]       NUMERIC (18, 2) NOT NULL,
    [GastosPersonales]       NUMERIC (18, 2) NOT NULL,
    [AporteFechaCorte]       NUMERIC (18, 2) NOT NULL,
    [BaseImponible]          NUMERIC (18, 2) NOT NULL,
    [FraccionBasica]         NUMERIC (18, 2) NOT NULL,
    [Excedente]              NUMERIC (18, 2) NOT NULL,
    [ImpuestoFraccionBasica] NUMERIC (18, 2) NOT NULL,
    [ImpuestoRentaCausado]   NUMERIC (18, 2) NOT NULL,
    [DescontadoFechaCorte]   NUMERIC (18, 2) NOT NULL,
    [LiquidacionFinal]       NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_ro_AjusteImpuestoRentaDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAjuste] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_AjusteImpuestoRentaDet_ro_AjusteImpuestoRenta] FOREIGN KEY ([IdEmpresa], [IdAjuste]) REFERENCES [dbo].[ro_AjusteImpuestoRenta] ([IdEmpresa], [IdAjuste]),
    CONSTRAINT [FK_ro_AjusteImpuestoRentaDet_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado])
);



