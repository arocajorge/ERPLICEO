CREATE TABLE [dbo].[ro_rol_detalle] (
    [IdEmpresa]           INT           NOT NULL,
    [IdRol]               NUMERIC (18)  NOT NULL,
    [IdEmpleado]          NUMERIC (18)  NOT NULL,
    [IdRubro]             VARCHAR (50)  NOT NULL,
    [Orden]               INT           NOT NULL,
    [Valor]               FLOAT (53)    NOT NULL,
    [rub_visible_reporte] BIT           NULL,
    [Observacion]         VARCHAR (255) NULL,
    [IdSucursal]          INT           NOT NULL,
    CONSTRAINT [PK_ro_rol_detalle] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRol] ASC, [IdEmpleado] ASC, [IdRubro] ASC),
    CONSTRAINT [FK_ro_rol_detalle_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_rol_detalle_ro_rol] FOREIGN KEY ([IdEmpresa], [IdRol]) REFERENCES [dbo].[ro_rol] ([IdEmpresa], [IdRol]),
    CONSTRAINT [FK_ro_rol_detalle_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_rol_detalle_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);







