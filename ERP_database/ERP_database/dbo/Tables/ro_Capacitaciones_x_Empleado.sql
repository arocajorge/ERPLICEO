CREATE TABLE [dbo].[ro_Capacitaciones_x_Empleado] (
    [IdEmpresa]     INT           NOT NULL,
    [IdEmpleado]    NUMERIC (18)  NOT NULL,
    [Secuencia]     INT           NOT NULL,
    [Instititucion] VARCHAR (100) NOT NULL,
    [NombreCurso]   VARCHAR (200) NOT NULL,
    [Horas]         INT           NOT NULL,
    [Observacion]   VARCHAR (200) NULL,
    CONSTRAINT [PK_ro_Capacitaciones_x_Empleado] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpleado] ASC, [Secuencia] ASC)
);

