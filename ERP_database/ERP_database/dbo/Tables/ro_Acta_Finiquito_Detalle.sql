CREATE TABLE [dbo].[ro_Acta_Finiquito_Detalle] (
    [IdEmpresa]       INT           NOT NULL,
    [IdActaFiniquito] NUMERIC (18)  NOT NULL,
    [IdSecuencia]     INT           NOT NULL,
    [IdRubro]         VARCHAR (50)  NOT NULL,
    [Observacion]     VARCHAR (250) NOT NULL,
    [Valor]           FLOAT (53)    NOT NULL,
    CONSTRAINT [PK_ro_Acta_Finiquito_Detalle_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdActaFiniquito] ASC, [IdSecuencia] ASC),
    CONSTRAINT [FK_ro_Acta_Finiquito_Detalle_ro_Acta_Finiquito] FOREIGN KEY ([IdEmpresa], [IdActaFiniquito]) REFERENCES [dbo].[ro_Acta_Finiquito] ([IdEmpresa], [IdActaFiniquito]),
    CONSTRAINT [FK_ro_Acta_Finiquito_Detalle_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);

