CREATE TABLE [dbo].[ro_novedad_x_empleado] (
    [IdEmpresa]              INT           NOT NULL,
    [IdTransaccion]          DECIMAL (18)  NOT NULL,
    [IdEmpresa_Emp_Novedad]  INT           NOT NULL,
    [IdNovedad_Emp_Novedad]  NUMERIC (18)  NOT NULL,
    [IdEmpleado_Emp_Novedad] NUMERIC (18)  NOT NULL,
    [Observacion]            VARCHAR (150) NOT NULL,
    [estado]                 CHAR (1)      NOT NULL,
    [Fecha]                  DATETIME      NOT NULL,
    [IdNomina_Tipo]          INT           NOT NULL,
    [IdNomina_TipoLiqui]     INT           NOT NULL,
    [IdRubro]                VARCHAR (10)  NOT NULL,
    CONSTRAINT [PK_ro_Novedad_x_Empleado] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTransaccion] ASC, [IdEmpresa_Emp_Novedad] ASC, [IdNovedad_Emp_Novedad] ASC, [IdEmpleado_Emp_Novedad] ASC)
);



