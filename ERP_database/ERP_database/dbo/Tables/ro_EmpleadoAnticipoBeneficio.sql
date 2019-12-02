CREATE TABLE [dbo].[ro_EmpleadoAnticipoBeneficio] (
    [IdEmpresa]             INT           NOT NULL,
    [IdAnticipo]            NUMERIC (18)  NOT NULL,
    [IdEmpleado]            NUMERIC (18)  NOT NULL,
    [FechaDesde]            DATE          NOT NULL,
    [FechaHasta]            DATE          NOT NULL,
    [IdRubro]               VARCHAR (50)  NOT NULL,
    [Valor]                 FLOAT (53)    NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ro_EmpleadoAnticipoBeneficio] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnticipo] ASC),
    CONSTRAINT [FK_ro_EmpleadoAnticipoBeneficio_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_EmpleadoAnticipoBeneficio_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);



