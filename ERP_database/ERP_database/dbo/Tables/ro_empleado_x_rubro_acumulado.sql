CREATE TABLE [dbo].[ro_empleado_x_rubro_acumulado] (
    [IdEmpresa]              INT          NOT NULL,
    [IdEmpleado]             NUMERIC (18) NOT NULL,
    [IdRubro]                VARCHAR (50) NOT NULL,
    [FechaIngresa]           DATETIME     NOT NULL,
    [UsuarioIngresa]         VARCHAR (25) NOT NULL,
    [FechaModifica]          DATETIME     NULL,
    [UsuarioModifica]        VARCHAR (25) NULL,
    [Fec_Inicio_Acumulacion] DATETIME     NULL,
    [Fec_Fin_Acumulacion]    DATETIME     NULL,
    CONSTRAINT [PK_ro_empleado_x_rubro_acumulado] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpleado] ASC, [IdRubro] ASC),
    CONSTRAINT [FK_ro_empleado_x_rubro_acumulado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_empleado_x_rubro_acumulado_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);

