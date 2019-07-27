CREATE TABLE [web].[ba_SPBAN_012] (
    [IdEmpresa]       INT           NOT NULL,
    [IdBanco]         INT           NOT NULL,
    [IdTipoFlujo]     INT           NOT NULL,
    [IdUsuario]       VARCHAR (50)  NOT NULL,
    [ba_descripcion]  VARCHAR (500) NOT NULL,
    [nom_tipo_flujo]  VARCHAR (500) NOT NULL,
    [SaldoInicial]    FLOAT (53)    NOT NULL,
    [Ingresos]        FLOAT (53)    NOT NULL,
    [Egresos]         FLOAT (53)    NOT NULL,
    [SaldoFinal]      FLOAT (53)    NOT NULL,
    [SaldoFinalBanco] FLOAT (53)    NOT NULL,
    CONSTRAINT [PK_ba_SPBAN_012] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdBanco] ASC, [IdTipoFlujo] ASC, [IdUsuario] ASC)
);

