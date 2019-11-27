CREATE TABLE [dbo].[ro_rol_detalle_x_ro_rubro_fijo] (
    [IdEmpresa]  INT          NOT NULL,
    [IdRol]      NUMERIC (18) NOT NULL,
    [IdEmpleado] NUMERIC (18) NOT NULL,
    [IdRubro]    VARCHAR (50) NOT NULL,
    [IdDivision] INT          NULL,
    [IdArea]     INT          NULL,
    [Valor]      FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_ro_rol_detalle_x_ro_rubro_fijo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRol] ASC, [IdEmpleado] ASC, [IdRubro] ASC)
);

