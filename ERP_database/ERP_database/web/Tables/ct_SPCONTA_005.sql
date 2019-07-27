CREATE TABLE [web].[ct_SPCONTA_005] (
    [IdEmpresa]             INT           NOT NULL,
    [IdPunto_cargo]         INT           NOT NULL,
    [IdUsuario]             VARCHAR (50)  NOT NULL,
    [IdPunto_cargo_grupo]   INT           NOT NULL,
    [nom_punto_cargo]       VARCHAR (MAX) NOT NULL,
    [nom_punto_cargo_grupo] VARCHAR (MAX) NOT NULL,
    [SaldoAnterior]         FLOAT (53)    NOT NULL,
    [Debitos]               FLOAT (53)    NOT NULL,
    [Creditos]              FLOAT (53)    NOT NULL,
    [SaldoFinal]            FLOAT (53)    NOT NULL,
    CONSTRAINT [PK_ct_SPCONTA_005] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPunto_cargo] ASC, [IdUsuario] ASC)
);

