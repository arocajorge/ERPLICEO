CREATE TABLE [dbo].[ro_empleado_novedad_det] (
    [IdEmpresa]     INT           NOT NULL,
    [IdNovedad]     NUMERIC (18)  NOT NULL,
    [Secuencia]     INT           NOT NULL,
    [Observacion]   VARCHAR (200) NULL,
    [IdRubro]       VARCHAR (50)  NULL,
    [FechaPago]     DATETIME      NOT NULL,
    [Valor]         FLOAT (53)    NOT NULL,
    [EstadoCobro]   VARCHAR (50)  NOT NULL,
    [CantidadHoras] FLOAT (53)    NULL,
    CONSTRAINT [PK_ro_empleado_novedad_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNovedad] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_empleado_novedad_det_ro_empleado_Novedad] FOREIGN KEY ([IdEmpresa], [IdNovedad]) REFERENCES [dbo].[ro_empleado_Novedad] ([IdEmpresa], [IdNovedad]),
    CONSTRAINT [FK_ro_empleado_novedad_det_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);





