CREATE TABLE [dbo].[ro_HorasProfesores_det] (
    [IdEmpresa]     INT           NOT NULL,
    [IdCarga]       NUMERIC (18)  NOT NULL,
    [Secuencia]     INT           NOT NULL,
    [IdRubro]       VARCHAR (50)  NOT NULL,
    [IdEmpresa_nov] INT           NOT NULL,
    [IdNovedad]     NUMERIC (18)  NOT NULL,
    [Observacion]   VARCHAR (MAX) NULL,
    [IdEmpleado]    NUMERIC (18)  NOT NULL,
    [NumHoras]      FLOAT (53)    NOT NULL,
    [IdSucursal]    INT           NOT NULL,
    [ValorHora]     FLOAT (53)    NOT NULL,
    CONSTRAINT [PK_ro_HorasProfesores_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCarga] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_HorasProfesores_det_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_HorasProfesores_det_ro_empleado_Novedad] FOREIGN KEY ([IdEmpresa], [IdNovedad]) REFERENCES [dbo].[ro_empleado_Novedad] ([IdEmpresa], [IdNovedad]),
    CONSTRAINT [FK_ro_HorasProfesores_det_ro_HorasProfesores] FOREIGN KEY ([IdEmpresa], [IdCarga]) REFERENCES [dbo].[ro_HorasProfesores] ([IdEmpresa], [IdCarga]),
    CONSTRAINT [FK_ro_HorasProfesores_det_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);



