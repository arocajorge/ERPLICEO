CREATE TABLE [dbo].[ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar] (
    [IdEmpresa]    INT           NOT NULL,
    [IdNomina]     INT           NOT NULL,
    [IdNominaTipo] INT           NOT NULL,
    [IdCtaCble]    VARCHAR (20)  NOT NULL,
    [Observacion]  VARCHAR (100) NULL,
    [IdTipoFlujo]  NUMERIC (18)  NULL,
    CONSTRAINT [PK_ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNomina] ASC, [IdNominaTipo] ASC, [IdCtaCble] ASC),
    CONSTRAINT [FK_ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar_ro_Nomina_Tipoliqui] FOREIGN KEY ([IdEmpresa], [IdNomina], [IdNominaTipo]) REFERENCES [dbo].[ro_Nomina_Tipoliqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui])
);

