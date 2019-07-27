CREATE TABLE [dbo].[ro_area_x_departamento] (
    [IdEmpresa]      INT NOT NULL,
    [Secuencia]      INT NOT NULL,
    [IdDivision]     INT NOT NULL,
    [IdArea]         INT NOT NULL,
    [IdDepartamento] INT NOT NULL,
    CONSTRAINT [PK_ro_area_x_departamento] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_area_x_departamento_ro_area] FOREIGN KEY ([IdEmpresa], [IdDivision], [IdArea]) REFERENCES [dbo].[ro_area] ([IdEmpresa], [IdDivision], [IdArea]),
    CONSTRAINT [FK_ro_area_x_departamento_ro_Departamento] FOREIGN KEY ([IdEmpresa], [IdDepartamento]) REFERENCES [dbo].[ro_Departamento] ([IdEmpresa], [IdDepartamento])
);

