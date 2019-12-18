CREATE TABLE [dbo].[ro_AjusteImpuestoRentaDetOI] (
    [IdEmpresa]     INT             NOT NULL,
    [IdAjuste]      NUMERIC (18)    NOT NULL,
    [IdEmpleado]    NUMERIC (18)    NOT NULL,
    [Secuencia]     INT             NOT NULL,
    [DescripcionOI] VARCHAR (1000)  NOT NULL,
    [Valor]         NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_ro_AjusteImpuestoRentaDetOI] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAjuste] ASC, [IdEmpleado] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_AjusteImpuestoRentaDetOI_ro_AjusteImpuestoRenta] FOREIGN KEY ([IdEmpresa], [IdAjuste]) REFERENCES [dbo].[ro_AjusteImpuestoRenta] ([IdEmpresa], [IdAjuste]),
    CONSTRAINT [FK_ro_AjusteImpuestoRentaDetOI_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado])
);

