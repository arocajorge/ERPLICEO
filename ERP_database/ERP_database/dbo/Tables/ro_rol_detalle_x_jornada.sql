CREATE TABLE [dbo].[ro_rol_detalle_x_jornada] (
    [IdEmpresa]           INT           NOT NULL,
    [IdRol]               NUMERIC (18)  NOT NULL,
    [IdEmpleado]          NUMERIC (18)  NOT NULL,
    [IdRubro]             VARCHAR (50)  NOT NULL,
    [IdJornada]           INT           NOT NULL,
    [Orden]               INT           NOT NULL,
    [Valor]               FLOAT (53)    NOT NULL,
    [rub_visible_reporte] BIT           NULL,
    [Observacion]         VARCHAR (255) NULL,
    [IdSucursal]          INT           NOT NULL,
    CONSTRAINT [PK_ro_rol_detalle_x_jornada] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRol] ASC, [IdEmpleado] ASC, [IdRubro] ASC, [IdJornada] ASC)
);

